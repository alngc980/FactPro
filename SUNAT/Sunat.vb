Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Sunat
    Dim scn As New SqlConnection
    Dim cn As New CapaSUNAT.ConexionN
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Dim scmd As New SqlCommand
    Dim sda As New SqlDataAdapter
    Dim ds As New DataSet


    Public DsResumen As DataSet
    Dim nidEmpresa As String
    Dim nIdVenta As String

    Sub ListarEmpresas()
        dgvempresas.DataSource = listar_forma_pago.Tables("Empresa")
        'cbempresa.DisplayMember = "EMPRESA"
        'cbempresa.ValueMember = "ID"

        dgvempresas.Columns(0).Visible = False
        dgvempresas.Columns(1).Width = 400
    End Sub

    Sub ListarTipoDocs()
        cbdocs.DataSource = listar_Tipo_Docs.Tables("TipoDoc")
        cbdocs.DisplayMember = "Descrip"
        cbdocs.ValueMember = "Id"
    End Sub

    Private Sub Sunat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpfechanueva.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        dtpfechanueva.Format = DateTimePickerFormat.Custom


        ListarEmpresas()
        ListarTipoDocs()
        ExtraerDatosEmpresa()
    End Sub

    Public CEmpresaDepartamento,
           CID_EmpresaDepartamento,
           CEmpresaProvincia,
           CID_EmpresaProvincia,
           CEmpresaDistrito,
           CID_EmpresaDistrito,
           CEmpresaRazonSocial,
           CEmpresaRUC,
           CEmpresaDireccion,
           CEmisorSucursal,
           CclaveCert,
           nAfectacionIGV As String

    Sub ExtraerDatosEmpresa()

        Dim dt = ExtraerDatosEmpresa(ObtenerEmpresaSeleccionada())
        If dt.Rows.Count() > 0 Then
            CEmpresaDepartamento = dt.Rows(0)(0).ToString()
            CID_EmpresaDepartamento = dt.Rows(0)(1).ToString()
            CEmpresaProvincia = dt.Rows(0)(2).ToString()
            CID_EmpresaProvincia = dt.Rows(0)(3).ToString()
            CEmpresaDistrito = dt.Rows(0)(4).ToString()
            CID_EmpresaDistrito = dt.Rows(0)(5).ToString()
            CEmpresaRazonSocial = dt.Rows(0)(6).ToString()
            CEmpresaRUC = dt.Rows(0)(7).ToString()
            CEmpresaDireccion = dt.Rows(0)(8).ToString()
            CEmisorSucursal = dt.Rows(0)(12).ToString()
            CclaveCert = dt.Rows(0)(9).ToString()
            nAfectacionIGV = dt.Rows(0)(13).ToString()

            lblempresa.Text = CEmpresaRUC & " - " & CEmpresaRazonSocial
        End If
    End Sub

    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs) Handles txtbuscar.TextChanged
        Try
            BuscarComprobante(nidEmpresa, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue)
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
    Public Function listar_Tipo_Docs() As DataSet
        Try
            scn = cn.conectar()
            scn.Open()
            Dim dsl As New DataSet
            sda = New SqlDataAdapter("ListarDocs", scn)
            sda.Fill(dsl, "TipoDoc")
            Return dsl

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
            comand = New SqlCommand("select Departamento_Nombre,Departamento_Id,Provincia_Nombre,Provincia_Id,Distrito_Nombre,Distrito_id, razon_social,ruc,direccion,cClaveCert, cCorreo, cCorreoClave, CodigoDomicilioEmisor, nAfectacion from Empresa where id = " & nIdEmpresa, scn)
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
                                               ByVal SunatError As String, ByVal SunatConsultaCPE As String, ByVal SunatOtro As String)
        Try
            scn = cn.conectar
            scn.Open()
            Dim Sql As String = ""
            Sql = "exec ActualizarEstadoCpe " + nIdEmpresa + "," + nIdVenta + ",'" + SunatEstado + "','" + SunatDescripcion + "','" + SunatError + "','" + SunatConsultaCPE + "','" + SunatOtro + "'"
            scmd = New SqlCommand(Sql, scn)
            scmd.CommandType = CommandType.Text
            scmd.ExecuteNonQuery()

        Catch ex As Exception
            txtmensaje.Text = ex.Message
        Finally
            scn.Close()
            scn.Dispose()
            scmd.Dispose()
        End Try
    End Sub

    Public Sub ActualizaFechaComprobante(ByVal nIdEmpresa As String, ByVal nIdVenta As String,
                                               ByVal fecha As DateTime)
        Try
            scn = cn.conectar
            scn.Open()

            scmd = New SqlCommand("update ventas set dFechaHora = '" + Format(fecha, "yyyyMMdd HH:mm:ss") + "' where nIdEmpresa = " + nIdEmpresa + " and nIdVenta = " + nIdVenta, scn)
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

    Public Sub ActualizaEstadoResumenBoleta(ByVal nIdEmpresa As String, ByVal nIdVenta As String,
                                               ByVal SunatTicket As String)
        Try
            scn = cn.conectar
            scn.Open()

            scmd = New SqlCommand("update Ventas set SunatOtro = '" & SunatTicket & "' where nIdEmpresa = " & nIdEmpresa & " and nIdVenta = " & nIdVenta, scn)
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

    Public Sub ActualizarSeleccionEmpresa(ByVal nIdEmpresa As String)
        Try
            scn = cn.conectar
            scn.Open()

            scmd = New SqlCommand("exec ActualizarSeleccion " & nIdEmpresa, scn)
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

    Public Function ObtenerCorrelativos(ByVal nIdEmpresa As String, ByVal Fecha As Date, ByVal nTipoDoc As Integer)
        Try
            scn = cn.conectar
            scn.Open()
            Dim comand As SqlCommand
            Select Case nTipoDoc
                Case 20
                    comand = New SqlCommand("select dbo.ObtenerCorrelativoResumen(" + nIdEmpresa + ",'" + Fecha + "')", scn)
                Case Else
                    comand = New SqlCommand("select dbo.ObtenerCorrelativoBajaDoc(" + nIdEmpresa + ",'" + Fecha + "')", scn)
            End Select

            sda = New SqlDataAdapter(comand)
            Dim dt As New DataTable()
            sda.Fill(dt)

            ObtenerCorrelativos = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerEmpresaSeleccionada()
        Try
            scn = cn.conectar
            scn.Open()
            Dim comand As SqlCommand
            comand = New SqlCommand("select * from Empresa where bSeleccion = 1", scn)
            sda = New SqlDataAdapter(comand)
            Dim dt As New DataTable()
            sda.Fill(dt)

            nidEmpresa = dt.Rows(0)(0).ToString()
            ObtenerEmpresaSeleccionada = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub RegistrarResumenDiario(ByVal nIdEmpresa As String, ByVal Fecha As Date,
                                               ByVal SunatTicket As String)
        Try
            scn = cn.conectar
            scn.Open()

            scmd = New SqlCommand("exec GuardaResumenDiario " + nIdEmpresa + ",'" + Fecha + "'," + SunatTicket, scn)
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

    Public Function BuscarComprobante(ByVal nIdEmpresa As Integer, ByVal text As String, bFecha As Integer, dFecha As Date, ByVal tabla As DataGridView, ByVal nTipoDoc As String, Optional nReportMensual As Integer = 0)
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
            sda.SelectCommand.Parameters.AddWithValue("@Docs", nTipoDoc)
            sda.SelectCommand.Parameters.AddWithValue("@bReportMensual", nReportMensual)
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
            tabla.Columns(5).Width = 100
            tabla.Columns(6).Width = 80
            tabla.Columns(7).Width = 150
            tabla.Columns(8).Width = 50
            tabla.Columns(9).Width = 30
            tabla.Columns(10).Width = 100
            tabla.Columns(11).Width = 100
            tabla.Columns(12).Visible = False
            tabla.Columns(13).Visible = False
            tabla.Columns(14).Visible = False
            tabla.Columns(15).Visible = False
            'tabla.Columns(16).Visible = False
            tabla.Columns(17).Visible = False
            lblcantdocs.Text = tabla.Rows.Count

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
        If dgvlista.Rows.Count > 0 Then
            If dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(2).Value.ToString = "1" Or dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(2).Value.ToString = "2" Then
                If dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(9).Value.ToString <> "0" Then
                    EnviarCPE(dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(1).Value, 1)
                    BuscarComprobante(nidEmpresa, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue)
                End If
            End If
        End If
    End Sub

    Public Sub EnviarCPE(nIdVenta As String, nTipoDoc As Integer)   '1|3 boleta o Factura   7 NotaCredito

        Dim cab As CapaSUNAT.CapaSUNAT.ViewModels.Cabecera = New CapaSUNAT.CapaSUNAT.ViewModels.Cabecera()

        cab.EmpresaDepartamento = CEmpresaDepartamento
        cab.ID_EmpresaDepartamento = CID_EmpresaDepartamento
        cab.EmpresaProvincia = CEmpresaProvincia
        cab.ID_EmpresaProvincia = CID_EmpresaProvincia
        cab.EmpresaDistrito = CEmpresaDistrito
        cab.ID_EmpresaDistrito = CID_EmpresaDistrito
        cab.EmpresaRazonSocial = CEmpresaRazonSocial
        cab.EmpresaRUC = CEmpresaRUC
        cab.EmpresaDireccion = CEmpresaDireccion
        cab.EmisorSucursal = CEmisorSucursal

        Dim dt1 = ExtraerDatosCPE(nidEmpresa, nIdVenta)
        cab.Serie = dt1.Rows(0)(0).ToString
        cab.Numero = dt1.Rows(0)(1).ToString
        cab.Fechaemision = dt1.Rows(0)(2).ToString
        cab.Fechavencimiento = dt1.Rows(0)(3).ToString
        cab.Idtipocomp = dt1.Rows(0)(4).ToString        '--> TIPO COMPROB
        cab.Idmoneda = dt1.Rows(0)(5).ToString
        cab.ClienteTipodocumento = dt1.Rows(0)(6).ToString
        cab.ClienteNumeroDocumento = dt1.Rows(0)(7).ToString
        cab.ClienteRazonSocial = dt1.Rows(0)(8).ToString
        Dim AfectacionIGV As Integer
        AfectacionIGV = dt1.Rows(0)(28).ToString
        cab.AfectacionIGV = AfectacionIGV

        If nTipoDoc = 7 Then  'MOTIVO NOTA CREDITO
            cab.Cab_Ref_Motivo = "ANULACION DE LA OPERACION"
            cab.Cab_Ref_Serie = ""                  '
            cab.Cab_Ref_Numero = ""                 '
            cab.Cab_Ref_TipoDeDocumento = ""        '01 O 07 BOLETA O FACTURA
        End If

        cab.Porcigv = dt1.Rows(0)(9).ToString
        cab.TotSubtotal = dt1.Rows(0)(10).ToString
        cab.TotDsctos = dt1.Rows(0)(11).ToString

        cab.TotIgv = dt1.Rows(0)(12).ToString
        cab.TotISC = dt1.Rows(0)(13).ToString
        cab.TotTotal = dt1.Rows(0)(14).ToString
        cab.TotOtros = dt1.Rows(0)(15).ToString
        cab.TotNeto = dt1.Rows(0)(16).ToString


        Dim dtCondicionPago = ExtraerCondicionPago(nidEmpresa, nIdVenta)
        If dtCondicionPago.Rows.Count() = 0 Then '  ES CONTADO
            cab.FormaPago = "Contado"
            cab.NumeroCuotas = 1
            cab.MontoCuota = cab.TotNeto
        Else
            cab.FormaPago = "Credito"
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
                detalledocumento.AfectacionIGV = AfectacionIGV
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
        utilservicios.Password_Certificado = CclaveCert '"Canarias123a"

        utilservicios.Ruta_ENVIOS = Path.GetDirectoryName(Application.ExecutablePath) & "\ENVIOS\" & cab.EmpresaRUC & "\"
        utilservicios.Ruta_CDRS = Path.GetDirectoryName(Application.ExecutablePath) & "\CDR\" & cab.EmpresaRUC & "\"
        Dim ruc As String = cab.EmpresaRUC 'Util.ObtenerValorParametro("EMPRESA", "RUC_PRUEBA")

        Dim RespuestaEnvio As String

        Select Case nTipoDoc
            Case 1
                RespuestaEnvio = utilservicios.GenerarComprobanteFB_XML(cab, dtCondicionPago)
            Case 7
                RespuestaEnvio = utilservicios.GenerarComprobanteNC_XML(cab, dtCondicionPago)
            Case Else
                RespuestaEnvio = "XX"
        End Select



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
                ActualizaEstadoComprobante(nidEmpresa, nIdVenta, rsp(0), rsp(3), "", "", "")
                txtmensaje.Text = rsp(0) & "-" & rsp(3)
            Else
                'MessageBox.Show("El comprobante Número: " + cab.Serie & "-" & cab.Numero & ", no se pudo enviar a la  SUNAT", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ActualizaEstadoComprobante(nidEmpresa, nIdVenta, "", "", rsp(3), "", "")
                txtmensaje.Text = rsp(3)
            End If
        Else
            ActualizaEstadoComprobante(nidEmpresa, nIdVenta, "", "", RespuestaEnvio.Replace("'", "-"), "", "")
        End If

    End Sub

    Private Sub btnenviarcorreo_Click(sender As Object, e As EventArgs) Handles btnenviarcorreo.Click
        Dim nIdVenta As String
        nIdVenta = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(1).Value

        Dim dt = ExtraerDatosEmpresa(nidEmpresa)
        Dim EmpresaRazonSocial = dt.Rows(0)(6).ToString()
        Dim EmpresaRUC = dt.Rows(0)(7).ToString()
        Dim EmpresaDireccion = dt.Rows(0)(8).ToString()
        Dim Correo = dt.Rows(0)(10).ToString()
        Dim Clave = dt.Rows(0)(11).ToString()

        Dim dt1 = ExtraerDatosCPE(nidEmpresa, nIdVenta)
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

    Private Sub dtpfecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpfecha.ValueChanged
        Try
            BuscarComprobante(nidEmpresa, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub cbempresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbempresa.SelectedIndexChanged
    '    Try
    '        If IsNumeric(cbempresa.SelectedValue) Then
    '            Dim dt = ExtraerDatosEmpresa(cbempresa.SelectedValue)
    '            Dim EmpresaRUC = dt.Rows(0)(7).ToString()

    '            Dim OriginalFile = Application.StartupPath() & "\CONFIG\" & EmpresaRUC & ".exe.config"
    '            Dim FileToReplace = Application.StartupPath() & "\SUNAT.exe.config"
    '            My.Computer.FileSystem.CopyFile(OriginalFile, FileToReplace, True)

    '            nidEmpresa = cbempresa.SelectedValue
    '        End If

    '        BuscarComprobante(cbempresa.SelectedValue, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue)

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub ReplaceFile(FileToMoveAndDelete As String, FileToReplace As String)
        File.Replace(FileToMoveAndDelete, FileToReplace, False)
    End Sub
    'comesdfbskd
    Private Sub btngenerarResumen_Click(sender As Object, e As EventArgs) Handles btngenerarResumen.Click
        If dgvlista.Rows.Count > 0 Then

            Try
                Dim cab As CapaSUNAT.CapaSUNAT.ViewModels.Cabecera = New CapaSUNAT.CapaSUNAT.ViewModels.Cabecera()
                Dim dt = ExtraerDatosEmpresa(nidEmpresa)
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

                Dim nCorrelativoResumen As Integer
                nCorrelativoResumen = ObtenerCorrelativos(nidEmpresa, dtpfecha.Text, 20)
                Dim numTicket As String = serv.GenerarResumenDiario_XML(dtpfecha.Text, rucEmpresa, razonEmpresa, DsResumen, nCorrelativoResumen, nAfectacionIGV)

                If Trim(numTicket) <> "" Then
                    For Renglones As Integer = 0 To dgvlista.RowCount - 1
                        Dim Empresa, CodVenta As String
                        Empresa = Me.dgvlista.Item(0, Renglones).Value
                        CodVenta = Me.dgvlista.Item(1, Renglones).Value
                        ActualizaEstadoResumenBoleta(Empresa, CodVenta, numTicket)
                    Next
                    RegistrarResumenDiario(nidEmpresa, dtpfecha.Text, numTicket)
                End If

                MessageBox.Show("Enviado a SUNAT, debe consultar el estado a tráves del número de ticket: " & numTicket, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End Try
        End If
    End Sub

    Private Sub btnValidarCpeMasivo_Click(sender As Object, e As EventArgs) Handles btnValidarCpeMasivo.Click
        Dim ruc, nTipoDoc, cSerie As String
        Dim nCorrelativo As Integer

        ruc = CEmpresaRUC


        If dgvlista.Rows.Count > 0 Then
            For Renglones As Integer = 0 To dgvlista.RowCount - 1
                If Me.dgvlista.Item(2, Renglones).Value.ToString = "1" Or Me.dgvlista.Item(2, Renglones).Value.ToString = "2" Then


                    nTipoDoc = ""
                    If Me.dgvlista.Item(2, Renglones).Value.ToString = "1" Then   ' SI ES BOLETA
                        nTipoDoc = "03"
                    End If
                    If Me.dgvlista.Item(2, Renglones).Value.ToString = "2" Then   ' SI ES FACTURA
                        nTipoDoc = "01"
                    End If

                    cSerie = Me.dgvlista.Item(3, Renglones).Value.ToString
                    nCorrelativo = Me.dgvlista.Item(4, Renglones).Value.ToString

                    Dim serv As New CapaSUNAT.CapaSUNAT.Servicios.SUNAT_UTIL
                    Dim Mensaje As String
                    Mensaje = serv.ConsultaEstadoCPE_Ws(ruc, nTipoDoc, cSerie, nCorrelativo)
                    ActualizaEstadoComprobante(nidEmpresa, Me.dgvlista.Item(1, Renglones).Value.ToString, "", "", "", Mensaje, "")

                End If
            Next
        End If

        MsgBox("Validacion de CPE Masivo Finalizado", Title:="Mensaje")

    End Sub

    Private Sub lblempresa_Click(sender As Object, e As EventArgs) Handles lblempresa.Click
        If panelEmpresa.Visible = True Then
            panelEmpresa.Visible = False
        Else
            panelEmpresa.Visible = True
        End If
    End Sub

    Private Sub btnbaja_Click(sender As Object, e As EventArgs) Handles btnbaja.Click
        If dgvlista.Rows.Count > 0 Then
            DarDeBaja(dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(1).Value)
        End If
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        BuscarComprobante(nidEmpresa, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue, 1)

        Dim Aplicacion As Excel.Application
        Dim Libro As Excel.Workbook
        Dim Hoja As Excel.Worksheet

        Aplicacion = New Excel.Application
        '" MES " + Month(dtpfecha.Text) + " AÑO " + Year(dtpfecha.Text) +
        Dim rutaNuevoExcel As String = Application.StartupPath + "\REPORT\VENTAS - " + lblempresa.Text + " MES " + CStr(Month(dtpfecha.Text)) + " AÑO " + CStr(Year(dtpfecha.Text)) + ".xlsx"

        If My.Computer.FileSystem.FileExists(rutaNuevoExcel) Then
            My.Computer.FileSystem.DeleteFile(rutaNuevoExcel)
        End If

        My.Computer.FileSystem.CopyFile(Application.StartupPath + "\REPORT\ReportMensual.xlsx", rutaNuevoExcel)

        Libro = Aplicacion.Workbooks.Open(rutaNuevoExcel)
        Hoja = Libro.Worksheets("Hoja1")

        Dim Rango As Integer
        Dim nTotal As Decimal, nAnulados As Decimal

        'Recorremos el DataGridView rellenando la hoja de trabajo
        Dim columnsCount As Integer = dgvlista.Columns.Count
        For i As Integer = 0 To dgvlista.Rows.Count - 1
            'For j As Integer = 0 To dgvkardex.Columns.Count - 1
            Hoja.Cells(i + 2, 1) = CEmpresaRUC
            Hoja.Cells(i + 2, 2) = dgvlista.Rows(i).Cells(17).Value.ToString()

            Hoja.Cells(i + 2, 3) = dgvlista.Rows(i).Cells(3).Value.ToString()
            Hoja.Cells(i + 2, 4) = dgvlista.Rows(i).Cells(4).Value.ToString()
            Hoja.Cells(i + 2, 5) = CDate(dgvlista.Rows(i).Cells(5).Value).Day.ToString().PadLeft(2, "0") + "/" +
                                    CDate(dgvlista.Rows(i).Cells(5).Value).Month.ToString().PadLeft(2, "0") + "/" +
                                    CDate(dgvlista.Rows(i).Cells(5).Value).Year.ToString()

            Hoja.Cells(i + 2, 6) = dgvlista.Rows(i).Cells(6).Value.ToString()
            Hoja.Cells(i + 2, 7) = dgvlista.Rows(i).Cells(7).Value.ToString()
            Hoja.Cells(i + 2, 8) = dgvlista.Rows(i).Cells(8).Value.ToString()
            Hoja.Cells(i + 2, 9) = dgvlista.Rows(i).Cells(15).Value.ToString()
            Hoja.Cells(i + 2, 10) = dgvlista.Rows(i).Cells(16).Value.ToString()

            nTotal = nTotal + CDec(dgvlista.Rows(i).Cells(8).Value)

            If CInt(IIf(IsDBNull(dgvlista.Rows(i).Cells(15).Value), 11, dgvlista.Rows(i).Cells(15).Value)) <> 1 Then
                nAnulados = nAnulados + CDec(dgvlista.Rows(i).Cells(8).Value)
            End If

            Rango = i + 2
        Next
        Hoja.Range("A1", "J" + Trim(Str(Rango))).Borders().LineStyle = 1
        Hoja.Cells(Rango + 1, 7).Value = "TOTAL"
        Hoja.Cells(Rango + 2, 7).Value = "ANULADOS"
        Hoja.Cells(Rango + 3, 7).Value = "TOTAL FINAL"

        Hoja.Range("G" + Trim(Str(Rango + 1)), "H" + Trim(Str(Rango + 3))).Borders().LineStyle = 1
        Hoja.Range("G" + Trim(Str(Rango + 1)), "H" + Trim(Str(Rango + 3))).Font.Bold = 1
        Hoja.Range("G" + Trim(Str(Rango + 1)), "G" + Trim(Str(Rango + 3))).Interior.Color = RGB(221, 235, 247)

        Hoja.Columns.AutoFit()

        Hoja.Cells(Rango + 1, 8).Value = nTotal
        Hoja.Cells(Rango + 2, 8).Value = nAnulados
        Hoja.Cells(Rango + 3, 8).Value = nTotal - nAnulados


        Libro.Save()
        'Libro.Close()
        'Libro = Nothing

        Aplicacion.Visible = True

        releaseObject(Aplicacion)
        releaseObject(Libro)
        releaseObject(Hoja)

        Dim rutaExcel As String = "D:\pdfexport.pdf"
        ExceltoPdf(rutaNuevoExcel, rutaExcel)

        Try
            Process.Start(rutaExcel)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        'Dim result As DialogResult = PrintDialog1.ShowDialog()
        'If (result = DialogResult.OK) Then
        '    Dim psi As New ProcessStartInfo
        '    psi.UseShellExecute = True
        '    psi.Verb = "01M-HALL4PISO"
        '    psi.WindowStyle = ProcessWindowStyle.Hidden
        '    psi.Arguments = $"""{PrintDialog1.PrinterSettings.PrinterName}"""
        '    psi.FileName = rutaExcel
        '    Process.Start(psi)
        'End If

        'Dim Esperas As Integer = 0
        'Using p As New Process
        '    p.StartInfo.FileName = rutaExcel
        '    p.StartInfo.Verb = "01M-HALL4PISO"
        '    p.Start()
        '    Threading.Thread.Sleep(3000) ' tiempo X para que el programa cliente se active he imprima
        '    p.CloseMainWindow() ' Cierre ventana cliente
        '    ' si la ventana sigue abierta, se encicla hasta cerrarla.
        '    While Not p.HasExited
        '        Threading.Thread.Sleep(1000)
        '        Esperas += 1
        '        p.CloseMainWindow()
        '    End While
        'End Using
        'MsgBox("Fin del proceso # " & Esperas) ' cantidad de esperas para poder cerrar ventana cliente


        BuscarComprobante(nidEmpresa, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue, 0)
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub ctncerrar_Click(sender As Object, e As EventArgs) Handles ctncerrar.Click
        panel1.Visible = False
    End Sub


    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        ActualizaFechaComprobante(nidEmpresa, nIdVenta, dtpfechanueva.Text)
        ctncerrar.PerformClick()
        BuscarComprobante(nidEmpresa, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue)
    End Sub

    Private Sub panel1_Leave(sender As Object, e As EventArgs) Handles panel1.Leave
        panel1.Visible = False
    End Sub

    Private Sub dgvlista_DoubleClick(sender As Object, e As EventArgs) Handles dgvlista.DoubleClick
        Try
            lblseriecorrelativo.Text = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(3).Value & "-" & dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(4).Value
            dtpfechanueva.Text = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(5).Value
            nIdVenta = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(1).Value
            panel1.Visible = True
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnValidarResumen_Click(sender As Object, e As EventArgs) Handles btnValidarResumen.Click
        Try

            If dgvlista.Rows.Count > 0 Then

                Dim cTicket As String
                cTicket = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(14).Value

                If cTicket.Trim() = "" Then
                    Return
                End If

                Dim serv As New CapaSUNAT.CapaSUNAT.Servicios.SUNAT_UTIL
                Dim resp As String = serv.ObtenerEstado(cTicket)

                If resp = "0" Then MessageBox.Show("Procesado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If resp = "1" Then MessageBox.Show("En Proceso...", "Procesando", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If resp = "2" Then MessageBox.Show("Procesado con Errores", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                If resp = "0098" Then MessageBox.Show("En Proceso. Vuelva a consultar el ticket mas tarde!", "Procesando", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If resp = "0127" Then MessageBox.Show("!Ticket no existe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])

                '0   Procesado correctamente	Disponible
                '98  En Proceso	Volver a consultar
                '99  Procesado con errores	Disponible

            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub cbdocs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbdocs.SelectedIndexChanged
        Try
            BuscarComprobante(nidEmpresa, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue)

            If cbdocs.SelectedValue = "3" Then
                btngenerarResumen.Enabled = True
            Else
                btngenerarResumen.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnvalidarcpe_Click(sender As Object, e As EventArgs) Handles btnvalidarcpe.Click

        Dim ruc, nTipoDoc, cSerie As String
        Dim nCorrelativo As Integer

        ruc = CEmpresaRUC

        nTipoDoc = ""
        If dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(2).Value = "1" Then   ' SI ES BOLETA
            nTipoDoc = "03"
        End If
        If dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(2).Value = "2" Then   ' SI ES FACTURA
            nTipoDoc = "01"
        End If

        cSerie = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(3).Value
        nCorrelativo = dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(4).Value

        Dim serv As New CapaSUNAT.CapaSUNAT.Servicios.SUNAT_UTIL
        txtmensaje.Text = serv.ConsultaEstadoCPE_Ws(ruc, nTipoDoc, cSerie, nCorrelativo)
        ActualizaEstadoComprobante(nidEmpresa, dgvlista.Rows(dgvlista.CurrentRow.Index).Cells(1).Value, "", "", "", txtmensaje.Text, "")

    End Sub

    Private Sub btnEnvioMasivo_Click(sender As Object, e As EventArgs) Handles btnEnvioMasivo.Click
        If dgvlista.Rows.Count > 0 Then
            For Renglones As Integer = 0 To dgvlista.RowCount - 1
                If Me.dgvlista.Item(2, Renglones).Value.ToString = "1" Or Me.dgvlista.Item(2, Renglones).Value.ToString = "2" Then
                    If dgvlista.Item(9, Renglones).Value.ToString <> "0" Then
                        EnviarCPE(Me.dgvlista.Item(1, Renglones).Value, 1)
                    End If
                End If
            Next
            BuscarComprobante(nidEmpresa, txtbuscar.Text, 0, dtpfecha.Text, dgvlista, cbdocs.SelectedValue)
        End If
    End Sub

    Private Sub dgvempresas_DoubleClick(sender As Object, e As EventArgs) Handles dgvempresas.DoubleClick
        Try
            If IsNumeric(dgvempresas.Rows(dgvempresas.CurrentRow.Index).Cells(0).Value) Then
                Dim dt = ExtraerDatosEmpresa(dgvempresas.Rows(dgvempresas.CurrentRow.Index).Cells(0).Value)
                Dim EmpresaRUC = dt.Rows(0)(7).ToString()

                Dim OriginalFile = Application.StartupPath() & "\CONFIG\" & EmpresaRUC & ".exe.config"
                Dim FileToReplace = Application.StartupPath() & "\SUNAT.exe.config"
                My.Computer.FileSystem.CopyFile(OriginalFile, FileToReplace, True)

                nidEmpresa = dgvempresas.Rows(dgvempresas.CurrentRow.Index).Cells(0).Value
                ActualizarSeleccionEmpresa(nidEmpresa)
                Application.Restart()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub DarDeBaja(nIdVentaaaa)

        Try


            Dim dt1 = ExtraerDatosCPE(nidEmpresa, nIdVentaaaa)
            Dim Serie As String = dt1.Rows(0)(0).ToString
            Dim Numero As String = dt1.Rows(0)(1).ToString
            Dim Fechaemision As Date = dt1.Rows(0)(2).ToString
            Dim cTipoComprobante As String = dt1.Rows(0)(4).ToString()

            Dim serv As New CapaSUNAT.CapaSUNAT.Servicios.SUNAT_UTIL

            serv.Ruta_XML = Path.GetDirectoryName(Application.ExecutablePath) & "\XML\" & CEmpresaRUC & "\"
            serv.Ruta_Certificado = Path.GetDirectoryName(Application.ExecutablePath) & "\CERTIFICADO\" & CEmpresaRUC & ".pfx"
            serv.Password_Certificado = CclaveCert
            serv.Ruta_ENVIOS = Path.GetDirectoryName(Application.ExecutablePath) & "\ENVIOS\" & CEmpresaRUC & "\"
            serv.Ruta_CDRS = Path.GetDirectoryName(Application.ExecutablePath) & "\CDR\" & CEmpresaRUC & "\"
            Dim rucEmpresa As String = CEmpresaRUC
            Dim razonEmpresa As String = CEmpresaRazonSocial

            Dim correl As Integer = ObtenerCorrelativos(nidEmpresa, dtpfecha.Text, 25)
            Dim numTicket As String = serv.GenerarComunicacionBaja_XML(Convert.ToDateTime(Fechaemision), rucEmpresa, razonEmpresa, cTipoComprobante, Serie, Numero, "ANULACION DEL DOCUMENTO", correl)

            If Trim(numTicket) <> "" Then
                ActualizaEstadoComprobante(nidEmpresa, nIdVentaaaa, "1", "DADO DE BAJA", "", "XX-DADO DE BAJA", numTicket)
            End If

            'txtTicket.Text = numTicket
            MessageBox.Show("Enviado a SUNAT, debe consultar el estado a tráves del número de ticket: " & numTicket, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            'txtTicket.Text = "0"
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try

    End Sub


    Public Function ExceltoPdf(ByVal excelLocation As String, ByVal outputLocation As String) As String
        Try
            Dim app As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
            app.Visible = False
            Dim wkb As Microsoft.Office.Interop.Excel.Workbook = app.Workbooks.Open(excelLocation)
            wkb.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, outputLocation)
            wkb.Close()
            app.Quit()
            Return outputLocation
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Throw ex
        End Try
    End Function

End Class
