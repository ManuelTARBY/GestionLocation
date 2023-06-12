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
    public partial class Biens : Form
    {

        private readonly MySqlConnection connexion;
        private MySqlCommand command;
        private string req;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Biens(MySqlConnection connexion)
        {
            InitializeComponent();
            this.connexion = connexion;
            RemplirLstBiens();
        }

        /// <summary>
        /// Gère le remplissage de la liste des biens
        /// </summary>
        public void RemplirLstBiens()
        {
            lstBiens.Items.Clear();
            this.command = new MySqlCommand(ConstruitReqListeBien(), this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstBiens.Items.Add($"{reader["NomBien"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Construit la requête qui sert à remplir la listBox des biens
        /// </summary>
        /// <returns></returns>
        private string ConstruitReqListeBien()
        {
            this.req = "SELECT nombien FROM bien WHERE bienarchive = ";
            if (rdbBienArchive.Checked)
            {
                this.req += "1";
            }
            else
            {
                this.req += "0";
            }
            this.req += " ORDER BY nombien";
            return this.req;
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout/modification de bien pour modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedItem != null)
            {
                this.req = $"SELECT idbien FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, this.connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                int id = reader.GetInt32(0);
                reader.Close();
                AjoutModifBiens modifBiens = new AjoutModifBiens(this, this.connexion, "UPDATE", id);
                modifBiens.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez saisir un bien dans la liste pour pouvoir le modifier.");
            }
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout/modification de bien pour création
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            AjoutModifBiens modifBiens = new AjoutModifBiens(this, this.connexion, "INSERT INTO");
            modifBiens.ShowDialog();
        }

        private void BtnArchiverDesarchiver_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez saisir un bien dans la liste.");
            }
            else
            {
                // Requête pour récupérer la valeur de bienarchive pour le bien passé en paramètre
                this.command = new MySqlCommand($"SELECT bienarchive FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"", this.connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.req = $"UPDATE bien SET bienarchive = {!(bool)reader["bienarchive"]} WHERE nombien = \"{lstBiens.SelectedItem}\"";
                reader.Close();
                // Exécute la requête de modification
                ExecuteReqIUD();
                // Met à jour la liste des biens
                RemplirLstBiens();
            }
        }

        /// <summary>
        /// Met à jour la liste des biens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRechercher_Click(object sender, EventArgs e)
        {
            RemplirLstBiens();
        }

        /// <summary>
        /// Gère l'appui sur le bouton supprimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez saisir un bien dans la liste pour pouvoir le supprimer.");
            }
            else
            {
                this.req = $"SELECT idbien FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, this.connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                int id = reader.GetInt32(0);
                reader.Close();
                if (VerifIntegrite(id) == true)
                {
                    this.req = $"DELETE FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
                    ExecuteReqIUD();
                    RemplirLstBiens();
                }
                else
                {
                    MessageBox.Show("Ce bien est relié à une ou plusieurs locations. Vous ne pouvez pas le supprimer.");
                }
            }
        }

        /// <summary>
        /// Exécute une requête insert, update, delete
        /// </summary>
        private void ExecuteReqIUD()
        {
            this.command = new MySqlCommand(this.req, this.connexion);
            // préparation de la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }

        /// <summary>
        /// Vérifie si un bien n'est pas liée à une ou plusieurs locations
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True s'il n'y a pas de conflit d'intégrité, False dans le cas contraire</returns>
        private bool VerifIntegrite(int id)
        {
            this.req = $"SELECT idlocation FROM location WHERE idbien = {id}";
            this.command = new MySqlCommand(this.req, this.connexion);
            List<string> liste = new List<string>();
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                liste.Add($"{reader["idlocation"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
            if (liste.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
