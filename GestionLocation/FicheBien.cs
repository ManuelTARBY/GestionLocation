using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestionLocation
{
    public partial class FicheBien : Form
    {

        private MySqlCommand command;
        private string req;
        private readonly Dictionary<string, string> infoBien;
        // Id de la location actuelle
        private string idLocActuelle;
        private int dureeLocActuelle, nbDeBiens;
        private readonly List<int> bienSelectionne;


        /// <summary>
        /// Constructeur de FicheBien
        /// </summary>
        /// <param name="data">Contient le type, l'id et le nom du bien</param>
        public FicheBien(string[] data)
        {
            InitializeComponent();
            this.idLocActuelle = "0";
            this.infoBien = new Dictionary<string, string>
            {
                { "type", data[0] },
                { "id", data[1] },
                { "nom", data[2] }
            };
            if (this.infoBien["type"] == "groupe")
            {
                btnListeCharges.Visible = false;
            }
            this.bienSelectionne = new List<int>();
            chartCF.Series["Series1"].ChartType = SeriesChartType.Line;
            chartCF.Series["Series1"].Name = "CA annuel";
            // Crée la série pour les charges annuelles
            Series serieCharges = new Series("Charges annuelles")
            {
                ChartType = SeriesChartType.Line
            };
            // Ajoute la série des charges annuelles
            chartCF.Series.Add(serieCharges);
            GetListeDesBiensSelectionnes();
            GetLesAnnees();
            this.nbDeBiens = 1;
            RemplirChamps();
        }


        /// <summary>
        /// Récupère les années d'exploitation pour un bien ou un groupe de bien
        /// </summary>
        /// <returns></returns>
        public void GetLesAnnees()
        {
            int anneeMini, anneeMaxi;
            List<int> lesAnnees = new List<int>();
            List<int> lesBiens = new List<int>();
            MySqlDataReader reader;
            // Si c'est un bien qui est sélectionné
            if (this.infoBien["type"] == "bien")
            {
                lesBiens.Add(int.Parse(this.infoBien["id"]));
            }
            // Si c'est un groupe de biens qui est sélectionné
            else
            {
                this.req = $"SELECT idbien FROM lignegroupe WHERE idgroupe = {this.infoBien["id"]}";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                reader = this.command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lesBiens.Add(reader.GetInt32(0));
                    }
                    reader.Close();
                }
            }

            // Détermine la première année d'exploitation
            this.req = $"SELECT MIN(YEAR(debutlocation)) FROM location " +
                $"WHERE idbien IN ({string.Join(",", lesBiens.ConvertAll(v => v.ToString()))})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            reader.Read();
            anneeMini = reader.GetInt32(0);
            reader.Close();

            // Détermine la dernière année d'exploitation (sans dépasser l'année actuelle)
            this.req = $"SELECT LEAST(MAX(YEAR(finlocation)), YEAR(CURDATE())) FROM location " +
                $"WHERE idbien IN ({string.Join(",", lesBiens.ConvertAll(v => v.ToString()))})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            reader.Read();
            anneeMaxi = reader.GetInt32(0);
            reader.Close();

            for (int i = anneeMini; i <= anneeMaxi; i++)
            {
                lesAnnees.Add(i);
            }

            // Met à jour la chart du cash-flow par année
            // Réglez les valeurs minimales et maximales de l'axe des abscisses
            chartCF.ChartAreas[0].AxisX.Minimum = anneeMini;
            chartCF.ChartAreas[0].AxisX.Maximum = anneeMaxi;

            // Assurez-vous que les valeurs de l'axe des abscisses sont affichées correctement
            chartCF.ChartAreas[0].AxisX.Interval = 1;
            CompleterChartCF(lesAnnees, lesBiens);
        }


        /// <summary>
        /// Permet de récupérer les infos sur le bien ou le groupe de bien sélectionné
        /// </summary>
        /// <returns>Infos sur le bien ou le groupe de bien sélectionné</returns>
        public Dictionary<string, string> GetInfoBien()
        {
            return this.infoBien;
        }


        /// <summary>
        /// Récupère la liste des biens concernés par l'affichage de la fenêtre
        /// </summary>
        public void GetListeDesBiensSelectionnes()
        {
            this.bienSelectionne.Clear();
            if (this.infoBien["type"] == "bien")
            {
                this.bienSelectionne.Add(int.Parse(this.infoBien["id"]));
            }
            else if (this.infoBien["type"] == "groupe")
            {
                this.req = "SELECT idbien FROM lignegroupe WHERE idgroupe = " +
                       $"(SELECT idgroupe FROM grpedebiens WHERE idgroupe = {this.infoBien["id"]})";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        this.bienSelectionne.Add(reader.GetInt32(0));
                    }
                    reader.Close();
                }
            }
        }


        /// <summary>
        /// Revoie le type de bien sélectionné
        /// </summary>
        /// <returns>Chaîne contenant le type de bien sélectionné</returns>
        public string GetTypeBien()
        {
            return this.infoBien["type"];
        }


        /// <summary>
        /// Retourne la liste des biens sélectionnés
        /// </summary>
        /// <returns> Liste des biens sélectionnés</returns>
        public List<int> GetLesBiens()
        {
            return this.bienSelectionne;
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
            if (this.idLocActuelle.Equals("0") || this.infoBien["type"].Equals("groupe"))
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
        /// <param name="idLocat">Id du locataire à trouver</param>
        /// <returns>Nom complet du locataire</returns>
        public string RecupLocataire(string idLocat)
        {
            string leNom;
            this.req = "SELECT CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', SUBSTRING_INDEX(nomlocataire, ',' , 1)) " +
                $"FROM locataire WHERE idlocataire ={idLocat}";
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
            float charges = float.Parse(txtCharges.Text.Replace(" €", ""));
            float chImput = 0;
            if (!txtChargesImputables.Text.Equals("-"))
            {
                chImput = float.Parse(txtChargesImputables.Text.Replace(" €", ""));
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
            switch (this.infoBien["type"])
            {
                case "bien":
                    this.req = $"SELECT * FROM bien WHERE idbien = {this.infoBien["id"]}";
                    this.command = new MySqlCommand(this.req, Global.Connexion);
                    this.command.Prepare();
                    MySqlDataReader reader = this.command.ExecuteReader();
                    reader.Read();
                    // Remplissage des champs récupérés dans la ligne
                    lblNomBien.Text = $"{this.infoBien["nom"].ToUpper()}   -   {reader.GetString(5)} {reader.GetString(6)} {reader.GetString(7).ToUpper()}";
                    txtLoyerHC.Text = $"{reader.GetFloat(2):N} €";
                    txtCharges.Text = $"{reader.GetFloat(3):N} €";
                    txtLoyerCC.Text = $"{reader.GetFloat(4):N} €";
                    try
                    {
                        txtChargesImputables.Text = $"{reader.GetFloat(8):N} €";
                    }
                    catch
                    {
                        txtChargesImputables.Text = "-";
                    }
                    try
                    {
                        txtChargesAnnuelles.Text = $"{reader.GetFloat(9):N} €";
                    }
                    catch
                    {
                        txtChargesAnnuelles.Text = "-";
                    }
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
                    lblNomBien.Text = $"{this.infoBien["nom"].ToUpper()}";
                    txtChargesAnnuelles.Text = RecupChargesAnnuGrpe().ToString("N") + " €";
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
            double exploitJours = 0;
            double exploitAnnees = 0;
            switch (this.infoBien["type"])
            {
                case "bien":
                    this.nbDeBiens = 1;
                    // Calcule le début d'exploitation
                    this.req = $"SELECT MIN(debutlocation) FROM (SELECT debutlocation FROM location WHERE idbien={this.infoBien["id"]}) AS reqC";
                    txtDebutExploit.Text = CalculDebutExploit();

                    // Calcule la fin d'exploitation
                    this.req = $"SELECT MAX(finlocation) FROM (SELECT finlocation FROM location WHERE idbien={this.infoBien["id"]}) AS reqD";
                    txtFinExploit.Text = CalculFinExploit();

                    // Calcule la durée d'exploitation
                    dureeExploit = CalculDureeExploit(txtDebutExploit.Text, txtFinExploit.Text);
                    exploitJours = dureeExploit[0];
                    exploitAnnees = dureeExploit[1];
                    break;
                case "groupe":
                    this.nbDeBiens = this.bienSelectionne.Count;
                    string[] lesDebutExploit = new string[nbDeBiens];
                    string[] lesFinExploit = new string[nbDeBiens];
                    for (int i = 0; i < nbDeBiens; i++)
                    {
                        this.req = $"SELECT MIN(debutlocation) FROM (SELECT debutlocation FROM location WHERE idbien={this.bienSelectionne[i]}) AS reqA";
                        lesDebutExploit[i] = CalculDebutExploit();
                        this.req = $"SELECT MAX(finlocation) FROM (SELECT finlocation FROM location WHERE idbien={this.bienSelectionne[i]}) AS reqB";
                        lesFinExploit[i] = CalculFinExploit();
                    }
                    for (int j = 0; j < nbDeBiens; j++)
                    {
                        dureeExploit = CalculDureeExploit(lesDebutExploit[j], lesFinExploit[j]);
                        exploitJours += dureeExploit[0];
                        exploitAnnees += dureeExploit[1];
                    }
                    break;
                default:
                    break;
            }
            // Affichages des durées d'exploitation
            txtDureeExploitEnJours.Text = String.Format("{0: # ###}", exploitJours);
            txtDureeExploitEnAnnees.Text = String.Format("{0:0.#}", exploitAnnees);

            // Récupération des durées de location
            switch (this.infoBien["type"])
            {
                case "bien":
                    this.req = $"SELECT * FROM location WHERE idbien={this.infoBien["id"]}";
                    break;
                case "groupe":
                    this.req = $"SELECT * FROM location WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = {this.infoBien["id"]})";
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
                // Si la location est l'actuelle location
                if (debutLoc < today && finLoc > today)
                {
                    this.idLocActuelle = reader.GetString(0);
                    if (finLoc > today.AddDays(30))
                    {
                        finLoc = today.AddDays(30);
                        this.dureeLocActuelle = finLoc.Subtract(debutLoc).Days + 1;
                    }
                    else
                    {
                        this.dureeLocActuelle = today.Subtract(debutLoc).Days + 1;
                    }
                }
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
            if (this.infoBien["type"].Equals("groupe"))
            {
                dureeTotaleDeLoc /= this.nbDeBiens;
            }
            double vacanceJours = exploitJours / this.nbDeBiens - dureeTotaleDeLoc;
            float vacancePrc = (float)Math.Round(vacanceJours / (exploitJours / this.nbDeBiens) * 100, 1);
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
            if (this.infoBien["type"].Equals("bien"))
            {
                this.req = $"SELECT COUNT(idlocation) AS 'Nb de loc' FROM (SELECT idlocation FROM location WHERE idbien={this.infoBien["id"]}) AS req";
            }
            else
            {
                this.req = $"SELECT COUNT(idlocation) AS 'Nb de loc' FROM location WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = \'{this.infoBien["id"]}\')";
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
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            try
            {
                if (reader.GetDateTime(0) > DateTime.Now.AddDays(30))
                {
                    finExploit = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
                }
                else
                {
                    finExploit = $"{reader.GetDateTime(0):d}";
                }
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
        /// Remplit le champ des charges annuelles pour un groupe de biens
        /// </summary>
        /// <returns>Montant des charges annuelles pour ce groupe de biens</returns>
        public float RecupChargesAnnuGrpe()
        {
            float totalCh;
            this.req = $"SELECT SUM(chargeannuelles) AS 'total' FROM bien " +
                $"WHERE idbien IN ({string.Join(",", this.bienSelectionne.ConvertAll(v => v.ToString()))})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            //totalCh = reader["total"].ToString();
            totalCh = reader.GetFloat(0);
            reader.Close();
            return totalCh;
        }


        /// <summary>
        /// Remplit les champs LoyerHC, Charges, Charges imputables et LoyerCC pour un groupe de biens
        /// </summary>
        public void RemplirLoyerChargeGrpe()
        {
            this.req = $"SELECT SUM(loyerHC) AS 'Loyers HC', SUM(charges) AS 'Total charges', SUM(loyercc) AS 'Loyers CC', " +
                "SUM(chargesimputables) AS 'Imputables' FROM bien " +
                $"WHERE idbien IN ({string.Join(",", this.bienSelectionne.ConvertAll(v => v.ToString()))})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            //txtLoyerHC.Text = reader["Loyers HC"].ToString() + " €";
            txtLoyerHC.Text = reader.GetInt32(0).ToString("N") + " €";
            //txtCharges.Text = reader["Total charges"].ToString() + " €";
            txtCharges.Text = reader.GetInt32(1).ToString("N") + " €";
            //txtLoyerCC.Text = reader["Loyers CC"].ToString() + " €";
            txtLoyerCC.Text = reader.GetInt32(2).ToString("N") + " €";
            //txtChargesImputables.Text = reader["Imputables"].ToString() + " €";
            txtChargesImputables.Text = reader.GetInt32(3).ToString("N") + " €";
            reader.Close();
        }


        /// <summary>
        /// Remplit le DataGridView avec la liste des locations
        /// </summary>
        public void RemplirListeLocations()
        {
            if (this.infoBien["type"].Equals("bien"))
            {
                this.req = "SELECT CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', nomlocataire) AS 'Locataire', " +
                    "debutlocation AS 'Début de location', LEAST(finlocation, DATE_ADD(CURRENT_DATE(), INTERVAL 30 DAY)) AS 'Fin de location', " +
                    "CONCAT(ROUND(DATEDIFF(LEAST(finlocation, DATE_ADD(CURRENT_DATE(), INTERVAL 30 DAY)), DATE_SUB(debutlocation, INTERVAL 1 DAY)) / 30.417, 1), ' mois') AS 'Durée' " +
                    $"FROM location NATURAL JOIN locataire WHERE idbien = {this.infoBien["id"]} ORDER BY debutlocation DESC";
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


        /// <summary>
        /// Met à jour la chart
        /// </summary>
        public void CompleterChartCF(List<int> lesAnnees, List<int> lesBiens)
        {
            chartCF.Series["CA annuel"].Points.Clear();
            chartCF.Series["Charges annuelles"].Points.Clear();
            // Récupère le CA par année
            Dictionary<int, float> lesCA = new Dictionary<int, float>();
            foreach (int annee in lesAnnees)
            {
                this.req = "SELECT SUM(montantpaye) FROM paiement NATURAL JOIN location NATURAL JOIN bien " +
                      $"WHERE periodefacturee LIKE '{annee}%' AND " +
                      $"idbien IN ({string.Join(",", lesBiens.ConvertAll(v => v.ToString()))})";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                lesCA.Add(annee, reader.GetFloat(0));
                reader.Close();
            }

            // Intègre les données par année dans le graphique
            foreach (var uneAnnee in lesCA)
            {
                chartCF.Series["CA annuel"].Points.AddXY(uneAnnee.Key, uneAnnee.Value);
                chartCF.Series["Charges annuelles"].Points.AddXY(uneAnnee.Key, GetChargesAnnuelles(uneAnnee.Key));
            }
        }


        /// <summary>
        /// Calcule le total des charges payées sur une année
        /// </summary>
        /// <param name="annee">Année pour laquelle on veut calculer le montant des charges annuelles</param>
        /// <returns>Montant des charges annuelles</returns>
        public float GetChargesAnnuelles(int annee)
        {
            // Déclarations
            float ch, chargesFixes = 0, chargesPonctuelles = 0, chargesAnnuelles;
            MySqlDataReader reader;

            // Charges fixes
            foreach (int bien in this.bienSelectionne)
            {
                // Récupère les charges fixes de l'année pour le bien
                this.req = "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles " +
                    $"WHERE refFrequence != 'Ponctuelle' AND idbien = {bien}";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                reader = this.command.ExecuteReader();
                reader.Read();
                ch = reader.GetFloat(0);
                reader.Close();

                // S'il s'agit de la première année d'exploitation, faire un prorata des charges annuelles
                // Récupère la date de la première mise en location
                this.req = $"SELECT MIN(debutlocation) AS 'premiereloc' FROM location WHERE idbien = {bien}";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                reader = this.command.ExecuteReader();
                reader.Read();
                string moisDebutExploit = reader["premiereloc"].ToString();
                reader.Close();

                // Calcule le prorata de charges et l'additionne au total des charges fixes
                if (annee == int.Parse(moisDebutExploit.Substring(6, 4)))
                {
                    chargesFixes += ch / 12 * (13 - int.Parse(moisDebutExploit.Substring(0, 2)));
                }
                else if (annee < int.Parse(moisDebutExploit.Substring(6, 4)))
                {
                    chargesFixes += 0;
                }
                else
                {
                    chargesFixes += ch;
                }
            }

            // Charges ponctuelles
            foreach (int bien in this.bienSelectionne)
            {
                this.req = "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles " +
                    $"WHERE refFrequence = 'Ponctuelle' AND annee = {annee} AND idbien = {bien}";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                reader = this.command.ExecuteReader();
                reader.Read();
                chargesPonctuelles += reader.GetFloat(0);
                reader.Close();
            }

            // Calcule le total annuel
            chargesAnnuelles = chargesFixes + chargesPonctuelles;
            return chargesAnnuelles;
        }
    }
}
