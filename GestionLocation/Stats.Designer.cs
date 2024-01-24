
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.annéeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vcaannuelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gestionlocationDataSet = new GestionLocation.gestionlocationDataSet();
            this.v_ca_annuelTableAdapter = new GestionLocation.gestionlocationDataSetTableAdapters.v_ca_annuelTableAdapter();
            this.cbxListBiens = new System.Windows.Forms.ComboBox();
            this.cbxAnnee = new System.Windows.Forms.ComboBox();
            this.lblAnnee = new System.Windows.Forms.Label();
            this.txtCFAnnuel = new System.Windows.Forms.TextBox();
            this.lblCFAnnuel = new System.Windows.Forms.Label();
            this.lblCAAnnuel = new System.Windows.Forms.Label();
            this.txtCAAnnuel = new System.Windows.Forms.TextBox();
            this.lblChargesAnnuelles = new System.Windows.Forms.Label();
            this.txtChargesAnnuelles = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vcaannuelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gestionlocationDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.annéeDataGridViewTextBoxColumn,
            this.cADataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.vcaannuelBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(305, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(483, 141);
            this.dataGridView1.TabIndex = 0;
            // 
            // annéeDataGridViewTextBoxColumn
            // 
            this.annéeDataGridViewTextBoxColumn.DataPropertyName = "Année";
            this.annéeDataGridViewTextBoxColumn.HeaderText = "Année";
            this.annéeDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.annéeDataGridViewTextBoxColumn.Name = "annéeDataGridViewTextBoxColumn";
            this.annéeDataGridViewTextBoxColumn.ReadOnly = true;
            this.annéeDataGridViewTextBoxColumn.Width = 150;
            // 
            // cADataGridViewTextBoxColumn
            // 
            this.cADataGridViewTextBoxColumn.DataPropertyName = "CA";
            this.cADataGridViewTextBoxColumn.HeaderText = "CA";
            this.cADataGridViewTextBoxColumn.MinimumWidth = 8;
            this.cADataGridViewTextBoxColumn.Name = "cADataGridViewTextBoxColumn";
            this.cADataGridViewTextBoxColumn.ReadOnly = true;
            this.cADataGridViewTextBoxColumn.Width = 150;
            // 
            // vcaannuelBindingSource
            // 
            this.vcaannuelBindingSource.DataMember = "v_ca_annuel";
            this.vcaannuelBindingSource.DataSource = this.gestionlocationDataSet;
            // 
            // gestionlocationDataSet
            // 
            this.gestionlocationDataSet.DataSetName = "gestionlocationDataSet";
            this.gestionlocationDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // v_ca_annuelTableAdapter
            // 
            this.v_ca_annuelTableAdapter.ClearBeforeFill = true;
            // 
            // cbxListBiens
            // 
            this.cbxListBiens.FormattingEnabled = true;
            this.cbxListBiens.Location = new System.Drawing.Point(40, 12);
            this.cbxListBiens.Name = "cbxListBiens";
            this.cbxListBiens.Size = new System.Drawing.Size(225, 28);
            this.cbxListBiens.TabIndex = 1;
            // 
            // cbxAnnee
            // 
            this.cbxAnnee.FormattingEnabled = true;
            this.cbxAnnee.Location = new System.Drawing.Point(40, 228);
            this.cbxAnnee.Name = "cbxAnnee";
            this.cbxAnnee.Size = new System.Drawing.Size(211, 28);
            this.cbxAnnee.TabIndex = 2;
            this.cbxAnnee.SelectedIndexChanged += new System.EventHandler(this.CbxAnnee_SelectedIndexChanged);
            // 
            // lblAnnee
            // 
            this.lblAnnee.AutoSize = true;
            this.lblAnnee.Location = new System.Drawing.Point(45, 205);
            this.lblAnnee.Name = "lblAnnee";
            this.lblAnnee.Size = new System.Drawing.Size(56, 20);
            this.lblAnnee.TabIndex = 3;
            this.lblAnnee.Text = "Année";
            // 
            // txtCFAnnuel
            // 
            this.txtCFAnnuel.Location = new System.Drawing.Point(319, 344);
            this.txtCFAnnuel.Name = "txtCFAnnuel";
            this.txtCFAnnuel.Size = new System.Drawing.Size(143, 26);
            this.txtCFAnnuel.TabIndex = 4;
            // 
            // lblCFAnnuel
            // 
            this.lblCFAnnuel.AutoSize = true;
            this.lblCFAnnuel.Location = new System.Drawing.Point(315, 319);
            this.lblCFAnnuel.Name = "lblCFAnnuel";
            this.lblCFAnnuel.Size = new System.Drawing.Size(135, 20);
            this.lblCFAnnuel.TabIndex = 5;
            this.lblCFAnnuel.Text = "Cash Flow annuel";
            // 
            // lblCAAnnuel
            // 
            this.lblCAAnnuel.AutoSize = true;
            this.lblCAAnnuel.Location = new System.Drawing.Point(315, 203);
            this.lblCAAnnuel.Name = "lblCAAnnuel";
            this.lblCAAnnuel.Size = new System.Drawing.Size(83, 20);
            this.lblCAAnnuel.TabIndex = 7;
            this.lblCAAnnuel.Text = "CA annuel";
            // 
            // txtCAAnnuel
            // 
            this.txtCAAnnuel.Location = new System.Drawing.Point(319, 228);
            this.txtCAAnnuel.Name = "txtCAAnnuel";
            this.txtCAAnnuel.Size = new System.Drawing.Size(143, 26);
            this.txtCAAnnuel.TabIndex = 6;
            // 
            // lblChargesAnnuelles
            // 
            this.lblChargesAnnuelles.AutoSize = true;
            this.lblChargesAnnuelles.Location = new System.Drawing.Point(315, 261);
            this.lblChargesAnnuelles.Name = "lblChargesAnnuelles";
            this.lblChargesAnnuelles.Size = new System.Drawing.Size(141, 20);
            this.lblChargesAnnuelles.TabIndex = 9;
            this.lblChargesAnnuelles.Text = "Charges annuelles";
            // 
            // txtChargesAnnuelles
            // 
            this.txtChargesAnnuelles.Location = new System.Drawing.Point(319, 286);
            this.txtChargesAnnuelles.Name = "txtChargesAnnuelles";
            this.txtChargesAnnuelles.Size = new System.Drawing.Size(143, 26);
            this.txtChargesAnnuelles.TabIndex = 8;
            // 
            // Stats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblChargesAnnuelles);
            this.Controls.Add(this.txtChargesAnnuelles);
            this.Controls.Add(this.lblCAAnnuel);
            this.Controls.Add(this.txtCAAnnuel);
            this.Controls.Add(this.lblCFAnnuel);
            this.Controls.Add(this.txtCFAnnuel);
            this.Controls.Add(this.lblAnnee);
            this.Controls.Add(this.cbxAnnee);
            this.Controls.Add(this.cbxListBiens);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Stats";
            this.Text = "Stats";
            this.Load += new System.EventHandler(this.Stats_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vcaannuelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gestionlocationDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private gestionlocationDataSet gestionlocationDataSet;
        private System.Windows.Forms.BindingSource vcaannuelBindingSource;
        private gestionlocationDataSetTableAdapters.v_ca_annuelTableAdapter v_ca_annuelTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn annéeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cADataGridViewTextBoxColumn;
        private System.Windows.Forms.ComboBox cbxListBiens;
        private System.Windows.Forms.ComboBox cbxAnnee;
        private System.Windows.Forms.Label lblAnnee;
        private System.Windows.Forms.TextBox txtCFAnnuel;
        private System.Windows.Forms.Label lblCFAnnuel;
        private System.Windows.Forms.Label lblCAAnnuel;
        private System.Windows.Forms.TextBox txtCAAnnuel;
        private System.Windows.Forms.Label lblChargesAnnuelles;
        private System.Windows.Forms.TextBox txtChargesAnnuelles;
    }
}