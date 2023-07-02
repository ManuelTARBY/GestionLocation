
namespace GestionLocation
{
    partial class Locations
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstLocations = new System.Windows.Forms.ListBox();
            this.MajLocations = new System.Windows.Forms.Button();
            this.grpLocationArchive = new System.Windows.Forms.GroupBox();
            this.rbnNonArchive = new System.Windows.Forms.RadioButton();
            this.rbnArchive = new System.Windows.Forms.RadioButton();
            this.rbnToutes = new System.Windows.Forms.RadioButton();
            this.clbBiens = new System.Windows.Forms.CheckedListBox();
            this.btnTous = new System.Windows.Forms.Button();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnArchiver = new System.Windows.Forms.Button();
            this.btnAucun = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnFenPaiements = new System.Windows.Forms.Button();
            this.grpLocationArchive.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstLocations
            // 
            this.lstLocations.FormattingEnabled = true;
            this.lstLocations.ItemHeight = 20;
            this.lstLocations.Location = new System.Drawing.Point(12, 228);
            this.lstLocations.Name = "lstLocations";
            this.lstLocations.Size = new System.Drawing.Size(894, 164);
            this.lstLocations.TabIndex = 1;
            // 
            // MajLocations
            // 
            this.MajLocations.Location = new System.Drawing.Point(202, 168);
            this.MajLocations.Name = "MajLocations";
            this.MajLocations.Size = new System.Drawing.Size(182, 40);
            this.MajLocations.TabIndex = 2;
            this.MajLocations.Text = "Rechercher";
            this.MajLocations.UseVisualStyleBackColor = true;
            this.MajLocations.Click += new System.EventHandler(this.MajLocations_Click);
            // 
            // grpLocationArchive
            // 
            this.grpLocationArchive.Controls.Add(this.rbnNonArchive);
            this.grpLocationArchive.Controls.Add(this.rbnArchive);
            this.grpLocationArchive.Controls.Add(this.rbnToutes);
            this.grpLocationArchive.Location = new System.Drawing.Point(202, 31);
            this.grpLocationArchive.Name = "grpLocationArchive";
            this.grpLocationArchive.Size = new System.Drawing.Size(182, 120);
            this.grpLocationArchive.TabIndex = 3;
            this.grpLocationArchive.TabStop = false;
            // 
            // rbnNonArchive
            // 
            this.rbnNonArchive.AutoSize = true;
            this.rbnNonArchive.Checked = true;
            this.rbnNonArchive.Location = new System.Drawing.Point(19, 85);
            this.rbnNonArchive.Name = "rbnNonArchive";
            this.rbnNonArchive.Size = new System.Drawing.Size(134, 24);
            this.rbnNonArchive.TabIndex = 2;
            this.rbnNonArchive.TabStop = true;
            this.rbnNonArchive.Text = "Non archivées";
            this.rbnNonArchive.UseVisualStyleBackColor = true;
            // 
            // rbnArchive
            // 
            this.rbnArchive.AutoSize = true;
            this.rbnArchive.Location = new System.Drawing.Point(19, 55);
            this.rbnArchive.Name = "rbnArchive";
            this.rbnArchive.Size = new System.Drawing.Size(103, 24);
            this.rbnArchive.TabIndex = 1;
            this.rbnArchive.TabStop = true;
            this.rbnArchive.Text = "Archivées";
            this.rbnArchive.UseVisualStyleBackColor = true;
            // 
            // rbnToutes
            // 
            this.rbnToutes.AutoSize = true;
            this.rbnToutes.Location = new System.Drawing.Point(19, 25);
            this.rbnToutes.Name = "rbnToutes";
            this.rbnToutes.Size = new System.Drawing.Size(83, 24);
            this.rbnToutes.TabIndex = 0;
            this.rbnToutes.TabStop = true;
            this.rbnToutes.Text = "Toutes";
            this.rbnToutes.UseVisualStyleBackColor = true;
            // 
            // clbBiens
            // 
            this.clbBiens.FormattingEnabled = true;
            this.clbBiens.Location = new System.Drawing.Point(12, 89);
            this.clbBiens.Name = "clbBiens";
            this.clbBiens.Size = new System.Drawing.Size(174, 119);
            this.clbBiens.TabIndex = 6;
            // 
            // btnTous
            // 
            this.btnTous.Location = new System.Drawing.Point(106, 37);
            this.btnTous.Name = "btnTous";
            this.btnTous.Size = new System.Drawing.Size(80, 41);
            this.btnTous.TabIndex = 7;
            this.btnTous.Text = "Tous";
            this.btnTous.UseVisualStyleBackColor = true;
            this.btnTous.Click += new System.EventHandler(this.BtnTous_Click);
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(408, 37);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(182, 46);
            this.btnAjouter.TabIndex = 8;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(408, 89);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(182, 46);
            this.btnModifier.TabIndex = 9;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.BtnModifier_Click);
            // 
            // btnArchiver
            // 
            this.btnArchiver.Location = new System.Drawing.Point(724, 168);
            this.btnArchiver.Name = "btnArchiver";
            this.btnArchiver.Size = new System.Drawing.Size(182, 40);
            this.btnArchiver.TabIndex = 10;
            this.btnArchiver.Text = "Archiver/Désarchiver";
            this.btnArchiver.UseVisualStyleBackColor = true;
            this.btnArchiver.Click += new System.EventHandler(this.BtnArchiver_Click);
            // 
            // btnAucun
            // 
            this.btnAucun.Location = new System.Drawing.Point(12, 37);
            this.btnAucun.Name = "btnAucun";
            this.btnAucun.Size = new System.Drawing.Size(80, 41);
            this.btnAucun.TabIndex = 11;
            this.btnAucun.Text = "Aucun";
            this.btnAucun.UseVisualStyleBackColor = true;
            this.btnAucun.Click += new System.EventHandler(this.BtnAucun_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Location = new System.Drawing.Point(408, 141);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(182, 46);
            this.btnSupprimer.TabIndex = 14;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);
            // 
            // btnFenPaiements
            // 
            this.btnFenPaiements.Location = new System.Drawing.Point(724, 37);
            this.btnFenPaiements.Name = "btnFenPaiements";
            this.btnFenPaiements.Size = new System.Drawing.Size(182, 46);
            this.btnFenPaiements.TabIndex = 15;
            this.btnFenPaiements.Text = "Afficher les paiements";
            this.btnFenPaiements.UseVisualStyleBackColor = true;
            this.btnFenPaiements.Click += new System.EventHandler(this.BtnFenPaiements_Click);
            // 
            // Locations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 438);
            this.Controls.Add(this.btnFenPaiements);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnAucun);
            this.Controls.Add(this.btnArchiver);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.btnTous);
            this.Controls.Add(this.clbBiens);
            this.Controls.Add(this.grpLocationArchive);
            this.Controls.Add(this.MajLocations);
            this.Controls.Add(this.lstLocations);
            this.Name = "Locations";
            this.Text = "Locations";
            this.grpLocationArchive.ResumeLayout(false);
            this.grpLocationArchive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lstLocations;
        private System.Windows.Forms.Button MajLocations;
        private System.Windows.Forms.GroupBox grpLocationArchive;
        private System.Windows.Forms.RadioButton rbnNonArchive;
        private System.Windows.Forms.RadioButton rbnArchive;
        private System.Windows.Forms.RadioButton rbnToutes;
        private System.Windows.Forms.CheckedListBox clbBiens;
        private System.Windows.Forms.Button btnTous;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnArchiver;
        private System.Windows.Forms.Button btnAucun;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnFenPaiements;
    }
}

