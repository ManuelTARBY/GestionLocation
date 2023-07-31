using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class FicheBien : Form
    {

        private MySqlCommand command;
        private string req;
        private readonly string[] leBien = new string[2];


        /// <summary>
        /// Constructeur de FicheBien
        /// </summary>
        /// <param name="id">id du bien</param>
        public FicheBien(int id)
        {
            InitializeComponent();
            this.leBien[0] = id.ToString();
            RemplirChamps();
        }


        /// <summary>
        /// Remplit tous les champs de la fenêtre
        /// </summary>
        public void RemplirChamps()
        {
            RemplirBien();
            RemplirLocation();
        }


        /// <summary>
        /// Remplit les champs de la fenêtre avec les données issues de la table Bien
        /// </summary>
        public void RemplirBien()
        {
            this.req = $"SELECT * FROM bien WHERE idbien ={this.leBien[0]}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            this.leBien[1] = reader.GetString(1);
            // Remplissage des champs récupérés dans la ligne
            lblNomBien.Text = $"{reader.GetString(1).ToUpper()}   --   {reader.GetString(5)} {reader.GetString(6)} {reader.GetString(7).ToUpper()}";
            txtLoyerHC.Text = $"{reader.GetString(2)} €";
            txtCharges.Text = $"{reader.GetString(3)} €";
            txtLoyerCC.Text = $"{reader.GetString(4)} €";
            try
            {
                txtChargesImputables.Text = $"{reader.GetString(8)} €";
            }
            catch
            {
                txtChargesImputables.Text = "-";
            }
            try
            {
                txtChargesAnnuelles.Text = $"{reader.GetString(9)} €";
            }
            catch
            {
                txtChargesAnnuelles.Text = "-";
            }
            CalculSeuilRenta(reader.GetInt32(4));
            if ((bool)reader["bienarchive"])
            {
                txtArchive.Text = "Oui";
            }
            else
            {
                txtArchive.Text = "Non";
            }
            reader.Close();
        }


        /// <summary>
        /// Remplit les champs de la fenêtre issus de la table Location
        /// </summary>
        public void RemplirLocation()
        {
            this.req = $"SELECT COUNT(idlocation) FROM (SELECT idlocation FROM location WHERE idbien={this.leBien[0]}) AS req";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            txtNbLoc.Text = reader.GetString(0);
            reader.Close();
            this.req = $"SELECT MIN(debutlocation) FROM (SELECT debutlocation FROM location WHERE idbien={this.leBien[0]}) AS req";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            try 
            {
                txtPremiereLoc.Text = $"{reader.GetDateTime(0):d}";
            }
            catch
            {
                txtPremiereLoc.Text = "-";
            }
            reader.Close();
        }


        /// <summary>
        /// Gère le clic sur le bouton fermer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Gère le calcul du seuil de rentabilité
        /// </summary>
        /// <param name="loyerCC"></param>
        public void CalculSeuilRenta(int loyerCC)
        {
            // Si les charges annuelles sont inconnues
            if (txtChargesAnnuelles.Text.Equals("-"))
            {
                txtSeuilRenta.Text = "-";
                txtSeuilRentaJours.Text = "-";
            }
            // Sinon
            else
            {
                string[] strChargesAnnuelles = txtChargesAnnuelles.Text.Split(' ');
                int loyerCCAnnuel = loyerCC * 12;
                float renta = float.Parse(strChargesAnnuelles[0]) / loyerCCAnnuel * 100;
                // En pourcentage
                txtSeuilRenta.Text = $"{Math.Round(renta, 1)} %";
                // En jours
                int rentaJours = (int)(365 * renta / 100);
                txtSeuilRentaJours.Text = $"{rentaJours}";
            }
        }


        /// <summary>
        /// Ouvre la page qui liste toutes les charges propres au bien
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnListeCharges_Click(object sender, EventArgs e)
        {
            ListeCharges fenListeCharges = new ListeCharges(this);
            fenListeCharges.ShowDialog();
        }


        /// <summary>
        /// renvoie le couple idbien-nombien
        /// </summary>
        /// <returns>idbien - nombien</returns>
        public string[] GetLeBien()
        {
            return this.leBien;
        }
    }
}
