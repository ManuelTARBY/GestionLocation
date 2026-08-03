using System;
using System.Globalization;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class DateAssurance : Form
    {
        // Propriétés publiques typées accessibles par le formulaire parent après validation
        public DateTime DateSouscription { get; private set; }
        public DateTime DateEcheance { get; private set; }
        public decimal MontantAssurance { get; private set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public DateAssurance()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// Validation et enregistrement des données
        /// </summary>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            // Normalisation de la saisie (accepte le point et la virgule)
            string montantTexte = txtMontantAssur.Text.Trim().Replace(',', '.');

            if (decimal.TryParse(montantTexte, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal montant) && montant >= 0)
            {
                // Assignation des propriétés
                DateSouscription = dateSouscri.Value.Date;
                DateEcheance = DateSouscription.AddYears(1).AddDays(-1);
                MontantAssurance = montant;

                // Ferme la fenêtre en indiquant au parent que la saisie est validée
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Veuillez saisir un montant valide pour la prime d'assurance.",
                    "Saisie incorrecte",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMontantAssur.Focus();
                txtMontantAssur.SelectAll();
            }
        }
    }
}