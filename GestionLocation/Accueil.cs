﻿using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Accueil : Form
    {
        
        private MySqlCommand command;
        private readonly Connexion fenConnexion;
        private readonly string idUser;
        private string req;
        
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
            this.command.Prepare();
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
            this.req = "SELECT nombien, CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', nomlocataire) AS 'locataire', "+
                "debutlocation, finlocation, CONCAT(SUBSTRING_INDEX(prenomcaution, ',', 1), ' ', nomcaution) AS 'caution' " +
                "FROM location JOIN locataire USING(idlocataire) JOIN bien USING(idbien) JOIN caution USING(idcaution)"+
                "WHERE locationarchivee = 0 ORDER BY nombien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstLocations.Items.Add($"{reader["nombien"]} || {reader["locataire"]} "+
                    $" || Du {reader.GetDateTime(2):d} au {reader.GetDateTime(3):d} || Caution : {reader["caution"]}");
                finCurseur = !reader.Read();
            }
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
            this.Visible = false;
            location.ShowDialog();
            this.Visible = true;
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
            this.Visible = false;
            bien.ShowDialog();
            this.Visible = true;
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
            this.Visible = false;
            locataire.ShowDialog();
            this.Visible = true;
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
            this.Visible = false;
            fenCaution.ShowDialog();
            this.Visible = true;
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
            this.Visible = false;
            fenCharges.ShowDialog();
            this.Visible = true;
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
            this.Visible = false;
            fenPaiements.ShowDialog();
            this.Visible = true;
        }


        /// <summary>
        /// Récupère l'id de l'utilisateur
        /// </summary>
        /// <returns>Id de l'utilisateur</returns>
        public string GetIdUser()
        {
            return this.idUser;
        }


        /// <summary>
        /// Gère le survol du bouton Utilisateur
        /// </summary>
        /// <param name="sender">Bouton survolé</param>
        /// <param name="e"></param>
        private void BtnUser_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }


        /// <summary>
        /// Gère la sortie de survol du bouton Utilisateur
        /// </summary>
        /// <param name="sender">Bouton concerné</param>
        /// <param name="e"></param>
        private void BtnUser_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }

        /// <summary>
        /// Gère le survol du bouton Groupes
        /// </summary>
        /// <param name="sender">Bouton survolé</param>
        /// <param name="e"></param>
        private void BtnGroupes_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }

        /// <summary>
        /// Gère la sortie de survol du bouton Groupes
        /// </summary>
        /// <param name="sender">Bouton concerné</param>
        /// <param name="e"></param>
        private void BtnGroupes_MouseLeave(object sender, EventArgs e)
        {
            SurvolSortie((Button)sender);
        }

        /// <summary>
        /// Ouvre la fenêtre AjoutModifUtilisateurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDatas_Click(object sender, EventArgs e)
        {
            Stats fenStats = new Stats();
            this.Visible = false;
            fenStats.ShowDialog();
            this.Visible = true;
        }


        /// <summary>
        /// Récupère dans un tableau les infos concernant l'utilisateur à partir de son ID
        /// </summary>
        /// <param name="idUser">ID de l'utilisateur</param>
        /// <returns>Tableau contenant les infos sur l'utilisateur</returns>
        public string[] RecupInfosUser(string idUser)
        {
            string[] infos = new string[14];
            infos[1] = idUser;
            string req = $"SELECT * FROM utilisateur WHERE iduser = {this.idUser}";
            this.command = new MySqlCommand(req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            for (int i = 1; i < infos.Length - 1; i++)
            {
                infos[i + 1] = reader.GetString(i);
            }
            if (!reader["signature"].ToString().Equals(""))
            {
                infos[13] = $"{Environment.CurrentDirectory}/ Signature /{ infos[3]} {infos[4]}.png";
            }
            reader.Close();
            return infos;
        }

        /// <summary>
        /// Ouvre la fenêtre de gestion des groupes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGroupes_Click(object sender, EventArgs e)
        {
            GroupesDeBiens fenGroupes = new GroupesDeBiens();
            this.Visible = false;
            fenGroupes.ShowDialog();
            this.Visible = true;
        }


        /// <summary>
        /// Ouvre la fenêtre AjoutModifUtilisateurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUser_Click_1(object sender, EventArgs e)
        {
            // Ouvre la fenêtre de l'utilisateur
            string[] infos = RecupInfosUser(this.idUser);
            infos[0] = "UPDATE utilisateur SET";
            AjoutModifUtilisateurs fenUser = new AjoutModifUtilisateurs(infos, this.fenConnexion);
            this.Visible = false;
            fenUser.ShowDialog();
            this.Visible = true;
        }


        /// <summary>
        /// Gère la fermeture de la fenêtre (ferme l'application et coupe la connexion)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accueil_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.Connexion.Close();
            Application.Exit();
        }
    }
}
