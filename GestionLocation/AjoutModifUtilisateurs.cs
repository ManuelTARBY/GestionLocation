﻿using MySql.Data.MySqlClient;
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
    public partial class AjoutModifUtilisateurs : Form
    {

        private string req, adresseSmtp;
        private int port;
        private MySqlCommand command;
        private readonly string[] infos;
        private readonly Connexion fenConnexion;

        // Constructeur de la fenêtreAjoutModifUtilisateur
        public AjoutModifUtilisateurs(string[] infos, Connexion fenConnexion)
        {
            InitializeComponent();
            this.infos = infos;
            this.fenConnexion = fenConnexion;
            lblID.Text = infos[0];
            txtPrenom.Text = infos[3];
            txtNom.Text = infos[4];
            txtAdresse.Text = infos[5];
            txtCp.Text = infos[6];
            txtVille.Text = infos[7];
            txtEmail.Text = infos[8];
            txtPwdEmail.Text = infos[9];
            txtServeurSMTP.Text = infos[10];
            txtPort.Text = infos[11];
        }


        /// <summary>
        /// Enregistre/Modifie l'utilisateur et ouvre la fenêtre Accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (VerifChamps() == true)
            {

                string messagerie = ChercherMessagerie();
                if (ChercherInfosClientMail(messagerie))
                {
                    if (this.infos[this.infos.Length - 1].Equals("INSERT INTO"))
                    {
                        ConstruitReqAjout();
                    }
                    else
                    {
                        ConstruitReqModif();
                    }
                    EnvoiReqCUD();
                    // Ouvre la fenêtre accueil
                    this.fenConnexion.SetIdUser(this.infos[0]);
                    Accueil fenAccueil = new Accueil(this.fenConnexion);
                    fenAccueil.Show();
                    this.Dispose();
                }
            }
        }


        /// <summary>
        /// Vérifie que tous les champs soient bien remplis
        /// </summary>
        /// <returns></returns>
        public bool VerifChamps()
        {
            if (txtPrenom.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ prénom svp.");
                txtPrenom.Focus();
                return false;
            }
            else if (txtNom.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ nom svp.");
                txtNom.Focus();
                return false;
            }
            else if (txtAdresse.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ adresse svp.");
                txtAdresse.Focus();
                return false;
            }
            else if (txtCp.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ code postal svp.");
                txtCp.Focus();
                return false;
            }
            else if (txtVille.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ ville svp.");
                txtVille.Focus();
                return false;
            }
            else if (txtEmail.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ email svp.");
                txtEmail.Focus();
                return false;
            }
            else if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Veuillez saisir une adresse email correcte svp.");
                txtEmail.Focus();
                return false;
            }
            else if (txtPwdEmail.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ mot de passe de la messagerie svp.");
                txtPwdEmail.Focus();
                return false;
            }
/*            else if (txtServeurSMTP.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ adresse serveur SMTP svp.");
                txtServeurSMTP.Focus();
                return false;
            }
            else if (txtPort.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir le champ port svp.");
                txtPort.Focus();
                return false;
            }*/
            else
            {
                /*try
                {
                    int port = int.Parse(txtPort.Text);*/
                return true;
/*                }
                catch
                {
                    MessageBox.Show("Veuillez saisir une valeur correcte pour le champ port svp.");
                    txtPort.Focus();
                    return false;
                }*/
            }
        }


        /// <summary>
        /// Récupère le type de messagerie
        /// </summary>
        public string ChercherMessagerie()
        {
            string email = txtEmail.Text;
            string[] emails = email.Split('@');
            emails = emails[1].Split('.');
            return emails[0];
        }


        /// <summary>
        /// Récupère les infos (port et adresse serveur smtp à partir de l'adresse mail)
        /// </summary>
        /// <param name="messagerie">Type de messagerie</param>
        public bool ChercherInfosClientMail(string messagerie)
        {
            bool trouve = true;
            this.port = 587;
            switch (messagerie)
            {
                case "orange":
                    this.adresseSmtp = "smtp.orange.fr";
                    break;
                case "aliceadsl":
                    this.adresseSmtp = "smtp.aliceadsl.fr";
                    break;
                case "aol":
                    this.adresseSmtp = "smtp.aol.com";
                    break;
                case "ionos":
                    this.adresseSmtp = "smtp.ionos.fr";
                    break;
                case "laposte":
                    this.adresseSmtp = "smtp.laposte.fr";
                    break;
                case "gmail":
                    this.adresseSmtp = "smtp.gmail.com";
                    break;
                case "free":
                    this.adresseSmtp = "smtp.free.fr";
                    break;
                case "sfr":
                    this.adresseSmtp = "smtp.sfr.fr";
                    break;
                case "live": case "outlook": case "hotmail":
                    this.adresseSmtp = "smtp.office365.com";
                    break;
                case "yahoo":
                    this.adresseSmtp = "smtp.mail.yahoo.com";
                    break;
                default:
                    MessageBox.Show("Type d'adresse mail non reconnu.\nVeuillez saisir une autre adresse mail.");
                    txtEmail.Focus();
                    trouve = false;
                    break;
            }
            return trouve;
        }


        /// <summary>
        /// Construit la requête d'ajout d'un enregistrement de Utilisateur
        /// </summary>
        public void ConstruitReqAjout()
        {
            this.req = $"{this.infos[this.infos.Length - 1]} utilisateur (iduser, login, pwd, prenomuser, nomuser, adresseuser, cpuser, villeuser, emailuser, " +
                $"pwdemail, adresseserveursmtp, port, clientid, clientsecret) " +
                $"VALUES ({lblID.Text}, \'{infos[1]}\', \'{infos[2]}\', \'{Capitalize(txtPrenom.Text)}\', \'{txtNom.Text.ToUpper()}\', " +
                $"\'{txtAdresse.Text}\', \'{txtCp.Text}\', \'{txtVille.Text.ToUpper()}\', \'{txtEmail.Text}\', \'{txtPwdEmail.Text}\', " +
                $"\'{this.adresseSmtp}\', \'{this.port}\', \'{""}\' , \'{""}\')";
        }


        /// <summary>
        /// Construit la requête de modification d'un enregistrement de Utilisateur
        /// </summary>
        public void ConstruitReqModif()
        {

        }


        /// <summary>
        /// Envoie la requête de type ajout ou modification
        /// </summary>
        public void EnvoiReqCUD()
        {
            this.command = new MySqlCommand(this.req, Global.Connexion);
            // préparation de la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }


        /// <summary>
        /// Met une majuscule sur la première lettre d'un mot ou d'une phrase
        /// </summary>
        /// <param name="s">Chaîne concernée par la mise en forme</param>
        /// <returns>Chaine de caractère avec une majuscule sur le premier caractère</returns>
        public string Capitalize(string s)
        {
            return s[0].ToString().ToUpper() + s.Substring(1).ToLower();
        }
    }
}
