Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

Public Class Sunat
    Dim scn As New SqlConnection
    Dim cn As New CapaSUNAT.ConexionN
    Dim dt As New DataTable
    Dim scmd As New SqlCommand
    Dim sda As New SqlDataAdapter
    Dim ds As New DataSet


    Public DsResumen As DataSet

    Sub ListarEmpresas()
        cbempresa.DataSource = listar_forma_pago.Tables("Empresa")
        cbempresa.DisplayMember = "EMPRESA"
        cbempresa.ValueMember = "ID"

    End Sub

    Private Sub Sunat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListarEmpresas()
    End Sub

    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs) Handles txtbuscar.TextChanged
        Try
            BuscarComprobante(cbempresa.SelectedValue, txtbuscar.Text, IIf(check.Checked, 1, 0), dtpfecha.Text, dgvlista)
        Catch ex As Exception

        End Try
    End Sub





    Public Function listar_forma_pago() As DataSet
        Try
            scn = cn.conectar()
            scn.Open()
            ds.Clear()
            sda = New SqlDataAdapter("ListarEmpresas", scn)
            sda.Fill(ds, "Empresa")
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            scn.Close()
            sda.Dispose()
            ds.Dispose()
        End Try
    End Function

    Public Function ExtraerDatosEmpresa(ByVal nIdEmpresa As String) As DataTable
        Try
            scn = cn.conectar
            scn.Open()
            Dim comand As SqlCommand
            comand = New SqlCommand("select Departamento_Nombre,Departamento_Id,Provincia_Nombre,Provincia_Id,Distrito_Nombre,Distrito_id, razon_social,ruc,direccion,cClaveCert, cCorreo, cCorreoClave from Empresa where id = " & nIdEmpresa, scn)
            sda = New SqlDataAdapter(comand)
            Dim dt As New DataTable()
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ExtraerDatosCPE(ByVal nIdEmpresa As String, nIdVenta As String) As DataTable
        Try
            scn = cn.conectar
            scn.Open()
            Dim comand As SqlCommand
            comand = New SqlCommand("exec ObtenerDatosComprobante " & nIdEmpresa & "," & nIdVenta, scn)
            sda = New SqlDataAdapter(comand)
            Dim dt As New DataTable()
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ExtraerCondicionPago(ByVal nIdEmpresa As String, nIdVenta As String) As DataTable
        Try
            scn = cn.conectar
            scn.Open()
            Dim comand As SqlCommand
            comand = New SqlCommand("exec ObtenerCondicionPago " & nIdEmpresa & "," & nIdVenta, scn)
            sda = New SqlDataAdapter(comand)
            Dim dt As New DataTable()
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ActualizaEstadoComprobante(ByVal nIdEmpresa As String, ByVal nIdVenta As String,
                                               ByVal SunatEstado As String, ByVal SunatDescripcion As String,
                                               ByVal SunatError As String)
        Try
            scn = cn.conectar
            scn.Open()

            scmd = New SqlCommand("update Ventas set SunatEstado = '" & SunatEstado & "', SunatDescripcion = '" & SunatDescripcion & "', SunatError = '" & SunatError & "' where nIdEmpresa = " & nIdEmpresa & " and nIdVenta = " & nIdVenta, scn)
            scmd.CommandType = CommandType.Text
            scmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            scn.Close()
            scn.Dispose()
            scmd.Dispose()
        End Try
    End Sub

    Public Function BuscarComprobante(ByVal nIdEmpresa As Integer, ByVal text As String, bFecha As Integer, dFecha As Date, ByVal tabla As DataGridView)
        Try
            scn = cn.conectar
            scn.Open()
            dt.Clear()
            sda = New SqlDataAdapter("BuscarComprobante", scn)
            sda.SelectCommand.CommandType = CommandType.StoredProcedure
            sda.SelectCommand.Parameters.AddWithValue("@nIdEmpresa", nIdEmpresa)
            sda.SelectCommand.Parameters.AddWithValue("@txtBuscar", text)
            sda.SelectCommand.Parameters.AddWithValue("@bFecha", bFecha)
            sda.SelectCommand.Parameters.AddWithValue("@dFecha", dFecha)

            If ckresumen.Checked = True Then
                sda.SelectCommand.Parameters.AddWithValue("@Docs", "1")
            Else
                sda.SelectCommand.Parameters.AddWithValue("@Docs", "0")
            End If

            sda.Fill(dt)

            Dim ds As New DataSet
            sda.Fill(ds)
            DsResumen = ds

            tabla.DataSource = dt

            tabla.Columns(0).Visible = False
            tabla.Columns(1).Visible = False
            tabla.Columns(2).Visible = False
            tabla.Columns(3).Width = 50
            tabla.Columns(4).Width = 60
            tabla.Columns(5).Width = 120
            tabla.Columns(6).Width = 80
            tabla.Columns(7).Width = 150
            tabla.Columns(8).Width = 50
            tabla.Columns(9).Width = 30
            tabla.Columns(10).Width = 100
            tabla.Columns(11).Width = 100

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            scn.Close()
            sda.Dispose()
            dt.Dispose()
        End Try
        Return False
    End Function

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click

        Dim nIdVenta As String
        nIdVenta = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(1).Value

        Dim cab As CapaSUNAT.CapaSUNAT.ViewModels.Cabecera = New CapaSUNAT.CapaSUNAT.ViewModels.Cabecera()

        Dim dt = ExtraerDatosEmpresa(cbempresa.SelectedValue)
        If dt.Rows.Count() > 0 Then
            cab.EmpresaDepartamento = dt.Rows(0)(0).ToString()
            cab.ID_EmpresaDepartamento = dt.Rows(0)(1).ToString()
            cab.EmpresaProvincia = dt.Rows(0)(2).ToString()
            cab.ID_EmpresaProvincia = dt.Rows(0)(3).ToString()
            cab.EmpresaDistrito = dt.Rows(0)(4).ToString()
            cab.ID_EmpresaDistrito = dt.Rows(0)(5).ToString()

            cab.EmpresaRazonSocial = dt.Rows(0)(6).ToString()
            cab.EmpresaRUC = dt.Rows(0)(7).ToString()
            cab.EmpresaDireccion = dt.Rows(0)(8).ToString()
        End If



        Dim dt1 = ExtraerDatosCPE(cbempresa.SelectedValue, nIdVenta)
        cab.Serie = dt1.Rows(0)(0).ToString
        cab.Numero = dt1.Rows(0)(1).ToString
        cab.Fechaemision = dt1.Rows(0)(2).ToString
        cab.Fechavencimiento = dt1.Rows(0)(3).ToString
        cab.Idtipocomp = dt1.Rows(0)(4).ToString
        cab.Idmoneda = dt1.Rows(0)(5).ToString
        cab.ClienteTipodocumento = dt1.Rows(0)(6).ToString
        cab.ClienteNumeroDocumento = dt1.Rows(0)(7).ToString
        cab.ClienteRazonSocial = dt1.Rows(0)(8).ToString

        cab.Porcigv = dt1.Rows(0)(9).ToString
        cab.TotSubtotal = dt1.Rows(0)(10).ToString
        cab.TotDsctos = dt1.Rows(0)(11).ToString
        cab.TotIgv = dt1.Rows(0)(12).ToString
        cab.TotISC = dt1.Rows(0)(13).ToString
        cab.TotTotal = dt1.Rows(0)(14).ToString
        cab.TotOtros = dt1.Rows(0)(15).ToString
        cab.TotNeto = dt1.Rows(0)(16).ToString


        Dim dtCondicionPago = ExtraerCondicionPago(cbempresa.SelectedValue, nIdVenta)
        If dtCondicionPago.Rows.Count() = 0 Then '  ES CONTADO
            cab.FormaPago = "Contado"
            cab.NumeroCuotas = 1
            cab.MontoCuota = cab.TotNeto
        Else
            cab.FormaPago = "credito"
            cab.Fechavencimiento = Convert.ToDateTime(dtCondicionPago.Rows(0)(2).ToString).ToString("dd/MM/yyyy")
        End If


        Dim lista As List(Of CapaSUNAT.CapaSUNAT.ViewModels.Detalles) = New List(Of CapaSUNAT.CapaSUNAT.ViewModels.Detalles)()

        If dt1.Rows.Count() > 0 Then
            For fila = 1 To dt1.Rows.Count
                Dim detalledocumento As CapaSUNAT.CapaSUNAT.ViewModels.Detalles = New CapaSUNAT.CapaSUNAT.ViewModels.Detalles()
                detalledocumento.Codcom = dt1.Rows(fila - 1)(17).ToString
                detalledocumento.DescripcionProducto = dt1.Rows(fila - 1)(18).ToString
                detalledocumento.Cantidad = dt1.Rows(fila - 1)(19).ToString
                detalledocumento.UnidadMedida = dt1.Rows(fila - 1)(20).ToString
                detalledocumento.Precio = dt1.Rows(fila - 1)(21).ToString
                detalledocumento.Descuento = dt1.Rows(fila - 1)(22).ToString
                detalledocumento.Total = dt1.Rows(fila - 1)(23).ToString
                detalledocumento.mtoValorVentaItem = dt1.Rows(fila - 1)(24).ToString
                detalledocumento.porIgvItem = dt1.Rows(fila - 1)(25).ToString
                lista.Add(detalledocumento)
            Next
        End If
        cab.Detalles = lista
        cab.cLeyenda = dt1.Rows(0)(26).ToString

        Dim datos As String() = Nothing
        Dim numerodoc As String = cab.Numero

        Dim utilservicios As New CapaSUNAT.CapaSUNAT.Servicios.SUNAT_UTIL

        utilservicios.Ruta_XML = Path.GetDirectoryName(Application.ExecutablePath) & "\XML\" & cab.EmpresaRUC & "\"
        utilservicios.Ruta_Certificado = Path.GetDirectoryName(Application.ExecutablePath) & "\CERTIFICADO\" & cab.EmpresaRUC & ".pfx"
        utilservicios.Password_Certificado = dt.Rows(0)(9).ToString() '"Canarias123a"

        utilservicios.Ruta_ENVIOS = Path.GetDirectoryName(Application.ExecutablePath) & "\ENVIOS\" & cab.EmpresaRUC & "\"
        utilservicios.Ruta_CDRS = Path.GetDirectoryName(Application.ExecutablePath) & "\CDR\" & cab.EmpresaRUC & "\"
        Dim ruc As String = cab.EmpresaRUC 'Util.ObtenerValorParametro("EMPRESA", "RUC_PRUEBA")

        Dim RespuestaEnvio As String
        RespuestaEnvio = utilservicios.GenerarComprobanteFB_XML(cab, dtCondicionPago)

        'Obtener Firma 
        Dim archXML As String = utilservicios.Ruta_XML + ruc + "-" + cab.Idtipocomp + "-" + cab.Serie + "-" + cab.Numero + ".xml"
        Dim firma As String = Util.ObtenerDigestValueSunat(archXML)         '' CREO QUE ESTE ES EL HASH DE XML GENERADO

        'Generar Codigo QR
        Util.GenerarQR(ruc, cab.Idtipocomp,
            cab.Serie + "-" + cab.Numero, cab.TotIgv, cab.TotNeto, Convert.ToDateTime(cab.Fechaemision).ToString("dd-MM-yyyy"),
            cab.ClienteTipodocumento, cab.ClienteNumeroDocumento, firma)

        Dim help As New CapaSUNAT.CapaSUNAT.Util.Helper()

        If RespuestaEnvio = "0" Then
            Dim rsp As String() = help.ObtenerRespuestaZIPSunat(utilservicios.Ruta_CDRS & "R-" & ruc & "-" & cab.Idtipocomp & "-" & cab.Serie & "-" & cab.Numero & ".zip", ruc)
            If rsp(0) = "0" Then
                'crear metodo para actualizar el campo enviado en true de la factura grabada
                'MessageBox.Show("El comprobante Número: " + cab.Serie & "-" & cab.Numero & ", fue enviada y aceptada por SUNAT", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ActualizaEstadoComprobante(cbempresa.SelectedValue, nIdVenta, rsp(0), rsp(3), "")
            Else
                'MessageBox.Show("El comprobante Número: " + cab.Serie & "-" & cab.Numero & ", no se pudo enviar a la  SUNAT", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ActualizaEstadoComprobante(cbempresa.SelectedValue, nIdVenta, "", "", "ERROR")
            End If
        Else
            ActualizaEstadoComprobante(cbempresa.SelectedValue, nIdVenta, "", "", RespuestaEnvio.Replace("'", "-"))
        End If

        BuscarComprobante(cbempresa.SelectedValue, txtbuscar.Text, IIf(check.Checked, 1, 0), dtpfecha.Text, dgvlista)

    End Sub

    Private Sub btnenviarcorreo_Click(sender As Object, e As EventArgs) Handles btnenviarcorreo.Click
        Dim nIdVenta As String
        nIdVenta = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(1).Value

        Dim dt = ExtraerDatosEmpresa(cbempresa.SelectedValue)
        Dim EmpresaRazonSocial = dt.Rows(0)(6).ToString()
        Dim EmpresaRUC = dt.Rows(0)(7).ToString()
        Dim EmpresaDireccion = dt.Rows(0)(8).ToString()
        Dim Correo = dt.Rows(0)(10).ToString()
        Dim Clave = dt.Rows(0)(11).ToString()

        Dim dt1 = ExtraerDatosCPE(cbempresa.SelectedValue, nIdVenta)
        Dim Serie = dt1.Rows(0)(0).ToString
        Dim Numero = dt1.Rows(0)(1).ToString
        Dim Idtipocomp = dt1.Rows(0)(4).ToString
        Dim ClienteCorreo = dt1.Rows(0)(27).ToString

        Dim Ruta_XML = Path.GetDirectoryName(Application.ExecutablePath) & "\XML\" & EmpresaRUC & "\"
        Dim Ruta_CDRS = Path.GetDirectoryName(Application.ExecutablePath) & "\CDR\" & EmpresaRUC & "\"

        Dim ArchivoXML As String = Ruta_XML & EmpresaRUC & "-" & Idtipocomp & "-" & Serie & "-" & Numero & ".xml"
        Dim ArchivoCRD As String = Ruta_CDRS & "R-" & EmpresaRUC & "-" & Idtipocomp & "-" & Serie & "-" & Numero & ".zip"
        Dim CadenaAdjuntos As String = ""
        If File.Exists(ArchivoXML) Then
            CadenaAdjuntos = ArchivoXML & ";"
        End If

        If File.Exists(ArchivoCRD) Then
            CadenaAdjuntos = CadenaAdjuntos & ArchivoCRD & ";"
        End If
        If CadenaAdjuntos <> "" Then
            CadenaAdjuntos = CadenaAdjuntos.Substring(0, Len(CadenaAdjuntos) - 1)
        End If


        Dim enviar As New EnviarCorreo
        enviar.txtcorreo.Text = Correo
        enviar.txtclave.Text = Clave
        enviar.txtasunto.Text = EmpresaRazonSocial & " le ha enviado un documento"
        enviar.txtdestinatarios.Text = ClienteCorreo
        enviar.txtdescripcion.Text = "Estimado Cliente." & vbCrLf & "Te ha llegado desde nuestro sistema un nuevo comprobante N° " & Serie & "-" & Numero & "." & vbCrLf &
                                       "Se Adjunta Documentos."
        enviar.txtadjunto.Text = CadenaAdjuntos

        enviar.ShowDialog()

    End Sub

    Private Sub check_CheckedChanged(sender As Object, e As EventArgs) Handles check.CheckedChanged
        Try
            BuscarComprobante(cbempresa.SelectedValue, txtbuscar.Text, IIf(check.Checked, 1, 0), dtpfecha.Text, dgvlista)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtpfecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpfecha.ValueChanged
        Try
            BuscarComprobante(cbempresa.SelectedValue, txtbuscar.Text, IIf(check.Checked, 1, 0), dtpfecha.Text, dgvlista)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnreiniciar_Click(sender As Object, e As EventArgs) Handles btnreiniciar.Click
        Application.Restart()
    End Sub

    Private Sub cbempresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbempresa.SelectedIndexChanged
        Try
            If IsNumeric(cbempresa.SelectedValue) Then
                Dim dt = ExtraerDatosEmpresa(cbempresa.SelectedValue)
                Dim EmpresaRUC = dt.Rows(0)(7).ToString()

                Dim OriginalFile = Application.StartupPath() & "\CONFIG\" & EmpresaRUC & ".exe.config"
                Dim FileToReplace = Application.StartupPath() & "\SUNAT.exe.config"
                My.Computer.FileSystem.CopyFile(OriginalFile, FileToReplace, True)

                Dim OriginalFile1 = Application.StartupPath() & "\CONFIG\" & EmpresaRUC & ".vshost.exe.config"
                Dim FileToReplace1 = Application.StartupPath() & "\SUNAT.vshost.exe.config"
                My.Computer.FileSystem.CopyFile(OriginalFile, FileToReplace, True)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReplaceFile(FileToMoveAndDelete As String, FileToReplace As String)
        File.Replace(FileToMoveAndDelete, FileToReplace, False)
    End Sub

    Private Sub btngenerarResumen_Click(sender As Object, e As EventArgs) Handles btngenerarResumen.Click
        If dgvlista.Rows.Count > 0 Then

            Try
                Dim cab As CapaSUNAT.CapaSUNAT.ViewModels.Cabecera = New CapaSUNAT.CapaSUNAT.ViewModels.Cabecera()
                Dim dt = ExtraerDatosEmpresa(cbempresa.SelectedValue)
                If dt.Rows.Count() > 0 Then
                    cab.EmpresaDepartamento = dt.Rows(0)(0).ToString()
                    cab.ID_EmpresaDepartamento = dt.Rows(0)(1).ToString()
                    cab.EmpresaProvincia = dt.Rows(0)(2).ToString()
                    cab.ID_EmpresaProvincia = dt.Rows(0)(3).ToString()
                    cab.EmpresaDistrito = dt.Rows(0)(4).ToString()
                    cab.ID_EmpresaDistrito = dt.Rows(0)(5).ToString()

                    cab.EmpresaRazonSocial = dt.Rows(0)(6).ToString()
                    cab.EmpresaRUC = dt.Rows(0)(7).ToString()
                    cab.EmpresaDireccion = dt.Rows(0)(8).ToString()
                End If

                Dim serv As New CapaSUNAT.CapaSUNAT.Servicios.SUNAT_UTIL

                serv.Ruta_XML = Path.GetDirectoryName(Application.ExecutablePath) & "\XML\" & cab.EmpresaRUC & "\"
                serv.Ruta_Certificado = Path.GetDirectoryName(Application.ExecutablePath) & "\CERTIFICADO\" & cab.EmpresaRUC & ".pfx"
                serv.Password_Certificado = dt.Rows(0)(9).ToString()

                serv.Ruta_ENVIOS = Path.GetDirectoryName(Application.ExecutablePath) & "\ENVIOS\" & cab.EmpresaRUC & "\"
                serv.Ruta_CDRS = Path.GetDirectoryName(Application.ExecutablePath) & "\CDR\" & cab.EmpresaRUC & "\"
                Dim rucEmpresa As String = cab.EmpresaRUC
                Dim razonEmpresa As String = cab.EmpresaRazonSocial

                Dim numTicket As String = serv.GenerarResumenDiario_XML(dtpfecha.Text, rucEmpresa, razonEmpresa, DsResumen)
                'txtNumTicket.Text = numTicket
                MessageBox.Show("Enviado a SUNAT, debe consultar el estado a tráves del número de ticket: " & numTicket, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End Try
        End If
    End Sub

    Private Sub ckresumen_CheckedChanged(sender As Object, e As EventArgs) Handles ckresumen.CheckedChanged
        Try
            BuscarComprobante(cbempresa.SelectedValue, txtbuscar.Text, IIf(check.Checked, 1, 0), dtpfecha.Text, dgvlista)
        Catch ex As Exception

        End Try
        If ckresumen.Checked = True Then
            btngenerarResumen.Enabled = True
        Else
            btngenerarResumen.Enabled = False
        End If
    End Sub
End Class
