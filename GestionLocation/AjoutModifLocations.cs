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
    public partial class AjoutModifLocations : Form
    {
        private readonly Locations fenLocation;
        private readonly MySqlConnection connexion;
        private readonly string typeReq;
        private readonly int id;
        private string req;
        private MySqlCommand command;
        private readonly string[] rubLocations = { "idlocation", "idbien", "idcaution", "idlocataire", "debutlocation", "finlocation", "depotgarantie", "locationarchivee" };

        /// <summary>
        /// Constructeur de AjoutModifLocations
        /// </summary>
        /// <param name="fenLocations">Fenêtre de Locations ayant créé l'instance de AjoutModifLocations</param>
        /// <param name="connexion">Chaîne de connexion</param>
        /// <param name="typeReq">Type de requête</param>
        /// <param name="id">id de la location</param>
        public AjoutModifLocations(Locations fenLocation, MySqlConnection connexion, string typeReq, int id = 0)
        {
            InitializeComponent();
            this.fenLocation = fenLocation;
            this.connexion = connexion;
            this.typeReq = typeReq;
            this.id = id;
            // Remplit les listes des biens, des locataires et des cautions
            AfficheLesListes();
            // Si c'est une modification que l'on souhaite faire
            if (this.id != 0)
            {
                SelectionnerElements();
            }
            else
            {
                this.req = "SELECT MAX(req.idlocation) FROM (SELECT idlocation FROM location) AS req";
                this.command = new MySqlCommand(this.req, this.connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            lblID.Text = $"ID : {this.id}";
        }

        /// <summary>
        /// Remplit les listes des biens, locataires et cautions
        /// </summary>
        private void AfficheLesListes()
        {
            RemplirLstBiens();
            RemplirLstLocataires();
            RemplirLstCautions();
        }

        /// <summary>
        /// Gère le remplissage de la liste des biens
        /// </summary>
        private void RemplirLstBiens()
        {
            lstBiens.Items.Clear();
            this.req = "SELECT nombien FROM bien WHERE bienarchive = 0 ORDER BY nombien";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstBiens.Items.Add($"{reader["nombien"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Gère le remplissage de la liste des locataires
        /// </summary>
        private void RemplirLstLocataires()
        {
            lstLocataires.Items.Clear();
            this.req = "SELECT nomcompletlocataire FROM locataire WHERE locatairearchive = 0 ORDER BY nomcompletlocataire";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstLocataires.Items.Add($"{reader["nomcompletlocataire"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Gère le remplissage de la liste des locataires
        /// </summary>
        private void RemplirLstCautions()
        {
            lstCautions.Items.Clear();
            this.req = "SELECT nomcompletcaution FROM caution WHERE cautionarchivee = 0 ORDER BY nomcompletcaution";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstCautions.Items.Add($"{reader["nomcompletcaution"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }

        /// <summary>
        /// Sélectionne le bien, le locataire et la caution de la location sélectionnée
        /// </summary>
        private void SelectionnerElements()
        {
            lstBiens.SelectedIndex = lstBiens.Items.IndexOf(RetrouveBien());
            lstLocataires.SelectedIndex = lstLocataires.Items.IndexOf(RetrouveLocataire());
            lstCautions.SelectedIndex = lstCautions.Items.IndexOf(RetrouveCaution());
            this.req = $"SELECT * FROM location WHERE idlocation = {this.id}";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            datDebut.Value = reader.GetDateTime(4);
            datFin.Value = reader.GetDateTime(5);
            txtDepotGarantie.Text = reader.GetString(6);
            if ((bool)reader["locationarchivee"])
            {
                cbxArchive.Checked = true;
            }
            else
            {
                cbxArchive.Checked = false;
            }
            reader.Close();
        }

        /// <summary>
        /// Retrouve le bien à partir de l'id d'une location
        /// </summary>
        private string RetrouveBien()
        {
            this.req = $"SELECT nombien FROM bien WHERE idbien = (SELECT idbien FROM location WHERE idlocation = {this.id})";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string nombien = ($"{reader["nombien"]}");
            reader.Close();
            return nombien;
        }


        /// <summary>
        /// Retrouve le locataire à partir de l'id d'une location
        /// </summary>
        private string RetrouveLocataire()
        {
            this.req = $"SELECT nomcompletlocataire FROM locataire WHERE idlocataire = (SELECT idlocataire FROM location WHERE idlocation = {this.id})";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string nomLocataire = ($"{reader["nomcompletlocataire"]}");
            reader.Close();
            return nomLocataire;
        }

        /// <summary>
        /// Retrouve la caution à partir de l'id d'une location
        /// </summary>
        private string RetrouveCaution()
        {
            this.req = $"SELECT nomcompletcaution FROM caution WHERE idcaution = (SELECT idcaution FROM location WHERE idlocation = {this.id})";
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string nomCaution = ($"{reader["nomcompletcaution"]}");
            reader.Close();
            return nomCaution;
        }

        /// <summary>
        /// Enregistre la location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (ChampsRenseignes())
            {
                string[] lesId = RecupLesId();
                if (this.typeReq.Equals("UPDATE"))
                {
                    // Construit la requête de modification
                    ConstruitReqModif(lesId);
                }
                else
                {
                    // Construit la requête d'ajout
                    ConstruitReqAjout(lesId);
                }
                // Exécute la requête
                this.command = new MySqlCommand(this.req, this.connexion);
                // préparation de la requête
                this.command.Prepare();
                // exécution de la requête
                this.command.ExecuteNonQuery();
                this.fenLocation.AfficherBiens();
                this.fenLocation.AfficherLocations();
                this.Dispose();
            }
        }

        /// <summary>
        /// Vérifie si tous les champs ont été renseignés
        /// </summary>
        /// <returns>Vrai si tous les champs sont remplis, Faux dans le cas contraire</returns>
        private bool ChampsRenseignes()
        {
            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un bien.");
                return false;
            }
            else if (lstLocataires.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un locataire.");
                return false;
            }
            else if (lstCautions.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une caution.");
                return false;
            }
            else if (datDebut.Value.Equals(""))
            {
                MessageBox.Show("Veuillez sélectionner une date de début.");
                return false;
            }
            else if (datFin.Value.Equals(""))
            {
                MessageBox.Show("Veuillez sélectionner une date de fin.");
                return false;
            }
            else if (datFin.Value < datDebut.Value)
            {
                MessageBox.Show("Veuillez sélectionner des dates cohérentes.");
                return false;
            }
            else
            {
                if (txtDepotGarantie.Text.Equals(""))
                {
                    txtDepotGarantie.Text = "0";
                }
                return true;
            }
        }

        /// <summary>
        /// Récupère l'id du bien sélectionné ainsi que celui du locataire et de la caution
        /// </summary>
        /// <returns></returns>
        private string[] RecupLesId()
        {
            string[] lesId = { "", "", "" };
            // Récupère l'ID du bien
            this.req = $"SELECT idbien FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
            lesId[0] = ReqSelectUnElement();
            // Récupère l'ID du locataire
            this.req = $"SELECT idlocataire FROM locataire WHERE nomcompletlocataire = \"{lstLocataires.SelectedItem}\"";
            lesId[1] = ReqSelectUnElement();
            // Récupère l'ID de la caution
            this.req = $"SELECT idcaution FROM caution WHERE nomcompletcaution = \"{lstCautions.SelectedItem}\"";
            lesId[2] = ReqSelectUnElement();
            return lesId;
        }

        /// <summary>
        /// Construit la requête de modification
        /// </summary>
        private void ConstruitReqModif(string[] lesId)
        {
            this.req = $"{this.typeReq} location SET ";
            this.req += $"idlocation = {this.id}, idbien = \"{lesId[0]}\", idcaution = \"{lesId[2]}\", idlocataire = \"{lesId[1]}\", " +
                $"debutlocation = \"{datDebut.Value:yyyy-MM-dd}\", finlocation = \"{datFin.Value:yyyy-MM-dd}\", " +
                $"depotgarantie = \"{txtDepotGarantie.Text}\", locationarchivee = {cbxArchive.Checked} WHERE idlocation = {this.id}";
        }

        /// <summary>
        /// Construit la requête d'ajout
        /// </summary>
        private void ConstruitReqAjout(string[] lesId)
        {
            this.req = $"{this.typeReq} location (";
            for (int i = 0; i < this.rubLocations.Length - 1; i++)
            {
                this.req += $"{rubLocations[i]}, ";
            }
            this.req += $"{rubLocations[rubLocations.Length - 1]}) VALUES ({this.id}, {lesId[0]}, {lesId[2]}, " +
                $"{lesId[1]}, \"{datDebut.Value:yyyy-MM-dd}\", \"{datFin.Value:yyyy-MM-dd}\", {txtDepotGarantie.Text}, {cbxArchive.Checked})";
        }

        /// <summary>
        /// Retourne le résultat unique d'une requête select
        /// </summary>
        /// <returns>Valeur retournée par la requête</returns>
        private string ReqSelectUnElement()
        {
            this.command = new MySqlCommand(this.req, this.connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string resultat = reader.GetString(0);
            reader.Close();
            return resultat;
        }

        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
