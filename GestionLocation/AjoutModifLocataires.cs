using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifLocataires : Form
    {

        private readonly Locataires fenLocataire;
        private readonly string typeReq;
        private readonly int id;
        private string req;
        private MySqlCommand command;
        private readonly string[] rubLocataires = { "idlocataire", "prenomlocataire", "nomlocataire", "nomcompletlocataire", "adresselocataire", "cplocataire", "villelocataire", "datenaissancelocataire", "lieunaissancelocataire", "telephonelocataire", "emailocataire", "locatairearchive" };
        private readonly string[] stringDelimit = { ", " };
        private string[] nomprenom;

        /// <summary>
        /// Constructeur de AjoutModifLocataire
        /// </summary>
        /// <param name="fenLocataire"></param>
        /// <param name="typeReq"></param>
        /// <param name="id"></param>
        public AjoutModifLocataires(Locataires fenLocataire, string typeReq, int id = 0)
        {
            InitializeComponent();
            this.Text = "Ajout/Modification d'un locataire";
            this.fenLocataire = fenLocataire;
            this.typeReq = typeReq;
            this.id = id;
            if (this.id == 0)
            {
                this.req = "SELECT MAX(req.idlocataire) FROM (SELECT idlocataire FROM locataire) AS req";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            else
            {
                AfficheInfo();
            }
            lblID.Text = $"ID : {this.id}";
        }

        /// <summary>
        /// Remplit les champs
        /// </summary>
        /// <param name="id"></param>
        private void AfficheInfo()
        {
            this.req = $"SELECT * FROM locataire WHERE idlocataire = {this.id}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            // affichage des champs récupérés dans la ligne
            txtPrenom.Text = reader.GetString(1);
            txtNom.Text = reader.GetString(2);
            txtAdresse.Text = reader.GetString(4);
            txtCp.Text = reader.GetString(5);
            txtVille.Text = reader.GetString(6);
            datDateNaissance.Value = reader.GetDateTime(7);
            txtLieuNaissance.Text = reader.GetString(8);
            txtTelephone.Text = reader.GetString(9);
            txtEmail.Text = reader.GetString(10);
            if ((bool)reader["locatairearchive"])
            {
                cbxArchive.Checked = true;
            }
            else
            {
                cbxArchive.Checked = false;
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Gère le clic sur la bouton "Valider"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!ChampsRenseignes())
            {
                MessageBox.Show("Vous devez au moins remplir les champs Prénom, Nom, Téléphone et Email pour pouvoir valider la saisie.");
            }
            else
            {
                if (this.typeReq.Equals("UPDATE"))
                {
                    // Construit la requête de modification
                    ConstruitReqModif();
                }
                else
                {
                    // Construit la requête d'ajout
                    ConstruitReqAjout();
                }
                // Exécute la requête
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Parameters.AddWithValue("@prenom", this.nomprenom[0]);
                this.command.Parameters.AddWithValue("@nom", this.nomprenom[1]);
                this.command.Parameters.AddWithValue("@nomcomplet", this.nomprenom[2]);
                this.command.Parameters.AddWithValue("@adresse", txtAdresse.Text);
                this.command.Parameters.AddWithValue("@ville", txtVille.Text.ToUpper());
                this.command.Parameters.AddWithValue("@lieunaissance", txtLieuNaissance.Text.ToUpper());
                // préparation de la requête
                this.command.Prepare();
                // exécution de la requête
                this.command.ExecuteNonQuery();
                this.fenLocataire.RemplirLstLocataires();
                this.Dispose();
            }
        }

        /// <summary>
        /// Vérifie si tous les champs ont été renseignés
        /// </summary>
        /// <returns></returns>
        private bool ChampsRenseignes()
        {
            if (txtPrenom.Text.Equals("") || txtNom.Text.Equals("") || txtTelephone.Text.Equals("") || txtEmail.Text.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Construit la requête de modification
        /// </summary>
        private void ConstruitReqModif()
        {
            this.nomprenom = MiseEnFormeNomPrenom();
            this.req = $"{this.typeReq} locataire SET ";
            this.req += $"idlocataire = {this.id}, prenomlocataire = @prenom, nomlocataire = @nom, " +
                $"adresselocataire = @adresse, cplocataire = \'{txtCp.Text}\', villelocataire = @ville, " +
                $"datenaissancelocataire = \'{datDateNaissance.Value:yyyy-MM-dd}\', lieunaissancelocataire = @lieunaissance, " +
                $"telephonelocataire = \'{EspacerNumTel()}\', emailocataire = \'{txtEmail.Text}\', locatairearchive = {cbxArchive.Checked}, " +
                $"nomcompletlocataire = @nomcomplet WHERE idlocataire = {this.id}";
        }

        /// <summary>
        /// Construit la requête d'ajout
        /// </summary>
        private void ConstruitReqAjout()
        {
            this.nomprenom = MiseEnFormeNomPrenom();
            this.req = $"{this.typeReq} locataire (";
            for (int i = 0; i < this.rubLocataires.Length - 1; i++)
            {
                this.req += $"{rubLocataires[i]}, ";
            }
            this.req += $"{rubLocataires[rubLocataires.Length - 1]}) VALUES ({this.id}, @prenom, @nom," +
                $"@nomcomplet, @adresse, \"{txtCp.Text}\", @ville, \"{datDateNaissance.Value:yyyy-MM-dd}\"," +
                $" @lieunaissance, \"{EspacerNumTel()}\", \"{txtEmail.Text}\", {cbxArchive.Checked})";
        }

        /// <summary>
        /// Génère les espaces tous les deux chiffres pour les numéros de téléphone
        /// </summary>
        /// <returns>numéro de téléphone avec les espaces</returns>
        private StringBuilder EspacerNumTel()
        {
            StringBuilder leNum = new StringBuilder(txtTelephone.Text);
            if (leNum.Length == 10)
            {
                int[] indices = { 2, 5, 8, 11 };
                foreach (int i in indices)
                {
                    leNum.Insert(i, " ");
                }
            }
            return leNum;
        }


        /// <summary>
        /// Récupère et met en forme les noms (tout en majuscule) et prénoms (majuscule sur la première lettre) du formulaire
        /// </summary>
        /// <returns>Tableau de chaîne contenant : 1, les prénoms, 2, le nom, 3, le nom complet (nom + premier prénom)</returns>
        private string[] MiseEnFormeNomPrenom()
        {
            string[] nomprenom = { "", "", "" };
            //string[] lesPrenoms = txtPrenom.Text.Split(charDelimit);
            string[] lesPrenoms = txtPrenom.Text.Split(stringDelimit, StringSplitOptions.RemoveEmptyEntries);
            // Met le nom tout en majuscule
            nomprenom[1] = txtNom.Text.ToUpper();
            // Construit le nom complet (nom + prénom)
            nomprenom[2] = nomprenom[1] + " " + Global.Capitalize((lesPrenoms[0]));
            // Reconstruit la chaîne des prénoms avec une majuscule à chaque prenom
            for (int i = 0; i < lesPrenoms.Length - 1; i++)
            {
                nomprenom[0] += Global.Capitalize(lesPrenoms[i]) + ", ";
            }
            nomprenom[0] += Global.Capitalize(lesPrenoms[lesPrenoms.Length - 1]);
            return nomprenom;
        }
    }
}
