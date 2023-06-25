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
    public partial class ListeCharges : Form
    {

        private readonly MySqlConnection connexion;
        private readonly string[] leBien;
        private string req;
        private MySqlCommand command;
        private Dictionary<string, string> lesCharges;
        private readonly FicheBien fenFicheBien;
        
        /// <summary>
        /// Constructeur de ListeCharges
        /// </summary>
        /// <param name="connexion">Chaîne de connexion à la base de données</param>
        /// <param name="nombien">Nom du bien</param>
        //public ListeCharges(MySqlConnection connexion, string[] leBien)
        public ListeCharges(FicheBien fenFicheBien)
        {
            this.fenFicheBien = fenFicheBien;
            this.connexion = fenFicheBien.GetConnexion();
            this.leBien = fenFicheBien.GetLeBien();
            this.lesCharges = new Dictionary<string, string>();
            InitializeComponent();
            lblNomBien.Text = $"Liste des charges - {this.leBien[1].ToUpper()}";
            RecupListeCharges();
        }


        /// <summary>
        /// Récupère la liste des charges à partir de l'id du bien 
        /// </summary>
        public void RecupListeCharges()
        {
            // Vide la liste
            lstCharges.Items.Clear();
            this.lesCharges.Clear();
            this.req = $"SELECT idchargeannuelle, libelle, montantcharge, refFrequence FROM chargesannuelles WHERE idbien={this.leBien[0]} ORDER BY libelle";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();

            while (!finCurseur)
            {
                // Remplit la listbox
                lstCharges.Items.Add($"{reader["libelle"]} || Montant : {reader["montantcharge"]} € || Fréquence : {reader["refFrequence"]}");
                // Remplit le dictionnaire avec le contenu du listbox: idchargesannuelles
                lesCharges.Add(lstCharges.Items[lstCharges.Items.Count - 1].ToString(), reader.GetString(0));
               // lesCharges.Add($"{reader["libelle"]} || Montant : {reader["montantcharge"]} € || Fréquence : {reader["refFrequence"]}", reader.GetString(0));
                finCurseur = !reader.Read();
            }
            reader.Close();
        }

        /// <summary>
        /// Gère le clic sur le bouton Ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            AjoutModifChargeAnnuelle fenCharge = new AjoutModifChargeAnnuelle(this);
            fenCharge.ShowDialog();
        }


        /// <summary>
        /// Permet de récupérer la chaîne de connexion
        /// </summary>
        /// <returns>Chaîne de connexion</returns>
        public MySqlConnection GetConnexion()
        {
            return this.connexion;
        }


        /// <summary>
        /// Permet de récupérer le bien
        /// </summary>
        /// <returns>Le bien</returns>
        public string[] GetLeBien()
        {
            return this.leBien;
        }

        /// <summary>
        /// Gère le clic sur le bouton modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstCharges.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une charge pour pouvoir la modifier.");
            }
            else
            {
                AjoutModifChargeAnnuelle fenCharge = new AjoutModifChargeAnnuelle(this, lesCharges[lstCharges.SelectedItem.ToString()]);
                fenCharge.ShowDialog();
            }
        }

        /// <summary>
        /// Renvoie l'instance de FicheBien
        /// </summary>
        /// <returns>Instance de fichebien</returns>
        public FicheBien GetFenFicheBien()
        {
            return this.fenFicheBien;
        }

        /// <summary>
        /// Gère le demande de suppression d'une charge de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstCharges.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une charge dans la liste pour pouvoir la supprimer.");
            }
            else
            {
                // Demande confirmation de suppression du bien
                DialogResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la charge : {lstCharges.SelectedItem} ?", "Confirmer suppression", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.req = $"DELETE FROM chargesannuelles WHERE idchargeannuelle = \"{lesCharges[lstCharges.SelectedItem.ToString()]}\"";
                    ExecuteReqIUD();
                }
            }
            MajChargesDuBien();
            RecupListeCharges();
            this.fenFicheBien.RemplirChamps();
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
        /// Met à jour la table bien au niveau du montant total des charges annuelles
        /// </summary>
        public void MajChargesDuBien()
        {
            // Calcule la charge annuelle du bien
            float charges = 0;
            this.req = $"SELECT chargeannuelle FROM chargesannuelles WHERE idbien = {leBien[0]}";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                charges += float.Parse(reader["chargeannuelle"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();

            // Calcule la charge imputable au locataire du bien
            this.req += " AND imputable = True";
            float chImputables = 0;
            this.command = new MySqlCommand(this.req, this.connexion);
            reader = this.command.ExecuteReader();
            finCurseur = !reader.Read();
            while (!finCurseur)
            {
                chImputables += float.Parse(reader["chargeannuelle"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();

            // Met à jour la table bien
            this.req = $"UPDATE bien SET chargeannuelles = \'{Math.Round(charges, 2)}\', chargesimputables = \'{Math.Round(chImputables / 12, 2)}\' WHERE idbien = {leBien[0]}";
            // Exécute la requête
            ExecuteReqIUD();
        }
    }
}
