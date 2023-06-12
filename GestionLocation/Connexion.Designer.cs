
using System.Drawing;

namespace GestionLocation
{
    partial class Connexion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Tentative max de connexion
        private static int essaiMax = 3;

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
            this.btnConnexion = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblPresentation = new System.Windows.Forms.Label();
            this.lblErreur = new System.Windows.Forms.Label();
            this.lblCptEssai = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnexion
            // 
            this.btnConnexion.Location = new System.Drawing.Point(352, 174);
            this.btnConnexion.Name = "btnConnexion";
            this.btnConnexion.Size = new System.Drawing.Size(198, 43);
            this.btnConnexion.TabIndex = 0;
            this.btnConnexion.Text = "CONNEXION";
            this.btnConnexion.UseVisualStyleBackColor = true;
            this.btnConnexion.Click += new System.EventHandler(this.BtnConnexion_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(352, 76);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(198, 26);
            this.txtId.TabIndex = 1;
            this.txtId.Text = "";
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtId_KeyPress);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(352, 118);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(198, 26);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPwd_KeyPress);
            // 
            // lblPresentation
            // 
            this.lblPresentation.AutoSize = true;
            this.lblPresentation.Font = new System.Drawing.Font("Verdana", 12F);
            this.lblPresentation.Location = new System.Drawing.Point(23, 9);
            this.lblPresentation.Name = "lblPresentation";
            this.lblPresentation.Size = new System.Drawing.Size(559, 29);
            this.lblPresentation.TabIndex = 3;
            this.lblPresentation.Text = "Veuillez entrez vos informations de connexion";
            // 
            // lblErreur
            // 
            this.lblErreur.AutoSize = true;
            this.lblErreur.Location = new System.Drawing.Point(53, 80);
            this.lblErreur.Name = "lblErreur";
            this.lblErreur.Size = new System.Drawing.Size(0, 20);
            this.lblErreur.TabIndex = 4;
            // 
            // lblCptEssai
            // 
            this.lblCptEssai.AutoSize = true;
            this.lblCptEssai.Location = new System.Drawing.Point(399, 238);
            this.lblCptEssai.Name = "lblCptEssai";
            this.lblCptEssai.Size = new System.Drawing.Size(0, 20);
            this.lblCptEssai.TabIndex = 5;
            // 
            // Connexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 278);
            this.Controls.Add(this.lblCptEssai);
            this.Controls.Add(this.lblErreur);
            this.Controls.Add(this.lblPresentation);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnConnexion);
            this.MaximumSize = new System.Drawing.Size(630, 334);
            this.Name = "Connexion";
            this.Text = "Connexion";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Connexion_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnexion;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblPresentation;
        private System.Windows.Forms.Label lblErreur;
        private System.Windows.Forms.Label lblCptEssai;
    }
}