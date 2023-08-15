using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
            AppliquerCouleurs();
        }


        /// <summary>
        /// Gère les couleurs d'alerte en cas d'anomalie
        /// </summary>
        public void AppliquerCouleurs()
        {
            // Charges imputables
            int charges = int.Parse(txtCharges.Text.Replace(" €", ""));
            int chImput = 0;
            if (!txtChargesImputables.Text.Equals("-"))
            {
                chImput = int.Parse(txtChargesImputables.Text.Replace(" €", ""));
            }            
            if (chImput > charges)
            {
                txtChargesImputables.BackColor = System.Drawing.Color.DarkRed;
                txtChargesImputables.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                txtChargesImputables.BackColor = System.Drawing.SystemColors.Control;
                txtChargesImputables.ForeColor = System.Drawing.SystemColors.WindowText;
            }

            // Vacance locative
            float renta = float.Parse(txtSeuilRenta.Text.Replace(" %", ""));
            float vacance = float.Parse(txtVacanceLocative.Text.Replace(" %", ""));
            if (vacance > 100 - renta)
            {
                txtVacanceLocative.BackColor = System.Drawing.Color.DarkRed;
                txtVacanceLocative.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                txtVacanceLocative.BackColor = System.Drawing.SystemColors.Control;
                txtVacanceLocative.ForeColor = System.Drawing.SystemColors.WindowText;
            }
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
            this.Text = this.leBien[1];
            // Remplissage des champs récupérés dans la ligne
            lblNomBien.Text = $"{reader.GetString(1).ToUpper()}   -   {reader.GetString(5)} {reader.GetString(6)} {reader.GetString(7).ToUpper()}";
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
            // Calcule le nombre de locations
            CalculNbLoc();
            
            // Calcule le début d'exploitation
            CalculDebutExploit();
            
            // Calcule la fin d'exploitation
            CalculFinExploit();
            
            // Calcule la durée d'exploitation
            CalculDureeExploit();
            
            // Récupération des durées de location
            this.req = $"SELECT * FROM location WHERE idbien={this.leBien[0]}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            List<int> lesDureesDeLoc = new List<int>();
            while (reader.Read())
            {
                DateTime debutLoc = DateTime.ParseExact($"{reader.GetDateTime(4):d}", "d", null);
                DateTime finLoc = DateTime.ParseExact($"{reader.GetDateTime(5):d}", "d", null);
                lesDureesDeLoc.Add(finLoc.Subtract(debutLoc).Days + 1);
            }
            reader.Close();
            // Calcul des durées mini, moyenne et maxi de location
            int dureeTotaleDeLoc = 0, dureeMini = 10000, dureeMaxi = 0;
            foreach (int duree in lesDureesDeLoc)
            {
                dureeTotaleDeLoc += duree;
                dureeMini = Math.Min(dureeMini, duree);
                dureeMaxi = Math.Max(dureeMaxi, duree);
            }
            // Conversion et affichage des valeurs
            txtDureeMoyenneLoc.Text = ConvertJoursVersMois(dureeTotaleDeLoc / int.Parse(txtNbLoc.Text));
            txtDureeMiniLoc.Text = ConvertJoursVersMois(dureeMini);
            txtDureeMaxiLoc.Text = ConvertJoursVersMois(dureeMaxi);

            // Calcul de la vacance locative
            float vacanceJours = int.Parse(txtDureeExploitEnJours.Text) - dureeTotaleDeLoc;
            float vacancePrc = (float)Math.Round(vacanceJours / int.Parse(txtDureeExploitEnJours.Text) * 100, 1);
            txtVacanceLocative.Text = $"{vacancePrc} %";
        }


        /// <summary>
        /// Convertit un nombre de jours en mois
        /// </summary>
        /// <param name="jours">Nombre de jours à convertir</param>
        /// <returns>Durée équivalente en mois</returns>
        public string ConvertJoursVersMois(int jours)
        {
            double mois = Math.Round((jours / 30.42), 1);
            return mois.ToString();
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
        /// Remplit le champ relatif au nombre de locations du bien
        /// </summary>
        public void CalculNbLoc()
        {
            this.req = $"SELECT COUNT(idlocation) FROM (SELECT idlocation FROM location WHERE idbien={this.leBien[0]}) AS req";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            txtNbLoc.Text = reader.GetString(0);
            reader.Close();
        }


        /// <summary>
        /// Remplit le champ relatif au début d'exploitation du bien
        /// </summary>
        public void CalculDebutExploit()
        {
            this.req = $"SELECT MIN(debutlocation) FROM (SELECT debutlocation FROM location WHERE idbien={this.leBien[0]}) AS req";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            try
            {
                txtDebutExploit.Text = $"{reader.GetDateTime(0):d}";
            }
            catch
            {
                txtDebutExploit.Text = "-";
            }
            reader.Close();
        }


        /// <summary>
        /// Remplit le champ relatif à la fin d'exploitation du bien
        /// </summary>
        public void CalculFinExploit()
        {
            this.req = $"SELECT MAX(finlocation) FROM (SELECT finlocation FROM location WHERE idbien={this.leBien[0]}) AS req";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            try
            {
                txtFinExploit.Text = $"{reader.GetDateTime(0):d}";
            }
            catch
            {
                txtDebutExploit.Text = "-";
            }
            reader.Close();
        }


        /// <summary>
        /// Remplit les champs relatifs aux durées d'exploitation pour le bien
        /// </summary>
        public void CalculDureeExploit()
        {
            DateTime debutExploit = DateTime.ParseExact(txtDebutExploit.Text, "d", null);
            DateTime finExploit = DateTime.ParseExact(txtFinExploit.Text, "d", null);
            TimeSpan dureeExploit = finExploit.Subtract(debutExploit);
            txtDureeExploitEnJours.Text = dureeExploit.Days.ToString();
            double exploitAnnees = (double)(dureeExploit.TotalDays / 365);
            txtDureeExploitEnAnnees.Text = Math.Round(exploitAnnees, 1).ToString();
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
