using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Locations : Form
    {

        private MySqlCommand command;
        private readonly Accueil fenAccueil;
        private string where, req;
        private readonly string idUser;
        private readonly Dictionary<string, int> lesId;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Locations(Accueil fenAccueil)
        {
            InitializeComponent();
            this.fenAccueil = fenAccueil;
            this.idUser = this.fenAccueil.GetIdUser();
            this.lesId = new Dictionary<string, int>();
            AfficherBiens();
            AfficherLocations();
        }


        /// <summary>
        /// Met à jour la liste des locations en fonction des critères sélectionnés par l'utilisateur
        /// </summary>
        public void AfficherLocations()
        {
            // Vide le champ liste et le dictionnaire contenant les id
            lstLocations.Items.Clear();
            lesId.Clear();
            // Construit la requête
            this.req = "SELECT nombien, CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', nomlocataire) AS `locataire`, " +
                "debutlocation AS `Début de location`, finlocation AS `Fin de location`, " +
                "CONCAT(SUBSTRING_INDEX(prenomcaution, ',', 1), ' ', nomcaution) AS 'caution', idlocation AS `id`" +
                "FROM location NATURAL JOIN locataire NATURAL JOIN bien NATURAL JOIN caution ";
            if (clbBiens.CheckedItems.Count > 0)
            {
                this.req += Where();
            }
            else
            {
                lstLocations.Items.Clear();
                return;
            }
            this.req += "ORDER BY nombien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                // Affiche la liste des locations
                string item = $"{reader["nombien"]} || {reader["locataire"]} || Du {reader.GetDateTime(2):d} au "+
                    $"{reader.GetDateTime(3):d} || Caution : {reader["caution"]}";
                lstLocations.Items.Add(item);
                lesId.Add(item, (int)(reader["id"]));
                finCurseur = !reader.Read();
            }
            reader.Close();
        }

        /// <summary>
        /// Gère l'affichage de la liste des biens non archivés dans la combo
        /// </summary>
        public void AfficherBiens()
        {
            clbBiens.Items.Clear();
            string req = "SELECT nombien FROM bien WHERE bienarchive = 0 ORDER BY nombien";
            this.command = new MySqlCommand(req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                clbBiens.Items.Add(reader["nombien"]);
                clbBiens.SetItemChecked(clbBiens.Items.IndexOf(reader["nombien"]), true);
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Gère la fermeture de l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermerAppli_Click(object sender, EventArgs e)
        {
            Global.Connexion.Close();
            Application.Exit();
        }

        /// <summary>
        /// Gère la construction de la ligne "WHERE" de la requête SQL
        /// </summary>
        /// <returns>ligne "WHERE" de la requête SQL</returns>
        private String Where()
        {
            this.where = "";
            CritereBien();
            CritereArchive();
            return this.where;
        }

        /// <summary>
        /// Gère le critère de bien sélectionné
        /// </summary>
        private void CritereBien()
        {
            string laListe = "";
            for (int i = 0; i < clbBiens.CheckedItems.Count -1; i ++)
            {
                laListe += $"\"{clbBiens.CheckedItems[i]}\", ";
            }
            laListe += $"\"{clbBiens.CheckedItems[clbBiens.CheckedItems.Count - 1]}\"";
            this.where += $"WHERE nombien IN ({laListe})";
        }

        /// <summary>
        /// Gère le critère de l'état d'archive sélectionné
        /// </summary>
        private void CritereArchive()
        {
            if (this.rbnArchive.Checked || this.rbnNonArchive.Checked)
            {
                PremiereCond();
                if (this.rbnArchive.Checked)
                {
                    // Global.LocArchiv = true;
                    this.where += $"locationarchivee = 1 ";
                }
                else if (this.rbnNonArchive.Checked)
                {
                    // Global.LocArchiv = false;
                    this.where += $"locationarchivee = 0 ";
                }
            }
        }

        /// <summary>
        /// Gère l'ajout du WHERE et du AND dans la requête
        /// </summary>
        public void PremiereCond()
        {
            if (this.where.Equals(""))
            {
                this.where += " WHERE ";
            }
            else
            {
                this.where += " AND ";
            }
        }

        /// <summary>
        /// Gère le lancement de la recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MajLocations_Click(object sender, EventArgs e)
        {
            if (clbBiens.CheckedItems != null)
            {
                AfficherLocations();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner au moins un bien.");
            }
        }

        /// <summary>
        /// Désélectionne tous les biens de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAucun_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbBiens.Items.Count; i++)
            {
                clbBiens.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// Sélectionne tous les biens de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTous_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbBiens.Items.Count; i++)
            {
                clbBiens.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// Gère le survol d'un bouton
        /// </summary>
        /// <param name="bouton">Bouton survolé</param>
        private void SurvolEntree(Button bouton)
        {
            bouton.Size = new Size(bouton.Width + 6, bouton.Height + 6);
            bouton.Location = new Point(bouton.Location.X - 3, bouton.Location.Y - 3);
            bouton.BackColor = Color.FromArgb(219, 0, 0);
            bouton.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            bouton.ForeColor = Color.White;
        }

        /// <summary>
        /// Gère la sortie de survol d'un bouton
        /// </summary>
        /// <param name="bouton">Bouton dont on quitte le survol</param>
        private void SurvolSortie(Button bouton)
        {
            bouton.Size = new Size(bouton.Width - 6, bouton.Height - 6);
            bouton.Location = new Point(bouton.Location.X + 3, bouton.Location.Y + 3);
            bouton.BackColor = Color.Transparent;
            bouton.ForeColor = Color.Black;
            bouton.Font = new Font("Microsoft Sans Serif", 8F);
        }

        /// <summary>
        /// Gère le survol du bouton fermer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermerAppli_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }

        /// <summary>
        /// Gère la sortie du survol du bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermerAppli_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }

        /// <summary>
        /// Gère l'archivage/désarchivage d'une location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArchiver_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une location dans la liste.");
            }
            else
            {
                // Récupère l'id de la location sélectionnée
                int idLocation = lesId[lstLocations.SelectedItem.ToString()];
                // Requête pour récupérer la valeur de locationarchivee de la location sélectionnée
                this.command = new MySqlCommand($"SELECT locationarchivee FROM location WHERE idlocation = {idLocation}", Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.req = $"UPDATE location SET locationarchivee = {!(bool)reader["locationarchivee"]} WHERE idlocation = \"{idLocation}\"";
                reader.Close();
                // Exécute la requête de modification
                ExecuteReqIUD();
                // Met à jour la liste des locations (fenêtre Locations et fenêtre Accueil)
                MajAffichageLoc();
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
        /// Ouvre la fenêtre de création d'une location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            AjoutModifLocations ajoutLocation = new AjoutModifLocations(this, "INSERT INTO");
            ajoutLocation.ShowDialog();
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'une location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem != null)
            {
                // Récupère l'id de la location sélectionnée
                int id = lesId[lstLocations.SelectedItem.ToString()];
                // Crée puis ouvre la fenêtre d'ajout/modif location
                AjoutModifLocations modifLocataire = new AjoutModifLocations(this, "UPDATE", id);
                modifLocataire.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez saisir une location dans la liste pour pouvoir la modifier.");
            }
        }

        /// <summary>
        /// Gère la suppression d'une location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem != null)
            {
                // Demande confirmation de suppression du bien
                DialogResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la location : {lstLocations.SelectedItem} et tous les paiements qui y sont reliés ?", "Confirmer suppression", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Récupère l'id de la location sélectionnée
                    int id = lesId[lstLocations.SelectedItem.ToString()];
                    // Supprime les paiements attachés à la location
                    this.req = $"DELETE FROM paiement WHERE idlocation = \"{id}\"";
                    ExecuteReqIUD();
                    // Supprime la location
                    this.req = $"DELETE FROM location WHERE idlocation = \"{id}\"";
                    ExecuteReqIUD();
                    MajAffichageLoc();
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir une location dans la liste pour pouvoir la supprimer.");
            }
        }


        /// <summary>
        /// Met à jour l'affichage de la liste des locations
        /// </summary>
        public void MajAffichageLoc()
        {
            AfficherLocations();
            this.fenAccueil.AfficherLocations();
        }


        /// <summary>
        /// Ouvre la fenêtre des enregistrements de la table Paiement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFenPaiements_Click(object sender, EventArgs e)
        {
            int id = 0;
            // Si une location est sélectionnée
            if (lstLocations.SelectedItem != null)
            {
                // Récupère l'id de la location sélectionnée
                id = lesId[lstLocations.SelectedItem.ToString()];
            }
            Paiements fenPaiement = new Paiements(this, id);
            fenPaiement.ShowDialog();
        }


        /// <summary>
        /// Retourne la fenêtre de type Accueil passée en paramètre du constructeur
        /// </summary>
        /// <returns>Instance de la classe accueil ayant ouvert l'instance actuelle de Locations</returns>
        public Accueil GetFenAccueil()
        {
            return this.fenAccueil;
        }


        /// <summary>
        /// Retourne l'id de l'utilisateur
        /// </summary>
        /// <returns>Id de l'utilisateur</returns>
        public string GetIdUser()
        {
            return this.idUser;
        }
    }
}
