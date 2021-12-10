<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Sunat
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnEnviar = New System.Windows.Forms.Button()
        Me.dtpfecha = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtbuscar = New System.Windows.Forms.TextBox()
        Me.check = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbempresa = New System.Windows.Forms.ComboBox()
        Me.dgvlista = New System.Windows.Forms.DataGridView()
        Me.btnenviarcorreo = New System.Windows.Forms.Button()
        Me.btnreiniciar = New System.Windows.Forms.Button()
        Me.btngenerarResumen = New System.Windows.Forms.Button()
        Me.ckresumen = New System.Windows.Forms.CheckBox()
        CType(Me.dgvlista, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEnviar
        '
        Me.btnEnviar.Location = New System.Drawing.Point(797, 39)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(75, 32)
        Me.btnEnviar.TabIndex = 1
        Me.btnEnviar.Text = "Enviar"
        Me.btnEnviar.UseVisualStyleBackColor = True
        '
        'dtpfecha
        '
        Me.dtpfecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfecha.Location = New System.Drawing.Point(695, 9)
        Me.dtpfecha.Name = "dtpfecha"
        Me.dtpfecha.Size = New System.Drawing.Size(86, 20)
        Me.dtpfecha.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(429, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "BUSCAR"
        '
        'txtbuscar
        '
        Me.txtbuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtbuscar.Location = New System.Drawing.Point(486, 9)
        Me.txtbuscar.Name = "txtbuscar"
        Me.txtbuscar.Size = New System.Drawing.Size(164, 20)
        Me.txtbuscar.TabIndex = 6
        '
        'check
        '
        Me.check.AutoSize = True
        Me.check.Location = New System.Drawing.Point(665, 12)
        Me.check.Name = "check"
        Me.check.Size = New System.Drawing.Size(15, 14)
        Me.check.TabIndex = 7
        Me.check.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "EMPRESA"
        '
        'cbempresa
        '
        Me.cbempresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbempresa.FormattingEnabled = True
        Me.cbempresa.Location = New System.Drawing.Point(77, 8)
        Me.cbempresa.Name = "cbempresa"
        Me.cbempresa.Size = New System.Drawing.Size(235, 21)
        Me.cbempresa.TabIndex = 9
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
        Me.dgvlista.Size = New System.Drawing.Size(777, 337)
        Me.dgvlista.TabIndex = 13
        '
        'btnenviarcorreo
        '
        Me.btnenviarcorreo.Location = New System.Drawing.Point(797, 317)
        Me.btnenviarcorreo.Name = "btnenviarcorreo"
        Me.btnenviarcorreo.Size = New System.Drawing.Size(75, 57)
        Me.btnenviarcorreo.TabIndex = 14
        Me.btnenviarcorreo.Text = "Enviar Correo"
        Me.btnenviarcorreo.UseVisualStyleBackColor = True
        '
        'btnreiniciar
        '
        Me.btnreiniciar.Location = New System.Drawing.Point(318, 8)
        Me.btnreiniciar.Name = "btnreiniciar"
        Me.btnreiniciar.Size = New System.Drawing.Size(35, 21)
        Me.btnreiniciar.TabIndex = 15
        Me.btnreiniciar.Text = "O"
        Me.btnreiniciar.UseVisualStyleBackColor = True
        '
        'btngenerarResumen
        '
        Me.btngenerarResumen.Enabled = False
        Me.btngenerarResumen.Location = New System.Drawing.Point(797, 158)
        Me.btngenerarResumen.Name = "btngenerarResumen"
        Me.btngenerarResumen.Size = New System.Drawing.Size(75, 38)
        Me.btngenerarResumen.TabIndex = 16
        Me.btngenerarResumen.Text = "Gen Resumen"
        Me.btngenerarResumen.UseVisualStyleBackColor = True
        '
        'ckresumen
        '
        Me.ckresumen.AutoSize = True
        Me.ckresumen.Location = New System.Drawing.Point(801, 135)
        Me.ckresumen.Name = "ckresumen"
        Me.ckresumen.Size = New System.Drawing.Size(71, 17)
        Me.ckresumen.TabIndex = 17
        Me.ckresumen.Text = "Resumen"
        Me.ckresumen.UseVisualStyleBackColor = True
        '
        'Sunat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(881, 386)
        Me.Controls.Add(Me.ckresumen)
        Me.Controls.Add(Me.btngenerarResumen)
        Me.Controls.Add(Me.btnreiniciar)
        Me.Controls.Add(Me.btnenviarcorreo)
        Me.Controls.Add(Me.dgvlista)
        Me.Controls.Add(Me.cbempresa)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.check)
        Me.Controls.Add(Me.txtbuscar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpfecha)
        Me.Controls.Add(Me.btnEnviar)
        Me.Name = "Sunat"
        Me.Text = "SUNAT"
        CType(Me.dgvlista, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEnviar As Button
    Friend WithEvents dtpfecha As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents txtbuscar As TextBox
    Friend WithEvents check As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cbempresa As ComboBox
    Friend WithEvents dgvlista As DataGridView
    Friend WithEvents btnenviarcorreo As Button
    Friend WithEvents btnreiniciar As Button
    Friend WithEvents btngenerarResumen As Button
    Friend WithEvents ckresumen As CheckBox
End Class
