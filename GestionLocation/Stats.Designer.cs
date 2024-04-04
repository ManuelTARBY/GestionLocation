
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stats));
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
            this.lblTauxRemplissage = new System.Windows.Forms.Label();
            this.txtTauxRemplissage = new System.Windows.Forms.TextBox();
            this.chartCF = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartCF)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxAnnee
            // 
            this.cbxAnnee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAnnee.FormattingEnabled = true;
            this.cbxAnnee.Location = new System.Drawing.Point(25, 103);
            this.cbxAnnee.Name = "cbxAnnee";
            this.cbxAnnee.Size = new System.Drawing.Size(131, 28);
            this.cbxAnnee.TabIndex = 2;
            this.cbxAnnee.SelectedIndexChanged += new System.EventHandler(this.CbxAnnee_SelectedIndexChanged);
            // 
            // lblAnnee
            // 
            this.lblAnnee.AutoSize = true;
            this.lblAnnee.Location = new System.Drawing.Point(30, 80);
            this.lblAnnee.Name = "lblAnnee";
            this.lblAnnee.Size = new System.Drawing.Size(56, 20);
            this.lblAnnee.TabIndex = 18;
            this.lblAnnee.Text = "Année";
            // 
            // txtCFAnnuel
            // 
            this.txtCFAnnuel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtCFAnnuel.Location = new System.Drawing.Point(304, 169);
            this.txtCFAnnuel.Name = "txtCFAnnuel";
            this.txtCFAnnuel.ReadOnly = true;
            this.txtCFAnnuel.Size = new System.Drawing.Size(143, 26);
            this.txtCFAnnuel.TabIndex = 5;
            // 
            // lblCFAnnuel
            // 
            this.lblCFAnnuel.AutoSize = true;
            this.lblCFAnnuel.Location = new System.Drawing.Point(300, 144);
            this.lblCFAnnuel.Name = "lblCFAnnuel";
            this.lblCFAnnuel.Size = new System.Drawing.Size(135, 20);
            this.lblCFAnnuel.TabIndex = 19;
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
            this.txtCAAnnuel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtCAAnnuel.Location = new System.Drawing.Point(304, 45);
            this.txtCAAnnuel.Name = "txtCAAnnuel";
            this.txtCAAnnuel.ReadOnly = true;
            this.txtCAAnnuel.Size = new System.Drawing.Size(143, 26);
            this.txtCAAnnuel.TabIndex = 3;
            // 
            // lblChargesAnnuelles
            // 
            this.lblChargesAnnuelles.AutoSize = true;
            this.lblChargesAnnuelles.Location = new System.Drawing.Point(300, 82);
            this.lblChargesAnnuelles.Name = "lblChargesAnnuelles";
            this.lblChargesAnnuelles.Size = new System.Drawing.Size(141, 20);
            this.lblChargesAnnuelles.TabIndex = 9;
            this.lblChargesAnnuelles.Text = "Charges annuelles";
            // 
            // txtChargesAnnuelles
            // 
            this.txtChargesAnnuelles.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtChargesAnnuelles.Location = new System.Drawing.Point(304, 107);
            this.txtChargesAnnuelles.Name = "txtChargesAnnuelles";
            this.txtChargesAnnuelles.ReadOnly = true;
            this.txtChargesAnnuelles.Size = new System.Drawing.Size(143, 26);
            this.txtChargesAnnuelles.TabIndex = 4;
            // 
            // lblBien
            // 
            this.lblBien.AutoSize = true;
            this.lblBien.Location = new System.Drawing.Point(30, 22);
            this.lblBien.Name = "lblBien";
            this.lblBien.Size = new System.Drawing.Size(41, 20);
            this.lblBien.TabIndex = 11;
            this.lblBien.Text = "Bien";
            // 
            // cbxBien
            // 
            this.cbxBien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBien.FormattingEnabled = true;
            this.cbxBien.Location = new System.Drawing.Point(25, 45);
            this.cbxBien.Name = "cbxBien";
            this.cbxBien.Size = new System.Drawing.Size(131, 28);
            this.cbxBien.TabIndex = 1;
            this.cbxBien.SelectedIndexChanged += new System.EventHandler(this.CbxBien_SelectedIndexChanged);
            // 
            // lblTauxRemplissage
            // 
            this.lblTauxRemplissage.AutoSize = true;
            this.lblTauxRemplissage.Location = new System.Drawing.Point(300, 206);
            this.lblTauxRemplissage.Name = "lblTauxRemplissage";
            this.lblTauxRemplissage.Size = new System.Drawing.Size(101, 20);
            this.lblTauxRemplissage.TabIndex = 20;
            this.lblTauxRemplissage.Text = "Remplissage";
            // 
            // txtTauxRemplissage
            // 
            this.txtTauxRemplissage.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtTauxRemplissage.Location = new System.Drawing.Point(304, 231);
            this.txtTauxRemplissage.Name = "txtTauxRemplissage";
            this.txtTauxRemplissage.ReadOnly = true;
            this.txtTauxRemplissage.Size = new System.Drawing.Size(143, 26);
            this.txtTauxRemplissage.TabIndex = 6;
            // 
            // chartCF
            // 
            this.chartCF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartCF.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartCF.Legends.Add(legend1);
            this.chartCF.Location = new System.Drawing.Point(12, 289);
            this.chartCF.Name = "chartCF";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartCF.Series.Add(series1);
            this.chartCF.Size = new System.Drawing.Size(490, 300);
            this.chartCF.TabIndex = 21;
            this.chartCF.Text = "Graphique CF";
            // 
            // Stats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 622);
            this.Controls.Add(this.chartCF);
            this.Controls.Add(this.txtTauxRemplissage);
            this.Controls.Add(this.lblTauxRemplissage);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stats";
            ((System.ComponentModel.ISupportInitialize)(this.chartCF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Label lblTauxRemplissage;
        private System.Windows.Forms.TextBox txtTauxRemplissage;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCF;
    }
}