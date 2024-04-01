using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifChargeAnnuelle : Form
    {

        private MySqlCommand command;
        private readonly string[] leBien;
        private readonly string idCharge;
        private string req;
        private readonly string typeReq;
        private readonly ListeCharges fenListeCharges;

        /// <summary>
        /// Constructeur de la fenêtre AjoutModifChargeAnnuelle
        /// </summary>
        /// <param name="fenListeCharges">Instance de la fenêtre ListeCharge</param>
        /// <param name="idCharge">Id de la charge annuelle</param>
        public AjoutModifChargeAnnuelle(ListeCharges fenListeCharges, string idCharge = "0")
        {
            InitializeComponent();
            this.Text = "Ajout/Modification d'une charge";
            this.fenListeCharges = fenListeCharges;
            this.leBien = fenListeCharges.GetLeBien();
            this.idCharge = idCharge;
            if (this.idCharge.Equals("0"))
            {
                this.typeReq = "INSERT";
            }
            else
            {
                this.typeReq = "UPDATE";
            }
            RemplirComboFreq();
            RemplirChamps();
        }

        /// <summary>
        /// Remplit le champ combo avec les fréquences
        /// </summary>
        public void RemplirComboFreq()
        {
            this.req = $"SELECT libelle FROM frequencepaiement";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                cobFrequence.Items.Add($"{reader["libelle"]}");
                finCurseur = !reader.Read();
            }
            reader.Close();
        }
        
        /// <summary>
        /// Gère le remplissage des différents champs de la fenêtre
        /// </summary>
        public void RemplirChamps()
        {
            txtBien.Text = this.leBien[1];
            if (this.idCharge.Equals("0"))
            {
                lblID.Text = $"{AttribuerIDCharge()}";
            }
            else
            {
                lblID.Text = $"{this.idCharge}";
                RecupDonnees();
            }
        }

        /// <summary>
        /// Attribue un id à la charge annuelle en cours de création
        /// </summary>
        public string AttribuerIDCharge()
        {
            this.req = $"SELECT MAX(idchargeannuelle) FROM (SELECT idchargeannuelle FROM chargesannuelles) AS req";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string idNouv;
            try
            {
               idNouv = (reader.GetInt32(0) + 1).ToString();
            }
            catch
            {
                idNouv = "1";
            }
            reader.Close();
            return idNouv;
        }

        /// <summary>
        /// Récupère les données de la charge qui est en train d'être modifiée
        /// </summary>
        public void RecupDonnees()
        {
            this.req = $"SELECT libelle, montantcharge, refFrequence, imputable, annee FROM chargesannuelles WHERE idchargeannuelle = {this.idCharge}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            txtLibelle.Text = $"{reader["libelle"]}";
            txtMontant.Text = $"{reader["montantcharge"]}";
            txtAnnee.Text = $"{reader["annee"]}";
            // Récupère le statut de "Imputable"
            if ((bool)reader["imputable"])
            {
                cbxImputable.Checked = true;
            }
            else
            {
                cbxImputable.Checked = false;
            }
            // Récupère la fréquence de paiement
            cobFrequence.SelectedItem = $"{reader["refFrequence"]}";
            reader.Close();
        }

        /// <summary>
        /// Valide l'enregistrement de la charge annuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (VerifChamps())
            {
                string annu = CalculerMontantAnnuel();
                // Prépare la requête d'ajout ou de modification de l'enregistrement de ChargesAnnuelles
                switch (this.typeReq)
                {
                    case "INSERT":
                        this.req = "INSERT INTO chargesannuelles (idchargeannuelle, idbien, libelle, refFrequence, annee, "+
                            "montantcharge, chargeannuelle, imputable) "+
                            $"VALUES ({lblID.Text}, {this.leBien[0]}, @libelle, \'{cobFrequence.SelectedItem}\', "+
                            $"\'{txtAnnee.Text}\', \'{MontantPoint()}\', \'{annu}\', {cbxImputable.Checked})";
                        break;
                    case "UPDATE":
                        this.req = "UPDATE chargesannuelles "+
                            $"SET idbien = {leBien[0]}, libelle = @libelle, refFrequence = \'{cobFrequence.SelectedItem}\', "+
                            $"annee = \'{txtAnnee.Text}\', montantcharge = \'{MontantPoint()}\', chargeannuelle = \'{annu}\', "+
                            $"imputable = {cbxImputable.Checked} WHERE idchargeannuelle = {this.idCharge}";
                        break;
                }
                // Exécute la requête
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Parameters.AddWithValue("@libelle", txtLibelle.Text);
                // Prépare la requête
                this.command.Prepare();
                // Exécute la requête
                this.command.ExecuteNonQuery();
                this.fenListeCharges.RecupListeCharges();
                // Met à jour la liste des charges pour le bien
                MajChargesDuBien();
                FicheBien laFicheBien = this.fenListeCharges.GetFenFicheBien();
                if (laFicheBien != null)
                {
                    laFicheBien.RemplirChamps();
                }
                this.Dispose();
            }
            else
            {
                if (MontantVirg() == 0)
                {
                    MessageBox.Show("Veuillez remplir un montant correct.");
                }
                else if (VerifAnnee() == false)
                {
                    MessageBox.Show("Veuillez saisir une année correcte.");
                }
                else
                {
                    MessageBox.Show("Veuillez remplir tous les champs pour pouvoir valider la saisie.");
                }
            }
        }

        /// <summary>
        /// Calcule le montant annuel de la charge en fonction du montant et de la fréquence renseignés
        /// </summary>
        /// <returns></returns>
        private string CalculerMontantAnnuel()
        {
            // Récupère l'occurrence de la charge
            this.req = $"SELECT occurrence FROM frequencepaiement WHERE libelle = \'{cobFrequence.SelectedItem}\'";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            float occurrence = (float)reader["occurrence"];
            reader.Close();
            float totalAnnuel = (occurrence * MontantVirg());
            totalAnnuel = (float)Math.Round(totalAnnuel, 2);
            string annu = totalAnnuel.ToString();
            annu = annu.Replace(',', '.');
            return annu;
        }

        /// <summary>
        /// Vérifie que les champs obligatoires soient remplis
        /// </summary>
        /// <returns>Vrai si tous les champs sont remplis, faux dans le cas contraire</returns>
        private bool VerifChamps()
        {
            // Cas d'erreur
            bool annee = VerifAnnee();
            if (txtLibelle.Text.Equals("") || txtMontant.Text.Equals("") || cobFrequence.SelectedItem == null || MontantVirg() == 0 || annee == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Met à jour la table bien au niveau du montant total des charges annuelles
        /// </summary>
        public void MajChargesDuBien()
        {
            // Calcule la charge annuelle du bien
            float charges = 0;
            this.req = $"SELECT chargeannuelle FROM chargesannuelles WHERE idbien = {leBien[0]}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                charges += float.Parse(reader["chargeannuelle"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();
            // Calcule la charge imputable au locataire pour le bien
            this.req += " AND imputable = True";
            float chImputables = 0;
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            finCurseur = !reader.Read();
            while (!finCurseur)
            {
                chImputables += float.Parse(reader["chargeannuelle"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();

            // Met à jour la table bien
            this.req = $"UPDATE bien SET chargeannuelles = \'{Math.Round(charges)}\', chargesimputables = \'{Math.Round(chImputables / 12)}\' WHERE idbien = {leBien[0]}";
            // Exécute la requête
            this.command = new MySqlCommand(this.req, Global.Connexion);
            // Prépare la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }


        /// <summary>
        /// Vérifie si l'année saisie est correcte
        /// </summary>
        /// <returns>true si l'année est correcte, sinon false</returns>
        public bool VerifAnnee()
        {
            if (cobFrequence.SelectedItem.Equals("Ponctuelle"))
            {
                if (int.TryParse(txtAnnee.Text, out _))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Rend le contenu du champ txtMontant convertible en float pour permettre le calcul du montant annuel
        /// </summary>
        /// <param name="chaine"></param>
        /// <returns>Chaîne initiale convertie en nombre flottant</returns>
        public float MontantVirg()
        {
            string chaine = txtMontant.Text;
            float result;
            if (chaine.Contains('.'))
            {
                chaine = chaine.Replace('.', ',');
            }
            try
            {
                result = float.Parse(chaine);
                return result;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Rend le champ txtMontant lisible par une requête
        /// </summary>
        /// <returns>Chaîne du montant à insérer dans la requête</returns>
        public string MontantPoint()
        {
            string chaine = txtMontant.Text;
            if (chaine.Contains(','))
            {
                chaine = chaine.Replace(',', '.');
            }
            return chaine;
        }


        /// <summary>
        ///  Ferme le fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Gère les changements de fréquences
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobFrequence_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAnnee.Visible = cobFrequence.SelectedItem.Equals("Ponctuelle");
            lblAnnee.Visible = cobFrequence.SelectedItem.Equals("Ponctuelle");
        }
    }
}
