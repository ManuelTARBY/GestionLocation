
namespace GestionLocation
{
    partial class DateAssurance
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
            this.dateSouscri = new System.Windows.Forms.DateTimePicker();
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblDatSouscri = new System.Windows.Forms.Label();
            this.btnValider = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateSouscri
            // 
            this.dateSouscri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateSouscri.Location = new System.Drawing.Point(3, 159);
            this.dateSouscri.MaxDate = new System.DateTime(2123, 12, 31, 0, 0, 0, 0);
            this.dateSouscri.MinDate = new System.DateTime(1901, 1, 1, 0, 0, 0, 0);
            this.dateSouscri.Name = "dateSouscri";
            this.dateSouscri.Size = new System.Drawing.Size(343, 26);
            this.dateSouscri.TabIndex = 4;
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTitre.Location = new System.Drawing.Point(3, 0);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(343, 114);
            this.lblTitre.TabIndex = 6;
            this.lblTitre.Text = "Veuillez renseigner la date de souscription à l\'assurance";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDatSouscri
            // 
            this.lblDatSouscri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDatSouscri.AutoSize = true;
            this.lblDatSouscri.Location = new System.Drawing.Point(3, 136);
            this.lblDatSouscri.Name = "lblDatSouscri";
            this.lblDatSouscri.Size = new System.Drawing.Size(155, 20);
            this.lblDatSouscri.TabIndex = 7;
            this.lblDatSouscri.Text = "Date de souscription";
            // 
            // btnValider
            // 
            this.btnValider.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnValider.BackColor = System.Drawing.Color.YellowGreen;
            this.btnValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnValider.Location = new System.Drawing.Point(74, 218);
            this.btnValider.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.btnValider.MaximumSize = new System.Drawing.Size(240, 65);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(200, 60);
            this.btnValider.TabIndex = 9;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblDatSouscri, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dateSouscri, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTitre, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnValider, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(349, 314);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // DateAssurance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 338);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DateAssurance";
            this.Text = "DateAssurance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DateAssurance_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateSouscri;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblDatSouscri;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}