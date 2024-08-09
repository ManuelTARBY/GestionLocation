using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifChargeAnnuelle : Form
    {

        private MySqlCommand command;
        private readonly Dictionary<string, string> infoBien;
        private string idCharge;
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
            this.infoBien = fenListeCharges.GetLeBien();
            this.idCharge = idCharge;
            if (this.idCharge.Equals("0"))
            {
                this.typeReq = "INSERT";
                // Attribution d'un numéro de charge
                this.req = "SELECT MAX(idchargeannuelle) + 1 AS 'idMaxi' FROM chargesannuelles";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.idCharge = reader["idMaxi"].ToString();
                reader.Close();
            }
            else
            {
                this.typeReq = "UPDATE";
                cobListeBien.Enabled = false;
            }
            RemplirComboListeBien();
            RemplirComboFreq();
            RemplirChamps();
        }

        /// <summary>
        /// Remplit la comboi avec la liste des biens et des groupes de bien
        /// </summary>
        public void RemplirComboListeBien()
        {
            // Les biens
            this.req = "SELECT nombien FROM bien ORDER BY nombien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                cobListeBien.Items.Add($"{reader["nombien"]}");
                finCurseur = !reader.Read();
            }
            reader.Close();

            // Les groupes de bien
            this.req = "SELECT nomdugroupe FROM grpedebiens ORDER BY nomdugroupe";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            finCurseur = !reader.Read();
            while (!finCurseur)
            {
                cobListeBien.Items.Add($"{reader["nomdugroupe"]}");
                finCurseur = !reader.Read();
            }
            reader.Close();
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
            //txtBien.Text = this.infoBien["nom"];
            // Si on est dans le cas d'une création
            if (this.typeReq.Equals("INSERT"))
            {
                lblID.Text = $"{AttribuerIDCharge()}";
            }
            // Si on est dans le cas d'une modification
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
            this.req = "SELECT libelle, montantcharge, refFrequence, imputable, annee, nombien " +
                $"FROM chargesannuelles NATURAL JOIN bien WHERE idchargeannuelle = {this.idCharge}";
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
            cobListeBien.SelectedItem = $"{reader["nombien"]}";
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
                // Récupère l'id du bien
                List<string> lesId = new List<string>();
                this.req = $"SELECT idbien, nombien FROM bien WHERE nombien = \'{cobListeBien.SelectedItem}\'";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                MySqlDataReader readerBien = this.command.ExecuteReader();
                if (readerBien.HasRows)
                {
                    readerBien.Read();
                    lesId.Add(readerBien["idbien"].ToString());
                    this.infoBien["id"] = readerBien["idbien"].ToString();
                    this.infoBien["nom"] = readerBien["nombien"].ToString();
                    readerBien.Close();
                }
                else
                {
                    readerBien.Close();
                    this.req = "SELECT idbien FROM lignegroupe WHERE idgroupe = " +
                        $"(SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = \'{cobListeBien.SelectedItem}\')";
                    this.command = new MySqlCommand(this.req, Global.Connexion);
                    this.command.Prepare();
                    MySqlDataReader readerGrpe = this.command.ExecuteReader();
                    bool finCurseur = !readerGrpe.Read();
                    while (!finCurseur)
                    {
                        lesId.Add(readerGrpe["idbien"].ToString());
                        finCurseur = !readerGrpe.Read();
                    }
                    readerGrpe.Close();
                }

                // Valeur annuelle de la charge
                string annu = CalculerMontantAnnuel();
                try
                {
                    annu = (Math.Round(float.Parse(annu.Replace('.', ',')) / lesId.Count, 2)).ToString();
                }
                catch
                {
                    Console.WriteLine("Impossible de convertir le montant annuel en float.");
                }

                // Prépare la requête d'ajout ou de modification de l'enregistrement de ChargesAnnuelles
                string montantCharge = "0";
                foreach (string id in lesId)
                {
                    try
                    {
                        montantCharge = ((float) Math.Round(float.Parse(txtMontant.Text.Replace('.', ',')) / lesId.Count, 2)).ToString();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("The string is not a valid float.");
                    }
                    switch (this.typeReq)
                    {
                        case "INSERT":
                            this.req = "INSERT INTO chargesannuelles (idchargeannuelle, idbien, libelle, refFrequence, annee, " +
                                "montantcharge, chargeannuelle, imputable) " +
                                $"VALUES ({this.idCharge}, {id}, @libelle, \'{cobFrequence.SelectedItem}\', " +
                                $"\'{txtAnnee.Text}\', \'{montantCharge.Replace(',', '.')}\', \'{annu.Replace(',', '.')}\', {cbxImputable.Checked})";
                            break;
                        case "UPDATE":
                            this.req = "UPDATE chargesannuelles "+
                                $"SET idbien = {id}, libelle = @libelle, refFrequence = \'{cobFrequence.SelectedItem}\', "+
                                $"annee = \'{txtAnnee.Text}\', montantcharge = \'{montantCharge.Replace(',', '.')}\', chargeannuelle = \'{annu.Replace(',', '.')}\', "+
                                $"imputable = {cbxImputable.Checked} WHERE idchargeannuelle = {this.idCharge}";
                            break;
                    }
                
                    // Exécute la requête
                    this.command = new MySqlCommand(this.req, Global.Connexion);
                    this.command.Parameters.AddWithValue("@libelle", txtLibelle.Text);
                    this.command.Prepare();
                    this.command.ExecuteNonQuery();
                    this.fenListeCharges.RecupListeCharges();

                    this.idCharge = (int.Parse(this.idCharge) + 1).ToString();

                    // Met à jour la liste des charges pour le bien
                    MajChargesDuBien(id);
                }

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
        /// Calcule le montant annuel de la charge en fonction du montant et de la fréquence renseignée
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
            // Calcule le montant annuel de la charge
            float totalAnnuel = (occurrence * MontantVirg());
            totalAnnuel = (float)Math.Round(totalAnnuel, 2);
            string annu = totalAnnuel.ToString();
            return annu.Replace(',', '.');
        }

        /// <summary>
        /// Vérifie que les champs obligatoires soient remplis
        /// </summary>
        /// <returns>Vrai si tous les champs sont remplis, faux dans le cas contraire</returns>
        private bool VerifChamps()
        {
            // Cas d'erreur
            bool annee = VerifAnnee();
            if (txtLibelle.Text.Equals("") || txtMontant.Text.Equals("") || cobFrequence.SelectedItem == null || MontantVirg() == 0 || annee == false || cobListeBien.SelectedItem == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Met à jour la table bien au niveau du montant total des charges annuelles et des charges imputables
        /// </summary>
        public void MajChargesDuBien(string idBien)
        {
            // Calcule la charge annuelle du bien
            float charges = 0;
            this.req = "SELECT SUM(chargeannuelle) AS 'TotalCharges' FROM chargesannuelles " +
                $"WHERE idbien = {idBien} AND annee = YEAR(NOW())";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                charges = float.Parse(reader["TotalCharges"].ToString());
                reader.Close();
            }

            // Calcule la charge imputable au locataire pour le bien
            this.req += " AND imputable = True";
            float chImputables = 0;
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                chImputables = float.Parse(reader["TotalCharges"].ToString());
                reader.Close();
            }

            // Met à jour les champs charges annuelles et charges imputables de la table bien
            string chImput = ((float)Math.Round(chImputables/12, 2)).ToString();
            chImput = chImput.Replace(',', '.');
            this.req = $"UPDATE bien SET chargeannuelles = \'{Math.Round(charges)}\', chargesimputables = \'{chImput}\' " +
                $"WHERE idbien = {idBien}";
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
/*            if (cobFrequence.SelectedItem.Equals("Ponctuelle"))
            {*/
                if (int.TryParse(txtAnnee.Text, out _))
                {
                    return true;
                }
                else
                {
                    return false;
                }
/*            }
            else
            {
                return true;
            }*/
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


/*        /// <summary>
        /// Gère les changements de fréquences
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobFrequence_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAnnee.Visible = cobFrequence.SelectedItem.Equals("Ponctuelle");
            lblAnnee.Visible = cobFrequence.SelectedItem.Equals("Ponctuelle");
        }*/
    }
}
