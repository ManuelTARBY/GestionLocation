
namespace GestionLocation
{
    partial class Stats
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stats));
            this.vcaannuelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbxAnnee = new System.Windows.Forms.ComboBox();
            this.lblAnnee = new System.Windows.Forms.Label();
            this.txtCFAnnuel = new System.Windows.Forms.TextBox();
            this.lblCFAnnuel = new System.Windows.Forms.Label();
            this.lblCAAnnuel = new System.Windows.Forms.Label();
            this.txtCAAnnuel = new System.Windows.Forms.TextBox();
            this.lblChargesAnnuelles = new System.Windows.Forms.Label();
            this.txtChargesAnnuelles = new System.Windows.Forms.TextBox();
            this.lblBien = new System.Windows.Forms.Label();
            this.cbxBien = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.vcaannuelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // vcaannuelBindingSource
            // 
            this.vcaannuelBindingSource.DataMember = "v_ca_annuel";
            // 
            // cbxAnnee
            // 
            this.cbxAnnee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAnnee.FormattingEnabled = true;
            this.cbxAnnee.Location = new System.Drawing.Point(12, 101);
            this.cbxAnnee.Name = "cbxAnnee";
            this.cbxAnnee.Size = new System.Drawing.Size(131, 28);
            this.cbxAnnee.TabIndex = 2;
            this.cbxAnnee.SelectedIndexChanged += new System.EventHandler(this.CbxAnnee_SelectedIndexChanged);
            // 
            // lblAnnee
            // 
            this.lblAnnee.AutoSize = true;
            this.lblAnnee.Location = new System.Drawing.Point(17, 78);
            this.lblAnnee.Name = "lblAnnee";
            this.lblAnnee.Size = new System.Drawing.Size(56, 20);
            this.lblAnnee.TabIndex = 3;
            this.lblAnnee.Text = "Année";
            // 
            // txtCFAnnuel
            // 
            this.txtCFAnnuel.Location = new System.Drawing.Point(304, 161);
            this.txtCFAnnuel.Name = "txtCFAnnuel";
            this.txtCFAnnuel.Size = new System.Drawing.Size(143, 26);
            this.txtCFAnnuel.TabIndex = 4;
            // 
            // lblCFAnnuel
            // 
            this.lblCFAnnuel.AutoSize = true;
            this.lblCFAnnuel.Location = new System.Drawing.Point(300, 136);
            this.lblCFAnnuel.Name = "lblCFAnnuel";
            this.lblCFAnnuel.Size = new System.Drawing.Size(135, 20);
            this.lblCFAnnuel.TabIndex = 5;
            this.lblCFAnnuel.Text = "Cash Flow annuel";
            // 
            // lblCAAnnuel
            // 
            this.lblCAAnnuel.AutoSize = true;
            this.lblCAAnnuel.Location = new System.Drawing.Point(300, 20);
            this.lblCAAnnuel.Name = "lblCAAnnuel";
            this.lblCAAnnuel.Size = new System.Drawing.Size(83, 20);
            this.lblCAAnnuel.TabIndex = 7;
            this.lblCAAnnuel.Text = "CA annuel";
            // 
            // txtCAAnnuel
            // 
            this.txtCAAnnuel.Location = new System.Drawing.Point(304, 45);
            this.txtCAAnnuel.Name = "txtCAAnnuel";
            this.txtCAAnnuel.Size = new System.Drawing.Size(143, 26);
            this.txtCAAnnuel.TabIndex = 6;
            // 
            // lblChargesAnnuelles
            // 
            this.lblChargesAnnuelles.AutoSize = true;
            this.lblChargesAnnuelles.Location = new System.Drawing.Point(300, 78);
            this.lblChargesAnnuelles.Name = "lblChargesAnnuelles";
            this.lblChargesAnnuelles.Size = new System.Drawing.Size(141, 20);
            this.lblChargesAnnuelles.TabIndex = 9;
            this.lblChargesAnnuelles.Text = "Charges annuelles";
            // 
            // txtChargesAnnuelles
            // 
            this.txtChargesAnnuelles.Location = new System.Drawing.Point(304, 103);
            this.txtChargesAnnuelles.Name = "txtChargesAnnuelles";
            this.txtChargesAnnuelles.Size = new System.Drawing.Size(143, 26);
            this.txtChargesAnnuelles.TabIndex = 8;
            // 
            // lblBien
            // 
            this.lblBien.AutoSize = true;
            this.lblBien.Location = new System.Drawing.Point(17, 20);
            this.lblBien.Name = "lblBien";
            this.lblBien.Size = new System.Drawing.Size(41, 20);
            this.lblBien.TabIndex = 11;
            this.lblBien.Text = "Bien";
            // 
            // cbxBien
            // 
            this.cbxBien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBien.FormattingEnabled = true;
            this.cbxBien.Location = new System.Drawing.Point(12, 43);
            this.cbxBien.Name = "cbxBien";
            this.cbxBien.Size = new System.Drawing.Size(131, 28);
            this.cbxBien.TabIndex = 1;
            this.cbxBien.SelectedIndexChanged += new System.EventHandler(this.CbxBien_SelectedIndexChanged);
            // 
            // Stats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 238);
            this.Controls.Add(this.cbxBien);
            this.Controls.Add(this.lblBien);
            this.Controls.Add(this.lblChargesAnnuelles);
            this.Controls.Add(this.txtChargesAnnuelles);
            this.Controls.Add(this.lblCAAnnuel);
            this.Controls.Add(this.txtCAAnnuel);
            this.Controls.Add(this.lblCFAnnuel);
            this.Controls.Add(this.txtCFAnnuel);
            this.Controls.Add(this.lblAnnee);
            this.Controls.Add(this.cbxAnnee);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Stats";
            this.Text = "Stats";
            ((System.ComponentModel.ISupportInitialize)(this.vcaannuelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource vcaannuelBindingSource;
        private System.Windows.Forms.ComboBox cbxAnnee;
        private System.Windows.Forms.Label lblAnnee;
        private System.Windows.Forms.TextBox txtCFAnnuel;
        private System.Windows.Forms.Label lblCFAnnuel;
        private System.Windows.Forms.Label lblCAAnnuel;
        private System.Windows.Forms.TextBox txtCAAnnuel;
        private System.Windows.Forms.Label lblChargesAnnuelles;
        private System.Windows.Forms.TextBox txtChargesAnnuelles;
        private System.Windows.Forms.Label lblBien;
        private System.Windows.Forms.ComboBox cbxBien;
    }
}