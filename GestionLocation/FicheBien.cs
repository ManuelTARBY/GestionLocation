using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestionLocation
{
    public partial class FicheBien : Form
    {
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
            Series serieCharges = new Series("Charges annuelles")
            {
                ChartType = SeriesChartType.Line
            };
            chartCF.Series.Add(serieCharges);
            GetListeDesBiensSelectionnes();
            GetLesAnnees();
            this.nbDeBiens = 1;
            RemplirChamps();
        }

        /// <summary>
        /// Récupère les années d'exploitation pour un bien ou un groupe de bien et met à jour le graphique.
        /// Si aucune location n'existe encore pour le(s) bien(s), affiche le graphique vide plutôt que de planter.
        /// </summary>
        public void GetLesAnnees()
        {
            int? anneeMini = RecupAnneeMinMax(this.bienSelectionne, min: true);
            int? anneeMaxi = RecupAnneeMinMax(this.bienSelectionne, min: false);

            if (anneeMini == null || anneeMaxi == null)
            {
                int anneeCourante = DateTime.Now.Year;
                chartCF.ChartAreas[0].AxisX.Minimum = anneeCourante;
                chartCF.ChartAreas[0].AxisX.Maximum = anneeCourante;
                chartCF.ChartAreas[0].AxisX.Interval = 1;
                CompleterChartCF(new List<int>(), this.bienSelectionne);
                return;
            }

            List<int> lesAnnees = new List<int>();
            for (int i = anneeMini.Value; i <= anneeMaxi.Value; i++)
            {
                lesAnnees.Add(i);
            }

            chartCF.ChartAreas[0].AxisX.Minimum = anneeMini.Value;
            chartCF.ChartAreas[0].AxisX.Maximum = anneeMaxi.Value;
            chartCF.ChartAreas[0].AxisX.Interval = 1;
            CompleterChartCF(lesAnnees, this.bienSelectionne);
        }

        /// <summary>
        /// Récupère l'année min ou max d'exploitation pour une liste de biens.
        /// Retourne null si aucune location n'existe (au lieu de planter sur un MIN/MAX NULL).
        /// </summary>
        private int? RecupAnneeMinMax(List<int> lesBiens, bool min)
        {
            if (lesBiens.Count == 0)
            {
                return null;
            }

            string idsParams = string.Join(",", lesBiens.Select((_, idx) => $"@id{idx}"));
            string req = min
                ? $"SELECT MIN(YEAR(debutlocation)) FROM location WHERE idbien IN ({idsParams})"
                : $"SELECT LEAST(MAX(YEAR(finlocation)), YEAR(CURDATE())) FROM location WHERE idbien IN ({idsParams})";

            using var command = new MySqlCommand(req, Global.Connexion);
            for (int i = 0; i < lesBiens.Count; i++)
            {
                command.Parameters.AddWithValue($"@id{i}", lesBiens[i]);
            }

            using var reader = command.ExecuteReader();
            if (reader.Read() && !reader.IsDBNull(0))
            {
                return reader.GetInt32(0);
            }
            return null;
        }

        /// <summary>
        /// Permet de récupérer les infos sur le bien ou le groupe de bien sélectionné
        /// </summary>
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
                return;
            }

            const string req = "SELECT idbien FROM lignegroupe WHERE idgroupe = @idgroupe";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@idgroupe", int.Parse(this.infoBien["id"]));

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.bienSelectionne.Add(reader.GetInt32(0));
            }
        }

        /// <summary>
        /// Renvoie le type de bien sélectionné
        /// </summary>
        public string GetTypeBien()
        {
            return this.infoBien["type"];
        }

        /// <summary>
        /// Retourne la liste des biens sélectionnés
        /// </summary>
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
            if (this.idLocActuelle.Equals("0") || this.infoBien["type"].Equals("groupe"))
            {
                txtDureeOccup.Visible = false;
                lblDureeOccup.Visible = false;
                return;
            }

            const string req = "SELECT idlocataire FROM location WHERE idlocation = @idLoc";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@idLoc", this.idLocActuelle);

            using var reader = command.ExecuteReader();
            reader.Read();
            string idLocataire = reader.GetString(0);
            reader.Close();

            txtActuelLocat.Text = RecupLocataire(idLocataire);
            txtDureeOccup.Text = ConvertJoursVersMois(this.dureeLocActuelle);
        }

        /// <summary>
        /// Récupère le nom complet d'un locataire à partir de son id
        /// </summary>
        public string RecupLocataire(string idLocat)
        {
            const string req =
                "SELECT CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', SUBSTRING_INDEX(nomlocataire, ',', 1)) " +
                "FROM locataire WHERE idlocataire = @id";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", idLocat);

            using var reader = command.ExecuteReader();
            reader.Read();
            return reader.GetString(0);
        }

        /// <summary>
        /// Gère les couleurs d'alerte en cas d'anomalie.
        /// Ne tente plus de parser "-" en float (cause du plantage quand les charges
        /// annuelles ne sont pas renseignées pour un bien).
        /// </summary>
        public void AppliquerCouleurs()
        {
            float charges = float.Parse(txtCharges.Text.Replace(" €", ""));
            float chImput = txtChargesImputables.Text.Equals("-")
                ? 0
                : float.Parse(txtChargesImputables.Text.Replace(" €", ""));

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

            // Le seuil de rentabilité n'est pas calculable si les charges annuelles sont inconnues
            if (txtSeuilRenta.Text.Equals("-"))
            {
                txtVacanceLocative.BackColor = System.Drawing.SystemColors.Control;
                txtVacanceLocative.ForeColor = System.Drawing.SystemColors.WindowText;
                return;
            }

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
                    RemplirBienUnique();
                    break;
                case "groupe":
                    RemplirBienGroupe();
                    break;
            }
            CalculSeuilRenta(txtLoyerCC.Text);
        }

        private void RemplirBienUnique()
        {
            const string req =
                "SELECT adressebien, cpbien, villebien, loyerhc, charges, loyercc, " +
                "chargesimputables, chargeannuelles, bienarchive " +
                "FROM bien WHERE idbien = @id";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", this.infoBien["id"]);

            using var reader = command.ExecuteReader();
            reader.Read();

            lblNomBien.Text = $"{this.infoBien["nom"].ToUpper()}   -   " +
                $"{reader.GetString("adressebien")} {reader.GetString("cpbien")} {reader.GetString("villebien").ToUpper()}";
            txtLoyerHC.Text = $"{reader.GetFloat("loyerhc"):N} €";
            txtCharges.Text = $"{reader.GetFloat("charges"):N} €";
            txtLoyerCC.Text = $"{reader.GetFloat("loyercc"):N} €";

            txtChargesImputables.Text = reader.IsDBNull(reader.GetOrdinal("chargesimputables"))
                ? "-"
                : $"{reader.GetFloat("chargesimputables"):N} €";

            // chargeannuelles est un INT en base (pas un FLOAT) : lire avec GetInt32,
            // pas GetFloat, sinon la lecture échoue systématiquement (colonne toujours
            // affichée à "-" même quand elle est renseignée).
            txtChargesAnnuelles.Text = reader.IsDBNull(reader.GetOrdinal("chargeannuelles"))
                ? "-"
                : $"{reader.GetInt32("chargeannuelles"):N} €";

            txtArchive.Text = reader.GetBoolean("bienarchive") ? "Oui" : "Non";
        }

        private void RemplirBienGroupe()
        {
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
        }

        /// <summary>
        /// Remplit les champs de la fenêtre issus de la table Location
        /// </summary>
        public void RemplirLocation()
        {
            CalculNbLoc();
            double exploitJours;
            double exploitAnnees;

            switch (this.infoBien["type"])
            {
                case "bien":
                    this.nbDeBiens = 1;
                    txtDebutExploit.Text = CalculDebutExploit(this.infoBien["id"]);
                    txtFinExploit.Text = CalculFinExploit(this.infoBien["id"]);
                    double[] duree = CalculDureeExploit(txtDebutExploit.Text, txtFinExploit.Text);
                    exploitJours = duree[0];
                    exploitAnnees = duree[1];
                    break;

                case "groupe":
                    this.nbDeBiens = this.bienSelectionne.Count;
                    exploitJours = 0;
                    exploitAnnees = 0;
                    foreach (int idBien in this.bienSelectionne)
                    {
                        string debutExploit = CalculDebutExploit(idBien.ToString());
                        string finExploit = CalculFinExploit(idBien.ToString());
                        double[] d = CalculDureeExploit(debutExploit, finExploit);
                        exploitJours += d[0];
                        exploitAnnees += d[1];
                    }
                    break;

                default:
                    exploitJours = 0;
                    exploitAnnees = 0;
                    break;
            }

            txtDureeExploitEnJours.Text = string.Format("{0: # ###}", exploitJours);
            txtDureeExploitEnAnnees.Text = string.Format("{0:0.#}", exploitAnnees);

            // Récupération des durées de location
            string req = this.infoBien["type"].Equals("bien")
                ? "SELECT idlocation, debutlocation, finlocation FROM location WHERE idbien = @id"
                : "SELECT idlocation, debutlocation, finlocation FROM location " +
                  "WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = @id)";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", this.infoBien["id"]);

            List<int> lesDureesDeLoc = new List<int>();
            DateTime today = DateTime.Now;

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateTime debutLoc = reader.GetDateTime("debutlocation").Date;
                    DateTime finLoc = reader.GetDateTime("finlocation").Date;

                    if (debutLoc < today && finLoc > today)
                    {
                        this.idLocActuelle = reader.GetInt32("idlocation").ToString();
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
            }

            int dureeTotaleDeLoc = 0, dureeMini = 10000, dureeMaxi = 0;
            foreach (int duree2 in lesDureesDeLoc)
            {
                dureeTotaleDeLoc += duree2;
                dureeMini = Math.Min(dureeMini, duree2);
                dureeMaxi = Math.Max(dureeMaxi, duree2);
            }

            int nbLoc = int.Parse(txtNbLoc.Text);
            if (nbLoc == 0)
            {
                // Aucune location : évite la division par zéro
                txtDureeMoyenneLoc.Text = "-";
                txtDureeMiniLoc.Text = "-";
                txtDureeMaxiLoc.Text = "-";
                txtVacanceLocative.Text = "-";
                return;
            }

            txtDureeMoyenneLoc.Text = ConvertJoursVersMois(dureeTotaleDeLoc / nbLoc);
            txtDureeMiniLoc.Text = ConvertJoursVersMois(dureeMini);
            txtDureeMaxiLoc.Text = ConvertJoursVersMois(dureeMaxi);

            if (this.infoBien["type"].Equals("groupe"))
            {
                dureeTotaleDeLoc /= this.nbDeBiens;
            }

            if (exploitJours == 0)
            {
                txtVacanceLocative.Text = "0 %";
            }
            else
            {
                double vacanceJours = exploitJours / this.nbDeBiens - dureeTotaleDeLoc;
                float vacancePrc = (float)Math.Round(vacanceJours / (exploitJours / this.nbDeBiens) * 100, 1);
                txtVacanceLocative.Text = $"{vacancePrc} %";
            }
        }

        /// <summary>
        /// Convertit un nombre de jours en mois
        /// </summary>
        public string ConvertJoursVersMois(int jours)
        {
            double mois = Math.Round(jours / 30.42, 1);
            return mois.ToString();
        }

        /// <summary>
        /// Gère le clic sur le bouton fermer
        /// </summary>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Remplit le champ relatif au nombre de locations du bien
        /// </summary>
        public void CalculNbLoc()
        {
            string req = this.infoBien["type"].Equals("bien")
                ? "SELECT COUNT(idlocation) FROM location WHERE idbien = @id"
                : "SELECT COUNT(idlocation) FROM location WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = @id)";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", this.infoBien["id"]);
            txtNbLoc.Text = Convert.ToInt32(command.ExecuteScalar()).ToString();
        }

        /// <summary>
        /// Calcule la date de début d'exploitation d'un bien.
        /// Retourne "-" si le bien n'a jamais eu de location (au lieu de planter).
        /// </summary>
        public string CalculDebutExploit(string idBien)
        {
            const string req = "SELECT MIN(debutlocation) FROM location WHERE idbien = @id";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", idBien);

            using var reader = command.ExecuteReader();
            reader.Read();
            if (reader.IsDBNull(0))
            {
                return "-";
            }
            return $"{reader.GetDateTime(0):d}";
        }

        /// <summary>
        /// Calcule la date de fin d'exploitation d'un bien (plafonnée à aujourd'hui + 30 jours).
        /// Retourne "-" si le bien n'a jamais eu de location.
        /// </summary>
        public string CalculFinExploit(string idBien)
        {
            const string req = "SELECT MAX(finlocation) FROM location WHERE idbien = @id";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", idBien);

            using var reader = command.ExecuteReader();
            reader.Read();
            if (reader.IsDBNull(0))
            {
                return "-";
            }

            DateTime fin = reader.GetDateTime(0);
            return fin > DateTime.Now.AddDays(30)
                ? DateTime.Now.AddDays(30).ToString("dd/MM/yyyy")
                : $"{fin:d}";
        }

        /// <summary>
        /// Calcule la durée d'exploitation en jours et en années.
        /// Retourne [0, 0] si aucune donnée n'est disponible (au lieu de planter sur "-").
        /// </summary>
        public double[] CalculDureeExploit(string debExpl, string finExpl)
        {
            if (debExpl.Equals("-") || finExpl.Equals("-"))
            {
                return new double[] { 0, 0 };
            }

            DateTime debutExploit = DateTime.ParseExact(debExpl, "d", null);
            DateTime finExploit = DateTime.ParseExact(finExpl, "d", null);
            TimeSpan dureeExploit = finExploit.Subtract(debutExploit);

            double[] lesDurees = new double[2];
            lesDurees[0] = dureeExploit.Days;
            lesDurees[1] = Math.Round(dureeExploit.TotalDays / 365, 1);
            return lesDurees;
        }

        /// <summary>
        /// Gère le calcul du seuil de rentabilité
        /// </summary>
        public void CalculSeuilRenta(string loyerCC)
        {
            if (txtChargesAnnuelles.Text.Equals("-"))
            {
                txtSeuilRenta.Text = "-";
                txtSeuilRentaJours.Text = "-";
                return;
            }

            float loyCC = float.Parse(loyerCC.Replace(" €", ""));
            float chargesAnnuelles = float.Parse(txtChargesAnnuelles.Text.Replace(" €", ""));
            float loyerCCAnnuel = loyCC * 12;
            float renta = chargesAnnuelles / loyerCCAnnuel * 100;

            txtSeuilRenta.Text = $"{Math.Round(renta, 1)} %";

            float rentaJours = 365 * renta / 100;
            txtSeuilRentaJours.Text = $"{string.Format("{0:0.}", rentaJours)}";
        }

        /// <summary>
        /// Ouvre la page qui liste toutes les charges propres au bien
        /// </summary>
        private void BtnListeCharges_Click(object sender, EventArgs e)
        {
            ListeCharges fenListeCharges = new ListeCharges(this);
            fenListeCharges.ShowDialog();
        }

        /// <summary>
        /// Calcule le montant des charges annuelles pour un groupe de biens
        /// </summary>
        public float RecupChargesAnnuGrpe()
        {
            if (this.bienSelectionne.Count == 0)
            {
                return 0;
            }

            string idsParams = string.Join(",", this.bienSelectionne.Select((_, i) => $"@id{i}"));
            string req = $"SELECT SUM(chargeannuelles) FROM bien WHERE idbien IN ({idsParams})";

            using var command = new MySqlCommand(req, Global.Connexion);
            for (int i = 0; i < this.bienSelectionne.Count; i++)
            {
                command.Parameters.AddWithValue($"@id{i}", this.bienSelectionne[i]);
            }

            using var reader = command.ExecuteReader();
            reader.Read();
            return reader.IsDBNull(0) ? 0 : reader.GetFloat(0);
        }

        /// <summary>
        /// Remplit les champs LoyerHC, Charges, Charges imputables et LoyerCC pour un groupe de biens.
        /// Utilisait GetInt32 sur des colonnes FLOAT, ce qui provoquait un plantage systématique
        /// à l'ouverture de la fiche d'un groupe : corrigé en GetFloat.
        /// </summary>
        public void RemplirLoyerChargeGrpe()
        {
            if (this.bienSelectionne.Count == 0)
            {
                txtLoyerHC.Text = txtCharges.Text = txtLoyerCC.Text = txtChargesImputables.Text = "-";
                return;
            }

            string idsParams = string.Join(",", this.bienSelectionne.Select((_, i) => $"@id{i}"));
            string req = "SELECT SUM(loyerHC), SUM(charges), SUM(loyercc), SUM(chargesimputables) " +
                         $"FROM bien WHERE idbien IN ({idsParams})";

            using var command = new MySqlCommand(req, Global.Connexion);
            for (int i = 0; i < this.bienSelectionne.Count; i++)
            {
                command.Parameters.AddWithValue($"@id{i}", this.bienSelectionne[i]);
            }

            using var reader = command.ExecuteReader();
            reader.Read();

            txtLoyerHC.Text = (reader.IsDBNull(0) ? 0 : reader.GetFloat(0)).ToString("N") + " €";
            txtCharges.Text = (reader.IsDBNull(1) ? 0 : reader.GetFloat(1)).ToString("N") + " €";
            txtLoyerCC.Text = (reader.IsDBNull(2) ? 0 : reader.GetFloat(2)).ToString("N") + " €";
            txtChargesImputables.Text = (reader.IsDBNull(3) ? 0 : reader.GetFloat(3)).ToString("N") + " €";
        }

        /// <summary>
        /// Remplit le DataGridView avec la liste des locations
        /// </summary>
        public void RemplirListeLocations()
        {
            if (!this.infoBien["type"].Equals("bien"))
            {
                datListeLocations.Visible = false;
                return;
            }

            const string req =
                "SELECT CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', nomlocataire) AS locataire, " +
                "debutlocation, " +

                // Si debutlocation > date du jour -> debutlocation + 30 jours, sinon -> logique initiale
                "IF(debutlocation > CURRENT_DATE(), " +
                "   DATE_ADD(debutlocation, INTERVAL 30 DAY), " +
                "   LEAST(finlocation, DATE_ADD(CURRENT_DATE(), INTERVAL 30 DAY))" +
                ") AS finlocation_ajustee, " +

                // On réutilise la même condition pour le calcul de la durée
                "CONCAT(ROUND(DATEDIFF(" +
                "   IF(debutlocation > CURRENT_DATE(), " +
                "      DATE_ADD(debutlocation, INTERVAL 30 DAY), " +
                "      LEAST(finlocation, DATE_ADD(CURRENT_DATE(), INTERVAL 30 DAY))" +
                "   ), " +
                "   DATE_SUB(debutlocation, INTERVAL 1 DAY)" +
                ") / 30.417, 1), ' mois') AS duree " +

                "FROM location NATURAL JOIN locataire WHERE idbien = @id ORDER BY debutlocation DESC";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", this.infoBien["id"]);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                datListeLocations.Rows.Add(
                    reader.GetString("locataire"),
                    reader.GetDateTime("debutlocation").ToString("dd/MM/yyyy"),
                    reader.GetDateTime("finlocation_ajustee").ToString("dd/MM/yyyy"),
                    reader.GetString("duree"));
            }
        }

        /// <summary>
        /// Met à jour la chart du cash-flow par année
        /// </summary>
        public void CompleterChartCF(List<int> lesAnnees, List<int> lesBiens)
        {
            chartCF.Series["CA annuel"].Points.Clear();
            chartCF.Series["Charges annuelles"].Points.Clear();

            if (lesBiens.Count == 0)
            {
                return;
            }

            string idsParams = string.Join(",", lesBiens.Select((_, i) => $"@id{i}"));

            foreach (int annee in lesAnnees)
            {
                // COALESCE(..., 0) : évite un plantage si aucun paiement n'existe pour cette année
                string req = "SELECT COALESCE(SUM(montantpaye), 0) FROM paiement NATURAL JOIN location NATURAL JOIN bien " +
                             $"WHERE periodefacturee LIKE @periode AND idbien IN ({idsParams})";

                using var command = new MySqlCommand(req, Global.Connexion);
                command.Parameters.AddWithValue("@periode", $"{annee}%");
                for (int i = 0; i < lesBiens.Count; i++)
                {
                    command.Parameters.AddWithValue($"@id{i}", lesBiens[i]);
                }

                float ca = Convert.ToSingle(command.ExecuteScalar());
                chartCF.Series["CA annuel"].Points.AddXY(annee, ca);
                chartCF.Series["Charges annuelles"].Points.AddXY(annee, GetChargesAnnuelles(annee));
            }
        }

        private void FicheBien_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Calcule le total des charges payées sur une année pour les biens sélectionnés
        /// </summary>
        public float GetChargesAnnuelles(int annee)
        {
            float ch = 0;
            const string req = "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles " +
                                "WHERE idbien = @id AND annee = @annee";

            foreach (int bien in this.bienSelectionne)
            {
                using var command = new MySqlCommand(req, Global.Connexion);
                command.Parameters.AddWithValue("@id", bien);
                command.Parameters.AddWithValue("@annee", annee);
                ch += Convert.ToSingle(command.ExecuteScalar());
            }
            return ch;
        }
    }
}
