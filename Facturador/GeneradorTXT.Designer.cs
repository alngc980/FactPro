namespace Facturador
{
    partial class GeneradorTXT
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
            this.cbempresa = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpfecha = new System.Windows.Forms.DateTimePicker();
            this.dgvboleta = new System.Windows.Forms.DataGridView();
            this.FECHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERIE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANULADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDVENTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvfactura = new System.Windows.Forms.DataGridView();
            this.SERIEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAZON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANULA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDVENTA2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btngenerar = new System.Windows.Forms.Button();
            this.lblboleta = new System.Windows.Forms.Label();
            this.lblfactura = new System.Windows.Forms.Label();
            this.btndirectorio = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvboleta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfactura)).BeginInit();
            this.SuspendLayout();
            // 
            // cbempresa
            // 
            this.cbempresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbempresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbempresa.FormattingEnabled = true;
            this.cbempresa.Location = new System.Drawing.Point(105, 12);
            this.cbempresa.Name = "cbempresa";
            this.cbempresa.Size = new System.Drawing.Size(459, 33);
            this.cbempresa.TabIndex = 35;
            this.cbempresa.SelectedIndexChanged += new System.EventHandler(this.cbempresa_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 18);
            this.label4.TabIndex = 34;
            this.label4.Text = "EMPRESA:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(595, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 36;
            this.label1.Text = "FECHA:";
            // 
            // dtpfecha
            // 
            this.dtpfecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfecha.Location = new System.Drawing.Point(654, 13);
            this.dtpfecha.Name = "dtpfecha";
            this.dtpfecha.Size = new System.Drawing.Size(137, 32);
            this.dtpfecha.TabIndex = 37;
            this.dtpfecha.ValueChanged += new System.EventHandler(this.dtpfecha_ValueChanged);
            this.dtpfecha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfecha_KeyDown);
            // 
            // dgvboleta
            // 
            this.dgvboleta.AllowUserToAddRows = false;
            this.dgvboleta.AllowUserToDeleteRows = false;
            this.dgvboleta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvboleta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FECHA,
            this.TIPO,
            this.SERIE,
            this.DOC,
            this.TOTAL,
            this.ANULADO,
            this.IDVENTA});
            this.dgvboleta.Location = new System.Drawing.Point(12, 79);
            this.dgvboleta.Name = "dgvboleta";
            this.dgvboleta.ReadOnly = true;
            this.dgvboleta.RowHeadersVisible = false;
            this.dgvboleta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvboleta.Size = new System.Drawing.Size(540, 422);
            this.dgvboleta.TabIndex = 39;
            // 
            // FECHA
            // 
            this.FECHA.HeaderText = "FECHA";
            this.FECHA.Name = "FECHA";
            this.FECHA.ReadOnly = true;
            this.FECHA.Width = 80;
            // 
            // TIPO
            // 
            this.TIPO.HeaderText = "TIPODOC";
            this.TIPO.Name = "TIPO";
            this.TIPO.ReadOnly = true;
            this.TIPO.Width = 60;
            // 
            // SERIE
            // 
            this.SERIE.HeaderText = "SERIE - CORREL";
            this.SERIE.Name = "SERIE";
            this.SERIE.ReadOnly = true;
            // 
            // DOC
            // 
            this.DOC.HeaderText = "DOC";
            this.DOC.Name = "DOC";
            this.DOC.ReadOnly = true;
            this.DOC.Width = 60;
            // 
            // TOTAL
            // 
            this.TOTAL.HeaderText = "TOTAL";
            this.TOTAL.Name = "TOTAL";
            this.TOTAL.ReadOnly = true;
            this.TOTAL.Width = 60;
            // 
            // ANULADO
            // 
            this.ANULADO.HeaderText = "ANULADO";
            this.ANULADO.Name = "ANULADO";
            this.ANULADO.ReadOnly = true;
            // 
            // IDVENTA
            // 
            this.IDVENTA.HeaderText = "IDVENTA";
            this.IDVENTA.Name = "IDVENTA";
            this.IDVENTA.ReadOnly = true;
            this.IDVENTA.Width = 50;
            // 
            // dgvfactura
            // 
            this.dgvfactura.AllowUserToAddRows = false;
            this.dgvfactura.AllowUserToDeleteRows = false;
            this.dgvfactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvfactura.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SERIEE,
            this.RUC,
            this.RAZON,
            this.TOTA,
            this.ANULA,
            this.IDVENTA2});
            this.dgvfactura.Location = new System.Drawing.Point(569, 79);
            this.dgvfactura.Name = "dgvfactura";
            this.dgvfactura.ReadOnly = true;
            this.dgvfactura.RowHeadersVisible = false;
            this.dgvfactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvfactura.Size = new System.Drawing.Size(573, 422);
            this.dgvfactura.TabIndex = 40;
            this.dgvfactura.DoubleClick += new System.EventHandler(this.dgvfactura_DoubleClick);
            // 
            // SERIEE
            // 
            this.SERIEE.HeaderText = "SERIE-CORREL";
            this.SERIEE.Name = "SERIEE";
            this.SERIEE.ReadOnly = true;
            // 
            // RUC
            // 
            this.RUC.HeaderText = "RUC";
            this.RUC.Name = "RUC";
            this.RUC.ReadOnly = true;
            // 
            // RAZON
            // 
            this.RAZON.HeaderText = "RAZON SOCIAL";
            this.RAZON.Name = "RAZON";
            this.RAZON.ReadOnly = true;
            // 
            // TOTA
            // 
            this.TOTA.HeaderText = "TOTAL";
            this.TOTA.Name = "TOTA";
            this.TOTA.ReadOnly = true;
            // 
            // ANULA
            // 
            this.ANULA.HeaderText = "ANULADO";
            this.ANULA.Name = "ANULA";
            this.ANULA.ReadOnly = true;
            // 
            // IDVENTA2
            // 
            this.IDVENTA2.HeaderText = "IDVENTA2";
            this.IDVENTA2.Name = "IDVENTA2";
            this.IDVENTA2.ReadOnly = true;
            this.IDVENTA2.Width = 50;
            // 
            // btngenerar
            // 
            this.btngenerar.Location = new System.Drawing.Point(827, 10);
            this.btngenerar.Name = "btngenerar";
            this.btngenerar.Size = new System.Drawing.Size(91, 35);
            this.btngenerar.TabIndex = 41;
            this.btngenerar.Text = "GENERAR TXT";
            this.btngenerar.UseVisualStyleBackColor = true;
            this.btngenerar.Click += new System.EventHandler(this.btngenerar_Click);
            // 
            // lblboleta
            // 
            this.lblboleta.AutoSize = true;
            this.lblboleta.BackColor = System.Drawing.SystemColors.Control;
            this.lblboleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblboleta.ForeColor = System.Drawing.Color.Red;
            this.lblboleta.Location = new System.Drawing.Point(12, 56);
            this.lblboleta.Name = "lblboleta";
            this.lblboleta.Size = new System.Drawing.Size(57, 20);
            this.lblboleta.TabIndex = 42;
            this.lblboleta.Text = "label2";
            // 
            // lblfactura
            // 
            this.lblfactura.AutoSize = true;
            this.lblfactura.BackColor = System.Drawing.SystemColors.Control;
            this.lblfactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfactura.ForeColor = System.Drawing.Color.Red;
            this.lblfactura.Location = new System.Drawing.Point(565, 56);
            this.lblfactura.Name = "lblfactura";
            this.lblfactura.Size = new System.Drawing.Size(57, 20);
            this.lblfactura.TabIndex = 43;
            this.lblfactura.Text = "label2";
            // 
            // btndirectorio
            // 
            this.btndirectorio.Location = new System.Drawing.Point(924, 10);
            this.btndirectorio.Name = "btndirectorio";
            this.btndirectorio.Size = new System.Drawing.Size(85, 35);
            this.btndirectorio.TabIndex = 44;
            this.btndirectorio.Text = "DIRECTORIO";
            this.btndirectorio.UseVisualStyleBackColor = true;
            this.btndirectorio.Click += new System.EventHandler(this.btndirectorio_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1015, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 35);
            this.button1.TabIndex = 45;
            this.button1.Text = "DIRECTORIO TXT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GeneradorTXT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 535);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btndirectorio);
            this.Controls.Add(this.lblfactura);
            this.Controls.Add(this.lblboleta);
            this.Controls.Add(this.btngenerar);
            this.Controls.Add(this.dgvfactura);
            this.Controls.Add(this.dgvboleta);
            this.Controls.Add(this.dtpfecha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbempresa);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GeneradorTXT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeneradorTXT";
            this.Load += new System.EventHandler(this.GeneradorTXT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvboleta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfactura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbempresa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpfecha;
        internal System.Windows.Forms.DataGridView dgvboleta;
        internal System.Windows.Forms.DataGridView dgvfactura;
        private System.Windows.Forms.Button btngenerar;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ANULADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDVENTA;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RUC;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAZON;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ANULA;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDVENTA2;
        private System.Windows.Forms.Label lblboleta;
        private System.Windows.Forms.Label lblfactura;
        private System.Windows.Forms.Button btndirectorio;
        private System.Windows.Forms.Button button1;
    }
}