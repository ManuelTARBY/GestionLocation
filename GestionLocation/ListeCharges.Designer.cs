
namespace GestionLocation
{
    partial class ListeCharges
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
            this.lblNomBien = new System.Windows.Forms.Label();
            this.lstCharges = new System.Windows.Forms.ListBox();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.lstBiens = new System.Windows.Forms.ListBox();
            this.btnFiltrer = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNomBien
            // 
            this.lblNomBien.AutoSize = true;
            this.lblNomBien.Location = new System.Drawing.Point(37, 98);
            this.lblNomBien.Name = "lblNomBien";
            this.lblNomBien.Size = new System.Drawing.Size(51, 20);
            this.lblNomBien.TabIndex = 0;
            this.lblNomBien.Text = "label1";
            // 
            // lstCharges
            // 
            this.lstCharges.FormattingEnabled = true;
            this.lstCharges.ItemHeight = 20;
            this.lstCharges.Location = new System.Drawing.Point(41, 137);
            this.lstCharges.Name = "lstCharges";
            this.lstCharges.Size = new System.Drawing.Size(517, 164);
            this.lstCharges.TabIndex = 1;
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(41, 28);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(153, 49);
            this.btnAjouter.TabIndex = 2;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(221, 28);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(153, 51);
            this.btnModifier.TabIndex = 3;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.BtnModifier_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnSupprimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupprimer.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSupprimer.Location = new System.Drawing.Point(405, 28);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(153, 52);
            this.btnSupprimer.TabIndex = 4;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = false;
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);
            // 
            // lstBiens
            // 
            this.lstBiens.FormattingEnabled = true;
            this.lstBiens.ItemHeight = 20;
            this.lstBiens.Location = new System.Drawing.Point(607, 137);
            this.lstBiens.Name = "lstBiens";
            this.lstBiens.Size = new System.Drawing.Size(170, 104);
            this.lstBiens.TabIndex = 5;
            // 
            // btnFiltrer
            // 
            this.btnFiltrer.Location = new System.Drawing.Point(607, 247);
            this.btnFiltrer.Name = "btnFiltrer";
            this.btnFiltrer.Size = new System.Drawing.Size(170, 54);
            this.btnFiltrer.TabIndex = 6;
            this.btnFiltrer.Text = "Filtrer";
            this.btnFiltrer.UseVisualStyleBackColor = true;
            this.btnFiltrer.Click += new System.EventHandler(this.BtnFiltrer_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(607, 28);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(170, 52);
            this.btnFermer.TabIndex = 7;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // ListeCharges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 327);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.btnFiltrer);
            this.Controls.Add(this.lstBiens);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.lstCharges);
            this.Controls.Add(this.lblNomBien);
            this.Name = "ListeCharges";
            this.Text = "ListeCharges";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomBien;
        private System.Windows.Forms.ListBox lstCharges;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.ListBox lstBiens;
        private System.Windows.Forms.Button btnFiltrer;
        private System.Windows.Forms.Button btnFermer;
    }
}