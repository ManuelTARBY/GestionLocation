
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
            this.txtBien = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(119, 21);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(38, 20);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID : ";
            // 
            // lblBien
            // 
            this.lblBien.AutoSize = true;
            this.lblBien.Location = new System.Drawing.Point(119, 59);
            this.lblBien.Name = "lblBien";
            this.lblBien.Size = new System.Drawing.Size(41, 20);
            this.lblBien.TabIndex = 2;
            this.lblBien.Text = "Bien";
            // 
            // lblMontant
            // 
            this.lblMontant.AutoSize = true;
            this.lblMontant.Location = new System.Drawing.Point(92, 143);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(68, 20);
            this.lblMontant.TabIndex = 3;
            this.lblMontant.Text = "Montant";
            // 
            // lblLibelle
            // 
            this.lblLibelle.AutoSize = true;
            this.lblLibelle.Location = new System.Drawing.Point(103, 102);
            this.lblLibelle.Name = "lblLibelle";
            this.lblLibelle.Size = new System.Drawing.Size(54, 20);
            this.lblLibelle.TabIndex = 4;
            this.lblLibelle.Text = "Libellé";
            // 
            // txtLibelle
            // 
            this.txtLibelle.Location = new System.Drawing.Point(179, 99);
            this.txtLibelle.Multiline = true;
            this.txtLibelle.Name = "txtLibelle";
            this.txtLibelle.Size = new System.Drawing.Size(200, 25);
            this.txtLibelle.TabIndex = 5;
            // 
            // txtMontant
            // 
            this.txtMontant.Location = new System.Drawing.Point(179, 140);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Size = new System.Drawing.Size(200, 26);
            this.txtMontant.TabIndex = 6;
            // 
            // lblFrequence
            // 
            this.lblFrequence.AutoSize = true;
            this.lblFrequence.Location = new System.Drawing.Point(74, 185);
            this.lblFrequence.Name = "lblFrequence";
            this.lblFrequence.Size = new System.Drawing.Size(86, 20);
            this.lblFrequence.TabIndex = 7;
            this.lblFrequence.Text = "Fréquence";
            // 
            // cobFrequence
            // 
            this.cobFrequence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobFrequence.FormattingEnabled = true;
            this.cobFrequence.Location = new System.Drawing.Point(179, 182);
            this.cobFrequence.Name = "cobFrequence";
            this.cobFrequence.Size = new System.Drawing.Size(200, 28);
            this.cobFrequence.TabIndex = 8;
            // 
            // cbxImputable
            // 
            this.cbxImputable.AutoSize = true;
            this.cbxImputable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxImputable.Location = new System.Drawing.Point(39, 226);
            this.cbxImputable.Name = "cbxImputable";
            this.cbxImputable.Size = new System.Drawing.Size(160, 24);
            this.cbxImputable.TabIndex = 9;
            this.cbxImputable.Text = "Charge imputable";
            this.cbxImputable.UseVisualStyleBackColor = true;
            // 
            // btnValider
            // 
            this.btnValider.Location = new System.Drawing.Point(52, 282);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(146, 41);
            this.btnValider.TabIndex = 10;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(233, 282);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(146, 41);
            this.btnFermer.TabIndex = 11;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            // 
            // txtBien
            // 
            this.txtBien.Location = new System.Drawing.Point(179, 56);
            this.txtBien.Multiline = true;
            this.txtBien.Name = "txtBien";
            this.txtBien.ReadOnly = true;
            this.txtBien.Size = new System.Drawing.Size(200, 25);
            this.txtBien.TabIndex = 12;
            // 
            // AjoutModifChargeAnnuelle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 341);
            this.Controls.Add(this.txtBien);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.cbxImputable);
            this.Controls.Add(this.cobFrequence);
            this.Controls.Add(this.lblFrequence);
            this.Controls.Add(this.txtMontant);
            this.Controls.Add(this.txtLibelle);
            this.Controls.Add(this.lblLibelle);
            this.Controls.Add(this.lblMontant);
            this.Controls.Add(this.lblBien);
            this.Controls.Add(this.lblID);
            this.Name = "AjoutModifChargeAnnuelle";
            this.Text = "ChargeAnnuelle";
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox txtBien;
    }
}