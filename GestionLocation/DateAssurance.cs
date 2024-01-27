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
    public partial class DateAssurance : Form
    {
        // Variable contenant les valeurs qui seront retournées
        public string[] lesDates = { "", "" };
        
        /// <summary>
        /// Constructeur
        /// </summary>
        public DateAssurance()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }


        /// <summary>
        /// Récupère les deux dates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Affecte les valeursde retour à la fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateAssurance_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.lesDates[0] = dateSouscri.Value.ToString("dd/MM/yyyy");
            this.lesDates[1] = dateSouscri.Value.AddYears(1).AddDays(-1).ToString("dd/MM/yyyy");
        }
    }
}
