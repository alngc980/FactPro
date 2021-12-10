Imports System.IO.Compression
Imports System.IO

Imports System.Net
Imports System.Net.Mail

Imports System.Xml
Imports CapaSUNAT
Imports ZXing
Imports ZXing.QrCode
Imports System.Text.RegularExpressions

Module Util
    Public Function SoloNumeros(tecla As Integer) As Boolean
        If ((tecla >= 48 And tecla <= 57) Or tecla = 8) Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function Totalizar(dgv As DataGridView, columna As Integer) As Decimal
        Dim total As Decimal = 0
        For Each item As DataGridViewRow In dgv.Rows
            total += item.Cells(columna).Value
        Next
        Return total
    End Function

    Public Sub GenerarQR(ByVal RUC As String, ByVal Tipocomprobante As String, ByVal Numeración As String, ByVal SumatoriaIGV As String,
                         ByVal ImporteTotalVenta As String, ByVal FechaEmisión As String, ByVal TipoDocumentoAdquirente As String,
                         ByVal NúmeroDocumentoAdquirente As String, ByVal CódigoHash As String)
        Dim result As Bitmap = Nothing
        Dim options As QrCodeEncodingOptions = New QrCodeEncodingOptions()
        options = New QrCodeEncodingOptions With {
            .DisableECI = True,
            .CharacterSet = "UTF-8",
            .Width = 250,
            .Height = 250
        }
        Dim writer = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE
        writer.Options = options

        If String.IsNullOrWhiteSpace(RUC) OrElse String.IsNullOrEmpty(Tipocomprobante) OrElse String.IsNullOrEmpty(Numeración) OrElse String.IsNullOrWhiteSpace(SumatoriaIGV) OrElse String.IsNullOrEmpty(ImporteTotalVenta) OrElse String.IsNullOrEmpty(FechaEmisión) OrElse String.IsNullOrWhiteSpace(ImporteTotalVenta) OrElse String.IsNullOrWhiteSpace(FechaEmisión) OrElse String.IsNullOrWhiteSpace(TipoDocumentoAdquirente) OrElse String.IsNullOrWhiteSpace(NúmeroDocumentoAdquirente) OrElse String.IsNullOrWhiteSpace(CódigoHash) Then
            Throw New Exception("Debe proporcionar todos los parametros para la generación del QR")
        Else
            Dim codigo As String = RUC & "|" & Tipocomprobante & "|" & Numeración & "|" & SumatoriaIGV & "|" & ImporteTotalVenta & "|" & FechaEmisión & "|" & TipoDocumentoAdquirente & "|" & NúmeroDocumentoAdquirente & "|" & CódigoHash
            Dim qr = New ZXing.BarcodeWriter()
            qr.Options = options
            qr.Format = ZXing.BarcodeFormat.QR_CODE
            result = New Bitmap(qr.Write(codigo.Trim()))
            result.Save(Application.StartupPath() & "\CODBARRAS\" & RUC & "-" & Numeración & ".jpg")
            'Return result
        End If
    End Sub

    Public Function ObtenerDigestValueSunat(archxml As String) As String
        Dim oXmlSunat As XmlDocument = New XmlDocument()
        oXmlSunat.Load(archxml)
        Dim manager As XmlNamespaceManager = New XmlNamespaceManager(oXmlSunat.NameTable)
        manager.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        manager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")
        Dim valor As String = oXmlSunat.SelectSingleNode("//ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/ds:Signature/ds:SignedInfo/ds:Reference/ds:DigestValue", manager).InnerText
        Return valor
    End Function

    Public Function EnviarCorreo(ByVal para As String, ByVal asunto As String, ByVal mensaje As String, ByVal adjunto As String, ByVal emailcuenta As String,
                                 ByVal nombreCuenta As String, ByVal password As String, ByVal puerto As Integer, ByVal smtp As String) As String
        'Crear un mensaje
        Dim msje As MailMessage = New MailMessage()

        Try
            msje.From = New MailAddress(emailcuenta, nombreCuenta)  'de

            'msje.To.Add(para) 'para (pueden ser varios porque es una coleccion)

            Dim texto As String = para
            'Split con array de delimitadores
            Dim delimitadores() As String = {";"}
            Dim vectoraux() As String
            vectoraux = texto.Split(delimitadores, StringSplitOptions.None)
            'mostrar resultado
            For Each item As String In vectoraux
                If EsCorreo(item.Trim()) Then
                    msje.To.Add(item.Trim())
                End If
            Next


            msje.Subject = asunto
            msje.Body = mensaje
            msje.IsBodyHtml = False


            'msje.Attachments.Add(New Attachment(adjunto))  'Adjuntar el archivo al correo

            msje.Attachments.Clear()
            Dim adjuntoads As String = adjunto
            Dim delimitadoresadjunto() As String = {";"}
            Dim vectorauxadjunto() As String
            vectorauxadjunto = adjuntoads.Split(delimitadoresadjunto, StringSplitOptions.None)
            For Each item As String In vectorauxadjunto
                msje.Attachments.Add(New Attachment(item))
            Next


            Dim credenciales As NetworkCredential = New NetworkCredential()
            credenciales.UserName = emailcuenta
            credenciales.Password = password
            Dim servidor As SmtpClient = New SmtpClient()
            servidor.Host = smtp
            servidor.Port = puerto
            servidor.Credentials = credenciales
            servidor.EnableSsl = True 'Esto es obligatorio para enviar correo por Hotmail , Outlook
            servidor.Send(msje)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Function LeerRepuestaCDR(ByVal ruta As String, ByVal nomfile As String, ByVal ruccliente As String) As String()
    '    Dim r As String = ""
    '    Dim file As String = ""
    '    Dim datos As String() = New String(2) {}
    '    Dim ruc As String = ruccliente
    '    Try

    '        Using zip As ZipArchive = System.IO.Compression.ZipFile.Open(ruta, ZipArchiveMode.Read)
    '            Dim zentry As ZipArchiveEntry = Nothing
    '            file = zip.Entries(1).ToString()
    '            zentry = zip.GetEntry(file)
    '            Dim xd As XmlDocument = New XmlDocument()
    '            xd.Load(zentry.Open())
    '            Dim xnl As XmlNodeList = xd.GetElementsByTagName("cbc:Description")

    '            For Each item As XmlElement In xnl
    '                r = item.InnerText
    '            Next
    '        End Using

    '    Catch ex As Exception
    '    End Try

    '    datos(0) = r
    '    datos(1) = file
    '    datos(2) = nomfile
    '    Return datos
    'End Function

    'Function ObtenerRespuestaZIPSunat(ByVal ruta As String, ByVal ruccliente As String) As String()
    '    Dim arch As System.IO.FileInfo = New System.IO.FileInfo(ruta)

    '    If arch.Extension = ".zip" Then
    '        Return LeerRepuestaCDR(ruta, System.IO.Path.GetFileName(ruta), ruccliente)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    Public Function EsCorreo(ByVal email As String) As Boolean
        If email = String.Empty Then Return False
        ' Compruebo si el formato de la dirección es correcto.
        Dim re As Regex = New Regex("^[\w._%-]+@[\w.-]+\.[a-zA-Z]{2,4}$")
        Dim m As Match = re.Match(email)
        Return (m.Captures.Count <> 0)
    End Function
End Module
