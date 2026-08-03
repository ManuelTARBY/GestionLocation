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
        /// <summary>
        /// Classe interne pour encapsuler l'ID et le texte d'affichage de la location dans la ListBox
        /// </summary>
        private class LocationItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

        private readonly Accueil fenAccueil;
        private readonly string idUser;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Locations(Accueil fenAccueil)
        {
            InitializeComponent();
            this.Text = "Locations";
            this.fenAccueil = fenAccueil;
            this.idUser = this.fenAccueil.GetIdUser();

            AfficherBiens();
            AfficherLocations();
        }

        /// <summary>
        /// Met à jour la liste des locations en fonction des critères sélectionnés
        /// </summary>
        public void AfficherLocations()
        {
            lstLocations.Items.Clear();

            // Si aucun bien n'est coché, inutile d'exécuter une requête
            if (clbBiens.CheckedItems.Count == 0)
            {
                return;
            }

            var sqlBuilder = new StringBuilder(@"
                SELECT 
                    nombien, 
                    CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', nomlocataire) AS `locataire`, 
                    debutlocation, 
                    finlocation, 
                    CONCAT(SUBSTRING_INDEX(prenomcaution, ',', 1), ' ', nomcaution) AS `caution`, 
                    idlocation AS `id`
                FROM location 
                NATURAL JOIN locataire 
                NATURAL JOIN bien 
                NATURAL JOIN caution 
                WHERE 1=1 ");

            using (var command = new MySqlCommand())
            {
                command.Connection = Global.Connexion;

                // Construction dynamique sécurisée de la clause IN avec des paramètres
                var bienParams = new List<string>();
                for (int i = 0; i < clbBiens.CheckedItems.Count; i++)
                {
                    string paramName = $"@bien{i}";
                    bienParams.Add(paramName);
                    command.Parameters.AddWithValue(paramName, clbBiens.CheckedItems[i].ToString());
                }
                sqlBuilder.Append($" AND nombien IN ({string.Join(", ", bienParams)})");

                // Filtre sur le statut d'archivage
                if (rbnArchive.Checked)
                {
                    sqlBuilder.Append(" AND locationarchivee = 1");
                }
                else if (rbnNonArchive.Checked)
                {
                    sqlBuilder.Append(" AND locationarchivee = 0");
                }

                sqlBuilder.Append(" ORDER BY nombien");
                command.CommandText = sqlBuilder.ToString();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string display = $"{reader["nombien"]} || {reader["locataire"]} || Du {reader.GetDateTime("debutlocation"):d} au {reader.GetDateTime("finlocation"):d} || Caution : {reader["caution"]}";

                        lstLocations.Items.Add(new LocationItem
                        {
                            Id = reader.GetInt32("id"),
                            DisplayText = display
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Gère l'affichage de la liste des biens non archivés
        /// </summary>
        public void AfficherBiens()
        {
            clbBiens.Items.Clear();
            const string req = "SELECT nombien FROM bien WHERE bienarchive = 0 ORDER BY nombien";

            using (var command = new MySqlCommand(req, Global.Connexion))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string nomBien = reader["nombien"].ToString();
                    int index = clbBiens.Items.Add(nomBien);
                    clbBiens.SetItemChecked(index, true);
                }
            }
        }

        /// <summary>
        /// Gère l'archivage/désarchivage d'une location
        /// </summary>
        private void BtnArchiver_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem is LocationItem location)
            {
                const string req = "UPDATE location SET locationarchivee = NOT locationarchivee WHERE idlocation = @id";

                using (var command = new MySqlCommand(req, Global.Connexion))
                {
                    command.Parameters.AddWithValue("@id", location.Id);
                    command.ExecuteNonQuery();
                }

                MajAffichageLoc();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une location dans la liste.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Gère la suppression d'une location et de ses paiements associés (sous transaction)
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem is LocationItem location)
            {
                DialogResult result = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer la location : {location.DisplayText} et tous les paiements qui y sont reliés ?",
                    "Confirmer suppression",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    const string deletePaiements = "DELETE FROM paiement WHERE idlocation = @id";
                    const string deleteLocation = "DELETE FROM location WHERE idlocation = @id";

                    using (var transaction = Global.Connexion.BeginTransaction())
                    {
                        try
                        {
                            // Suppression des paiements rattachés
                            using (var cmdPaiements = new MySqlCommand(deletePaiements, Global.Connexion, transaction))
                            {
                                cmdPaiements.Parameters.AddWithValue("@id", location.Id);
                                cmdPaiements.ExecuteNonQuery();
                            }

                            // Suppression de la location
                            using (var cmdLocation = new MySqlCommand(deleteLocation, Global.Connexion, transaction))
                            {
                                cmdLocation.Parameters.AddWithValue("@id", location.Id);
                                cmdLocation.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            MessageBox.Show("Une erreur est survenue lors de la suppression.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    MajAffichageLoc();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une location dans la liste pour pouvoir la supprimer.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Gère le lancement de la recherche
        /// </summary>
        private void MajLocations_Click(object sender, EventArgs e)
        {
            if (clbBiens.CheckedItems.Count > 0)
            {
                AfficherLocations();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner au moins un bien.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Désélectionne tous les biens
        /// </summary>
        private void BtnAucun_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbBiens.Items.Count; i++)
            {
                clbBiens.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// Sélectionne tous les biens
        /// </summary>
        private void BtnTous_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbBiens.Items.Count; i++)
            {
                clbBiens.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de création d'une location
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            using (var ajoutLocation = new AjoutModifLocations(this, "INSERT INTO"))
            {
                ajoutLocation.ShowDialog();
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'une location
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem is LocationItem location)
            {
                using (var modifLocation = new AjoutModifLocations(this, "UPDATE", location.Id))
                {
                    modifLocation.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une location dans la liste pour pouvoir la modifier.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre des enregistrements de la table Paiement
        /// </summary>
        private void BtnFenPaiements_Click(object sender, EventArgs e)
        {
            int id = (lstLocations.SelectedItem is LocationItem location) ? location.Id : 0;

            using (var fenPaiement = new Paiements(this, id))
            {
                fenPaiement.ShowDialog();
            }
        }

        /// <summary>
        /// Gère la fermeture de l'application
        /// </summary>
        private void BtnFermerAppli_Click(object sender, EventArgs e)
        {
            Global.Connexion.Close();
            Application.Exit();
        }

        private void SurvolEntree(Button bouton)
        {
            bouton.Size = new Size(bouton.Width + 6, bouton.Height + 6);
            bouton.Location = new Point(bouton.Location.X - 3, bouton.Location.Y - 3);
            bouton.BackColor = Color.FromArgb(219, 0, 0);
            bouton.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            bouton.ForeColor = Color.White;
        }

        private void SurvolSortie(Button bouton)
        {
            bouton.Size = new Size(bouton.Width - 6, bouton.Height - 6);
            bouton.Location = new Point(bouton.Location.X + 3, bouton.Location.Y + 3);
            bouton.BackColor = Color.Transparent;
            bouton.ForeColor = Color.Black;
            bouton.Font = new Font("Microsoft Sans Serif", 8F);
        }

        private void BtnFermerAppli_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }

        private void BtnFermerAppli_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }

        public void MajAffichageLoc()
        {
            AfficherLocations();
            this.fenAccueil.AfficherLocations();
        }

        public Accueil GetFenAccueil() => this.fenAccueil;

        public string GetIdUser() => this.idUser;
    }
}