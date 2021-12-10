namespace Facturador
{
    partial class FactSUNAT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactSUNAT));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.temporizador = new System.Windows.Forms.Timer(this.components);
            this.txt1 = new System.Windows.Forms.Label();
            this.txt2 = new System.Windows.Forms.Label();
            this.txt3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtdestino = new System.Windows.Forms.TextBox();
            this.txtorigen = new System.Windows.Forms.TextBox();
            this.btnrutas = new System.Windows.Forms.Button();
            this.btnlimpiar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnenviar = new System.Windows.Forms.Button();
            this.btngen = new System.Windows.Forms.Button();
            this.btnenviarM = new System.Windows.Forms.Button();
            this.btngenM = new System.Windows.Forms.Button();
            this.btngenyenviarM = new System.Windows.Forms.Button();
            this.btnactualizarlista = new System.Windows.Forms.Button();
            this.dgvlistaComprobantes = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtmensaje = new System.Windows.Forms.TextBox();
            this.lbltemp = new System.Windows.Forms.Label();
            this.Temp2 = new System.Windows.Forms.Timer(this.components);
            this.lbltem2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlistaComprobantes)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // temporizador
            // 
            this.temporizador.Tick += new System.EventHandler(this.temporizador_Tick);
            // 
            // txt1
            // 
            this.txt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt1.AutoSize = true;
            this.txt1.BackColor = System.Drawing.Color.Transparent;
            this.txt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt1.ForeColor = System.Drawing.Color.Blue;
            this.txt1.Location = new System.Drawing.Point(138, 1);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(21, 22);
            this.txt1.TabIndex = 13;
            this.txt1.Text = "0";
            // 
            // txt2
            // 
            this.txt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt2.AutoSize = true;
            this.txt2.BackColor = System.Drawing.Color.Transparent;
            this.txt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt2.ForeColor = System.Drawing.Color.Blue;
            this.txt2.Location = new System.Drawing.Point(451, 1);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(21, 22);
            this.txt2.TabIndex = 13;
            this.txt2.Text = "0";
            // 
            // txt3
            // 
            this.txt3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt3.AutoSize = true;
            this.txt3.BackColor = System.Drawing.Color.Transparent;
            this.txt3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt3.ForeColor = System.Drawing.Color.Blue;
            this.txt3.Location = new System.Drawing.Point(659, 1);
            this.txt3.Name = "txt3";
            this.txt3.Size = new System.Drawing.Size(21, 22);
            this.txt3.TabIndex = 13;
            this.txt3.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "POR PROCESAR";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(598, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "TOTAL";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(295, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "ENVIADOS A SUNAT";
            // 
            // txtdestino
            // 
            this.txtdestino.BackColor = System.Drawing.Color.Red;
            this.txtdestino.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdestino.ForeColor = System.Drawing.Color.Yellow;
            this.txtdestino.Location = new System.Drawing.Point(156, 251);
            this.txtdestino.Name = "txtdestino";
            this.txtdestino.Size = new System.Drawing.Size(678, 35);
            this.txtdestino.TabIndex = 25;
            this.txtdestino.Text = "D:\\SFS_v1.2\\sunat_archivos\\sfs\\DATA\\BACKUP\\";
            this.txtdestino.Visible = false;
            // 
            // txtorigen
            // 
            this.txtorigen.BackColor = System.Drawing.Color.Red;
            this.txtorigen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtorigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtorigen.ForeColor = System.Drawing.Color.Yellow;
            this.txtorigen.Location = new System.Drawing.Point(156, 202);
            this.txtorigen.Name = "txtorigen";
            this.txtorigen.Size = new System.Drawing.Size(678, 35);
            this.txtorigen.TabIndex = 26;
            this.txtorigen.Text = "D:\\SFS_v1.2\\sunat_archivos\\sfs\\DATA\\";
            this.txtorigen.Visible = false;
            // 
            // btnrutas
            // 
            this.btnrutas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnrutas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnrutas.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnrutas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrutas.Location = new System.Drawing.Point(838, 309);
            this.btnrutas.Name = "btnrutas";
            this.btnrutas.Size = new System.Drawing.Size(90, 49);
            this.btnrutas.TabIndex = 24;
            this.btnrutas.Text = "RUTAS DIRECTORIO";
            this.btnrutas.UseVisualStyleBackColor = false;
            this.btnrutas.Click += new System.EventHandler(this.btnrutas_Click);
            // 
            // btnlimpiar
            // 
            this.btnlimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnlimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnlimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnlimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlimpiar.Location = new System.Drawing.Point(838, 178);
            this.btnlimpiar.Name = "btnlimpiar";
            this.btnlimpiar.Size = new System.Drawing.Size(90, 46);
            this.btnlimpiar.TabIndex = 23;
            this.btnlimpiar.Text = "LIMPIAR";
            this.btnlimpiar.UseVisualStyleBackColor = false;
            this.btnlimpiar.Click += new System.EventHandler(this.btnlimpiar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // btnenviar
            // 
            this.btnenviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnenviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnenviar.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnenviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnenviar.Location = new System.Drawing.Point(838, 124);
            this.btnenviar.Name = "btnenviar";
            this.btnenviar.Size = new System.Drawing.Size(90, 48);
            this.btnenviar.TabIndex = 21;
            this.btnenviar.Text = "ENVIAR SUNAT";
            this.btnenviar.UseVisualStyleBackColor = false;
            this.btnenviar.Click += new System.EventHandler(this.btnenviar_Click);
            // 
            // btngen
            // 
            this.btngen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btngen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btngen.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btngen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngen.Location = new System.Drawing.Point(838, 69);
            this.btngen.Name = "btngen";
            this.btngen.Size = new System.Drawing.Size(90, 49);
            this.btngen.TabIndex = 20;
            this.btngen.Text = "GENERAR XML";
            this.btngen.UseVisualStyleBackColor = false;
            this.btngen.Click += new System.EventHandler(this.btngen_Click);
            // 
            // btnenviarM
            // 
            this.btnenviarM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnenviarM.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnenviarM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnenviarM.Location = new System.Drawing.Point(634, 11);
            this.btnenviarM.Name = "btnenviarM";
            this.btnenviarM.Size = new System.Drawing.Size(82, 51);
            this.btnenviarM.TabIndex = 19;
            this.btnenviarM.Text = "ENVIAR CPE MASIVO";
            this.btnenviarM.UseVisualStyleBackColor = false;
            this.btnenviarM.Click += new System.EventHandler(this.btnenviarM_Click);
            // 
            // btngenM
            // 
            this.btngenM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btngenM.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btngenM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenM.Location = new System.Drawing.Point(546, 11);
            this.btngenM.Name = "btngenM";
            this.btngenM.Size = new System.Drawing.Size(82, 51);
            this.btngenM.TabIndex = 18;
            this.btngenM.Text = "GENERAR XML MASIVO";
            this.btngenM.UseVisualStyleBackColor = false;
            this.btngenM.Click += new System.EventHandler(this.btngenM_Click);
            // 
            // btngenyenviarM
            // 
            this.btngenyenviarM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btngenyenviarM.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btngenyenviarM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenyenviarM.Location = new System.Drawing.Point(722, 11);
            this.btngenyenviarM.Name = "btngenyenviarM";
            this.btngenyenviarM.Size = new System.Drawing.Size(82, 51);
            this.btngenyenviarM.TabIndex = 17;
            this.btngenyenviarM.Text = "GENERAR Y ENVIAR MASIVO";
            this.btngenyenviarM.UseVisualStyleBackColor = false;
            this.btngenyenviarM.Click += new System.EventHandler(this.btngenyenviarM_Click);
            // 
            // btnactualizarlista
            // 
            this.btnactualizarlista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnactualizarlista.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnactualizarlista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnactualizarlista.Location = new System.Drawing.Point(458, 11);
            this.btnactualizarlista.Name = "btnactualizarlista";
            this.btnactualizarlista.Size = new System.Drawing.Size(82, 51);
            this.btnactualizarlista.TabIndex = 16;
            this.btnactualizarlista.Text = "ACTUALIZAR LISTA";
            this.btnactualizarlista.UseVisualStyleBackColor = false;
            this.btnactualizarlista.Click += new System.EventHandler(this.btnactualizarlista_Click);
            // 
            // dgvlistaComprobantes
            // 
            this.dgvlistaComprobantes.AllowUserToAddRows = false;
            this.dgvlistaComprobantes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvlistaComprobantes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvlistaComprobantes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvlistaComprobantes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvlistaComprobantes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvlistaComprobantes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvlistaComprobantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvlistaComprobantes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvlistaComprobantes.EnableHeadersVisualStyles = false;
            this.dgvlistaComprobantes.Location = new System.Drawing.Point(3, 69);
            this.dgvlistaComprobantes.Name = "dgvlistaComprobantes";
            this.dgvlistaComprobantes.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvlistaComprobantes.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvlistaComprobantes.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvlistaComprobantes.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvlistaComprobantes.RowTemplate.Height = 30;
            this.dgvlistaComprobantes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvlistaComprobantes.Size = new System.Drawing.Size(832, 494);
            this.dgvlistaComprobantes.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.Controls.Add(this.txt1);
            this.panel1.Controls.Add(this.txt2);
            this.panel1.Controls.Add(this.txt3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(3, 603);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 24);
            this.panel1.TabIndex = 27;
            // 
            // txtmensaje
            // 
            this.txtmensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtmensaje.BackColor = System.Drawing.Color.Red;
            this.txtmensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtmensaje.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmensaje.ForeColor = System.Drawing.Color.Yellow;
            this.txtmensaje.Location = new System.Drawing.Point(3, 569);
            this.txtmensaje.Name = "txtmensaje";
            this.txtmensaje.Size = new System.Drawing.Size(831, 30);
            this.txtmensaje.TabIndex = 28;
            // 
            // lbltemp
            // 
            this.lbltemp.AutoSize = true;
            this.lbltemp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbltemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltemp.ForeColor = System.Drawing.Color.Blue;
            this.lbltemp.Location = new System.Drawing.Point(830, 12);
            this.lbltemp.Name = "lbltemp";
            this.lbltemp.Size = new System.Drawing.Size(89, 46);
            this.lbltemp.TabIndex = 29;
            this.lbltemp.Text = "600";
            this.lbltemp.Click += new System.EventHandler(this.lbltemp_Click);
            // 
            // Temp2
            // 
            this.Temp2.Tick += new System.EventHandler(this.Temp2_Tick);
            // 
            // lbltem2
            // 
            this.lbltem2.AutoSize = true;
            this.lbltem2.Enabled = false;
            this.lbltem2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltem2.ForeColor = System.Drawing.Color.Blue;
            this.lbltem2.Location = new System.Drawing.Point(866, 239);
            this.lbltem2.Name = "lbltem2";
            this.lbltem2.Size = new System.Drawing.Size(43, 46);
            this.lbltem2.TabIndex = 30;
            this.lbltem2.Text = "0";
            this.lbltem2.Visible = false;
            this.lbltem2.TextChanged += new System.EventHandler(this.lbltem2_TextChanged);
            this.lbltem2.Click += new System.EventHandler(this.lbltem2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 4;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(838, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 51);
            this.button1.TabIndex = 31;
            this.button1.Text = "GENERAR TXT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(168, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 18);
            this.label4.TabIndex = 32;
            this.label4.Text = "EMPRESA:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(168, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(280, 33);
            this.comboBox1.TabIndex = 33;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.ValueMemberChanged += new System.EventHandler(this.comboBox1_ValueMemberChanged);
            // 
            // FactSUNAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(931, 630);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbltem2);
            this.Controls.Add(this.lbltemp);
            this.Controls.Add(this.txtmensaje);
            this.Controls.Add(this.txtdestino);
            this.Controls.Add(this.txtorigen);
            this.Controls.Add(this.btnrutas);
            this.Controls.Add(this.btnlimpiar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnenviar);
            this.Controls.Add(this.btngen);
            this.Controls.Add(this.btnenviarM);
            this.Controls.Add(this.btngenM);
            this.Controls.Add(this.btngenyenviarM);
            this.Controls.Add(this.btnactualizarlista);
            this.Controls.Add(this.dgvlistaComprobantes);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FactSUNAT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FACTURADOR - > ENVIO DE COMPROBANTES";
            this.Load += new System.EventHandler(this.FactSUNAT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlistaComprobantes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer temporizador;
        private System.Windows.Forms.Label txt1;
        private System.Windows.Forms.Label txt2;
        private System.Windows.Forms.Label txt3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtdestino;
        private System.Windows.Forms.TextBox txtorigen;
        private System.Windows.Forms.Button btnrutas;
        private System.Windows.Forms.Button btnlimpiar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnenviar;
        private System.Windows.Forms.Button btngen;
        private System.Windows.Forms.Button btnenviarM;
        private System.Windows.Forms.Button btngenM;
        private System.Windows.Forms.Button btngenyenviarM;
        private System.Windows.Forms.Button btnactualizarlista;
        private System.Windows.Forms.DataGridView dgvlistaComprobantes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtmensaje;
        private System.Windows.Forms.Label lbltemp;
        private System.Windows.Forms.Timer Temp2;
        private System.Windows.Forms.Label lbltem2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}