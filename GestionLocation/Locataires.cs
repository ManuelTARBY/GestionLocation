using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Locataires : Form
    {
        /// <summary>
        /// Classe interne permettant d'embarquer l'ID et le nom dans la ListBox
        /// </summary>
        private class LocataireItem
        {
            public int Id { get; set; }
            public string NomComplet { get; set; }

            public override string ToString()
            {
                return NomComplet;
            }
        }

        /// <summary>
        /// Constructeur de Locataires
        /// </summary>
        public Locataires()
        {
            InitializeComponent();
            this.Text = "Locataires";
            RemplirLstLocataires();
        }

        /// <summary>
        /// Gère le remplissage de la liste des locataires
        /// </summary>
        public void RemplirLstLocataires()
        {
            lstLocataires.Items.Clear();

            const string req = @"SELECT idlocataire, nomcompletlocataire 
                                FROM locataire 
                                WHERE locatairearchive = @archive 
                                ORDER BY nomlocataire";

            using (var command = new MySqlCommand(req, Global.Connexion))
            {
                command.Parameters.AddWithValue("@archive", rdbLocataireArchive.Checked ? 1 : 0);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lstLocataires.Items.Add(new LocataireItem
                        {
                            Id = reader.GetInt32("idlocataire"),
                            NomComplet = reader["nomcompletlocataire"].ToString()
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Met à jour la liste des locataires
        /// </summary>
        private void BtnRechercher_Click(object sender, EventArgs e)
        {
            RemplirLstLocataires();
        }

        /// <summary>
        /// Change le statut d'archive d'un locataire
        /// </summary>
        private void BtnArchiver_Click(object sender, EventArgs e)
        {
            if (lstLocataires.SelectedItem is LocataireItem locataire)
            {
                int nouvelEtatArchive = rdbLocataireArchive.Checked ? 0 : 1;

                const string req = "UPDATE locataire SET locatairearchive = @archive WHERE idlocataire = @id";

                using (var command = new MySqlCommand(req, Global.Connexion))
                {
                    command.Parameters.AddWithValue("@archive", nouvelEtatArchive);
                    command.Parameters.AddWithValue("@id", locataire.Id);
                    command.ExecuteNonQuery();
                }

                RemplirLstLocataires();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un locataire dans la liste pour pouvoir l'archiver ou le désarchiver.",
                                "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Gère la suppression d'un locataire
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstLocataires.SelectedItem is LocataireItem locataire)
            {
                DialogResult result = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer le locataire : {locataire.NomComplet} ?",
                    "Confirmer suppression",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (VerifIntegrite(locataire.Id))
                    {
                        const string req = "DELETE FROM locataire WHERE idlocataire = @id";

                        using (var command = new MySqlCommand(req, Global.Connexion))
                        {
                            command.Parameters.AddWithValue("@id", locataire.Id);
                            command.ExecuteNonQuery();
                        }

                        RemplirLstLocataires();
                    }
                    else
                    {
                        MessageBox.Show("Ce locataire est relié à une ou plusieurs locations. Pour pouvoir le supprimer, vous devez d'abord " +
                                        "supprimer les locations auxquelles il est rattaché.",
                                        "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un locataire dans la liste pour pouvoir le supprimer.",
                                "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout/modification de locataire pour création
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            using (var modifLocataire = new AjoutModifLocataires(this, "INSERT INTO"))
            {
                modifLocataire.ShowDialog();
            }
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout/modification de locataire pour modification
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstLocataires.SelectedItem is LocataireItem locataire)
            {
                using (var modifLocataire = new AjoutModifLocataires(this, "UPDATE", locataire.Id))
                {
                    modifLocataire.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un locataire dans la liste pour pouvoir le modifier.",
                                "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Vérifie si un locataire n'est pas lié à une ou plusieurs locations
        /// </summary>
        /// <param name="id">ID du locataire</param>
        /// <returns>True s'il n'y a pas de conflit d'intégrité, False dans le cas contraire</returns>
        private bool VerifIntegrite(int id)
        {
            const string req = "SELECT COUNT(*) FROM location WHERE idlocataire = @id";

            using (var command = new MySqlCommand(req, Global.Connexion))
            {
                command.Parameters.AddWithValue("@id", id);
                long count = Convert.ToInt64(command.ExecuteScalar());
                return count == 0;
            }
        }
    }
}