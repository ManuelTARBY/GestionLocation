using GestionLocation.DTO;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Connexion : Form
    {
        // Chaîne de connexion technique à la BDD (compte de service, PAS lié à l'utilisateur applicatif)
        // À terme, à externaliser dans App.config / appsettings.json plutôt qu'en dur dans le code
        public string ChaineConnexionTechnique =
            "server=localhost;user id=manu;" +
            "Convert Zero Datetime=True;Allow Zero Datetime=true;SslMode=none;database=gestionlocationtest";

        // Compteur de tentatives de connexion
        private int cptEssai = 1;
        private string idUser;

        /// <summary>
        /// Constructeur de Connexion
        /// </summary>
        public Connexion()
        {
            InitializeComponent();
            CheckDir();
            lblCptEssai.Text = $"Essai : {this.cptEssai}/{essaiMax}";
            this.AcceptButton = btnConnexion;
        }

        /// <summary>
        /// Déclenche la tentative de connexion à la BDD au clic sur le bouton "Connexion"
        /// </summary>
        private void BtnConnexion_Click(object sender, EventArgs e)
        {

            lblErreur.Text = "";

            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                lblErreur.Text = "Veuillez entrer votre login";
                return;
            }

            if (this.cptEssai > essaiMax)
            {
                return;
            }

            // Étape 1 : s'assurer que la connexion technique à la BDD est ouverte
            if (!AssurerConnexionTechnique())
            {
                lblErreur.Text = "Impossible de joindre la base de données.";
                return;
            }

            // Étape 2 : authentification applicative (login/mot de passe hashé)
            if (AuthentifierUtilisateur(txtId.Text, txtPwd.Text))
            {
                Accueil accueil = new Accueil(this);
                this.Visible = false;
                accueil.ShowDialog();
            }
            else
            {
                lblErreur.Text = "La connexion a échoué";
                this.cptEssai++;

                if (this.cptEssai > essaiMax)
                {
                    MessageBox.Show("Nombre de tentatives maximum atteint.");
                    Application.Exit();
                }
                else
                {
                    lblCptEssai.Text = $"Essai : {this.cptEssai}/{essaiMax}";
                }
            }
        }

        /// <summary>
        /// Ouvre la connexion technique à la BDD si elle n'est pas déjà ouverte.
        /// Cette connexion est indépendante des identifiants applicatifs saisis par l'utilisateur.
        /// </summary>
        private bool AssurerConnexionTechnique()
        {
            if (Global.Connexion != null && Global.Connexion.State == System.Data.ConnectionState.Open)
            {
                return true;
            }

            try
            {
                Global.Connexion = new MySqlConnection(ChaineConnexionTechnique);
                Global.Connexion.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // TODO: logger ex quelque part (fichier de log, etc.)
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// Vérifie le couple login / mot de passe contre la table utilisateur.
        /// Le mot de passe est comparé via BCrypt (jamais en clair en SQL).
        /// </summary>
        /// <returns>True si l'utilisateur existe et le mot de passe correspond</returns>
        private bool AuthentifierUtilisateur(string login, string motDePasse)
        {
            const string req = "SELECT iduser, pwd FROM utilisateur WHERE login = @login";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@login", login);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                // Login inconnu : on propose la création d'utilisateur
                reader.Close();
                ProposerCreationUtilisateur(login, motDePasse);
                return false;
            }

            string idUserTrouve = reader.GetString(0);
            string hashStocke = reader.GetString(1);

            if (BCrypt.Net.BCrypt.Verify(motDePasse, hashStocke))
            {
                this.idUser = idUserTrouve;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Propose la création d'un nouvel utilisateur lorsque le login saisi n'existe pas.
        /// </summary>
        private void ProposerCreationUtilisateur(string login, string motDePasseClair)
        {
            MessageBox.Show("Vous devez créer un utilisateur.");

            // IFNULL(MAX(...), 0) + 1 plutôt que COUNT(...) + 1 : évite une collision d'id
            // si un utilisateur a été supprimé entre-temps.
            const string reqNextId = "SELECT IFNULL(MAX(iduser), 0) + 1 FROM utilisateur";
            using var command = new MySqlCommand(reqNextId, Global.Connexion);
            int prochainId = Convert.ToInt32(command.ExecuteScalar());

            // Le mot de passe saisi est hashé immédiatement, jamais transmis/stocké en clair
            string hash = BCrypt.Net.BCrypt.HashPassword(motDePasseClair);

            var nouvelUtilisateur = new UtilisateurDTO
            {
                IdUser = prochainId,
                Login = login,
                Pwd = hash
                // Les autres champs sont laissés vides : l'utilisateur les complète
                // dans la fenêtre AjoutModifUtilisateurs qui s'ouvre juste après.
            };

            AjoutModifUtilisateurs fenUtilisateur = new AjoutModifUtilisateurs(nouvelUtilisateur, estNouveau: true, this);
            fenUtilisateur.ShowDialog();
        }

        /// <summary>
        /// Gère l'appui sur la touche Entrée depuis la fenêtre Connexion.
        /// Note : this.AcceptButton = btnConnexion dans le constructeur suffit normalement
        /// à déclencher BtnConnexion_Click sur Entrée pour toute la fenêtre.
        /// Les handlers KeyPress dédiés à txtId/txtPwd ont été supprimés car redondants
        /// (à retirer aussi du Designer si toujours câblés).
        /// </summary>
        private void Connexion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnConnexion_Click(sender, e);
            }
        }

        /// <summary>
        /// Permet d'accéder à la connexion
        /// </summary>
        public MySqlConnection GetConnexion()
        {
            return Global.Connexion;
        }

        /// <summary>
        /// Setter sur l'idUser
        /// </summary>
        public void SetIdUser(string idUser)
        {
            this.idUser = idUser;
        }

        /// <summary>
        /// Permet de récupérer l'id de l'utilisateur
        /// </summary>
        public string GetIdUser()
        {
            return this.idUser;
        }

        /// <summary>
        /// Vérifie si les répertoires contenant les quittances et les signatures existent et les crée si besoin
        /// </summary>
        public void CheckDir()
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "/Quittances");
            Directory.CreateDirectory(Environment.CurrentDirectory + "/Signature");
        }
    }
}