﻿
using System;
using System.Windows.Forms;

namespace GestionLocation
{
    partial class AjoutModifLocataires
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AjoutModifLocataires));
            this.btnValider = new System.Windows.Forms.Button();
            this.lblArchive = new System.Windows.Forms.Label();
            this.cbxArchive = new System.Windows.Forms.CheckBox();
            this.lblLieuNaissance = new System.Windows.Forms.Label();
            this.lblDateNaissance = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblVille = new System.Windows.Forms.Label();
            this.lblCp = new System.Windows.Forms.Label();
            this.lblAdresse = new System.Windows.Forms.Label();
            this.lblPrenom = new System.Windows.Forms.Label();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.txtLieuNaissance = new System.Windows.Forms.TextBox();
            this.txtVille = new System.Windows.Forms.TextBox();
            this.txtCp = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelephone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.datDateNaissance = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnValider
            // 
            this.btnValider.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnValider.Location = new System.Drawing.Point(175, 417);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(244, 42);
            this.btnValider.TabIndex = 11;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // lblArchive
            // 
            this.lblArchive.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblArchive.AutoSize = true;
            this.lblArchive.Location = new System.Drawing.Point(108, 378);
            this.lblArchive.Name = "lblArchive";
            this.lblArchive.Size = new System.Drawing.Size(61, 20);
            this.lblArchive.TabIndex = 36;
            this.lblArchive.Text = "Archivé";
            // 
            // cbxArchive
            // 
            this.cbxArchive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxArchive.AutoSize = true;
            this.cbxArchive.Location = new System.Drawing.Point(175, 378);
            this.cbxArchive.Name = "cbxArchive";
            this.cbxArchive.Size = new System.Drawing.Size(22, 21);
            this.cbxArchive.TabIndex = 10;
            this.cbxArchive.UseVisualStyleBackColor = true;
            // 
            // lblLieuNaissance
            // 
            this.lblLieuNaissance.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLieuNaissance.AutoSize = true;
            this.lblLieuNaissance.Location = new System.Drawing.Point(130, 267);
            this.lblLieuNaissance.Name = "lblLieuNaissance";
            this.lblLieuNaissance.Size = new System.Drawing.Size(39, 20);
            this.lblLieuNaissance.TabIndex = 34;
            this.lblLieuNaissance.Text = "Lieu";
            // 
            // lblDateNaissance
            // 
            this.lblDateNaissance.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDateNaissance.AutoSize = true;
            this.lblDateNaissance.Location = new System.Drawing.Point(27, 230);
            this.lblDateNaissance.Name = "lblDateNaissance";
            this.lblDateNaissance.Size = new System.Drawing.Size(142, 20);
            this.lblDateNaissance.TabIndex = 33;
            this.lblDateNaissance.Text = "Date de naissance";
            // 
            // lblNom
            // 
            this.lblNom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(127, 82);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(42, 20);
            this.lblNom.TabIndex = 32;
            this.lblNom.Text = "Nom";
            // 
            // lblVille
            // 
            this.lblVille.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVille.AutoSize = true;
            this.lblVille.Location = new System.Drawing.Point(131, 193);
            this.lblVille.Name = "lblVille";
            this.lblVille.Size = new System.Drawing.Size(38, 20);
            this.lblVille.TabIndex = 31;
            this.lblVille.Text = "Ville";
            // 
            // lblCp
            // 
            this.lblCp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCp.AutoSize = true;
            this.lblCp.Location = new System.Drawing.Point(139, 156);
            this.lblCp.Name = "lblCp";
            this.lblCp.Size = new System.Drawing.Size(30, 20);
            this.lblCp.TabIndex = 30;
            this.lblCp.Text = "CP";
            // 
            // lblAdresse
            // 
            this.lblAdresse.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAdresse.AutoSize = true;
            this.lblAdresse.Location = new System.Drawing.Point(101, 119);
            this.lblAdresse.Name = "lblAdresse";
            this.lblAdresse.Size = new System.Drawing.Size(68, 20);
            this.lblAdresse.TabIndex = 29;
            this.lblAdresse.Text = "Adresse";
            // 
            // lblPrenom
            // 
            this.lblPrenom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(105, 45);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(64, 20);
            this.lblPrenom.TabIndex = 28;
            this.lblPrenom.Text = "Prénom";
            // 
            // txtAdresse
            // 
            this.txtAdresse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAdresse.Location = new System.Drawing.Point(175, 116);
            this.txtAdresse.MaxLength = 100;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(244, 26);
            this.txtAdresse.TabIndex = 3;
            // 
            // txtLieuNaissance
            // 
            this.txtLieuNaissance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLieuNaissance.Location = new System.Drawing.Point(175, 264);
            this.txtLieuNaissance.MaxLength = 50;
            this.txtLieuNaissance.Name = "txtLieuNaissance";
            this.txtLieuNaissance.Size = new System.Drawing.Size(244, 26);
            this.txtLieuNaissance.TabIndex = 7;
            // 
            // txtVille
            // 
            this.txtVille.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtVille.Location = new System.Drawing.Point(175, 190);
            this.txtVille.MaxLength = 50;
            this.txtVille.Name = "txtVille";
            this.txtVille.Size = new System.Drawing.Size(244, 26);
            this.txtVille.TabIndex = 5;
            // 
            // txtCp
            // 
            this.txtCp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCp.Location = new System.Drawing.Point(175, 153);
            this.txtCp.MaxLength = 5;
            this.txtCp.Name = "txtCp";
            this.txtCp.Size = new System.Drawing.Size(95, 26);
            this.txtCp.TabIndex = 4;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(175, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 20);
            this.lblID.TabIndex = 27;
            this.lblID.Visible = false;
            // 
            // txtNom
            // 
            this.txtNom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNom.Location = new System.Drawing.Point(175, 79);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(244, 26);
            this.txtNom.TabIndex = 2;
            // 
            // txtPrenom
            // 
            this.txtPrenom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPrenom.Location = new System.Drawing.Point(175, 42);
            this.txtPrenom.MaxLength = 50;
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(244, 26);
            this.txtPrenom.TabIndex = 1;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTelephone.Location = new System.Drawing.Point(175, 301);
            this.txtTelephone.MaxLength = 14;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(167, 26);
            this.txtTelephone.TabIndex = 8;
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtEmail.Location = new System.Drawing.Point(175, 338);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(244, 26);
            this.txtEmail.TabIndex = 9;
            // 
            // lblTelephone
            // 
            this.lblTelephone.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(85, 304);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(84, 20);
            this.lblTelephone.TabIndex = 41;
            this.lblTelephone.Text = "Téléphone";
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(121, 341);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(48, 20);
            this.lblEmail.TabIndex = 42;
            this.lblEmail.Text = "Email";
            // 
            // datDateNaissance
            // 
            this.datDateNaissance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.datDateNaissance.CustomFormat = "dd/MM/yyyy";
            this.datDateNaissance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datDateNaissance.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datDateNaissance.Location = new System.Drawing.Point(175, 227);
            this.datDateNaissance.MaxDate = new System.DateTime(2023, 6, 4, 0, 0, 0, 0);
            this.datDateNaissance.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.datDateNaissance.Name = "datDateNaissance";
            this.datDateNaissance.Size = new System.Drawing.Size(210, 26);
            this.datDateNaissance.TabIndex = 6;
            this.datDateNaissance.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.txtPrenom, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbxArchive, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblArchive, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblEmail, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTelephone, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblPrenom, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtEmail, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtNom, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblLieuNaissance, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtTelephone, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblDateNaissance, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtAdresse, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblVille, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblNom, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCp, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtCp, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblAdresse, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtVille, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.datDateNaissance, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtLieuNaissance, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnValider, 1, 11);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(492, 469);
            this.tableLayoutPanel1.TabIndex = 43;
            // 
            // AjoutModifLocataires
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 481);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AjoutModifLocataires";
            this.Text = "AjoutModifLocataires";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Label lblArchive;
        private System.Windows.Forms.CheckBox cbxArchive;
        private System.Windows.Forms.Label lblLieuNaissance;
        private System.Windows.Forms.Label lblDateNaissance;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblVille;
        private System.Windows.Forms.Label lblCp;
        private System.Windows.Forms.Label lblAdresse;
        private System.Windows.Forms.Label lblPrenom;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.TextBox txtLieuNaissance;
        private System.Windows.Forms.TextBox txtVille;
        private System.Windows.Forms.TextBox txtCp;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelephone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.DateTimePicker datDateNaissance;
        private TableLayoutPanel tableLayoutPanel1;
    }
}