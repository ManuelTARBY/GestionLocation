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
    public partial class GroupesDeBiens : Form
    {
        // id du groupe, contenu de la requête
        private string req, type;
        private int idGrpe;
        private MySqlCommand command;
        
        public GroupesDeBiens()
        {
            InitializeComponent();
            AfficheDroite(false);
            RemplirListeGroupes();
        }

        /// <summary>
        /// Gère l'accès à la partie droite de la fenêtre
        /// </summary>
        /// <param name="val"></param>
        private void AfficheDroite(bool val)
        {
            txtNomGroupe.Enabled = val;
            btnValider.Enabled = val;
            if (val == false)
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
            this.req = $"SELECT * FROM grpedebiens ORDER BY nomdugroupe";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                lstGroupes.Items.Add(reader["nomdugroupe"]);
                finCurseur = !reader.Read();
            }
            reader.Close();
        }


        /// <summary>
        /// Rafraîchit la liste des biens qui composent le groupe sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstGroupes_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstGroupes.SelectedItem != null)
            {
                cbxCompoGroupe.Items.Clear();
                txtNomGroupe.Text = "";
                lstContenuGroupe.Items.Clear();
                this.req = $"SELECT nombien FROM bien WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = (SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = \'{lstGroupes.SelectedItem}\')) ORDER BY nombien";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                bool finCurseur = !reader.Read();
                while (!finCurseur)
                {
                    lstContenuGroupe.Items.Add(reader["nombien"]);
                    finCurseur = !reader.Read();
                }
                reader.Close();
            }
        }


        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Lance la procédure de création d'un groupe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreer_Click(object sender, EventArgs e)
        {
            AfficheDroite(true);
            cbxCompoGroupe.Items.Clear();
            this.type = "insert";
            this.req = $"SELECT MAX(idgroupe) AS idmax FROM grpedebiens";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            this.idGrpe = Int32.Parse(reader["idmax"].ToString()) + 1;
            reader.Close();
            RemplirCbxCompoGroupe();
        }


        /// <summary>
        /// Enregistre les modifications dans la table des groupes de biens et celle des lignes de groupes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!txtNomGroupe.Text.Equals("") && cbxCompoGroupe.CheckedItems.Count >= 2)
            {
                switch (this.type)
                {
                    case "update":
                        this.req = $"UPDATE grpedebiens SET nomdugroupe = \'{txtNomGroupe.Text}\' WHERE idgroupe = {this.idGrpe}";
                        ExecuteReqUID();
                        this.req = $"DELETE FROM lignegroupe WHERE idgroupe = {this.idGrpe}";
                        ExecuteReqUID();
                        break;
                    case "insert":
                        // Met à jour la table grpedebiens
                        this.req = $"INSERT INTO grpedebiens (idgroupe, nomdugroupe) VALUES ({this.idGrpe}, \'{txtNomGroupe.Text}\')";
                        ExecuteReqUID();
                        // Met à jour la table lignegroupe
                        this.req = $"";
                        break;
                    default:
                        break;
                }
                
                // Ajout des enregistrements de lignegroupe
                string[] lesBiensChecked = new string[cbxCompoGroupe.CheckedItems.Count];
                // Récupère les noms des biens sélectionnés
                for (int i = 0; i < cbxCompoGroupe.CheckedItems.Count; i++)
                {
                    lesBiensChecked[i] = cbxCompoGroupe.CheckedItems[i].ToString();
                }
                // Récupère les id de chacun des biens sélectionnés
                for (int j = 0; j < lesBiensChecked.Count(); j++)
                {
                    this.req = $"SELECT idbien FROM bien WHERE nombien = \'{lesBiensChecked[j]}\'";
                    this.command = new MySqlCommand(this.req, Global.Connexion);
                    MySqlDataReader reader = this.command.ExecuteReader();
                    reader.Read();
                    // Construit la requête pour ajouter l'enregistrement dans la table lignegroupe
                    this.req = $"INSERT INTO lignegroupe (idgroupe, idbien) VALUES ({this.idGrpe},{int.Parse(reader["idbien"].ToString())})";
                    reader.Close();
                    ExecuteReqUID();
                }
                AfficheDroite(false);
                lstContenuGroupe.Items.Clear();
                lstGroupes.Items.Clear();
                RemplirListeGroupes();
            }
            else
            {
                if (txtNomGroupe.Text.Equals(""))
                {
                    MessageBox.Show("Vous devez saisir un nom pour le groupe.");
                }
                else if (cbxCompoGroupe.CheckedItems.Count <= 1)
                {
                    MessageBox.Show($"Vous devez sélectionner au moins deux biens.");
                }
            }
        }


        /// <summary>
        /// Remplit la combobox de la liste des biens
        /// </summary>
        public void RemplirCbxCompoGroupe()
        {
            MySqlDataReader reader;
            bool finCurseur;
            // S'il s'agit d'un update, récupère les noms de tous les biens
            List<string> lesBiens = new List<string>();
            if (this.type.Equals("update"))
            {
                this.req = $"SELECT nombien FROM bien WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = {this.idGrpe})";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                reader = this.command.ExecuteReader();
                finCurseur = !reader.Read();
                while (!finCurseur)
                {
                    lesBiens.Add(reader["nombien"].ToString());
                    finCurseur = !reader.Read();
                }
                reader.Close();
            }
            this.req = $"SELECT idbien, nombien FROM bien ORDER BY nombien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            finCurseur = !reader.Read();
            while (!finCurseur)
            {
                cbxCompoGroupe.Items.Add(reader["nombien"]);
                if (lesBiens.Contains(reader["nombien"].ToString()))
                {
                    cbxCompoGroupe.SetItemChecked(cbxCompoGroupe.Items.IndexOf(reader["nombien"]), true);
                }
                finCurseur = !reader.Read();
            }
            reader.Close();
        }


        /// <summary>
        /// Supprime le groupe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstGroupes.SelectedItem != null)
            {
                // Demande confirmation de suppression du groupe
                DialogResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le groupe : {lstGroupes.SelectedItem} ?", "Confirmer suppression", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Récupère l'id du groupe sélectionné
                    RecupID();
                    this.req = $"DELETE FROM lignegroupe WHERE idgroupe = {this.idGrpe}";
                    ExecuteReqUID();
                    this.req = $"DELETE FROM grpedebiens WHERE idgroupe = {this.idGrpe}";
                    ExecuteReqUID();
                    lstContenuGroupe.Items.Clear();
                    lstGroupes.Items.Clear();
                    RemplirListeGroupes();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un groupe pour pouvoir le supprimer");
            }
        }


        /// <summary>
        /// Lance la procédure de modification d'un groupe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstGroupes.SelectedItem != null)
            {
                cbxCompoGroupe.Items.Clear();
                AfficheDroite(true);
                this.type = "update";
                RecupID();
                this.req = $"SELECT nomdugroupe FROM grpedebiens WHERE idgroupe = {this.idGrpe}";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                txtNomGroupe.Text = reader["nomdugroupe"].ToString();
                reader.Close();
                RemplirCbxCompoGroupe();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un groupe pour pouvoir le modifier.");
            }
        }


        /// <summary>
        /// Exécute une requête de type update, insert ou delete
        /// </summary>
        private void ExecuteReqUID()
        {
            // Exécute la requête
            this.command = new MySqlCommand(this.req, Global.Connexion);
            // préparation de la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }


        /// <summary>
        /// Récupère l'id du groupe sélectionné
        /// </summary>
        public void RecupID()
        {
            this.req = $"SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = \'{lstGroupes.SelectedItem}\'";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            this.idGrpe = Int32.Parse(reader["idgroupe"].ToString());
            reader.Close();
        }
    }
}
