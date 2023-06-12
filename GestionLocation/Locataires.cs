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
    public partial class Locataires : Form
    {

        private readonly MySqlConnection connexion;
        private MySqlCommand command;
        private string req;

        /// <summary>
        /// Constructeur de Locataires
        /// </summary>
        /// <param name="connexion">Connexion SQL appelant la fenêtre</param>
        public Locataires(MySqlConnection connexion)
        {
            this.connexion = connexion;
            InitializeComponent();
            RemplirLstLocataires();
        }

        /// <summary>
        /// Gère le remplissage de la liste des locataires
        /// </summary>
        public void RemplirLstLocataires()
        {
            lstLocataires.Items.Clear();
            this.command = new MySqlCommand(ConstruitReqListeLocataires(), this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstLocataires.Items.Add($"{reader["nomcompletlocataire"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Construit la requête qui sert à remplir la listBox des locataires
        /// </summary>
        /// <returns></returns>
        private string ConstruitReqListeLocataires()
        {
            this.req = "SELECT nomcompletlocataire FROM locataire WHERE locatairearchive = ";
            if (rdbLocataireArchive.Checked)
            {
                this.req += "1";
            }
            else
            {
                this.req += "0";
            }
            this.req += " ORDER BY nomlocataire";
            return this.req;
        }

        /// <summary>
        /// Met à jour la liste des locataires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRechercher_Click(object sender, EventArgs e)
        {
            RemplirLstLocataires();
        }

        /// <summary>
        /// Change le statut d'archive d'un locataire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArchiver_Click(object sender, EventArgs e)
        {
            if (lstLocataires.SelectedItem == null)
            {
                MessageBox.Show("Veuillez saisir un locataire dans la liste pour pouvoir l'archiver ou le désarchiver.");
            }
            else
            {
                // Requête pour récupérer la valeur de locatairearchive pour le bien passé en paramètre
                this.command = new MySqlCommand($"SELECT locatairearchive FROM locataire WHERE nomcompletlocataire = \"{lstLocataires.SelectedItem}\"", this.connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.req = $"UPDATE locataire SET locatairearchive = {!(bool)reader["locatairearchive"]} WHERE nomcompletlocataire = \"{lstLocataires.SelectedItem}\"";
                reader.Close();
                // Exécute la requête de modification
                ExecuteReqIUD();
                // Met à jour la liste des locataires
                RemplirLstLocataires();
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
        /// Gère la suppression d'un locataire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstLocataires.SelectedItem == null)
            {
                MessageBox.Show("Veuillez saisir un locataire dans la liste pour pouvoir le supprimer.");
            }
            else
            {
                this.req = $"SELECT idlocataire FROM locataire WHERE nomcompletlocataire = \"{lstLocataires.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, this.connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                int id = reader.GetInt32(0);
                reader.Close();
                if (VerifIntegrite(id) == true)
                {
                    this.req = $"DELETE FROM locataire WHERE nomcompletlocataire = \"{lstLocataires.SelectedItem}\"";
                    ExecuteReqIUD();
                    RemplirLstLocataires();
                }
                else
                {
                    MessageBox.Show("Ce locataire est relié à une ou plusieurs locations. Vous ne pouvez pas le supprimer.");
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout/modification de locataire pour création
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            AjoutModifLocataires modifLocataire = new AjoutModifLocataires(this, this.connexion, "INSERT INTO");
            modifLocataire.ShowDialog();
        }

        /// <summary>
        /// Ouvre le fenêtre d'ajout/modification de locataire pour modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstLocataires.SelectedItem != null)
            {
                // Récupère l'id du locataire sélectionné à l'aide d'une requête Select
                this.req = $"SELECT idlocataire FROM locataire WHERE nomcompletlocataire = \"{lstLocataires.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, this.connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                int id = reader.GetInt32(0);
                reader.Close();
                // Crée puis ouvre la fenêtre d'ajout/modif locataire
                AjoutModifLocataires modifLocataire = new AjoutModifLocataires(this, this.connexion, "UPDATE", id);
                modifLocataire.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un locataire dans la liste pour pouvoir le modifier.");
            }
        }

        /// <summary>
        /// Vérifie si un locataire n'est pas lié à une ou plusieurs locations
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True s'il n'y a pas de conflit d'intégrité, False dans le cas contraire</returns>
        private bool VerifIntegrite(int id)
        {
            this.req = $"SELECT idlocation FROM location WHERE idlocataire = {id}";
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
