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
    public partial class AjoutModifBiens : Form
    {

        private readonly MySqlConnection connexion;
        private MySqlCommand command;
        private readonly Biens leBien;
        private string req;
        private readonly int id;
        private readonly string typeReq;
        private readonly string[] rubBiens = {"idbien", "nombien", "loyerhc", "charges", "loyercc", "adressebien", "cpbien", "villebien", "bienarchive"};

        public AjoutModifBiens(Biens fenBien, MySqlConnection connexion, string typeReq, int id = 0)
        {
            InitializeComponent();
            this.leBien = fenBien;
            this.id = id;
            this.connexion = connexion;
            this.typeReq = typeReq;
            if (this.id == 0)
            {
                this.req = "SELECT MAX(req.idbien) FROM (SELECT idbien FROM bien) AS req";
                this.command = new MySqlCommand(this.req, this.connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            else
            {
                AfficheInfo(this.id);
            }
            lblID.Text = $"ID : {this.id}";
        }

        /// <summary>
        /// Remplit les champs
        /// </summary>
        /// <param name="id"></param>
        private void AfficheInfo(int id)
        {
            this.req = $"SELECT * FROM bien WHERE idbien = {id}";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            // affichage des champs récupérés dans la ligne
            txtNom.Text = reader.GetString(1);
            txtLoyerHC.Text = reader.GetString(2);
            txtCharges.Text = reader.GetString(3);
            txtLoyerCC.Text = reader.GetString(4);
            txtAdresse.Text = reader.GetString(5);
            txtCp.Text = reader.GetString(6);
            txtVille.Text = reader.GetString(7);
            if ((bool)reader["bienarchive"])
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
        /// Gère le clic sur le bouton "Valider"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!ChampsRenseignes())
            {
                MessageBox.Show("Veuillez remplir tous les champs pour pouvoir valider la saisie.");
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
                this.command = new MySqlCommand(this.req, this.connexion);
                // préparation de la requête
                this.command.Prepare();
                // exécution de la requête
                this.command.ExecuteNonQuery();
                this.leBien.RemplirLstBiens();
                this.Dispose();
            }
        }

        /// <summary>
        /// Vérifie si tous les champs ont été renseignés
        /// </summary>
        /// <returns></returns>
        private bool ChampsRenseignes()
        {
            if (txtNom.Text.Equals("") || txtLoyerHC.Text.Equals("") || txtCharges.Text.Equals("") || txtLoyerCC.Text.Equals("")
                || txtAdresse.Text.Equals("") || txtCp.Text.Equals("") || txtVille.Text.Equals(""))
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
            this.req = $"{this.typeReq} bien SET ";
            this.req += $"idbien = {this.id}, nombien = \"{txtNom.Text}\", loyerHC = \"{txtLoyerHC.Text}\", " +
                $"charges = \"{txtCharges.Text}\", loyerCC = \"{txtLoyerCC.Text}\", adressebien = \"{txtAdresse.Text}\", " +
                $"cpbien = \"{txtCp.Text}\", villebien = \"{txtVille.Text}\", bienarchive = {cbxArchive.Checked} WHERE idbien = {this.id}";
        }

        /// <summary>
        /// Construit la requête d'ajout
        /// </summary>
        private void ConstruitReqAjout()
        {
            this.req = $"{this.typeReq} bien (";
            for (int i = 0; i < this.rubBiens.Length - 1; i++)
            {
                this.req += $"{rubBiens[i]}, ";
            }
            this.req += $"{rubBiens[8]}) VALUES ({this.id}, \"{txtNom.Text}\", \"{txtLoyerHC.Text}\", \"{txtCharges.Text}\", " +
                $"\"{txtLoyerCC.Text}\", \"{txtAdresse.Text}\", \"{txtCp.Text}\", \"{txtVille.Text}\", {cbxArchive.Checked})";
        }
    }
}
