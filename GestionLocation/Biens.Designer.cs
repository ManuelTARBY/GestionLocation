
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.gbrBienArchive.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstBiens
            // 
            this.lstBiens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBiens.FormattingEnabled = true;
            this.lstBiens.ItemHeight = 20;
            this.lstBiens.Location = new System.Drawing.Point(3, 3);
            this.lstBiens.Name = "lstBiens";
            this.lstBiens.Size = new System.Drawing.Size(193, 144);
            this.lstBiens.TabIndex = 0;
            // 
            // btnAjouter
            // 
            this.btnAjouter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAjouter.Location = new System.Drawing.Point(4, 3);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(126, 44);
            this.btnAjouter.TabIndex = 1;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnModifier.Location = new System.Drawing.Point(136, 3);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(126, 44);
            this.btnModifier.TabIndex = 2;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.BtnModifier_Click);
            // 
            // btnArchiver
            // 
            this.btnArchiver.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnArchiver.Location = new System.Drawing.Point(8, 3);
            this.btnArchiver.Name = "btnArchiver";
            this.btnArchiver.Size = new System.Drawing.Size(183, 44);
            this.btnArchiver.TabIndex = 3;
            this.btnArchiver.Text = "Archiver/Désarchiver";
            this.btnArchiver.UseVisualStyleBackColor = true;
            this.btnArchiver.Click += new System.EventHandler(this.BtnArchiverDesarchiver_Click);
            // 
            // gbrBienArchive
            // 
            this.gbrBienArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbrBienArchive.Controls.Add(this.rdbBienNonArchive);
            this.gbrBienArchive.Controls.Add(this.rdbBienArchive);
            this.gbrBienArchive.Location = new System.Drawing.Point(3, 3);
            this.gbrBienArchive.Name = "gbrBienArchive";
            this.gbrBienArchive.Size = new System.Drawing.Size(188, 94);
            this.gbrBienArchive.TabIndex = 4;
            this.gbrBienArchive.TabStop = false;
            this.gbrBienArchive.Text = "Tri";
            // 
            // rdbBienNonArchive
            // 
            this.rdbBienNonArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.rdbBienArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnRechercher.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRechercher.Location = new System.Drawing.Point(3, 103);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(188, 38);
            this.btnRechercher.TabIndex = 5;
            this.btnRechercher.Text = "Chercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Click += new System.EventHandler(this.BtnRechercher_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSupprimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnSupprimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupprimer.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSupprimer.Location = new System.Drawing.Point(269, 3);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSupprimer.Size = new System.Drawing.Size(127, 44);
            this.btnSupprimer.TabIndex = 6;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = false;
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);
            // 
            // btnFicheBien
            // 
            this.btnFicheBien.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFicheBien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(190)))), ((int)(((byte)(54)))));
            this.btnFicheBien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFicheBien.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFicheBien.Location = new System.Drawing.Point(206, 3);
            this.btnFicheBien.Name = "btnFicheBien";
            this.btnFicheBien.Size = new System.Drawing.Size(186, 44);
            this.btnFicheBien.TabIndex = 7;
            this.btnFicheBien.Text = "Fiche du bien";
            this.btnFicheBien.UseVisualStyleBackColor = false;
            this.btnFicheBien.Click += new System.EventHandler(this.BtnFicheBien_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(405, 280);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.btnAjouter, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnModifier, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSupprimer, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(399, 50);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lstBiens, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 59);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(399, 162);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.btnRechercher, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.gbrBienArchive, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(202, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(194, 144);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btnFicheBien, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnArchiver, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 227);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(399, 50);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // Biens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 304);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Biens";
            this.Text = "Biens";
            this.gbrBienArchive.ResumeLayout(false);
            this.gbrBienArchive.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}