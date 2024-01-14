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
    }
}
