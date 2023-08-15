using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Connexion : Form
    {
        // Stocke la chaine de connexion à la BDD
        private string chaineConnexion;
        // Compteur de tentatives de connexion
        private int cptEssai = 1;
        private MySqlCommand command;
        private string idUser, req;


        /// <summary>
        /// Constructeur de Connexion
        /// </summary>
        public Connexion()
        {
            InitializeComponent();
            CheckDir();
            lblCptEssai.Text = $"Essai : 1/{essaiMax}";
            this.AcceptButton = btnConnexion;
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
                    GenererChaineConnexion();
                    // Teste la connexion à la BDD
                    if (ConnexionSql())
                    {
                        RecupIdUser();
                        // Si l'id de l'utilisateur a été trouvé dans la table Utilisateur de la BDD
                        if (!this.idUser.Equals(""))
                        {
                            // Ouvre la fenêtre Accueil
                            Accueil accueil = new Accueil(this);
                            this.Visible = false;
                            accueil.ShowDialog();
                        }
                        // Sinon, ouvre la fenêtre de création d'utilisateur
                        else
                        {
                            MessageBox.Show("Vous devez créer un utilisateur.");
                            this.req = $"SELECT COUNT(iduser) FROM utilisateur";
                            this.command = new MySqlCommand(this.req, Global.Connexion);
                            // Création du tableau contenant tous les champs de la table Utilisateur + le type de requête à l'indice 0
                            MySqlDataReader reader = this.command.ExecuteReader();
                            reader.Read();
                            int result = int.Parse(reader.GetString(0)) + 1;
                            reader.Close();
                            string[] infos = { "INSERT INTO", "", txtId.Text, txtPwd.Text, "", "", "", "", "", "", "", "", "", "", "" };
                            infos[1] = result.ToString();
                            // Ouvre la fenêtre AjoutModifUtilisateurs
                            AjoutModifUtilisateurs fenUtilisateur = new AjoutModifUtilisateurs(infos, this);
                            fenUtilisateur.ShowDialog();
                        }
                    }
                    else
                    {
                        lblErreur.Text = "La connexion a échoué";
                        this.cptEssai++;
                        lblCptEssai.Text = $"Essai : {this.cptEssai}/{essaiMax}";
                    }
                }
                else
                {
                    MessageBox.Show("Nombre de tentatives maximum atteint.");
                    Application.Exit();
                }
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
            this.chaineConnexion = $"server=localhost;user id={txtId.Text};password={txtPwd.Text};Convert Zero Datetime=True;Convert Zero Datetime=True;Allow Zero Datetime=true;SslMode=none;database=gestionlocation";
        }


        /// <summary>
        /// Teste la connexion à la base de données
        /// </summary>
        /// <returns>True si la connexion la connexion a pu être faite, False dans le cas contraire</returns>
        private bool ConnexionSql()
        {
            try
            {
                Global.Connexion = new MySqlConnection(this.chaineConnexion);
                Global.Connexion.Open();
            }
            catch(MySqlException e)
            {
                MessageBox.Show(e.Message);
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


        /// <summary>
        /// Vérifie si les répertoires contenant les quittances et les signatures existent et les crée si besoin
        /// </summary>
        public void CheckDir()
        {
            // Crée le répertoire des quittances
            Directory.CreateDirectory(Environment.CurrentDirectory + "/Quittances");

            // Crée le répertoire des signatures
            Directory.CreateDirectory(Environment.CurrentDirectory + "/Signature");
        }
    }
}

