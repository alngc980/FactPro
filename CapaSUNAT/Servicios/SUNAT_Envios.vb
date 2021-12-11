Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports CapaSUNAT.ServiceSunat
Imports System.Xml
Imports System.IO
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Runtime.InteropServices
Imports System.Net
Imports CapaSUNAT.CapaSUNAT.Modelos
Imports CapaSUNAT.CapaSUNAT.Resumenes
'Imports CapaSUNAT.CapaSUNAT.ViewModels
Imports System.Globalization
Imports CapaSUNAT.CapaSUNAT.ViewModels

Namespace CapaSUNAT.Servicios
    <ClassInterface(ClassInterfaceType.AutoDual)>
    <ProgId("firmadoCE.firmado")>
    Public Class SUNAT_UTIL
        Public Property Ruta_XML As String
        Public Property Ruta_Certificado As String
        Public Property Password_Certificado As String
        Public Property Ruta_ENVIOS As String
        Public Property Ruta_CDRS As String

        Function GenerarComprobanteFB_XML(ByVal Comprobante As ViewModels.Cabecera, ByVal CondicionPago As DataTable) As String

            Dim Factura As CapaSUNAT.Modelos.InvoiceType = New CapaSUNAT.Modelos.InvoiceType()
            Try

                Factura.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                Factura.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
                Factura.Ccts = "urn:un:unece:uncefact:documentation:2"
                Factura.Ds = "http://www.w3.org/2000/09/xmldsig#"
                Factura.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
                Factura.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2"
                Factura.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2"
                Dim ublExtensiones As CapaSUNAT.Modelos.UBLExtensionType() = New CapaSUNAT.Modelos.UBLExtensionType(4) {}
                Dim ublExtension As CapaSUNAT.Modelos.UBLExtensionType = New CapaSUNAT.Modelos.UBLExtensionType()
                ublExtensiones(0) = ublExtension
                Factura.UBLExtensions = ublExtensiones
                Factura.UBLVersionID = New CapaSUNAT.Modelos.UBLVersionIDType()
                Factura.UBLVersionID.Value = "2.1"
                Factura.CustomizationID = New CapaSUNAT.Modelos.CustomizationIDType()
                Factura.CustomizationID.Value = "2.0"
                Factura.ID = New CapaSUNAT.Modelos.IDType()
                Factura.ID.Value = Comprobante.Serie & "-" + Comprobante.Numero
                Factura.IssueDate = New CapaSUNAT.Modelos.IssueDateType()
                Dim fechaemision As String = Convert.ToDateTime(Comprobante.Fechaemision).ToString("dd/MM/yyyy")
                Factura.IssueDate.Value = Convert.ToDateTime(fechaemision)
                Factura.IssueTime = New CapaSUNAT.Modelos.IssueTimeType()
                Factura.IssueTime.Value = DateTime.Now.ToString("HH:mm:ss")
                Factura.DueDate = New CapaSUNAT.Modelos.DueDateType()
                Factura.DueDate.Value = Convert.ToDateTime(Comprobante.Fechavencimiento)

                Dim TipoDoc As CapaSUNAT.Modelos.InvoiceTypeCodeType = New CapaSUNAT.Modelos.InvoiceTypeCodeType()
                TipoDoc.listSchemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo51"
                TipoDoc.name = "Tipo de Operacion"
                TipoDoc.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01"
                TipoDoc.listName = "Tipo de Documento"
                TipoDoc.listAgencyName = "PE:SUNAT"
                TipoDoc.Value = Comprobante.Idtipocomp.ToString()
                TipoDoc.listID = "0101"
                Factura.InvoiceTypeCode = TipoDoc

                Dim Leyenda As CapaSUNAT.Modelos.NoteType = New CapaSUNAT.Modelos.NoteType()
                Leyenda.languageLocaleID = "1000"
                Leyenda.Value = Comprobante.cLeyenda
                Dim notasLeyenda As List(Of CapaSUNAT.Modelos.NoteType) = New List(Of Modelos.NoteType)()
                notasLeyenda.Add(Leyenda)
                Factura.Note = notasLeyenda.ToArray()
                Dim moneda As CapaSUNAT.Modelos.DocumentCurrencyCodeType = New CapaSUNAT.Modelos.DocumentCurrencyCodeType() With {
                    .listID = "ISO 4217 Alpha",
                    .listName = "Currency",
                    .listAgencyName = "United Nations Economic Commission for Europe",
                    .Value = Comprobante.Idmoneda
                }
                Factura.DocumentCurrencyCode = moneda

                Dim numitems As Modelos.LineCountNumericType = New Modelos.LineCountNumericType()
                numitems.Value = Comprobante.Detalles.Count

                Dim Firma As CapaSUNAT.Modelos.SignatureType = New CapaSUNAT.Modelos.SignatureType()
                Dim Firmas As CapaSUNAT.Modelos.SignatureType() = New CapaSUNAT.Modelos.SignatureType(1) {}
                Dim partySign As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim partyIdentificacion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim partyIdentificacions As CapaSUNAT.Modelos.PartyIdentificationType() = New CapaSUNAT.Modelos.PartyIdentificationType(1) {}
                Dim idFirma As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idFirma.Value = Comprobante.EmpresaRUC
                Firma.ID = idFirma
                partyIdentificacion.ID = idFirma
                partyIdentificacions(0) = partyIdentificacion
                partySign.PartyIdentification = partyIdentificacions
                Firma.SignatoryParty = partySign

                Dim Nota As CapaSUNAT.Modelos.NoteType = New CapaSUNAT.Modelos.NoteType()
                Dim Notas As CapaSUNAT.Modelos.NoteType() = New CapaSUNAT.Modelos.NoteType(1) {}
                Nota.Value = "Elaborado por Sistema de Emision Electronica HardSoft"
                Notas(0) = Nota
                Firma.Note = Notas

                Dim partyName As CapaSUNAT.Modelos.PartyNameType = New CapaSUNAT.Modelos.PartyNameType()
                Dim partyNames As CapaSUNAT.Modelos.PartyNameType() = New CapaSUNAT.Modelos.PartyNameType(1) {}
                Dim RazonSocialFirma As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                RazonSocialFirma.Value = Comprobante.EmpresaRazonSocial
                partyName.Name = RazonSocialFirma
                partyNames(0) = partyName
                partySign.PartyName = partyNames

                Dim attachType As CapaSUNAT.Modelos.AttachmentType = New CapaSUNAT.Modelos.AttachmentType()
                Dim externaReferencia As CapaSUNAT.Modelos.ExternalReferenceType = New CapaSUNAT.Modelos.ExternalReferenceType()
                Dim uri As CapaSUNAT.Modelos.URIType = New CapaSUNAT.Modelos.URIType()
                uri.Value = Comprobante.EmpresaRUC
                externaReferencia.URI = uri
                Firma.DigitalSignatureAttachment = attachType
                attachType.ExternalReference = externaReferencia
                Firma.DigitalSignatureAttachment = attachType
                Firmas(0) = Firma
                Factura.Signature = Firmas
                'Firmas

                Dim empresa As CapaSUNAT.Modelos.SupplierPartyType = New CapaSUNAT.Modelos.SupplierPartyType()
                Dim _party As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim _partyidentificacion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim _partyidentificacions As CapaSUNAT.Modelos.PartyIdentificationType() = New CapaSUNAT.Modelos.PartyIdentificationType(5) {}

                Dim _idEmpresa As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                _idEmpresa.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                _idEmpresa.schemeName = "Documento de Identidad"
                _idEmpresa.schemeID = "6"
                _idEmpresa.schemeAgencyName = "PE:SUNAT"
                _idEmpresa.Value = Comprobante.EmpresaRUC

                _partyidentificacion.ID = _idEmpresa
                _partyidentificacions(0) = _partyidentificacion
                '_party.PartyIdentification = _partyidentificacions


                Dim _partyname As CapaSUNAT.Modelos.PartyNameType = New CapaSUNAT.Modelos.PartyNameType()
                Dim _partynames As List(Of CapaSUNAT.Modelos.PartyNameType) = New List(Of CapaSUNAT.Modelos.PartyNameType)()

                Dim nameEmisor As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                nameEmisor.Value = Comprobante.EmpresaRazonSocial
                partyName.Name = nameEmisor

                _partynames.Add(partyName)
                _party.PartyName = _partynames.ToArray()

                Dim PartyTaxScheme As CapaSUNAT.Modelos.PartyTaxSchemeType = New CapaSUNAT.Modelos.PartyTaxSchemeType()
                Dim PartyTaxSchemes As List(Of CapaSUNAT.Modelos.PartyTaxSchemeType) = New List(Of CapaSUNAT.Modelos.PartyTaxSchemeType)()
                Dim registerNameEmisor As CapaSUNAT.Modelos.RegistrationNameType = New CapaSUNAT.Modelos.RegistrationNameType()
                registerNameEmisor.Value = Comprobante.EmpresaRazonSocial
                PartyTaxScheme.RegistrationName = registerNameEmisor

                Dim compañia As Modelos.CompanyIDType = New Modelos.CompanyIDType()
                compañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                compañia.schemeAgencyName = "PE:SUNAT"
                compañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                compañia.schemeID = "6"
                compañia.Value = Comprobante.EmpresaRUC

                Dim direccion As CapaSUNAT.Modelos.AddressType = New CapaSUNAT.Modelos.AddressType()
                Dim addrestypecode As CapaSUNAT.Modelos.AddressTypeCodeType = New CapaSUNAT.Modelos.AddressTypeCodeType()
                addrestypecode.listName = "Establecimientos anexos"
                addrestypecode.listAgencyName = "PE:SUNAT"
                addrestypecode.Value = "0000"
                direccion.AddressTypeCode = addrestypecode
                PartyTaxScheme.RegistrationAddress = direccion

                Dim taxSchema As Modelos.TaxSchemeType = New Modelos.TaxSchemeType()
                Dim idsupplier As Modelos.IDType = New Modelos.IDType()
                idsupplier.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idsupplier.schemeAgencyName = "PE:SUNAT"
                idsupplier.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idsupplier.schemeID = "6"
                idsupplier.Value = Comprobante.EmpresaRUC
                taxSchema.ID = idsupplier

                PartyTaxScheme.CompanyID = compañia
                PartyTaxScheme.TaxScheme = taxSchema
                PartyTaxSchemes.Add(PartyTaxScheme)
                Dim partelegals As List(Of Modelos.PartyLegalEntityType) = New List(Of Modelos.PartyLegalEntityType)()
                Dim partelegal As Modelos.PartyLegalEntityType = New Modelos.PartyLegalEntityType()
                Dim registerNamePL As CapaSUNAT.Modelos.RegistrationNameType = New CapaSUNAT.Modelos.RegistrationNameType()
                registerNamePL.Value = Comprobante.EmpresaRazonSocial
                partelegal.RegistrationName = registerNamePL

                Dim direccionPL As Modelos.AddressType = New Modelos.AddressType()          'ANGC DIRECCION DE QUIEN?
                Dim iddireccionPL As Modelos.IDType = New Modelos.IDType()
                iddireccionPL.schemeAgencyName = "PE:INEI"
                iddireccionPL.schemeName = "Ubigeos"
                iddireccionPL.Value = Comprobante.ID_EmpresaDepartamento + Comprobante.ID_EmpresaProvincia + Comprobante.ID_EmpresaDistrito
                direccionPL.ID = iddireccionPL

                Dim address_TypeCodeType As Modelos.AddressTypeCodeType = New Modelos.AddressTypeCodeType()
                address_TypeCodeType.listName = "Establecimientos anexos"
                address_TypeCodeType.listAgencyName = "PE:SUNAT"
                address_TypeCodeType.Value = "0000"
                direccionPL.AddressTypeCode = address_TypeCodeType

                Dim Departamento As CapaSUNAT.Modelos.CityNameType = New CapaSUNAT.Modelos.CityNameType()       'ANGC ME PARECE DIRECCION DEL EMISOR
                Departamento.Value = Comprobante.EmpresaDepartamento
                direccionPL.CityName = Departamento
                Dim Provincia As CapaSUNAT.Modelos.CountrySubentityType = New CapaSUNAT.Modelos.CountrySubentityType()
                Provincia.Value = Comprobante.EmpresaProvincia
                direccionPL.CountrySubentity = Provincia
                Dim distrito As CapaSUNAT.Modelos.DistrictType = New CapaSUNAT.Modelos.DistrictType()
                distrito.Value = Comprobante.EmpresaDistrito
                direccionPL.District = distrito
                Dim direcciones As List(Of Modelos.AddressLineType) = New List(Of Modelos.AddressLineType)()
                Dim direccionEmisor As Modelos.AddressLineType = New Modelos.AddressLineType()
                Dim local1 As Modelos.LineType = New Modelos.LineType()
                local1.Value = Comprobante.EmpresaDireccion
                direccionPL.AddressLine = direcciones.ToArray()
                direccionEmisor.Line = local1
                direcciones.Add(direccionEmisor)
                direccionPL.AddressLine = direcciones.ToArray()

                Dim pais As CapaSUNAT.Modelos.CountryType = New CapaSUNAT.Modelos.CountryType()
                Dim codigoPais As CapaSUNAT.Modelos.IdentificationCodeType = New CapaSUNAT.Modelos.IdentificationCodeType()
                codigoPais.listName = "Country"
                codigoPais.listAgencyName = "United Nations Economic Commission for Europe"
                codigoPais.listID = "ISO 3166-1"
                codigoPais.Value = "PE"
                pais.IdentificationCode = codigoPais

                direccionPL.Country = pais
                partelegal.RegistrationAddress = direccionPL
                partelegals.Add(partelegal)

                _party.PartyLegalEntity = partelegals.ToArray()
                _party.PartyName = _partynames.ToArray()
                _party.PartyIdentification = _partyidentificacions
                empresa.Party = _party
                Factura.AccountingSupplierParty = empresa


                Dim taxschemeCliente As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                Dim CustomerPartyCliente As CapaSUNAT.Modelos.CustomerPartyType = New CapaSUNAT.Modelos.CustomerPartyType()
                Dim partyCliente As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim partyIdentificion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim partyIdentificions As List(Of CapaSUNAT.Modelos.PartyIdentificationType) = New List(Of CapaSUNAT.Modelos.PartyIdentificationType)()
                Dim idtipo As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()

                idtipo.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"      'ANGC CREO QUE APARTIR DE AQUI ES CLIENTE
                idtipo.schemeName = "Documento de Identidad"
                idtipo.schemeAgencyName = "PE:SUNAT"
                idtipo.schemeID = Comprobante.ClienteTipodocumento
                idtipo.Value = Comprobante.ClienteNumeroDocumento

                partyIdentificion.ID = idtipo
                partyIdentificions.Add(partyIdentificion)
                partyCliente.PartyIdentification = partyIdentificions.ToArray()

                Dim RazSocClientes As List(Of CapaSUNAT.Modelos.PartyNameType) = New List(Of CapaSUNAT.Modelos.PartyNameType)()
                Dim RazSocCliente As CapaSUNAT.Modelos.PartyNameType = New CapaSUNAT.Modelos.PartyNameType()
                Dim razSocial As Modelos.NameType1 = New Modelos.NameType1()

                razSocial.Value = Comprobante.ClienteRazonSocial
                RazSocCliente.Name = razSocial
                RazSocClientes.Add(RazSocCliente)

                Dim partySchemas As List(Of CapaSUNAT.Modelos.PartyTaxSchemeType) = New List(Of Modelos.PartyTaxSchemeType)()
                Dim partySchema As CapaSUNAT.Modelos.PartyTaxSchemeType = New CapaSUNAT.Modelos.PartyTaxSchemeType()
                Dim RegistroNombre As Modelos.RegistrationNameType = New Modelos.RegistrationNameType()
                RegistroNombre.Value = Comprobante.ClienteRazonSocial
                partySchema.RegistrationName = RegistroNombre

                Dim idcompañia As Modelos.CompanyIDType = New Modelos.CompanyIDType()
                idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idcompañia.schemeAgencyName = "PE:SUNAT"
                idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idcompañia.schemeID = Comprobante.ClienteTipodocumento
                idcompañia.Value = Comprobante.ClienteNumeroDocumento
                Dim schemeType As Modelos.TaxSchemeType = New Modelos.TaxSchemeType()


                Dim idc As Modelos.IDType = New Modelos.IDType()
                idc.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idc.schemeAgencyName = "PE:SUNAT"
                idc.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idc.schemeID = Comprobante.ClienteTipodocumento
                idc.Value = Comprobante.ClienteNumeroDocumento
                schemeType.ID = idc
                idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idcompañia.schemeAgencyName = "PE:SUNAT"
                idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idcompañia.schemeID = Comprobante.ClienteTipodocumento
                idcompañia.Value = Comprobante.ClienteNumeroDocumento

                Dim partyLegals As List(Of Modelos.PartyLegalEntityType) = New List(Of Modelos.PartyLegalEntityType)()
                Dim partyLegal As Modelos.PartyLegalEntityType = New Modelos.PartyLegalEntityType()
                Dim Registro_Nombre As Modelos.RegistrationNameType = New Modelos.RegistrationNameType()
                Registro_Nombre.Value = Comprobante.ClienteRazonSocial
                partyLegal.RegistrationName = Registro_Nombre

                Dim direccionCliente As Modelos.AddressType = New Modelos.AddressType()
                Dim dirs As List(Of Modelos.AddressLineType) = New List(Of Modelos.AddressLineType)()
                Dim dir As Modelos.AddressLineType = New Modelos.AddressLineType()
                Dim lineas As List(Of Modelos.LineType) = New List(Of Modelos.LineType)()
                Dim linea As Modelos.LineType = New Modelos.LineType()
                linea.Value = Comprobante.ClienteDireccion
                dir.Line = linea
                dirs.Add(dir)
                direccionCliente.AddressLine = dirs.ToArray()

                Dim paisC As CapaSUNAT.Modelos.CountryType = New CapaSUNAT.Modelos.CountryType()
                Dim codigoPaisC As CapaSUNAT.Modelos.IdentificationCodeType = New CapaSUNAT.Modelos.IdentificationCodeType()
                codigoPaisC.Value = "PE"
                paisC.IdentificationCode = codigoPaisC
                partyLegals.Add(partyLegal)
                partySchema.CompanyID = idcompañia
                partySchema.TaxScheme = schemeType
                partySchemas.Add(partySchema)
                partyCliente.PartyLegalEntity = partyLegals.ToArray()
                CustomerPartyCliente.Party = partyCliente

                Dim accoutingCustomerParty As Modelos.CustomerPartyType = New Modelos.CustomerPartyType()
                accoutingCustomerParty.Party = partyCliente
                Factura.AccountingCustomerParty = accoutingCustomerParty

                ''FORMA DE PAGO
                Dim pagosCuotas As New List(Of Modelos.PaymentTermsType)

                If Comprobante.FormaPago.ToLower = "credito" Then       'CHECK IT THIS


                    Dim formapago As New Modelos.PaymentTermsType
                    Dim idformapago As New Modelos.IDType
                    idformapago.Value = "FormaPago"
                    formapago.ID = idformapago

                    Dim tipoformapagos As New List(Of Modelos.PaymentMeansIDType)
                    Dim tipoformapago As New Modelos.PaymentMeansIDType
                    tipoformapago.Value = "Credito"
                    tipoformapagos.Add(tipoformapago)
                    formapago.PaymentMeansID = tipoformapagos.ToArray

                    Dim formapagotipoMoneda As New Modelos.AmountType2
                    formapagotipoMoneda.currencyID = Comprobante.Idmoneda
                    formapagotipoMoneda.Value = Comprobante.TotNeto
                    formapago.Amount = formapagotipoMoneda

                    pagosCuotas.Add(formapago)


                    For i As Integer = 1 To CondicionPago.Rows.Count 'Comprobante.NumeroCuotas
                        Dim pagoCuota As New Modelos.PaymentTermsType
                        Dim idpagoCuota As New Modelos.IDType

                        Dim tipoMoneda As New Modelos.AmountType2
                        tipoMoneda.currencyID = Comprobante.Idmoneda
                        tipoMoneda.Value = CondicionPago.Rows(i - 1)(1).ToString ' Comprobante.MontoCuota
                        pagoCuota.Amount = tipoMoneda

                        Dim fechapagocuota As New Modelos.PaymentDueDateType
                        Dim fechaPago As String = Convert.ToDateTime(CondicionPago.Rows(i - 1)(2).ToString).ToString("dd/MM/yyyy")
                        fechapagocuota.Value = Convert.ToDateTime(fechaPago)
                        pagoCuota.PaymentDueDate = fechapagocuota

                        idpagoCuota.Value = "Cuota" & i.ToString().PadLeft(3, "0")
                        pagoCuota.ID = idpagoCuota
                        pagosCuotas.Add(pagoCuota)
                    Next
                    Factura.PaymentTerms = pagosCuotas.ToArray

                Else
                    Dim pagos As New List(Of Modelos.PaymentTermsType)
                    Dim pago As New Modelos.PaymentTermsType
                    Dim idpago As New Modelos.IDType
                    idpago.Value = "FormaPago"
                    pago.ID = idpago

                    Dim tipopagos As New List(Of Modelos.PaymentMeansIDType)
                    Dim tipopago As New Modelos.PaymentMeansIDType
                    tipopago.Value = Comprobante.FormaPago
                    tipopagos.Add(tipopago)
                    pago.PaymentMeansID = tipopagos.ToArray
                    pagos.Add(pago)
                    Factura.PaymentTerms = pagos.ToArray
                End If

                '**********    CHECKEAMOS ESTA PARTE PARA COMPRAR CON FACTURAS EXONERADAS     **********
                Dim TotalImptos As CapaSUNAT.Modelos.TaxTotalType = New CapaSUNAT.Modelos.TaxTotalType()
                Dim taxAmountImpto As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                taxAmountImpto.currencyID = Comprobante.Idmoneda
                taxAmountImpto.Value = Convert.ToDecimal(Comprobante.TotIgv)
                TotalImptos.TaxAmount = taxAmountImpto

                Dim subtotales As List(Of CapaSUNAT.Modelos.TaxSubtotalType) = New List(Of CapaSUNAT.Modelos.TaxSubtotalType)()
                Dim subtotal As CapaSUNAT.Modelos.TaxSubtotalType = New CapaSUNAT.Modelos.TaxSubtotalType()
                Dim taxsubtotal As CapaSUNAT.Modelos.TaxableAmountType = New CapaSUNAT.Modelos.TaxableAmountType()
                taxsubtotal.currencyID = Comprobante.Idmoneda
                taxsubtotal.Value = Convert.ToDecimal(Comprobante.TotSubtotal)
                subtotal.TaxableAmount = taxsubtotal

                Dim TotalTaxAmountTotal As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                TotalTaxAmountTotal.currencyID = Comprobante.Idmoneda
                TotalTaxAmountTotal.Value = Convert.ToDecimal(Comprobante.TotIgv)
                subtotal.TaxAmount = TotalTaxAmountTotal

                Dim subTotalIGV As Modelos.TaxSubtotalType = New Modelos.TaxSubtotalType()
                subTotalIGV.TaxableAmount = taxsubtotal
                subtotales.Add(subtotal)
                TotalImptos.TaxSubtotal = subtotales.ToArray()

                Dim taxcategoryTotal As CapaSUNAT.Modelos.TaxCategoryType = New CapaSUNAT.Modelos.TaxCategoryType()
                Dim taxScheme As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                Dim idTotal As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idTotal.schemeID = "UN/ECE 5305"
                idTotal.schemeName = "Tax Category Identifier"
                idTotal.schemeAgencyName = "United Nations Economic Commission for Europe"
                idTotal.Value = "S"

                Dim nametypeImpto As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                nametypeImpto.Value = "EXO" '"IGV"

                Dim taxtypecodeImpto As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                taxtypecodeImpto.Value = "VAT"

                Dim idTot As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idTot.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05"
                idTot.schemeAgencyName = "PE:SUNAT"
                idTot.schemeName = "Codigo de tributos"
                idTot.Value = "9997" '"1000"
                taxScheme.ID = idTot

                Dim nametypeImptoIGV As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                nametypeImptoIGV.Value = "IGV"

                Dim taxtypecodeImpuesto As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                taxtypecodeImpuesto.Value = "VAT"
                taxScheme.Name = nametypeImpto
                taxScheme.TaxTypeCode = taxtypecodeImpto
                taxcategoryTotal.TaxScheme = taxScheme
                subtotal.TaxCategory = taxcategoryTotal

                Dim TaxSubtotals As List(Of CapaSUNAT.Modelos.TaxSubtotalType) = New List(Of CapaSUNAT.Modelos.TaxSubtotalType)()
                TaxSubtotals.Add(subtotal)

                For Each det In Comprobante.Detalles

                    If det.Codcom.Contains("BBBB") Then
                        Dim TotalICBPER As Modelos.TaxSubtotalType = New Modelos.TaxSubtotalType()
                        Dim taxICBPER As Modelos.TaxAmountType = New Modelos.TaxAmountType()
                        taxICBPER.currencyID = "PEN"
                        taxICBPER.Value = Math.Round((det.Cantidad * 0.2D), 2)
                        TotalICBPER.TaxAmount = taxICBPER

                        Dim taxCategoria As Modelos.TaxCategoryType = New Modelos.TaxCategoryType()
                        Dim taxSchemaicb As Modelos.TaxSchemeType = New Modelos.TaxSchemeType()
                        Dim idTaschema As Modelos.IDType = New Modelos.IDType()
                        idTaschema.Value = "7152"

                        Dim nombreICB As Modelos.NameType1 = New Modelos.NameType1()
                        nombreICB.Value = "ICBPER"

                        Dim taxtypecodeICPER As Modelos.TaxTypeCodeType = New Modelos.TaxTypeCodeType()
                        taxtypecodeICPER.Value = "OTH"
                        taxSchemaicb.ID = idTaschema
                        taxSchemaicb.Name = nombreICB
                        taxSchemaicb.TaxTypeCode = taxtypecodeICPER
                        taxCategoria.TaxScheme = taxSchemaicb
                        TotalICBPER.TaxCategory = taxCategoria
                        TaxSubtotals.Add(TotalICBPER)
                        Exit For
                    End If
                Next

                TotalImptos.TaxSubtotal = TaxSubtotals.ToArray()
                Dim taxTotals As List(Of CapaSUNAT.Modelos.TaxTotalType) = New List(Of CapaSUNAT.Modelos.TaxTotalType)()
                taxTotals.Add(TotalImptos)
                Factura.TaxTotal = taxTotals.ToArray()

                Dim TotalValorVenta As CapaSUNAT.Modelos.MonetaryTotalType = New CapaSUNAT.Modelos.MonetaryTotalType()
                Dim TValorVenta As CapaSUNAT.Modelos.LineExtensionAmountType = New CapaSUNAT.Modelos.LineExtensionAmountType()
                TValorVenta.currencyID = Comprobante.Idmoneda
                TValorVenta.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotSubtotal))
                TotalValorVenta.LineExtensionAmount = TValorVenta

                Dim TotalPrecioVenta As CapaSUNAT.Modelos.TaxInclusiveAmountType = New CapaSUNAT.Modelos.TaxInclusiveAmountType()
                TotalPrecioVenta.currencyID = Comprobante.Idmoneda
                TotalPrecioVenta.Value = Convert.ToDecimal(Comprobante.TotNeto)

                Dim MtoTotalDsctos As CapaSUNAT.Modelos.AllowanceTotalAmountType = New CapaSUNAT.Modelos.AllowanceTotalAmountType()
                MtoTotalDsctos.currencyID = Comprobante.Idmoneda
                MtoTotalDsctos.Value = Convert.ToDecimal(Comprobante.TotDsctos)
                TotalValorVenta.AllowanceTotalAmount = MtoTotalDsctos

                Dim MtoTotalOtrosCargos As CapaSUNAT.Modelos.ChargeTotalAmountType = New CapaSUNAT.Modelos.ChargeTotalAmountType()
                MtoTotalOtrosCargos.currencyID = Comprobante.Idmoneda
                MtoTotalOtrosCargos.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotOtros))
                TotalValorVenta.ChargeTotalAmount = MtoTotalOtrosCargos

                Dim MtoCargos As CapaSUNAT.Modelos.PrepaidAmountType = New CapaSUNAT.Modelos.PrepaidAmountType()
                MtoCargos.currencyID = Comprobante.Idmoneda
                MtoCargos.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotOtros))
                MtoCargos.Value = Convert.ToDecimal(String.Format("{0:0.00}", 0))
                TotalValorVenta.PrepaidAmount = MtoCargos

                Dim ImporteTotalVenta As CapaSUNAT.Modelos.PayableAmountType = New CapaSUNAT.Modelos.PayableAmountType()
                ImporteTotalVenta.currencyID = Comprobante.Idmoneda
                ImporteTotalVenta.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotNeto))
                TotalValorVenta.LineExtensionAmount = TValorVenta
                TotalValorVenta.TaxInclusiveAmount = TotalPrecioVenta
                TotalValorVenta.AllowanceTotalAmount = MtoTotalDsctos
                TotalValorVenta.ChargeTotalAmount = MtoTotalOtrosCargos
                TotalValorVenta.PrepaidAmount = MtoCargos
                TotalValorVenta.PayableAmount = ImporteTotalVenta
                Factura.LegalMonetaryTotal = TotalValorVenta
                Dim items As List(Of CapaSUNAT.Modelos.InvoiceLineType) = New List(Of CapaSUNAT.Modelos.InvoiceLineType)()
                Dim iditem As Integer = 1

                For Each det In Comprobante.Detalles                'DETALLES DE VENTA

                    Dim item As CapaSUNAT.Modelos.InvoiceLineType = New CapaSUNAT.Modelos.InvoiceLineType()
                    Dim numeroItem As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    numeroItem.Value = iditem.ToString()
                    item.ID = numeroItem

                    Dim cantidad As CapaSUNAT.Modelos.InvoicedQuantityType = New CapaSUNAT.Modelos.InvoicedQuantityType()
                    cantidad.unitCodeListAgencyName = "United Nations Economic Commission for Europe"
                    cantidad.unitCodeListID = "UN/ECE rec 20"
                    cantidad.unitCode = det.UnidadMedida
                    item.InvoicedQuantity = cantidad
                    cantidad.Value = det.Cantidad
                    item.InvoicedQuantity = cantidad

                    Dim ValorVenta As CapaSUNAT.Modelos.LineExtensionAmountType = New CapaSUNAT.Modelos.LineExtensionAmountType()
                    ValorVenta.currencyID = Comprobante.Idmoneda
                    ValorVenta.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.Total)) 'Convert.ToDecimal(String.Format("{0:0.00}", det.Total / 1.18D))
                    item.LineExtensionAmount = ValorVenta

                    Dim ValorReferenUnitario As CapaSUNAT.Modelos.PricingReferenceType = New CapaSUNAT.Modelos.PricingReferenceType()
                    Dim TipoPrecios As List(Of CapaSUNAT.Modelos.PriceType) = New List(Of CapaSUNAT.Modelos.PriceType)()
                    Dim TipoPrecio As CapaSUNAT.Modelos.PriceType = New CapaSUNAT.Modelos.PriceType()
                    Dim PrecioMonto As CapaSUNAT.Modelos.PriceAmountType = New CapaSUNAT.Modelos.PriceAmountType()
                    PrecioMonto.currencyID = Comprobante.Idmoneda
                    PrecioMonto.Value = Convert.ToDecimal(String.Format("{0:0.000}", det.Precio))
                    TipoPrecio.PriceAmount = PrecioMonto

                    Dim TipoPrecioCode As CapaSUNAT.Modelos.PriceTypeCodeType = New CapaSUNAT.Modelos.PriceTypeCodeType()
                    TipoPrecioCode.listName = "Tipo de Precio"
                    TipoPrecioCode.listAgencyName = "PE:SUNAT"
                    TipoPrecioCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16"
                    TipoPrecioCode.Value = "01"
                    TipoPrecio.PriceTypeCode = TipoPrecioCode
                    TipoPrecios.Add(TipoPrecio)

                    ValorReferenUnitario.AlternativeConditionPrice = TipoPrecios.ToArray()      '' ESTO ES VALOR IGV
                    item.PricingReference = ValorReferenUnitario
                    Dim Totales_Items As List(Of CapaSUNAT.Modelos.TaxTotalType) = New List(Of CapaSUNAT.Modelos.TaxTotalType)()
                    Dim Totales_Item As CapaSUNAT.Modelos.TaxTotalType = New CapaSUNAT.Modelos.TaxTotalType()
                    Dim Total_Item As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                    Total_Item.currencyID = Comprobante.Idmoneda
                    Total_Item.Value = 0 'Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18D)))
                    Totales_Item.TaxAmount = Total_Item                                         '' ESTO ES VALOR IGV

                    Dim subtotal_Items As List(Of CapaSUNAT.Modelos.TaxSubtotalType) = New List(Of CapaSUNAT.Modelos.TaxSubtotalType)()
                    Dim subtotal_Item As CapaSUNAT.Modelos.TaxSubtotalType = New CapaSUNAT.Modelos.TaxSubtotalType()
                    Dim taxsubtotal_IGVItem As CapaSUNAT.Modelos.TaxableAmountType = New CapaSUNAT.Modelos.TaxableAmountType()
                    taxsubtotal_IGVItem.currencyID = Comprobante.Idmoneda
                    taxsubtotal_IGVItem.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem)) 'Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem / 1.18D))
                    subtotal_Item.TaxableAmount = taxsubtotal_IGVItem                           '' VALOR DE VENTA

                    Dim TotalTaxAmount_IGVItem As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                    TotalTaxAmount_IGVItem.currencyID = Comprobante.Idmoneda
                    TotalTaxAmount_IGVItem.Value = Convert.ToDecimal(String.Format("{0:0.00}", 0))  'Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18D)))
                    subtotal_Item.TaxAmount = TotalTaxAmount_IGVItem
                    subtotal_Items.Add(subtotal_Item)
                    Totales_Item.TaxSubtotal = subtotal_Items.ToArray()

                    Dim taxcategory_IGVItem As CapaSUNAT.Modelos.TaxCategoryType = New CapaSUNAT.Modelos.TaxCategoryType()
                    Dim idTaxCategoria As Modelos.IDType = New Modelos.IDType()
                    idTaxCategoria.schemeAgencyName = "United Nations Economic Commission for Europe"
                    idTaxCategoria.schemeName = "Tax Category Identifier"
                    idTaxCategoria.schemeID = "UN/ECE 5305"
                    idTaxCategoria.Value = "S"

                    Dim porcentaje As Modelos.PercentType1 = New Modelos.PercentType1()
                    porcentaje.Value = Convert.ToDecimal("0.00") * 100  'Convert.ToDecimal(det.porIgvItem) * 100
                    taxcategory_IGVItem.Percent = porcentaje
                    subtotal_Item.TaxCategory = taxcategory_IGVItem

                    Dim ReasonCode As Modelos.TaxExemptionReasonCodeType = New Modelos.TaxExemptionReasonCodeType()
                    ReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07"
                    ReasonCode.listName = "Afectacion del IGV"
                    ReasonCode.listAgencyName = "PE:SUNAT"
                    ReasonCode.Value = "20" '"10"
                    taxcategory_IGVItem.TaxExemptionReasonCode = ReasonCode

                    Dim taxscheme_IGVItem As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                    Dim id2_IGVItem As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    id2_IGVItem.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05"
                    id2_IGVItem.schemeAgencyName = "PE:SUNAT"
                    id2_IGVItem.schemeName = "Codigo de tributos"
                    id2_IGVItem.Value = "9997" '"1000"
                    taxscheme_IGVItem.ID = id2_IGVItem

                    Dim nombreImpto_IGVItem As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                    nombreImpto_IGVItem.Value = "EXO"   '"IGV"
                    taxscheme_IGVItem.Name = nombreImpto_IGVItem
                    Dim nombreImpto_IGVItemInter As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                    nombreImpto_IGVItemInter.Value = "VAT"
                    taxscheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter

                    taxscheme_IGVItem.Name = nombreImpto_IGVItem
                    taxcategory_IGVItem.TaxScheme = taxscheme_IGVItem

                    If det.Codcom.Contains("BBBB") Then                 '' ME PARECERE QUE ES ICBPER
                        Dim TotalIcb As Modelos.TaxSubtotalType = New Modelos.TaxSubtotalType()
                        Dim taxAmounticb As Modelos.TaxAmountType = New Modelos.TaxAmountType()
                        taxAmounticb.currencyID = Comprobante.Idmoneda
                        taxAmounticb.Value = Math.Round((det.Cantidad * 0.2D), 2)
                        Dim baseicb As Modelos.BaseUnitMeasureType = New Modelos.BaseUnitMeasureType()
                        baseicb.unitCode = det.UnidadMedida
                        baseicb.Value = Convert.ToInt32(det.Cantidad)
                        Dim perunicb As Modelos.PerUnitAmountType = New Modelos.PerUnitAmountType()
                        perunicb.currencyID = Comprobante.Idmoneda
                        perunicb.Value = det.Precio
                        TotalIcb.TaxAmount = taxAmounticb
                        TotalIcb.BaseUnitMeasure = baseicb
                        Dim categoryicb As Modelos.TaxCategoryType = New Modelos.TaxCategoryType()
                        Dim taxicb As Modelos.TaxSchemeType = New Modelos.TaxSchemeType()
                        Dim idtaxcat As Modelos.IDType = New Modelos.IDType()
                        idtaxcat.schemeID = "UN/ECE 5305"
                        idtaxcat.schemeName = "Codigo de tributos"
                        idtaxcat.schemeAgencyName = "PE:SUNAT"
                        idtaxcat.Value = "S"
                        categoryicb.ID = idtaxcat
                        categoryicb.PerUnitAmount = perunicb
                        Dim idicp As Modelos.IDType = New Modelos.IDType()
                        idicp.Value = "7152"
                        Dim nombreicb As Modelos.NameType1 = New Modelos.NameType1()
                        nombreicb.Value = "ICBPER"
                        Dim codicb As Modelos.TaxTypeCodeType = New Modelos.TaxTypeCodeType()
                        codicb.Value = "OTH"
                        taxicb.ID = idicp
                        taxicb.Name = nombreicb
                        taxicb.TaxTypeCode = codicb
                        categoryicb.TaxScheme = taxicb
                        TotalIcb.TaxCategory = categoryicb
                        subtotal_Items.Add(TotalIcb)
                    End If

                    Totales_Item.TaxSubtotal = subtotal_Items.ToArray()
                    Totales_Items.Add(Totales_Item)
                    item.TaxTotal = Totales_Items.ToArray()

                    Dim descriptions As List(Of CapaSUNAT.Modelos.DescriptionType) = New List(Of CapaSUNAT.Modelos.DescriptionType)()
                    Dim description As CapaSUNAT.Modelos.DescriptionType = New CapaSUNAT.Modelos.DescriptionType()
                    description.Value = det.DescripcionProducto

                    Dim codigoProd As CapaSUNAT.Modelos.ItemIdentificationType = New CapaSUNAT.Modelos.ItemIdentificationType()
                    Dim id As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    id.Value = det.Codcom
                    codigoProd.ID = id

                    Dim PrecioProducto As CapaSUNAT.Modelos.PriceType = New CapaSUNAT.Modelos.PriceType()
                    Dim PrecioMontoTipo As CapaSUNAT.Modelos.PriceAmountType = New CapaSUNAT.Modelos.PriceAmountType()
                    PrecioMontoTipo.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.Precio))    'Convert.ToDecimal(String.Format("{0:0.00}", det.Precio / (det.porIgvItem + 1))) 'VALOR DE VENTA
                    PrecioMontoTipo.currencyID = Comprobante.Idmoneda
                    PrecioProducto.PriceAmount = PrecioMontoTipo

                    Dim itemTipo As CapaSUNAT.Modelos.ItemType = New CapaSUNAT.Modelos.ItemType()
                    descriptions.Add(description)
                    itemTipo.Description = descriptions.ToArray()
                    itemTipo.SellersItemIdentification = codigoProd

                    Dim codSunats As List(Of Modelos.CommodityClassificationType) = New List(Of Modelos.CommodityClassificationType)()
                    Dim codSunat As Modelos.CommodityClassificationType = New Modelos.CommodityClassificationType()
                    Dim codClas As Modelos.ItemClassificationCodeType = New Modelos.ItemClassificationCodeType()

                    codClas.listName = "Item Classification"
                    codClas.listAgencyName = "GS1 US"
                    codClas.listID = "UNSPSC"
                    codClas.Value = "25172405"
                    codSunat.ItemClassificationCode = codClas
                    codSunats.Add(codSunat)
                    itemTipo.CommodityClassification = codSunats.ToArray()

                    item.Item = itemTipo
                    item.Price = PrecioProducto
                    items.Add(item)
                    iditem += 1
                Next

                Factura.InvoiceLine = items.ToArray()
                Dim archXML As String = GenerarComprobante(Factura, Comprobante.EmpresaRUC, Comprobante.Idtipocomp, Comprobante.Serie, Comprobante.Numero, "RUCEmpresa")    'AQUI SE GENERA Y GUARDA EL XML SIN FIRMAR
                FirmarXML(archXML, Ruta_Certificado, Password_Certificado)          ''AQUI SE FIRMA EL XML GENERADO
                Dim strEnvio As String = Ruta_ENVIOS & Path.GetFileName(archXML).Replace(".xml", ".zip")
                Comprimir(archXML, strEnvio)    ' COMPRIMIR XML FIMRADO
                EnviarDocumento(strEnvio)       ' ENVIAR DOCUMENTO  y GUARDAR CDR
                Dim xmlfirmado As String = Comprobante.EmpresaRUC & "-" + Comprobante.Idtipocomp.PadLeft(2, "0"c) & "-" + Comprobante.Serie & "-" + Comprobante.Numero & ".xml"
                Dim rutafirmado As String = Ruta_XML & xmlfirmado

                Dim help As New Util.Helper
                Dim _firma As String = help.ObtenerDigestValueSunat(rutafirmado)
                Return "0"
            Catch ex As Exception
                'Throw New Exception(ex.Message)
                Return ex.Message
            End Try
        End Function

        Function GenerarFBGuiaRemision_XML(ByVal guia As CapaSUNAT.ViewModels.GuiaRemision, ByVal Comprobante As Cabecera) As Integer
            GenerarGuiaRemision_XML(guia, Comprobante.Detalles, Comprobante.EmpresaRUC)
            Return 1
        End Function

        Function GenerarGuiaRemision_XML(ByVal Comprobante As CapaSUNAT.ViewModels.GuiaRemision, ByVal Detalles As ICollection(Of Detalles),
                                         ByVal EmpresaRUC As String) As Integer

            Dim Guia As Guias.DespatchAdviceType = New Guias.DespatchAdviceType()
            Try

                Guia.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                Guia.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
                Guia.Ccts = "urn:un:unece:uncefact:documentation:2"
                Guia.Ds = "http://www.w3.org/2000/09/xmldsig#"
                Guia.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
                Guia.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2"
                Guia.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2"

                Dim ublExtensiones As Guias.UBLExtensionType() = New Guias.UBLExtensionType(4) {}
                Dim ublExtension As Guias.UBLExtensionType = New Guias.UBLExtensionType()
                ublExtensiones(0) = ublExtension
                Guia.UBLExtensions = ublExtensiones
                Guia.UBLVersionID = New Guias.UBLVersionIDType()
                Guia.UBLVersionID.Value = "2.1"
                Guia.CustomizationID = New Guias.CustomizationIDType()
                Guia.CustomizationID.Value = "1.0"
                Guia.ID = New Guias.IDType()
                Guia.ID.Value = Comprobante.NumeroGuiaRemision
                Guia.IssueDate = New Guias.IssueDateType()

                Dim fechaemision As String = Convert.ToDateTime(Comprobante.FechaEmision).ToString("dd/MM/yyyy")
                Guia.IssueDate.Value = Convert.ToDateTime(fechaemision)

                Dim TipoDoc As Guias.DespatchAdviceTypeCodeType = New Guias.DespatchAdviceTypeCodeType()
                TipoDoc.Value = "09"
                Guia.DespatchAdviceTypeCode = TipoDoc

                Dim Nota As Guias.NoteType = New Guias.NoteType()
                Dim Notas As Guias.NoteType() = New Guias.NoteType(1) {}
                Nota.Value = "GUIA DE PRUEBA"
                Notas(0) = Nota
                Guia.Note = Notas

                Dim Firmas As Guias.SignatureType() = New Guias.SignatureType(1) {}
                Dim Firma As Guias.SignatureType = New Guias.SignatureType()
                Dim NumeroGuia As Guias.IDType = New Guias.IDType()
                NumeroGuia.Value = Comprobante.NumeroGuiaRemision
                Firma.ID = NumeroGuia

                Dim partySign As Guias.PartyType = New Guias.PartyType()
                Dim partyIdentificacion As Guias.PartyIdentificationType = New Guias.PartyIdentificationType()
                Dim partyIdentificacions As Guias.PartyIdentificationType() = New Guias.PartyIdentificationType(1) {}
                Dim idType As Guias.IDType = New Guias.IDType()
                idType.Value = EmpresaRUC
                partyIdentificacion.ID = idType
                partyIdentificacions(0) = partyIdentificacion
                partySign.PartyIdentification = partyIdentificacions
                Firma.SignatoryParty = partySign

                Dim partyName As Guias.PartyNameType = New Guias.PartyNameType()
                Dim partyNames As Guias.PartyNameType() = New Guias.PartyNameType(1) {}
                Dim RazonSocialFirma As Guias.NameType1 = New Guias.NameType1()
                RazonSocialFirma.Value = Comprobante.EmpresaRazonSocial
                partyName.Name = RazonSocialFirma
                partyNames(0) = partyName
                partySign.PartyName = partyNames

                Dim attachType As Guias.AttachmentType = New Guias.AttachmentType()
                Dim externaReferencia As Guias.ExternalReferenceType = New Guias.ExternalReferenceType()
                Dim uri As Guias.URIType = New Guias.URIType()
                uri.Value = EmpresaRUC & "-" & Comprobante.NumeroGuiaRemision
                externaReferencia.URI = uri
                Firma.DigitalSignatureAttachment = attachType
                attachType.ExternalReference = externaReferencia
                Firma.DigitalSignatureAttachment = attachType
                Firmas(0) = Firma
                Guia.Signature = Firmas

                Dim Remitente As Guias.SupplierPartyType = New Guias.SupplierPartyType()
                Dim party As Guias.PartyType = New Guias.PartyType()
                Dim _partyidentificacion As Guias.PartyIdentificationType = New Guias.PartyIdentificationType()
                Dim _partyidentificacions As Guias.PartyIdentificationType() = New Guias.PartyIdentificationType(1) {}
                Dim idEmpresa As Guias.IDType = New Guias.IDType()
                Dim RemitenteTipoDocumento As Guias.CustomerAssignedAccountIDType = New Guias.CustomerAssignedAccountIDType()
                RemitenteTipoDocumento.schemeID = Comprobante.TipoDocRemite
                RemitenteTipoDocumento.Value = Comprobante.NumDocRemite
                Remitente.CustomerAssignedAccountID = RemitenteTipoDocumento

                Dim pLEntityRemite As Guias.PartyLegalEntityType = New Guias.PartyLegalEntityType()
                Dim pLEntityRemites As Guias.PartyLegalEntityType() = New Guias.PartyLegalEntityType(1) {}
                Dim registerNameEmisor As Guias.RegistrationNameType = New Guias.RegistrationNameType()
                registerNameEmisor.Value = Comprobante.EmpresaRazonSocial
                pLEntityRemite.RegistrationName = registerNameEmisor
                pLEntityRemites(0) = pLEntityRemite
                party.PartyLegalEntity = pLEntityRemites
                Remitente.Party = party
                Guia.DespatchSupplierParty = Remitente

                Dim Destinatario As Guias.CustomerPartyType = New Guias.CustomerPartyType()
                Dim DestinatarioTipoDocumento As Guias.CustomerAssignedAccountIDType = New Guias.CustomerAssignedAccountIDType()
                DestinatarioTipoDocumento.schemeID = Comprobante.TipoDocDestinatario
                DestinatarioTipoDocumento.Value = Comprobante.NumDocDestinatario
                Destinatario.CustomerAssignedAccountID = DestinatarioTipoDocumento

                Dim DestinatarioRazonSocial As Guias.PartyType = New Guias.PartyType()
                Dim DestinatarioRazon As Guias.PartyLegalEntityType = New Guias.PartyLegalEntityType()
                Dim DestinatariosRazon As Guias.PartyLegalEntityType() = New Guias.PartyLegalEntityType(1) {}
                Dim DestinararioNombre As Guias.RegistrationNameType = New Guias.RegistrationNameType()
                DestinararioNombre.Value = Comprobante.RazonSocialDestinatario
                DestinatarioRazon.RegistrationName = DestinararioNombre
                DestinatariosRazon(0) = DestinatarioRazon
                DestinatarioRazonSocial.PartyLegalEntity = DestinatariosRazon
                Destinatario.Party = DestinatarioRazonSocial
                Guia.DeliveryCustomerParty = Destinatario

                Dim Envio As Guias.ShipmentType = New Guias.ShipmentType()
                Dim idEnvio As Guias.IDType = New Guias.IDType()
                idEnvio.Value = "001"
                Envio.ID = idEnvio

                Dim TipoEnvio As Guias.HandlingCodeType = New Guias.HandlingCodeType()
                TipoEnvio.Value = Comprobante.MotivoTraslado
                Envio.HandlingCode = TipoEnvio

                Dim Motivos As Guias.InformationType() = New Guias.InformationType(1) {}
                Dim Motivo As Guias.InformationType = New Guias.InformationType()
                Motivo.Value = Comprobante.DescMotivo
                Motivos(0) = Motivo
                Envio.Information = Motivos

                Dim Peso As Guias.GrossWeightMeasureType = New Guias.GrossWeightMeasureType()
                Peso.unitCode = Comprobante.UniMedPesoBruto
                Peso.Value = Convert.ToDecimal(Comprobante.Peso)
                Envio.GrossWeightMeasure = Peso

                Dim Indicador As Guias.SplitConsignmentIndicatorType = New Guias.SplitConsignmentIndicatorType()
                Indicador.Value = False
                Envio.SplitConsignmentIndicator = Indicador

                Dim Transportes As Guias.ShipmentStageType() = New Guias.ShipmentStageType(1) {}
                Dim Transporte As Guias.ShipmentStageType = New Guias.ShipmentStageType()
                Dim ModoTransporte As Guias.TransportModeCodeType = New Guias.TransportModeCodeType()
                ModoTransporte.Value = Comprobante.MovilTraslado
                Transporte.TransportModeCode = ModoTransporte

                Dim Periodo As Guias.PeriodType = New Guias.PeriodType()
                Dim FechaInicio As Guias.StartDateType = New Guias.StartDateType()
                FechaInicio.Value = Convert.ToDateTime(Comprobante.FechaInicioTraslado)
                Periodo.StartDate = FechaInicio
                Transporte.TransitPeriod = Periodo

                Dim CompañiaTransporte As Guias.PartyType = New Guias.PartyType()
                Dim CompañiasTransportes As Guias.PartyType() = New Guias.PartyType(1) {}
                Dim RUCTransporte As Guias.PartyIdentificationType = New Guias.PartyIdentificationType()
                Dim RUCsTransportes As Guias.PartyIdentificationType() = New Guias.PartyIdentificationType(1) {}
                Dim idDocumento As Guias.IDType = New Guias.IDType()
                idDocumento.Value = Comprobante.NumRucTransportista
                RUCTransporte.ID = idDocumento
                RUCsTransportes(0) = RUCTransporte
                CompañiaTransporte.PartyIdentification = RUCsTransportes
                CompañiasTransportes(0) = CompañiaTransporte
                Transporte.CarrierParty = CompañiasTransportes

                Dim NombresTransportes As Guias.PartyNameType() = New Guias.PartyNameType(1) {}
                Dim NombreTransporte As Guias.PartyNameType = New Guias.PartyNameType()
                Dim Nombre As Guias.NameType1 = New Guias.NameType1()
                Nombre.Value = Comprobante.RazonSocialTransportista
                NombreTransporte.Name = Nombre
                NombresTransportes(0) = NombreTransporte
                CompañiaTransporte.PartyName = NombresTransportes
                CompañiasTransportes(0) = CompañiaTransporte
                Transporte.CarrierParty = CompañiasTransportes

                Dim Transportista As Guias.TransportMeansType = New Guias.TransportMeansType()
                Dim TransportistaPlaca As Guias.RoadTransportType = New Guias.RoadTransportType()
                Dim NumeroPlaca As Guias.LicensePlateIDType = New Guias.LicensePlateIDType()
                NumeroPlaca.Value = Comprobante.Placa
                TransportistaPlaca.LicensePlateID = NumeroPlaca
                Transportista.RoadTransport = TransportistaPlaca
                Transporte.TransportMeans = Transportista

                Dim conductores As Guias.PersonType() = New Guias.PersonType(1) {}
                Dim conductor As Guias.PersonType = New Guias.PersonType()
                Dim idlicencia As Guias.IDType = New Guias.IDType()
                idlicencia.Value = Comprobante.LicenciaConducir
                idlicencia.schemeID = "1"
                conductor.ID = idlicencia
                conductores(0) = conductor
                Transporte.DriverPerson = conductores
                Transportes(0) = Transporte
                Envio.ShipmentStage = Transportes

                Dim Entrega As Guias.DeliveryType = New Guias.DeliveryType()
                Dim DireccionEntrega As Guias.AddressType = New Guias.AddressType()
                Dim UbigeoEntrega As Guias.IDType = New Guias.IDType()
                UbigeoEntrega.Value = Comprobante.UbigeoPuntoLlegada
                DireccionEntrega.ID = UbigeoEntrega

                Dim calle As Guias.StreetNameType = New Guias.StreetNameType()
                calle.Value = Comprobante.Llegada
                DireccionEntrega.StreetName = calle

                Dim Pais As Guias.CountryType = New Guias.CountryType()
                Dim Codigo As Guias.IdentificationCodeType = New Guias.IdentificationCodeType()
                Codigo.Value = "PE"
                Pais.IdentificationCode = Codigo
                DireccionEntrega.Country = Pais
                Entrega.DeliveryAddress = DireccionEntrega
                Envio.Delivery = Entrega

                Dim DireccionPartida As Guias.AddressType = New Guias.AddressType()
                Dim idDireccionPartida As Guias.IDType = New Guias.IDType()
                idDireccionPartida.Value = Comprobante.UbigeoPuntoPartida
                DireccionPartida.ID = idDireccionPartida

                Dim CallePartida As Guias.StreetNameType = New Guias.StreetNameType()
                CallePartida.Value = Comprobante.Partida
                DireccionPartida.StreetName = CallePartida

                Dim PaisOrigen As Guias.CountryType = New Guias.CountryType()
                Dim CodigoOrigen As Guias.IdentificationCodeType = New Guias.IdentificationCodeType()
                CodigoOrigen.Value = "PE"
                PaisOrigen.IdentificationCode = CodigoOrigen
                DireccionPartida.Country = PaisOrigen
                Envio.OriginAddress = DireccionPartida
                Guia.Shipment = Envio

                Dim items As Guias.DespatchLineType() = New Guias.DespatchLineType(9) {}
                Dim i As Integer = 1

                For Each det As Detalles In Detalles

                    Dim item As Guias.DespatchLineType = New Guias.DespatchLineType()
                    Dim iditem As Guias.IDType = New Guias.IDType()
                    iditem.Value = i.ToString()
                    item.ID = iditem
                    Dim Cantidad As Guias.DeliveredQuantityType = New Guias.DeliveredQuantityType()
                    Cantidad.Value = Convert.ToDecimal(det.Cantidad)
                    Cantidad.unitCode = det.UnidadMedida
                    item.DeliveredQuantity = Cantidad
                    Dim Ordenes As Guias.OrderLineReferenceType() = New Guias.OrderLineReferenceType(1) {}
                    Dim Orden As Guias.OrderLineReferenceType = New Guias.OrderLineReferenceType()
                    Dim Linea As Guias.LineIDType = New Guias.LineIDType()
                    Linea.Value = i.ToString()
                    Orden.LineID = Linea
                    Ordenes(0) = Orden
                    item.OrderLineReference = Ordenes
                    Dim itemTipo As Guias.ItemType = New Guias.ItemType()
                    Dim NombreItem As Guias.NameType1 = New Guias.NameType1()
                    NombreItem.Value = det.DescripcionProducto
                    itemTipo.Name = NombreItem
                    Dim Identificacion As Guias.ItemIdentificationType = New Guias.ItemIdentificationType()
                    Dim idTipo As Guias.IDType = New Guias.IDType()
                    idTipo.Value = det.Codcom
                    Identificacion.ID = idTipo
                    itemTipo.SellersItemIdentification = Identificacion
                    item.Item = itemTipo
                    items(i) = item
                    i = i + 1
                Next

                Guia.DespatchLine = items
                Dim archXML As String = GenerarGuiaRemitente(Guia, EmpresaRUC)
                FirmarXML(archXML, Ruta_Certificado, Password_Certificado)
                Dim strEnvio As String = Ruta_ENVIOS & Path.GetFileName(archXML).Replace(".xml", ".zip")
                Comprimir(archXML, strEnvio)
                'Dim servicio As New SUNAT_UTIL()
                EnviarDocumentoGuiaRemision(strEnvio)
                Return 1
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Function GenerarComprobanteNC_XML(ByVal Comprobante As Cabecera) As Integer
            Dim Factura As CapaSUNAT.Modelos.CreditNoteType = New CapaSUNAT.Modelos.CreditNoteType()

            Try
                Factura.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                Factura.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
                Factura.Ccts = "urn:un:unece:uncefact:documentation:2"
                Factura.Ds = "http://www.w3.org/2000/09/xmldsig#"
                Factura.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
                Factura.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2"
                Factura.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2"
                Dim ublExtensiones As CapaSUNAT.Modelos.UBLExtensionType() = New CapaSUNAT.Modelos.UBLExtensionType(4) {}
                Dim ublExtension As CapaSUNAT.Modelos.UBLExtensionType = New CapaSUNAT.Modelos.UBLExtensionType()
                ublExtensiones(0) = ublExtension
                Factura.UBLExtensions = ublExtensiones
                Factura.UBLVersionID = New CapaSUNAT.Modelos.UBLVersionIDType()
                Factura.UBLVersionID.Value = "2.1"
                Factura.CustomizationID = New CapaSUNAT.Modelos.CustomizationIDType()
                Factura.CustomizationID.Value = "2.0"


                'DATOS DE CABECERA DEL COMPROBANTE NOTA DE CREDITO
                '******************************************************************************************************************************************
                Factura.ID = New CapaSUNAT.Modelos.IDType()
                Factura.ID.Value = Comprobante.Serie & "-" + Comprobante.Numero
                Factura.IssueDate = New CapaSUNAT.Modelos.IssueDateType()
                Dim fechaemision As String = Convert.ToDateTime(Comprobante.Fechaemision).ToString("dd/MM/yyyy")
                Factura.IssueDate.Value = Convert.ToDateTime(fechaemision)
                Factura.IssueTime = New CapaSUNAT.Modelos.IssueTimeType()
                Dim hora As String = Convert.ToDateTime(Comprobante.Fechaemision).ToString("HH:mm:ss")
                Factura.IssueTime.Value = hora
                Dim moneda As CapaSUNAT.Modelos.DocumentCurrencyCodeType = New CapaSUNAT.Modelos.DocumentCurrencyCodeType() With {
                    .listSchemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo51",
                    .listID = "ISO 4217 Alpha",
                    .listName = "Currency",
                    .listAgencyName = "United Nations Economic Commission for Europe",
                    .Value = Comprobante.Idmoneda
                }

                Factura.DocumentCurrencyCode = moneda
                Dim DocumentoRel As CapaSUNAT.Modelos.ResponseType = New CapaSUNAT.Modelos.ResponseType()
                Dim DocumentoRels As CapaSUNAT.Modelos.ResponseType() = New CapaSUNAT.Modelos.ResponseType(1) {}
                Dim NumeroDocRel As CapaSUNAT.Modelos.ReferenceIDType = New CapaSUNAT.Modelos.ReferenceIDType()
                NumeroDocRel.Value = Comprobante.Serie & "-" + Comprobante.Numero
                Dim TipoDocRel As CapaSUNAT.Modelos.ResponseCodeType = New CapaSUNAT.Modelos.ResponseCodeType()
                TipoDocRel.Value = Comprobante.Idtipocomp
                Dim Motivo As CapaSUNAT.Modelos.DescriptionType = New CapaSUNAT.Modelos.DescriptionType()
                Dim Motivos As CapaSUNAT.Modelos.DescriptionType() = New CapaSUNAT.Modelos.DescriptionType(1) {}
                Motivos(0) = Motivo
                Motivo.Value = Comprobante.Cab_Ref_Motivo
                DocumentoRel.ReferenceID = NumeroDocRel
                DocumentoRel.ResponseCode = TipoDocRel
                DocumentoRel.Description = Motivos
                DocumentoRels(0) = DocumentoRel
                Factura.DiscrepancyResponse = DocumentoRels
                Dim referencias As CapaSUNAT.Modelos.BillingReferenceType() = New CapaSUNAT.Modelos.BillingReferenceType(1) {}
                Dim referencia As CapaSUNAT.Modelos.BillingReferenceType = New CapaSUNAT.Modelos.BillingReferenceType()
                Dim documento As CapaSUNAT.Modelos.DocumentReferenceType = New CapaSUNAT.Modelos.DocumentReferenceType()
                Dim docRela As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                docRela.Value = Comprobante.Cab_Ref_Serie & "-" + Comprobante.Cab_Ref_Numero
                Dim TipoDocumentoRel As CapaSUNAT.Modelos.DocumentTypeCodeType = New CapaSUNAT.Modelos.DocumentTypeCodeType()
                TipoDocumentoRel.Value = Comprobante.Cab_Ref_TipoDeDocumento
                documento.DocumentTypeCode = TipoDocumentoRel
                documento.ID = docRela
                referencia.InvoiceDocumentReference = documento
                referencias(0) = referencia
                Factura.BillingReference = referencias

                'FIRMA DEL COMPROBANTE - SIGNATURE
                '********************************************************************************************************************************
                Dim Firma As CapaSUNAT.Modelos.SignatureType = New CapaSUNAT.Modelos.SignatureType()
                Dim Firmas As CapaSUNAT.Modelos.SignatureType() = New CapaSUNAT.Modelos.SignatureType(1) {}
                Dim partySign As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim partyIdentificacion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim partyIdentificacions As CapaSUNAT.Modelos.PartyIdentificationType() = New CapaSUNAT.Modelos.PartyIdentificationType(1) {}
                Dim idFirma As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idFirma.Value = Comprobante.EmpresaRUC
                Firma.ID = idFirma
                partyIdentificacion.ID = idFirma
                partyIdentificacions(0) = partyIdentificacion
                partySign.PartyIdentification = partyIdentificacions
                Firma.SignatoryParty = partySign


                'DATOS DE LA EMPRESA EMISORA
                '***********************************************************************************************************************************************************
                Dim empresa As CapaSUNAT.Modelos.SupplierPartyType = New CapaSUNAT.Modelos.SupplierPartyType()
                Dim party As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim _partyidentificacion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim _partyidentificacions As CapaSUNAT.Modelos.PartyIdentificationType() = New CapaSUNAT.Modelos.PartyIdentificationType(2) {}

                Dim idEmpresa As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idEmpresa.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idEmpresa.schemeName = "Documento de Identidad"
                idEmpresa.schemeID = "6"
                idEmpresa.schemeAgencyName = "PE:SUNAT"
                idEmpresa.Value = Comprobante.EmpresaRUC

                _partyidentificacion.ID = idEmpresa
                _partyidentificacions(0) = _partyidentificacion


                Dim partyname As CapaSUNAT.Modelos.PartyNameType = New CapaSUNAT.Modelos.PartyNameType()
                Dim partynames As List(Of CapaSUNAT.Modelos.PartyNameType) = New List(Of CapaSUNAT.Modelos.PartyNameType)()
                Dim nameEmisor As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()

                nameEmisor.Value = Comprobante.EmpresaRazonSocial
                partyname.Name = nameEmisor
                partynames.Add(partyname)
                party.PartyName = partynames.ToArray()


                Dim registerNameEmisor As CapaSUNAT.Modelos.RegistrationNameType = New CapaSUNAT.Modelos.RegistrationNameType()
                registerNameEmisor.Value = Comprobante.EmpresaRazonSocial
                Dim compañia As Modelos.CompanyIDType = New Modelos.CompanyIDType()

                compañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                compañia.schemeAgencyName = "PE:SUNAT"
                compañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                compañia.schemeID = "6"
                compañia.Value = Comprobante.EmpresaRUC

                Dim direccion As CapaSUNAT.Modelos.AddressType = New CapaSUNAT.Modelos.AddressType()
                Dim addrestypecode As CapaSUNAT.Modelos.AddressTypeCodeType = New CapaSUNAT.Modelos.AddressTypeCodeType()
                addrestypecode.listName = "Establecimientos anexos"
                addrestypecode.listAgencyName = "PE:SUNAT"
                addrestypecode.Value = "0000"

                Dim taxSchema As Modelos.TaxSchemeType = New Modelos.TaxSchemeType()
                Dim idsupplier As Modelos.IDType = New Modelos.IDType()
                idsupplier.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idsupplier.schemeAgencyName = "PE:SUNAT"
                idsupplier.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idsupplier.schemeID = "6"
                idsupplier.Value = Comprobante.EmpresaRUC
                taxSchema.ID = idsupplier

                Dim partelegals As List(Of Modelos.PartyLegalEntityType) = New List(Of Modelos.PartyLegalEntityType)()
                Dim partelegal As Modelos.PartyLegalEntityType = New Modelos.PartyLegalEntityType()
                Dim registerNamePL As CapaSUNAT.Modelos.RegistrationNameType = New CapaSUNAT.Modelos.RegistrationNameType()
                registerNamePL.Value = Comprobante.EmpresaRazonSocial
                partelegal.RegistrationName = registerNamePL

                Dim direccionPL As Modelos.AddressType = New Modelos.AddressType()
                Dim iddireccionPL As Modelos.IDType = New Modelos.IDType()
                iddireccionPL.schemeAgencyName = "PE:INEI"
                iddireccionPL.schemeName = "Ubigeos"
                iddireccionPL.Value = Comprobante.ID_EmpresaDepartamento + Comprobante.ID_EmpresaProvincia + Comprobante.ID_EmpresaDistrito
                direccionPL.ID = iddireccionPL

                Dim address_TypeCodeType As Modelos.AddressTypeCodeType = New Modelos.AddressTypeCodeType()
                address_TypeCodeType.listName = "Establecimientos anexos"
                address_TypeCodeType.listAgencyName = "PE:SUNAT"
                address_TypeCodeType.Value = "0001"
                direccionPL.AddressTypeCode = address_TypeCodeType

                Dim Departamento As CapaSUNAT.Modelos.CityNameType = New CapaSUNAT.Modelos.CityNameType()
                Departamento.Value = Comprobante.EmpresaDepartamento
                direccionPL.CityName = Departamento

                Dim Provincia As CapaSUNAT.Modelos.CountrySubentityType = New CapaSUNAT.Modelos.CountrySubentityType()
                Provincia.Value = Comprobante.EmpresaProvincia
                direccionPL.CountrySubentity = Provincia

                Dim distrito As CapaSUNAT.Modelos.DistrictType = New CapaSUNAT.Modelos.DistrictType()
                distrito.Value = Comprobante.EmpresaDistrito
                direccionPL.District = distrito

                Dim direcciones As List(Of Modelos.AddressLineType) = New List(Of Modelos.AddressLineType)()
                Dim direccionEmisor As Modelos.AddressLineType = New Modelos.AddressLineType()
                Dim local1 As Modelos.LineType = New Modelos.LineType()
                local1.Value = Comprobante.EmpresaDireccion
                direccionPL.AddressLine = direcciones.ToArray()

                direccionEmisor.Line = local1
                direcciones.Add(direccionEmisor)
                direccionPL.AddressLine = direcciones.ToArray()

                Dim pais As CapaSUNAT.Modelos.CountryType = New CapaSUNAT.Modelos.CountryType()
                Dim codigoPais As CapaSUNAT.Modelos.IdentificationCodeType = New CapaSUNAT.Modelos.IdentificationCodeType()
                codigoPais.listName = "Country"
                codigoPais.listAgencyName = "United Nations Economic Commission for Europe"
                codigoPais.listID = "ISO 3166-1"
                codigoPais.Value = "PE"
                pais.IdentificationCode = codigoPais
                direccionPL.Country = pais
                partelegal.RegistrationAddress = direccionPL
                partelegals.Add(partelegal)

                party.PartyLegalEntity = partelegals.ToArray()
                party.PartyName = partynames.ToArray()
                party.PartyIdentification = _partyidentificacions

                empresa.Party = party
                Factura.AccountingSupplierParty = empresa


                'Datos del cliente
                '********************************************************************************************************************************************
                Dim taxschemeCliente As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                Dim CustomerPartyCliente As CapaSUNAT.Modelos.CustomerPartyType = New CapaSUNAT.Modelos.CustomerPartyType()
                Dim partyCliente As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim partyIdentificion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim partyIdentificions As List(Of CapaSUNAT.Modelos.PartyIdentificationType) = New List(Of CapaSUNAT.Modelos.PartyIdentificationType)()

                Dim idtipo As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idtipo.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idtipo.schemeName = "Documento de Identidad"
                idtipo.schemeAgencyName = "PE:SUNAT"
                idtipo.schemeID = Comprobante.ClienteTipodocumento
                idtipo.Value = Comprobante.ClienteNumeroDocumento
                partyIdentificion.ID = idtipo
                partyIdentificions.Add(partyIdentificion)
                partyCliente.PartyIdentification = partyIdentificions.ToArray()

                Dim RazSocClientes As List(Of CapaSUNAT.Modelos.PartyNameType) = New List(Of CapaSUNAT.Modelos.PartyNameType)()
                Dim RazSocCliente As CapaSUNAT.Modelos.PartyNameType = New CapaSUNAT.Modelos.PartyNameType()
                Dim razSocial As Modelos.NameType1 = New Modelos.NameType1()
                razSocial.Value = Comprobante.ClienteRazonSocial
                RazSocCliente.Name = razSocial
                RazSocClientes.Add(RazSocCliente)
                Dim partySchemas As List(Of CapaSUNAT.Modelos.PartyTaxSchemeType) = New List(Of Modelos.PartyTaxSchemeType)()
                Dim partySchema As CapaSUNAT.Modelos.PartyTaxSchemeType = New CapaSUNAT.Modelos.PartyTaxSchemeType()
                Dim RegistroNombre As Modelos.RegistrationNameType = New Modelos.RegistrationNameType()

                RegistroNombre.Value = Comprobante.ClienteRazonSocial
                partySchema.RegistrationName = RegistroNombre
                Dim idcompañia As Modelos.CompanyIDType = New Modelos.CompanyIDType()
                idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idcompañia.schemeAgencyName = "PE:SUNAT"
                idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idcompañia.schemeID = Comprobante.ClienteTipodocumento
                idcompañia.Value = Comprobante.ClienteNumeroDocumento
                Dim schemeType As Modelos.TaxSchemeType = New Modelos.TaxSchemeType()
                Dim idc As Modelos.IDType = New Modelos.IDType()
                idc.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idc.schemeAgencyName = "PE:SUNAT"
                idc.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idc.schemeID = Comprobante.ClienteTipodocumento
                idc.Value = Comprobante.ClienteNumeroDocumento
                schemeType.ID = idc
                idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idcompañia.schemeAgencyName = "PE:SUNAT"
                idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idcompañia.schemeID = Comprobante.ClienteTipodocumento
                idcompañia.Value = Comprobante.ClienteNumeroDocumento
                Dim partyLegals As List(Of Modelos.PartyLegalEntityType) = New List(Of Modelos.PartyLegalEntityType)()
                Dim partyLegal As Modelos.PartyLegalEntityType = New Modelos.PartyLegalEntityType()
                Dim Registro_Nombre As Modelos.RegistrationNameType = New Modelos.RegistrationNameType()
                Registro_Nombre.Value = Comprobante.ClienteRazonSocial
                partyLegal.RegistrationName = Registro_Nombre
                Dim direccionCliente As Modelos.AddressType = New Modelos.AddressType()
                Dim dirs As List(Of Modelos.AddressLineType) = New List(Of Modelos.AddressLineType)()
                Dim dir As Modelos.AddressLineType = New Modelos.AddressLineType()
                Dim lineas As List(Of Modelos.LineType) = New List(Of Modelos.LineType)()
                Dim linea As Modelos.LineType = New Modelos.LineType()
                linea.Value = Comprobante.ClienteDireccion
                dir.Line = linea
                dirs.Add(dir)
                direccionCliente.AddressLine = dirs.ToArray()
                Dim paisC As CapaSUNAT.Modelos.CountryType = New CapaSUNAT.Modelos.CountryType()
                Dim codigoPaisC As CapaSUNAT.Modelos.IdentificationCodeType = New CapaSUNAT.Modelos.IdentificationCodeType()
                codigoPaisC.Value = "PE"
                paisC.IdentificationCode = codigoPaisC
                partyLegals.Add(partyLegal)
                partySchema.CompanyID = idcompañia
                partySchema.TaxScheme = schemeType
                partySchemas.Add(partySchema)
                partyCliente.PartyLegalEntity = partyLegals.ToArray()
                CustomerPartyCliente.Party = partyCliente


                Dim accoutingCustomerParty As Modelos.CustomerPartyType = New Modelos.CustomerPartyType()
                accoutingCustomerParty.Party = partyCliente
                Factura.AccountingCustomerParty = accoutingCustomerParty


                'TOTALES  DE COMPROBANTE 
                '****************************************************************************************************************************************************
                Dim TotalImptos As CapaSUNAT.Modelos.TaxTotalType = New CapaSUNAT.Modelos.TaxTotalType()
                Dim taxAmountImpto As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                taxAmountImpto.currencyID = Comprobante.Idmoneda
                taxAmountImpto.Value = Convert.ToDecimal(Comprobante.TotIgv)
                TotalImptos.TaxAmount = taxAmountImpto
                Dim subtotales As CapaSUNAT.Modelos.TaxSubtotalType() = New CapaSUNAT.Modelos.TaxSubtotalType(1) {}
                Dim subtotal As CapaSUNAT.Modelos.TaxSubtotalType = New CapaSUNAT.Modelos.TaxSubtotalType()
                Dim taxsubtotal As CapaSUNAT.Modelos.TaxableAmountType = New CapaSUNAT.Modelos.TaxableAmountType()
                taxsubtotal.currencyID = Comprobante.Idmoneda
                taxsubtotal.Value = Convert.ToDecimal(Comprobante.TotSubtotal)
                subtotal.TaxableAmount = taxsubtotal
                Dim TotalTaxAmountTotal As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                TotalTaxAmountTotal.currencyID = Comprobante.Idmoneda
                TotalTaxAmountTotal.Value = Convert.ToDecimal(Comprobante.TotIgv)
                subtotal.TaxAmount = TotalTaxAmountTotal

                Dim subTotalIGV As Modelos.TaxSubtotalType = New Modelos.TaxSubtotalType()
                subTotalIGV.TaxableAmount = taxsubtotal
                subtotales(0) = subtotal
                TotalImptos.TaxSubtotal = subtotales

                Dim taxcategoryTotal As CapaSUNAT.Modelos.TaxCategoryType = New CapaSUNAT.Modelos.TaxCategoryType()
                Dim taxScheme As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                Dim idTotal As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idTotal.schemeID = "UN/ECE 5305"
                idTotal.schemeName = "Tax Category Identifier"
                idTotal.schemeAgencyName = "United Nations Economic Commission for Europe"
                idTotal.Value = "S"

                Dim nametypeImpto As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                nametypeImpto.Value = "IGV"
                Dim taxtypecodeImpto As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                taxtypecodeImpto.Value = "VAT"
                Dim idTot As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idTot.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05"
                idTot.schemeAgencyName = "PE:SUNAT"
                idTot.schemeName = "Codigo de tributos"
                idTot.Value = "1000"
                taxScheme.ID = idTot

                Dim nametypeImptoIGV As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                nametypeImptoIGV.Value = "IGV"
                Dim taxtypecodeImpuesto As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                taxtypecodeImpuesto.Value = "VAT"
                taxScheme.Name = nametypeImpto
                taxScheme.TaxTypeCode = taxtypecodeImpto
                taxcategoryTotal.TaxScheme = taxScheme
                subtotal.TaxCategory = taxcategoryTotal

                Dim TaxSubtotals As List(Of CapaSUNAT.Modelos.TaxSubtotalType) = New List(Of CapaSUNAT.Modelos.TaxSubtotalType)()
                TaxSubtotals.Add(subtotal)
                TotalImptos.TaxSubtotal = TaxSubtotals.ToArray()
                Dim taxTotals As List(Of CapaSUNAT.Modelos.TaxTotalType) = New List(Of CapaSUNAT.Modelos.TaxTotalType)()
                taxTotals.Add(TotalImptos)
                Factura.TaxTotal = taxTotals.ToArray()


                Dim TotalValorVenta As CapaSUNAT.Modelos.MonetaryTotalType = New CapaSUNAT.Modelos.MonetaryTotalType()
                Dim TValorVenta As CapaSUNAT.Modelos.LineExtensionAmountType = New CapaSUNAT.Modelos.LineExtensionAmountType()
                TValorVenta.currencyID = Comprobante.Idmoneda
                TValorVenta.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotSubtotal))
                TotalValorVenta.LineExtensionAmount = TValorVenta
                Dim TotalPrecioVenta As CapaSUNAT.Modelos.TaxInclusiveAmountType = New CapaSUNAT.Modelos.TaxInclusiveAmountType()
                TotalPrecioVenta.currencyID = Comprobante.Idmoneda
                TotalPrecioVenta.Value = Convert.ToDecimal(Comprobante.TotNeto)
                Dim MtoTotalDsctos As CapaSUNAT.Modelos.AllowanceTotalAmountType = New CapaSUNAT.Modelos.AllowanceTotalAmountType()
                MtoTotalDsctos.currencyID = Comprobante.Idmoneda
                MtoTotalDsctos.Value = Convert.ToDecimal(Comprobante.TotDsctos)
                Dim MtoTotalOtrosCargos As CapaSUNAT.Modelos.ChargeTotalAmountType = New CapaSUNAT.Modelos.ChargeTotalAmountType()
                MtoTotalOtrosCargos.currencyID = Comprobante.Idmoneda
                MtoTotalOtrosCargos.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotOtros))
                TotalValorVenta.ChargeTotalAmount = MtoTotalOtrosCargos
                Dim MtoCargos As CapaSUNAT.Modelos.PrepaidAmountType = New CapaSUNAT.Modelos.PrepaidAmountType()
                MtoCargos.currencyID = Comprobante.Idmoneda
                MtoCargos.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotOtros))
                MtoCargos.Value = Convert.ToDecimal(String.Format("{0:0.00}", 0))
                TotalValorVenta.PrepaidAmount = MtoCargos
                Dim ImporteTotalVenta As CapaSUNAT.Modelos.PayableAmountType = New CapaSUNAT.Modelos.PayableAmountType()
                ImporteTotalVenta.currencyID = Comprobante.Idmoneda
                ImporteTotalVenta.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotNeto))
                TotalValorVenta.LineExtensionAmount = TValorVenta
                TotalValorVenta.TaxInclusiveAmount = TotalPrecioVenta
                TotalValorVenta.AllowanceTotalAmount = MtoTotalDsctos
                TotalValorVenta.ChargeTotalAmount = MtoTotalOtrosCargos
                TotalValorVenta.PrepaidAmount = MtoCargos
                TotalValorVenta.PayableAmount = ImporteTotalVenta
                Factura.LegalMonetaryTotal = TotalValorVenta
                Dim items As CapaSUNAT.Modelos.CreditNoteLineType() = New CapaSUNAT.Modelos.CreditNoteLineType(9) {}
                Dim iditem As Integer = 1

                'Detalles del comprobante
                '***********************************************************************************************************************************************************************
                For Each det As Detalles In Comprobante.Detalles
                    Dim item As CapaSUNAT.Modelos.CreditNoteLineType = New CapaSUNAT.Modelos.CreditNoteLineType()
                    Dim numeroItem As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    numeroItem.Value = iditem.ToString()
                    item.ID = numeroItem
                    Dim cantidad As CapaSUNAT.Modelos.CreditedQuantityType = New CapaSUNAT.Modelos.CreditedQuantityType()
                    cantidad.unitCodeListAgencyName = "United Nations Economic Commission for Europe"
                    cantidad.unitCodeListID = "UN/ECE rec 20"
                    cantidad.unitCode = det.UnidadMedida
                    cantidad.Value = Convert.ToInt32(det.Cantidad)
                    item.CreditedQuantity = cantidad
                    Dim ValorVenta As CapaSUNAT.Modelos.LineExtensionAmountType = New CapaSUNAT.Modelos.LineExtensionAmountType()
                    ValorVenta.currencyID = Comprobante.Idmoneda
                    ValorVenta.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.Total / 1.18D))
                    item.LineExtensionAmount = ValorVenta
                    Dim ValorReferenUnitario As CapaSUNAT.Modelos.PricingReferenceType = New CapaSUNAT.Modelos.PricingReferenceType()
                    Dim TipoPrecios As CapaSUNAT.Modelos.PriceType() = New CapaSUNAT.Modelos.PriceType(1) {}
                    Dim TipoPrecio As CapaSUNAT.Modelos.PriceType = New CapaSUNAT.Modelos.PriceType()
                    Dim PrecioMonto As CapaSUNAT.Modelos.PriceAmountType = New CapaSUNAT.Modelos.PriceAmountType()
                    PrecioMonto.currencyID = Comprobante.Idmoneda
                    PrecioMonto.Value = Convert.ToDecimal(String.Format("{0:0.000}", det.Precio))
                    TipoPrecio.PriceAmount = PrecioMonto
                    Dim TipoPrecioCode As CapaSUNAT.Modelos.PriceTypeCodeType = New CapaSUNAT.Modelos.PriceTypeCodeType()
                    TipoPrecioCode.listName = "Tipo de Precio"
                    TipoPrecioCode.listAgencyName = "PE:SUNAT"
                    TipoPrecioCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16"
                    TipoPrecioCode.Value = "01"
                    TipoPrecio.PriceTypeCode = TipoPrecioCode
                    TipoPrecios(0) = TipoPrecio
                    ValorReferenUnitario.AlternativeConditionPrice = TipoPrecios
                    item.PricingReference = ValorReferenUnitario
                    Dim Totales_Items As CapaSUNAT.Modelos.TaxTotalType() = New CapaSUNAT.Modelos.TaxTotalType(1) {}
                    Dim Totales_Item As CapaSUNAT.Modelos.TaxTotalType = New CapaSUNAT.Modelos.TaxTotalType()
                    Dim Total_Item As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                    Total_Item.currencyID = Comprobante.Idmoneda
                    Total_Item.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18D)))
                    Totales_Item.TaxAmount = Total_Item
                    Dim subtotal_Items As CapaSUNAT.Modelos.TaxSubtotalType() = New CapaSUNAT.Modelos.TaxSubtotalType(1) {}
                    Dim subtotal_Item As CapaSUNAT.Modelos.TaxSubtotalType = New CapaSUNAT.Modelos.TaxSubtotalType()
                    Dim taxsubtotal_IGVItem As CapaSUNAT.Modelos.TaxableAmountType = New CapaSUNAT.Modelos.TaxableAmountType()
                    taxsubtotal_IGVItem.currencyID = Comprobante.Idmoneda
                    taxsubtotal_IGVItem.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem / 1.18D))
                    subtotal_Item.TaxableAmount = taxsubtotal_IGVItem
                    Dim TotalTaxAmount_IGVItem As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                    TotalTaxAmount_IGVItem.currencyID = Comprobante.Idmoneda
                    TotalTaxAmount_IGVItem.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18D)))
                    subtotal_Item.TaxAmount = TotalTaxAmount_IGVItem
                    subtotal_Items(0) = subtotal_Item
                    Totales_Item.TaxSubtotal = subtotal_Items
                    Dim taxcategory_IGVItem As CapaSUNAT.Modelos.TaxCategoryType = New CapaSUNAT.Modelos.TaxCategoryType()
                    Dim idTaxCategoria As Modelos.IDType = New Modelos.IDType()
                    idTaxCategoria.schemeAgencyName = "United Nations Economic Commission for Europe"
                    idTaxCategoria.schemeName = "Tax Category Identifier"
                    idTaxCategoria.schemeID = "UN/ECE 5305"
                    idTaxCategoria.Value = "S"
                    Dim porcentaje As Modelos.PercentType1 = New Modelos.PercentType1()
                    porcentaje.Value = Convert.ToDecimal(det.porIgvItem) * 100
                    taxcategory_IGVItem.Percent = porcentaje
                    subtotal_Item.TaxCategory = taxcategory_IGVItem
                    Dim ReasonCode As Modelos.TaxExemptionReasonCodeType = New Modelos.TaxExemptionReasonCodeType()
                    ReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07"
                    ReasonCode.listName = "Afectacion del IGV"
                    ReasonCode.listAgencyName = "PE:SUNAT"
                    ReasonCode.Value = "10"
                    taxcategory_IGVItem.TaxExemptionReasonCode = ReasonCode
                    Dim taxscheme_IGVItem As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                    Dim id2_IGVItem As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    id2_IGVItem.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05"
                    id2_IGVItem.schemeAgencyName = "PE:SUNAT"
                    id2_IGVItem.schemeName = "Codigo de tributos"
                    id2_IGVItem.Value = "1000"
                    taxscheme_IGVItem.ID = id2_IGVItem
                    Dim nombreImpto_IGVItem As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                    nombreImpto_IGVItem.Value = "IGV"
                    taxscheme_IGVItem.Name = nombreImpto_IGVItem
                    Dim nombreImpto_IGVItemInter As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                    nombreImpto_IGVItemInter.Value = "VAT"
                    taxscheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter
                    taxscheme_IGVItem.Name = nombreImpto_IGVItem
                    taxcategory_IGVItem.TaxScheme = taxscheme_IGVItem
                    subtotal_Items(0) = subtotal_Item
                    Totales_Item.TaxSubtotal = subtotal_Items
                    Totales_Items(0) = Totales_Item
                    item.TaxTotal = Totales_Items
                    Dim descriptions As CapaSUNAT.Modelos.DescriptionType() = New CapaSUNAT.Modelos.DescriptionType(1) {}
                    Dim description As CapaSUNAT.Modelos.DescriptionType = New CapaSUNAT.Modelos.DescriptionType()
                    description.Value = det.DescripcionProducto
                    Dim codigoProd As CapaSUNAT.Modelos.ItemIdentificationType = New CapaSUNAT.Modelos.ItemIdentificationType()
                    Dim id As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    id.Value = det.Codcom
                    codigoProd.ID = id
                    Dim PrecioProducto As CapaSUNAT.Modelos.PriceType = New CapaSUNAT.Modelos.PriceType()
                    Dim PrecioMontoTipo As CapaSUNAT.Modelos.PriceAmountType = New CapaSUNAT.Modelos.PriceAmountType()
                    PrecioMontoTipo.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.Precio / (det.porIgvItem + 1)))
                    PrecioMontoTipo.currencyID = Comprobante.Idmoneda
                    PrecioProducto.PriceAmount = PrecioMontoTipo
                    Dim itemTipo As CapaSUNAT.Modelos.ItemType = New CapaSUNAT.Modelos.ItemType()
                    descriptions(0) = description
                    itemTipo.Description = descriptions
                    itemTipo.SellersItemIdentification = codigoProd
                    Dim codSunats As List(Of Modelos.CommodityClassificationType) = New List(Of Modelos.CommodityClassificationType)()
                    Dim codSunat As Modelos.CommodityClassificationType = New Modelos.CommodityClassificationType()
                    Dim codClas As Modelos.ItemClassificationCodeType = New Modelos.ItemClassificationCodeType()
                    codClas.listName = "Item Classification"
                    codClas.listAgencyName = "GS1 US"
                    codClas.listID = "UNSPSC"
                    codClas.Value = "25172405"
                    codSunat.ItemClassificationCode = codClas
                    codSunats.Add(codSunat)
                    itemTipo.CommodityClassification = codSunats.ToArray()
                    item.Item = itemTipo
                    item.Price = PrecioProducto
                    items(iditem) = item
                    iditem += 1
                Next
                Factura.CreditNoteLine = items
                '*********************************************************************************************************************************************************
                'Generar XML
                Dim archXML As String = GenerarComprobante(Factura, Comprobante.EmpresaRUC, Comprobante.Idtipocomp, Comprobante.Serie, Comprobante.Numero, Comprobante.EmpresaRUC)
                'Firmar
                FirmarXML(archXML, Ruta_Certificado, Password_Certificado)
                'Comprimir en ZIP
                Dim strEnvio As String = Ruta_ENVIOS & Path.GetFileName(archXML).Replace(".xml", ".zip")
                Comprimir(archXML, strEnvio)
                'Enviar a SUNAT
                EnviarDocumento(strEnvio)
                Return 1
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Function GenerarResumenDiario_XML(ByVal Fecha As DateTime, ByVal EmpresaRUC As String, ByVal EmpresaRazonSocial As String,
                                          ByVal dsResumen As System.Data.DataSet, ByVal nIdCorrelativo As Integer) As String
            Dim Resumen As Resumenes.SummaryDocumentsType = New Resumenes.SummaryDocumentsType()
            Dim numTicket As String = ""

            Try

                Resumen.Sac = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1"
                Resumen.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
                Resumen.Ds = "http://www.w3.org/2000/09/xmldsig#"
                Resumen.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
                Resumen.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"

                If dsResumen.Tables(0).Rows.Count > 0 Then
                    Dim ublExtensiones As Resumenes.UBLExtensionType() = New Resumenes.UBLExtensionType(4) {}
                    Dim ublExtension As Resumenes.UBLExtensionType = New Resumenes.UBLExtensionType()
                    ublExtensiones(0) = ublExtension
                    Resumen.UBLExtensions = ublExtensiones
                    Resumen.UBLVersionID = New Resumenes.UBLVersionIDType()
                    Resumen.UBLVersionID.Value = "2.0"
                    Resumen.CustomizationID = New Resumenes.CustomizationIDType()
                    Resumen.CustomizationID.Value = "1.1"
                    Resumen.ID = New Resumenes.IDType()
                    Resumen.ID.Value = "RC-" & Fecha.ToString("yyyyMMdd") & "-001"
                    Dim FechaEmision As Resumenes.ReferenceDateType = New Resumenes.ReferenceDateType()
                    FechaEmision.Value = Convert.ToDateTime(dsResumen.Tables(0).Rows(0)(5))
                    Resumen.ReferenceDate = FechaEmision
                    Resumen.IssueDate = New Resumenes.IssueDateType()
                    Dim fechaGeneracion As DateTime = Fecha.Date
                    Resumen.IssueDate.Value = Convert.ToDateTime(fechaGeneracion)
                    Dim Firma As Resumenes.SignatureType = New Resumenes.SignatureType()
                    Dim Firmas As Resumenes.SignatureType() = New Resumenes.SignatureType(1) {}
                    Dim partySign As Resumenes.PartyType = New Resumenes.PartyType()
                    Dim partyIdentificacion As Resumenes.PartyIdentificationType = New Resumenes.PartyIdentificationType()
                    Dim partyIdentificacions As Resumenes.PartyIdentificationType() = New Resumenes.PartyIdentificationType(1) {}
                    Dim idFirma As Resumenes.IDType = New Resumenes.IDType()
                    idFirma.Value = EmpresaRUC
                    Firma.ID = idFirma
                    partyIdentificacion.ID = idFirma
                    partyIdentificacions(0) = partyIdentificacion
                    partySign.PartyIdentification = partyIdentificacions
                    Firma.SignatoryParty = partySign

                    Dim Nota As Resumenes.NoteType = New Resumenes.NoteType()
                    Nota.Value = "Elaborado por Sistema de Emision Electronica NET SOLUTION DEVELOPER "
                    Firma.Note = Nota

                    Dim _partyName As Resumenes.PartyNameType = New Resumenes.PartyNameType()
                    Dim _partyNames As Resumenes.PartyNameType() = New Resumenes.PartyNameType(1) {}
                    Dim RazonSocialFirma As Resumenes.NameType1 = New Resumenes.NameType1()
                    RazonSocialFirma.Value = EmpresaRazonSocial
                    _partyName.Name = RazonSocialFirma
                    _partyNames(0) = _partyName
                    partySign.PartyName = _partyNames

                    Dim attachType As Resumenes.AttachmentType = New Resumenes.AttachmentType()
                    Dim externaReferencia As Resumenes.ExternalReferenceType = New Resumenes.ExternalReferenceType()
                    Dim uri As Resumenes.URIType = New Resumenes.URIType()
                    uri.Value = "SIGN"
                    externaReferencia.URI = uri
                    Firma.DigitalSignatureAttachment = attachType
                    attachType.ExternalReference = externaReferencia
                    Firma.DigitalSignatureAttachment = attachType
                    Firmas(0) = Firma
                    Resumen.Signature = Firmas

                    Dim empresa As Resumenes.SupplierPartyType = New Resumenes.SupplierPartyType()
                    Dim party As Resumenes.PartyType = New Resumenes.PartyType()
                    Dim TipoDocumentoEmisor As Resumenes.AdditionalAccountIDType = New Resumenes.AdditionalAccountIDType()
                    Dim TipoDocumentoEmisors As Resumenes.AdditionalAccountIDType() = New Resumenes.AdditionalAccountIDType(1) {}
                    TipoDocumentoEmisors(0) = TipoDocumentoEmisor
                    TipoDocumentoEmisor.Value = "6"
                    empresa.AdditionalAccountID = TipoDocumentoEmisors
                    Dim RUCEmisor As Resumenes.CustomerAssignedAccountIDType = New Resumenes.CustomerAssignedAccountIDType()
                    RUCEmisor.Value = EmpresaRUC
                    empresa.CustomerAssignedAccountID = RUCEmisor

                    Dim parteLegalEntity As Resumenes.PartyLegalEntityType = New Resumenes.PartyLegalEntityType()
                    Dim parteLegalEntitys As Resumenes.PartyLegalEntityType() = New Resumenes.PartyLegalEntityType(1) {}
                    Dim registerNameEmisor As Resumenes.RegistrationNameType = New Resumenes.RegistrationNameType()

                    registerNameEmisor.Value = EmpresaRazonSocial
                    parteLegalEntity.RegistrationName = registerNameEmisor
                    parteLegalEntitys(0) = parteLegalEntity
                    party.PartyLegalEntity = parteLegalEntitys

                    empresa.Party = party
                    Resumen.AccountingSupplierParty = empresa

                    Dim items As Resumenes.SummaryDocumentsLineType() = New Resumenes.SummaryDocumentsLineType(99) {}
                    Dim iditem As Integer = 1
                    Dim TotalesTributos As Resumenes.TaxTotalType() = New Resumenes.TaxTotalType(99) {}

                    For Each reg As System.Data.DataRow In dsResumen.Tables(0).Rows

                        Dim item As Resumenes.SummaryDocumentsLineType = New Resumenes.SummaryDocumentsLineType()
                        Dim numeroItem As Resumenes.LineIDType = New Resumenes.LineIDType()
                        numeroItem.Value = iditem.ToString()
                        item.LineID = numeroItem
                        Dim TipoDocumento As Resumenes.DocumentTypeCodeType = New Resumenes.DocumentTypeCodeType()
                        TipoDocumento.Value = "03"  'reg("IDTIPOCOMP").ToString()
                        item.DocumentTypeCode = TipoDocumento
                        Dim NumDocumento As Resumenes.IDType = New Resumenes.IDType()
                        NumDocumento.Value = reg(3) & "-" & reg(4)   'reg("NumeroComprobante").ToString()
                        item.ID = NumDocumento

                        Dim Cliente As Resumenes.CustomerPartyType = New Resumenes.CustomerPartyType()
                        Dim NumeroDocumento As Resumenes.CustomerAssignedAccountIDType = New Resumenes.CustomerAssignedAccountIDType()
                        NumeroDocumento.Value = reg(6)  'reg("NumDoc").ToString()
                        Cliente.CustomerAssignedAccountID = NumeroDocumento
                        Dim TipoDocumentoCliente As Resumenes.AdditionalAccountIDType = New Resumenes.AdditionalAccountIDType()
                        Dim TipoDocumentoClientes As Resumenes.AdditionalAccountIDType() = New Resumenes.AdditionalAccountIDType(1) {}
                        TipoDocumentoClientes(0) = TipoDocumentoCliente
                        TipoDocumentoCliente.Value = reg(12)
                        Cliente.AdditionalAccountID = TipoDocumentoClientes
                        item.AccountingCustomerParty = Cliente
                        Dim Estado As Resumenes.StatusType = New Resumenes.StatusType()
                        Dim condicion As Resumenes.ConditionCodeType = New Resumenes.ConditionCodeType()
                        condicion.Value = "1" 'reg("Adicionar").ToString()
                        Estado.ConditionCode = condicion
                        item.Status = Estado
                        Dim Total As Resumenes.AmountType1 = New Resumenes.AmountType1()

                        If reg(13).ToString() = "PEN" Then
                            Total.currencyID = CurrencyCodeContentType.PEN
                        ElseIf reg(13).ToString() = "USD" Then
                            Total.currencyID = CurrencyCodeContentType.USD
                        End If

                        Total.Value = Convert.ToDecimal(reg(8))
                        item.TotalAmount = Total
                        Dim PagoSubtotal As Resumenes.PaymentType = New Resumenes.PaymentType()
                        Dim PagoSubtotals As Resumenes.PaymentType() = New Resumenes.PaymentType(1) {}
                        Dim SubTotal As Resumenes.PaidAmountType = New Resumenes.PaidAmountType()
                        Dim TipoImporteTotal As Resumenes.InstructionIDType = New Resumenes.InstructionIDType()
                        TipoImporteTotal.Value = "01"

                        If reg(13).ToString() = "PEN" Then
                            Total.currencyID = CurrencyCodeContentType.PEN
                        ElseIf reg(13).ToString() = "USD" Then
                            Total.currencyID = CurrencyCodeContentType.USD
                        End If

                        SubTotal.Value = Convert.ToDecimal(reg(8))
                        Dim Tipo As Resumenes.InstructionIDType = New Resumenes.InstructionIDType()
                        Tipo.Value = "01"
                        PagoSubtotal.PaidAmount = SubTotal
                        PagoSubtotals(0) = PagoSubtotal
                        PagoSubtotal.InstructionID = Tipo


                        Dim Totals_ISCItems As Resumenes.TaxTotalType() = New Resumenes.TaxTotalType(1) {}
                        Dim Total_ISCItem As Resumenes.TaxTotalType = New Resumenes.TaxTotalType()
                        Dim Total_ItemISC As Resumenes.TaxAmountType = New Resumenes.TaxAmountType()
                        'Total_ItemISC.Value = Convert.ToDecimal(reg("TOT_ISC"))

                        'If reg("IDMONEDA").ToString() = "PEN" Then
                        '    Total_ItemISC.currencyID = CurrencyCodeContentType.PEN
                        'ElseIf reg("IDMONEDA").ToString() = "USD" Then
                        '    Total_ItemISC.currencyID = CurrencyCodeContentType.USD
                        'End If

                        'Total_ISCItem.TaxAmount = Total_ItemISC
                        'Totals_ISCItems(0) = Total_ISCItem
                        'Dim Category_ISCItem As Resumenes.TaxCategoryType = New Resumenes.TaxCategoryType()
                        'Dim TaxScheme_ISCItem As Resumenes.TaxSchemeType = New Resumenes.TaxSchemeType()
                        'Dim id_ISCitem As Resumenes.IDType = New Resumenes.IDType()
                        'id_ISCitem.Value = "2000"   '''''''''''''''''''''''
                        'TaxScheme_ISCItem.ID = id_ISCitem
                        'Dim nombreImpto_ISCItem As Resumenes.NameType1 = New Resumenes.NameType1()
                        'nombreImpto_ISCItem.Value = "ISC"
                        'TaxScheme_ISCItem.Name = nombreImpto_ISCItem
                        'Dim nombreImpto_ISCItemInter As Resumenes.TaxTypeCodeType = New Resumenes.TaxTypeCodeType()
                        'nombreImpto_ISCItemInter.Value = "EXC"
                        'TaxScheme_ISCItem.TaxTypeCode = nombreImpto_ISCItemInter
                        'Category_ISCItem.TaxScheme = TaxScheme_ISCItem
                        'Dim subtotal_ISCs As Resumenes.TaxSubtotalType() = New Resumenes.TaxSubtotalType(1) {}
                        'Dim subtotal_ISC As Resumenes.TaxSubtotalType = New Resumenes.TaxSubtotalType()
                        'subtotal_ISC.TaxCategory = Category_ISCItem
                        'subtotal_ISC.TaxAmount = Total_ItemISC
                        'subtotal_ISCs(0) = subtotal_ISC
                        'Total_ISCItem.TaxSubtotal = subtotal_ISCs
                        'TotalesTributos(0) = Total_ISCItem

                        '*************************************************************INICIA TRUBUTO*************************************************************
                        Dim Totals_IGVItems As Resumenes.TaxTotalType() = New Resumenes.TaxTotalType(1) {}
                        Dim Total_IGVItem As Resumenes.TaxTotalType = New Resumenes.TaxTotalType()
                        Dim Total_ItemIGV As Resumenes.TaxAmountType = New Resumenes.TaxAmountType()
                        Total_ItemIGV.currencyID = CurrencyCodeContentType.PEN
                        Total_ItemIGV.Value = "0"   'Convert.ToDecimal(reg("TOT_IGV"))

                        If reg(13).ToString() = "PEN" Then
                            Total.currencyID = CurrencyCodeContentType.PEN
                        ElseIf reg(13).ToString() = "USD" Then
                            Total.currencyID = CurrencyCodeContentType.USD
                        End If

                        Total_IGVItem.TaxAmount = Total_ItemIGV
                        Totals_IGVItems(0) = Total_IGVItem
                        Dim Category_IGVItem As Resumenes.TaxCategoryType = New Resumenes.TaxCategoryType()
                        Dim TaxScheme_IGVItem As Resumenes.TaxSchemeType = New Resumenes.TaxSchemeType()
                        Dim id_IGVitem As Resumenes.IDType = New Resumenes.IDType()
                        id_IGVitem.Value = "1000"
                        TaxScheme_IGVItem.ID = id_IGVitem
                        Dim nombreImpto_IGVItem As Resumenes.NameType1 = New Resumenes.NameType1()
                        nombreImpto_IGVItem.Value = "IGV"
                        TaxScheme_IGVItem.Name = nombreImpto_IGVItem
                        Dim nombreImpto_IGVItemInter As Resumenes.TaxTypeCodeType = New Resumenes.TaxTypeCodeType()
                        nombreImpto_IGVItemInter.Value = "VAT"
                        TaxScheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter
                        Category_IGVItem.TaxScheme = TaxScheme_IGVItem
                        Dim subtotal_IGVs As Resumenes.TaxSubtotalType() = New Resumenes.TaxSubtotalType(1) {}
                        Dim subtotal_IGV As Resumenes.TaxSubtotalType = New Resumenes.TaxSubtotalType()
                        subtotal_IGV.TaxCategory = Category_IGVItem
                        subtotal_IGV.TaxAmount = Total_ItemIGV
                        subtotal_IGVs(0) = subtotal_IGV
                        Total_IGVItem.TaxSubtotal = subtotal_IGVs
                        TotalesTributos(1) = Total_IGVItem
                        '*************************************************************FIN TRUBUTO*************************************************************


                        Dim Totals_OtrosItems As Resumenes.TaxTotalType() = New Resumenes.TaxTotalType(1) {}
                        Dim Total_OtrosItem As Resumenes.TaxTotalType = New Resumenes.TaxTotalType()
                        Dim Total_ItemOtros As Resumenes.TaxAmountType = New Resumenes.TaxAmountType()
                        Total_ItemOtros.currencyID = CurrencyCodeContentType.PEN
                        Total_ItemOtros.Value = 0 'Convert.ToDecimal(reg(8))

                        If reg(13).ToString() = "PEN" Then
                            Total.currencyID = CurrencyCodeContentType.PEN
                        ElseIf reg(13).ToString() = "USD" Then
                            Total.currencyID = CurrencyCodeContentType.USD
                        End If

                        Total_OtrosItem.TaxAmount = Total_ItemOtros
                        Totals_OtrosItems(0) = Total_OtrosItem
                        Dim subtotal_Otross As Resumenes.TaxSubtotalType() = New Resumenes.TaxSubtotalType(1) {}
                        Dim subtotal_Otros As Resumenes.TaxSubtotalType = New Resumenes.TaxSubtotalType()
                        Dim Total_ItemOtrosSub As Resumenes.TaxAmountType = New Resumenes.TaxAmountType()
                        Total_ItemOtrosSub.currencyID = CurrencyCodeContentType.PEN

                        If reg(13).ToString() = "PEN" Then
                            Total.currencyID = CurrencyCodeContentType.PEN
                        ElseIf reg(13).ToString() = "USD" Then
                            Total.currencyID = CurrencyCodeContentType.USD
                        End If

                        subtotal_Otros.TaxAmount = Total_ItemOtrosSub
                        Dim Category_OtrosItem As Resumenes.TaxCategoryType = New Resumenes.TaxCategoryType()
                        Dim TaxScheme_OtrosItem As Resumenes.TaxSchemeType = New Resumenes.TaxSchemeType()
                        Dim id_Otrositem As Resumenes.IDType = New Resumenes.IDType()
                        id_Otrositem.Value = "9999"
                        TaxScheme_OtrosItem.ID = id_Otrositem
                        Dim nombreImpto_OtrosItem As Resumenes.NameType1 = New Resumenes.NameType1()
                        nombreImpto_OtrosItem.Value = "OTROS"
                        TaxScheme_OtrosItem.Name = nombreImpto_OtrosItem
                        Dim nombreImpto_OtrosItemInter As Resumenes.TaxTypeCodeType = New Resumenes.TaxTypeCodeType()
                        nombreImpto_OtrosItemInter.Value = "OTH"
                        TaxScheme_OtrosItem.TaxTypeCode = nombreImpto_OtrosItemInter
                        Category_OtrosItem.TaxScheme = TaxScheme_OtrosItem
                        subtotal_Otros.TaxCategory = Category_OtrosItem
                        subtotal_Otros.TaxAmount = Total_ItemOtrosSub
                        subtotal_Otross(0) = subtotal_Otros

                        Total_OtrosItem.TaxSubtotal = subtotal_Otross
                        TotalesTributos(2) = Total_OtrosItem
                        item.TaxTotal = TotalesTributos

                        items(iditem) = item
                        iditem += 1
                    Next

                    Resumen.SummaryDocumentsLine = items
                    Dim archXML As String = GenerarResumenDiario(Resumen, Fecha, "RC", nIdCorrelativo, EmpresaRUC)
                    FirmarXML(archXML, Ruta_Certificado, Password_Certificado)
                    Dim strEnvio As String = Ruta_ENVIOS & Path.GetFileName(archXML).Replace(".xml", ".zip")
                    Comprimir(archXML, strEnvio)
                    Dim servicio As New CapaSUNAT.Servicios.SUNAT_UTIL
                    numTicket = servicio.EnviarResumen(strEnvio)
                End If

                Return numTicket
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Function GenerarComunicacionBaja_XML(ByVal Fecha As DateTime, ByVal EmpresaRUC As String, ByVal EmpresaRazonSocial As String, ByVal TipoDocumento As String, ByVal SerieDocumento As String, ByVal NumeroDocumento As String, ByVal MotivoBaja As String) As String
            Dim Baja As Bajas.VoidedDocumentsType = New Bajas.VoidedDocumentsType()
            Dim numTicket As String = ""

            Try
                Baja.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                Baja.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
                Baja.Ds = "http://www.w3.org/2000/09/xmldsig#"
                Baja.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
                Baja.Sac = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1"
                Dim ublExtensiones As Bajas.UBLExtensionType() = New Bajas.UBLExtensionType(4) {}
                Dim ublExtension As Bajas.UBLExtensionType = New Bajas.UBLExtensionType()
                ublExtensiones(0) = ublExtension
                Baja.UBLExtensions = ublExtensiones
                Baja.UBLVersionID = New Bajas.UBLVersionIDType()
                Baja.UBLVersionID.Value = "2.0"
                Baja.CustomizationID = New Bajas.CustomizationIDType()
                Baja.CustomizationID.Value = "1.0"
                Baja.ID = New Bajas.IDType()
                Baja.ID.Value = "RA-" & DateTime.Now.ToString("yyyyMMdd") & "-001"
                Dim FechaEmision As Bajas.ReferenceDateType = New Bajas.ReferenceDateType()
                FechaEmision.Value = Fecha
                Baja.ReferenceDate = FechaEmision
                Baja.IssueDate = New Bajas.IssueDateType()
                Dim fechaGeneracion As DateTime = DateTime.Now.Date
                Baja.IssueDate.Value = Convert.ToDateTime(fechaGeneracion)
                Dim Firma As Bajas.SignatureType = New Bajas.SignatureType()
                Dim Firmas As Bajas.SignatureType() = New Bajas.SignatureType(1) {}
                Dim partySign As Bajas.PartyType = New Bajas.PartyType()
                Dim partyIdentificacion As Bajas.PartyIdentificationType = New Bajas.PartyIdentificationType()
                Dim partyIdentificacions As Bajas.PartyIdentificationType() = New Bajas.PartyIdentificationType(1) {}
                Dim idFirma As Bajas.IDType = New Bajas.IDType()
                idFirma.Value = EmpresaRUC
                Firma.ID = idFirma
                partyIdentificacion.ID = idFirma
                partyIdentificacions(0) = partyIdentificacion
                partySign.PartyIdentification = partyIdentificacions
                Firma.SignatoryParty = partySign
                Dim Nota As Bajas.NoteType = New Bajas.NoteType()
                Nota.Value = "Elaborado por Sistema de Emision Electronica NET SOLUTION DEVELOPER "
                Firma.Note = Nota
                Dim partyName As Bajas.PartyNameType = New Bajas.PartyNameType()
                Dim partyNames As Bajas.PartyNameType() = New Bajas.PartyNameType(1) {}
                Dim RazonSocialFirma As Bajas.NameType1 = New Bajas.NameType1()
                RazonSocialFirma.Value = EmpresaRazonSocial
                partyName.Name = RazonSocialFirma
                partyNames(0) = partyName
                partySign.PartyName = partyNames
                Dim attachType As Bajas.AttachmentType = New Bajas.AttachmentType()
                Dim externaReferencia As Bajas.ExternalReferenceType = New Bajas.ExternalReferenceType()
                Dim uri As Bajas.URIType = New Bajas.URIType()
                uri.Value = "SIGN"
                externaReferencia.URI = uri
                Firma.DigitalSignatureAttachment = attachType
                attachType.ExternalReference = externaReferencia
                Firma.DigitalSignatureAttachment = attachType
                Firmas(0) = Firma
                Baja.Signature = Firmas
                Dim empresa As Bajas.SupplierPartyType = New Bajas.SupplierPartyType()
                Dim party As Bajas.PartyType = New Bajas.PartyType()
                Dim TipoDocumentoEmisor As Bajas.AdditionalAccountIDType = New Bajas.AdditionalAccountIDType()
                Dim TipoDocumentoEmisors As Bajas.AdditionalAccountIDType() = New Bajas.AdditionalAccountIDType(1) {}
                TipoDocumentoEmisors(0) = TipoDocumentoEmisor
                TipoDocumentoEmisor.Value = "6"
                empresa.AdditionalAccountID = TipoDocumentoEmisors
                Dim RUCEmisor As Bajas.CustomerAssignedAccountIDType = New Bajas.CustomerAssignedAccountIDType()
                RUCEmisor.Value = EmpresaRUC
                empresa.CustomerAssignedAccountID = RUCEmisor
                Dim parteLegalEntity As Bajas.PartyLegalEntityType = New Bajas.PartyLegalEntityType()
                Dim parteLegalEntitys As Bajas.PartyLegalEntityType() = New Bajas.PartyLegalEntityType(1) {}
                Dim registerNameEmisor As Bajas.RegistrationNameType = New Bajas.RegistrationNameType()
                registerNameEmisor.Value = EmpresaRazonSocial
                parteLegalEntity.RegistrationName = registerNameEmisor
                parteLegalEntitys(0) = parteLegalEntity
                party.PartyLegalEntity = parteLegalEntitys
                empresa.Party = party
                Baja.AccountingSupplierParty = empresa
                Dim ItemBaja As Bajas.VoidedDocumentsLineType = New Bajas.VoidedDocumentsLineType()
                Dim ItemsBajas As Bajas.VoidedDocumentsLineType() = New Bajas.VoidedDocumentsLineType(1) {}
                Dim numeroItem As Bajas.LineIDType = New Bajas.LineIDType()
                numeroItem.Value = "1"
                ItemBaja.LineID = numeroItem
                Dim Tipo_Documento As Bajas.DocumentTypeCodeType = New Bajas.DocumentTypeCodeType()
                Tipo_Documento.Value = TipoDocumento
                ItemBaja.DocumentTypeCode = Tipo_Documento
                Dim Serie_Documento As Bajas.IdentifierType = New Bajas.SerialIDType()
                Serie_Documento.Value = SerieDocumento
                ItemBaja.DocumentSerialID = Serie_Documento
                Dim Numero_Documento As Bajas.IdentifierType = New Bajas.SerialIDType()
                Numero_Documento.Value = NumeroDocumento
                ItemBaja.DocumentNumberID = Numero_Documento
                Dim Motivo_Baja As Bajas.TextType = New Bajas.TextType()
                Motivo_Baja.Value = MotivoBaja
                ItemBaja.VoidReasonDescription = Motivo_Baja
                ItemsBajas(0) = ItemBaja
                Baja.VoidedDocumentsLine = ItemsBajas

                'Enviar a SUNAT
                Dim archXML As String = GenerarComunicacionBaja(Baja, Fecha, "RA", 1, EmpresaRUC)
                FirmarXML(archXML, Ruta_Certificado, Password_Certificado)
                Dim strEnvio As String = Ruta_ENVIOS & Path.GetFileName(archXML).Replace(".xml", ".zip")
                Comprimir(archXML, strEnvio)
                Dim servicio As New CapaSUNAT.Servicios.SUNAT_UTIL
                numTicket = servicio.EnviarResumen(strEnvio)
                Return numTicket
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Private Function GenerarComprobante(ByVal Factura As InvoiceType, ByVal RUC As String, ByVal TipoDocumento As String, ByVal Serie As String, ByVal Numero As String, ByVal RUCEmpresa As String) As String
            Dim setting As XmlWriterSettings = New XmlWriterSettings()
            setting.Indent = True
            setting.IndentChars = vbTab
            Dim ArchivoXML As String = RUC & "-" & TipoDocumento & "-" & Serie & "-" & Numero
            Dim rutaXML As String = String.Format("{0}{1}.xml", Ruta_XML, ArchivoXML)

            Using writer As XmlWriter = XmlWriter.Create(rutaXML, setting)
                Dim typeToSerialize As Type = GetType(InvoiceType)
                Dim xs As XmlSerializer = New XmlSerializer(typeToSerialize)
                xs.Serialize(writer, Factura)
                Return rutaXML
            End Using
        End Function

        Private Function GenerarComprobante(ByVal Nota As CreditNoteType, ByVal RUC As String, ByVal TipoDocumento As String, ByVal Serie As String, ByVal Numero As String, ByVal RUCEmpresa As String) As String
            Dim setting As XmlWriterSettings = New XmlWriterSettings()
            setting.Indent = True
            setting.IndentChars = vbTab
            Dim ArchivoXML As String = RUC & "-" & TipoDocumento & "-" & Serie & "-" & Numero
            Dim rutaXML As String = String.Format("{0}{1}.xml", Ruta_XML, ArchivoXML)

            Using writer As XmlWriter = XmlWriter.Create(rutaXML, setting)
                Dim typeToSerialize As Type = GetType(CreditNoteType)
                Dim xs As XmlSerializer = New XmlSerializer(typeToSerialize)
                xs.Serialize(writer, Nota)
                Return rutaXML
            End Using
        End Function

        Private Function GenerarComprobante(ByVal Nota As DebitNoteType, ByVal RUC As String, ByVal TipoDocumento As String, ByVal Serie As String, ByVal Numero As String, ByVal RUCEmpresa As String) As String
            Dim setting As XmlWriterSettings = New XmlWriterSettings()
            setting.Indent = True
            setting.IndentChars = vbTab
            Dim ArchivoXML As String = RUC & "-" & TipoDocumento & "-" & Serie & "-" & Numero
            Dim rutaXML As String = String.Format("{0}{1}.xml", Ruta_XML, ArchivoXML)

            Using writer As XmlWriter = XmlWriter.Create(rutaXML, setting)
                Dim typeToSerialize As Type = GetType(DebitNoteType)
                Dim xs As XmlSerializer = New XmlSerializer(typeToSerialize)
                xs.Serialize(writer, Nota)
                Return rutaXML
            End Using
        End Function

        Private Function GenerarResumenDiario(ByVal ResumenDiario As SummaryDocumentsType, ByVal Fecha As DateTime, ByVal TipoDocumento As String, ByVal correlativo As Integer, ByVal RUCEmpresa As String) As String
            Dim setting As XmlWriterSettings = New XmlWriterSettings()
            setting.Indent = True
            setting.IndentChars = vbTab
            Dim ArchivoXML As String = RUCEmpresa & "-" & TipoDocumento & "-" & Fecha.ToString("yyyyMMdd") & "-" & correlativo.ToString().PadLeft(3, "0"c)
            Dim rutaXML As String = String.Format("{0}{1}.xml", Ruta_XML, ArchivoXML)

            Using writer As XmlWriter = XmlWriter.Create(rutaXML, setting)
                Dim typeToSerialize As Type = GetType(SummaryDocumentsType)
                Dim xs As XmlSerializer = New XmlSerializer(typeToSerialize)
                xs.Serialize(writer, ResumenDiario)
                Return rutaXML
            End Using
        End Function

        Private Function GenerarComunicacionBaja(ByVal Baja As Bajas.VoidedDocumentsType, ByVal Fecha As DateTime, ByVal TipoDocumento As String, ByVal correlativo As Integer, ByVal RUCEmpresa As String) As String
            Dim setting As XmlWriterSettings = New XmlWriterSettings()
            setting.Indent = True
            setting.IndentChars = vbTab
            Dim ArchivoXML As String = RUCEmpresa & "-" & TipoDocumento & "-" & DateTime.Now.ToString("yyyyMMdd") & "-" & correlativo.ToString().PadLeft(3, "0"c)
            Dim rutaXML As String = String.Format("{0}{1}.xml", Ruta_XML, ArchivoXML)

            Using writer As XmlWriter = XmlWriter.Create(rutaXML, setting)
                Dim typeToSerialize As Type = GetType(Bajas.VoidedDocumentsType)
                Dim xs As XmlSerializer = New XmlSerializer(typeToSerialize)
                xs.Serialize(writer, Baja)
                Return rutaXML
            End Using
        End Function

        Private Function GenerarGuiaRemitente(ByVal guia As Guias.DespatchAdviceType, ByVal RUC As String) As String
            Dim setting As XmlWriterSettings = New XmlWriterSettings()
            setting.Indent = True
            setting.IndentChars = vbTab
            Dim ArchivoXML As String = RUC & "-09-" & guia.ID.Value
            Dim rutaXML As String = String.Format("{0}{1}.xml", Ruta_XML, ArchivoXML)

            Using writer As XmlWriter = XmlWriter.Create(rutaXML, setting)
                Dim typeToSerialize As Type = GetType(Guias.DespatchAdviceType)
                Dim xs As XmlSerializer = New XmlSerializer(typeToSerialize)
                xs.Serialize(writer, guia)
                Return rutaXML
            End Using
        End Function

        Function FirmarXML(ByVal cRutaArchivo As String, ByVal cCertificado As String, ByVal cClave As String) As String
            Dim _file As String = cRutaArchivo
            Dim text As String = File.ReadAllText(_file)
            text = text.Replace("<ext:UBLExtension />", "<ext:UBLExtension> <ext:ExtensionContent /></ext:UBLExtension>")
            text = text.Replace("xsi:type=", "")
            text = text.Replace("cbc:SerialIDType", "")
            text = text.Replace("""""", "")
            File.WriteAllText(_file, text)
            Dim cRpta As String
            Dim tipo As String = Path.GetFileName(cRutaArchivo)
            Dim local_typoDocumento As String = tipo.Substring(12, 2)
            Dim l_xpath As String = ""
            Dim f_certificat As String = cCertificado
            Dim f_pwd As String = cClave
            Dim xmlFile As String = cRutaArchivo
            Dim MonCertificat As X509Certificate2 = New X509Certificate2(f_certificat, f_pwd)
            Dim xmlDoc As XmlDocument = New XmlDocument()
            xmlDoc.PreserveWhitespace = True
            xmlDoc.Load(xmlFile)
            Dim signedXml As SignedXml = New SignedXml(xmlDoc)
            signedXml.SigningKey = MonCertificat.PrivateKey
            Dim KeyInfo As KeyInfo = New KeyInfo()
            Dim Reference As Reference = New Reference()
            Reference.Uri = ""
            Reference.AddTransform(New XmlDsigEnvelopedSignatureTransform())
            signedXml.AddReference(Reference)
            Dim X509Chain As X509Chain = New X509Chain()
            X509Chain.Build(MonCertificat)
            Dim local_element As X509ChainElement = X509Chain.ChainElements(0)
            Dim x509Data As KeyInfoX509Data = New KeyInfoX509Data(local_element.Certificate)
            Dim subjectName As String = local_element.Certificate.Subject
            x509Data.AddSubjectName(subjectName)
            KeyInfo.AddClause(x509Data)
            signedXml.KeyInfo = KeyInfo
            signedXml.ComputeSignature()
            Dim signature As XmlElement = signedXml.GetXml()
            signature.Prefix = "ds"
            signedXml.ComputeSignature()

            For Each node As XmlNode In signature.SelectNodes("descendant-or-self::*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#']")

                If node.LocalName = "Signature" Then
                    Dim newAttribute As XmlAttribute = xmlDoc.CreateAttribute("Id")
                    newAttribute.Value = "SignSUNAT"
                    node.Attributes.Append(newAttribute)
                End If
            Next

            Dim nsMgr As XmlNamespaceManager
            nsMgr = New XmlNamespaceManager(xmlDoc.NameTable)
            nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")
            nsMgr.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2")
            nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance")

            Select Case local_typoDocumento
                Case "01", "03"
                    nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")
                    l_xpath = "/tns:Invoice/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent"
                    Exit Select
                Case "07"
                    nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2")
                    l_xpath = "/tns:CreditNote/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent"
                    Exit Select
                Case "08"
                    nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2")
                    l_xpath = "/tns:DebitNote/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent"
                    Exit Select
                Case "RA"
                    nsMgr.AddNamespace("tns", "urn:sunat:names:specification:ubl:peru:schema:xsd:VoidedDocuments-1")
                    l_xpath = "/tns:VoidedDocuments/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent"
                    Exit Select
                Case "RC"
                    nsMgr.AddNamespace("tns", "urn:sunat:names:specification:ubl:peru:schema:xsd:SummaryDocuments-1")
                    l_xpath = "/tns:SummaryDocuments/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent"
                    Exit Select
                Case Else
                    nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2")
                    l_xpath = "/tns:DespatchAdvice/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent"
                    Exit Select
            End Select

            nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
            nsMgr.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
            nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
            nsMgr.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
            nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
            nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")
            xmlDoc.SelectSingleNode(l_xpath, nsMgr).AppendChild(xmlDoc.ImportNode(signature, True))
            xmlDoc.Save(xmlFile)
            Dim nodeList As XmlNodeList = xmlDoc.GetElementsByTagName("ds:Signature")

            If (nodeList.Count <> 1) Then
                cRpta = "SE PRODUJO ERROR EN LA FIRMA"
            End If

            signedXml.LoadXml(CType(nodeList(0), XmlElement))

            If (signedXml.CheckSignature()) = False Then
                cRpta = "SE PRODUJO UN ERROR EN LA FIRMA  DE DOCUMENTO"
            Else
                cRpta = "OK"
            End If

            Return cRpta
        End Function

        Function Comprimir(ByVal cnombrearchivoOrigen As String, ByVal cnombreArchivoDestino As String) As String
            Dim zip As Ionic.Zip.ZipFile = New Ionic.Zip.ZipFile()
            zip.AddFile(cnombrearchivoOrigen, "")
            zip.Save(cnombreArchivoDestino)
            Dim rpta As String = "OK"
            Return rpta
        End Function

        Function EnviarDocumento(ByVal pArchivo As String) As String
            Dim strRetorno As String = ""
            Dim filezip As String = pArchivo
            Dim filepath As String = filezip
            Dim bitArray As Byte() = File.ReadAllBytes(filepath)

            Try

                Using servicio As ServiceSunat.billServiceClient = New billServiceClient()
                    ServicePointManager.UseNagleAlgorithm = True
                    ServicePointManager.Expect100Continue = False
                    ServicePointManager.CheckCertificateRevocationList = True
                    servicio.Open()
                    filezip = Path.GetFileName(filezip)
                    Dim returnByte As Byte() = servicio.sendBill(filezip, bitArray, "")
                    servicio.Close()
                    filezip = Path.GetFileName(filezip)
                    Dim fs As FileStream = New FileStream(Ruta_CDRS & "R-" & filezip, FileMode.Create)
                    fs.Write(returnByte, 0, returnByte.Length)
                    fs.Close()
                    strRetorno = "Archivo generado con exito"
                End Using

            Catch ex As System.ServiceModel.FaultException
                strRetorno = ex.Code.Name
                Throw
            End Try

            Return strRetorno
        End Function

        Function EnviarDocumentoGuiaRemision(ByVal pArchivo As String) As String
            Dim strRetorno As String = ""
            Dim filezip As String = pArchivo
            Dim filepath As String = filezip
            Dim bitArray As Byte() = File.ReadAllBytes(filepath)

            Try

                Using servicio As ServiceGuia.billServiceClient = New ServiceGuia.billServiceClient()
                    ServicePointManager.UseNagleAlgorithm = True
                    ServicePointManager.Expect100Continue = False
                    ServicePointManager.CheckCertificateRevocationList = True
                    servicio.Open()
                    filezip = Path.GetFileName(filezip)
                    Dim returnByte As Byte() = servicio.sendBill(filezip, bitArray, "")
                    servicio.Close()
                    filezip = Path.GetFileName(filezip)
                    Dim fs As FileStream = New FileStream(Ruta_CDRS & "R-" & filezip, FileMode.Create)
                    fs.Write(returnByte, 0, returnByte.Length)
                    fs.Close()
                    strRetorno = "Archivo generado con exito"
                End Using

            Catch ex As System.ServiceModel.FaultException
                strRetorno = ex.Code.Name
                Throw
            End Try

            Return strRetorno
        End Function

        Function EnviarResumen(ByVal pArchivo As String) As String
            Dim strRetorno As String = ""
            Dim filezip As String = pArchivo
            Dim filepath As String = filezip
            Dim bitArray As Byte() = File.ReadAllBytes(filepath)

            Try

                Using servicio As ServiceSunat.billServiceClient = New billServiceClient()
                    ServicePointManager.UseNagleAlgorithm = True
                    ServicePointManager.Expect100Continue = False
                    ServicePointManager.CheckCertificateRevocationList = True
                    servicio.Open()
                    filezip = Path.GetFileName(filezip)
                    Dim ticket As String = servicio.sendSummary(filezip, bitArray, "")
                    servicio.Close()
                    strRetorno = ticket
                End Using

            Catch ex As System.ServiceModel.FaultException
                strRetorno = ex.Code.Name
                Throw
            End Try

            Return strRetorno
        End Function

        Function ObtenerEstado(ByVal ticket As String) As String
            Dim strRetorno As String = ""

            Try

                Using servicio As ServiceSunat.billServiceClient = New billServiceClient()
                    servicio.Open()
                    Dim returnstring As statusResponse = servicio.getStatus(ticket)
                    strRetorno = returnstring.statusCode
                    servicio.Close()
                    Return strRetorno
                End Using

            Catch ex As System.ServiceModel.FaultException
                Throw New Exception(ex.Code.Name)
            End Try
        End Function

        Function GenerarComprobanteND_XML(ByVal Comprobante As Cabecera) As Integer
            Dim Factura As CapaSUNAT.Modelos.DebitNoteType = New CapaSUNAT.Modelos.DebitNoteType()

            Try
                Factura.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                Factura.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
                Factura.Ccts = "urn:un:unece:uncefact:documentation:2"
                Factura.Ds = "http://www.w3.org/2000/09/xmldsig#"
                Factura.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
                Factura.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2"
                Factura.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2"
                Dim ublExtensiones As CapaSUNAT.Modelos.UBLExtensionType() = New CapaSUNAT.Modelos.UBLExtensionType(4) {}
                Dim ublExtension As CapaSUNAT.Modelos.UBLExtensionType = New CapaSUNAT.Modelos.UBLExtensionType()
                ublExtensiones(0) = ublExtension
                Factura.UBLExtensions = ublExtensiones
                Factura.UBLVersionID = New CapaSUNAT.Modelos.UBLVersionIDType()
                Factura.UBLVersionID.Value = "2.1"
                Factura.CustomizationID = New CapaSUNAT.Modelos.CustomizationIDType()
                Factura.CustomizationID.Value = "2.0"


                Factura.ID = New CapaSUNAT.Modelos.IDType() 'Serie - Numero
                Factura.ID.Value = Comprobante.Serie & "-" + Comprobante.Numero

                Factura.IssueDate = New CapaSUNAT.Modelos.IssueDateType() 'Fecha Emision
                Dim fechaemision As String = Convert.ToDateTime(Comprobante.Fechaemision).ToString("dd/MM/yyyy")
                Factura.IssueDate.Value = Convert.ToDateTime(fechaemision)

                Factura.IssueTime = New CapaSUNAT.Modelos.IssueTimeType() 'hora de emision
                Dim hora As String = Convert.ToDateTime(Comprobante.Fechaemision).ToString("HH:mm:ss")
                Factura.IssueTime.Value = hora

                'Moneda
                Dim moneda As CapaSUNAT.Modelos.DocumentCurrencyCodeType = New CapaSUNAT.Modelos.DocumentCurrencyCodeType() With {
                    .listSchemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo51",
                    .listID = "ISO 4217 Alpha",
                    .listName = "Currency",
                    .listAgencyName = "United Nations Economic Commission for Europe",
                    .Value = Comprobante.Idmoneda
                }
                Factura.DocumentCurrencyCode = moneda


                Dim DocumentoRel As CapaSUNAT.Modelos.ResponseType = New CapaSUNAT.Modelos.ResponseType()
                Dim DocumentoRels As CapaSUNAT.Modelos.ResponseType() = New CapaSUNAT.Modelos.ResponseType(1) {}
                Dim NumeroDocRel As CapaSUNAT.Modelos.ReferenceIDType = New CapaSUNAT.Modelos.ReferenceIDType()
                NumeroDocRel.Value = Comprobante.Serie & "-" + Comprobante.Numero

                'Tipo de nota 
                Dim TipoDocRel As CapaSUNAT.Modelos.ResponseCodeType = New CapaSUNAT.Modelos.ResponseCodeType()
                TipoDocRel.Value = Comprobante.Cab_Ref_TipoNotaDebito

                Dim Motivo As CapaSUNAT.Modelos.DescriptionType = New CapaSUNAT.Modelos.DescriptionType()
                Dim Motivos As CapaSUNAT.Modelos.DescriptionType() = New CapaSUNAT.Modelos.DescriptionType(1) {}
                Motivos(0) = Motivo
                Motivo.Value = Comprobante.Cab_Ref_Motivo  'Motivo

                'Motivo de nota
                DocumentoRel.ReferenceID = NumeroDocRel
                DocumentoRel.ResponseCode = TipoDocRel
                DocumentoRel.Description = Motivos
                DocumentoRels(0) = DocumentoRel
                Factura.DiscrepancyResponse = DocumentoRels

                Dim referencias As CapaSUNAT.Modelos.BillingReferenceType() = New CapaSUNAT.Modelos.BillingReferenceType(1) {}
                Dim referencia As CapaSUNAT.Modelos.BillingReferenceType = New CapaSUNAT.Modelos.BillingReferenceType()
                Dim documento As CapaSUNAT.Modelos.DocumentReferenceType = New CapaSUNAT.Modelos.DocumentReferenceType()
                Dim docRela As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                docRela.Value = Comprobante.Cab_Ref_Serie & "-" + Comprobante.Cab_Ref_Numero

                'Documento relacionado
                Dim TipoDocumentoRel As CapaSUNAT.Modelos.DocumentTypeCodeType = New CapaSUNAT.Modelos.DocumentTypeCodeType()
                TipoDocumentoRel.Value = Comprobante.Cab_Ref_TipoDeDocumento
                documento.DocumentTypeCode = TipoDocumentoRel
                documento.ID = docRela
                referencia.InvoiceDocumentReference = documento
                referencias(0) = referencia
                Factura.BillingReference = referencias

                'Firma
                Dim Firma As CapaSUNAT.Modelos.SignatureType = New CapaSUNAT.Modelos.SignatureType()
                Dim Firmas As CapaSUNAT.Modelos.SignatureType() = New CapaSUNAT.Modelos.SignatureType(1) {}
                Dim partySign As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim partyIdentificacion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim partyIdentificacions As CapaSUNAT.Modelos.PartyIdentificationType() = New CapaSUNAT.Modelos.PartyIdentificationType(1) {}
                Dim idFirma As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idFirma.Value = Comprobante.EmpresaRUC
                Firma.ID = idFirma
                partyIdentificacion.ID = idFirma
                partyIdentificacions(0) = partyIdentificacion
                partySign.PartyIdentification = partyIdentificacions
                Firma.SignatoryParty = partySign

                'Datos de la empresa Emisora
                Dim empresa As CapaSUNAT.Modelos.SupplierPartyType = New CapaSUNAT.Modelos.SupplierPartyType()
                Dim party As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim _partyidentificacion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim _partyidentificacions As CapaSUNAT.Modelos.PartyIdentificationType() = New CapaSUNAT.Modelos.PartyIdentificationType(1) {}
                Dim idEmpresa As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()

                idEmpresa.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idEmpresa.schemeName = "Documento de Identidad"
                idEmpresa.schemeID = "6"
                idEmpresa.schemeAgencyName = "PE:SUNAT"
                idEmpresa.Value = Comprobante.EmpresaRUC
                _partyidentificacion.ID = idEmpresa
                _partyidentificacions(0) = _partyidentificacion


                Dim partyname As CapaSUNAT.Modelos.PartyNameType = New CapaSUNAT.Modelos.PartyNameType()
                Dim partynames As List(Of CapaSUNAT.Modelos.PartyNameType) = New List(Of CapaSUNAT.Modelos.PartyNameType)()
                Dim nameEmisor As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                nameEmisor.Value = Comprobante.EmpresaRazonSocial
                partyname.Name = nameEmisor
                partynames.Add(partyname)
                party.PartyName = partynames.ToArray()

                Dim registerNameEmisor As CapaSUNAT.Modelos.RegistrationNameType = New CapaSUNAT.Modelos.RegistrationNameType()
                registerNameEmisor.Value = Comprobante.EmpresaRazonSocial

                Dim compañia As Modelos.CompanyIDType = New Modelos.CompanyIDType()
                compañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                compañia.schemeAgencyName = "PE:SUNAT"
                compañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                compañia.schemeID = "6"
                compañia.Value = Comprobante.EmpresaRUC

                Dim direccion As CapaSUNAT.Modelos.AddressType = New CapaSUNAT.Modelos.AddressType()
                Dim addrestypecode As CapaSUNAT.Modelos.AddressTypeCodeType = New CapaSUNAT.Modelos.AddressTypeCodeType()
                addrestypecode.listName = "Establecimientos anexos"
                addrestypecode.listAgencyName = "PE:SUNAT"
                addrestypecode.Value = "0000"

                Dim taxSchema As Modelos.TaxSchemeType = New Modelos.TaxSchemeType()
                Dim idsupplier As Modelos.IDType = New Modelos.IDType()
                idsupplier.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idsupplier.schemeAgencyName = "PE:SUNAT"
                idsupplier.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idsupplier.schemeID = "6"
                idsupplier.Value = Comprobante.EmpresaRUC
                taxSchema.ID = idsupplier

                Dim partelegals As List(Of Modelos.PartyLegalEntityType) = New List(Of Modelos.PartyLegalEntityType)()
                Dim partelegal As Modelos.PartyLegalEntityType = New Modelos.PartyLegalEntityType()
                Dim registerNamePL As CapaSUNAT.Modelos.RegistrationNameType = New CapaSUNAT.Modelos.RegistrationNameType()
                registerNamePL.Value = Comprobante.EmpresaRazonSocial
                partelegal.RegistrationName = registerNamePL

                Dim direccionPL As Modelos.AddressType = New Modelos.AddressType()
                Dim iddireccionPL As Modelos.IDType = New Modelos.IDType()
                iddireccionPL.schemeAgencyName = "PE:INEI"
                iddireccionPL.schemeName = "Ubigeos"
                iddireccionPL.Value = Comprobante.ID_EmpresaDepartamento + Comprobante.ID_EmpresaProvincia + Comprobante.ID_EmpresaDistrito
                direccionPL.ID = iddireccionPL

                Dim address_TypeCodeType As Modelos.AddressTypeCodeType = New Modelos.AddressTypeCodeType()
                address_TypeCodeType.listName = "Establecimientos anexos"
                address_TypeCodeType.listAgencyName = "PE:SUNAT"
                address_TypeCodeType.Value = "0001"
                direccionPL.AddressTypeCode = address_TypeCodeType

                Dim Departamento As CapaSUNAT.Modelos.CityNameType = New CapaSUNAT.Modelos.CityNameType()
                Departamento.Value = Comprobante.EmpresaDepartamento
                direccionPL.CityName = Departamento

                Dim Provincia As CapaSUNAT.Modelos.CountrySubentityType = New CapaSUNAT.Modelos.CountrySubentityType()
                Provincia.Value = Comprobante.EmpresaProvincia
                direccionPL.CountrySubentity = Provincia

                Dim distrito As CapaSUNAT.Modelos.DistrictType = New CapaSUNAT.Modelos.DistrictType()
                distrito.Value = Comprobante.EmpresaDistrito
                direccionPL.District = distrito

                Dim direcciones As List(Of Modelos.AddressLineType) = New List(Of Modelos.AddressLineType)()
                Dim direccionEmisor As Modelos.AddressLineType = New Modelos.AddressLineType()
                Dim local1 As Modelos.LineType = New Modelos.LineType()
                local1.Value = Comprobante.EmpresaDireccion
                direccionPL.AddressLine = direcciones.ToArray()
                direccionEmisor.Line = local1
                direcciones.Add(direccionEmisor)
                direccionPL.AddressLine = direcciones.ToArray()

                Dim pais As CapaSUNAT.Modelos.CountryType = New CapaSUNAT.Modelos.CountryType()
                Dim codigoPais As CapaSUNAT.Modelos.IdentificationCodeType = New CapaSUNAT.Modelos.IdentificationCodeType()
                codigoPais.listName = "Country"
                codigoPais.listAgencyName = "United Nations Economic Commission for Europe"
                codigoPais.listID = "ISO 3166-1"
                codigoPais.Value = "PE"
                pais.IdentificationCode = codigoPais
                direccionPL.Country = pais
                partelegal.RegistrationAddress = direccionPL
                partelegals.Add(partelegal)

                party.PartyLegalEntity = partelegals.ToArray()
                party.PartyName = partynames.ToArray()
                party.PartyIdentification = _partyidentificacions

                empresa.Party = party
                Factura.AccountingSupplierParty = empresa


                'DATOS DEL CLIENTE
                Dim taxschemeCliente As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                Dim CustomerPartyCliente As CapaSUNAT.Modelos.CustomerPartyType = New CapaSUNAT.Modelos.CustomerPartyType()
                Dim partyCliente As CapaSUNAT.Modelos.PartyType = New CapaSUNAT.Modelos.PartyType()
                Dim partyIdentificion As CapaSUNAT.Modelos.PartyIdentificationType = New CapaSUNAT.Modelos.PartyIdentificationType()
                Dim partyIdentificions As List(Of CapaSUNAT.Modelos.PartyIdentificationType) = New List(Of CapaSUNAT.Modelos.PartyIdentificationType)()
                Dim idtipo As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()

                idtipo.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idtipo.schemeName = "Documento de Identidad"
                idtipo.schemeAgencyName = "PE:SUNAT"
                idtipo.schemeID = Comprobante.ClienteTipodocumento
                idtipo.Value = Comprobante.ClienteNumeroDocumento
                partyIdentificion.ID = idtipo
                partyIdentificions.Add(partyIdentificion)
                partyCliente.PartyIdentification = partyIdentificions.ToArray()

                Dim RazSocClientes As List(Of CapaSUNAT.Modelos.PartyNameType) = New List(Of CapaSUNAT.Modelos.PartyNameType)()
                Dim RazSocCliente As CapaSUNAT.Modelos.PartyNameType = New CapaSUNAT.Modelos.PartyNameType()
                Dim razSocial As Modelos.NameType1 = New Modelos.NameType1()
                razSocial.Value = Comprobante.ClienteRazonSocial
                RazSocCliente.Name = razSocial
                RazSocClientes.Add(RazSocCliente)

                Dim partySchemas As List(Of CapaSUNAT.Modelos.PartyTaxSchemeType) = New List(Of Modelos.PartyTaxSchemeType)()
                Dim partySchema As CapaSUNAT.Modelos.PartyTaxSchemeType = New CapaSUNAT.Modelos.PartyTaxSchemeType()
                Dim RegistroNombre As Modelos.RegistrationNameType = New Modelos.RegistrationNameType()
                RegistroNombre.Value = Comprobante.ClienteRazonSocial
                partySchema.RegistrationName = RegistroNombre

                Dim idcompañia As Modelos.CompanyIDType = New Modelos.CompanyIDType()
                idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idcompañia.schemeAgencyName = "PE:SUNAT"
                idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idcompañia.schemeID = Comprobante.ClienteTipodocumento
                idcompañia.Value = Comprobante.ClienteNumeroDocumento

                Dim schemeType As Modelos.TaxSchemeType = New Modelos.TaxSchemeType()
                Dim idc As Modelos.IDType = New Modelos.IDType()
                idc.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idc.schemeAgencyName = "PE:SUNAT"
                idc.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idc.schemeID = Comprobante.ClienteTipodocumento
                idc.Value = Comprobante.ClienteNumeroDocumento
                schemeType.ID = idc
                idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                idcompañia.schemeAgencyName = "PE:SUNAT"
                idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad"
                idcompañia.schemeID = Comprobante.ClienteTipodocumento
                idcompañia.Value = Comprobante.ClienteNumeroDocumento

                Dim partyLegals As List(Of Modelos.PartyLegalEntityType) = New List(Of Modelos.PartyLegalEntityType)()
                Dim partyLegal As Modelos.PartyLegalEntityType = New Modelos.PartyLegalEntityType()
                Dim Registro_Nombre As Modelos.RegistrationNameType = New Modelos.RegistrationNameType()
                Registro_Nombre.Value = Comprobante.ClienteRazonSocial
                partyLegal.RegistrationName = Registro_Nombre

                Dim direccionCliente As Modelos.AddressType = New Modelos.AddressType()
                Dim dirs As List(Of Modelos.AddressLineType) = New List(Of Modelos.AddressLineType)()
                Dim dir As Modelos.AddressLineType = New Modelos.AddressLineType()
                Dim lineas As List(Of Modelos.LineType) = New List(Of Modelos.LineType)()
                Dim linea As Modelos.LineType = New Modelos.LineType()
                linea.Value = Comprobante.ClienteDireccion
                dir.Line = linea
                dirs.Add(dir)
                direccionCliente.AddressLine = dirs.ToArray()

                Dim paisC As CapaSUNAT.Modelos.CountryType = New CapaSUNAT.Modelos.CountryType()
                Dim codigoPaisC As CapaSUNAT.Modelos.IdentificationCodeType = New CapaSUNAT.Modelos.IdentificationCodeType()
                codigoPaisC.Value = "PE"
                paisC.IdentificationCode = codigoPaisC
                partyLegals.Add(partyLegal)
                partySchema.CompanyID = idcompañia
                partySchema.TaxScheme = schemeType
                partySchemas.Add(partySchema)
                partyCliente.PartyLegalEntity = partyLegals.ToArray()
                CustomerPartyCliente.Party = partyCliente

                Dim accoutingCustomerParty As Modelos.CustomerPartyType = New Modelos.CustomerPartyType()
                accoutingCustomerParty.Party = partyCliente
                Factura.AccountingCustomerParty = accoutingCustomerParty

                Dim TotalImptos As CapaSUNAT.Modelos.TaxTotalType = New CapaSUNAT.Modelos.TaxTotalType()
                Dim taxAmountImpto As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                taxAmountImpto.currencyID = Comprobante.Idmoneda
                taxAmountImpto.Value = Convert.ToDecimal(Comprobante.TotIgv)
                TotalImptos.TaxAmount = taxAmountImpto

                Dim subtotales As CapaSUNAT.Modelos.TaxSubtotalType() = New CapaSUNAT.Modelos.TaxSubtotalType(1) {}
                Dim subtotal As CapaSUNAT.Modelos.TaxSubtotalType = New CapaSUNAT.Modelos.TaxSubtotalType()
                Dim taxsubtotal As CapaSUNAT.Modelos.TaxableAmountType = New CapaSUNAT.Modelos.TaxableAmountType()
                taxsubtotal.currencyID = Comprobante.Idmoneda
                taxsubtotal.Value = Convert.ToDecimal(Comprobante.TotSubtotal)
                subtotal.TaxableAmount = taxsubtotal

                Dim TotalTaxAmountTotal As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                TotalTaxAmountTotal.currencyID = Comprobante.Idmoneda
                TotalTaxAmountTotal.Value = Convert.ToDecimal(Comprobante.TotIgv)
                subtotal.TaxAmount = TotalTaxAmountTotal

                Dim subTotalIGV As Modelos.TaxSubtotalType = New Modelos.TaxSubtotalType()
                subTotalIGV.TaxableAmount = taxsubtotal
                subtotales(0) = subtotal
                TotalImptos.TaxSubtotal = subtotales

                Dim taxcategoryTotal As CapaSUNAT.Modelos.TaxCategoryType = New CapaSUNAT.Modelos.TaxCategoryType()
                Dim taxScheme As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                Dim idTotal As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idTotal.schemeID = "UN/ECE 5305"
                idTotal.schemeName = "Tax Category Identifier"
                idTotal.schemeAgencyName = "United Nations Economic Commission for Europe"
                idTotal.Value = "S"

                Dim nametypeImpto As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                nametypeImpto.Value = "IGV"

                Dim taxtypecodeImpto As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                taxtypecodeImpto.Value = "VAT"

                Dim idTot As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                idTot.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05"
                idTot.schemeAgencyName = "PE:SUNAT"
                idTot.schemeName = "Codigo de tributos"
                idTot.Value = "1000"
                taxScheme.ID = idTot

                Dim nametypeImptoIGV As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                nametypeImptoIGV.Value = "IGV"

                Dim taxtypecodeImpuesto As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                taxtypecodeImpuesto.Value = "VAT"
                taxScheme.Name = nametypeImpto
                taxScheme.TaxTypeCode = taxtypecodeImpto
                taxcategoryTotal.TaxScheme = taxScheme
                subtotal.TaxCategory = taxcategoryTotal

                Dim TaxSubtotals As List(Of CapaSUNAT.Modelos.TaxSubtotalType) = New List(Of CapaSUNAT.Modelos.TaxSubtotalType)()
                TaxSubtotals.Add(subtotal)
                TotalImptos.TaxSubtotal = TaxSubtotals.ToArray()

                Dim taxTotals As List(Of CapaSUNAT.Modelos.TaxTotalType) = New List(Of CapaSUNAT.Modelos.TaxTotalType)()
                taxTotals.Add(TotalImptos)
                Factura.TaxTotal = taxTotals.ToArray()

                Dim TotalValorVenta As CapaSUNAT.Modelos.MonetaryTotalType = New CapaSUNAT.Modelos.MonetaryTotalType()
                Dim ImporteTotalVenta As CapaSUNAT.Modelos.PayableAmountType = New CapaSUNAT.Modelos.PayableAmountType()
                ImporteTotalVenta.currencyID = Comprobante.Idmoneda
                ImporteTotalVenta.Value = Convert.ToDecimal(String.Format("{0:0.00}", Comprobante.TotNeto))
                TotalValorVenta.PayableAmount = ImporteTotalVenta
                Factura.RequestedMonetaryTotal = TotalValorVenta

                Dim items As CapaSUNAT.Modelos.DebitNoteLineType() = New CapaSUNAT.Modelos.DebitNoteLineType(9) {}
                Dim iditem As Integer = 1

                'Detalles del comprobante
                For Each det As Detalles In Comprobante.Detalles
                    Dim item As CapaSUNAT.Modelos.DebitNoteLineType = New CapaSUNAT.Modelos.DebitNoteLineType()
                    Dim numeroItem As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    numeroItem.Value = iditem.ToString()
                    item.ID = numeroItem

                    Dim cantidad As CapaSUNAT.Modelos.DebitedQuantityType = New CapaSUNAT.Modelos.DebitedQuantityType()
                    cantidad.unitCodeListAgencyName = "United Nations Economic Commission for Europe"
                    cantidad.unitCodeListID = "UN/ECE rec 20"
                    cantidad.unitCode = det.UnidadMedida
                    cantidad.Value = Convert.ToInt32(det.Cantidad)
                    item.DebitedQuantity = cantidad

                    Dim ValorVenta As CapaSUNAT.Modelos.LineExtensionAmountType = New CapaSUNAT.Modelos.LineExtensionAmountType()
                    ValorVenta.currencyID = Comprobante.Idmoneda
                    ValorVenta.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.Total / 1.18D))
                    item.LineExtensionAmount = ValorVenta

                    Dim ValorReferenUnitario As CapaSUNAT.Modelos.PricingReferenceType = New CapaSUNAT.Modelos.PricingReferenceType()
                    Dim TipoPrecios As CapaSUNAT.Modelos.PriceType() = New CapaSUNAT.Modelos.PriceType(1) {}
                    Dim TipoPrecio As CapaSUNAT.Modelos.PriceType = New CapaSUNAT.Modelos.PriceType()
                    Dim PrecioMonto As CapaSUNAT.Modelos.PriceAmountType = New CapaSUNAT.Modelos.PriceAmountType()
                    PrecioMonto.currencyID = Comprobante.Idmoneda
                    PrecioMonto.Value = Convert.ToDecimal(String.Format("{0:0.000}", det.Precio))
                    TipoPrecio.PriceAmount = PrecioMonto

                    Dim TipoPrecioCode As CapaSUNAT.Modelos.PriceTypeCodeType = New CapaSUNAT.Modelos.PriceTypeCodeType()
                    TipoPrecioCode.listName = "Tipo de Precio"
                    TipoPrecioCode.listAgencyName = "PE:SUNAT"
                    TipoPrecioCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16"
                    TipoPrecioCode.Value = "01"
                    TipoPrecio.PriceTypeCode = TipoPrecioCode
                    TipoPrecios(0) = TipoPrecio
                    ValorReferenUnitario.AlternativeConditionPrice = TipoPrecios
                    item.PricingReference = ValorReferenUnitario

                    Dim Totales_Items As CapaSUNAT.Modelos.TaxTotalType() = New CapaSUNAT.Modelos.TaxTotalType(1) {}
                    Dim Totales_Item As CapaSUNAT.Modelos.TaxTotalType = New CapaSUNAT.Modelos.TaxTotalType()
                    Dim Total_Item As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                    Total_Item.currencyID = Comprobante.Idmoneda
                    Total_Item.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18D)))
                    Totales_Item.TaxAmount = Total_Item

                    Dim subtotal_Items As CapaSUNAT.Modelos.TaxSubtotalType() = New CapaSUNAT.Modelos.TaxSubtotalType(1) {}
                    Dim subtotal_Item As CapaSUNAT.Modelos.TaxSubtotalType = New CapaSUNAT.Modelos.TaxSubtotalType()
                    Dim taxsubtotal_IGVItem As CapaSUNAT.Modelos.TaxableAmountType = New CapaSUNAT.Modelos.TaxableAmountType()
                    taxsubtotal_IGVItem.currencyID = Comprobante.Idmoneda
                    taxsubtotal_IGVItem.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem / 1.18D))
                    subtotal_Item.TaxableAmount = taxsubtotal_IGVItem

                    Dim TotalTaxAmount_IGVItem As CapaSUNAT.Modelos.TaxAmountType = New CapaSUNAT.Modelos.TaxAmountType()
                    TotalTaxAmount_IGVItem.currencyID = Comprobante.Idmoneda
                    TotalTaxAmount_IGVItem.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18D)))
                    subtotal_Item.TaxAmount = TotalTaxAmount_IGVItem
                    subtotal_Items(0) = subtotal_Item
                    Totales_Item.TaxSubtotal = subtotal_Items

                    Dim taxcategory_IGVItem As CapaSUNAT.Modelos.TaxCategoryType = New CapaSUNAT.Modelos.TaxCategoryType()
                    Dim idTaxCategoria As Modelos.IDType = New Modelos.IDType()
                    idTaxCategoria.schemeAgencyName = "United Nations Economic Commission for Europe"
                    idTaxCategoria.schemeName = "Tax Category Identifier"
                    idTaxCategoria.schemeID = "UN/ECE 5305"
                    idTaxCategoria.Value = "S"

                    Dim porcentaje As Modelos.PercentType1 = New Modelos.PercentType1()
                    porcentaje.Value = Convert.ToDecimal(det.porIgvItem) * 100
                    taxcategory_IGVItem.Percent = porcentaje
                    subtotal_Item.TaxCategory = taxcategory_IGVItem

                    Dim ReasonCode As Modelos.TaxExemptionReasonCodeType = New Modelos.TaxExemptionReasonCodeType()
                    ReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07"
                    ReasonCode.listName = "Afectacion del IGV"
                    ReasonCode.listAgencyName = "PE:SUNAT"
                    ReasonCode.Value = "10"
                    taxcategory_IGVItem.TaxExemptionReasonCode = ReasonCode

                    Dim taxscheme_IGVItem As CapaSUNAT.Modelos.TaxSchemeType = New CapaSUNAT.Modelos.TaxSchemeType()
                    Dim id2_IGVItem As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    id2_IGVItem.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05"
                    id2_IGVItem.schemeAgencyName = "PE:SUNAT"
                    id2_IGVItem.schemeName = "Codigo de tributos"
                    id2_IGVItem.Value = "1000"
                    taxscheme_IGVItem.ID = id2_IGVItem

                    Dim nombreImpto_IGVItem As CapaSUNAT.Modelos.NameType1 = New CapaSUNAT.Modelos.NameType1()
                    nombreImpto_IGVItem.Value = "IGV"
                    taxscheme_IGVItem.Name = nombreImpto_IGVItem

                    Dim nombreImpto_IGVItemInter As CapaSUNAT.Modelos.TaxTypeCodeType = New CapaSUNAT.Modelos.TaxTypeCodeType()
                    nombreImpto_IGVItemInter.Value = "VAT"
                    taxscheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter
                    taxscheme_IGVItem.Name = nombreImpto_IGVItem
                    taxcategory_IGVItem.TaxScheme = taxscheme_IGVItem
                    subtotal_Items(0) = subtotal_Item
                    Totales_Item.TaxSubtotal = subtotal_Items
                    Totales_Items(0) = Totales_Item
                    item.TaxTotal = Totales_Items

                    Dim descriptions As CapaSUNAT.Modelos.DescriptionType() = New CapaSUNAT.Modelos.DescriptionType(1) {}
                    Dim description As CapaSUNAT.Modelos.DescriptionType = New CapaSUNAT.Modelos.DescriptionType()
                    description.Value = det.DescripcionProducto

                    Dim codigoProd As CapaSUNAT.Modelos.ItemIdentificationType = New CapaSUNAT.Modelos.ItemIdentificationType()
                    Dim id As CapaSUNAT.Modelos.IDType = New CapaSUNAT.Modelos.IDType()
                    id.Value = det.Codcom
                    codigoProd.ID = id

                    Dim PrecioProducto As CapaSUNAT.Modelos.PriceType = New CapaSUNAT.Modelos.PriceType()
                    Dim PrecioMontoTipo As CapaSUNAT.Modelos.PriceAmountType = New CapaSUNAT.Modelos.PriceAmountType()
                    PrecioMontoTipo.Value = Convert.ToDecimal(String.Format("{0:0.00}", det.Precio / (det.porIgvItem + 1)))
                    PrecioMontoTipo.currencyID = Comprobante.Idmoneda
                    PrecioProducto.PriceAmount = PrecioMontoTipo

                    Dim itemTipo As CapaSUNAT.Modelos.ItemType = New CapaSUNAT.Modelos.ItemType()
                    descriptions(0) = description
                    itemTipo.Description = descriptions
                    itemTipo.SellersItemIdentification = codigoProd

                    Dim codSunats As List(Of Modelos.CommodityClassificationType) = New List(Of Modelos.CommodityClassificationType)()
                    Dim codSunat As Modelos.CommodityClassificationType = New Modelos.CommodityClassificationType()
                    Dim codClas As Modelos.ItemClassificationCodeType = New Modelos.ItemClassificationCodeType()
                    codClas.listName = "Item Classification"
                    codClas.listAgencyName = "GS1 US"
                    codClas.listID = "UNSPSC"
                    codClas.Value = "25172405"
                    codSunat.ItemClassificationCode = codClas
                    codSunats.Add(codSunat)
                    itemTipo.CommodityClassification = codSunats.ToArray()
                    item.Item = itemTipo
                    item.Price = PrecioProducto
                    items(iditem) = item
                    iditem += 1

                Next
                Factura.DebitNoteLine = items

                'PROCESO PARA LA GENERACION Y ENVIO DEL XML
                '*******************************************************************************************************************************************************
                Dim archXML As String = GenerarComprobante(Factura, Comprobante.EmpresaRUC, Comprobante.Idtipocomp, Comprobante.Serie, Comprobante.Numero, Comprobante.EmpresaRUC)
                FirmarXML(archXML, Ruta_Certificado, Password_Certificado)
                Dim strEnvio As String = Ruta_ENVIOS & Path.GetFileName(archXML).Replace(".xml", ".zip")
                Comprimir(archXML, strEnvio)
                EnviarDocumento(strEnvio)
                Return 1
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace