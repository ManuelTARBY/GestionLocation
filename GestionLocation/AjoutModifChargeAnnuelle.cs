using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifChargeAnnuelle : Form
    {

        private readonly MySqlConnection connexion;
        private MySqlCommand command;
        private readonly string[] leBien;
        private readonly string idCharge;
        private string req;
        private readonly string typeReq;
        private readonly ListeCharges fenListeCharges;

        //public AjoutModifChargeAnnuelle(MySqlConnection connexion, string[] leBien, string idCharge = "0")
        public AjoutModifChargeAnnuelle(ListeCharges fenListeCharges, string idCharge = "0")
        {
            this.fenListeCharges = fenListeCharges;
            this.connexion = fenListeCharges.GetConnexion();
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
            InitializeComponent();
            RemplirComboFreq();
            RemplirChamps();
        }

        /// <summary>
        /// Remplit le champ combo avec les fréquences
        /// </summary>
        public void RemplirComboFreq()
        {
            this.req = $"SELECT libelle FROM frequencepaiement";
            this.command = new MySqlCommand(this.req, this.connexion);
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
                lblID.Text = AttribuerIDCharge();
            }
            else
            {
                lblID.Text = this.idCharge;
                RecupDonnees();
            }
        }

        /// <summary>
        /// Attribue un id à la charge annuelle en cours de création
        /// </summary>
        public string AttribuerIDCharge()
        {
            this.req = $"SELECT MAX(idchargeannuelle) FROM (SELECT idchargeannuelle FROM chargesannuelles WHERE idbien = {this.leBien[0]}) AS req";
            this.command = new MySqlCommand(this.req, this.connexion);
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
            this.req = $"SELECT libelle, montantcharge, refFrequence, imputable FROM chargesannuelles WHERE idchargeannuelle = {this.idCharge}";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            txtLibelle.Text = $"{reader["libelle"]}";
            txtMontant.Text = $"{reader["montantcharge"]}";
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
                // Prépare la requête
                switch (this.typeReq)
                {
                    case "INSERT":
                        this.req = "INSERT INTO chargesannuelles (idchargeannuelle, idbien, libelle, refFrequence, montantcharge, chargeannuelle, imputable) " +
                            $"VALUES ({lblID.Text}, {this.leBien[0]}, \'{txtLibelle.Text}\', " +
                            $"\'{cobFrequence.SelectedItem}\', {txtMontant.Text}, {CalculerMontantAnnuel()}, {cbxImputable.Checked})";
                        break;
                    case "UPDATE":
                        this.req = $"UPDATE chargesannuelles SET idbien = {leBien[0]}, libelle = \'{txtLibelle.Text}\', refFrequence = \'{cobFrequence.SelectedItem}\', " +
                            $"montantcharge = \'{txtMontant.Text}\', chargeannuelle = \'{CalculerMontantAnnuel()}\', imputable = {cbxImputable.Checked} " +
                            $"WHERE idchargeannuelle = {this.idCharge}";
                        break;
                }
                // Exécute la requête
                this.command = new MySqlCommand(this.req, this.connexion);
                // Prépare la requête
                this.command.Prepare();
                // exécution de la requête
                this.command.ExecuteNonQuery();
                this.fenListeCharges.RecupListeCharges();
                // Met à jour la liste des charges pour le bien
                MajChargesDuBien();
                FicheBien laFicheBien = this.fenListeCharges.GetFenFicheBien();
                laFicheBien.RemplirChamps();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs pour pouvoir valider la saisie.");
            }
        }

        /// <summary>
        /// Calcule le montant annuel de la charge en fonction du montant et de la fréquence renseignés
        /// </summary>
        /// <returns></returns>
        private float CalculerMontantAnnuel()
        {
            this.req = $"SELECT occurrence FROM frequencepaiement WHERE libelle = \'{cobFrequence.SelectedItem}\'";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            float occurrence = (int)(reader["occurrence"]);
            reader.Close();
            float totalAnnuel = (occurrence * float.Parse(txtMontant.Text));
            return (int)Math.Round(totalAnnuel, 2);
        }

        /// <summary>
        /// Vérifie que les champs obligatoires soient remplis
        /// </summary>
        /// <returns>Vrai si tous les champs sont remplis, faux dans le cas contraire</returns>
        private bool VerifChamps()
        {
            if (txtLibelle.Text.Equals("") || txtMontant.Text.Equals("") || cobFrequence.SelectedItem == null)
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
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                charges += float.Parse(reader["chargeannuelle"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();

            // Calcule la charge imputable au locataire du bien
            this.req += " AND imputable = True";
            float chImputables = 0;
            this.command = new MySqlCommand(this.req, this.connexion);
            reader = this.command.ExecuteReader();
            finCurseur = !reader.Read();
            while (!finCurseur)
            {
                chImputables += float.Parse(reader["chargeannuelle"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();

            // Met à jour la table bien
            this.req = $"UPDATE bien SET chargeannuelles = \'{Math.Round(charges, 2)}\', chargesimputables = \'{Math.Round(chImputables / 12, 2)}\' WHERE idbien = {leBien[0]}";
            // Exécute la requête
            this.command = new MySqlCommand(this.req, this.connexion);
            // Prépare la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }
    }
}
