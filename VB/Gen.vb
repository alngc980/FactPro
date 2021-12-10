Imports System.Data.SqlClient
Imports System.IO

Public Class Gen
    Public RUC As String
    Public Ruta As String
    Public AFECTACION As String = "EXO" ' IGV

    '**************************************************************************************
    Public Sub Generar_CAB(DOC As String, SERIE As String, CORRELATIVO As String, CONTENIDO As String)
        Dim G As String = "-"
        Dim EXTENSION As String = ".CAB"

        Dim TITULO_CAB As String = RUC + G + DOC + G + SERIE + G + CORRELATIVO + EXTENSION

        Dim file As String = Ruta + TITULO_CAB

        Dim sw As New System.IO.StreamWriter(file, False, System.Text.Encoding.Default)
        sw.WriteLine(CONTENIDO)
        sw.Close()
    End Sub
    Public Sub Generar_DET(DOC As String, SERIE As String, CORRELATIVO As String, CONTENIDO As String)
        Dim G As String = "-"
        Dim EXTENSION As String = ".DET"

        Dim TITULO_CAB As String = RUC + G + DOC + G + SERIE + G + CORRELATIVO + EXTENSION

        Dim file As String = Ruta + TITULO_CAB

        Dim sw As New System.IO.StreamWriter(file, False, System.Text.Encoding.Default)
        sw.WriteLine(CONTENIDO)
        sw.Close()
    End Sub
    Public Sub Generar_TRI(DOC As String, SERIE As String, CORRELATIVO As String, CONTENIDO As String)
        Dim G As String = "-"
        Dim EXTENSION As String = ".TRI"

        Dim TITULO_CAB As String = RUC + G + DOC + G + SERIE + G + CORRELATIVO + EXTENSION

        Dim file As String = Ruta + TITULO_CAB

        Dim sw As New System.IO.StreamWriter(file, False, System.Text.Encoding.Default)
        sw.WriteLine(CONTENIDO)
        sw.Close()
    End Sub
    Public Sub Generar_LEY(DOC As String, SERIE As String, CORRELATIVO As String, CONT As String)
        Dim G As String = "-"
        Dim EXTENSION As String = ".LEY"

        Dim CONTENIDO As String = "1000|" & CONT
        Dim TITULO_CAB As String = RUC + G + DOC + G + SERIE + G + CORRELATIVO + EXTENSION

        Dim file As String = Ruta + TITULO_CAB

        Dim sw As New System.IO.StreamWriter(file, False, System.Text.Encoding.Default)
        sw.WriteLine(CONTENIDO)
        sw.Close()
    End Sub
    '**************************************************************************************
    Public Sub Generar_RESUMEN_RDI(FECHA As String, CONTENIDO As String)
        Dim G As String = "-"
        Dim EXTENSION As String = ".RDI"

        Dim TITULO_CAB As String = RUC + G + "RC" + G + FECHA + G + "001" + EXTENSION

        Dim file As String = Ruta + TITULO_CAB

        Dim sw As New System.IO.StreamWriter(file, False, System.Text.Encoding.Default)
        sw.WriteLine(CONTENIDO)
        sw.Close()
    End Sub

    Public Sub Generar_RESUMEN_TRD(FECHA As String, CONTENIDO As String)
        Dim G As String = "-"
        Dim EXTENSION As String = ".TRD"

        Dim TITULO_CAB As String = RUC + G + "RC" + G + FECHA + G + "001" + EXTENSION

        Dim file As String = Ruta + TITULO_CAB

        Dim sw As New System.IO.StreamWriter(file, False, System.Text.Encoding.Default)
        sw.WriteLine(CONTENIDO)
        sw.Close()
    End Sub



    Public Sub GenerarResumenDiario(dtpfecha As DateTimePicker, datagrid As DataGridView, Afectacion As String)

        Try
            Dim GENERAR As Boolean = True
            If datagrid.Rows.Count > 0 Then
                If ExisteArchivo(dtpfecha.Text) Then
                    Dim RESUL As MsgBoxResult = MsgBox("EL RESUMEN DE BOLETAS PARA ESTE DIA YA EXISTE!" & vbCrLf & "¿DESEA GENERAR NUEVAMENTE?", MsgBoxStyle.YesNo, "MENSAJE")
                    If RESUL = DialogResult.Yes Then
                        GENERAR = True
                    Else
                        GENERAR = False
                    End If
                Else
                    GENERAR = True
                End If

                If GENERAR = True Then
                    Dim CONTENIDO_RDI As String = ""
                    Dim CONTENIDO_TRD As String = ""
                    Dim CONTA As Integer = 1
                    Dim PA As String = "|"
                    Dim hasta As Integer = datagrid.RowCount - 1
                    For Renglones As Integer = 0 To datagrid.RowCount - 1
                        Dim N1_FECHA As String = Format(CDate(dtpfecha.Text), "yyyy-MM-dd")
                        Dim N2_FECHA2 As String = N1_FECHA
                        Dim N3_TIPODOC As String = "03"
                        Dim N4_SERIECOR As String = datagrid.Item(2, Renglones).Value
                        Dim N5_TIPODNI As String = Validar_Documento_Identidad_Resumen(datagrid.Item(3, Renglones).Value)
                        Dim N6_NUMDNI As String = datagrid.Item(3, Renglones).Value
                        Dim N7_MONEDA As String = "PEN"
                        Dim N8_T_GRAV As String
                        Dim N9_T_EXON As String
                        If Afectacion = "1" Then
                            N8_T_GRAV = Math.Round(datagrid.Item(4, Renglones).Value / (1 + 0.18), 2)
                            N9_T_EXON = "0.00"
                        Else
                            N8_T_GRAV = "0.00"
                            N9_T_EXON = datagrid.Item(4, Renglones).Value
                        End If
                        Dim N10_T_INAF As String = "0.00"
                        Dim N11_T_EXPO As String = "0.00"
                        Dim N12_T_GRAT As String = "0.00"
                        Dim N13_T_OTROSCARGOS As String = "0.00"
                        Dim N14_T_IMPORTEVENTA As String = datagrid.Item(4, Renglones).Value
                        Dim N15_Estado As String = ""
                        If datagrid.Item(5, Renglones).Value = "0" Then
                            N15_Estado = "1"
                        Else
                            N15_Estado = "3"
                        End If

                        If Renglones = hasta Then
                            CONTENIDO_RDI = CONTENIDO_RDI & N1_FECHA & PA & N2_FECHA2 & PA & N3_TIPODOC & PA &
                                        N4_SERIECOR & PA & N5_TIPODNI & PA & N6_NUMDNI & PA & N7_MONEDA & PA & N8_T_GRAV &
                                        PA & N9_T_EXON & PA & N10_T_INAF & PA & N11_T_EXPO & PA & N12_T_GRAT & PA &
                                        N13_T_OTROSCARGOS & PA & N14_T_IMPORTEVENTA & PA & "||||||0|0|" & N15_Estado

                            If Afectacion = "1" Then
                                CONTENIDO_TRD = CONTENIDO_TRD & CONTA & "|1000|IGV|VAT|" & N8_T_GRAV & "|" & Math.Round(Convert.ToDecimal(N14_T_IMPORTEVENTA), 2) - Math.Round(Convert.ToDecimal(N8_T_GRAV), 2)
                            Else
                                CONTENIDO_TRD = CONTENIDO_TRD & CONTA & "|1000|IGV|VAT|0.00|0.00"
                            End If


                        Else
                            CONTENIDO_RDI = CONTENIDO_RDI & N1_FECHA & PA & N2_FECHA2 & PA & N3_TIPODOC & PA &
                                        N4_SERIECOR & PA & N5_TIPODNI & PA & N6_NUMDNI & PA & N7_MONEDA & PA & N8_T_GRAV &
                                        PA & N9_T_EXON & PA & N10_T_INAF & PA & N11_T_EXPO & PA & N12_T_GRAT & PA &
                                        N13_T_OTROSCARGOS & PA & N14_T_IMPORTEVENTA & PA & "||||||0|0|" & N15_Estado & vbCrLf

                            If Afectacion = "1" Then
                                CONTENIDO_TRD = CONTENIDO_TRD & CONTA & "|1000|IGV|VAT|" & N8_T_GRAV & "|" & Math.Round(Convert.ToDecimal(N14_T_IMPORTEVENTA), 2) - Math.Round(Convert.ToDecimal(N8_T_GRAV), 2) & vbCrLf
                            Else
                                CONTENIDO_TRD = CONTENIDO_TRD & CONTA & "|1000|IGV|VAT|0.00|0.00" & vbCrLf
                            End If
                        End If

                        CONTA = CONTA + 1
                    Next
                    Generar_RESUMEN_RDI(Format(CDate(dtpfecha.Text), "yyyyMMdd"), CONTENIDO_RDI)
                    Generar_RESUMEN_TRD(Format(CDate(dtpfecha.Text), "yyyyMMdd"), CONTENIDO_TRD)
                    MsgBox("SE HA GENERADO EL RESUMEN DIARIO DE BOLETAS", MsgBoxStyle.Information, "MENSAJE")
                End If
            Else
                MsgBox("NO SE PUEDE GENERAR EL RESUMEN PORQUE NO EXISTEN BOLETAS!", MsgBoxStyle.Information, "MENSAJE")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "ERROR AL GENERAR EL RESUMEN DIARIO!")
        End Try
    End Sub

    Function ExisteArchivo(fecha As Date) As Boolean
        Dim FechaC As String = Format(CDate(fecha), "yyyyMMdd")
        If File.Exists(Ruta & RUC & "-RC-" & FechaC & "-001.RDI") Then
            Return True
        Else
            Return False
        End If
    End Function

    Function Validar_Documento_Identidad_Resumen(DOCUMENT As String) As String
        If DOCUMENT.Length = 8 Then
            Return "1"          ' CODIGO DE DNI
        Else
            Return "0"          ' CODIGO SIN DOCUMENTO
        End If
    End Function

    '*******************************************************************************************************************
    '*******************************************************************************************************************

    Dim scn As New SqlConnection
    Dim sda As SqlDataAdapter

    Dim conexion As SqlConnection
    Public Function conectar() As SqlConnection
        conexion = New SqlConnection("SERVER=SERVER;database=DBFacturador;Integrated security=true")
        Return conexion
    End Function


    Public Sub GenerarTxt_Venta(IdEmpresa As String, IdVenta As String)
        Try
            scn = conectar()
            scn.Open()
            Dim comand As SqlCommand

            comand = New SqlCommand("exec rpt_comprobante " + IdEmpresa + ", " + IdVenta, scn)
            sda = New SqlDataAdapter(comand)
            Dim dt As New DataTable()
            sda.Fill(dt)
            If dt.Rows.Count() > 0 Then

                ' INICIO CREACION CONTENIDO CABECERA ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim fechaEmision As DateTime = dt.Rows(0)(6)
                Dim MES As String = Validando_Fechas(fechaEmision.Month)
                Dim DIA As String = Validando_Fechas(fechaEmision.Day)
                Dim CONTENIDO_CAB As String = ""
                Dim PA As String = "|"

                Dim N1_TIPO_OPERACION As String = "0101"                               '' CATALOGO 51      |0101 VENTA INTERNA |0102 EXPORTACION
                Dim N2_FECHA_EMISION As String = fechaEmision.Year & "-" & MES & "-" & DIA
                Dim N3_HORA_EMISION As String = fechaEmision.ToLongTimeString ''Principal.lblhora.Text
                Dim N4_FECHA_VENCE As String = "-"
                Dim N5_CODIGO_DOMICILIO_EMISOR As String = "0000" ' dt.Rows(0)(22).ToString
                Dim N6_TIPO_DOC_IDENTIDAD As String = Validar_Documento_Identidad(dt.Rows(0)(7).ToString)    '' CATALOGO 6       |1 DNI |4 CARNET EXTRANJERIA |6 RUC |7 PASAPORTE
                Dim N7_NUMERO_DOC_IDENTI As String = dt.Rows(0)(7).ToString ' txtdniruc.Text
                Dim N8_NOMBRE_RAZON As String = dt.Rows(0)(8).ToString ' txtnombres.Text
                Dim N9_TIPO_MONEDA As String = "PEN"                                   '' CATALOGO 2       |PEN |USD
                Dim N10_SUMATORIA_TRIBUTOS As String = "0"
                Dim N11_TOTAL_VALOR_VENTA As String = Convert.ToDecimal(dt.Rows(0)(9)) 'txttotal.Text)
                Dim N12_TOTAL_PRECIO_VENTA As String = N11_TOTAL_VALOR_VENTA
                Dim N13_TOTAL_DESCUENTOS As String = "0"
                Dim N14_SUMATORIA_OTROS_CARGOS As String = "0"
                Dim N15_TOTAL_ANTICIPOS As String = "0"
                Dim N16_IMPORTE_TOTAL_VENTA As String = N12_TOTAL_PRECIO_VENTA
                Dim N17_VERSION_UBL As String = "2.1"
                Dim N18_CUSTOM_DOC As String = "2.0"
                CONTENIDO_CAB = CONTENIDO_CAB & N1_TIPO_OPERACION & PA & N2_FECHA_EMISION & PA & N3_HORA_EMISION & PA & N4_FECHA_VENCE & PA & N5_CODIGO_DOMICILIO_EMISOR & PA &
                    N6_TIPO_DOC_IDENTIDAD & PA & N7_NUMERO_DOC_IDENTI & PA & N8_NOMBRE_RAZON & PA & N9_TIPO_MONEDA & PA & N10_SUMATORIA_TRIBUTOS & PA & N11_TOTAL_VALOR_VENTA & PA & N12_TOTAL_PRECIO_VENTA &
                    PA & N13_TOTAL_DESCUENTOS & PA & N14_SUMATORIA_OTROS_CARGOS & PA & N15_TOTAL_ANTICIPOS & PA & N16_IMPORTE_TOTAL_VENTA & PA & N17_VERSION_UBL & PA & N18_CUSTOM_DOC
                ' FINALIZACION CREACION DE CONTENIDO CABECERA ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim CONTENIDO_DET As String = ""
                Dim hasta As Integer = dt.Rows.Count() - 1
                For i = 0 To dt.Rows.Count() - 1 Step 1
                    ''''''' INICIO DE CREACION DE CONTENIDO DET''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim N1_COD_UNIDAD_MEDIDA As String = "NIU"                                         '' CATALOGO 3
                    Dim N2_CANTIDAD_UNIDADES_POR_ITEM As String = dt.Rows(i)(11).ToString ' edv.Vd_cant
                    Dim N3_COD_PRODUCTO As String = dt.Rows(i)(13).ToString '"SER0" & Renglones
                    Dim N4_COD_PRODUCTO_SUNAT As String = "-"
                    Dim N5_DESCRIPCION As String = dt.Rows(i)(15).ToString
                    Dim N6_VALOR_UNITARIO As String = dt.Rows(i)(16).ToString ' edv.Vd_precio
                    Dim N7_SUMATORIA_TRIBUTOS_ITEM As String = "0"
                    Dim N8_TRIBUTO As String = "9997"                                               '' CATALOGO 5   |1000 IGV  |9995 EXPORTACION
                    Dim N9_MONTO_IGV_ITEM As String = "0"
                    Dim N1O_BASE_IMPONIBLE_IGV As String = dt.Rows(i)(17).ToString ' edv.Vd_subtotal
                    Dim N11_NOMBRE_TRIBUTO_ITEM As String = "EXO"                                   '' CATALOGO 5 NAME      |
                    Dim N12_COD_TIPO_TRIBUTO As String = "VAT"                                      '' CATALOGO 5
                    Dim N13_AFECTACION_AL_IGV_ITEM As String = "20"                                 '' CATALOGO 7   |40 EXPORTACION |10 GRABADO-OPERACION ONEROSA
                    Dim N14_PORCENTAJE_IGV As String = "0"                                       '' CATALOGO 5
                    Dim N15_TIPO_ISC As String = "-"
                    Dim N16_MONTO_ISC_ITEM As String = ""
                    Dim N17_BASE_IMPONIBLE_ISC As String = ""
                    Dim N18_NOMBRE_TRIBUTO_ITEM As String = ""
                    Dim N19_COD_TIPO_TRIBUTO_ITEM As String = ""
                    Dim N20_TIPO_SISTEMA_ISC As String = ""
                    Dim N21_PORCENTAJE_ISC As String = ""
                    Dim N22_TIPO_TRIBUTO_OTRO As String = "-"
                    Dim N23_MONTO_OTRO_TRIBUTO As String = ""
                    Dim N24_BASE_OTRO_TRIBUTO As String = ""
                    Dim N25_NOMBRE_OTRO_TRIBUTO As String = ""                                      '' CATALOGO 5 NAME
                    Dim N26_COD_OTRO_TRIBUTO As String = ""                                         '' CATALOGO 5
                    Dim N27_PORCENTAJE_OTRO_TRI As String = ""

                    Dim N28_TRIBUTO_ICBPER As String = "-"
                    Dim N29_MONTOTRIBUTO_ICBP As String = "0.00"
                    Dim N30_CANTDEOLSAS_ICBP As String = "0"
                    Dim N31_TRIBUTO_ICBPER As String = "ICBPER"
                    Dim N32_TRIBUTO_ICBPER As String = "OTH"
                    Dim N33_TRIBUTO_ICBPER As String
                    Select Case Now.Year.ToString
                        Case "2020"
                            N33_TRIBUTO_ICBPER = "0.20"
                        Case "2021"
                            N33_TRIBUTO_ICBPER = "0.30"
                        Case "2022"
                            N33_TRIBUTO_ICBPER = "0.40"
                        Case Else
                            N33_TRIBUTO_ICBPER = "0.50"
                    End Select

                    Dim N34_PRECIO_VENTA_UNITARIO As String = dt.Rows(i)(16).ToString
                    Dim N35_VALOR_VENTA_ITEM As String = dt.Rows(i)(17).ToString
                    Dim N36_VALOR_REFER_UNITA As String = "0.00"

                    If i = hasta Then
                        CONTENIDO_DET = CONTENIDO_DET & N1_COD_UNIDAD_MEDIDA & PA & N2_CANTIDAD_UNIDADES_POR_ITEM & PA & N3_COD_PRODUCTO & PA &
                        N4_COD_PRODUCTO_SUNAT & PA & N5_DESCRIPCION & PA & N6_VALOR_UNITARIO & PA & N7_SUMATORIA_TRIBUTOS_ITEM & PA & N8_TRIBUTO &
                        PA & N9_MONTO_IGV_ITEM & PA & N1O_BASE_IMPONIBLE_IGV & PA & N11_NOMBRE_TRIBUTO_ITEM & PA & N12_COD_TIPO_TRIBUTO & PA &
                        N13_AFECTACION_AL_IGV_ITEM & PA & N14_PORCENTAJE_IGV & PA & N15_TIPO_ISC & PA & N16_MONTO_ISC_ITEM & PA & N17_BASE_IMPONIBLE_ISC &
                        PA & N18_NOMBRE_TRIBUTO_ITEM & PA & N19_COD_TIPO_TRIBUTO_ITEM & PA & N20_TIPO_SISTEMA_ISC & PA & N21_PORCENTAJE_ISC &
                        PA & N22_TIPO_TRIBUTO_OTRO & PA & N23_MONTO_OTRO_TRIBUTO & PA & N24_BASE_OTRO_TRIBUTO & PA & N25_NOMBRE_OTRO_TRIBUTO &
                        PA & N26_COD_OTRO_TRIBUTO & PA & N27_PORCENTAJE_OTRO_TRI & PA & N28_TRIBUTO_ICBPER & PA & N29_MONTOTRIBUTO_ICBP & PA &
                        N30_CANTDEOLSAS_ICBP & PA & N31_TRIBUTO_ICBPER & PA & N32_TRIBUTO_ICBPER & PA & N33_TRIBUTO_ICBPER & PA & N34_PRECIO_VENTA_UNITARIO & PA & N35_VALOR_VENTA_ITEM &
                        PA & N36_VALOR_REFER_UNITA
                    Else
                        CONTENIDO_DET = CONTENIDO_DET & N1_COD_UNIDAD_MEDIDA & PA & N2_CANTIDAD_UNIDADES_POR_ITEM & PA & N3_COD_PRODUCTO & PA &
                        N4_COD_PRODUCTO_SUNAT & PA & N5_DESCRIPCION & PA & N6_VALOR_UNITARIO & PA & N7_SUMATORIA_TRIBUTOS_ITEM & PA & N8_TRIBUTO &
                        PA & N9_MONTO_IGV_ITEM & PA & N1O_BASE_IMPONIBLE_IGV & PA & N11_NOMBRE_TRIBUTO_ITEM & PA & N12_COD_TIPO_TRIBUTO & PA &
                        N13_AFECTACION_AL_IGV_ITEM & PA & N14_PORCENTAJE_IGV & PA & N15_TIPO_ISC & PA & N16_MONTO_ISC_ITEM & PA & N17_BASE_IMPONIBLE_ISC &
                        PA & N18_NOMBRE_TRIBUTO_ITEM & PA & N19_COD_TIPO_TRIBUTO_ITEM & PA & N20_TIPO_SISTEMA_ISC & PA & N21_PORCENTAJE_ISC &
                        PA & N22_TIPO_TRIBUTO_OTRO & PA & N23_MONTO_OTRO_TRIBUTO & PA & N24_BASE_OTRO_TRIBUTO & PA & N25_NOMBRE_OTRO_TRIBUTO &
                        PA & N26_COD_OTRO_TRIBUTO & PA & N27_PORCENTAJE_OTRO_TRI & PA & N28_TRIBUTO_ICBPER & PA & N29_MONTOTRIBUTO_ICBP & PA &
                        N30_CANTDEOLSAS_ICBP & PA & N31_TRIBUTO_ICBPER & PA & N32_TRIBUTO_ICBPER & PA & N33_TRIBUTO_ICBPER & PA & N34_PRECIO_VENTA_UNITARIO & PA & N35_VALOR_VENTA_ITEM &
                        PA & N36_VALOR_REFER_UNITA & vbCrLf
                    End If
                Next


                Dim CONTENIDO_TRI As String
                CONTENIDO_TRI = "9997|EXO|VAT|" & Trim(N12_TOTAL_PRECIO_VENTA) & "|0.00"
                'CREANDO LOS ARCHIVOS TXT PARA EL FACTURADOR'''''''''''''''''''''''''''''''''''
                'A PARTIR DE LOS DATOS QUE TENEMOS'''''''''''''''''''''''''''''''''''''''''''''

                If dt.Rows(0)(3).ToString = "2" Then
                    Generar_CAB("01", dt.Rows(0)(4).ToString, dt.Rows(0)(5).ToString, CONTENIDO_CAB)    'FACTURA 
                    Generar_DET("01", dt.Rows(0)(4).ToString, dt.Rows(0)(5).ToString, CONTENIDO_DET)
                    Generar_TRI("01", dt.Rows(0)(4).ToString, dt.Rows(0)(5).ToString, CONTENIDO_TRI)
                    Generar_LEY("01", dt.Rows(0)(4).ToString, dt.Rows(0)(5).ToString, dt.Rows(0)(10).ToString)
                Else
                    Generar_CAB("03", dt.Rows(0)(4).ToString, dt.Rows(0)(5).ToString, CONTENIDO_CAB)    'BOLETA
                    Generar_DET("03", dt.Rows(0)(4).ToString, dt.Rows(0)(5).ToString, CONTENIDO_DET)
                    Generar_TRI("03", dt.Rows(0)(4).ToString, dt.Rows(0)(5).ToString, CONTENIDO_TRI)
                    Generar_LEY("03", dt.Rows(0)(4).ToString, dt.Rows(0)(5).ToString, dt.Rows(0)(10).ToString)
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function Validando_Fechas(valor As Integer) As String
        If valor <= 9 Then
            Return "0" & valor
        Else
            Return valor
        End If
    End Function
    Function Validar_Documento_Identidad(TipoDoc As String) As String
        If TipoDoc = "BOLETA DE VENTA ELECTRONICA" Then
            Return "1"          ' CODIGO DE DNI
        Else
            Return "6"          ' CODIGO DE RUC
        End If
    End Function


    Dim dt As New DataTable
    Dim scmd As New SqlCommand
    Dim ds As New DataSet

    Public Function ModificarFecha(Fecha As String, Empresa As String, IdVenta As String)
        Try
            scn = conectar()
            scn.Open()

            scmd = New SqlCommand("update VentasFact set dFechaHora = '" + Fecha + "' where nIdEmpresa = " + Empresa + " and nIdVenta = " + IdVenta, scn)
            scmd.CommandType = CommandType.Text

            scmd.ExecuteNonQuery()
            MsgBox("Registro Actualizado", MsgBoxStyle.Information, "MENSAJE")
            Return scmd
        Catch ex As Exception
            Throw ex
        Finally
            scn.Close()
            scn.Dispose()
            scmd.Dispose()
        End Try
    End Function
End Class
