
namespace GestionLocation
{
    partial class Cautions
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
            this.btnRechercher = new System.Windows.Forms.Button();
            this.gbrCautionArchive = new System.Windows.Forms.GroupBox();
            this.rdbCautionNonArchive = new System.Windows.Forms.RadioButton();
            this.rdbCautionArchive = new System.Windows.Forms.RadioButton();
            this.btnArchiver = new System.Windows.Forms.Button();
            this.lstCautions = new System.Windows.Forms.ListBox();
            this.gbrCautionArchive.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnSupprimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupprimer.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSupprimer.Location = new System.Drawing.Point(306, 23);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(132, 48);
            this.btnSupprimer.TabIndex = 20;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = false;
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(168, 23);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(132, 48);
            this.btnModifier.TabIndex = 16;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.BtnModifier_Click);
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(30, 23);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(132, 48);
            this.btnAjouter.TabIndex = 15;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);
            // 
            // btnRechercher
            // 
            this.btnRechercher.Location = new System.Drawing.Point(285, 192);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(153, 43);
            this.btnRechercher.TabIndex = 19;
            this.btnRechercher.Text = "Chercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Click += new System.EventHandler(this.BtnRechercher_Click);
            // 
            // gbrCautionArchive
            // 
            this.gbrCautionArchive.Controls.Add(this.rdbCautionNonArchive);
            this.gbrCautionArchive.Controls.Add(this.rdbCautionArchive);
            this.gbrCautionArchive.Location = new System.Drawing.Point(285, 90);
            this.gbrCautionArchive.Name = "gbrCautionArchive";
            this.gbrCautionArchive.Size = new System.Drawing.Size(153, 92);
            this.gbrCautionArchive.TabIndex = 18;
            this.gbrCautionArchive.TabStop = false;
            this.gbrCautionArchive.Text = "Tri";
            // 
            // rdbCautionNonArchive
            // 
            this.rdbCautionNonArchive.AutoSize = true;
            this.rdbCautionNonArchive.Checked = true;
            this.rdbCautionNonArchive.Location = new System.Drawing.Point(6, 55);
            this.rdbCautionNonArchive.Name = "rdbCautionNonArchive";
            this.rdbCautionNonArchive.Size = new System.Drawing.Size(117, 24);
            this.rdbCautionNonArchive.TabIndex = 1;
            this.rdbCautionNonArchive.TabStop = true;
            this.rdbCautionNonArchive.Text = "Non archivé";
            this.rdbCautionNonArchive.UseVisualStyleBackColor = true;
            // 
            // rdbCautionArchive
            // 
            this.rdbCautionArchive.AutoSize = true;
            this.rdbCautionArchive.Location = new System.Drawing.Point(6, 25);
            this.rdbCautionArchive.Name = "rdbCautionArchive";
            this.rdbCautionArchive.Size = new System.Drawing.Size(86, 24);
            this.rdbCautionArchive.TabIndex = 0;
            this.rdbCautionArchive.TabStop = true;
            this.rdbCautionArchive.Text = "Archivé";
            this.rdbCautionArchive.UseVisualStyleBackColor = true;
            // 
            // btnArchiver
            // 
            this.btnArchiver.Location = new System.Drawing.Point(285, 248);
            this.btnArchiver.Name = "btnArchiver";
            this.btnArchiver.Size = new System.Drawing.Size(153, 66);
            this.btnArchiver.TabIndex = 17;
            this.btnArchiver.Text = "Archiver / Désarchiver";
            this.btnArchiver.UseVisualStyleBackColor = true;
            this.btnArchiver.Click += new System.EventHandler(this.BtnArchiver_Click);
            // 
            // lstCautions
            // 
            this.lstCautions.FormattingEnabled = true;
            this.lstCautions.ItemHeight = 20;
            this.lstCautions.Location = new System.Drawing.Point(30, 90);
            this.lstCautions.Name = "lstCautions";
            this.lstCautions.Size = new System.Drawing.Size(230, 224);
            this.lstCautions.TabIndex = 14;
            // 
            // Cautions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 322);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.gbrCautionArchive);
            this.Controls.Add(this.btnArchiver);
            this.Controls.Add(this.lstCautions);
            this.Name = "Cautions";
            this.Text = "Caution";
            this.gbrCautionArchive.ResumeLayout(false);
            this.gbrCautionArchive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.GroupBox gbrCautionArchive;
        private System.Windows.Forms.RadioButton rdbCautionNonArchive;
        private System.Windows.Forms.RadioButton rdbCautionArchive;
        private System.Windows.Forms.Button btnArchiver;
        private System.Windows.Forms.ListBox lstCautions;
    }
}