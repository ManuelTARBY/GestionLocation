using MySql.Data.MySqlClient;
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
    public partial class AjoutModifCautions : Form
    {

        private readonly Cautions fenCaution;
        private readonly string typeReq;
        private readonly int id;
        private string req;
        private MySqlCommand command;
        private readonly string[] rubCautions = { "idcaution", "prenomcaution", "nomcaution", "nomcompletcaution",
            "adressecaution", "cpcaution", "villecaution", "telephonecaution", "emailcaution", "cautionarchivee" };
        private readonly string[] stringDelimit = { ", " };
        private string[] nomprenom;

        /// <summary>
        /// Constructeur de la fenêtre AjoutModifCautions
        /// </summary>
        /// <param name="fenLCaution"></param>
        /// <param name="typeReq"></param>
        /// <param name="id"></param>
        public AjoutModifCautions(Cautions fenCaution, string typeReq, int id = 0)
        {
            InitializeComponent();
            this.Text = "Ajout/Modification d'une caution";
            this.fenCaution = fenCaution;
            this.typeReq = typeReq;
            this.id = id;
            if (this.id == 0)
            {
                this.req = "SELECT MAX(req.idcaution) FROM (SELECT idcaution FROM caution) AS req";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
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
            this.req = $"SELECT * FROM caution WHERE idcaution = {this.id}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            this.command.Prepare();
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            // affichage des champs récupérés dans la ligne
            txtPrenom.Text = reader.GetString(1);
            txtNom.Text = reader.GetString(2);
            txtAdresse.Text = reader.GetString(4);
            txtCp.Text = reader.GetString(5);
            txtVille.Text = reader.GetString(6);
            txtTelephone.Text = reader.GetString(7);
            txtEmail.Text = reader.GetString(8);
            if ((bool)reader["cautionarchivee"])
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
        /// Vérifie si tous les champs ont été renseignés
        /// </summary>
        /// <returns></returns>
        private bool ChampsRenseignes()
        {
            if (txtPrenom.Text.Equals("") || txtNom.Text.Equals("") )
            {
                return false;
            }
            else if (txtEmail.Text.Equals("") && txtTelephone.Text.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton Valider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!ChampsRenseignes())
            {
                MessageBox.Show("Vous devez au moins remplir les champs Prénom, Nom et Téléphone ou Email pour pouvoir valider la saisie.");
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
                // préparation de la requête
                this.command.Prepare();
                // exécution de la requête
                this.command.ExecuteNonQuery();
                this.fenCaution.RemplirLstCautions();
                this.Dispose();
            }
        }

        /// <summary>
        /// Construit la requête de modification
        /// </summary>
        private void ConstruitReqModif()
        {
            this.nomprenom = MiseEnFormeNomPrenom();
            this.req = $"{this.typeReq} caution SET ";
            this.req += $"idcaution = {this.id}, prenomcaution = @prenom, nomcaution = @nom, " +
                $"adressecaution = @adresse, cpcaution = \"{txtCp.Text}\", villecaution = @ville, " +
                $"telephonecaution = \"{EspacerNumTel()}\", emailcaution = \"{txtEmail.Text}\", cautionarchivee = {cbxArchive.Checked}, " +
                $"nomcompletcaution = @nomcomplet WHERE idcaution = {this.id}";
        }

        /// <summary>
        /// Construit la requête d'ajout
        /// </summary>
        private void ConstruitReqAjout()
        {
            this.nomprenom = MiseEnFormeNomPrenom();
            // Construit la chaîne de la requête
            this.req = $"{this.typeReq} caution (";
            for (int i = 0; i < this.rubCautions.Length - 1; i++)
            {
                this.req += $"{rubCautions[i]}, ";
            }
            this.req += $"{rubCautions[rubCautions.Length - 1]}) VALUES ({this.id}, @prenom," +
                $"@nom, @nomcomplet, @adresse, \"{txtCp.Text}\", @ville," +
                $" \"{EspacerNumTel()}\", \"{txtEmail.Text}\", {cbxArchive.Checked})";
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
