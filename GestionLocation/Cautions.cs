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
    public partial class Cautions : Form
    {

        private MySqlCommand command;
        private string req;

        /// <summary>
        /// Constructeur Cautions
        /// </summary>
        /// <param name="connexion">Connexion SQL appelant la fenêtre</param>
        public Cautions()
        {
            InitializeComponent();
            RemplirLstCautions();
        }

        /// <summary>
        /// Gère le remplissage de la liste des cautions
        /// </summary>
        public void RemplirLstCautions()
        {
            lstCautions.Items.Clear();
            ConstruitReqListeCautions();
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstCautions.Items.Add($"{reader["nomcompletcaution"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Construit la requête qui sert à remplir la listBox des cautions
        /// </summary>
        private void ConstruitReqListeCautions()
        {
            this.req = "SELECT nomcompletcaution FROM caution WHERE cautionarchivee = ";
            if (rdbCautionArchive.Checked)
            {
                this.req += "1";
            }
            else
            {
                this.req += "0";
            }
            this.req += " ORDER BY nomcaution";
        }

        /// <summary>
        /// Gère le rafraîchissement de la recherche des cautions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRechercher_Click(object sender, EventArgs e)
        {
            RemplirLstCautions();
        }

        /// <summary>
        /// Inverse le statut d'archive de la caution sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArchiver_Click(object sender, EventArgs e)
        {
            if (lstCautions.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une caution dans la liste pour pouvoir l'archiver ou la désarchiver.");
            }
            else
            {
                // Requête pour récupérer la valeur de locatairearchive pour le bien passé en paramètre
                this.command = new MySqlCommand($"SELECT cautionarchivee FROM caution WHERE nomcompletcaution = \"{lstCautions.SelectedItem}\"", Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.req = $"UPDATE caution SET cautionarchivee = {!(bool)reader["cautionarchivee"]} WHERE nomcompletcaution = \"{lstCautions.SelectedItem}\"";
                reader.Close();
                // Exécute la requête de modification
                ExecuteReqIUD();
                // Met à jour la liste des cautions
                RemplirLstCautions();
            }
        }

        /// <summary>
        /// Exécute une requête insert, update, delete
        /// </summary>
        private void ExecuteReqIUD()
        {
            this.command = new MySqlCommand(this.req, Global.Connexion);
            // préparation de la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }

        /// <summary>
        /// Gère la suppression de la caution sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstCautions.SelectedItem == null)
            {
                MessageBox.Show("Veuillez saisir une caution dans la liste pour pouvoir la supprimer.");
            }
            else
            {
                // Demande confirmation de suppression du bien
                DialogResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la caution : {lstCautions.SelectedItem} ?", "Confirmer suppression", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.req = $"SELECT idcaution FROM caution WHERE nomcompletcaution = \"{lstCautions.SelectedItem}\"";
                    this.command = new MySqlCommand(this.req, Global.Connexion);
                    MySqlDataReader reader = this.command.ExecuteReader();
                    reader.Read();
                    int id = reader.GetInt32(0);
                    reader.Close();
                    if (VerifIntegrite(id) == true)
                    {
                        this.req = $"DELETE FROM caution WHERE nomcompletcaution = \"{lstCautions.SelectedItem}\"";
                        ExecuteReqIUD();
                        RemplirLstCautions();
                    }
                    else
                    {
                        MessageBox.Show("Cette caution est reliée à une ou plusieurs locations. Vous ne pouvez pas la supprimer.");
                    }
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre AjoutModifCaution pour réaliser un ajout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            AjoutModifCautions modifCaution = new AjoutModifCautions(this, "INSERT INTO");
            modifCaution.ShowDialog();
        }

        /// <summary>
        /// Ouvre la fenêtre AjoutModifCaution pour réaliser une modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstCautions.SelectedItem != null)
            {
                // Récupère l'id du locataire sélectionné à l'aide d'une requête Select
                this.req = $"SELECT idcaution FROM caution WHERE nomcompletcaution = \"{lstCautions.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                int id = reader.GetInt32(0);
                reader.Close();
                // Crée puis ouvre la fenêtre d'ajout/modif caution
                AjoutModifCautions modifCaution = new AjoutModifCautions(this, "UPDATE", id);
                modifCaution.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une caution dans la liste pour pouvoir la modifier.");
            }
        }

        /// <summary>
        /// Vérifie si une caution n'est pas liée à une ou plusieurs locations
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True s'il n'y a pas de conflit d'intégrité, False dans le cas contraire</returns>
        private bool VerifIntegrite(int id)
        {
            this.req = $"SELECT idlocation FROM location WHERE idcaution = {id}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
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
