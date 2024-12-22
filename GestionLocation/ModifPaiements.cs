using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class ModifPaiements : Form
    {

        private readonly string idPaiement;
        private string req;
        private MySqlCommand command;
        private readonly Paiements fenPaiements;
        
        /// <summary>
        /// Constructeur de la fenêtre ModifPaiement
        /// </summary>
        /// <param name="connexion">Connexion à la BDD</param>
        /// <param name="idPaiement">id de l'enregistrement de la table Paiement à modifier</param>
        public ModifPaiements(Paiements fenPaiements)
        {
            InitializeComponent();
            this.fenPaiements = fenPaiements;
            this.idPaiement = this.fenPaiements.GetIdPaiement();
            RemplirChamps();
        }


        /// <summary>
        /// Remplit les différents champs de la fenêtre
        /// </summary>
        public void RemplirChamps()
        {
            // Construit la requête pour récupérer les informations du paiement à partir de son id
            this.req = $"SELECT * FROM paiement WHERE idpaiement = {this.idPaiement}";
            this.command = new MySqlCommand(req.ToString(), Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            // S'il n'y a pas de date, mettre la date du jour par défaut
            try
            {
                datPaiement.Value = reader.GetDateTime(2);
            }
            catch
            {
                datPaiement.Value = DateTime.Today;
            }
            txtMontantPaye.Text = reader.GetString(3);
            string periode = $"{reader.GetDateTime(4):MMMM yyyy}";
            txtMontantDu.Text = reader.GetString(5);
            txtResteAPayer.Text = reader.GetString(6);
            if ((bool)reader["loyerregle"])
            {
                cbxRegle.Checked = true;
            }
            else
            {
                cbxRegle.Checked = false;
            }
            string idLocation = reader.GetString(1);
            reader.Close();

            // Récupère les informations sur le locataire
            this.req = $"SELECT nomcompletlocataire FROM locataire WHERE idlocataire = (SELECT idlocataire FROM location WHERE idlocation = {idLocation})";
            this.command = new MySqlCommand(req.ToString(), Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            string locataire = reader["nomcompletlocataire"].ToString();
            reader.Close();

            // Récupère les informations sur le bien
            this.req = $"SELECT nombien FROM bien WHERE idbien = (SELECT idbien FROM location WHERE idlocation = {idLocation})";
            this.command = new MySqlCommand(req.ToString(), Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            string bien = reader["nombien"].ToString();
            reader.Close();

            // Affiche le locataire + bien
            string ligneUn = $"{locataire} - {bien.ToUpper()}";
            string separation = "";
            for (int i = 0; i < (ligneUn.Length * 1.2); i++)
            {
                separation += "-";
            }
            lblLocation.Text = $"{ligneUn}\n{separation}\n{periode.ToUpper()}";
        }


        // Ferme la fenêtre
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        /// <summary>
        /// Gère l'enregistrement du Paiement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            // Modifie le format du montant payé
            string strMontant = txtMontantPaye.Text.Replace('.', ',');
            bool reussi = float.TryParse(strMontant, out float montant);
            // Si le contenu du champ du montant payé est incorrect
            if (reussi == false)
            {
                MessageBox.Show("Montant incorrect");
                txtMontantPaye.Focus();
            }
            // Sinon, met à jour l'enregistrement concerné
            else
            {
                // Calcule les valeurs de l'enregistrement à mettre à jour
                string montantDu = txtMontantDu.Text.Replace('.', ',');
                string resteAPayer = (float.Parse(montantDu) - montant).ToString();
                bool loyerregle = false;
                if (float.Parse(resteAPayer) <= 0)
                {
                    loyerregle = true;

                }
                // Construit la requête de mise à jour
                this.req = $"UPDATE paiement SET datepaiement = \'{datPaiement.Value:yyyy-MM-dd}\', montantpaye = \'{txtMontantPaye.Text.Replace(',', '.')}\', " +
                    $"resteapayer = \'{resteAPayer.Replace(',', '.')}\', loyerregle = {loyerregle} WHERE idpaiement = {this.idPaiement}";
                // Exécute la requête d'enregistrement du paiement
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                this.command.ExecuteNonQuery();
                // Met à jour l'affichage
                this.req = fenPaiements.GetRequete();
                this.fenPaiements.EnvoiReqSelectPaiements();
                // Si le loyer est réglé, demande s'il faut envoyer la quittance par mail au locataire
                if (loyerregle == true)
                {
                   /* // Demande pour envoi de la quittance de loyer
                    DialogResult result = MessageBox.Show($"Voulez-vous envoyer une quittance au locataire ?", "Envoi de quittance", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Préparation et envoi de la quittance
                        if (!fenPaiements.VerifMail().Equals(""))
                        {
                            this.fenPaiements.GestionQuittance(this.idPaiement);
                        }
                        else
                        {
                            MessageBox.Show("Impossible d'envoyer la quittance au locataire, vous n'avez pas renseigné son adresse mail.");
                        }
                    }*/
                }
                // Ferme la fenêtre
                this.Dispose();
            }
        }
    }
}
