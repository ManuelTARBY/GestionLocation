
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
            this.SuspendLayout();
            // 
            // btnValider
            // 
            this.btnValider.Location = new System.Drawing.Point(182, 339);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(244, 42);
            this.btnValider.TabIndex = 0;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(178, 36);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 20);
            this.lblID.TabIndex = 8;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(182, 59);
            this.txtNom.MaxLength = 50;
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(244, 26);
            this.txtNom.TabIndex = 1;
            // 
            // txtLoyerHC
            // 
            this.txtLoyerHC.Location = new System.Drawing.Point(182, 91);
            this.txtLoyerHC.Name = "txtLoyerHC";
            this.txtLoyerHC.Size = new System.Drawing.Size(244, 26);
            this.txtLoyerHC.TabIndex = 2;
            this.txtLoyerHC.TextChanged += new System.EventHandler(this.TxtLoyerHC_TextChanged);
            // 
            // txtCharges
            // 
            this.txtCharges.Location = new System.Drawing.Point(182, 123);
            this.txtCharges.Name = "txtCharges";
            this.txtCharges.Size = new System.Drawing.Size(244, 26);
            this.txtCharges.TabIndex = 3;
            this.txtCharges.TextChanged += new System.EventHandler(this.TxtCharges_TextChanged);
            // 
            // txtVille
            // 
            this.txtVille.Location = new System.Drawing.Point(182, 251);
            this.txtVille.MaxLength = 50;
            this.txtVille.Name = "txtVille";
            this.txtVille.Size = new System.Drawing.Size(244, 26);
            this.txtVille.TabIndex = 7;
            // 
            // txtCp
            // 
            this.txtCp.Location = new System.Drawing.Point(182, 219);
            this.txtCp.MaxLength = 5;
            this.txtCp.Name = "txtCp";
            this.txtCp.Size = new System.Drawing.Size(244, 26);
            this.txtCp.TabIndex = 6;
            // 
            // txtLoyerCC
            // 
            this.txtLoyerCC.Enabled = false;
            this.txtLoyerCC.Location = new System.Drawing.Point(182, 155);
            this.txtLoyerCC.Name = "txtLoyerCC";
            this.txtLoyerCC.Size = new System.Drawing.Size(244, 26);
            this.txtLoyerCC.TabIndex = 4;
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(182, 187);
            this.txtAdresse.MaxLength = 100;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(244, 26);
            this.txtAdresse.TabIndex = 5;
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(68, 59);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(98, 20);
            this.lblNom.TabIndex = 11;
            this.lblNom.Text = "Nom du bien";
            // 
            // lblAdresse
            // 
            this.lblAdresse.AutoSize = true;
            this.lblAdresse.Location = new System.Drawing.Point(68, 185);
            this.lblAdresse.Name = "lblAdresse";
            this.lblAdresse.Size = new System.Drawing.Size(68, 20);
            this.lblAdresse.TabIndex = 12;
            this.lblAdresse.Text = "Adresse";
            // 
            // lblCp
            // 
            this.lblCp.AutoSize = true;
            this.lblCp.Location = new System.Drawing.Point(68, 217);
            this.lblCp.Name = "lblCp";
            this.lblCp.Size = new System.Drawing.Size(30, 20);
            this.lblCp.TabIndex = 13;
            this.lblCp.Text = "CP";
            // 
            // lblVille
            // 
            this.lblVille.AutoSize = true;
            this.lblVille.Location = new System.Drawing.Point(68, 249);
            this.lblVille.Name = "lblVille";
            this.lblVille.Size = new System.Drawing.Size(38, 20);
            this.lblVille.TabIndex = 14;
            this.lblVille.Text = "Ville";
            // 
            // lblLoyerHC
            // 
            this.lblLoyerHC.AutoSize = true;
            this.lblLoyerHC.Location = new System.Drawing.Point(68, 89);
            this.lblLoyerHC.Name = "lblLoyerHC";
            this.lblLoyerHC.Size = new System.Drawing.Size(75, 20);
            this.lblLoyerHC.TabIndex = 15;
            this.lblLoyerHC.Text = "Loyer HC";
            // 
            // lblCharges
            // 
            this.lblCharges.AutoSize = true;
            this.lblCharges.Location = new System.Drawing.Point(68, 121);
            this.lblCharges.Name = "lblCharges";
            this.lblCharges.Size = new System.Drawing.Size(69, 20);
            this.lblCharges.TabIndex = 16;
            this.lblCharges.Text = "Charges";
            // 
            // lblLoyerCC
            // 
            this.lblLoyerCC.AutoSize = true;
            this.lblLoyerCC.Location = new System.Drawing.Point(68, 153);
            this.lblLoyerCC.Name = "lblLoyerCC";
            this.lblLoyerCC.Size = new System.Drawing.Size(74, 20);
            this.lblLoyerCC.TabIndex = 17;
            this.lblLoyerCC.Text = "Loyer CC";
            // 
            // lblArchive
            // 
            this.lblArchive.AutoSize = true;
            this.lblArchive.Location = new System.Drawing.Point(68, 283);
            this.lblArchive.Name = "lblArchive";
            this.lblArchive.Size = new System.Drawing.Size(61, 20);
            this.lblArchive.TabIndex = 19;
            this.lblArchive.Text = "Archivé";
            // 
            // cbxArchive
            // 
            this.cbxArchive.AutoSize = true;
            this.cbxArchive.Location = new System.Drawing.Point(182, 283);
            this.cbxArchive.Name = "cbxArchive";
            this.cbxArchive.Size = new System.Drawing.Size(22, 21);
            this.cbxArchive.TabIndex = 18;
            this.cbxArchive.UseVisualStyleBackColor = true;
            // 
            // AjoutModifBiens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 444);
            this.Controls.Add(this.lblArchive);
            this.Controls.Add(this.cbxArchive);
            this.Controls.Add(this.lblLoyerCC);
            this.Controls.Add(this.lblCharges);
            this.Controls.Add(this.lblLoyerHC);
            this.Controls.Add(this.lblVille);
            this.Controls.Add(this.lblCp);
            this.Controls.Add(this.lblAdresse);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtAdresse);
            this.Controls.Add(this.txtLoyerCC);
            this.Controls.Add(this.txtVille);
            this.Controls.Add(this.txtCp);
            this.Controls.Add(this.txtCharges);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtLoyerHC);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.btnValider);
            this.Name = "AjoutModifBiens";
            this.Text = "AjoutModifBiens";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}