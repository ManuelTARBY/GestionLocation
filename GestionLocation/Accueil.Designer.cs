
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
            this.btnUser = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnGroupes = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstLocations
            // 
            this.lstLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLocations.Enabled = false;
            this.lstLocations.FormattingEnabled = true;
            this.lstLocations.ItemHeight = 20;
            this.lstLocations.Location = new System.Drawing.Point(3, 215);
            this.lstLocations.Name = "lstLocations";
            this.lstLocations.Size = new System.Drawing.Size(976, 164);
            this.lstLocations.TabIndex = 2;
            // 
            // btnFermerAppli
            // 
            this.btnFermerAppli.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermerAppli.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnFermerAppli.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFermerAppli.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFermerAppli.Location = new System.Drawing.Point(832, 3);
            this.btnFermerAppli.Name = "btnFermerAppli";
            this.btnFermerAppli.Size = new System.Drawing.Size(141, 173);
            this.btnFermerAppli.TabIndex = 5;
            this.btnFermerAppli.Text = "Quitter";
            this.btnFermerAppli.UseVisualStyleBackColor = false;
            this.btnFermerAppli.Click += new System.EventHandler(this.BtnFermerAppli_Click);
            // 
            // lblLocEnCours
            // 
            this.lblLocEnCours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLocEnCours.AutoSize = true;
            this.lblLocEnCours.Location = new System.Drawing.Point(3, 192);
            this.lblLocEnCours.Name = "lblLocEnCours";
            this.lblLocEnCours.Size = new System.Drawing.Size(143, 20);
            this.lblLocEnCours.TabIndex = 6;
            this.lblLocEnCours.Text = "Locations en cours";
            // 
            // btnLocations
            // 
            this.btnLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocations.Location = new System.Drawing.Point(3, 3);
            this.btnLocations.Name = "btnLocations";
            this.btnLocations.Size = new System.Drawing.Size(199, 80);
            this.btnLocations.TabIndex = 9;
            this.btnLocations.Text = "Locations";
            this.btnLocations.UseVisualStyleBackColor = true;
            this.btnLocations.Click += new System.EventHandler(this.BtnLocations_Click);
            this.btnLocations.MouseEnter += new System.EventHandler(this.BtnLocations_MouseEnter);
            this.btnLocations.MouseLeave += new System.EventHandler(this.BtnLocations_MouseLeave);
            // 
            // btnBiens
            // 
            this.btnBiens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBiens.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBiens.Location = new System.Drawing.Point(208, 3);
            this.btnBiens.Name = "btnBiens";
            this.btnBiens.Size = new System.Drawing.Size(199, 80);
            this.btnBiens.TabIndex = 10;
            this.btnBiens.Text = "Biens";
            this.btnBiens.UseVisualStyleBackColor = true;
            this.btnBiens.Click += new System.EventHandler(this.BtnBiens_Click);
            this.btnBiens.MouseEnter += new System.EventHandler(this.BtnBiens_MouseEnter);
            this.btnBiens.MouseLeave += new System.EventHandler(this.BtnBiens_MouseLeave);
            // 
            // btnLocataires
            // 
            this.btnLocataires.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocataires.Location = new System.Drawing.Point(413, 3);
            this.btnLocataires.Name = "btnLocataires";
            this.btnLocataires.Size = new System.Drawing.Size(199, 80);
            this.btnLocataires.TabIndex = 11;
            this.btnLocataires.Text = "Locataires";
            this.btnLocataires.UseVisualStyleBackColor = true;
            this.btnLocataires.Click += new System.EventHandler(this.BtnLocataires_Click);
            this.btnLocataires.MouseEnter += new System.EventHandler(this.BtnLocataires_MouseEnter);
            this.btnLocataires.MouseLeave += new System.EventHandler(this.BtnLocataires_MouseLeave);
            // 
            // btnCautions
            // 
            this.btnCautions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCautions.Location = new System.Drawing.Point(618, 3);
            this.btnCautions.Name = "btnCautions";
            this.btnCautions.Size = new System.Drawing.Size(202, 80);
            this.btnCautions.TabIndex = 12;
            this.btnCautions.Text = "Cautions";
            this.btnCautions.UseVisualStyleBackColor = true;
            this.btnCautions.Click += new System.EventHandler(this.BtnCautions_Click);
            this.btnCautions.MouseEnter += new System.EventHandler(this.BtnCautions_MouseEnter);
            this.btnCautions.MouseLeave += new System.EventHandler(this.BtnCautions_MouseLeave);
            // 
            // btnCharges
            // 
            this.btnCharges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCharges.Location = new System.Drawing.Point(3, 89);
            this.btnCharges.Name = "btnCharges";
            this.btnCharges.Size = new System.Drawing.Size(199, 81);
            this.btnCharges.TabIndex = 13;
            this.btnCharges.Text = "Charges";
            this.btnCharges.UseVisualStyleBackColor = true;
            this.btnCharges.Click += new System.EventHandler(this.BtnCharges_Click);
            this.btnCharges.MouseEnter += new System.EventHandler(this.BtnCharges_MouseEnter);
            this.btnCharges.MouseLeave += new System.EventHandler(this.BtnCharges_MouseLeave);
            // 
            // btnPaiements
            // 
            this.btnPaiements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPaiements.Location = new System.Drawing.Point(208, 89);
            this.btnPaiements.Name = "btnPaiements";
            this.btnPaiements.Size = new System.Drawing.Size(199, 81);
            this.btnPaiements.TabIndex = 14;
            this.btnPaiements.Text = "Paiements";
            this.btnPaiements.UseVisualStyleBackColor = true;
            this.btnPaiements.Click += new System.EventHandler(this.BtnPaiements_Click);
            this.btnPaiements.MouseEnter += new System.EventHandler(this.BtnPaiements_MouseEnter);
            this.btnPaiements.MouseLeave += new System.EventHandler(this.BtnPaiements_MouseLeave);
            // 
            // btnUser
            // 
            this.btnUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUser.Location = new System.Drawing.Point(618, 89);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(202, 81);
            this.btnUser.TabIndex = 15;
            this.btnUser.Text = "Utilisateur";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.BtnUser_Click);
            this.btnUser.MouseEnter += new System.EventHandler(this.BtnUser_MouseEnter);
            this.btnUser.MouseLeave += new System.EventHandler(this.BtnUser_MouseLeave);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLocEnCours, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lstLocations, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(982, 387);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFermerAppli, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(976, 179);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.btnPaiements, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnLocations, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnBiens, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnCharges, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnLocataires, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnCautions, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnGroupes, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnUser, 3, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(823, 173);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // btnGroupes
            // 
            this.btnGroupes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGroupes.Location = new System.Drawing.Point(413, 89);
            this.btnGroupes.Name = "btnGroupes";
            this.btnGroupes.Size = new System.Drawing.Size(199, 81);
            this.btnGroupes.TabIndex = 16;
            this.btnGroupes.Text = "Groupes";
            this.btnGroupes.UseVisualStyleBackColor = true;
            this.btnGroupes.Click += new System.EventHandler(this.BtnGroupes_Click);
            this.btnGroupes.MouseEnter += new System.EventHandler(this.BtnGroupes_MouseEnter);
            this.btnGroupes.MouseLeave += new System.EventHandler(this.BtnGroupes_MouseLeave);
            // 
            // Accueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 411);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Accueil";
            this.Text = "Accueil";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnGroupes;
    }
}