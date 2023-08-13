
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnValider
            // 
            this.btnValider.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnValider.Location = new System.Drawing.Point(184, 351);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(244, 59);
            this.btnValider.TabIndex = 0;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(184, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 20);
            this.lblID.TabIndex = 8;
            // 
            // txtNom
            // 
            this.txtNom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNom.Location = new System.Drawing.Point(184, 41);
            this.txtNom.MaxLength = 50;
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(244, 26);
            this.txtNom.TabIndex = 1;
            // 
            // txtLoyerHC
            // 
            this.txtLoyerHC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLoyerHC.Location = new System.Drawing.Point(184, 82);
            this.txtLoyerHC.Name = "txtLoyerHC";
            this.txtLoyerHC.Size = new System.Drawing.Size(103, 26);
            this.txtLoyerHC.TabIndex = 2;
            this.txtLoyerHC.TextChanged += new System.EventHandler(this.TxtLoyerHC_TextChanged);
            // 
            // txtCharges
            // 
            this.txtCharges.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCharges.Location = new System.Drawing.Point(184, 120);
            this.txtCharges.Name = "txtCharges";
            this.txtCharges.Size = new System.Drawing.Size(103, 26);
            this.txtCharges.TabIndex = 3;
            this.txtCharges.TextChanged += new System.EventHandler(this.TxtCharges_TextChanged);
            // 
            // txtVille
            // 
            this.txtVille.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtVille.Location = new System.Drawing.Point(184, 272);
            this.txtVille.MaxLength = 50;
            this.txtVille.Name = "txtVille";
            this.txtVille.Size = new System.Drawing.Size(295, 26);
            this.txtVille.TabIndex = 7;
            // 
            // txtCp
            // 
            this.txtCp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCp.Location = new System.Drawing.Point(184, 234);
            this.txtCp.MaxLength = 5;
            this.txtCp.Name = "txtCp";
            this.txtCp.Size = new System.Drawing.Size(103, 26);
            this.txtCp.TabIndex = 6;
            // 
            // txtLoyerCC
            // 
            this.txtLoyerCC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLoyerCC.Enabled = false;
            this.txtLoyerCC.Location = new System.Drawing.Point(184, 158);
            this.txtLoyerCC.Name = "txtLoyerCC";
            this.txtLoyerCC.Size = new System.Drawing.Size(103, 26);
            this.txtLoyerCC.TabIndex = 4;
            // 
            // txtAdresse
            // 
            this.txtAdresse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAdresse.Location = new System.Drawing.Point(184, 196);
            this.txtAdresse.MaxLength = 100;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(295, 26);
            this.txtAdresse.TabIndex = 5;
            // 
            // lblNom
            // 
            this.lblNom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(80, 47);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(98, 20);
            this.lblNom.TabIndex = 11;
            this.lblNom.Text = "Nom du bien";
            // 
            // lblAdresse
            // 
            this.lblAdresse.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAdresse.AutoSize = true;
            this.lblAdresse.Location = new System.Drawing.Point(110, 199);
            this.lblAdresse.Name = "lblAdresse";
            this.lblAdresse.Size = new System.Drawing.Size(68, 20);
            this.lblAdresse.TabIndex = 12;
            this.lblAdresse.Text = "Adresse";
            // 
            // lblCp
            // 
            this.lblCp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCp.AutoSize = true;
            this.lblCp.Location = new System.Drawing.Point(148, 237);
            this.lblCp.Name = "lblCp";
            this.lblCp.Size = new System.Drawing.Size(30, 20);
            this.lblCp.TabIndex = 13;
            this.lblCp.Text = "CP";
            // 
            // lblVille
            // 
            this.lblVille.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVille.AutoSize = true;
            this.lblVille.Location = new System.Drawing.Point(140, 275);
            this.lblVille.Name = "lblVille";
            this.lblVille.Size = new System.Drawing.Size(38, 20);
            this.lblVille.TabIndex = 14;
            this.lblVille.Text = "Ville";
            // 
            // lblLoyerHC
            // 
            this.lblLoyerHC.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLoyerHC.AutoSize = true;
            this.lblLoyerHC.Location = new System.Drawing.Point(103, 85);
            this.lblLoyerHC.Name = "lblLoyerHC";
            this.lblLoyerHC.Size = new System.Drawing.Size(75, 20);
            this.lblLoyerHC.TabIndex = 15;
            this.lblLoyerHC.Text = "Loyer HC";
            // 
            // lblCharges
            // 
            this.lblCharges.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCharges.AutoSize = true;
            this.lblCharges.Location = new System.Drawing.Point(109, 123);
            this.lblCharges.Name = "lblCharges";
            this.lblCharges.Size = new System.Drawing.Size(69, 20);
            this.lblCharges.TabIndex = 16;
            this.lblCharges.Text = "Charges";
            // 
            // lblLoyerCC
            // 
            this.lblLoyerCC.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLoyerCC.AutoSize = true;
            this.lblLoyerCC.Location = new System.Drawing.Point(104, 161);
            this.lblLoyerCC.Name = "lblLoyerCC";
            this.lblLoyerCC.Size = new System.Drawing.Size(74, 20);
            this.lblLoyerCC.TabIndex = 17;
            this.lblLoyerCC.Text = "Loyer CC";
            // 
            // lblArchive
            // 
            this.lblArchive.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblArchive.AutoSize = true;
            this.lblArchive.Location = new System.Drawing.Point(117, 313);
            this.lblArchive.Name = "lblArchive";
            this.lblArchive.Size = new System.Drawing.Size(61, 20);
            this.lblArchive.TabIndex = 19;
            this.lblArchive.Text = "Archivé";
            // 
            // cbxArchive
            // 
            this.cbxArchive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxArchive.AutoSize = true;
            this.cbxArchive.Location = new System.Drawing.Point(184, 312);
            this.cbxArchive.Name = "cbxArchive";
            this.cbxArchive.Size = new System.Drawing.Size(22, 21);
            this.cbxArchive.TabIndex = 18;
            this.cbxArchive.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.btnValider, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLoyerCC, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblArchive, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblCharges, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCp, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblLoyerHC, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbxArchive, 1, 8);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(519, 420);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // AjoutModifBiens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 444);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AjoutModifBiens";
            this.Text = "AjoutModifBiens";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
    }
}