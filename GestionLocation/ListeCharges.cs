using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class ListeCharges : Form
    {

        private readonly string[] leBien = new string[2];
        private string req;
        private MySqlCommand command;
        private readonly Dictionary<string, string> lesCharges, lesBiens, chargeBien;
        private readonly FicheBien fenFicheBien;
        

        /// <summary>
        /// Constructeur de ListeCharges
        /// </summary>
        /// <param name="fenetre">Instance de la fenêtre ayant appelé le constructeur</param>
        public ListeCharges(Form fenetre)
        {
            InitializeComponent();
            // Si le paramètre contient la fenêtre de type FicheBien
            if (typeof(FicheBien).IsInstanceOfType(fenetre))
            {
                this.fenFicheBien = fenetre as FicheBien;
                this.leBien = this.fenFicheBien.GetLeBien();
            }
            this.Text = "Liste des charges";
            this.lesCharges = new Dictionary<string, string>();
            this.lesBiens = new Dictionary<string, string>();
            this.chargeBien = new Dictionary<string, string>();
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
            if (this.leBien[1] != null) { 
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
            this.chargeBien.Clear();
            this.req = $"SELECT idchargeannuelle, idbien, libelle, montantcharge, refFrequence FROM chargesannuelles ";
            if (this.fenFicheBien != null || lstBiens.SelectedItem != null)
            {
                this.req += $"WHERE idbien={this.leBien[0]} ";
            }
            this.req += "ORDER BY libelle";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            string ligneCharge;
            while (reader.Read())
            {
                string bien = lesBiens.FirstOrDefault(x => x.Value == reader["idbien"].ToString()).Key;
                // Remplit la listbox
                ligneCharge = $"{bien} || {reader["libelle"]} || Montant : {reader["montantcharge"]} € || Fréquence : {reader["refFrequence"]}";
                lstCharges.Items.Add(ligneCharge);
                // Remplit le dictionnaire avec le contenu du listbox: idchargesannuelles
                lesCharges.Add(ligneCharge, reader.GetString(0));
                chargeBien.Add(ligneCharge, reader.GetString(1));
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
            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner le bien concerné par la charge.");
            }
            else
            {
                this.leBien[0] = lesBiens[lstBiens.SelectedItem.ToString()];
                this.leBien[1] = lstBiens.SelectedItem.ToString();
                AjoutModifChargeAnnuelle fenCharge = new AjoutModifChargeAnnuelle(this);
                fenCharge.ShowDialog();
            }
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
                RecupDonneesBien();
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
                    MajChargesDuBien();
                    RecupListeCharges();
                    if (this.fenFicheBien != null)
                    {
                        this.fenFicheBien.RemplirChamps();
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
        /// Met à jour le champ charges annuelles du bien
        /// </summary>
        public void MajChargesDuBien()
        {
            // Calcule la charge annuelle du bien
            float charges = 0;
            this.req = $"SELECT chargeannuelle FROM chargesannuelles WHERE idbien = {chargeBien[lstCharges.SelectedItem.ToString()]}";
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
                lesBiens.Add(reader.GetString(1), reader.GetString(0));
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
                this.leBien[0] = lesBiens[lstBiens.SelectedItem.ToString()];
                RecupListeCharges();
                AfficheTitre();
            }
        }

        /// <summary>
        /// Retrouve l'id d'un bien à partir de son nom
        /// </summary>
        public void RecupDonneesBien()
        {
            this.leBien[0] = chargeBien[lstCharges.SelectedItem.ToString()];
            this.req = $"SELECT * FROM bien WHERE idbien = {this.leBien[0]}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            this.leBien[1] = reader.GetString(1);
            reader.Close();
        }
    }
}
