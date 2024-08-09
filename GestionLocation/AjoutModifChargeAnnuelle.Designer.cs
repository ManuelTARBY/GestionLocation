
namespace GestionLocation
{
    partial class AjoutModifChargeAnnuelle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AjoutModifChargeAnnuelle));
            this.lblID = new System.Windows.Forms.Label();
            this.lblBien = new System.Windows.Forms.Label();
            this.lblMontant = new System.Windows.Forms.Label();
            this.lblLibelle = new System.Windows.Forms.Label();
            this.txtLibelle = new System.Windows.Forms.TextBox();
            this.txtMontant = new System.Windows.Forms.TextBox();
            this.lblFrequence = new System.Windows.Forms.Label();
            this.cobFrequence = new System.Windows.Forms.ComboBox();
            this.cbxImputable = new System.Windows.Forms.CheckBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAnnee = new System.Windows.Forms.Label();
            this.txtAnnee = new System.Windows.Forms.TextBox();
            this.cobListeBien = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(158, 7);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(38, 20);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID : ";
            this.lblID.Visible = false;
            // 
            // lblBien
            // 
            this.lblBien.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblBien.AutoSize = true;
            this.lblBien.Location = new System.Drawing.Point(155, 43);
            this.lblBien.Name = "lblBien";
            this.lblBien.Size = new System.Drawing.Size(41, 20);
            this.lblBien.TabIndex = 2;
            this.lblBien.Text = "Bien";
            // 
            // lblMontant
            // 
            this.lblMontant.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMontant.AutoSize = true;
            this.lblMontant.Location = new System.Drawing.Point(128, 119);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(68, 20);
            this.lblMontant.TabIndex = 3;
            this.lblMontant.Text = "Montant";
            // 
            // lblLibelle
            // 
            this.lblLibelle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLibelle.AutoSize = true;
            this.lblLibelle.Location = new System.Drawing.Point(142, 81);
            this.lblLibelle.Name = "lblLibelle";
            this.lblLibelle.Size = new System.Drawing.Size(54, 20);
            this.lblLibelle.TabIndex = 4;
            this.lblLibelle.Text = "Libellé";
            // 
            // txtLibelle
            // 
            this.txtLibelle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLibelle.Location = new System.Drawing.Point(202, 78);
            this.txtLibelle.Multiline = true;
            this.txtLibelle.Name = "txtLibelle";
            this.txtLibelle.Size = new System.Drawing.Size(193, 25);
            this.txtLibelle.TabIndex = 2;
            // 
            // txtMontant
            // 
            this.txtMontant.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMontant.Location = new System.Drawing.Point(202, 116);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Size = new System.Drawing.Size(125, 26);
            this.txtMontant.TabIndex = 3;
            // 
            // lblFrequence
            // 
            this.lblFrequence.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFrequence.AutoSize = true;
            this.lblFrequence.Location = new System.Drawing.Point(110, 155);
            this.lblFrequence.Name = "lblFrequence";
            this.lblFrequence.Size = new System.Drawing.Size(86, 20);
            this.lblFrequence.TabIndex = 7;
            this.lblFrequence.Text = "Fréquence";
            // 
            // cobFrequence
            // 
            this.cobFrequence.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cobFrequence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobFrequence.FormattingEnabled = true;
            this.cobFrequence.Location = new System.Drawing.Point(202, 151);
            this.cobFrequence.Name = "cobFrequence";
            this.cobFrequence.Size = new System.Drawing.Size(193, 28);
            this.cobFrequence.TabIndex = 4;
            // 
            // cbxImputable
            // 
            this.cbxImputable.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbxImputable.AutoSize = true;
            this.cbxImputable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxImputable.Location = new System.Drawing.Point(36, 221);
            this.cbxImputable.Name = "cbxImputable";
            this.cbxImputable.Size = new System.Drawing.Size(160, 24);
            this.cbxImputable.TabIndex = 6;
            this.cbxImputable.Text = "Charge imputable";
            this.cbxImputable.UseVisualStyleBackColor = true;
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.Location = new System.Drawing.Point(71, 273);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(125, 41);
            this.btnValider.TabIndex = 7;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFermer.Location = new System.Drawing.Point(202, 273);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(125, 41);
            this.btnFermer.TabIndex = 8;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cobFrequence, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblFrequence, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtMontant, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblMontant, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblBien, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblLibelle, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtLibelle, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnValider, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnFermer, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.cbxImputable, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblAnnee, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtAnnee, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.cobListeBien, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(398, 317);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // lblAnnee
            // 
            this.lblAnnee.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAnnee.AutoSize = true;
            this.lblAnnee.Location = new System.Drawing.Point(140, 189);
            this.lblAnnee.Name = "lblAnnee";
            this.lblAnnee.Size = new System.Drawing.Size(56, 20);
            this.lblAnnee.TabIndex = 13;
            this.lblAnnee.Text = "Année";
            this.lblAnnee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAnnee
            // 
            this.txtAnnee.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAnnee.Location = new System.Drawing.Point(202, 186);
            this.txtAnnee.Name = "txtAnnee";
            this.txtAnnee.Size = new System.Drawing.Size(125, 26);
            this.txtAnnee.TabIndex = 5;
            // 
            // cobListeBien
            // 
            this.cobListeBien.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cobListeBien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobListeBien.FormattingEnabled = true;
            this.cobListeBien.Location = new System.Drawing.Point(202, 39);
            this.cobListeBien.Name = "cobListeBien";
            this.cobListeBien.Size = new System.Drawing.Size(193, 28);
            this.cobListeBien.TabIndex = 1;
            // 
            // AjoutModifChargeAnnuelle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 341);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AjoutModifChargeAnnuelle";
            this.Text = "ChargeAnnuelle";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblBien;
        private System.Windows.Forms.Label lblMontant;
        private System.Windows.Forms.Label lblLibelle;
        private System.Windows.Forms.TextBox txtLibelle;
        private System.Windows.Forms.TextBox txtMontant;
        private System.Windows.Forms.Label lblFrequence;
        private System.Windows.Forms.ComboBox cobFrequence;
        private System.Windows.Forms.CheckBox cbxImputable;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblAnnee;
        private System.Windows.Forms.TextBox txtAnnee;
        private System.Windows.Forms.ComboBox cobListeBien;
    }
}