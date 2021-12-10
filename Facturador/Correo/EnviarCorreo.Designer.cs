namespace Facturador
{
    partial class EnviarCorreo
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
            this.label8 = new System.Windows.Forms.Label();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.btncancelar = new System.Windows.Forms.Button();
            this.btnenviar = new System.Windows.Forms.Button();
            this.btnabrir = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbmensaje = new System.Windows.Forms.RichTextBox();
            this.txtadjuntos = new System.Windows.Forms.TextBox();
            this.txtasunto = new System.Windows.Forms.TextBox();
            this.txtcc = new System.Windows.Forms.TextBox();
            this.txtpara = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtremitente = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(547, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Tu nombre:";
            // 
            // txtnombre
            // 
            this.txtnombre.Location = new System.Drawing.Point(551, 34);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(339, 20);
            this.txtnombre.TabIndex = 36;
            // 
            // btncancelar
            // 
            this.btncancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.btncancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen;
            this.btncancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancelar.ForeColor = System.Drawing.Color.DarkRed;
            this.btncancelar.Location = new System.Drawing.Point(22, 383);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(156, 134);
            this.btncancelar.TabIndex = 35;
            this.btncancelar.Text = "Cancelar";
            this.btncancelar.UseVisualStyleBackColor = true;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // btnenviar
            // 
            this.btnenviar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.btnenviar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen;
            this.btnenviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnenviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnenviar.Location = new System.Drawing.Point(22, 252);
            this.btnenviar.Name = "btnenviar";
            this.btnenviar.Size = new System.Drawing.Size(156, 125);
            this.btnenviar.TabIndex = 34;
            this.btnenviar.Text = "Enviar";
            this.btnenviar.UseVisualStyleBackColor = true;
            this.btnenviar.Click += new System.EventHandler(this.btnenviar_Click);
            // 
            // btnabrir
            // 
            this.btnabrir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.btnabrir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen;
            this.btnabrir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnabrir.Location = new System.Drawing.Point(742, 194);
            this.btnabrir.Name = "btnabrir";
            this.btnabrir.Size = new System.Drawing.Size(55, 26);
            this.btnabrir.TabIndex = 33;
            this.btnabrir.Text = "...";
            this.btnabrir.UseVisualStyleBackColor = true;
            this.btnabrir.Click += new System.EventHandler(this.btnabrir_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 229);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Mensaje:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Archivos adjuntos (separados por \'|\'):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Asunto:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(414, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "CC:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Destinatarios (separados por \', \'):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Tu contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Tu correo:";
            // 
            // rtbmensaje
            // 
            this.rtbmensaje.Location = new System.Drawing.Point(199, 252);
            this.rtbmensaje.Name = "rtbmensaje";
            this.rtbmensaje.Size = new System.Drawing.Size(691, 333);
            this.rtbmensaje.TabIndex = 25;
            this.rtbmensaje.Text = "";
            // 
            // txtadjuntos
            // 
            this.txtadjuntos.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtadjuntos.Location = new System.Drawing.Point(22, 200);
            this.txtadjuntos.Name = "txtadjuntos";
            this.txtadjuntos.ReadOnly = true;
            this.txtadjuntos.Size = new System.Drawing.Size(670, 20);
            this.txtadjuntos.TabIndex = 24;
            // 
            // txtasunto
            // 
            this.txtasunto.Location = new System.Drawing.Point(22, 144);
            this.txtasunto.Name = "txtasunto";
            this.txtasunto.Size = new System.Drawing.Size(868, 20);
            this.txtasunto.TabIndex = 23;
            // 
            // txtcc
            // 
            this.txtcc.Location = new System.Drawing.Point(418, 86);
            this.txtcc.Name = "txtcc";
            this.txtcc.Size = new System.Drawing.Size(472, 20);
            this.txtcc.TabIndex = 22;
            // 
            // txtpara
            // 
            this.txtpara.Location = new System.Drawing.Point(22, 86);
            this.txtpara.Name = "txtpara";
            this.txtpara.Size = new System.Drawing.Size(358, 20);
            this.txtpara.TabIndex = 21;
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(280, 34);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(252, 20);
            this.txtpassword.TabIndex = 20;
            this.txtpassword.UseSystemPasswordChar = true;
            // 
            // txtremitente
            // 
            this.txtremitente.Location = new System.Drawing.Point(22, 34);
            this.txtremitente.Name = "txtremitente";
            this.txtremitente.Size = new System.Drawing.Size(238, 20);
            this.txtremitente.TabIndex = 19;
            // 
            // EnviarCorreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 610);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtnombre);
            this.Controls.Add(this.btncancelar);
            this.Controls.Add(this.btnenviar);
            this.Controls.Add(this.btnabrir);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbmensaje);
            this.Controls.Add(this.txtadjuntos);
            this.Controls.Add(this.txtasunto);
            this.Controls.Add(this.txtcc);
            this.Controls.Add(this.txtpara);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtremitente);
            this.Name = "EnviarCorreo";
            this.Text = "EnviarCorreo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.Button btncancelar;
        private System.Windows.Forms.Button btnenviar;
        private System.Windows.Forms.Button btnabrir;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbmensaje;
        private System.Windows.Forms.TextBox txtadjuntos;
        private System.Windows.Forms.TextBox txtasunto;
        private System.Windows.Forms.TextBox txtcc;
        private System.Windows.Forms.TextBox txtpara;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtremitente;
    }
}