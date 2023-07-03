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
    public partial class Paiements : Form
    {
        
        private readonly MySqlConnection connexion;
        private MySqlCommand command;
        private string req;
        private int idLocation;
        private readonly Dictionary<string, string> lesPaiements;
        private readonly Dictionary<string, int> lesId;

        /// <summary>
        /// Constructeur de la fenêtre Paiements
        /// </summary>
        /// <param name="connexion">Connexion à la base de données</param>
        public Paiements(MySqlConnection connexion, int idLocation = 0)
        {
            InitializeComponent();
            this.connexion = connexion;
            this.idLocation = idLocation;
            this.lesPaiements = new Dictionary<string, string>();
            this.lesId = new Dictionary<string, int>();
            AfficherLocations(false);
            RemplirListePaiements();
            SelectionnerLocation();
        }


        /// <summary>
        /// Liste tous les paiements
        /// </summary>
        public void RemplirListePaiements()
        {
            // Détermination de la requête
            if (this.idLocation != 0)
            {
                this.req = $"SELECT * FROM paiement WHERE idlocation = {this.idLocation}";
            }
            else
            {
                if (lstLocations.SelectedItem == null)
                {
                    this.req = $"SELECT * FROM paiement WHERE loyerregle = False";
                }
                else
                {
                    this.idLocation = this.lesId[lstLocations.SelectedItem.ToString()];
                    this.req = $"SELECT * FROM paiement WHERE loyerregle = False AND idlocation = {this.idLocation}";
                }
            }
            this.req += " ORDER BY periodefacturee";
            // Affiche les enregistrement de la table Paiement et récupère les idpaiement et idlocation dans un dictionnaire
            EnvoiRequeteSelect();
        }


        /// <summary>
        /// Récupère les id des locations
        /// </summary>
        /// <param name="etat">Etat d'archive des locations à trouver</param>
        /// <returns>Liste des id des locations qui sont à cet état</returns>
/*        public void RecupLocationArchive(bool etat)
        {
            this.req = $"SELECT idlocation FROM location WHERE locationarchivee={etat}";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            List<string> lesId = new List<string>();
            while (!finCurseur)
            {
                lesId.Add(reader["idlocation"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();
            this.lesIdP = new string[lesId.Count()];
            for (int i = 0; i < lesId.Count(); i++)
            {
                this.lesIdP[i] = lesId[i];
            }
        }*/


        /// <summary>
        /// Lance la requête, affiche les enregistrements de paiements et enregistre les id
        /// </summary>
        public void EnvoiRequeteSelect()
        {
            this.lesPaiements.Clear();
            lstPaiements.Items.Clear();
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                string ligne = $"Location n°{reader.GetString(1)} || Période : {reader.GetDateTime(4):MMMM yyyy} || " +
                    $"Montant dû : {reader.GetString(5)} || Montant payé : {reader.GetString(3)} || Restant dû : {reader.GetString(6)}";
                lstPaiements.Items.Add(ligne);
                this.lesPaiements.Add(ligne, reader["idpaiement"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();
        }


        /// <summary>
        /// Met à jour la liste des locations en fonction des critères sélectionnés par l'utilisateur
        /// </summary>
        public void AfficherLocations(bool etat)
        {
            // Vide le champ liste, paiements et le dictionnaire contenant les id
            lstLocations.Items.Clear();
            lstPaiements.Items.Clear();
            lesId.Clear();
            // Construit la requête
            StringBuilder req = new StringBuilder();
            req.AppendLine("SELECT nombien AS `Bien`, nomcompletlocataire AS `Locataire`, debutlocation AS `Début de location`, finlocation AS `Fin de location`, nomcompletcaution AS `Caution`, idlocation AS `id`");
            req.AppendLine("FROM location JOIN locataire USING(idlocataire) JOIN bien USING(idbien) JOIN caution USING(idcaution)");
            req.AppendLine($"WHERE locationarchivee = {etat} ORDER BY nombien");
            this.command = new MySqlCommand(req.ToString(), this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                string item = $"{reader["Bien"]} || {reader["Locataire"]} || Du {reader.GetDateTime(2):d} au {reader.GetDateTime(3):d} || Caution : {reader["Caution"]}";
                lstLocations.Items.Add(item);
                lesId.Add(item, (int)(reader["id"]));
                finCurseur = !reader.Read();
            }
            reader.Close();
            // Paramètre le texte sur le message du bouton afficher locations archivees/non archivées
            if (etat)
            {
                btnFiltreArchive.Text = "Afficher les locations non archivées";
            }
            else
            {
                btnFiltreArchive.Text = "Afficher les locations archivées";
            }
        }


        /// <summary>
        /// Sélectionne la location concernée par les paiements affichés
        /// </summary>
        public void SelectionnerLocation()
        {
            foreach (var paire in this.lesId)
            {
                if (paire.Value == this.idLocation)
                {
                    lstLocations.SelectedIndex = lstLocations.Items.IndexOf(paire.Key);
                }
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
        /// Gère l'ouverture de la fenêtre de modification d'un enregistrement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaisirPaiement_Click(object sender, EventArgs e)
        {
            if (lstPaiements.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un paiement dans la liste pour pouvoir le modifier");
            }
            else
            {
                ModifPaiements fenModifPaiement = new ModifPaiements(this, this.lesPaiements[lstPaiements.SelectedItem.ToString()]);
                fenModifPaiement.Show();
            }
        }


        /// <summary>
        /// Permet de récupérer la connexion
        /// </summary>
        /// <returns>Connexion</returns>
        public MySqlConnection GetConnexion()
        {
            return this.connexion;
        }


        /// <summary>
        /// Gère la sélection d'une location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstLocations_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstLocations.SelectedItem != null)
            {
                // Récupère le nouvel id de location
                this.idLocation = this.lesId[lstLocations.SelectedItem.ToString()];
                // Met à jour la liste des paiements
                RemplirListePaiements();
            }
        }


        /// <summary>
        /// Affiche les locations archivées ou non archivées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFiltreArchive_Click(object sender, EventArgs e)
        {
            if (btnFiltreArchive.Text.Equals("Afficher les locations archivées"))
            {
                AfficherLocations(true);
            }
            else
            {
                AfficherLocations(false);
            }
        }


        /// <summary>
        /// Met à jour la liste des paiements pour n'afficher que ceux qui ne sont pas complets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNonRegle_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem != null)
            {
                this.req = $"SELECT * FROM paiement WHERE loyerregle = False AND idlocation = {this.lesId[lstLocations.SelectedItem.ToString()]} ORDER BY periodefacturee";
            }
            else
            {
                this.idLocation = 0;
                this.req = $"SELECT * FROM paiement WHERE loyerregle = False ORDER BY periodefacturee";
            }
            EnvoiRequeteSelect();
        }
    }
}
