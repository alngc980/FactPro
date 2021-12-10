Imports System
Imports System.Linq
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports ZXing.QrCode
Imports System.Net.Mail
Imports System.Net
Imports System.Xml
Imports System.IO.Compression
Imports System.IO
'Imports Ionic.Zip

Namespace CapaSUNAT.Util
    Public Class Helper
        Function EnviarCorreo(ByVal para As String, ByVal asunto As String, ByVal mensaje As String, ByVal adjunto As String, ByVal emailcuenta As String, ByVal nombreCuenta As String, ByVal password As String, ByVal puerto As Integer, ByVal smtp As String) As String
            Dim msje As MailMessage = New MailMessage()

            Try
                msje.From = New MailAddress(emailcuenta, nombreCuenta)
                msje.[To].Add(para)
                msje.Subject = asunto
                msje.Body = mensaje
                msje.IsBodyHtml = False
                msje.Attachments.Add(New Attachment(adjunto))
                Dim credenciales As NetworkCredential = New NetworkCredential()
                credenciales.UserName = emailcuenta
                credenciales.Password = password
                Dim servidor As SmtpClient = New SmtpClient()
                servidor.Host = smtp
                servidor.Port = puerto
                servidor.Credentials = credenciales
                servidor.EnableSsl = True
                servidor.Send(msje)
                Return "Enviado"
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Sub GenerarQR(ByVal RUC As String, ByVal Tipocomprobante As String, ByVal Numeración As String, ByVal SumatoriaIGV As String, ByVal ImporteTotalVenta As String, ByVal FechaEmisión As String, ByVal TipoDocumentoAdquirente As String, ByVal NúmeroDocumentoAdquirente As String, ByVal CódigoHash As String, ByVal Path As String, ByVal id As Integer)
            If String.IsNullOrWhiteSpace(RUC) OrElse String.IsNullOrEmpty(Tipocomprobante) OrElse String.IsNullOrEmpty(Numeración) OrElse String.IsNullOrWhiteSpace(SumatoriaIGV) OrElse String.IsNullOrEmpty(ImporteTotalVenta) OrElse String.IsNullOrEmpty(FechaEmisión) OrElse String.IsNullOrWhiteSpace(ImporteTotalVenta) OrElse String.IsNullOrWhiteSpace(FechaEmisión) OrElse String.IsNullOrWhiteSpace(TipoDocumentoAdquirente) OrElse String.IsNullOrWhiteSpace(NúmeroDocumentoAdquirente) OrElse String.IsNullOrWhiteSpace(CódigoHash) Then
                Throw New Exception("Debe proporcionar todos los parametros para la generación del QR")
            Else
                Dim codigo As String = RUC & "|" & Tipocomprobante & "|" & Numeración & "|" & SumatoriaIGV & "|" & ImporteTotalVenta & "|" & FechaEmisión & "|" & TipoDocumentoAdquirente & "|" & NúmeroDocumentoAdquirente & "|" & CódigoHash
                Dim options As QrCodeEncodingOptions = New QrCodeEncodingOptions()
                options = New QrCodeEncodingOptions With {
                    .DisableECI = True,
                    .CharacterSet = "UTF-8",
                    .Width = 250,
                    .Height = 250
                }
                Dim qr = New ZXing.BarcodeWriter()
                qr.Options = options
                qr.Format = ZXing.BarcodeFormat.QR_CODE
                Dim result = qr.Write(codigo)
                Dim ruta As String = Path & id & ".jpg"
                result.Save(ruta)
            End If
        End Sub

        Function Right(ByVal original As String, ByVal numberCharacters As Integer) As String
            Return original.Substring(original.Length - numberCharacters)
        End Function

        Function ObtenerRespuestaSunat(ByVal ruta As String) As String
            Dim arch As System.IO.FileInfo = New System.IO.FileInfo(ruta)

            If arch.Extension = ".xml" Then
                Dim oXmlSunat As XmlDocument = New XmlDocument()
                oXmlSunat.Load(ruta)
                Dim oResult As String = ""
                Dim manager As XmlNamespaceManager = New XmlNamespaceManager(oXmlSunat.NameTable)
                manager.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
                manager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
                Dim valor As String = oXmlSunat.SelectSingleNode("//cac:DocumentResponse/cac:Response/cbc:ResponseCode", manager).InnerText

                If valor = "0" Then
                    oResult = "Aceptado por SUNAT"
                Else
                    oResult = "Rechazado por SUNAT"
                End If

                Return oResult
            Else
                Return ""
            End If
        End Function

        Function LeerRepuestaCDR(ByVal ruta As String, ByVal nomfile As String, ByVal ruccliente As String) As String()
            Dim r As String = ""
            Dim rMensaje As String = ""
            Dim file As String = ""
            Dim datos As String() = New String(3) {}
            Dim ruc As String = ruccliente
            Try

                Using zip As ZipArchive = System.IO.Compression.ZipFile.Open(ruta, ZipArchiveMode.Read)
                    Dim zentry As ZipArchiveEntry = Nothing
                    file = zip.Entries(1).ToString()
                    zentry = zip.GetEntry(file)
                    Dim xd As XmlDocument = New XmlDocument()
                    xd.Load(zentry.Open())
                    Dim xnl As XmlNodeList = xd.GetElementsByTagName("cbc:ResponseCode")

                    For Each item As XmlElement In xnl
                        r = item.InnerText
                    Next

                    Dim xnlDescrip As XmlNodeList = xd.GetElementsByTagName("cbc:Description")
                    For Each item As XmlElement In xnlDescrip
                        rMensaje = item.InnerText
                    Next
                End Using

            Catch ex As Exception
            End Try

            datos(0) = r
            datos(1) = file
            datos(2) = nomfile
            datos(3) = rMensaje
            Return datos
        End Function

        Function ObtenerRespuestaZIPSunat(ByVal ruta As String, ByVal ruccliente As String) As String()
            Dim arch As System.IO.FileInfo = New System.IO.FileInfo(ruta)

            If arch.Extension = ".zip" Then
                Return LeerRepuestaCDR(ruta, System.IO.Path.GetFileName(ruta), ruccliente)
            Else
                Return Nothing
            End If
        End Function

        Function ObtenerDigestValueSunat(ByVal archxml As String) As String
            Dim oXmlSunat As XmlDocument = New XmlDocument()
            oXmlSunat.Load(archxml)
            Dim manager As XmlNamespaceManager = New XmlNamespaceManager(oXmlSunat.NameTable)
            manager.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
            manager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")
            Dim valor As String = oXmlSunat.SelectSingleNode("//ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/ds:Signature/ds:SignedInfo/ds:Reference/ds:DigestValue", manager).InnerText
            Return valor
        End Function

        Function ObtenerSignatureValueSunat(ByVal oXmlSunat As XmlDocument) As String
            Dim manager As XmlNamespaceManager = New XmlNamespaceManager(oXmlSunat.NameTable)
            manager.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
            manager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")
            Dim valor As String = oXmlSunat.SelectSingleNode("//ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/ds:Signature/ds:SignatureValue", manager).InnerText
            Return valor
        End Function

        Function ObtenerNombreArchivoSummary(ByVal oXmlSunat As XmlDocument) As String
            Dim manager As XmlNamespaceManager = New XmlNamespaceManager(oXmlSunat.NameTable)
            manager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
            Dim valor As String = oXmlSunat.SelectSingleNode("//cbc:ID", manager).InnerText
            Return valor
        End Function

        Function NumeroDocumentoSunatVoid(ByVal RucEmpresa As String, ByVal sCorrelativo As String) As String
            Dim sValues As String() = {RucEmpresa, "RA-" & DateTime.Now.Date.ToString("yyyyMMdd"), sCorrelativo}
            Dim cNroDocumentoVoid As String = String.Format("{0}-{1}-{2}", sValues)
            Return cNroDocumentoVoid
        End Function

        Function NumeroDocumentoSunatSummary(ByVal RucEmpresa As String, ByVal sCorrelativo As String) As String
            Dim sValues As String() = {RucEmpresa, "RC-" & DateTime.Now.Date.ToString("yyyyMMdd"), sCorrelativo}
            Dim cNroDocumentoSummary As String = String.Format("{0}-{1}-{2}", sValues)
            Return cNroDocumentoSummary
        End Function

        Function NumeroDocumentoSunatReversion(ByVal RucEmpresa As String, ByVal sCorrelativo As String) As String
            Dim sValues As String() = {RucEmpresa, "RR-" & DateTime.Now.Date.ToString("yyyyMMdd"), sCorrelativo}
            Dim cNroDocumentoVoid As String = String.Format("{0}-{1}-{2}", sValues)
            Return cNroDocumentoVoid
        End Function
    End Class
End Namespace
