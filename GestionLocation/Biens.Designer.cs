
using System.Drawing;

namespace GestionLocation
{
    partial class Biens
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
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnArchiver = new System.Windows.Forms.Button();
            this.gbrBienArchive = new System.Windows.Forms.GroupBox();
            this.rdbBienNonArchive = new System.Windows.Forms.RadioButton();
            this.rdbBienArchive = new System.Windows.Forms.RadioButton();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnFicheBien = new System.Windows.Forms.Button();
            this.gbrBienArchive.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstBiens
            // 
            this.lstBiens.FormattingEnabled = true;
            this.lstBiens.ItemHeight = 20;
            this.lstBiens.Location = new System.Drawing.Point(24, 79);
            this.lstBiens.Name = "lstBiens";
            this.lstBiens.Size = new System.Drawing.Size(183, 144);
            this.lstBiens.TabIndex = 0;
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(12, 12);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(132, 48);
            this.btnAjouter.TabIndex = 1;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(150, 12);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(132, 48);
            this.btnModifier.TabIndex = 2;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.BtnModifier_Click);
            // 
            // btnArchiver
            // 
            this.btnArchiver.Location = new System.Drawing.Point(24, 243);
            this.btnArchiver.Name = "btnArchiver";
            this.btnArchiver.Size = new System.Drawing.Size(183, 48);
            this.btnArchiver.TabIndex = 3;
            this.btnArchiver.Text = "Archiver/Désarchiver";
            this.btnArchiver.UseVisualStyleBackColor = true;
            this.btnArchiver.Click += new System.EventHandler(this.BtnArchiverDesarchiver_Click);
            // 
            // gbrBienArchive
            // 
            this.gbrBienArchive.Controls.Add(this.rdbBienNonArchive);
            this.gbrBienArchive.Controls.Add(this.rdbBienArchive);
            this.gbrBienArchive.Location = new System.Drawing.Point(226, 79);
            this.gbrBienArchive.Name = "gbrBienArchive";
            this.gbrBienArchive.Size = new System.Drawing.Size(197, 92);
            this.gbrBienArchive.TabIndex = 4;
            this.gbrBienArchive.TabStop = false;
            this.gbrBienArchive.Text = "Tri";
            // 
            // rdbBienNonArchive
            // 
            this.rdbBienNonArchive.AutoSize = true;
            this.rdbBienNonArchive.Checked = true;
            this.rdbBienNonArchive.Location = new System.Drawing.Point(6, 55);
            this.rdbBienNonArchive.Name = "rdbBienNonArchive";
            this.rdbBienNonArchive.Size = new System.Drawing.Size(117, 24);
            this.rdbBienNonArchive.TabIndex = 1;
            this.rdbBienNonArchive.TabStop = true;
            this.rdbBienNonArchive.Text = "Non archivé";
            this.rdbBienNonArchive.UseVisualStyleBackColor = true;
            // 
            // rdbBienArchive
            // 
            this.rdbBienArchive.AutoSize = true;
            this.rdbBienArchive.Location = new System.Drawing.Point(6, 25);
            this.rdbBienArchive.Name = "rdbBienArchive";
            this.rdbBienArchive.Size = new System.Drawing.Size(86, 24);
            this.rdbBienArchive.TabIndex = 0;
            this.rdbBienArchive.TabStop = true;
            this.rdbBienArchive.Text = "Archivé";
            this.rdbBienArchive.UseVisualStyleBackColor = true;
            // 
            // btnRechercher
            // 
            this.btnRechercher.Location = new System.Drawing.Point(226, 177);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(197, 46);
            this.btnRechercher.TabIndex = 5;
            this.btnRechercher.Text = "Chercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Click += new System.EventHandler(this.BtnRechercher_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnSupprimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupprimer.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSupprimer.Location = new System.Drawing.Point(291, 12);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSupprimer.Size = new System.Drawing.Size(132, 48);
            this.btnSupprimer.TabIndex = 6;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = false;
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);
            // 
            // btnFicheBien
            // 
            this.btnFicheBien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(190)))), ((int)(((byte)(54)))));
            this.btnFicheBien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFicheBien.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFicheBien.Location = new System.Drawing.Point(237, 243);
            this.btnFicheBien.Name = "btnFicheBien";
            this.btnFicheBien.Size = new System.Drawing.Size(186, 48);
            this.btnFicheBien.TabIndex = 7;
            this.btnFicheBien.Text = "Fiche du bien";
            this.btnFicheBien.UseVisualStyleBackColor = false;
            this.btnFicheBien.Click += new System.EventHandler(this.BtnFicheBien_Click);
            // 
            // Biens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 304);
            this.Controls.Add(this.btnFicheBien);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.gbrBienArchive);
            this.Controls.Add(this.btnArchiver);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.lstBiens);
            this.Name = "Biens";
            this.Text = "Biens";
            this.gbrBienArchive.ResumeLayout(false);
            this.gbrBienArchive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstBiens;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnArchiver;
        private System.Windows.Forms.GroupBox gbrBienArchive;
        private System.Windows.Forms.RadioButton rdbBienNonArchive;
        private System.Windows.Forms.RadioButton rdbBienArchive;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnFicheBien;
    }
}