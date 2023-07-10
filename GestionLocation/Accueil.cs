using MailKit.Security;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Accueil : Form
    {
        
        private MySqlCommand command;
        private readonly Connexion fenConnexion;
        private readonly string idUser;
        
        /// <summary>
        /// Constructeur de la fenêtre Accueil
        /// </summary>
        /// <param name="fenConnexion">Instance de la fenêtre Connexion</param>
        public Accueil(Connexion fenConnexion)
        {
            InitializeComponent();
            this.fenConnexion = fenConnexion;
            this.idUser = this.fenConnexion.GetIdUser();
            Global.Connexion = this.fenConnexion.GetConnexion();
            RecupInfoUser();
            AfficherLocations();
        }


        /// <summary>
        /// Met à jour les données sur la session email de l'utilisateur
        /// </summary>
        private void RecupInfoUser()
        {
            string req = $"SELECT * FROM utilisateur WHERE iduser={this.idUser}";
            this.command = new MySqlCommand(req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            Global.User = $"{reader.GetString(3)} {reader.GetString(4)}";
            Global.EmailUser = $"{reader.GetString(8)}";
            Global.PwdUser = $"{reader.GetString(9)}";
            Global.ServeurSmtp = $"{reader.GetString(10)}";
            Global.PortEmail = (int)reader["port"];
            reader.Close();
        }


        /// <summary>
        /// Met à jour la liste des locations en fonction des critères sélectionnés par l'utilisateur
        /// </summary>
        public void AfficherLocations()
        {
            lstLocations.Items.Clear();
            StringBuilder req = new StringBuilder();
            req.AppendLine("SELECT nombien AS `Bien`, nomcompletlocataire AS `Locataire`, debutlocation AS `Début de location`, finlocation AS `Fin de location`, nomcompletcaution AS `Caution`");
            req.AppendLine("FROM location JOIN locataire USING(idlocataire) JOIN bien USING(idbien) JOIN caution USING(idcaution)");
            req.AppendLine("WHERE locationarchivee = 0");
            req.AppendLine("ORDER BY nombien");
            this.command = new MySqlCommand(req.ToString(), Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstLocations.Items.Add($"{reader["Bien"]} || {reader["Locataire"]} || Du {reader.GetDateTime(2):d} au {reader.GetDateTime(3):d} || Caution : {reader["Caution"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Crée et affiche la fenêtre de gestion des locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLocations_Click(object sender, EventArgs e)
        {
            Locations location = new Locations(this);
            location.ShowDialog();
        }

        
        /// <summary>
        /// Gère la fermeture de l'application et de la connexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermerAppli_Click(object sender, EventArgs e)
        {
            Global.Connexion.Close();
            Application.Exit();
        }

        /// <summary>
        /// Gère l'ouverture de la fenêtre de gestion des biens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBiens_Click(object sender, EventArgs e)
        {
            Biens bien = new Biens();
            bien.ShowDialog();
        }

        /// <summary>
        /// Gère le survol du bouton Biens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBiens_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button) sender);
        }

        /// <summary>
        /// Gère la sortie de survol du bouton Biens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBiens_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button) sender);
        }

        /// <summary>
        /// Gère l'évènement de survol d'un bouton
        /// </summary>
        /// <param name="bouton">bouton survolé</param>
        private void SurvolEntree(Button bouton)
        {
            bouton.Size = new Size(bouton.Width + 6, bouton.Height + 6);
            bouton.Location = new Point(bouton.Location.X - 3, bouton.Location.Y - 3);
            bouton.BackColor = Color.FromArgb(79, 242, 120);
        }

        /// <summary>
        /// Gère l'évènenement de sortie de survol d'un bouton
        /// </summary>
        /// <param name="bouton"></param>
        private void SurvolSortie(Button bouton)
        {
            bouton.Size = new Size(bouton.Width - 6, bouton.Height - 6);
            bouton.Location = new Point(bouton.Location.X + 3, bouton.Location.Y + 3);
            bouton.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Gère le survol du bouton Locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLocations_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }

        /// <summary>
        /// Gère la sortie de survol du bouton Locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLocations_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }

        /// <summary>
        /// Gère le survol du bouton Locataires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLocataires_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }

        /// <summary>
        /// Gère la sortie de survol du bouton Locataires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLocataires_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }

        /// <summary>
        /// Gère le clic sur le bouton Locataires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLocataires_Click(object sender, EventArgs e)
        {
            Locataires locataire = new Locataires();
            locataire.ShowDialog();
        }

        /// <summary>
        /// Gère le survol du bouton Cautions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCautions_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }

        /// <summary>
        /// Gère la sortie de survol du bouton Cautions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCautions_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }

        /// <summary>
        /// Gère le clic sur le bouton Cautions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCautions_Click(object sender, EventArgs e)
        {
            Cautions fenCaution = new Cautions();
            fenCaution.ShowDialog();
        }

        /// <summary>
        /// Gère le survol du bouton Charges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCharges_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }

        /// <summary>
        /// Gère la sortie de survol du bouton Charges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCharges_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }

        /// <summary>
        /// Gère l'ouverture de la fenêtre de la liste des charges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCharges_Click(object sender, EventArgs e)
        {
            ListeCharges fenCharges = new ListeCharges(this);
            fenCharges.ShowDialog();
        }


        /// <summary>
        /// Renvoie l'instance de la connexion Sql
        /// </summary>
        /// <returns>Connexion</returns>
        public MySqlConnection GetConnexion()
        {
            return Global.Connexion;
        }


        /// <summary>
        /// Gère le survol du bouton Paiements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPaiements_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }


        /// <summary>
        /// Gère la sortie de survol du bouton Paiements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPaiements_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }


        /// <summary>
        /// Ouvre la fenêtre des Paiements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPaiements_Click(object sender, EventArgs e)
        {
            Paiements fenPaiements = new Paiements(this);
            fenPaiements.ShowDialog();
        }


        /// <summary>
        /// Récupère l'id de l'utilisateur
        /// </summary>
        /// <returns>Id de l'utilisateur</returns>
        public string GetIdUser()
        {
            return this.idUser;
        }
    }
}
