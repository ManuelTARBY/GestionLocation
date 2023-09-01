
using System;

namespace GestionLocation
{
    partial class AjoutModifLocations
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
            this.lstLocataires = new System.Windows.Forms.ListBox();
            this.lstCautions = new System.Windows.Forms.ListBox();
            this.datDebut = new System.Windows.Forms.DateTimePicker();
            this.datFin = new System.Windows.Forms.DateTimePicker();
            this.lblID = new System.Windows.Forms.Label();
            this.lblDebutLoc = new System.Windows.Forms.Label();
            this.lblFinLoc = new System.Windows.Forms.Label();
            this.txtDepotGarantie = new System.Windows.Forms.TextBox();
            this.lblDepotGarantie = new System.Windows.Forms.Label();
            this.cbxArchive = new System.Windows.Forms.CheckBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.lblBiens = new System.Windows.Forms.Label();
            this.lblLocataires = new System.Windows.Forms.Label();
            this.lblCautions = new System.Windows.Forms.Label();
            this.btnFermer = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstBiens
            // 
            this.lstBiens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBiens.FormattingEnabled = true;
            this.lstBiens.ItemHeight = 20;
            this.lstBiens.Location = new System.Drawing.Point(3, 99);
            this.lstBiens.Name = "lstBiens";
            this.lstBiens.Size = new System.Drawing.Size(154, 104);
            this.lstBiens.TabIndex = 0;
            // 
            // lstLocataires
            // 
            this.lstLocataires.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLocataires.FormattingEnabled = true;
            this.lstLocataires.ItemHeight = 20;
            this.lstLocataires.Location = new System.Drawing.Point(163, 99);
            this.lstLocataires.Name = "lstLocataires";
            this.lstLocataires.Size = new System.Drawing.Size(314, 104);
            this.lstLocataires.TabIndex = 1;
            // 
            // lstCautions
            // 
            this.lstCautions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCautions.FormattingEnabled = true;
            this.lstCautions.ItemHeight = 20;
            this.lstCautions.Location = new System.Drawing.Point(483, 99);
            this.lstCautions.Name = "lstCautions";
            this.lstCautions.Size = new System.Drawing.Size(315, 104);
            this.lstCautions.TabIndex = 2;
            // 
            // datDebut
            // 
            this.datDebut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datDebut.Location = new System.Drawing.Point(163, 35);
            this.datDebut.MaxDate = new System.DateTime(2123, 12, 31, 0, 0, 0, 0);
            this.datDebut.MinDate = new System.DateTime(1901, 1, 1, 0, 0, 0, 0);
            this.datDebut.Name = "datDebut";
            this.datDebut.Size = new System.Drawing.Size(314, 26);
            this.datDebut.TabIndex = 3;
            this.datDebut.Value = DateTime.Today;
            // 
            // datFin
            // 
            this.datFin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datFin.Location = new System.Drawing.Point(483, 35);
            this.datFin.MaxDate = new System.DateTime(2123, 12, 31, 0, 0, 0, 0);
            this.datFin.MinDate = new System.DateTime(1901, 1, 1, 0, 0, 0, 0);
            this.datFin.Name = "datFin";
            this.datFin.Size = new System.Drawing.Size(315, 26);
            this.datFin.TabIndex = 4;
            this.datFin.Value = this.datDebut.Value = DateTime.Today;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(9, 7);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 20);
            this.lblID.TabIndex = 28;
            // 
            // lblDebutLoc
            // 
            this.lblDebutLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDebutLoc.AutoSize = true;
            this.lblDebutLoc.Location = new System.Drawing.Point(163, 12);
            this.lblDebutLoc.Name = "lblDebutLoc";
            this.lblDebutLoc.Size = new System.Drawing.Size(134, 20);
            this.lblDebutLoc.TabIndex = 29;
            this.lblDebutLoc.Text = "Début de location";
            // 
            // lblFinLoc
            // 
            this.lblFinLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFinLoc.AutoSize = true;
            this.lblFinLoc.Location = new System.Drawing.Point(483, 12);
            this.lblFinLoc.Name = "lblFinLoc";
            this.lblFinLoc.Size = new System.Drawing.Size(112, 20);
            this.lblFinLoc.TabIndex = 30;
            this.lblFinLoc.Text = "Fin de location";
            // 
            // txtDepotGarantie
            // 
            this.txtDepotGarantie.Location = new System.Drawing.Point(3, 35);
            this.txtDepotGarantie.Name = "txtDepotGarantie";
            this.txtDepotGarantie.Size = new System.Drawing.Size(133, 26);
            this.txtDepotGarantie.TabIndex = 31;
            // 
            // lblDepotGarantie
            // 
            this.lblDepotGarantie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDepotGarantie.AutoSize = true;
            this.lblDepotGarantie.Location = new System.Drawing.Point(3, 12);
            this.lblDepotGarantie.Name = "lblDepotGarantie";
            this.lblDepotGarantie.Size = new System.Drawing.Size(137, 20);
            this.lblDepotGarantie.TabIndex = 32;
            this.lblDepotGarantie.Text = "Dépôt de garantie";
            // 
            // cbxArchive
            // 
            this.cbxArchive.AutoSize = true;
            this.cbxArchive.Location = new System.Drawing.Point(3, 226);
            this.cbxArchive.Name = "cbxArchive";
            this.cbxArchive.Size = new System.Drawing.Size(96, 24);
            this.cbxArchive.TabIndex = 33;
            this.cbxArchive.Text = "Archivée";
            this.cbxArchive.UseVisualStyleBackColor = true;
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.Location = new System.Drawing.Point(163, 226);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(314, 53);
            this.btnValider.TabIndex = 34;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // lblBiens
            // 
            this.lblBiens.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBiens.AutoSize = true;
            this.lblBiens.Location = new System.Drawing.Point(3, 76);
            this.lblBiens.Name = "lblBiens";
            this.lblBiens.Size = new System.Drawing.Size(49, 20);
            this.lblBiens.TabIndex = 35;
            this.lblBiens.Text = "Biens";
            // 
            // lblLocataires
            // 
            this.lblLocataires.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLocataires.AutoSize = true;
            this.lblLocataires.Location = new System.Drawing.Point(163, 76);
            this.lblLocataires.Name = "lblLocataires";
            this.lblLocataires.Size = new System.Drawing.Size(83, 20);
            this.lblLocataires.TabIndex = 36;
            this.lblLocataires.Text = "Locataires";
            // 
            // lblCautions
            // 
            this.lblCautions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCautions.AutoSize = true;
            this.lblCautions.Location = new System.Drawing.Point(483, 76);
            this.lblCautions.Name = "lblCautions";
            this.lblCautions.Size = new System.Drawing.Size(72, 20);
            this.lblCautions.TabIndex = 37;
            this.lblCautions.Text = "Cautions";
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.Location = new System.Drawing.Point(483, 226);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(315, 53);
            this.btnFermer.TabIndex = 38;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.lblFinLoc, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnValider, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblLocataires, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnFermer, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.lstLocataires, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDebutLoc, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.datDebut, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbxArchive, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.datFin, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCautions, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstCautions, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtDepotGarantie, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDepotGarantie, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstBiens, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblBiens, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.79452F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.20548F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(801, 329);
            this.tableLayoutPanel1.TabIndex = 39;
            // 
            // AjoutModifLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 348);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblID);
            this.Name = "AjoutModifLocations";
            this.Text = "Ajout / Modification d\'une location";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstBiens;
        private System.Windows.Forms.ListBox lstLocataires;
        private System.Windows.Forms.ListBox lstCautions;
        private System.Windows.Forms.DateTimePicker datDebut;
        private System.Windows.Forms.DateTimePicker datFin;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblDebutLoc;
        private System.Windows.Forms.Label lblFinLoc;
        private System.Windows.Forms.TextBox txtDepotGarantie;
        private System.Windows.Forms.Label lblDepotGarantie;
        private System.Windows.Forms.CheckBox cbxArchive;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Label lblBiens;
        private System.Windows.Forms.Label lblLocataires;
        private System.Windows.Forms.Label lblCautions;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}