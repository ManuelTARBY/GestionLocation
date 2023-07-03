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
        // Nombre maximum d'essai de connexion
      //  private static readonly int essaiMax = 3;

        public Connexion()
        {
            InitializeComponent();
            lblCptEssai.Text = $"Essai : 1/{essaiMax}";
        }
        // Chaîne qui va stocker la chaine de connexion à la BDD
        private string chaineConnexion;
        // Compteur de tentatives de connexion
        private int cptEssai = 0;
        private MySqlConnection connexion;

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
                    if (ConnexionSql())
                    {
                        //Locations accueil = new Locations(this.connexion);
                        Accueil accueil = new Accueil(this.connexion);
                        accueil.Show();
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
        /// Génère la chaîne de connexion à partir des éléments remplis dans les zones de saisie
        /// </summary>
        private void GenererChaineConnexion()
        {
            this.chaineConnexion = $"server={serveurbdd};user id={txtId.Text};password={txtPwd.Text};Convert Zero Datetime=True;Convert Zero Datetime=True;Allow Zero Datetime=true;database=gestionlocation";
        }

        /// <summary>
        /// Teste la connexion à la base de données
        /// </summary>
        /// <returns>true si la connexion la connexion a pu être faite</returns>
        private bool ConnexionSql()
        {
            this.connexion = new MySqlConnection(this.chaineConnexion);
            try
            {
                this.connexion.Open();
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
    }
}

