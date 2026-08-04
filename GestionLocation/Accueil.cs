using GestionLocation.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class Accueil : Form
    {
        
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

            AbonnerEffetsSurvol();

            RecupInfoUser();
            AfficherLocations();
        }

        /// <summary>
        /// Met à jour les données sur la session (email, SMTP...) de l'utilisateur connecté
        /// </summary>
        private void RecupInfoUser()
        {
            const string req = "SELECT prenomuser, nomuser, emailuser, pwdemail, adresseserveursmtp, port " +
                                "FROM utilisateur WHERE iduser = @idUser";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@idUser", this.idUser);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                Global.User = $"{reader.GetString("prenomuser")} {reader.GetString("nomuser")}";
                Global.EmailUser = reader.GetString("emailuser");
                Global.PwdUser = reader.GetString("pwdemail");
                Global.ServeurSmtp = reader.GetString("adresseserveursmtp");
                Global.PortEmail = reader.GetInt32("port");
            }
        }

        /// <summary>
        /// Met à jour la liste des locations en fonction des critères sélectionnés par l'utilisateur
        /// </summary>
        public void AfficherLocations()
        {
            lstLocations.Items.Clear();

            const string req =
                "SELECT nombien, " +
                "CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', nomlocataire) AS locataire, " +
                "debutlocation, finlocation, " +
                "CONCAT(SUBSTRING_INDEX(prenomcaution, ',', 1), ' ', nomcaution) AS caution " +
                "FROM location " +
                "JOIN locataire USING(idlocataire) " +
                "JOIN bien USING(idbien) " +
                "JOIN caution USING(idcaution) " +
                "WHERE locationarchivee = 0 " +
                "ORDER BY nombien";

            using var command = new MySqlCommand(req, Global.Connexion);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lstLocations.Items.Add(
                    $"{reader["nombien"]} || {reader["locataire"]} " +
                    $" || Du {reader.GetDateTime("debutlocation"):d} au {reader.GetDateTime("finlocation"):d} " +
                    $"|| Caution : {reader["caution"]}");
            }
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
            FermerApplication();
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
        /// Abonne tous les boutons du menu principal aux mêmes handlers génériques
        /// de survol (au lieu d'un handler dédié par bouton).
        /// </summary>
        private void AbonnerEffetsSurvol()
        {
            Button[] boutonsAvecSurvol =
            {
                btnBiens, btnLocations, btnLocataires, btnCautions,
                btnCharges, btnPaiements, btnUser, btnGroupes,btnStats
            };

            foreach (Button bouton in boutonsAvecSurvol)
            {
                bouton.MouseEnter += Bouton_MouseEnter;
                bouton.MouseLeave += Bouton_MouseLeave;
            }
        }

        /// <summary>
        /// Gère le survol de n'importe quel bouton du menu principal
        /// </summary>
        private void Bouton_MouseEnter(object sender, EventArgs e)
        {
            SurvolEntree((Button)sender);
        }

        /// <summary>
        /// Gère la sortie de survol de n'importe quel bouton du menu principal
        /// </summary>
        private void Bouton_MouseLeave(object sender, EventArgs e)
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
        /// Récupère les infos d'un utilisateur sous forme de DTO
        /// </summary>
        /// <param name="idUser">ID de l'utilisateur</param>
        /// <returns>UtilisateurDTO, ou null si l'utilisateur n'existe pas</returns>
        public UtilisateurDTO RecupInfosUser(string idUser)
        {
            const string req =
                "SELECT login, pwd, prenomuser, nomuser, adresseuser, cpuser, villeuser, " +
                "emailuser, pwdemail, adresseserveursmtp, port, clientid, clientsecret, signature " +
                "FROM utilisateur WHERE iduser = @idUser";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@idUser", idUser);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            var utilisateur = new UtilisateurDTO
            {
                IdUser = int.Parse(idUser),
                Login = reader.GetString("login"),
                Pwd = reader.GetString("pwd"),
                Prenom = reader.GetString("prenomuser"),
                Nom = reader.GetString("nomuser"),
                Adresse = reader.GetString("adresseuser"),
                CodePostal = reader.GetString("cpuser"),
                Ville = reader.GetString("villeuser"),
                Email = reader.GetString("emailuser"),
                PwdEmail = reader.GetString("pwdemail"),
                ServeurSmtp = reader.GetString("adresseserveursmtp"),
                Port = reader.GetInt32("port"),
                ClientId = reader.GetString("clientid"),
                ClientSecret = reader.GetString("clientsecret")
            };

            int ordSignature = reader.GetOrdinal("signature");
            if (!reader.IsDBNull(ordSignature) && reader.GetString("signature") != "")
            {
                utilisateur.CheminSignature =
                    $"{Environment.CurrentDirectory}/Signature/{utilisateur.Prenom} {utilisateur.Nom}.png";
            }

            return utilisateur;
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
            UtilisateurDTO utilisateur = RecupInfosUser(this.idUser);
            AjoutModifUtilisateurs fenUser = new AjoutModifUtilisateurs(utilisateur, estNouveau: false, this.fenConnexion);
            this.Visible = false;
            fenUser.ShowDialog();
            this.Visible = true;
        }


        private void FermerApplication()
        {
            Global.Connexion.Close();
            Application.Exit();
        }

        /// <summary>
        /// Gère la fermeture de la fenêtre (ferme l'application et coupe la connexion)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accueil_FormClosing(object sender, FormClosingEventArgs e)
        {
            FermerApplication();
        }
    }
}
