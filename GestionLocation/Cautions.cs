using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Cautions : Form
    {
        /// <summary>
        /// Classe interne pour stocker à la fois l'ID et le Nom dans la ListBox
        /// </summary>
        private class CautionItem
        {
            public int Id { get; set; }
            public string NomComplet { get; set; }

            public override string ToString()
            {
                return NomComplet;
            }
        }

        /// <summary>
        /// Constructeur Cautions
        /// </summary>
        public Cautions()
        {
            InitializeComponent();
            this.Text = "Cautions";
            RemplirLstCautions();
        }

        /// <summary>
        /// Gère le remplissage de la liste des cautions
        /// </summary>
        public void RemplirLstCautions()
        {
            lstCautions.Items.Clear();

            const string req = @"SELECT idcaution, nomcompletcaution 
                                FROM caution 
                                WHERE cautionarchivee = @archive 
                                ORDER BY nomcaution";

            using (var command = new MySqlCommand(req, Global.Connexion))
            {
                command.Parameters.AddWithValue("@archive", rdbCautionArchive.Checked ? 1 : 0);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lstCautions.Items.Add(new CautionItem
                        {
                            Id = reader.GetInt32("idcaution"),
                            NomComplet = reader["nomcompletcaution"].ToString()
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Gère le rafraîchissement de la recherche des cautions
        /// </summary>
        private void BtnRechercher_Click(object sender, EventArgs e)
        {
            RemplirLstCautions();
        }

        /// <summary>
        /// Inverse le statut d'archive de la caution sélectionnée
        /// </summary>
        private void BtnArchiver_Click(object sender, EventArgs e)
        {
            if (lstCautions.SelectedItem is CautionItem caution)
            {
                // Si la vue actuelle est "Archivés", la bascule passe à 0 (Non-archivé), et vice-versa
                int nouvelEtatArchive = rdbCautionArchive.Checked ? 0 : 1;

                const string req = "UPDATE caution SET cautionarchivee = @archive WHERE idcaution = @id";

                using (var command = new MySqlCommand(req, Global.Connexion))
                {
                    command.Parameters.AddWithValue("@archive", nouvelEtatArchive);
                    command.Parameters.AddWithValue("@id", caution.Id);
                    command.ExecuteNonQuery();
                }

                RemplirLstCautions();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une caution dans la liste pour pouvoir l'archiver ou la désarchiver.",
                                "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Gère la suppression de la caution sélectionnée
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstCautions.SelectedItem is CautionItem caution)
            {
                DialogResult result = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer la caution : {caution.NomComplet} ?",
                    "Confirmer suppression",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (VerifIntegrite(caution.Id))
                    {
                        const string req = "DELETE FROM caution WHERE idcaution = @id";
                        using (var command = new MySqlCommand(req, Global.Connexion))
                        {
                            command.Parameters.AddWithValue("@id", caution.Id);
                            command.ExecuteNonQuery();
                        }

                        RemplirLstCautions();
                    }
                    else
                    {
                        MessageBox.Show("Cette caution est reliée à une ou plusieurs locations. Vous ne pouvez pas la supprimer.",
                                        "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une caution dans la liste pour pouvoir la supprimer.",
                                "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre AjoutModifCautions pour réaliser un ajout
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            using (var modifCaution = new AjoutModifCautions(this, "INSERT INTO"))
            {
                modifCaution.ShowDialog();
            }
        }

        /// <summary>
        /// Ouvre la fenêtre AjoutModifCautions pour réaliser une modification
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstCautions.SelectedItem is CautionItem caution)
            {
                using (var modifCaution = new AjoutModifCautions(this, "UPDATE", caution.Id))
                {
                    modifCaution.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une caution dans la liste pour pouvoir la modifier.",
                                "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Vérifie si une caution n'est pas liée à une ou plusieurs locations
        /// </summary>
        /// <param name="id">ID de la caution</param>
        /// <returns>True s'il n'y a pas de conflit d'intégrité, False dans le cas contraire</returns>
        private bool VerifIntegrite(int id)
        {
            const string req = "SELECT COUNT(*) FROM location WHERE idcaution = @id";

            using (var command = new MySqlCommand(req, Global.Connexion))
            {
                command.Parameters.AddWithValue("@id", id);
                long count = Convert.ToInt64(command.ExecuteScalar());
                return count == 0;
            }
        }
    }
}