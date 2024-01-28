using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

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
            lblCptEssai.Text = $"Essai : {this.cptEssai}/{essaiMax}";
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
                if (this.cptEssai <= essaiMax)
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
                            string[] infos = { "INSERT INTO", "", txtId.Text, txtPwd.Text, "", "", "", "", "", "", "", "", "", "", "", "" };
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
            catch
            {
                MessageBox.Show("La connexion n'a pas pu être établie.");
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

        private async void Button2_ClickAsync(object sender, EventArgs e)
        {
            string uri = "https://api.insee.fr/series/BDM/data/SERIES_BDM/001515333";
            string bearerToken = "f6960065-2fab-3db3-a88a-908fd5d75461";

            HttpClient client = new HttpClient();
            // Configure les en-têtes de requête
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.AcceptEncoding.Clear();
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            // Envoie une requête GET à l'URL
            try
            {
                HttpResponseMessage httpResponse = await client.GetAsync(uri);

                if (httpResponse.IsSuccessStatusCode)
                {
                    Stream responseStream = await httpResponse.Content.ReadAsStreamAsync();
                    // Décompresse le contenu gzip
                    using (GZipStream gzipStream = new GZipStream(responseStream, CompressionMode.Decompress))
                    using (StreamReader reader = new StreamReader(gzipStream))
                    {
                        string response = await reader.ReadToEndAsync();

                        // Charger le XML dans un XmlDocument
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(response);
                        XmlNodeList elements = xmlDoc.GetElementsByTagName("Obs");
                        foreach (XmlNode elt in elements)
                        {
                            // Accéder aux éléments des IRL
                            if (!elt.Attributes["DATE_JO"].Value.Equals(""))
                            {
                                string period = elt.Attributes["TIME_PERIOD"].Value;
                                string valeur = elt.Attributes["OBS_VALUE"].Value;
                                Console.WriteLine(period.Replace("Q", "T") + " : " + valeur);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("La requête a échoué avec le code : " + httpResponse.StatusCode);
                }
            }
            catch (HttpRequestException err)
            {
                Console.WriteLine("Une erreur s'est produite. " + err.Message);
            }
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

