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
    public partial class Connexion : Form
    {
        // Stocke la chaine de connexion à la BDD
        private string chaineConnexion;
        // Compteur de tentatives de connexion
        private int cptEssai = 0;
        private MySqlCommand command;
        private string idUser, req;

        public Connexion()
        {
            InitializeComponent();
            lblCptEssai.Text = $"Essai : 1/{essaiMax}";
        }


        /// <summary>
        /// Déclenche la tentative de connexion à la BDD au clic sur le bouton "Connexion"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            Console.WriteLine(this.cptEssai);
            lblErreur.Text = "";
            if (txtId.Text != "")
            {
                if (this.cptEssai < essaiMax)
                {
                    this.cptEssai++;
                    GenererChaineConnexion();
                    // Teste la connexion à la BDD
                    if (ConnexionSql())
                    {
                        RecupIdUser();
                        // Si l'id a été trouvé dans la table Utilisateur de la BDD
                        if (!this.idUser.Equals(""))
                        {
                            Accueil accueil = new Accueil(this);
                            accueil.Show();
                        }
                        // Sinon, ouvre la fenêtre de création d'utilisateur
                        else
                        {
                            MessageBox.Show("Vous devez créer un utilisateur.");
                            this.req = $"SELECT COUNT(iduser) FROM utilisateur";
                            this.command = new MySqlCommand(this.req, Global.Connexion);
                            // Création du tableau contenant tous les champs de la table Utilisateur + le type de requête à l'indice 14
                            string[] infos = { "", txtId.Text, txtPwd.Text, "", "", "", "", "", "", "", "", "", "", "", "INSERT INTO" };
                            MySqlDataReader reader = this.command.ExecuteReader();
                            reader.Read();
                            int result = int.Parse(reader.GetString(0)) + 1;
                            reader.Close();
                            infos[0] = result.ToString();
                            AjoutModifUtilisateurs fenUtilisateur = new AjoutModifUtilisateurs(infos, this);
                            fenUtilisateur.Show();
                        }
                        this.Visible = false;
                    }
                    else
                    {
                        lblErreur.Text = "La tentative de connexion a échoué";
                    }
                }
                else
                {
                    MessageBox.Show("Nombre de tentatives maximum atteint.");
                    Application.Exit();
                }
                lblCptEssai.Text = $"Essai : {this.cptEssai + 1}/{essaiMax}";
            }
            else
            {
                lblErreur.Text = "Veuillez entrez votre login";
            }
        }


        /// <summary>
        /// Récupère l'id de l'utilisateur
        /// </summary>
        public void RecupIdUser()
        {
            this.req = $"SELECT iduser FROM utilisateur WHERE login = \'{txtId.Text}\' AND pwd = \'{txtPwd.Text}\'";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            try
            {
                this.idUser = reader.GetString(0);
            }
            catch
            {
                this.idUser = "";
            }
            reader.Close();
        }


        /// <summary>
        /// Génère la chaîne de connexion à partir des éléments remplis dans les zones de saisie
        /// </summary>
        private void GenererChaineConnexion()
        {
            this.chaineConnexion = $"server={serveurbdd};user id={txtId.Text};password={txtPwd.Text};Convert Zero Datetime=True;Convert Zero Datetime=True;Allow Zero Datetime=true;database=gestionlocation";
        }


        /// <summary>
        /// Teste la connexion à la base de données
        /// </summary>
        /// <returns>True si la connexion la connexion a pu être faite, False dans le cas contraire</returns>
        private bool ConnexionSql()
        {
            Global.Connexion = new MySqlConnection(this.chaineConnexion);
            try
            {
                Global.Connexion.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gère l'appui sur la touche Entrée depuis la fenêtre Connexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connexion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnConnexion_Click(sender, e);
            }
        }

        /// <summary>
        /// Gère l'appui sur la touche Entrée depuis txtId
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnConnexion_Click(sender, e);
            }
        }

        /// <summary>
        /// Gère l'appui sur la touche Entrée depuis txtPwd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnConnexion_Click(sender, e);
            }
        }


        /// <summary>
        /// Permet d'accéder à la connexion
        /// </summary>
        /// <returns>Chaîne de connexion à la BDD</returns>
        public MySqlConnection GetConnexion()
        {
            return Global.Connexion;
        }


        /// <summary>
        /// Setter sur l'idUser
        /// </summary>
        /// <param name="idUser"></param>
        public void SetIdUser(string idUser)
        {
            this.idUser = idUser;
        }


        /// <summary>
        /// Permet de récupérer l'id de l'utilisateur
        /// </summary>
        /// <returns>Id de l'utilisateur</returns>
        public string GetIdUser()
        {
            return this.idUser;
        }
    }
}

