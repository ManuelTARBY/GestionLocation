
namespace GestionLocation
{
    partial class Locataires
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
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.rdbLocataireArchive = new System.Windows.Forms.RadioButton();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.gbrLocataireArchive = new System.Windows.Forms.GroupBox();
            this.rdbLocataireNonArchive = new System.Windows.Forms.RadioButton();
            this.btnArchiver = new System.Windows.Forms.Button();
            this.lstLocataires = new System.Windows.Forms.ListBox();
            this.gbrLocataireArchive.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Location = new System.Drawing.Point(306, 18);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(132, 48);
            this.btnSupprimer.TabIndex = 13;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(168, 18);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(132, 48);
            this.btnModifier.TabIndex = 9;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.BtnModifier_Click);
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(30, 18);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(132, 48);
            this.btnAjouter.TabIndex = 8;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);
            // 
            // rdbCautionArchive
            // 
            this.rdbLocataireArchive.AutoSize = true;
            this.rdbLocataireArchive.Location = new System.Drawing.Point(6, 25);
            this.rdbLocataireArchive.Name = "rdbLocataireArchive";
            this.rdbLocataireArchive.Size = new System.Drawing.Size(86, 24);
            this.rdbLocataireArchive.TabIndex = 0;
            this.rdbLocataireArchive.TabStop = true;
            this.rdbLocataireArchive.Text = "Archivé";
            this.rdbLocataireArchive.UseVisualStyleBackColor = true;
            // 
            // btnRechercher
            // 
            this.btnRechercher.Location = new System.Drawing.Point(285, 194);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(153, 35);
            this.btnRechercher.TabIndex = 12;
            this.btnRechercher.Text = "Chercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Click += new System.EventHandler(this.BtnRechercher_Click);
            // 
            // gbrCautionArchive
            // 
            this.gbrLocataireArchive.Controls.Add(this.rdbLocataireNonArchive);
            this.gbrLocataireArchive.Controls.Add(this.rdbLocataireArchive);
            this.gbrLocataireArchive.Location = new System.Drawing.Point(285, 85);
            this.gbrLocataireArchive.Name = "gbrLocataireArchive";
            this.gbrLocataireArchive.Size = new System.Drawing.Size(153, 92);
            this.gbrLocataireArchive.TabIndex = 11;
            this.gbrLocataireArchive.TabStop = false;
            this.gbrLocataireArchive.Text = "Tri";
            // 
            // rdbCautionNonArchive
            // 
            this.rdbLocataireNonArchive.AutoSize = true;
            this.rdbLocataireNonArchive.Checked = true;
            this.rdbLocataireNonArchive.Location = new System.Drawing.Point(6, 55);
            this.rdbLocataireNonArchive.Name = "rdbLocataireNonArchive";
            this.rdbLocataireNonArchive.Size = new System.Drawing.Size(117, 24);
            this.rdbLocataireNonArchive.TabIndex = 1;
            this.rdbLocataireNonArchive.TabStop = true;
            this.rdbLocataireNonArchive.Text = "Non archivé";
            this.rdbLocataireNonArchive.UseVisualStyleBackColor = true;
            // 
            // btnArchiver
            // 
            this.btnArchiver.Location = new System.Drawing.Point(48, 249);
            this.btnArchiver.Name = "btnArchiver";
            this.btnArchiver.Size = new System.Drawing.Size(183, 48);
            this.btnArchiver.TabIndex = 10;
            this.btnArchiver.Text = "Archiver/Désarchiver";
            this.btnArchiver.UseVisualStyleBackColor = true;
            this.btnArchiver.Click += new System.EventHandler(this.BtnArchiver_Click);
            // 
            // lstCautions
            // 
            this.lstLocataires.FormattingEnabled = true;
            this.lstLocataires.ItemHeight = 20;
            this.lstLocataires.Location = new System.Drawing.Point(30, 85);
            this.lstLocataires.Name = "lstLocataires";
            this.lstLocataires.Size = new System.Drawing.Size(230, 144);
            this.lstLocataires.TabIndex = 7;
            // 
            // Locataires
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 322);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.gbrLocataireArchive);
            this.Controls.Add(this.btnArchiver);
            this.Controls.Add(this.lstLocataires);
            this.Name = "Locataires";
            this.Text = "Locataires";
            this.gbrLocataireArchive.ResumeLayout(false);
            this.gbrLocataireArchive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.RadioButton rdbLocataireArchive;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.GroupBox gbrLocataireArchive;
        private System.Windows.Forms.RadioButton rdbLocataireNonArchive;
        private System.Windows.Forms.Button btnArchiver;
        private System.Windows.Forms.ListBox lstLocataires;
    }
}