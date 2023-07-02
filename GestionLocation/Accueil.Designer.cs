
using System.Drawing;

namespace GestionLocation
{
    partial class Accueil
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
            this.lstLocations = new System.Windows.Forms.ListBox();
            this.btnFermerAppli = new System.Windows.Forms.Button();
            this.lblLocEnCours = new System.Windows.Forms.Label();
            this.btnLocations = new System.Windows.Forms.Button();
            this.btnBiens = new System.Windows.Forms.Button();
            this.btnLocataires = new System.Windows.Forms.Button();
            this.btnCautions = new System.Windows.Forms.Button();
            this.btnCharges = new System.Windows.Forms.Button();
            this.btnPaiements = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstLocations
            // 
            this.lstLocations.FormattingEnabled = true;
            this.lstLocations.ItemHeight = 20;
            this.lstLocations.Location = new System.Drawing.Point(12, 224);
            this.lstLocations.Name = "lstLocations";
            this.lstLocations.Size = new System.Drawing.Size(982, 164);
            this.lstLocations.TabIndex = 2;
            // 
            // btnFermerAppli
            // 
            this.btnFermerAppli.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnFermerAppli.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFermerAppli.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFermerAppli.Location = new System.Drawing.Point(873, 12);
            this.btnFermerAppli.Name = "btnFermerAppli";
            this.btnFermerAppli.Size = new System.Drawing.Size(121, 86);
            this.btnFermerAppli.TabIndex = 5;
            this.btnFermerAppli.Text = "Quitter";
            this.btnFermerAppli.UseVisualStyleBackColor = false;
            this.btnFermerAppli.Click += new System.EventHandler(this.BtnFermerAppli_Click);
            // 
            // lblLocEnCours
            // 
            this.lblLocEnCours.AutoSize = true;
            this.lblLocEnCours.Location = new System.Drawing.Point(12, 193);
            this.lblLocEnCours.Name = "lblLocEnCours";
            this.lblLocEnCours.Size = new System.Drawing.Size(143, 20);
            this.lblLocEnCours.TabIndex = 6;
            this.lblLocEnCours.Text = "Locations en cours";
            // 
            // btnLocations
            // 
            this.btnLocations.Location = new System.Drawing.Point(35, 23);
            this.btnLocations.Name = "btnLocations";
            this.btnLocations.Size = new System.Drawing.Size(136, 66);
            this.btnLocations.TabIndex = 9;
            this.btnLocations.Text = "Locations";
            this.btnLocations.UseVisualStyleBackColor = true;
            this.btnLocations.Click += new System.EventHandler(this.BtnLocations_Click);
            this.btnLocations.MouseEnter += new System.EventHandler(this.BtnLocations_MouseEnter);
            this.btnLocations.MouseLeave += new System.EventHandler(this.BtnLocations_MouseLeave);
            // 
            // btnBiens
            // 
            this.btnBiens.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBiens.Location = new System.Drawing.Point(199, 23);
            this.btnBiens.Name = "btnBiens";
            this.btnBiens.Size = new System.Drawing.Size(136, 66);
            this.btnBiens.TabIndex = 10;
            this.btnBiens.Text = "Biens";
            this.btnBiens.UseVisualStyleBackColor = true;
            this.btnBiens.Click += new System.EventHandler(this.BtnBiens_Click);
            this.btnBiens.MouseEnter += new System.EventHandler(this.BtnBiens_MouseEnter);
            this.btnBiens.MouseLeave += new System.EventHandler(this.BtnBiens_MouseLeave);
            // 
            // btnLocataires
            // 
            this.btnLocataires.Location = new System.Drawing.Point(364, 23);
            this.btnLocataires.Name = "btnLocataires";
            this.btnLocataires.Size = new System.Drawing.Size(136, 66);
            this.btnLocataires.TabIndex = 11;
            this.btnLocataires.Text = "Locataires";
            this.btnLocataires.UseVisualStyleBackColor = true;
            this.btnLocataires.Click += new System.EventHandler(this.BtnLocataires_Click);
            this.btnLocataires.MouseEnter += new System.EventHandler(this.BtnLocataires_MouseEnter);
            this.btnLocataires.MouseLeave += new System.EventHandler(this.BtnLocataires_MouseLeave);
            // 
            // btnCautions
            // 
            this.btnCautions.Location = new System.Drawing.Point(530, 23);
            this.btnCautions.Name = "btnCautions";
            this.btnCautions.Size = new System.Drawing.Size(136, 66);
            this.btnCautions.TabIndex = 12;
            this.btnCautions.Text = "Cautions";
            this.btnCautions.UseVisualStyleBackColor = true;
            this.btnCautions.Click += new System.EventHandler(this.BtnCautions_Click);
            this.btnCautions.MouseEnter += new System.EventHandler(this.BtnCautions_MouseEnter);
            this.btnCautions.MouseLeave += new System.EventHandler(this.BtnCautions_MouseLeave);
            // 
            // btnCharges
            // 
            this.btnCharges.Location = new System.Drawing.Point(199, 107);
            this.btnCharges.Name = "btnCharges";
            this.btnCharges.Size = new System.Drawing.Size(136, 66);
            this.btnCharges.TabIndex = 13;
            this.btnCharges.Text = "Charges";
            this.btnCharges.UseVisualStyleBackColor = true;
            this.btnCharges.Click += new System.EventHandler(this.BtnCharges_Click);
            this.btnCharges.MouseEnter += new System.EventHandler(this.BtnCharges_MouseEnter);
            this.btnCharges.MouseLeave += new System.EventHandler(this.BtnCharges_MouseLeave);
            // 
            // btnPaiements
            // 
            this.btnPaiements.Location = new System.Drawing.Point(35, 107);
            this.btnPaiements.Name = "btnPaiements";
            this.btnPaiements.Size = new System.Drawing.Size(136, 66);
            this.btnPaiements.TabIndex = 14;
            this.btnPaiements.Text = "Paiements";
            this.btnPaiements.UseVisualStyleBackColor = true;
            this.btnPaiements.Click += new System.EventHandler(this.BtnPaiements_Click);
            this.btnPaiements.MouseEnter += new System.EventHandler(this.BtnPaiements_MouseEnter);
            this.btnPaiements.MouseLeave += new System.EventHandler(this.BtnPaiements_MouseLeave);
            // 
            // Accueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 411);
            this.Controls.Add(this.btnPaiements);
            this.Controls.Add(this.btnCharges);
            this.Controls.Add(this.btnCautions);
            this.Controls.Add(this.btnLocataires);
            this.Controls.Add(this.btnBiens);
            this.Controls.Add(this.btnLocations);
            this.Controls.Add(this.lblLocEnCours);
            this.Controls.Add(this.btnFermerAppli);
            this.Controls.Add(this.lstLocations);
            this.Name = "Accueil";
            this.Text = "Accueil";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstLocations;
        private System.Windows.Forms.Button btnFermerAppli;
        private System.Windows.Forms.Label lblLocEnCours;
        private System.Windows.Forms.Button btnLocations;
        private System.Windows.Forms.Button btnBiens;
        private System.Windows.Forms.Button btnLocataires;
        private System.Windows.Forms.Button btnCautions;
        private System.Windows.Forms.Button btnCharges;
        private System.Windows.Forms.Button btnPaiements;
    }
}