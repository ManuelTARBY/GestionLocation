
using System.Windows.Forms;

namespace GestionLocation
{
    partial class AjoutModifBiens
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AjoutModifBiens));
            this.btnValider = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtLoyerHC = new System.Windows.Forms.TextBox();
            this.txtCharges = new System.Windows.Forms.TextBox();
            this.txtVille = new System.Windows.Forms.TextBox();
            this.txtCp = new System.Windows.Forms.TextBox();
            this.txtLoyerCC = new System.Windows.Forms.TextBox();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblAdresse = new System.Windows.Forms.Label();
            this.lblCp = new System.Windows.Forms.Label();
            this.lblVille = new System.Windows.Forms.Label();
            this.lblLoyerHC = new System.Windows.Forms.Label();
            this.lblCharges = new System.Windows.Forms.Label();
            this.lblLoyerCC = new System.Windows.Forms.Label();
            this.lblArchive = new System.Windows.Forms.Label();
            this.cbxArchive = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtNbPiece = new System.Windows.Forms.TextBox();
            this.lblNbPiece = new System.Windows.Forms.Label();
            this.txtSuperficie = new System.Windows.Forms.TextBox();
            this.lblSuperficie = new System.Windows.Forms.Label();
            this.lblPeriodConstruc = new System.Windows.Forms.Label();
            this.txtPerConstruc = new System.Windows.Forms.TextBox();
            this.lblTypeHabitat = new System.Windows.Forms.Label();
            this.cbxTypeHabitat = new System.Windows.Forms.ComboBox();
            this.lblRegimeJuridique = new System.Windows.Forms.Label();
            this.lblProdEauChaude = new System.Windows.Forms.Label();
            this.lblProdChauff = new System.Windows.Forms.Label();
            this.cbxRegimeJuri = new System.Windows.Forms.ComboBox();
            this.cbxProdChauff = new System.Windows.Forms.ComboBox();
            this.cbxProdEauChaude = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblEleEquip = new System.Windows.Forms.Label();
            this.lblAutre = new System.Windows.Forms.Label();
            this.txtDescriLogement = new System.Windows.Forms.TextBox();
            this.txtAutre = new System.Windows.Forms.TextBox();
            this.txtElemEquip = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAnneeDeReference = new System.Windows.Forms.Label();
            this.txtAnneeReference = new System.Windows.Forms.TextBox();
            this.txtEstimationConso = new System.Windows.Forms.TextBox();
            this.lblEstimationConsommation = new System.Windows.Forms.Label();
            this.lblClasseDPE = new System.Windows.Forms.Label();
            this.lblNumeroFiscal = new System.Windows.Forms.Label();
            this.cbxClasseDPE = new System.Windows.Forms.ComboBox();
            this.txtNumeroFiscal = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.Location = new System.Drawing.Point(18, 639);
            this.btnValider.Margin = new System.Windows.Forms.Padding(18);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(449, 60);
            this.btnValider.TabIndex = 19;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(136, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 20);
            this.lblID.TabIndex = 8;
            this.lblID.Visible = false;
            // 
            // txtNom
            // 
            this.txtNom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNom.Location = new System.Drawing.Point(136, 93);
            this.txtNom.MaxLength = 50;
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(223, 26);
            this.txtNom.TabIndex = 1;
            // 
            // txtLoyerHC
            // 
            this.txtLoyerHC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLoyerHC.Location = new System.Drawing.Point(136, 164);
            this.txtLoyerHC.Name = "txtLoyerHC";
            this.txtLoyerHC.Size = new System.Drawing.Size(103, 26);
            this.txtLoyerHC.TabIndex = 2;
            this.txtLoyerHC.TextChanged += new System.EventHandler(this.TxtLoyerHC_TextChanged);
            // 
            // txtCharges
            // 
            this.txtCharges.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCharges.Location = new System.Drawing.Point(136, 235);
            this.txtCharges.Name = "txtCharges";
            this.txtCharges.Size = new System.Drawing.Size(103, 26);
            this.txtCharges.TabIndex = 3;
            this.txtCharges.TextChanged += new System.EventHandler(this.TxtCharges_TextChanged);
            // 
            // txtVille
            // 
            this.txtVille.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVille.Location = new System.Drawing.Point(136, 519);
            this.txtVille.MaxLength = 50;
            this.txtVille.Name = "txtVille";
            this.txtVille.Size = new System.Drawing.Size(223, 26);
            this.txtVille.TabIndex = 7;
            // 
            // txtCp
            // 
            this.txtCp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCp.Location = new System.Drawing.Point(136, 448);
            this.txtCp.MaxLength = 5;
            this.txtCp.Name = "txtCp";
            this.txtCp.Size = new System.Drawing.Size(103, 26);
            this.txtCp.TabIndex = 6;
            // 
            // txtLoyerCC
            // 
            this.txtLoyerCC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLoyerCC.Enabled = false;
            this.txtLoyerCC.Location = new System.Drawing.Point(136, 306);
            this.txtLoyerCC.Name = "txtLoyerCC";
            this.txtLoyerCC.Size = new System.Drawing.Size(103, 26);
            this.txtLoyerCC.TabIndex = 4;
            // 
            // txtAdresse
            // 
            this.txtAdresse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdresse.Location = new System.Drawing.Point(136, 377);
            this.txtAdresse.MaxLength = 100;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(223, 26);
            this.txtAdresse.TabIndex = 5;
            // 
            // lblNom
            // 
            this.lblNom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(32, 96);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(98, 20);
            this.lblNom.TabIndex = 11;
            this.lblNom.Text = "Nom du bien";
            // 
            // lblAdresse
            // 
            this.lblAdresse.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAdresse.AutoSize = true;
            this.lblAdresse.Location = new System.Drawing.Point(62, 380);
            this.lblAdresse.Name = "lblAdresse";
            this.lblAdresse.Size = new System.Drawing.Size(68, 20);
            this.lblAdresse.TabIndex = 12;
            this.lblAdresse.Text = "Adresse";
            // 
            // lblCp
            // 
            this.lblCp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCp.AutoSize = true;
            this.lblCp.Location = new System.Drawing.Point(100, 451);
            this.lblCp.Name = "lblCp";
            this.lblCp.Size = new System.Drawing.Size(30, 20);
            this.lblCp.TabIndex = 13;
            this.lblCp.Text = "CP";
            // 
            // lblVille
            // 
            this.lblVille.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVille.AutoSize = true;
            this.lblVille.Location = new System.Drawing.Point(92, 522);
            this.lblVille.Name = "lblVille";
            this.lblVille.Size = new System.Drawing.Size(38, 20);
            this.lblVille.TabIndex = 14;
            this.lblVille.Text = "Ville";
            // 
            // lblLoyerHC
            // 
            this.lblLoyerHC.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLoyerHC.AutoSize = true;
            this.lblLoyerHC.Location = new System.Drawing.Point(55, 167);
            this.lblLoyerHC.Name = "lblLoyerHC";
            this.lblLoyerHC.Size = new System.Drawing.Size(75, 20);
            this.lblLoyerHC.TabIndex = 15;
            this.lblLoyerHC.Text = "Loyer HC";
            // 
            // lblCharges
            // 
            this.lblCharges.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCharges.AutoSize = true;
            this.lblCharges.Location = new System.Drawing.Point(61, 238);
            this.lblCharges.Name = "lblCharges";
            this.lblCharges.Size = new System.Drawing.Size(69, 20);
            this.lblCharges.TabIndex = 16;
            this.lblCharges.Text = "Charges";
            // 
            // lblLoyerCC
            // 
            this.lblLoyerCC.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLoyerCC.AutoSize = true;
            this.lblLoyerCC.Location = new System.Drawing.Point(56, 309);
            this.lblLoyerCC.Name = "lblLoyerCC";
            this.lblLoyerCC.Size = new System.Drawing.Size(74, 20);
            this.lblLoyerCC.TabIndex = 17;
            this.lblLoyerCC.Text = "Loyer CC";
            // 
            // lblArchive
            // 
            this.lblArchive.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblArchive.AutoSize = true;
            this.lblArchive.Location = new System.Drawing.Point(69, 668);
            this.lblArchive.Name = "lblArchive";
            this.lblArchive.Size = new System.Drawing.Size(61, 20);
            this.lblArchive.TabIndex = 19;
            this.lblArchive.Text = "Archivé";
            // 
            // cbxArchive
            // 
            this.cbxArchive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxArchive.AutoSize = true;
            this.cbxArchive.Location = new System.Drawing.Point(136, 667);
            this.cbxArchive.Name = "cbxArchive";
            this.cbxArchive.Size = new System.Drawing.Size(22, 21);
            this.cbxArchive.TabIndex = 15;
            this.cbxArchive.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63F));
            this.tableLayoutPanel1.Controls.Add(this.txtNbPiece, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblNbPiece, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLoyerCC, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblCharges, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCp, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblLoyerHC, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblNom, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAdresse, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtCharges, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtNom, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtLoyerHC, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtLoyerCC, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtAdresse, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblVille, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtVille, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblCp, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtSuperficie, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblSuperficie, 0, 9);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(362, 717);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // txtNbPiece
            // 
            this.txtNbPiece.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNbPiece.Location = new System.Drawing.Point(136, 590);
            this.txtNbPiece.Name = "txtNbPiece";
            this.txtNbPiece.Size = new System.Drawing.Size(100, 26);
            this.txtNbPiece.TabIndex = 12;
            // 
            // lblNbPiece
            // 
            this.lblNbPiece.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNbPiece.AutoSize = true;
            this.lblNbPiece.Location = new System.Drawing.Point(39, 583);
            this.lblNbPiece.Name = "lblNbPiece";
            this.lblNbPiece.Size = new System.Drawing.Size(91, 40);
            this.lblNbPiece.TabIndex = 25;
            this.lblNbPiece.Text = "Nombre de pièce";
            // 
            // txtSuperficie
            // 
            this.txtSuperficie.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSuperficie.Location = new System.Drawing.Point(136, 665);
            this.txtSuperficie.MaxLength = 100;
            this.txtSuperficie.Name = "txtSuperficie";
            this.txtSuperficie.Size = new System.Drawing.Size(100, 26);
            this.txtSuperficie.TabIndex = 11;
            // 
            // lblSuperficie
            // 
            this.lblSuperficie.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSuperficie.AutoSize = true;
            this.lblSuperficie.Location = new System.Drawing.Point(18, 668);
            this.lblSuperficie.Name = "lblSuperficie";
            this.lblSuperficie.Size = new System.Drawing.Size(112, 20);
            this.lblSuperficie.TabIndex = 24;
            this.lblSuperficie.Text = "Superficie (m²)";
            // 
            // lblPeriodConstruc
            // 
            this.lblPeriodConstruc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPeriodConstruc.AutoSize = true;
            this.lblPeriodConstruc.Location = new System.Drawing.Point(34, 15);
            this.lblPeriodConstruc.Name = "lblPeriodConstruc";
            this.lblPeriodConstruc.Size = new System.Drawing.Size(96, 40);
            this.lblPeriodConstruc.TabIndex = 23;
            this.lblPeriodConstruc.Text = "Période de construction";
            // 
            // txtPerConstruc
            // 
            this.txtPerConstruc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPerConstruc.Location = new System.Drawing.Point(136, 22);
            this.txtPerConstruc.MaxLength = 4;
            this.txtPerConstruc.Name = "txtPerConstruc";
            this.txtPerConstruc.Size = new System.Drawing.Size(103, 26);
            this.txtPerConstruc.TabIndex = 10;
            // 
            // lblTypeHabitat
            // 
            this.lblTypeHabitat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTypeHabitat.AutoSize = true;
            this.lblTypeHabitat.Location = new System.Drawing.Point(22, 238);
            this.lblTypeHabitat.Name = "lblTypeHabitat";
            this.lblTypeHabitat.Size = new System.Drawing.Size(108, 20);
            this.lblTypeHabitat.TabIndex = 20;
            this.lblTypeHabitat.Text = "Type d\'habitat";
            // 
            // cbxTypeHabitat
            // 
            this.cbxTypeHabitat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTypeHabitat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTypeHabitat.FormattingEnabled = true;
            this.cbxTypeHabitat.Location = new System.Drawing.Point(136, 234);
            this.cbxTypeHabitat.Name = "cbxTypeHabitat";
            this.cbxTypeHabitat.Size = new System.Drawing.Size(223, 28);
            this.cbxTypeHabitat.TabIndex = 8;
            // 
            // lblRegimeJuridique
            // 
            this.lblRegimeJuridique.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRegimeJuridique.AutoSize = true;
            this.lblRegimeJuridique.Location = new System.Drawing.Point(3, 309);
            this.lblRegimeJuridique.Name = "lblRegimeJuridique";
            this.lblRegimeJuridique.Size = new System.Drawing.Size(127, 20);
            this.lblRegimeJuridique.TabIndex = 22;
            this.lblRegimeJuridique.Text = "Régime juridique";
            // 
            // lblProdEauChaude
            // 
            this.lblProdEauChaude.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProdEauChaude.AutoSize = true;
            this.lblProdEauChaude.Location = new System.Drawing.Point(3, 147);
            this.lblProdEauChaude.Name = "lblProdEauChaude";
            this.lblProdEauChaude.Size = new System.Drawing.Size(127, 60);
            this.lblProdEauChaude.TabIndex = 27;
            this.lblProdEauChaude.Text = "Mode de production d\'eau chaude";
            this.lblProdEauChaude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProdChauff
            // 
            this.lblProdChauff.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProdChauff.AutoSize = true;
            this.lblProdChauff.Location = new System.Drawing.Point(24, 76);
            this.lblProdChauff.Name = "lblProdChauff";
            this.lblProdChauff.Size = new System.Drawing.Size(106, 60);
            this.lblProdChauff.TabIndex = 30;
            this.lblProdChauff.Text = "Mode de production de chauffage";
            this.lblProdChauff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxRegimeJuri
            // 
            this.cbxRegimeJuri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxRegimeJuri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRegimeJuri.FormattingEnabled = true;
            this.cbxRegimeJuri.Location = new System.Drawing.Point(136, 305);
            this.cbxRegimeJuri.Name = "cbxRegimeJuri";
            this.cbxRegimeJuri.Size = new System.Drawing.Size(223, 28);
            this.cbxRegimeJuri.TabIndex = 9;
            // 
            // cbxProdChauff
            // 
            this.cbxProdChauff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxProdChauff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProdChauff.FormattingEnabled = true;
            this.cbxProdChauff.Location = new System.Drawing.Point(136, 92);
            this.cbxProdChauff.Name = "cbxProdChauff";
            this.cbxProdChauff.Size = new System.Drawing.Size(223, 28);
            this.cbxProdChauff.TabIndex = 13;
            // 
            // cbxProdEauChaude
            // 
            this.cbxProdEauChaude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxProdEauChaude.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProdEauChaude.FormattingEnabled = true;
            this.cbxProdEauChaude.Location = new System.Drawing.Point(136, 163);
            this.cbxProdEauChaude.Name = "cbxProdEauChaude";
            this.cbxProdEauChaude.Size = new System.Drawing.Size(223, 28);
            this.cbxProdEauChaude.TabIndex = 14;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 15);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(181, 20);
            this.lblDescription.TabIndex = 26;
            this.lblDescription.Text = "Description du logement";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1227, 723);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblDescription, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnValider, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.lblEleEquip, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblAutre, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.txtDescriLogement, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtAutre, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.txtElemEquip, 0, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(739, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(485, 717);
            this.tableLayoutPanel3.TabIndex = 21;
            // 
            // lblEleEquip
            // 
            this.lblEleEquip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEleEquip.AutoSize = true;
            this.lblEleEquip.Location = new System.Drawing.Point(3, 224);
            this.lblEleEquip.Name = "lblEleEquip";
            this.lblEleEquip.Size = new System.Drawing.Size(470, 40);
            this.lblEleEquip.TabIndex = 27;
            this.lblEleEquip.Text = "Eléments d\'équipement (cuisine équipée, détails des installations sanitaires)";
            // 
            // lblAutre
            // 
            this.lblAutre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAutre.AutoSize = true;
            this.lblAutre.Location = new System.Drawing.Point(3, 429);
            this.lblAutre.Name = "lblAutre";
            this.lblAutre.Size = new System.Drawing.Size(248, 20);
            this.lblAutre.TabIndex = 28;
            this.lblAutre.Text = "Autre (cave, grenier, terrasse etc.)";
            // 
            // txtDescriLogement
            // 
            this.txtDescriLogement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescriLogement.Location = new System.Drawing.Point(3, 38);
            this.txtDescriLogement.Multiline = true;
            this.txtDescriLogement.Name = "txtDescriLogement";
            this.txtDescriLogement.Size = new System.Drawing.Size(479, 166);
            this.txtDescriLogement.TabIndex = 16;
            // 
            // txtAutre
            // 
            this.txtAutre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAutre.Location = new System.Drawing.Point(3, 452);
            this.txtAutre.Multiline = true;
            this.txtAutre.Name = "txtAutre";
            this.txtAutre.Size = new System.Drawing.Size(479, 166);
            this.txtAutre.TabIndex = 18;
            // 
            // txtElemEquip
            // 
            this.txtElemEquip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtElemEquip.Location = new System.Drawing.Point(3, 267);
            this.txtElemEquip.Multiline = true;
            this.txtElemEquip.Name = "txtElemEquip";
            this.txtElemEquip.Size = new System.Drawing.Size(479, 144);
            this.txtElemEquip.TabIndex = 17;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63F));
            this.tableLayoutPanel4.Controls.Add(this.lblAnneeDeReference, 0, 8);
            this.tableLayoutPanel4.Controls.Add(this.txtAnneeReference, 1, 8);
            this.tableLayoutPanel4.Controls.Add(this.txtEstimationConso, 1, 7);
            this.tableLayoutPanel4.Controls.Add(this.lblArchive, 0, 9);
            this.tableLayoutPanel4.Controls.Add(this.cbxArchive, 1, 9);
            this.tableLayoutPanel4.Controls.Add(this.lblEstimationConsommation, 0, 7);
            this.tableLayoutPanel4.Controls.Add(this.lblClasseDPE, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.lblNumeroFiscal, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.lblRegimeJuridique, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.lblTypeHabitat, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.lblProdEauChaude, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.lblProdChauff, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.cbxRegimeJuri, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.cbxTypeHabitat, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.cbxProdEauChaude, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.cbxProdChauff, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblPeriodConstruc, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtPerConstruc, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbxClasseDPE, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.txtNumeroFiscal, 1, 5);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(371, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 10;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(362, 717);
            this.tableLayoutPanel4.TabIndex = 22;
            // 
            // lblAnneeDeReference
            // 
            this.lblAnneeDeReference.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAnneeDeReference.Location = new System.Drawing.Point(3, 583);
            this.lblAnneeDeReference.Name = "lblAnneeDeReference";
            this.lblAnneeDeReference.Size = new System.Drawing.Size(127, 40);
            this.lblAnneeDeReference.TabIndex = 39;
            this.lblAnneeDeReference.Text = "Année(s) de référence";
            this.lblAnneeDeReference.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAnneeReference
            // 
            this.txtAnneeReference.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAnneeReference.Location = new System.Drawing.Point(136, 590);
            this.txtAnneeReference.Name = "txtAnneeReference";
            this.txtAnneeReference.Size = new System.Drawing.Size(223, 26);
            this.txtAnneeReference.TabIndex = 38;
            // 
            // txtEstimationConso
            // 
            this.txtEstimationConso.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtEstimationConso.Location = new System.Drawing.Point(136, 519);
            this.txtEstimationConso.Name = "txtEstimationConso";
            this.txtEstimationConso.Size = new System.Drawing.Size(223, 26);
            this.txtEstimationConso.TabIndex = 37;
            // 
            // lblEstimationConsommation
            // 
            this.lblEstimationConsommation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEstimationConsommation.AutoSize = true;
            this.lblEstimationConsommation.Location = new System.Drawing.Point(17, 512);
            this.lblEstimationConsommation.Name = "lblEstimationConsommation";
            this.lblEstimationConsommation.Size = new System.Drawing.Size(113, 40);
            this.lblEstimationConsommation.TabIndex = 33;
            this.lblEstimationConsommation.Text = "Estimation consommation";
            this.lblEstimationConsommation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClasseDPE
            // 
            this.lblClasseDPE.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblClasseDPE.AutoSize = true;
            this.lblClasseDPE.Location = new System.Drawing.Point(36, 451);
            this.lblClasseDPE.Name = "lblClasseDPE";
            this.lblClasseDPE.Size = new System.Drawing.Size(94, 20);
            this.lblClasseDPE.TabIndex = 32;
            this.lblClasseDPE.Text = "Classe DPE";
            this.lblClasseDPE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNumeroFiscal
            // 
            this.lblNumeroFiscal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNumeroFiscal.AutoSize = true;
            this.lblNumeroFiscal.Location = new System.Drawing.Point(25, 380);
            this.lblNumeroFiscal.Name = "lblNumeroFiscal";
            this.lblNumeroFiscal.Size = new System.Drawing.Size(105, 20);
            this.lblNumeroFiscal.TabIndex = 31;
            this.lblNumeroFiscal.Text = "Numéro fiscal";
            this.lblNumeroFiscal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxClasseDPE
            // 
            this.cbxClasseDPE.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxClasseDPE.FormattingEnabled = true;
            this.cbxClasseDPE.Location = new System.Drawing.Point(136, 447);
            this.cbxClasseDPE.Name = "cbxClasseDPE";
            this.cbxClasseDPE.Size = new System.Drawing.Size(121, 28);
            this.cbxClasseDPE.TabIndex = 35;
            // 
            // txtNumeroFiscal
            // 
            this.txtNumeroFiscal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNumeroFiscal.Location = new System.Drawing.Point(136, 377);
            this.txtNumeroFiscal.Name = "txtNumeroFiscal";
            this.txtNumeroFiscal.Size = new System.Drawing.Size(223, 26);
            this.txtNumeroFiscal.TabIndex = 36;
            // 
            // AjoutModifBiens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 747);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AjoutModifBiens";
            this.Text = "AjoutModifBiens";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtLoyerHC;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtCharges;
        private System.Windows.Forms.TextBox txtCp;
        private System.Windows.Forms.TextBox txtVille;
        private System.Windows.Forms.TextBox txtLoyerCC;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblAdresse;
        private System.Windows.Forms.Label lblCp;
        private System.Windows.Forms.Label lblVille;
        private System.Windows.Forms.Label lblLoyerHC;
        private System.Windows.Forms.Label lblCharges;
        private System.Windows.Forms.Label lblLoyerCC;
        private System.Windows.Forms.Label lblArchive;
        private System.Windows.Forms.CheckBox cbxArchive;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTypeHabitat;
        private System.Windows.Forms.ComboBox cbxTypeHabitat;
        private System.Windows.Forms.Label lblRegimeJuridique;
        private System.Windows.Forms.Label lblPeriodConstruc;
        private System.Windows.Forms.Label lblSuperficie;
        private System.Windows.Forms.Label lblNbPiece;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblProdEauChaude;
        private System.Windows.Forms.Label lblProdChauff;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtPerConstruc;
        private System.Windows.Forms.ComboBox cbxRegimeJuri;
        private System.Windows.Forms.TextBox txtSuperficie;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblEleEquip;
        private System.Windows.Forms.Label lblAutre;
        private System.Windows.Forms.TextBox txtDescriLogement;
        private System.Windows.Forms.TextBox txtElemEquip;
        private System.Windows.Forms.TextBox txtAutre;
        private System.Windows.Forms.TextBox txtNbPiece;
        private System.Windows.Forms.ComboBox cbxProdChauff;
        private System.Windows.Forms.ComboBox cbxProdEauChaude;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblNumeroFiscal;
        private Label lblClasseDPE;
        private ComboBox cbxClasseDPE;
        private TextBox txtNumeroFiscal;
        private TextBox txtEstimationConso;
        private TextBox txtAnneeReference;
        private Label lblAnneeDeReference;
        private Label lblEstimationConsommation;
    }
}