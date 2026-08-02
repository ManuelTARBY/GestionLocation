using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Biens : Form
    {
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
            lesBiens.AddRange(ListeBiens());
            lesBiens.AddRange(ListeGroupesDeBiens());
            lesBiens.Sort();

            foreach (string bien in lesBiens)
            {
                lstBiens.Items.Add(bien);
            }
        }

        /// <summary>
        /// Récupère la liste des noms de biens (archivés ou non selon la case cochée)
        /// </summary>
        private List<string> ListeBiens()
        {
            var resultat = new List<string>();

            const string req = "SELECT nombien FROM bien WHERE bienarchive = @archive ORDER BY nombien";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@archive", rdbBienArchive.Checked);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                resultat.Add(reader.GetString(0));
            }

            return resultat;
        }

        /// <summary>
        /// Récupère la liste des noms de groupes de biens
        /// </summary>
        private List<string> ListeGroupesDeBiens()
        {
            var resultat = new List<string>();

            const string req = "SELECT nomdugroupe FROM grpedebiens ORDER BY nomdugroupe";
            using var command = new MySqlCommand(req, Global.Connexion);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                resultat.Add(reader.GetString(0));
            }

            return resultat;
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout/modification de bien pour modification
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez saisir un bien dans la liste pour pouvoir le modifier.");
                return;
            }

            int? id = RechercheIdBien(lstBiens.SelectedItem.ToString());
            if (id == null)
            {
                MessageBox.Show("Vous avez sélectionné un groupe, veuillez sélectionner un bien.");
                return;
            }

            AjoutModifBiens modifBiens = new AjoutModifBiens(this, estNouveau: false, id.Value);
            modifBiens.ShowDialog();
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout/modification de bien pour création
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            AjoutModifBiens modifBiens = new AjoutModifBiens(this, estNouveau: true);
            modifBiens.ShowDialog();
        }

        /// <summary>
        /// Archive ou désarchive le bien sélectionné
        /// </summary>
        private void BtnArchiverDesarchiver_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez saisir un bien dans la liste.");
                return;
            }

            string nomBien = lstBiens.SelectedItem.ToString();

            const string reqLecture = "SELECT bienarchive FROM bien WHERE nombien = @nom";
            bool estArchive;
            using (var command = new MySqlCommand(reqLecture, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nom", nomBien);
                using var reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("Ce bien n'existe plus.");
                    return;
                }
                estArchive = reader.GetBoolean("bienarchive");
            }

            const string reqMaj = "UPDATE bien SET bienarchive = @nouvelEtat WHERE nombien = @nom";
            using (var command = new MySqlCommand(reqMaj, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nouvelEtat", !estArchive);
                command.Parameters.AddWithValue("@nom", nomBien);
                command.ExecuteNonQuery();
            }

            RemplirLstBiens();
        }

        /// <summary>
        /// Met à jour la liste des biens
        /// </summary>
        private void BtnRechercher_Click(object sender, EventArgs e)
        {
            RemplirLstBiens();
        }

        /// <summary>
        /// Gère l'appui sur le bouton supprimer
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez saisir un bien dans la liste pour pouvoir le supprimer.");
                return;
            }

            string nomBien = lstBiens.SelectedItem.ToString();

            DialogResult result = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer le bien {nomBien} ?",
                "Confirmer suppression", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            int? id = RechercheIdBien(nomBien);
            if (id == null)
            {
                MessageBox.Show("Vous avez sélectionné un groupe, impossible de le supprimer depuis cet écran.");
                return;
            }

            if (!VerifIntegrite(id.Value))
            {
                MessageBox.Show("Ce bien est relié à une ou plusieurs locations. Pour pouvoir le supprimer, vous devez d'abord supprimer" +
                    " ces locations.");
                return;
            }

            const string req = "DELETE FROM bien WHERE nombien = @nom";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@nom", nomBien);
            command.ExecuteNonQuery();

            RemplirLstBiens();
        }

        /// <summary>
        /// Recherche l'id d'un bien à partir de son nom
        /// </summary>
        /// <returns>L'id du bien, ou null si le nom ne correspond à aucun bien</returns>
        private int? RechercheIdBien(string nomBien)
        {
            const string req = "SELECT idbien FROM bien WHERE nombien = @nom";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@nom", nomBien);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return reader.GetInt32(0);
        }

        /// <summary>
        /// Vérifie si un bien n'est pas lié à une ou plusieurs locations
        /// </summary>
        /// <returns>True s'il n'y a pas de conflit d'intégrité, False dans le cas contraire</returns>
        private bool VerifIntegrite(int id)
        {
            const string req = "SELECT COUNT(*) FROM location WHERE idbien = @id";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", id);

            long nbLocations = Convert.ToInt64(command.ExecuteScalar());
            return nbLocations == 0;
        }

        /// <summary>
        /// Gère le clic sur le bouton d'accès à la fiche du bien sélectionné
        /// </summary>
        private void BtnFicheBien_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner un bien pour pouvoir afficher sa fiche.");
                return;
            }

            string[] data = RechercheIdBienGroupe(lstBiens.SelectedItem.ToString());
            FicheBien modifBiens = new FicheBien(data);
            modifBiens.ShowDialog();
        }

        /// <summary>
        /// Récupère le type (bien ou groupe) et l'id à partir du nom sélectionné.
        /// Utilise une vraie vérification (reader.Read()) plutôt qu'un try/catch
        /// pour distinguer "bien" de "groupe" : l'ancien code s'appuyait sur
        /// l'exception levée par un accès à une ligne inexistante, ce qui capturait
        /// aussi de vraies erreurs de connexion et les confondait avec ce cas normal.
        /// </summary>
        /// <returns>Tableau : ["bien" ou "groupe", id, nom]</returns>
        private string[] RechercheIdBienGroupe(string nomSelectionne)
        {
            const string reqBien = "SELECT idbien FROM bien WHERE nombien = @nom";
            using (var command = new MySqlCommand(reqBien, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nom", nomSelectionne);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new[] { "bien", reader.GetInt32(0).ToString(), nomSelectionne };
                }
            }

            const string reqGroupe = "SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = @nom";
            using (var command = new MySqlCommand(reqGroupe, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nom", nomSelectionne);
                using var reader = command.ExecuteReader();
                reader.Read();
                return new[] { "groupe", reader.GetInt32(0).ToString(), nomSelectionne };
            }
        }
    }
}