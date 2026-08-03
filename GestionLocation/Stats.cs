using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestionLocation
{
    public partial class Stats : Form
    {
        // Propriétés
        private readonly List<int> bienSelectionne;
        private readonly List<int> biensPossibles;
        private readonly List<string> lesBiens;
        private readonly List<string> lesGroupes;

        /// <summary>
        /// Constructeur de la fenêtre Stats
        /// </summary>
        public Stats()
        {
            InitializeComponent();

            chartCF.Series["Series1"].ChartType = SeriesChartType.Line;
            chartCF.Series["Series1"].Name = "CA annuel";

            Series serieCharges = new Series("Charges annuelles")
            {
                ChartType = SeriesChartType.Line
            };
            chartCF.Series.Add(serieCharges);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.bienSelectionne = new List<int>();
            this.biensPossibles = new List<int>();
            this.lesBiens = new List<string>();
            this.lesGroupes = new List<string>();

            RemplirComboBien();
            cbxBien.Focus();
        }

        /// <summary>
        /// Remplit la combobox des biens et des groupes de biens
        /// </summary>
        public void RemplirComboBien()
        {
            List<string> listeFinale = new List<string>();
            cbxBien.Items.Clear();
            this.lesBiens.Clear();
            this.lesGroupes.Clear();

            try
            {
                // Récupère les biens
                const string reqBiens = "SELECT nombien FROM bien";
                using (var cmd = new MySqlCommand(reqBiens, Global.Connexion))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.lesBiens.Add(reader["nombien"].ToString());
                        }
                    }
                }

                // Récupère les groupes de biens
                const string reqGroupes = "SELECT nomdugroupe FROM grpedebiens WHERE nomdugroupe != 'Tous les biens'";
                using (var cmd = new MySqlCommand(reqGroupes, Global.Connexion))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.lesGroupes.Add(reader["nomdugroupe"].ToString());
                        }
                    }
                }

                // Fusionne les listes et trie par ordre alphabétique
                listeFinale.AddRange(this.lesBiens);
                listeFinale.AddRange(this.lesGroupes);
                listeFinale.Sort();

                // Remplit la ComboBox
                cbxBien.Items.Add("<Tous>");
                foreach (string elt in listeFinale)
                {
                    cbxBien.Items.Add(elt);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors du chargement des biens :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Remplit la combo des années selon le bien/groupe sélectionné
        /// </summary>
        private void RemplirComboAnnee()
        {
            cbxAnnee.Items.Clear();
            this.bienSelectionne.Clear();

            if (cbxBien.SelectedItem == null)
                return;

            string bienSelect = cbxBien.SelectedItem.ToString();

            try
            {
                // 1. Récupération des ID de biens concernés
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = Global.Connexion;

                    if (cbxBien.SelectedIndex == 0) // "<Tous>"
                    {
                        cmd.CommandText = "SELECT idbien FROM bien";
                    }
                    else if (this.lesBiens.Contains(bienSelect))
                    {
                        cmd.CommandText = "SELECT idbien FROM bien WHERE nombien = @nom";
                        cmd.Parameters.AddWithValue("@nom", bienSelect);
                    }
                    else
                    {
                        cmd.CommandText = @"
                            SELECT idbien 
                            FROM lignegroupe 
                            WHERE idgroupe = (SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = @nomGroupe)";
                        cmd.Parameters.AddWithValue("@nomGroupe", bienSelect);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.bienSelectionne.Add(Convert.ToInt32(reader["idbien"]));
                        }
                    }
                }

                // Si aucun bien n'est trouvé
                if (this.bienSelectionne.Count == 0)
                {
                    chartCF.Series["CA annuel"].Points.Clear();
                    chartCF.Series["Charges annuelles"].Points.Clear();
                    return;
                }

                // Préparation des paramètres dynamique pour le IN (...)
                var pNames = new List<string>();
                for (int i = 0; i < this.bienSelectionne.Count; i++)
                {
                    pNames.Add($"@id{i}");
                }
                string inClause = string.Join(",", pNames);

                int anneeMini = DateTime.Today.Year;
                int anneeMaxi = DateTime.Today.Year;

                // 2. Détermine la première année d'exploitation
                using (var cmdMini = new MySqlCommand())
                {
                    cmdMini.Connection = Global.Connexion;
                    for (int i = 0; i < this.bienSelectionne.Count; i++)
                    {
                        cmdMini.Parameters.AddWithValue($"@id{i}", this.bienSelectionne[i]);
                    }

                    cmdMini.CommandText = $"SELECT MIN(YEAR(debutlocation)) FROM location WHERE idbien IN ({inClause})";
                    object resMini = cmdMini.ExecuteScalar();
                    if (resMini != null && resMini != DBNull.Value)
                    {
                        anneeMini = Convert.ToInt32(resMini);
                    }
                }

                // 3. Détermine la dernière année d'exploitation
                using (var cmdMaxi = new MySqlCommand())
                {
                    cmdMaxi.Connection = Global.Connexion;
                    for (int i = 0; i < this.bienSelectionne.Count; i++)
                    {
                        cmdMaxi.Parameters.AddWithValue($"@id{i}", this.bienSelectionne[i]);
                    }

                    cmdMaxi.CommandText = $"SELECT LEAST(MAX(YEAR(finlocation)), YEAR(CURDATE())) FROM location WHERE idbien IN ({inClause})";
                    object resMaxi = cmdMaxi.ExecuteScalar();
                    if (resMaxi != null && resMaxi != DBNull.Value)
                    {
                        anneeMaxi = Convert.ToInt32(resMaxi);
                    }
                }

                if (anneeMini > anneeMaxi)
                {
                    anneeMini = anneeMaxi;
                }

                // Remplit la combo des années
                List<int> lesAnnees = new List<int>();
                for (int i = anneeMini; i <= anneeMaxi; i++)
                {
                    cbxAnnee.Items.Add(i);
                    lesAnnees.Add(i);
                }

                // Mise à jour de la charte
                chartCF.ChartAreas[0].AxisX.Minimum = anneeMini;
                chartCF.ChartAreas[0].AxisX.Maximum = anneeMaxi;
                chartCF.ChartAreas[0].AxisX.Interval = 1;

                CompleterChartCF(lesAnnees);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de la récupération des années :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbxBien_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCAAnnuel.Text = string.Empty;
            txtChargesAnnuelles.Text = string.Empty;
            txtCFAnnuel.Text = string.Empty;
            txtTauxRemplissage.Text = string.Empty;
            this.bienSelectionne.Clear();
            RemplirComboAnnee();
        }

        /// <summary>
        /// Met à jour le graphique avec une seule requête globale regroupée par année (Évite le N+1)
        /// </summary>
        public void CompleterChartCF(List<int> lesAnnees)
        {
            chartCF.Series["CA annuel"].Points.Clear();
            chartCF.Series["Charges annuelles"].Points.Clear();

            if (lesAnnees == null || lesAnnees.Count == 0 || this.bienSelectionne.Count == 0)
                return;

            Dictionary<int, decimal> lesCA = new Dictionary<int, decimal>();
            foreach (int annee in lesAnnees)
            {
                lesCA[annee] = 0m;
            }

            try
            {
                var paramNames = new List<string>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = Global.Connexion;
                    for (int i = 0; i < this.bienSelectionne.Count; i++)
                    {
                        string pName = $"@idBien{i}";
                        paramNames.Add(pName);
                        cmd.Parameters.AddWithValue(pName, this.bienSelectionne[i]);
                    }

                    cmd.CommandText = $@"
                        SELECT YEAR(p.periodefacturee) AS annee, COALESCE(SUM(p.montantpaye), 0) AS totalCA
                        FROM paiement p
                        JOIN location l ON p.idlocation = l.idlocation
                        JOIN bien b ON l.idbien = b.idbien
                        WHERE b.idbien IN ({string.Join(",", paramNames)})
                        GROUP BY YEAR(p.periodefacturee)";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["annee"] != DBNull.Value)
                            {
                                int annee = Convert.ToInt32(reader["annee"]);
                                if (lesCA.ContainsKey(annee))
                                {
                                    lesCA[annee] = Convert.ToDecimal(reader["totalCA"]);
                                }
                            }
                        }
                    }
                }

                // Alimentation des séries du graphique
                foreach (var uneAnnee in lesCA)
                {
                    chartCF.Series["CA annuel"].Points.AddXY(uneAnnee.Key, uneAnnee.Value);
                    chartCF.Series["Charges annuelles"].Points.AddXY(uneAnnee.Key, GetChargesAnnuelles(uneAnnee.Key));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de la génération du graphique :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Calcule le cash-flow et le taux de remplissage pour l'année sélectionnée
        /// </summary>
        private void CbxAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAnnee.SelectedItem == null || !int.TryParse(cbxAnnee.SelectedItem.ToString(), out int anneeSelect))
                return;

            if (this.bienSelectionne.Count == 0)
                return;

            decimal caAnnuel = 0m;
            decimal caMax = 0m;

            try
            {
                // 1. Détermine le CA annuel
                var pNames = new List<string>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = Global.Connexion;
                    cmd.Parameters.AddWithValue("@annee", anneeSelect);

                    for (int i = 0; i < this.bienSelectionne.Count; i++)
                    {
                        string pName = $"@id{i}";
                        pNames.Add(pName);
                        cmd.Parameters.AddWithValue(pName, this.bienSelectionne[i]);
                    }

                    cmd.CommandText = $@"
                        SELECT COALESCE(SUM(p.montantpaye), 0) 
                        FROM paiement p
                        JOIN location l ON p.idlocation = l.idlocation
                        JOIN bien b ON l.idbien = b.idbien
                        WHERE YEAR(p.periodefacturee) = @annee 
                        AND b.idbien IN ({string.Join(",", pNames)})";

                    object resCA = cmd.ExecuteScalar();
                    if (resCA != null && resCA != DBNull.Value)
                    {
                        caAnnuel = Convert.ToDecimal(resCA);
                    }
                }
                txtCAAnnuel.Text = $"{caAnnuel:N2} €";

                // 2. Détermine les biens exploités pour l'année sélectionnée
                this.biensPossibles.Clear();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = Global.Connexion;
                    cmd.Parameters.AddWithValue("@annee", anneeSelect);

                    var pNamesBp = new List<string>();
                    for (int i = 0; i < this.bienSelectionne.Count; i++)
                    {
                        string pName = $"@id{i}";
                        pNamesBp.Add(pName);
                        cmd.Parameters.AddWithValue(pName, this.bienSelectionne[i]);
                    }

                    cmd.CommandText = $@"
                        SELECT DISTINCT l.idbien 
                        FROM location l
                        JOIN paiement p ON p.idlocation = l.idlocation
                        WHERE l.idbien IN ({string.Join(",", pNamesBp)})
                        AND YEAR(p.periodefacturee) = @annee";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.biensPossibles.Add(Convert.ToInt32(reader["idbien"]));
                        }
                    }
                }

                // 3. Récupère le CA max possible
                if (this.biensPossibles.Count > 0)
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = Global.Connexion;
                        var pNamesMax = new List<string>();
                        for (int i = 0; i < this.biensPossibles.Count; i++)
                        {
                            string pName = $"@bp{i}";
                            pNamesMax.Add(pName);
                            cmd.Parameters.AddWithValue(pName, this.biensPossibles[i]);
                        }

                        cmd.CommandText = $@"
                            SELECT COALESCE(SUM(loyercc), 0) * 12 
                            FROM bien 
                            WHERE idbien IN ({string.Join(",", pNamesMax)})";

                        object resMax = cmd.ExecuteScalar();
                        if (resMax != null && resMax != DBNull.Value)
                        {
                            caMax = Convert.ToDecimal(resMax);
                        }
                    }
                }

                // 4. Calcule le taux de remplissage (Sécurité division par zéro)
                if (caMax > 0)
                {
                    decimal taux = (caAnnuel / caMax) * 100m;
                    txtTauxRemplissage.Text = $"{taux:N1}%";
                }
                else
                {
                    txtTauxRemplissage.Text = "0,0%";
                }

                // 5. Charges annuelles & Cash-flow
                decimal chargesAnnuelles = GetChargesAnnuelles(anneeSelect);
                txtChargesAnnuelles.Text = $"{chargesAnnuelles:N2} €";

                decimal cashFlow = caAnnuel - chargesAnnuelles;
                txtCFAnnuel.Text = $"{cashFlow:N2} €";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors du calcul du cash-flow :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Calcule le total des charges payées sur une année en une seule requête SQL
        /// </summary>
        /// <param name="annee">Année ciblée</param>
        /// <returns>Montant des charges annuelles</returns>
        public decimal GetChargesAnnuelles(int annee)
        {
            if (this.bienSelectionne.Count == 0)
                return 0m;

            try
            {
                var pNames = new List<string>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = Global.Connexion;
                    cmd.Parameters.AddWithValue("@annee", annee);

                    for (int i = 0; i < this.bienSelectionne.Count; i++)
                    {
                        string pName = $"@id{i}";
                        pNames.Add(pName);
                        cmd.Parameters.AddWithValue(pName, this.bienSelectionne[i]);
                    }

                    cmd.CommandText = $@"
                        SELECT COALESCE(SUM(chargeannuelle), 0) 
                        FROM chargesannuelles 
                        WHERE annee = @annee AND idbien IN ({string.Join(",", pNames)})";

                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0m;
                }
            }
            catch (MySqlException)
            {
                return 0m;
            }
        }
    }
}