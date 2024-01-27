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

        private MySqlCommand command;
        private string req;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Biens()
        {
            InitializeComponent();
            RemplirLstBiens();
        }

        /// <summary>
        /// Gère le remplissage de la liste des biens et des groupes de biens
        /// </summary>
        public void RemplirLstBiens()
        {
            lstBiens.Items.Clear();
            List<string> lesBiens = new List<string>();
            for (int i = 0; i < 2; i++)
            {
                this.command = new MySqlCommand(ConstruitReqListeBien(i), Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                while (reader.Read())
                {
                    // Récupère les biens et les groupe de bien et les tris par ordre alphabétique
                    lesBiens.Add(reader.GetString(0));
                    lesBiens.Sort();
                    //lstBiens.Items.Add(reader.GetString(0));
                }
                reader.Close();
            }
            // Alimente la liste box des biens et groupes de bien
            foreach (string bien in lesBiens)
            {
                lstBiens.Items.Add(bien);
            }
        }

        /// <summary>
        /// Construit la requête qui sert à remplir la listBox des biens
        /// </summary>
        /// <returns>Requête</returns>
        private string ConstruitReqListeBien(int cpt)
        {
            if (cpt == 0)
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
            }
            else if (cpt == 1)
            {
                this.req = "SELECT nomdugroupe FROM grpedebiens ORDER BY nomdugroupe";
            }
            return this.req;
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout/modification de bien pour modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedIndex > -1)
            {
                this.req = $"SELECT idbien FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                if (reader.HasRows == false)
                {
                    reader.Close();
                    MessageBox.Show("Vous avez sélectionné un groupe, veuillez sélectionner un bien.");
                    return;
                }
                int id = reader.GetInt32(0);
                reader.Close();
                AjoutModifBiens modifBiens = new AjoutModifBiens(this, "UPDATE", id);
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
            AjoutModifBiens modifBiens = new AjoutModifBiens(this, "INSERT INTO");
            modifBiens.ShowDialog();
        }

        private void BtnArchiverDesarchiver_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez saisir un bien dans la liste.");
            }
            else
            {
                // Requête pour récupérer la valeur de bienarchive pour le bien passé en paramètre
                this.command = new MySqlCommand($"SELECT bienarchive FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"", Global.Connexion);
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
            if (lstBiens.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez saisir un bien dans la liste pour pouvoir le supprimer.");
            }
            else
            {
                // Demande confirmation de suppression du bien
                DialogResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le bien {lstBiens.SelectedItem} ?", "Confirmer suppression", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.req = $"SELECT idbien FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
                    this.command = new MySqlCommand(this.req, Global.Connexion);
                    MySqlDataReader reader = this.command.ExecuteReader();
                    reader.Read();
                    int id = reader.GetInt32(0);
                    reader.Close();
                    // Si la suppression ne génère pas de problème d'intégrité
                    if (VerifIntegrite(id) == true)
                    {
                        this.req = $"DELETE FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
                        ExecuteReqIUD();
                        RemplirLstBiens();
                    }
                    else
                    {
                        MessageBox.Show("Ce bien est relié à une ou plusieurs locations. Pour pouvoir le supprimer, vous devez d'abord supprimer" +
                            " ces locations.");
                    }
                }
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
        /// Vérifie si un bien n'est pas liée à une ou plusieurs locations
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True s'il n'y a pas de conflit d'intégrité, False dans le cas contraire</returns>
        private bool VerifIntegrite(int id)
        {
            this.req = $"SELECT idlocation FROM location WHERE idbien = {id}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            List<string> liste = new List<string>();
            MySqlDataReader reader = this.command.ExecuteReader();
            // boucle tant que la ligne lue contient quelque chose
            while (reader.Read())
            {
                liste.Add($"{reader["idlocation"]}");
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

        /// <summary>
        /// Gère le clic sur le bouton d'accès à la fiche du bien sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFicheBien_Click(object sender, EventArgs e)
        {
            // Si aucun bien n'est sélectionné
            if (lstBiens.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner un bien pour pouvoir afficher sa fiche.");
            }
            else
            {
                // Récupère le type et l'id
                string[] data = RechercheIdBienGroupe();
                // Crée la fenêtre de fiche du bien avec le type (bien ou groupe) et l'(id en paramètre
                FicheBien modifBiens = new FicheBien(data);
                modifBiens.ShowDialog();
            }
        }

        /// <summary>
        /// Gère la récupération de l'id du bien ou du groupe à partir de son nom
        /// </summary>
        /// <returns>Tableau contenant le type (bien ou groupe de biens) et son id</returns>
        private string[] RechercheIdBienGroupe()
        {
            string[] dataBien = { "bien", "", $"{lstBiens.SelectedItem}" };
            this.req = $"SELECT * FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader readerBien = this.command.ExecuteReader();
            try
            {
                readerBien.Read();
                dataBien[1] = readerBien["idbien"].ToString();
                readerBien.Close();
            }
            catch
            {
                readerBien.Close();
                dataBien[0] = "groupe";
                this.req = $"SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = \"{lstBiens.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader readerGrpe = this.command.ExecuteReader();
                readerGrpe.Read();
                dataBien[1] = readerGrpe["idgroupe"].ToString();
                readerGrpe.Close();
            }
            return dataBien;
        }

    }
}
