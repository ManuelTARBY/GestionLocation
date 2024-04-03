using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Stats : Form
    {
        // Propriétés
        private string req;
        private string reqMini;
        private string reqMaxi;
        private MySqlCommand command;
        private readonly List<int> bienSelectionne;
        private readonly List<string> lesBiens;
        private readonly List<string> lesGroupes;

        /// <summary>
        /// Constructeur de la fenêtre Stats
        /// </summary>
        public Stats()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.bienSelectionne = new List<int>();
            this.lesBiens = new List<string>();
            this.lesGroupes = new List<string>();
            RemplirComboBien();
            cbxBien.Focus();
        }


        /// <summary>
        /// Remplit le combo de la liste des biens et des groupes de bien
        /// </summary>
        public void RemplirComboBien()
        {
            List<string> listeFinale = new List<string>();
            cbxBien.Items.Clear();
            
            // Récupère les biens
            this.req = "SELECT nombien FROM bien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    this.lesBiens.Add(reader.GetString(0));
                }
            }
            reader.Close();

            // Récupère les groupes de biens
            this.req = "SELECT nomdugroupe FROM grpedebiens WHERE nomdugroupe != 'Tous les biens'";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    this.lesGroupes.Add(reader.GetString(0));
                }
            }
            reader.Close();

            // Fusionne les listes et classe par ordre alphabétique
            listeFinale.AddRange(this.lesBiens);
            listeFinale.AddRange(this.lesGroupes);
            listeFinale.Sort();

            // Compose la combobox
            cbxBien.Items.Add("<Tous>");
            foreach (string elt in listeFinale)
            {
                cbxBien.Items.Add(elt);
            }
        }


        /// <summary>
        /// Remplit la combo des années
        /// </summary>
        private void RemplirComboAnnee()
        {
            cbxAnnee.Items.Clear();
            int anneeMini, anneeMaxi;
            // Si ce n'est pas <Tous> qui a été sélectionné
            if (!cbxBien.SelectedIndex.Equals(0)) {
                // Si c'est un bien qui a été sélectionné
                if (this.lesBiens.Contains(cbxBien.SelectedItem.ToString()))
                {
                    // Récupère l'id du bien sélectionné
                    this.req = $"SELECT idbien FROM bien WHERE nombien = \"{cbxBien.SelectedItem}\"";
                }
                // Si c'est un groupe de biens qui a été sélectionné
                else
                {
                    // Récupère les id des biens qui composent le groupe
                    this.req = "SELECT idbien FROM lignegroupe WHERE idgroupe = "+
                        $"(SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = \"{cbxBien.SelectedItem}\")";
                }
            }
            // Si on veut tous les biens
            else
            {
                this.req = $"SELECT idbien FROM bien";
            }

            // Récupère la liste des biens
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    this.bienSelectionne.Add((int)reader["idbien"]);
                }
            }
            reader.Close();

            // Détermine la première année d'exploitation
            this.reqMini = $"SELECT MIN(YEAR(debutlocation)) FROM location " +
                $"WHERE idbien IN ({string.Join(",", this.bienSelectionne.ConvertAll(v => v.ToString()))})";
            this.command = new MySqlCommand(this.reqMini, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            reader.Read();
            anneeMini = reader.GetInt32(0);
            reader.Close();

            // Détermine la dernière année d'exploitation (sans dépasser l'année actuelle)
            this.reqMaxi = $"SELECT LEAST(MAX(YEAR(finlocation)), YEAR(CURDATE())) FROM location " +
                $"WHERE idbien IN ({string.Join(",", this.bienSelectionne.ConvertAll(v => v.ToString()))})";
            this.command = new MySqlCommand(this.reqMaxi, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            reader.Read();
            anneeMaxi = reader.GetInt32(0);
            reader.Close();

            // Remplit la combo de la première année à l'année actuelle
            for (int i = anneeMini; i <= anneeMaxi; i++)
            {
                cbxAnnee.Items.Add(i);
            }
        }


        /// <summary>
        /// Déclenche l'alimentation de la combo de liste des années
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxBien_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCAAnnuel.Text = "";
            txtChargesAnnuelles.Text = "";
            txtCFAnnuel.Text = "";
            txtTauxRemplissage.Text = "";
            this.bienSelectionne.Clear();
            RemplirComboAnnee();
        }


        /// <summary>
        /// Calcule le cash-flow annuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Déclarations
            float caAnnuel = 0, chargesFixes = 0, chargesPonctuelles = 0, chargesAnnuelles = 0, ch = 0, caMax = 0;

            // Détermine le CA annuel pour l'année sélectionnée
            this.req = "SELECT SUM(montantpaye) FROM paiement NATURAL JOIN location NATURAL JOIN bien "+
                      $"WHERE periodefacturee LIKE '{cbxAnnee.SelectedItem}%' AND "+
                      $"idbien IN ({string.Join(",", this.bienSelectionne.ConvertAll(v => v.ToString()))})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            caAnnuel = reader.GetFloat(0);
            reader.Close();
            txtCAAnnuel.Text = caAnnuel.ToString("N") + " €";

            // Détermine le CA max possible sur une année
            this.req = "SELECT SUM(loyercc) * 12 AS 'LoyersMax' FROM bien " +
                      $"WHERE idbien IN ({string.Join(",", this.bienSelectionne.ConvertAll(v => v.ToString()))})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            reader = this.command.ExecuteReader();
            reader.Read();
            caMax = reader.GetFloat(0);
            reader.Close();
            txtTauxRemplissage.Text = (caAnnuel / caMax * 100).ToString("N1") + "%";

            // Détermine les charges annuelles pour l'année sélectionnée
            // Charges fixes
            foreach (int bien in this.bienSelectionne)
            {
                // Récupère les charges fixes de l'année pour le bien
                this.req = "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles "+
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
                if (int.Parse(cbxAnnee.SelectedItem.ToString()) == int.Parse(moisDebutExploit.Substring(6, 4)))
                {
                    chargesFixes += ch / 12 * (13 - int.Parse(moisDebutExploit.Substring(0, 2))); 
                }
                else if (int.Parse(cbxAnnee.SelectedItem.ToString()) < int.Parse(moisDebutExploit.Substring(6, 4)))
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
                this.req = "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles "+
                    $"WHERE refFrequence = 'Ponctuelle' AND annee = {cbxAnnee.SelectedItem} AND idbien = {bien}";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                reader = this.command.ExecuteReader();
                reader.Read();
                chargesPonctuelles += reader.GetFloat(0);
                reader.Close();
            }

            // Calcule le total annuel
            chargesAnnuelles = chargesFixes + chargesPonctuelles;
            txtChargesAnnuelles.Text = chargesAnnuelles.ToString("N") + " €";

            // Affiche le cash flow annuel pour l'année sélectionnée
            txtCFAnnuel.Text = (caAnnuel - chargesAnnuelles).ToString("N") + " €";
        }
    }
}
