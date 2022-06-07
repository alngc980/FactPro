<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sunat
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnEnviar = New System.Windows.Forms.Button()
        Me.dtpfecha = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtbuscar = New System.Windows.Forms.TextBox()
        Me.dgvlista = New System.Windows.Forms.DataGridView()
        Me.btnenviarcorreo = New System.Windows.Forms.Button()
        Me.btngenerarResumen = New System.Windows.Forms.Button()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.ctncerrar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblseriecorrelativo = New System.Windows.Forms.Label()
        Me.btnguardar = New System.Windows.Forms.Button()
        Me.dtpfechanueva = New System.Windows.Forms.DateTimePicker()
        Me.btnValidarResumen = New System.Windows.Forms.Button()
        Me.cbdocs = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblcantdocs = New System.Windows.Forms.Label()
        Me.btnEnvioMasivo = New System.Windows.Forms.Button()
        Me.btnvalidarcpe = New System.Windows.Forms.Button()
        Me.panelEmpresa = New System.Windows.Forms.Panel()
        Me.dgvempresas = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblempresa = New System.Windows.Forms.Label()
        Me.txtmensaje = New System.Windows.Forms.TextBox()
        Me.btnValidarCpeMasivo = New System.Windows.Forms.Button()
        Me.btnbaja = New System.Windows.Forms.Button()
        Me.btnExportar = New System.Windows.Forms.Button()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        CType(Me.dgvlista, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel1.SuspendLayout()
        Me.panelEmpresa.SuspendLayout()
        CType(Me.dgvempresas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEnviar
        '
        Me.btnEnviar.Location = New System.Drawing.Point(882, 39)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(75, 32)
        Me.btnEnviar.TabIndex = 1
        Me.btnEnviar.Text = "Enviar"
        Me.btnEnviar.UseVisualStyleBackColor = True
        '
        'dtpfecha
        '
        Me.dtpfecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfecha.Location = New System.Drawing.Point(780, 9)
        Me.dtpfecha.Name = "dtpfecha"
        Me.dtpfecha.Size = New System.Drawing.Size(86, 20)
        Me.dtpfecha.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(607, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "BUSCAR"
        '
        'txtbuscar
        '
        Me.txtbuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtbuscar.Location = New System.Drawing.Point(664, 9)
        Me.txtbuscar.Name = "txtbuscar"
        Me.txtbuscar.Size = New System.Drawing.Size(108, 20)
        Me.txtbuscar.TabIndex = 6
        '
        'dgvlista
        '
        Me.dgvlista.AllowUserToAddRows = False
        Me.dgvlista.AllowUserToDeleteRows = False
        Me.dgvlista.BackgroundColor = System.Drawing.Color.White
        Me.dgvlista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvlista.GridColor = System.Drawing.Color.DarkGray
        Me.dgvlista.Location = New System.Drawing.Point(13, 39)
        Me.dgvlista.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvlista.Name = "dgvlista"
        Me.dgvlista.ReadOnly = True
        Me.dgvlista.RowHeadersVisible = False
        Me.dgvlista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvlista.Size = New System.Drawing.Size(853, 376)
        Me.dgvlista.TabIndex = 13
        '
        'btnenviarcorreo
        '
        Me.btnenviarcorreo.Location = New System.Drawing.Point(882, 378)
        Me.btnenviarcorreo.Name = "btnenviarcorreo"
        Me.btnenviarcorreo.Size = New System.Drawing.Size(75, 37)
        Me.btnenviarcorreo.TabIndex = 14
        Me.btnenviarcorreo.Text = "Enviar Correo"
        Me.btnenviarcorreo.UseVisualStyleBackColor = True
        '
        'btngenerarResumen
        '
        Me.btngenerarResumen.Enabled = False
        Me.btngenerarResumen.Location = New System.Drawing.Point(882, 214)
        Me.btngenerarResumen.Name = "btngenerarResumen"
        Me.btngenerarResumen.Size = New System.Drawing.Size(75, 38)
        Me.btngenerarResumen.TabIndex = 16
        Me.btngenerarResumen.Text = "Gen Resumen"
        Me.btngenerarResumen.UseVisualStyleBackColor = True
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel1.Controls.Add(Me.ctncerrar)
        Me.panel1.Controls.Add(Me.Label3)
        Me.panel1.Controls.Add(Me.lblseriecorrelativo)
        Me.panel1.Controls.Add(Me.btnguardar)
        Me.panel1.Controls.Add(Me.dtpfechanueva)
        Me.panel1.Location = New System.Drawing.Point(270, 123)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(324, 145)
        Me.panel1.TabIndex = 18
        Me.panel1.Visible = False
        '
        'ctncerrar
        '
        Me.ctncerrar.BackColor = System.Drawing.Color.Red
        Me.ctncerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ctncerrar.Location = New System.Drawing.Point(290, 3)
        Me.ctncerrar.Name = "ctncerrar"
        Me.ctncerrar.Size = New System.Drawing.Size(31, 26)
        Me.ctncerrar.TabIndex = 21
        Me.ctncerrar.Text = "X"
        Me.ctncerrar.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "FECHA:"
        '
        'lblseriecorrelativo
        '
        Me.lblseriecorrelativo.AutoSize = True
        Me.lblseriecorrelativo.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblseriecorrelativo.ForeColor = System.Drawing.Color.Red
        Me.lblseriecorrelativo.Location = New System.Drawing.Point(15, 12)
        Me.lblseriecorrelativo.Name = "lblseriecorrelativo"
        Me.lblseriecorrelativo.Size = New System.Drawing.Size(101, 25)
        Me.lblseriecorrelativo.TabIndex = 19
        Me.lblseriecorrelativo.Text = "BUSCAR"
        '
        'btnguardar
        '
        Me.btnguardar.Location = New System.Drawing.Point(77, 96)
        Me.btnguardar.Name = "btnguardar"
        Me.btnguardar.Size = New System.Drawing.Size(184, 32)
        Me.btnguardar.TabIndex = 6
        Me.btnguardar.Text = "GRABAR"
        Me.btnguardar.UseVisualStyleBackColor = True
        '
        'dtpfechanueva
        '
        Me.dtpfechanueva.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfechanueva.Location = New System.Drawing.Point(77, 60)
        Me.dtpfechanueva.Name = "dtpfechanueva"
        Me.dtpfechanueva.Size = New System.Drawing.Size(224, 20)
        Me.dtpfechanueva.TabIndex = 5
        '
        'btnValidarResumen
        '
        Me.btnValidarResumen.Location = New System.Drawing.Point(882, 255)
        Me.btnValidarResumen.Name = "btnValidarResumen"
        Me.btnValidarResumen.Size = New System.Drawing.Size(75, 40)
        Me.btnValidarResumen.TabIndex = 19
        Me.btnValidarResumen.Text = "Validar Resumen"
        Me.btnValidarResumen.UseVisualStyleBackColor = True
        '
        'cbdocs
        '
        Me.cbdocs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbdocs.FormattingEnabled = True
        Me.cbdocs.Location = New System.Drawing.Point(473, 11)
        Me.cbdocs.Name = "cbdocs"
        Me.cbdocs.Size = New System.Drawing.Size(124, 21)
        Me.cbdocs.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(433, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Docs"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(702, 425)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "CANT:"
        '
        'lblcantdocs
        '
        Me.lblcantdocs.AutoSize = True
        Me.lblcantdocs.Location = New System.Drawing.Point(747, 425)
        Me.lblcantdocs.Name = "lblcantdocs"
        Me.lblcantdocs.Size = New System.Drawing.Size(13, 13)
        Me.lblcantdocs.TabIndex = 23
        Me.lblcantdocs.Text = "0"
        '
        'btnEnvioMasivo
        '
        Me.btnEnvioMasivo.Location = New System.Drawing.Point(882, 77)
        Me.btnEnvioMasivo.Name = "btnEnvioMasivo"
        Me.btnEnvioMasivo.Size = New System.Drawing.Size(75, 37)
        Me.btnEnvioMasivo.TabIndex = 24
        Me.btnEnvioMasivo.Text = "Enviar Masivo"
        Me.btnEnvioMasivo.UseVisualStyleBackColor = True
        '
        'btnvalidarcpe
        '
        Me.btnvalidarcpe.Location = New System.Drawing.Point(882, 120)
        Me.btnvalidarcpe.Name = "btnvalidarcpe"
        Me.btnvalidarcpe.Size = New System.Drawing.Size(75, 24)
        Me.btnvalidarcpe.TabIndex = 25
        Me.btnvalidarcpe.Text = "Validar CPE"
        Me.btnvalidarcpe.UseVisualStyleBackColor = True
        '
        'panelEmpresa
        '
        Me.panelEmpresa.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelEmpresa.Controls.Add(Me.dgvempresas)
        Me.panelEmpresa.Controls.Add(Me.Label5)
        Me.panelEmpresa.Location = New System.Drawing.Point(13, 39)
        Me.panelEmpresa.Name = "panelEmpresa"
        Me.panelEmpresa.Size = New System.Drawing.Size(530, 302)
        Me.panelEmpresa.TabIndex = 22
        Me.panelEmpresa.Visible = False
        '
        'dgvempresas
        '
        Me.dgvempresas.AllowUserToAddRows = False
        Me.dgvempresas.AllowUserToDeleteRows = False
        Me.dgvempresas.BackgroundColor = System.Drawing.Color.White
        Me.dgvempresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvempresas.GridColor = System.Drawing.Color.DarkGray
        Me.dgvempresas.Location = New System.Drawing.Point(19, 31)
        Me.dgvempresas.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvempresas.Name = "dgvempresas"
        Me.dgvempresas.ReadOnly = True
        Me.dgvempresas.RowHeadersVisible = False
        Me.dgvempresas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvempresas.Size = New System.Drawing.Size(482, 245)
        Me.dgvempresas.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(87, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(261, 25)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "SELECCIONE EMPRESA"
        '
        'lblempresa
        '
        Me.lblempresa.AutoSize = True
        Me.lblempresa.Location = New System.Drawing.Point(12, 14)
        Me.lblempresa.Name = "lblempresa"
        Me.lblempresa.Size = New System.Drawing.Size(129, 13)
        Me.lblempresa.TabIndex = 26
        Me.lblempresa.Text = "EMPRESA SELECCCION"
        '
        'txtmensaje
        '
        Me.txtmensaje.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtmensaje.Location = New System.Drawing.Point(15, 422)
        Me.txtmensaje.Multiline = True
        Me.txtmensaje.Name = "txtmensaje"
        Me.txtmensaje.ReadOnly = True
        Me.txtmensaje.Size = New System.Drawing.Size(560, 37)
        Me.txtmensaje.TabIndex = 27
        '
        'btnValidarCpeMasivo
        '
        Me.btnValidarCpeMasivo.Location = New System.Drawing.Point(882, 150)
        Me.btnValidarCpeMasivo.Name = "btnValidarCpeMasivo"
        Me.btnValidarCpeMasivo.Size = New System.Drawing.Size(75, 36)
        Me.btnValidarCpeMasivo.TabIndex = 28
        Me.btnValidarCpeMasivo.Text = "Validar CPE Masivo"
        Me.btnValidarCpeMasivo.UseVisualStyleBackColor = True
        '
        'btnbaja
        '
        Me.btnbaja.Location = New System.Drawing.Point(882, 349)
        Me.btnbaja.Name = "btnbaja"
        Me.btnbaja.Size = New System.Drawing.Size(75, 23)
        Me.btnbaja.TabIndex = 29
        Me.btnbaja.Text = "Dar Baja"
        Me.btnbaja.UseVisualStyleBackColor = True
        '
        'btnExportar
        '
        Me.btnExportar.Location = New System.Drawing.Point(583, 422)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(75, 22)
        Me.btnExportar.TabIndex = 30
        Me.btnExportar.Text = "Exportar"
        Me.btnExportar.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'Sunat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 469)
        Me.Controls.Add(Me.btnExportar)
        Me.Controls.Add(Me.btnbaja)
        Me.Controls.Add(Me.btnValidarCpeMasivo)
        Me.Controls.Add(Me.txtmensaje)
        Me.Controls.Add(Me.lblempresa)
        Me.Controls.Add(Me.panelEmpresa)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.btnvalidarcpe)
        Me.Controls.Add(Me.btnEnvioMasivo)
        Me.Controls.Add(Me.lblcantdocs)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbdocs)
        Me.Controls.Add(Me.btnValidarResumen)
        Me.Controls.Add(Me.btngenerarResumen)
        Me.Controls.Add(Me.btnenviarcorreo)
        Me.Controls.Add(Me.dgvlista)
        Me.Controls.Add(Me.txtbuscar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpfecha)
        Me.Controls.Add(Me.btnEnviar)
        Me.Name = "Sunat"
        Me.Text = "SUNAT"
        CType(Me.dgvlista, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.panelEmpresa.ResumeLayout(False)
        Me.panelEmpresa.PerformLayout()
        CType(Me.dgvempresas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEnviar As Button
    Friend WithEvents dtpfecha As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents txtbuscar As TextBox
    Friend WithEvents dgvlista As DataGridView
    Friend WithEvents btnenviarcorreo As Button
    Friend WithEvents btngenerarResumen As Button
    Friend WithEvents panel1 As Panel
    Friend WithEvents ctncerrar As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lblseriecorrelativo As Label
    Friend WithEvents btnguardar As Button
    Friend WithEvents dtpfechanueva As DateTimePicker
    Friend WithEvents btnValidarResumen As Button
    Friend WithEvents cbdocs As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblcantdocs As Label
    Friend WithEvents btnEnvioMasivo As Button
    Friend WithEvents btnvalidarcpe As Button
    Friend WithEvents panelEmpresa As Panel
    Friend WithEvents dgvempresas As DataGridView
    Friend WithEvents Label5 As Label
    Friend WithEvents lblempresa As Label
    Friend WithEvents txtmensaje As TextBox
    Friend WithEvents btnValidarCpeMasivo As Button
    Friend WithEvents btnbaja As Button
    Friend WithEvents btnExportar As Button
    Friend WithEvents PrintDialog1 As PrintDialog
End Class
