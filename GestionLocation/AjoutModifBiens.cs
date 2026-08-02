using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifBiens : Form
    {
        private readonly Biens leBien;
        private readonly int id;
        private readonly bool estNouveau;

        /// <summary>
        /// Constructeur de la fenêtre AjoutModifBiens
        /// </summary>
        /// <param name="fenBien">Instance de la fenêtre Biens</param>
        /// <param name="estNouveau">True pour une création, False pour une modification</param>
        /// <param name="id">Id du bien (ignoré si estNouveau = true)</param>
        public AjoutModifBiens(Biens fenBien, bool estNouveau, int id = 0)
        {
            InitializeComponent();
            RemplitLesCombos();
            this.Text = "Ajout/Modification d'un bien";
            this.leBien = fenBien;
            this.estNouveau = estNouveau;

            if (estNouveau)
            {
                this.id = ProchainIdBien();
            }
            else
            {
                this.id = id;
                AfficheInfo(this.id);
            }

            lblID.Text = $"ID : {this.id}";
        }

        /// <summary>
        /// Calcule le prochain id disponible pour un nouveau bien.
        /// IFNULL(...) évite une exception si la table est vide (MAX renverrait NULL).
        /// </summary>
        private int ProchainIdBien()
        {
            const string req = "SELECT IFNULL(MAX(idbien), 0) + 1 FROM bien";
            using var command = new MySqlCommand(req, Global.Connexion);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// Remplit les combobox de la fenêtre
        /// </summary>
        public void RemplitLesCombos()
        {
            cbxTypeHabitat.Items.Add("Appartement");
            cbxTypeHabitat.Items.Add("Maison");
            cbxTypeHabitat.Items.Add("Chambre en colocation");

            cbxRegimeJuri.Items.Add("Mono propriété");
            cbxRegimeJuri.Items.Add("Copropriété");

            cbxProdChauff.Items.Add("Individuelle");
            cbxProdChauff.Items.Add("Collective");

            cbxProdEauChaude.Items.Add("Individuelle");
            cbxProdEauChaude.Items.Add("Collective");

            // Classe DPE
            foreach (char classe in "ABCDEFG")
            {
                cbxClasseDPE.Items.Add(classe.ToString());
            }
        }

        /// <summary>
        /// Remplit les champs à partir de l'id du bien
        /// </summary>
        private void AfficheInfo(int id)
        {
            const string req =
                "SELECT nombien, loyerhc, charges, loyercc, adressebien, cpbien, villebien, bienarchive, " +
                "typehabitat, regimejuridique, periodeconstruction, superficie, nbpiece, description, " +
                "elementequip, autre, prodchauff, prodeauchaude, chargesimputables, chargeannuelles, " +
                "numerofiscal, classeDPE, estimationconsommation, anneereference " +
                "FROM bien WHERE idbien = @id";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("Ce bien n'existe plus.");
                this.Dispose();
                return;
            }

            txtNom.Text = reader.GetString("nombien");
            txtLoyerHC.Text = reader.GetString("loyerhc");
            txtCharges.Text = reader.GetString("charges");
            txtLoyerCC.Text = reader.GetString("loyercc");
            txtAdresse.Text = reader.GetString("adressebien");
            txtCp.Text = reader.GetString("cpbien");
            txtVille.Text = reader.GetString("villebien");
            cbxArchive.Checked = reader.GetBoolean("bienarchive");

            string typeHabitat = reader.GetString("typehabitat");
            cbxTypeHabitat.SelectedIndex = typeHabitat switch
            {
                "Appartement" => 0,
                "Maison" => 1,
                _ => 2
            };

            cbxRegimeJuri.SelectedIndex = reader.GetString("regimejuridique") == "Mono propriété" ? 0 : 1;

            txtPerConstruc.Text = reader.GetString("periodeconstruction");
            txtSuperficie.Text = reader.GetString("superficie");
            txtNbPiece.Text = reader.GetString("nbpiece");
            txtDescriLogement.Text = reader.GetString("description");
            txtElemEquip.Text = reader.GetString("elementequip");
            txtAutre.Text = reader.GetString("autre");

            cbxProdChauff.SelectedIndex = reader.GetString("prodchauff") == "Individuelle" ? 0 : 1;
            cbxProdEauChaude.SelectedIndex = reader.GetString("prodeauchaude") == "Individuelle" ? 0 : 1;

            // Champs optionnels (nullable en base)
            txtChargesImputables.Text = reader.IsDBNull(reader.GetOrdinal("chargesimputables"))
                ? "" : reader.GetFloat("chargesimputables").ToString(System.Globalization.CultureInfo.InvariantCulture);
            txtChargesAnnuelles.Text = reader.IsDBNull(reader.GetOrdinal("chargeannuelles"))
                ? "" : reader.GetInt32("chargeannuelles").ToString();

            // Champs obligatoires
            txtNumeroFiscal.Text = reader.GetString("numerofiscal");
            txtEstimationConso.Text = reader.GetString("estimationconsommation");
            txtAnneeReference.Text = reader.GetString("anneereference");

            string classeDpe = reader.GetString("classeDPE");
            cbxClasseDPE.SelectedIndex = cbxClasseDPE.Items.IndexOf(classeDpe);
        }

        /// <summary>
        /// Gère le clic sur le bouton "Valider"
        /// </summary>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!ChampsRenseignes())
            {
                return;
            }

            try
            {
                if (this.estNouveau)
                {
                    InsererBien();
                }
                else
                {
                    MettreAJourBien();
                }

                this.leBien.RemplirLstBiens();
                this.Dispose();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Une erreur est survenue lors de l'enregistrement du bien : " + ex.Message);
            }
        }

        /// <summary>
        /// Insère un nouveau bien
        /// </summary>
        private void InsererBien()
        {
            const string req =
                "INSERT INTO bien (idbien, nombien, loyerhc, charges, loyercc, adressebien, cpbien, villebien, " +
                "bienarchive, typehabitat, regimejuridique, periodeconstruction, superficie, nbpiece, description, " +
                "elementequip, autre, prodchauff, prodeauchaude, chargesimputables, chargeannuelles, " +
                "numerofiscal, classeDPE, estimationconsommation, anneereference) " +
                "VALUES (@id, @nombien, @loyerhc, @charges, @loyercc, @adresse, @cp, @laville, @archive, " +
                "@typehabitat, @regime, @periode, @superficie, @nbpiece, @description, @eltequipement, @autre, " +
                "@prodchauff, @prodeauchaude, @chargesimputables, @chargeannuelles, @numerofiscal, @classeDpe, " +
                "@estimconso, @anneeref)";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", this.id);
            AjouterParametresCommuns(command);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Met à jour un bien existant
        /// </summary>
        private void MettreAJourBien()
        {
            const string req =
                "UPDATE bien SET nombien = @nombien, loyerhc = @loyerhc, charges = @charges, loyercc = @loyercc, " +
                "adressebien = @adresse, cpbien = @cp, villebien = @laville, bienarchive = @archive, " +
                "typehabitat = @typehabitat, regimejuridique = @regime, periodeconstruction = @periode, " +
                "superficie = @superficie, nbpiece = @nbpiece, description = @description, " +
                "elementequip = @eltequipement, autre = @autre, prodchauff = @prodchauff, " +
                "prodeauchaude = @prodeauchaude, chargesimputables = @chargesimputables, " +
                "chargeannuelles = @chargeannuelles, numerofiscal = @numerofiscal, classeDPE = @classeDpe, " +
                "estimationconsommation = @estimconso, anneereference = @anneeref " +
                "WHERE idbien = @id";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", this.id);
            AjouterParametresCommuns(command);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Ajoute les paramètres communs à l'ajout et à la modification
        /// </summary>
        private void AjouterParametresCommuns(MySqlCommand command)
        {
            command.Parameters.AddWithValue("@nombien", Global.Capitalize(txtNom.Text));
            command.Parameters.AddWithValue("@loyerhc", txtLoyerHC.Text.Replace(',', '.'));
            command.Parameters.AddWithValue("@charges", txtCharges.Text.Replace(',', '.'));
            command.Parameters.AddWithValue("@loyercc", txtLoyerCC.Text.Replace(',', '.'));
            command.Parameters.AddWithValue("@adresse", txtAdresse.Text);
            command.Parameters.AddWithValue("@cp", txtCp.Text);
            command.Parameters.AddWithValue("@laville", txtVille.Text.ToUpper());
            command.Parameters.AddWithValue("@archive", cbxArchive.Checked);
            command.Parameters.AddWithValue("@typehabitat", cbxTypeHabitat.SelectedItem.ToString());
            command.Parameters.AddWithValue("@regime", cbxRegimeJuri.SelectedItem.ToString());
            command.Parameters.AddWithValue("@periode", txtPerConstruc.Text);
            command.Parameters.AddWithValue("@superficie", txtSuperficie.Text.Replace(',', '.'));
            command.Parameters.AddWithValue("@nbpiece", txtNbPiece.Text);
            command.Parameters.AddWithValue("@description", txtDescriLogement.Text);
            command.Parameters.AddWithValue("@eltequipement", txtElemEquip.Text);
            command.Parameters.AddWithValue("@autre", txtAutre.Text);
            command.Parameters.AddWithValue("@prodchauff", cbxProdChauff.SelectedItem.ToString());
            command.Parameters.AddWithValue("@prodeauchaude", cbxProdEauChaude.SelectedItem.ToString());

            // Champs optionnels : chaîne vide -> DBNull (colonnes nullable en base)
            command.Parameters.AddWithValue("@chargesimputables",
                string.IsNullOrWhiteSpace(txtChargesImputables.Text)
                    ? (object)DBNull.Value
                    : float.Parse(txtChargesImputables.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture));

            command.Parameters.AddWithValue("@chargeannuelles",
                string.IsNullOrWhiteSpace(txtChargesAnnuelles.Text)
                    ? (object)DBNull.Value
                    : int.Parse(txtChargesAnnuelles.Text));

            // Champs obligatoires
            command.Parameters.AddWithValue("@numerofiscal", txtNumeroFiscal.Text);
            command.Parameters.AddWithValue("@classeDpe", cbxClasseDPE.SelectedItem.ToString());
            command.Parameters.AddWithValue("@estimconso", txtEstimationConso.Text);
            command.Parameters.AddWithValue("@anneeref", txtAnneeReference.Text);
        }

        /// <summary>
        /// Vérifie si tous les champs ont été renseignés et sont valides
        /// </summary>
        private bool ChampsRenseignes()
        {
            if (txtNom.Text.Equals("") || txtLoyerHC.Text.Equals("") || txtCharges.Text.Equals("") || txtLoyerCC.Text.Equals("")
                || txtAdresse.Text.Equals("") || txtCp.Text.Equals("") || txtVille.Text.Equals("")
                || txtSuperficie.Text.Equals("") || txtNbPiece.Text.Equals("")
                || txtNumeroFiscal.Text.Equals("") || cbxClasseDPE.SelectedItem == null
                || txtEstimationConso.Text.Equals("") || txtAnneeReference.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires pour pouvoir valider la saisie.");
                return false;
            }

            // Champs optionnels : si renseignés, doivent être dans un format valide
            if (!string.IsNullOrWhiteSpace(txtChargesImputables.Text) &&
                !float.TryParse(txtChargesImputables.Text.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("Erreur de saisie pour les charges imputables.");
                txtChargesImputables.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtChargesAnnuelles.Text) && !int.TryParse(txtChargesAnnuelles.Text, out _))
            {
                MessageBox.Show("Erreur de saisie pour les charges annuelles.");
                txtChargesAnnuelles.Focus();
                return false;
            }

            if (!float.TryParse(txtLoyerHC.Text.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("Erreur de saisie pour le montant du loyer hors charges.");
                txtLoyerHC.Focus();
                return false;
            }

            if (!float.TryParse(txtCharges.Text.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("Erreur de saisie pour le montant des charges.");
                txtCharges.Focus();
                return false;
            }

            if (!float.TryParse(txtSuperficie.Text.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("Erreur de saisie pour la superficie.");
                txtSuperficie.Focus();
                return false;
            }

            if (!int.TryParse(txtNbPiece.Text, out _))
            {
                MessageBox.Show("Erreur de saisie pour le nombre de pièces.");
                txtNbPiece.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Modifie le montant du loyer CC à chaque modification du loyer HC
        /// </summary>
        private void TxtLoyerHC_TextChanged(object sender, EventArgs e)
        {
            RecalculeLoyerCC();
        }

        /// <summary>
        /// Recalcule le montant du loyer CC en fonction du contenu des champs LoyerHc et Charges
        /// </summary>
        private void RecalculeLoyerCC()
        {
            float.TryParse(txtLoyerHC.Text.Replace(',', '.'),
                System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out float loyerHc);
            float.TryParse(txtCharges.Text.Replace(',', '.'),
                System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out float charges);
            txtLoyerCC.Text = (loyerHc + charges).ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Modifie le montant du loyer CC à chaque modification des charges
        /// </summary>
        private void TxtCharges_TextChanged(object sender, EventArgs e)
        {
            RecalculeLoyerCC();
        }
    }
}