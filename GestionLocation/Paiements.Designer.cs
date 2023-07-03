
namespace GestionLocation
{
    partial class Paiements
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
            this.lstPaiements = new System.Windows.Forms.ListBox();
            this.lstLocations = new System.Windows.Forms.ListBox();
            this.btnFermer = new System.Windows.Forms.Button();
            this.btnSaisirPaiement = new System.Windows.Forms.Button();
            this.btnFiltreArchive = new System.Windows.Forms.Button();
            this.btnNonRegle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstPaiements
            // 
            this.lstPaiements.FormattingEnabled = true;
            this.lstPaiements.ItemHeight = 20;
            this.lstPaiements.Location = new System.Drawing.Point(20, 178);
            this.lstPaiements.Name = "lstPaiements";
            this.lstPaiements.Size = new System.Drawing.Size(958, 244);
            this.lstPaiements.TabIndex = 0;
            // 
            // lstLocations
            // 
            this.lstLocations.FormattingEnabled = true;
            this.lstLocations.ItemHeight = 20;
            this.lstLocations.Location = new System.Drawing.Point(20, 23);
            this.lstLocations.Name = "lstLocations";
            this.lstLocations.Size = new System.Drawing.Size(808, 144);
            this.lstLocations.TabIndex = 1;
            this.lstLocations.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LstLocations_MouseClick);
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(797, 442);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(181, 51);
            this.btnFermer.TabIndex = 3;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // btnSaisirPaiement
            // 
            this.btnSaisirPaiement.Location = new System.Drawing.Point(848, 46);
            this.btnSaisirPaiement.Name = "btnSaisirPaiement";
            this.btnSaisirPaiement.Size = new System.Drawing.Size(130, 80);
            this.btnSaisirPaiement.TabIndex = 4;
            this.btnSaisirPaiement.Text = "Saisir un paiement";
            this.btnSaisirPaiement.UseVisualStyleBackColor = true;
            this.btnSaisirPaiement.Click += new System.EventHandler(this.BtnSaisirPaiement_Click);
            // 
            // btnFiltreArchive
            // 
            this.btnFiltreArchive.Location = new System.Drawing.Point(20, 442);
            this.btnFiltreArchive.Name = "btnFiltreArchive";
            this.btnFiltreArchive.Size = new System.Drawing.Size(366, 51);
            this.btnFiltreArchive.TabIndex = 5;
            this.btnFiltreArchive.Text = "Afficher archivés";
            this.btnFiltreArchive.UseVisualStyleBackColor = true;
            this.btnFiltreArchive.Click += new System.EventHandler(this.BtnFiltreArchive_Click);
            // 
            // btnNonRegle
            // 
            this.btnNonRegle.Location = new System.Drawing.Point(407, 442);
            this.btnNonRegle.Name = "btnNonRegle";
            this.btnNonRegle.Size = new System.Drawing.Size(366, 51);
            this.btnNonRegle.TabIndex = 6;
            this.btnNonRegle.Text = "Afficher non réglés";
            this.btnNonRegle.UseVisualStyleBackColor = true;
            this.btnNonRegle.Click += new System.EventHandler(this.BtnNonRegle_Click);
            // 
            // Paiements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 505);
            this.Controls.Add(this.btnNonRegle);
            this.Controls.Add(this.btnFiltreArchive);
            this.Controls.Add(this.btnSaisirPaiement);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.lstLocations);
            this.Controls.Add(this.lstPaiements);
            this.Name = "Paiements";
            this.Text = "Paiements";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstPaiements;
        private System.Windows.Forms.ListBox lstLocations;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.Button btnSaisirPaiement;
        private System.Windows.Forms.Button btnFiltreArchive;
        private System.Windows.Forms.Button btnNonRegle;
    }
}