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
    public partial class Stats : Form
    {
        // Propriétés
        private string req;
        private string reqMini;
        private string reqMaxi;
        private MySqlCommand command;
        private int bienSelectionne;

        /// <summary>
        /// Constructeur de la fenêtre Stats
        /// </summary>
        public Stats()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.bienSelectionne = 0;
            RemplirComboBien();
            cbxBien.Focus();
        }


        /// <summary>
        /// Remplit le combo de la liste des biens
        /// </summary>
        public void RemplirComboBien()
        {
            cbxBien.Items.Clear();
            cbxBien.Items.Add("<Tous>");
            this.req = "SELECT nombien FROM bien ORDER BY nombien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cbxBien.Items.Add(reader["nombien"]);
                }
            }
            reader.Close();
        }


        /// <summary>
        /// Remplit la combo des années
        /// </summary>
        private void RemplirComboAnnee()
        {
            cbxAnnee.Items.Clear();
            int anneeMini, anneeMaxi;
            // Si c'est un bien qui a été sélectionné
            if (!cbxBien.SelectedIndex.Equals(0)) {
                // Récupère l'id du bien sélectionné
                this.req = $"SELECT idbien FROM bien WHERE nombien = \"{cbxBien.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader dreader = this.command.ExecuteReader();
                dreader.Read();
                this.bienSelectionne = (int)dreader["idbien"];
                dreader.Close();
                // Construit les requêtes
                // Première année d'exploitation
                this.reqMini = $"SELECT MIN(YEAR(debutlocation)) FROM location WHERE idbien = {this.bienSelectionne}";
                // Dernière année d'exploitation (max = année en cours)
                this.reqMaxi = $"SELECT LEAST(MAX(YEAR(finlocation)), YEAR(CURDATE())) FROM location WHERE idbien = {this.bienSelectionne}";
            }
            else
            {
                this.bienSelectionne = 0;
                // Construit les requêtes
                // Première année d'exploitation
                this.reqMini = "SELECT MIN(YEAR(debutlocation)) FROM location";
                // Dernière année d'exploitation (max = année en cours)
                this.reqMaxi = "SELECT LEAST(MAX(YEAR(finlocation)), YEAR(CURDATE())) FROM location";
            }

            // Détermine la première année d'exploitation
            this.command = new MySqlCommand(this.reqMini, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            anneeMini = reader.GetInt32(0);
            reader.Close();

            // Détermine la dernière année d'exploitation (sans dépasser l'année actuelle)
            this.command = new MySqlCommand(this.reqMaxi, Global.Connexion);
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
        /// Calcule le cash-flow annuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            float caAnnuel, chargesFixes, chargesPonctuelles, chargesAnnuelles;
            // Détermine le CA annuel pour l'année sélectionnée
            if (cbxBien.SelectedIndex.Equals(0))
            {
                this.req = $"SELECT SUM(montantpaye) FROM paiement WHERE periodefacturee LIKE '{cbxAnnee.SelectedItem}%'";
            }
            else
            {
                this.req = "SELECT SUM(montantpaye) FROM paiement NATURAL JOIN location NATURAL JOIN bien "+
                          $"WHERE periodefacturee LIKE '{cbxAnnee.SelectedItem}%' AND idbien = {this.bienSelectionne}";
            }
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            caAnnuel = reader.GetFloat(0);
            reader.Close();
            txtCAAnnuel.Text = caAnnuel.ToString("N") + " €";

            // Détermine les charges annuelles pour l'année sélectionnée
            // Charges fixes
            this.req = "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles "+
                $"WHERE refFrequence != 'Ponctuelle'";
            if (!cbxBien.SelectedIndex.Equals(0))
            {
                this.req += $" AND idbien = {this.bienSelectionne}";
            }
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            chargesFixes = reader.GetFloat(0);
            reader.Close();
            // Charges ponctuelles
            this.req = "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles "+
                $"WHERE refFrequence = 'Ponctuelle' AND annee = {cbxAnnee.SelectedItem}";
            if (!cbxBien.SelectedIndex.Equals(0))
            {
                this.req += $" AND idbien = {this.bienSelectionne}";
            }
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            chargesPonctuelles = reader.GetFloat(0);
            reader.Close();
            chargesAnnuelles = chargesFixes + chargesPonctuelles;
            txtChargesAnnuelles.Text = chargesAnnuelles.ToString("N") + " €";

            // Affiche le cash flow annuel pour l'année sélectionnée
            txtCFAnnuel.Text = (caAnnuel - chargesAnnuelles).ToString("N") + " €";
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
            RemplirComboAnnee();
        }
    }
}
