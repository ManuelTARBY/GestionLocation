
namespace GestionLocation
{
    partial class AjoutModifLocations
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
            this.lstBiens = new System.Windows.Forms.ListBox();
            this.lstLocataires = new System.Windows.Forms.ListBox();
            this.lstCautions = new System.Windows.Forms.ListBox();
            this.datDebut = new System.Windows.Forms.DateTimePicker();
            this.datFin = new System.Windows.Forms.DateTimePicker();
            this.lblID = new System.Windows.Forms.Label();
            this.lblDebutLoc = new System.Windows.Forms.Label();
            this.lblFinLoc = new System.Windows.Forms.Label();
            this.txtDepotGarantie = new System.Windows.Forms.TextBox();
            this.lblDepotGarantie = new System.Windows.Forms.Label();
            this.cbxArchive = new System.Windows.Forms.CheckBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.lblBiens = new System.Windows.Forms.Label();
            this.lblLocataires = new System.Windows.Forms.Label();
            this.lblCautions = new System.Windows.Forms.Label();
            this.btnFermer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstBiens
            // 
            this.lstBiens.FormattingEnabled = true;
            this.lstBiens.ItemHeight = 20;
            this.lstBiens.Location = new System.Drawing.Point(42, 106);
            this.lstBiens.Name = "lstBiens";
            this.lstBiens.Size = new System.Drawing.Size(133, 124);
            this.lstBiens.TabIndex = 0;
            // 
            // lstLocataires
            // 
            this.lstLocataires.FormattingEnabled = true;
            this.lstLocataires.ItemHeight = 20;
            this.lstLocataires.Location = new System.Drawing.Point(206, 106);
            this.lstLocataires.Name = "lstLocataires";
            this.lstLocataires.Size = new System.Drawing.Size(273, 144);
            this.lstLocataires.TabIndex = 1;
            // 
            // lstCautions
            // 
            this.lstCautions.FormattingEnabled = true;
            this.lstCautions.ItemHeight = 20;
            this.lstCautions.Location = new System.Drawing.Point(509, 106);
            this.lstCautions.Name = "lstCautions";
            this.lstCautions.Size = new System.Drawing.Size(271, 144);
            this.lstCautions.TabIndex = 2;
            // 
            // datDebut
            // 
            this.datDebut.Location = new System.Drawing.Point(206, 47);
            this.datDebut.Name = "datDebut";
            this.datDebut.Size = new System.Drawing.Size(273, 26);
            this.datDebut.TabIndex = 3;
            // 
            // datFin
            // 
            this.datFin.Location = new System.Drawing.Point(509, 47);
            this.datFin.Name = "datFin";
            this.datFin.Size = new System.Drawing.Size(271, 26);
            this.datFin.TabIndex = 4;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(9, 7);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 20);
            this.lblID.TabIndex = 28;
            // 
            // lblDebutLoc
            // 
            this.lblDebutLoc.AutoSize = true;
            this.lblDebutLoc.Location = new System.Drawing.Point(202, 24);
            this.lblDebutLoc.Name = "lblDebutLoc";
            this.lblDebutLoc.Size = new System.Drawing.Size(134, 20);
            this.lblDebutLoc.TabIndex = 29;
            this.lblDebutLoc.Text = "Début de location";
            // 
            // lblFinLoc
            // 
            this.lblFinLoc.AutoSize = true;
            this.lblFinLoc.Location = new System.Drawing.Point(505, 24);
            this.lblFinLoc.Name = "lblFinLoc";
            this.lblFinLoc.Size = new System.Drawing.Size(112, 20);
            this.lblFinLoc.TabIndex = 30;
            this.lblFinLoc.Text = "Fin de location";
            // 
            // txtDepotGarantie
            // 
            this.txtDepotGarantie.Location = new System.Drawing.Point(42, 47);
            this.txtDepotGarantie.Name = "txtDepotGarantie";
            this.txtDepotGarantie.Size = new System.Drawing.Size(133, 26);
            this.txtDepotGarantie.TabIndex = 31;
            // 
            // lblDepotGarantie
            // 
            this.lblDepotGarantie.AutoSize = true;
            this.lblDepotGarantie.Location = new System.Drawing.Point(38, 24);
            this.lblDepotGarantie.Name = "lblDepotGarantie";
            this.lblDepotGarantie.Size = new System.Drawing.Size(137, 20);
            this.lblDepotGarantie.TabIndex = 32;
            this.lblDepotGarantie.Text = "Dépôt de garantie";
            // 
            // cbxArchive
            // 
            this.cbxArchive.AutoSize = true;
            this.cbxArchive.Location = new System.Drawing.Point(42, 236);
            this.cbxArchive.Name = "cbxArchive";
            this.cbxArchive.Size = new System.Drawing.Size(96, 24);
            this.cbxArchive.TabIndex = 33;
            this.cbxArchive.Text = "Archivée";
            this.cbxArchive.UseVisualStyleBackColor = true;
            // 
            // btnValider
            // 
            this.btnValider.Location = new System.Drawing.Point(266, 274);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(229, 53);
            this.btnValider.TabIndex = 34;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // lblBiens
            // 
            this.lblBiens.AutoSize = true;
            this.lblBiens.Location = new System.Drawing.Point(38, 85);
            this.lblBiens.Name = "lblBiens";
            this.lblBiens.Size = new System.Drawing.Size(49, 20);
            this.lblBiens.TabIndex = 35;
            this.lblBiens.Text = "Biens";
            // 
            // lblLocataires
            // 
            this.lblLocataires.AutoSize = true;
            this.lblLocataires.Location = new System.Drawing.Point(202, 85);
            this.lblLocataires.Name = "lblLocataires";
            this.lblLocataires.Size = new System.Drawing.Size(83, 20);
            this.lblLocataires.TabIndex = 36;
            this.lblLocataires.Text = "Locataires";
            // 
            // lblCautions
            // 
            this.lblCautions.AutoSize = true;
            this.lblCautions.Location = new System.Drawing.Point(505, 85);
            this.lblCautions.Name = "lblCautions";
            this.lblCautions.Size = new System.Drawing.Size(72, 20);
            this.lblCautions.TabIndex = 37;
            this.lblCautions.Text = "Cautions";
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(619, 274);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(161, 53);
            this.btnFermer.TabIndex = 38;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // AjoutModifLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 348);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.lblCautions);
            this.Controls.Add(this.lblLocataires);
            this.Controls.Add(this.lblBiens);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.cbxArchive);
            this.Controls.Add(this.lblDepotGarantie);
            this.Controls.Add(this.txtDepotGarantie);
            this.Controls.Add(this.lblFinLoc);
            this.Controls.Add(this.lblDebutLoc);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.datFin);
            this.Controls.Add(this.datDebut);
            this.Controls.Add(this.lstCautions);
            this.Controls.Add(this.lstLocataires);
            this.Controls.Add(this.lstBiens);
            this.Name = "AjoutModifLocations";
            this.Text = "Ajout / Modification d\'une location";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstBiens;
        private System.Windows.Forms.ListBox lstLocataires;
        private System.Windows.Forms.ListBox lstCautions;
        private System.Windows.Forms.DateTimePicker datDebut;
        private System.Windows.Forms.DateTimePicker datFin;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblDebutLoc;
        private System.Windows.Forms.Label lblFinLoc;
        private System.Windows.Forms.TextBox txtDepotGarantie;
        private System.Windows.Forms.Label lblDepotGarantie;
        private System.Windows.Forms.CheckBox cbxArchive;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Label lblBiens;
        private System.Windows.Forms.Label lblLocataires;
        private System.Windows.Forms.Label lblCautions;
        private System.Windows.Forms.Button btnFermer;
    }
}