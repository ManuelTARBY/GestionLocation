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
        private MySqlCommand command;

        /// <summary>
        /// Constructeur de la fenêtre Stats
        /// </summary>
        public Stats()
        {
            InitializeComponent();
            RemplirComboBiens();
            cbxListBiens.SelectedIndex = 0;
            RemplirComboAnnee();
        }


        /// <summary>
        /// Gère le chargement de la fenêtre Stats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stats_Load(object sender, EventArgs e)
        {
            this.v_ca_annuelTableAdapter.Fill(this.gestionlocationDataSet.v_ca_annuel);

        }


        /// <summary>
        /// Remplit la combo des années
        /// </summary>
        private void RemplirComboAnnee()
        {
            cbxAnnee.Items.Clear();
            // Détermine la première année d'exploitation
            int anneeMini;
            this.req = "SELECT MIN(YEAR(debutlocation)) FROM location";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            anneeMini = reader.GetInt32(0);
            reader.Close();

            // Remplit la combo de la première année à l'année actuelle
            for (int i = anneeMini; i <= DateTime.Now.Year; i++)
            {
                cbxAnnee.Items.Add(i);
            }
        }


        /// <summary>
        /// Remplit la combo avec les noms des biens
        /// </summary>
        private void RemplirComboBiens()
        {
            cbxListBiens.Items.Clear();
            this.req = "SELECT nombien FROM bien ORDER BY nombien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                cbxListBiens.Items.Add($"{reader["nombien"]}");
                finCurseur = !reader.Read();
            }
            reader.Close();
        }



        /// <summary>
        /// Calcule le cash-flow annuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            float caAnnuel, chargesAnnuelles;
            // Détermine le CA annuel pour l'année sélectionnée
            this.req = $"SELECT SUM(montantpaye) FROM paiement WHERE periodefacturee LIKE '{cbxAnnee.SelectedItem}%'";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            caAnnuel = reader.GetFloat(0);
            reader.Close();
            txtCAAnnuel.Text = caAnnuel.ToString("N") + " €";

            // Détermine les charges annuelles pour l'année sélectionnée
            this.req = "SELECT SUM(chargeannuelle) + COALESCE((SELECT SUM(chargeannuelle) AS 'Charges ponctuelles 2024'" +
                $" FROM chargesannuelles WHERE annee = {cbxAnnee.SelectedItem}),0) AS 'Charges annuelles' FROM chargesannuelles " +
                $"WHERE refFrequence != 'Ponctuelle'";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            chargesAnnuelles = reader.GetFloat(0);
            reader.Close();
            txtChargesAnnuelles.Text = chargesAnnuelles.ToString("N") + " €";

            // Affiche le cash flow annuel pour l'année sélectionné
            txtCFAnnuel.Text = (caAnnuel - chargesAnnuelles).ToString("N") + " €";
        }
    }
}
