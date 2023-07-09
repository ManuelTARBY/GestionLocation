
using System.Windows.Forms;

namespace GestionLocation
{
    partial class ModifPaiements
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
            this.lblLocation = new System.Windows.Forms.Label();
            this.datPaiement = new System.Windows.Forms.DateTimePicker();
            this.lblDatePaiement = new System.Windows.Forms.Label();
            this.txtMontantDu = new System.Windows.Forms.TextBox();
            this.lblMontantDu = new System.Windows.Forms.Label();
            this.lblMontantPaye = new System.Windows.Forms.Label();
            this.txtMontantPaye = new System.Windows.Forms.TextBox();
            this.lblResteAPayer = new System.Windows.Forms.Label();
            this.txtResteAPayer = new System.Windows.Forms.TextBox();
            this.cbxRegle = new System.Windows.Forms.CheckBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.grbChamps = new System.Windows.Forms.GroupBox();
            this.grbChamps.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLocation
            // 
            this.lblLocation.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(0, 0);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(3);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(577, 105);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Info location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // datPaiement
            // 
            this.datPaiement.CustomFormat = "dd MMMM yyyy";
            this.datPaiement.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datPaiement.Location = new System.Drawing.Point(231, 79);
            this.datPaiement.Name = "datPaiement";
            this.datPaiement.Size = new System.Drawing.Size(195, 26);
            this.datPaiement.TabIndex = 3;
            // 
            // lblDatePaiement
            // 
            this.lblDatePaiement.AutoSize = true;
            this.lblDatePaiement.Location = new System.Drawing.Point(86, 84);
            this.lblDatePaiement.Name = "lblDatePaiement";
            this.lblDatePaiement.Size = new System.Drawing.Size(136, 20);
            this.lblDatePaiement.TabIndex = 4;
            this.lblDatePaiement.Text = "Date du paiement";
            // 
            // txtMontantDu
            // 
            this.txtMontantDu.Enabled = false;
            this.txtMontantDu.Location = new System.Drawing.Point(231, 30);
            this.txtMontantDu.Name = "txtMontantDu";
            this.txtMontantDu.Size = new System.Drawing.Size(127, 26);
            this.txtMontantDu.TabIndex = 5;
            // 
            // lblMontantDu
            // 
            this.lblMontantDu.AutoSize = true;
            this.lblMontantDu.Location = new System.Drawing.Point(132, 33);
            this.lblMontantDu.Name = "lblMontantDu";
            this.lblMontantDu.Size = new System.Drawing.Size(90, 20);
            this.lblMontantDu.TabIndex = 6;
            this.lblMontantDu.Text = "Montant du";
            // 
            // lblMontantPaye
            // 
            this.lblMontantPaye.AutoSize = true;
            this.lblMontantPaye.Location = new System.Drawing.Point(115, 129);
            this.lblMontantPaye.Name = "lblMontantPaye";
            this.lblMontantPaye.Size = new System.Drawing.Size(106, 20);
            this.lblMontantPaye.TabIndex = 8;
            this.lblMontantPaye.Text = "Montant payé";
            // 
            // txtMontantPaye
            // 
            this.txtMontantPaye.Location = new System.Drawing.Point(231, 126);
            this.txtMontantPaye.Name = "txtMontantPaye";
            this.txtMontantPaye.Size = new System.Drawing.Size(127, 26);
            this.txtMontantPaye.TabIndex = 7;
            // 
            // lblResteAPayer
            // 
            this.lblResteAPayer.AutoSize = true;
            this.lblResteAPayer.Location = new System.Drawing.Point(115, 176);
            this.lblResteAPayer.Name = "lblResteAPayer";
            this.lblResteAPayer.Size = new System.Drawing.Size(108, 20);
            this.lblResteAPayer.TabIndex = 10;
            this.lblResteAPayer.Text = "Reste à payer";
            // 
            // txtResteAPayer
            // 
            this.txtResteAPayer.Enabled = false;
            this.txtResteAPayer.Location = new System.Drawing.Point(231, 173);
            this.txtResteAPayer.Name = "txtResteAPayer";
            this.txtResteAPayer.Size = new System.Drawing.Size(127, 26);
            this.txtResteAPayer.TabIndex = 9;
            // 
            // cbxRegle
            // 
            this.cbxRegle.AutoSize = true;
            this.cbxRegle.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxRegle.Enabled = false;
            this.cbxRegle.Location = new System.Drawing.Point(175, 217);
            this.cbxRegle.Name = "cbxRegle";
            this.cbxRegle.Size = new System.Drawing.Size(77, 24);
            this.cbxRegle.TabIndex = 11;
            this.cbxRegle.Text = "Réglé";
            this.cbxRegle.UseVisualStyleBackColor = true;
            // 
            // btnValider
            // 
            this.btnValider.Location = new System.Drawing.Point(169, 402);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(205, 62);
            this.btnValider.TabIndex = 12;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(409, 402);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(130, 62);
            this.btnFermer.TabIndex = 13;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // grbChamps
            // 
            this.grbChamps.Controls.Add(this.cbxRegle);
            this.grbChamps.Controls.Add(this.lblResteAPayer);
            this.grbChamps.Controls.Add(this.txtResteAPayer);
            this.grbChamps.Controls.Add(this.lblMontantPaye);
            this.grbChamps.Controls.Add(this.txtMontantPaye);
            this.grbChamps.Controls.Add(this.lblMontantDu);
            this.grbChamps.Controls.Add(this.txtMontantDu);
            this.grbChamps.Controls.Add(this.lblDatePaiement);
            this.grbChamps.Controls.Add(this.datPaiement);
            this.grbChamps.Location = new System.Drawing.Point(33, 109);
            this.grbChamps.Name = "grbChamps";
            this.grbChamps.Size = new System.Drawing.Size(506, 261);
            this.grbChamps.TabIndex = 14;
            this.grbChamps.TabStop = false;
            // 
            // ModifPaiements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 476);
            this.Controls.Add(this.grbChamps);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.btnValider);
            this.Name = "ModifPaiements";
            this.Text = "ModifPaiements";
            this.grbChamps.ResumeLayout(false);
            this.grbChamps.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.DateTimePicker datPaiement;
        private System.Windows.Forms.Label lblDatePaiement;
        private System.Windows.Forms.TextBox txtMontantDu;
        private System.Windows.Forms.Label lblMontantDu;
        private System.Windows.Forms.Label lblMontantPaye;
        private System.Windows.Forms.TextBox txtMontantPaye;
        private System.Windows.Forms.Label lblResteAPayer;
        private System.Windows.Forms.TextBox txtResteAPayer;
        private System.Windows.Forms.CheckBox cbxRegle;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.GroupBox grbChamps;
    }
}