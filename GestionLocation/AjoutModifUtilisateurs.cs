using GestionLocation.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifUtilisateurs : Form
    {
        private string adresseSmtp;
        private int port;
        private readonly UtilisateurDTO utilisateur;
        private readonly bool estNouveau;
        private readonly Connexion fenConnexion;

        /// <summary>
        /// Constructeur de la fenêtre AjoutModifUtilisateur
        /// </summary>
        /// <param name="utilisateur">Utilisateur à créer ou modifier</param>
        /// <param name="estNouveau">True s'il s'agit d'une création, False pour une modification</param>
        /// <param name="fenConnexion">Instance de la classe Connexion</param>
        public AjoutModifUtilisateurs(UtilisateurDTO utilisateur, bool estNouveau, Connexion fenConnexion)
        {
            InitializeComponent();
            this.Text = "Ajout/Modification d'un utilisateur";
            this.utilisateur = utilisateur;
            this.estNouveau = estNouveau;
            this.fenConnexion = fenConnexion;

            lblID.Text = utilisateur.IdUser.ToString();
            txtPrenom.Text = utilisateur.Prenom;
            txtNom.Text = utilisateur.Nom;
            txtAdresse.Text = utilisateur.Adresse;
            txtCp.Text = utilisateur.CodePostal;
            txtVille.Text = utilisateur.Ville;
            txtEmail.Text = utilisateur.Email;
            txtPwdEmail.Text = utilisateur.PwdEmail;
            txtServeurSMTP.Text = utilisateur.ServeurSmtp;
            txtPort.Text = utilisateur.Port == 0 ? "" : utilisateur.Port.ToString();
            txtSignature.Text = utilisateur.CheminSignature;
        }

        /// <summary>
        /// Enregistre/Modifie l'utilisateur et ouvre la fenêtre Accueil
        /// </summary>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!VerifChamps())
            {
                return;
            }

            string messagerie = ChercherMessagerie();
            if (!ChercherInfosClientMail(messagerie))
            {
                return;
            }

            if (this.estNouveau)
            {
                InsererUtilisateur();
            }
            else
            {
                MettreAJourUtilisateur();
            }

            // Retaille l'image de la signature et la place dans le répertoire de l'application
            Image signature = ResizeImg();
            string dest = $"{Environment.CurrentDirectory}/Signature/{Global.Capitalize(txtPrenom.Text)} {txtNom.Text.ToUpper()}.png";
            signature.Save(dest);

            // Modifie l'iduser (n'a d'effet qu'en cas de création d'un utilisateur)
            this.fenConnexion.SetIdUser(this.utilisateur.IdUser.ToString());

            Accueil fenAccueil = new Accueil(this.fenConnexion);
            this.fenConnexion.Visible = false;
            this.Dispose();
            fenAccueil.ShowDialog();
        }

        /// <summary>
        /// Insère un nouvel utilisateur en base
        /// </summary>
        private void InsererUtilisateur()
        {
            const string req =
                "INSERT INTO utilisateur " +
                "(iduser, login, pwd, prenomuser, nomuser, adresseuser, cpuser, villeuser, " +
                "emailuser, pwdemail, adresseserveursmtp, port, clientid, clientsecret, signature) " +
                "VALUES (@id, @login, @pwd, @prenom, @nom, @adresse, @cp, @ville, @email, @pwdEmail, " +
                "@smtp, @port, @clientId, @clientSecret, @signature)";

            using var command = new MySqlCommand(req, Global.Connexion);
            AjouterParametresCommuns(command);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Met à jour un utilisateur existant en base
        /// </summary>
        private void MettreAJourUtilisateur()
        {
            const string req =
                "UPDATE utilisateur SET " +
                "login = @login, pwd = @pwd, prenomuser = @prenom, nomuser = @nom, " +
                "adresseuser = @adresse, cpuser = @cp, villeuser = @ville, emailuser = @email, " +
                "pwdemail = @pwdEmail, adresseserveursmtp = @smtp, port = @port, " +
                "clientid = @clientId, clientsecret = @clientSecret, signature = @signature " +
                "WHERE iduser = @id";

            using var command = new MySqlCommand(req, Global.Connexion);
            AjouterParametresCommuns(command);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Ajoute les paramètres communs à l'insertion et à la modification.
        /// clientid/clientsecret viennent du DTO existant et ne sont jamais écrasés
        /// par cette fenêtre, qui ne propose pas de les modifier.
        /// </summary>
        private void AjouterParametresCommuns(MySqlCommand command)
        {
            command.Parameters.AddWithValue("@id", this.utilisateur.IdUser);
            command.Parameters.AddWithValue("@login", this.utilisateur.Login);
            command.Parameters.AddWithValue("@pwd", this.utilisateur.Pwd);
            command.Parameters.AddWithValue("@prenom", Global.Capitalize(txtPrenom.Text));
            command.Parameters.AddWithValue("@nom", txtNom.Text.ToUpper());
            command.Parameters.AddWithValue("@adresse", txtAdresse.Text);
            command.Parameters.AddWithValue("@cp", txtCp.Text);
            command.Parameters.AddWithValue("@ville", txtVille.Text.ToUpper());
            command.Parameters.AddWithValue("@email", txtEmail.Text);
            command.Parameters.AddWithValue("@pwdEmail", txtPwdEmail.Text);
            command.Parameters.AddWithValue("@smtp", this.adresseSmtp);
            command.Parameters.AddWithValue("@port", this.port);
            command.Parameters.AddWithValue("@clientId", this.utilisateur.ClientId ?? "");
            command.Parameters.AddWithValue("@clientSecret", this.utilisateur.ClientSecret ?? "");
            // Nom de fichier canonique, cohérent avec le "dest" utilisé pour sauvegarder l'image
            command.Parameters.AddWithValue("@signature", $"{Global.Capitalize(txtPrenom.Text)} {txtNom.Text.ToUpper()}.png");
        }

        /// <summary>
        /// Vérifie que tous les champs soient bien remplis
        /// </summary>
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
            else if (txtSignature.Text.Equals(""))
            {
                MessageBox.Show("Veuillez choisir une signature.");
                return false;
            }
            else
            {
                return true;
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
        /// Récupère les infos (port et adresse serveur smtp) à partir de l'adresse mail
        /// </summary>
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
                case "live":
                case "outlook":
                case "hotmail":
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
        /// Gère le clic sur le bouton pour sélectionner la signature
        /// </summary>
        private void BtnExplorateur_Click(object sender, EventArgs e)
        {
            RecupSignature();
        }

        /// <summary>
        /// Récupère la signature de l'utilisateur
        /// </summary>
        public void RecupSignature()
        {
            OpenFileDialog open = new OpenFileDialog();
            string ext;
            do
            {
                MessageBox.Show("Veuillez choisir un fichier png.");
                open.ShowDialog();
                txtSignature.Text = open.FileName;
                ext = txtSignature.Text.Length >= 4
                    ? txtSignature.Text.Substring(txtSignature.Text.Length - 4, 4)
                    : "";
            } while (txtSignature.Text.Equals("") || !ext.Equals(".png"));
        }

        /// <summary>
        /// Redimensionne une image.
        /// Charge le fichier via un MemoryStream (et non Image.FromFile) pour ne
        /// jamais garder de verrou sur le fichier source : essentiel en modification,
        /// où txtSignature.Text pointe déjà vers le même fichier que la destination
        /// de sauvegarde (dest), ce qui provoquait "Une erreur générique dans GDI+"
        /// lors du Save().
        /// </summary>
        public Image ResizeImg()
        {
            Bitmap imgbitmap;
            byte[] bytes = File.ReadAllBytes(txtSignature.Text);
            using (var stream = new MemoryStream(bytes))
            using (Image img = Image.FromStream(stream))
            {
                imgbitmap = new Bitmap(img);
            }

            float rapport = (float)imgbitmap.Height / Global.HeightMaxSignature;
            int newWidth = (int)(imgbitmap.Width / rapport);
            Image signature = new Bitmap(imgbitmap, new Size(newWidth, Global.HeightMaxSignature));
            imgbitmap.Dispose();
            return signature;
        }
    }
}