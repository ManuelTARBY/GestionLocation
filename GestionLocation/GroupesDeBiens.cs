using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class GroupesDeBiens : Form
    {
        private int idGrpe;
        private bool estNouveau;

        public GroupesDeBiens()
        {
            InitializeComponent();
            AfficheDroite(false);
            RemplirListeGroupes();
        }

        /// <summary>
        /// Gère l'accès à la partie droite de la fenêtre
        /// </summary>
        private void AfficheDroite(bool val)
        {
            txtNomGroupe.Enabled = val;
            btnValider.Enabled = val;
            if (!val)
            {
                txtNomGroupe.Text = "";
                cbxCompoGroupe.Items.Clear();
            }
        }

        /// <summary>
        /// Remplit la liste des groupes
        /// </summary>
        public void RemplirListeGroupes()
        {
            lstGroupes.Items.Clear();

            const string req = "SELECT nomdugroupe FROM grpedebiens ORDER BY nomdugroupe";
            using var command = new MySqlCommand(req, Global.Connexion);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lstGroupes.Items.Add(reader.GetString(0));
            }
        }

        /// <summary>
        /// Rafraîchit la liste des biens qui composent le groupe sélectionné.
        /// Rattachée à SelectedIndexChanged (et non plus MouseClick) : avec MouseClick,
        /// changer la sélection au clavier (flèches) ne rafraîchissait pas lstContenuGroupe,
        /// laissant l'affichage désynchronisé de la sélection réelle.
        /// À REBRANCHER dans le Designer : remplacer l'abonnement à l'évènement MouseClick
        /// de lstGroupes par un abonnement à SelectedIndexChanged pointant sur cette méthode.
        /// </summary>
        private void LstGroupes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGroupes.SelectedItem == null)
            {
                return;
            }

            cbxCompoGroupe.Items.Clear();
            txtNomGroupe.Text = "";
            lstContenuGroupe.Items.Clear();

            const string req =
                "SELECT nombien FROM bien WHERE idbien IN (" +
                "SELECT idbien FROM lignegroupe WHERE idgroupe = (" +
                "SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = @nom)) " +
                "ORDER BY nombien";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@nom", lstGroupes.SelectedItem.ToString());

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lstContenuGroupe.Items.Add(reader.GetString(0));
            }
        }

        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Lance la procédure de création d'un groupe
        /// </summary>
        private void BtnCreer_Click(object sender, EventArgs e)
        {
            AfficheDroite(true);
            cbxCompoGroupe.Items.Clear();
            this.estNouveau = true;
            this.idGrpe = ProchainIdGroupe();
            RemplirCbxCompoGroupe();
        }

        /// <summary>
        /// Calcule le prochain id de groupe disponible.
        /// IFNULL(...) évite un plantage sur la création du tout premier groupe
        /// (MAX() renvoie NULL sur une table vide).
        /// </summary>
        private int ProchainIdGroupe()
        {
            const string req = "SELECT IFNULL(MAX(idgroupe), 0) + 1 FROM grpedebiens";
            using var command = new MySqlCommand(req, Global.Connexion);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// Enregistre les modifications dans la table des groupes de biens et celle des lignes de groupes.
        /// L'ensemble des opérations est fait dans une transaction : si un bien sélectionné a été
        /// supprimé entre-temps, tout est annulé plutôt que de laisser le groupe dans un état à moitié
        /// enregistré.
        /// </summary>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (txtNomGroupe.Text.Equals(""))
            {
                MessageBox.Show("Vous devez saisir un nom pour le groupe.");
                return;
            }

            if (cbxCompoGroupe.CheckedItems.Count < 2)
            {
                MessageBox.Show("Vous devez sélectionner au moins deux biens.");
                return;
            }

            List<string> biensSelectionnes = cbxCompoGroupe.CheckedItems
                .Cast<object>()
                .Select(o => o.ToString())
                .ToList();

            using var transaction = Global.Connexion.BeginTransaction();
            try
            {
                if (this.estNouveau)
                {
                    InsererGroupe(transaction);
                }
                else
                {
                    MettreAJourGroupe(transaction);
                }

                EnregistrerCompositionGroupe(biensSelectionnes, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Erreur lors de l'enregistrement du groupe : " + ex.Message);
                return;
            }

            AfficheDroite(false);
            lstContenuGroupe.Items.Clear();
            lstGroupes.Items.Clear();
            RemplirListeGroupes();
        }

        private void InsererGroupe(MySqlTransaction transaction)
        {
            const string req = "INSERT INTO grpedebiens (idgroupe, nomdugroupe) VALUES (@id, @nom)";
            using var command = new MySqlCommand(req, Global.Connexion, transaction);
            command.Parameters.AddWithValue("@id", this.idGrpe);
            command.Parameters.AddWithValue("@nom", txtNomGroupe.Text);
            command.ExecuteNonQuery();
        }

        private void MettreAJourGroupe(MySqlTransaction transaction)
        {
            const string reqMaj = "UPDATE grpedebiens SET nomdugroupe = @nom WHERE idgroupe = @id";
            using (var command = new MySqlCommand(reqMaj, Global.Connexion, transaction))
            {
                command.Parameters.AddWithValue("@nom", txtNomGroupe.Text);
                command.Parameters.AddWithValue("@id", this.idGrpe);
                command.ExecuteNonQuery();
            }

            const string reqSuppr = "DELETE FROM lignegroupe WHERE idgroupe = @id";
            using (var command = new MySqlCommand(reqSuppr, Global.Connexion, transaction))
            {
                command.Parameters.AddWithValue("@id", this.idGrpe);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Enregistre la composition du groupe (une ligne par bien). Lève une exception si un
        /// bien sélectionné n'existe plus en base, ce qui déclenche le rollback de la transaction
        /// appelante plutôt que de laisser lignegroupe à moitié rempli.
        /// </summary>
        private void EnregistrerCompositionGroupe(List<string> biensSelectionnes, MySqlTransaction transaction)
        {
            foreach (string nomBien in biensSelectionnes)
            {
                const string reqId = "SELECT idbien FROM bien WHERE nombien = @nom";
                int idBien;
                using (var command = new MySqlCommand(reqId, Global.Connexion, transaction))
                {
                    command.Parameters.AddWithValue("@nom", nomBien);
                    using var reader = command.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException($"Le bien \"{nomBien}\" n'existe plus.");
                    }
                    idBien = reader.GetInt32(0);
                }

                const string reqInsert = "INSERT INTO lignegroupe (idgroupe, idbien) VALUES (@idgroupe, @idbien)";
                using var cmdInsert = new MySqlCommand(reqInsert, Global.Connexion, transaction);
                cmdInsert.Parameters.AddWithValue("@idgroupe", this.idGrpe);
                cmdInsert.Parameters.AddWithValue("@idbien", idBien);
                cmdInsert.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Remplit la combobox de la liste des biens, en cochant ceux qui composent déjà le
        /// groupe en cas de modification
        /// </summary>
        public void RemplirCbxCompoGroupe()
        {
            List<string> biensDuGroupe = new List<string>();

            if (!this.estNouveau)
            {
                const string reqComposition =
                    "SELECT nombien FROM bien WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = @id)";
                using var command = new MySqlCommand(reqComposition, Global.Connexion);
                command.Parameters.AddWithValue("@id", this.idGrpe);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    biensDuGroupe.Add(reader.GetString(0));
                }
            }

            const string reqTousBiens = "SELECT nombien FROM bien ORDER BY nombien";
            using (var command = new MySqlCommand(reqTousBiens, Global.Connexion))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string nomBien = reader.GetString(0);
                    cbxCompoGroupe.Items.Add(nomBien);
                    if (biensDuGroupe.Contains(nomBien))
                    {
                        cbxCompoGroupe.SetItemChecked(cbxCompoGroupe.Items.Count - 1, true);
                    }
                }
            }
        }

        /// <summary>
        /// Supprime le groupe sélectionné (et sa composition), de façon atomique
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstGroupes.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un groupe pour pouvoir le supprimer");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer le groupe : {lstGroupes.SelectedItem} ?",
                "Confirmer suppression", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            int? id = RecupID(lstGroupes.SelectedItem.ToString());
            if (id == null)
            {
                MessageBox.Show("Ce groupe n'existe plus.");
                return;
            }
            this.idGrpe = id.Value;

            using var transaction = Global.Connexion.BeginTransaction();
            try
            {
                const string reqSupprLignes = "DELETE FROM lignegroupe WHERE idgroupe = @id";
                using (var command = new MySqlCommand(reqSupprLignes, Global.Connexion, transaction))
                {
                    command.Parameters.AddWithValue("@id", this.idGrpe);
                    command.ExecuteNonQuery();
                }

                const string reqSupprGroupe = "DELETE FROM grpedebiens WHERE idgroupe = @id";
                using (var command = new MySqlCommand(reqSupprGroupe, Global.Connexion, transaction))
                {
                    command.Parameters.AddWithValue("@id", this.idGrpe);
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Erreur lors de la suppression du groupe : " + ex.Message);
                return;
            }

            lstContenuGroupe.Items.Clear();
            lstGroupes.Items.Clear();
            RemplirListeGroupes();
        }

        /// <summary>
        /// Lance la procédure de modification d'un groupe
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstGroupes.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un groupe pour pouvoir le modifier.");
                return;
            }

            cbxCompoGroupe.Items.Clear();
            AfficheDroite(true);
            this.estNouveau = false;

            int? id = RecupID(lstGroupes.SelectedItem.ToString());
            if (id == null)
            {
                MessageBox.Show("Ce groupe n'existe plus.");
                AfficheDroite(false);
                return;
            }
            this.idGrpe = id.Value;

            const string req = "SELECT nomdugroupe FROM grpedebiens WHERE idgroupe = @id";
            using (var command = new MySqlCommand(req, Global.Connexion))
            {
                command.Parameters.AddWithValue("@id", this.idGrpe);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtNomGroupe.Text = reader.GetString(0);
                }
            }

            RemplirCbxCompoGroupe();
        }

        /// <summary>
        /// Récupère l'id du groupe à partir de son nom
        /// </summary>
        /// <returns>L'id du groupe, ou null s'il n'existe pas</returns>
        public int? RecupID(string nomGroupe)
        {
            const string req = "SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = @nom";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@nom", nomGroupe);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetInt32(0);
            }
            return null;
        }
    }
}