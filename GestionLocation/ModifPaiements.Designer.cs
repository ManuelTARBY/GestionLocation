
using System;
using System.Windows.Forms;

namespace GestionLocation
{
    partial class ModifPaiements
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
            this.lblLocation = new System.Windows.Forms.Label();
            this.datPaiement = new System.Windows.Forms.DateTimePicker();
            this.lblDatePaiement = new System.Windows.Forms.Label();
            this.txtMontantDu = new System.Windows.Forms.TextBox();
            this.lblMontantDu = new System.Windows.Forms.Label();
            this.lblMontantPaye = new System.Windows.Forms.Label();
            this.txtMontantPaye = new System.Windows.Forms.TextBox();
            this.lblResteAPayer = new System.Windows.Forms.Label();
            this.txtResteAPayer = new System.Windows.Forms.TextBox();
            this.cbxRegle = new System.Windows.Forms.CheckBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLocation
            // 
            this.lblLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(3, 3);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(3);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(547, 120);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Info location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // datPaiement
            // 
            this.datPaiement.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.datPaiement.CustomFormat = "dd MMMM yyyy";
            this.datPaiement.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datPaiement.Location = new System.Drawing.Point(276, 59);
            this.datPaiement.Name = "datPaiement";
            this.datPaiement.Size = new System.Drawing.Size(195, 26);
            this.datPaiement.TabIndex = 3;
            this.datPaiement.Value = new System.DateTime(2023, 7, 1, 0, 0, 0, 0);
            // 
            // lblDatePaiement
            // 
            this.lblDatePaiement.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDatePaiement.AutoSize = true;
            this.lblDatePaiement.Location = new System.Drawing.Point(134, 62);
            this.lblDatePaiement.Name = "lblDatePaiement";
            this.lblDatePaiement.Size = new System.Drawing.Size(136, 20);
            this.lblDatePaiement.TabIndex = 4;
            this.lblDatePaiement.Text = "Date du paiement";
            // 
            // txtMontantDu
            // 
            this.txtMontantDu.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMontantDu.Enabled = false;
            this.txtMontantDu.Location = new System.Drawing.Point(276, 11);
            this.txtMontantDu.Name = "txtMontantDu";
            this.txtMontantDu.Size = new System.Drawing.Size(127, 26);
            this.txtMontantDu.TabIndex = 5;
            // 
            // lblMontantDu
            // 
            this.lblMontantDu.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMontantDu.AutoSize = true;
            this.lblMontantDu.Location = new System.Drawing.Point(180, 14);
            this.lblMontantDu.Name = "lblMontantDu";
            this.lblMontantDu.Size = new System.Drawing.Size(90, 20);
            this.lblMontantDu.TabIndex = 6;
            this.lblMontantDu.Text = "Montant du";
            // 
            // lblMontantPaye
            // 
            this.lblMontantPaye.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMontantPaye.AutoSize = true;
            this.lblMontantPaye.Location = new System.Drawing.Point(164, 110);
            this.lblMontantPaye.Name = "lblMontantPaye";
            this.lblMontantPaye.Size = new System.Drawing.Size(106, 20);
            this.lblMontantPaye.TabIndex = 8;
            this.lblMontantPaye.Text = "Montant payé";
            // 
            // txtMontantPaye
            // 
            this.txtMontantPaye.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMontantPaye.Location = new System.Drawing.Point(276, 107);
            this.txtMontantPaye.Name = "txtMontantPaye";
            this.txtMontantPaye.Size = new System.Drawing.Size(127, 26);
            this.txtMontantPaye.TabIndex = 7;
            // 
            // lblResteAPayer
            // 
            this.lblResteAPayer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblResteAPayer.AutoSize = true;
            this.lblResteAPayer.Location = new System.Drawing.Point(162, 158);
            this.lblResteAPayer.Name = "lblResteAPayer";
            this.lblResteAPayer.Size = new System.Drawing.Size(108, 20);
            this.lblResteAPayer.TabIndex = 10;
            this.lblResteAPayer.Text = "Reste à payer";
            // 
            // txtResteAPayer
            // 
            this.txtResteAPayer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtResteAPayer.Enabled = false;
            this.txtResteAPayer.Location = new System.Drawing.Point(276, 155);
            this.txtResteAPayer.Name = "txtResteAPayer";
            this.txtResteAPayer.Size = new System.Drawing.Size(127, 26);
            this.txtResteAPayer.TabIndex = 9;
            // 
            // cbxRegle
            // 
            this.cbxRegle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbxRegle.AutoSize = true;
            this.cbxRegle.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxRegle.Enabled = false;
            this.cbxRegle.Location = new System.Drawing.Point(193, 205);
            this.cbxRegle.Name = "cbxRegle";
            this.cbxRegle.Size = new System.Drawing.Size(77, 24);
            this.cbxRegle.TabIndex = 11;
            this.cbxRegle.Text = "Réglé";
            this.cbxRegle.UseVisualStyleBackColor = true;
            // 
            // btnValider
            // 
            this.btnValider.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnValider.Location = new System.Drawing.Point(276, 5);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(130, 62);
            this.btnValider.TabIndex = 12;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFermer.Location = new System.Drawing.Point(414, 5);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(130, 62);
            this.btnFermer.TabIndex = 13;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblLocation, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(553, 452);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.cbxRegle, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblMontantDu, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblResteAPayer, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtMontantDu, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblMontantPaye, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtResteAPayer, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblDatePaiement, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.datPaiement, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtMontantPaye, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 129);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(547, 242);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.btnFermer, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnValider, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 377);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(547, 72);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // ModifPaiements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 476);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ModifPaiements";
            this.Text = "ModifPaiements";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.DateTimePicker datPaiement;
        private System.Windows.Forms.Label lblDatePaiement;
        private System.Windows.Forms.TextBox txtMontantDu;
        private System.Windows.Forms.Label lblMontantDu;
        private System.Windows.Forms.Label lblMontantPaye;
        private System.Windows.Forms.TextBox txtMontantPaye;
        private System.Windows.Forms.Label lblResteAPayer;
        private System.Windows.Forms.TextBox txtResteAPayer;
        private System.Windows.Forms.CheckBox cbxRegle;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnFermer;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
    }
}