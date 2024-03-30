using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class FicheBien : Form
    {

        private MySqlCommand command;
        private string req;
        // Stocke [id, nom]
        private readonly string[] leBien = new string[2];
        // Id de la location actuelle
        private string idLocActuelle;
        private readonly string type;
        private int dureeLocActuelle, nbDeBiens;


        /// <summary>
        /// Constructeur de FicheBien
        /// </summary>
        /// <param name="data">Contient le type, l'id et le nom du bien</param>
        public FicheBien(string[] data)
        {
            InitializeComponent();
            this.idLocActuelle = "0";
            this.leBien[0] = data[1];
            this.leBien[1] = data[2];
            this.type = data[0];
            this.nbDeBiens = 1;
            RemplirChamps();
        }


        /// <summary>
        /// Remplit tous les champs de la fenêtre
        /// </summary>
        public void RemplirChamps()
        {
            RemplirBien();
            RemplirLocation();
            RemplirLocataire();
            AppliquerCouleurs();
            RemplirListeLocations();
        }


        /// <summary>
        /// Remplit les champs relatifs au locataire (nom et durée de la location)
        /// </summary>
        public void RemplirLocataire()
        {
            // Si le bien n'est pas occupé ou qu'il s'agit d'un groupe de bien
            if (this.idLocActuelle.Equals("0") || this.type.Equals("groupe"))
            {
                txtDureeOccup.Visible = false;
                lblDureeOccup.Visible = false;
            }
            // Sinon
            else
            {
                // Cherche l'id du locataire relié à cette location
                string idLocataire;
                this.req = $"SELECT idlocataire FROM location WHERE idlocation ={this.idLocActuelle}";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                idLocataire = reader.GetString(0);
                reader.Close();
                // Recherche du nom complet du locataire à partir de son id
                txtActuelLocat.Text = RecupLocataire(idLocataire);
                // Calcule la durée de l'actuelle location en mois
                txtDureeOccup.Text = ConvertJoursVersMois(this.dureeLocActuelle);
            }
        }


        /// <summary>
        /// Récupère le nom complet d'un locataire à partir de son id
        /// </summary>
        /// <param name="idLocat">Iddu locataire à trouver</param>
        /// <returns>Nom complet du locataire</returns>
        public string RecupLocataire(string idLocat)
        {
            string leNom;
            this.req = $"SELECT nomcompletlocataire FROM locataire WHERE idlocataire ={idLocat}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            leNom = reader.GetString(0);
            reader.Close();
            return leNom;
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
            switch (this.type)
            {
                case "bien":
                    this.req = $"SELECT * FROM bien WHERE idbien ={this.leBien[0]}";
                    this.command = new MySqlCommand(this.req, Global.Connexion);
                    MySqlDataReader reader = this.command.ExecuteReader();
                    reader.Read();
                    // Remplissage des champs récupérés dans la ligne
                    lblNomBien.Text = $"{this.leBien[1].ToUpper()}   -   {reader.GetString(5)} {reader.GetString(6)} {reader.GetString(7).ToUpper()}";
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
                    //CalculSeuilRenta(reader.GetInt32(4));
                    if ((bool)reader["bienarchive"])
                    {
                        txtArchive.Text = "Oui";
                    }
                    else
                    {
                        txtArchive.Text = "Non";
                    }
                    reader.Close();
                    break;
                case "groupe":
                    lblActuelLocat.Visible = false;
                    txtActuelLocat.Visible = false;
                    lblArchive.Visible = false;
                    txtArchive.Visible = false;
                    lblDebutExploit.Visible = false;
                    txtDebutExploit.Visible = false;
                    lblFinExploit.Visible = false;
                    txtFinExploit.Visible = false;
                    lblNomBien.Text = $"{this.leBien[1].ToUpper()}";
                    txtChargesAnnuelles.Text = RecupChargesAnnuGrpe() + " €";
                    RemplirLoyerChargeGrpe();
                    break;
                default:
                    break;
            }
            CalculSeuilRenta(txtLoyerCC.Text);
        }


        /// <summary>
        /// Remplit les champs de la fenêtre issus de la table Location
        /// </summary>
        public void RemplirLocation()
        {
            // Calcule le nombre de locations
            CalculNbLoc();
            double[] dureeExploit;
            switch (this.type) {
                case "bien":
                    // Calcule le début d'exploitation
                    this.req = $"SELECT MIN(debutlocation) FROM (SELECT debutlocation FROM location WHERE idbien={this.leBien[0]}) AS reqC";
                    txtDebutExploit.Text = CalculDebutExploit();

                    // Calcule la fin d'exploitation
                    this.req = $"SELECT MAX(finlocation) FROM (SELECT finlocation FROM location WHERE idbien={this.leBien[0]}) AS reqD";
                    txtFinExploit.Text = CalculFinExploit();

                    // Calcule la durée d'exploitation
                    dureeExploit = CalculDureeExploit(txtDebutExploit.Text, txtFinExploit.Text);
                    txtDureeExploitEnJours.Text = dureeExploit[0].ToString();
                    txtDureeExploitEnAnnees.Text = String.Format("{0:0.#}", dureeExploit[1]);
                    break;
                case "groupe":
                    double exploitAnnees = 0, exploitJours = 0;
                    List<string> lesBiens = RecupLesBiens();
                    this.nbDeBiens = lesBiens.Count;
                    string[] lesDebutExploit = new string[nbDeBiens];
                    string[] lesFinExploit = new string[nbDeBiens];
                    for (int i = 0; i < nbDeBiens; i++)
                    {
                        this.req = $"SELECT MIN(debutlocation) FROM (SELECT debutlocation FROM location WHERE idbien={lesBiens[i]}) AS reqA";
                        lesDebutExploit[i] = CalculDebutExploit();
                        this.req = $"SELECT MAX(finlocation) FROM (SELECT finlocation FROM location WHERE idbien={lesBiens[i]}) AS reqB";
                        lesFinExploit[i] = CalculFinExploit();
                    }
                    for (int j = 0; j < nbDeBiens; j++)
                    {
                        dureeExploit = CalculDureeExploit(lesDebutExploit[j], lesFinExploit[j]);
                        exploitJours += dureeExploit[0];
                        exploitAnnees += dureeExploit[1];
                    }
                    txtDureeExploitEnJours.Text = String.Format("{0:0.}", exploitJours / nbDeBiens);
                    txtDureeExploitEnAnnees.Text = String.Format("{0:0.#}", exploitAnnees / nbDeBiens);
                    break;
                default:
                    break;
            }
            
            // Récupération des durées de location
            switch (type)
            {
                case "bien":
                    this.req = $"SELECT * FROM location WHERE idbien={this.leBien[0]}";
                    break;
                case "groupe":
                    this.req = $"SELECT * FROM location WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = {this.leBien[0]})";
                    break;
                default:
                    break;
            }
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            List<int> lesDureesDeLoc = new List<int>();
            DateTime today = DateTime.Now;
            while (reader.Read())
            {
                DateTime debutLoc = DateTime.ParseExact($"{reader.GetDateTime(4):d}", "d", null);
                DateTime finLoc = DateTime.ParseExact($"{reader.GetDateTime(5):d}", "d", null);
                lesDureesDeLoc.Add(finLoc.Subtract(debutLoc).Days + 1);
                // Si la location est l'actuelle location
                if (debutLoc < today && finLoc > today)
                {
                    this.idLocActuelle = reader.GetString(0);
                    this.dureeLocActuelle = today.Subtract(debutLoc).Days + 1;
                }
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
            if (this.type.Equals("groupe"))
            {
                dureeTotaleDeLoc /= this.nbDeBiens;
            }
            float vacanceJours = int.Parse(txtDureeExploitEnJours.Text) - dureeTotaleDeLoc;
            float vacancePrc = (float)Math.Round(vacanceJours / int.Parse(txtDureeExploitEnJours.Text) * 100, 1);
            txtVacanceLocative.Text = $"{vacancePrc} %";
        }

        /// <summary>
        /// Récupère les
        /// </summary>
        /// <returns></returns>
        public List<string> RecupLesBiens()
        {
            List<string> lesBiens = new List<string>();
            this.req = $"SELECT idbien FROM lignegroupe WHERE idgroupe = {this.leBien[0]}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            while (reader.Read())
            {
                lesBiens.Add(reader["idbien"].ToString());
            }
            reader.Close();
            return lesBiens;
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
            if (this.type.Equals("bien"))
            {
                this.req = $"SELECT COUNT(idlocation) AS 'Nb de loc' FROM (SELECT idlocation FROM location WHERE idbien={this.leBien[0]}) AS req";
            }
            else
            {
                this.req = $"SELECT COUNT(idlocation) AS 'Nb de loc' FROM location WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = \'{this.leBien[0]}\')";
            }
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            txtNbLoc.Text = reader["Nb de loc"].ToString();
            reader.Close();
        }


        /// <summary>
        /// Remplit le champ relatif au début d'exploitation du bien
        /// </summary>
        /// <returns>Date de début d'exploitation sous forme de chaîne</returns>
        public string CalculDebutExploit()
        {
            string debutExploit;
            //this.req = $"SELECT MIN(debutlocation) FROM (SELECT debutlocation FROM location WHERE idbien={this.leBien[0]}) AS req";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            try
            {
                debutExploit = $"{reader.GetDateTime(0):d}";
            }
            catch
            {
                debutExploit = "-";
            }
            reader.Close();
            return debutExploit;
        }


        /// <summary>
        /// Remplit le champ relatif à la fin d'exploitation du bien
        /// </summary>
        /// <return>Date de fin d'exploitation sous forme de chaîne</return>
        public string CalculFinExploit()
        {
            string finExploit;
            //this.req = $"SELECT MAX(finlocation) FROM (SELECT finlocation FROM location WHERE idbien={this.leBien[0]}) AS req";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            try
            {
                finExploit = $"{reader.GetDateTime(0):d}";
            }
            catch
            {
                finExploit = "-";
            }
            reader.Close();
            return finExploit;
        }


        /// <summary>
        /// Remplit les champs relatifs aux durées d'exploitation pour le bien
        /// </summary>
        public double[] CalculDureeExploit(string debExpl, string finExpl)
        {
            DateTime debutExploit = DateTime.ParseExact(debExpl, "d", null);
            DateTime finExploit = DateTime.ParseExact(finExpl, "d", null);
            TimeSpan dureeExploit = finExploit.Subtract(debutExploit);
            double[] lesDurees = new double[2];
            lesDurees[0] = dureeExploit.Days;
            double exploitAnnees = (double)(dureeExploit.TotalDays / 365);
            lesDurees[1] = Math.Round(exploitAnnees, 1);
            return lesDurees;
        }


        /// <summary>
        /// Gère le calcul du seuil de rentabilité
        /// </summary>
        /// <param name="loyerCC">Loyer charges comprises</param>
        public void CalculSeuilRenta(string loyerCC)
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
                float loyCC = float.Parse(loyerCC.Replace(" €", ""));
                float strChargesAnnuelles = float.Parse(txtChargesAnnuelles.Text.Replace(" €", ""));
                float loyerCCAnnuel = loyCC * 12;
                float renta = strChargesAnnuelles / loyerCCAnnuel * 100;
                // En pourcentage
                txtSeuilRenta.Text = $"{String.Format("{0:0.#}", renta)}";
                txtSeuilRenta.Text = $"{Math.Round(renta, 1)} %";
                // En jours
                float rentaJours = (365 * renta / 100);
                txtSeuilRentaJours.Text = $"{String.Format("{0:0.}", rentaJours)}";
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
        /// Renvoie le couple idbien-nombien
        /// </summary>
        /// <returns>idbien - nombien</returns>
        public string[] GetLeBien()
        {
            return this.leBien;
        }


        /// <summary>
        /// Remplit le champ des charges annuelles pour un groupe de biens
        /// </summary>
        /// <returns>Montant des charges annuelles pour ce groupe de biens</returns>
        public string RecupChargesAnnuGrpe()
        {
            string totalCh;
            this.req = $"SELECT SUM(chargeannuelle) AS total FROM chargesannuelles WHERE idbien IN (SELECT idbien FROM lignegroupe JOIN grpedebiens ON lignegroupe.idgroupe = grpedebiens.idgroupe WHERE nomdugroupe = \"{this.leBien[1]}\")";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            totalCh = reader["total"].ToString();
            reader.Close();
            return totalCh;
        }


        /// <summary>
        /// Remplit les champs LoyerHC, Charges et LoyerCC pour un groupe de biens
        /// </summary>
        public void RemplirLoyerChargeGrpe()
        {
            this.req = $"SELECT SUM(loyerHC) AS 'Loyers HC', SUM(charges) AS 'Total charges', SUM(loyercc) AS 'Loyers CC', SUM(chargesimputables) AS 'Imputables' FROM bien WHERE idbien IN (SELECT idbien FROM lignegroupe JOIN grpedebiens ON lignegroupe.idgroupe = grpedebiens.idgroupe WHERE nomdugroupe = \"{this.leBien[1]}\")";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            txtLoyerHC.Text = reader["Loyers HC"].ToString() + " €";
            txtCharges.Text = reader["Total charges"].ToString() + " €";
            txtLoyerCC.Text = reader["Loyers CC"].ToString() + " €";
            txtChargesImputables.Text = reader["Imputables"].ToString() + " €";
            reader.Close();
        }


        /// <summary>
        /// Remplit le DataGridView avec la liste des locations
        /// </summary>
        public void RemplirListeLocations()
        {
            if (this.type.Equals("bien"))
            {
                this.req = "SELECT CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', nomlocataire) AS 'Locataire', " +
                    "debutlocation AS 'Début de location', LEAST(finlocation, DATE_ADD(CURRENT_DATE(), INTERVAL 30 DAY)) AS 'Fin de location', " +
                    "CONCAT(ROUND(DATEDIFF(LEAST(finlocation, DATE_ADD(CURRENT_DATE(), INTERVAL 30 DAY)), debutlocation) / 30.417, 1), ' mois') AS 'Durée' " +
                    $"FROM location NATURAL JOIN locataire WHERE idbien = {this.leBien[0]} ORDER BY debutlocation";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        datListeLocations.Rows.Add(reader.GetString(0), reader.GetDateTime(1).ToString("dd/MM/yyyy"), reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetString(3));
                    }
                }
                reader.Close();
            }
            else
            {
                datListeLocations.Visible = false;
            }
        }
    }
}
