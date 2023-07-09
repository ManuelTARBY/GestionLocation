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

        private readonly string[] leBien = new string[2];
        private string req;
        private MySqlCommand command;
        private readonly Accueil fenAccueil;
        private readonly Dictionary<string, string> lesCharges;
        private readonly FicheBien fenFicheBien;
        private readonly Dictionary<string, string> lesBiens;
        

        /// <summary>
        /// Constructeur de ListeCharges
        /// </summary>
        /// <param name="fenetre">Instance de la fenêtre ayant appelé le constructeur</param>
        public ListeCharges(Form fenetre)
        {
            // Si le paramètre contient la fenêtre de type FicheBien
            if (typeof(FicheBien).IsInstanceOfType(fenetre))
            {
                this.fenFicheBien = fenetre as FicheBien;
                this.leBien = this.fenFicheBien.GetLeBien();
            }
            // Si la fenêtre a été ouverte depuis la fenêtre Accueil (sans bien sélectionné)
            else
            {
                this.fenAccueil = fenetre as Accueil;
            }
            this.lesCharges = new Dictionary<string, string>();
            this.lesBiens = new Dictionary<string, string>();
            InitializeComponent();
            RemplirListeBiens();
            RecupListeCharges();
            AfficheTitre();
        }

        /// <summary>
        /// Gère le titre à afficher
        /// </summary>
        /// <returns></returns>
        public void AfficheTitre()
        {
            string titre = "Liste des charges";
            if (/*this.fenFicheBien != null || */this.leBien[1] != null) { 
                titre += $" - {this.leBien[1].ToUpper()}";
            }
            lblNomBien.Text = titre;
        }

        /// <summary>
        /// Récupère la liste des charges à partir de l'id du bien 
        /// </summary>
        public void RecupListeCharges()
        {
            // Vide la liste
            lstCharges.Items.Clear();
            this.lesCharges.Clear();
            this.req = $"SELECT idchargeannuelle, libelle, montantcharge, refFrequence FROM chargesannuelles ";
            if (this.fenFicheBien != null || lstBiens.SelectedItem != null)
            {
                this.req += $"WHERE idbien={this.leBien[0]} ";
            }
            this.req += "ORDER BY libelle";
            this.command = new MySqlCommand(this.req, Global.Connexion);
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
            return Global.Connexion;
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
            this.command = new MySqlCommand(this.req, Global.Connexion);
            // préparation de la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }


        /// <summary>
        /// Met à jour le champ charges annuelles du bien
        /// </summary>
        public void MajChargesDuBien()
        {
            // Calcule la charge annuelle du bien
            float charges = 0;
            this.req = $"SELECT chargeannuelle FROM chargesannuelles WHERE idbien = {leBien[0]}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
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
            this.command = new MySqlCommand(this.req, Global.Connexion);
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


        /// <summary>
        /// Gère la fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Remplit la liste des biens
        /// </summary>
        private void RemplirListeBiens()
        {
            this.req = "SELECT idbien, nombien FROM bien ORDER BY nombien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                lesBiens.Add(reader.GetString(0), reader.GetString(1));
                lstBiens.Items.Add(reader["nombien"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();

            // Si la fenêtre a été ouverte depuis la fiche d'un bien
            if (fenFicheBien != null)
            {
                // Positionne le focus sur le bien en question
                int index = lstBiens.FindString(leBien[1]);
                lstBiens.SetSelected(index, true);
            }
        }


        /// <summary>
        /// Gère le filtrage des charges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFiltrer_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un bien dans la liste pour pouvoir afficher ses charges.");
            }
            else
            {
                this.leBien[1] = lstBiens.SelectedItem.ToString();
                RetrouverID();
                RecupListeCharges();
                AfficheTitre();
            }
        }

        /// <summary>
        /// Retrouve l'id d'un bien à partir de son nom
        /// </summary>
        public void RetrouverID()
        {
            // Parcourt le dictionnaire des biens
            foreach (var cle in lesBiens)
            {
                if (cle.Value.Equals(this.leBien[1]))
                {
                    this.leBien[0] = cle.Key;
                }
            }
        }
    }
}
