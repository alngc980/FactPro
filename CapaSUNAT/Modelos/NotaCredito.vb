Imports System.Xml.Serialization

Namespace CapaSUNAT.Modelos
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")>
    <System.SerializableAttribute()>
    <System.Diagnostics.DebuggerStepThroughAttribute()>
    <System.ComponentModel.DesignerCategoryAttribute("code")>
    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2")>
    <System.Xml.Serialization.XmlRootAttribute("CreditNote", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2", IsNullable:=False)>
    Partial Public Class CreditNoteType
        <XmlAttribute(AttributeName:="xmlns")>
        Public Property Xmlns As String
        <XmlAttribute(AttributeName:="cac", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Cac As String
        <XmlAttribute(AttributeName:="cbc", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Cbc As String
        <XmlAttribute(AttributeName:="ccts", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Ccts As String
        <XmlAttribute(AttributeName:="ds", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Ds As String
        <XmlAttribute(AttributeName:="ext", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Ext As String
        <XmlAttribute(AttributeName:="qdt", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Qdt As String
        <XmlAttribute(AttributeName:="udt", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Udt As String
        <XmlAttribute(AttributeName:="xsi", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Xsi As String
        Private uBLExtensionsField As UBLExtensionType()
        Private uBLVersionIDField As UBLVersionIDType
        Private customizationIDField As CustomizationIDType
        Private profileIDField As ProfileIDType
        Private profileExecutionIDField As ProfileExecutionIDType
        Private idField As IDType
        Private copyIndicatorField As CopyIndicatorType
        Private uUIDField As UUIDType
        Private issueDateField As IssueDateType
        Private issueTimeField As IssueTimeType
        Private taxPointDateField As TaxPointDateType
        Private creditNoteTypeCodeField As CreditNoteTypeCodeType
        Private noteField As NoteType()
        Private documentCurrencyCodeField As DocumentCurrencyCodeType
        Private taxCurrencyCodeField As TaxCurrencyCodeType
        Private pricingCurrencyCodeField As PricingCurrencyCodeType
        Private paymentCurrencyCodeField As PaymentCurrencyCodeType
        Private paymentAlternativeCurrencyCodeField As PaymentAlternativeCurrencyCodeType
        Private accountingCostCodeField As AccountingCostCodeType
        Private accountingCostField As AccountingCostType
        Private lineCountNumericField As LineCountNumericType
        Private buyerReferenceField As BuyerReferenceType
        Private invoicePeriodField As PeriodType()
        Private discrepancyResponseField As ResponseType()
        Private orderReferenceField As OrderReferenceType
        Private billingReferenceField As BillingReferenceType()
        Private despatchDocumentReferenceField As DocumentReferenceType()
        Private receiptDocumentReferenceField As DocumentReferenceType()
        Private contractDocumentReferenceField As DocumentReferenceType()
        Private additionalDocumentReferenceField As DocumentReferenceType()
        Private statementDocumentReferenceField As DocumentReferenceType()
        Private originatorDocumentReferenceField As DocumentReferenceType()
        Private signatureField As SignatureType()
        Private accountingSupplierPartyField As SupplierPartyType
        Private accountingCustomerPartyField As CustomerPartyType
        Private payeePartyField As PartyType
        Private buyerCustomerPartyField As CustomerPartyType
        Private sellerSupplierPartyField As SupplierPartyType
        Private taxRepresentativePartyField As PartyType
        Private deliveryField As DeliveryType()
        Private deliveryTermsField As DeliveryTermsType()
        Private paymentMeansField As PaymentMeansType()
        Private paymentTermsField As PaymentTermsType()
        Private taxExchangeRateField As ExchangeRateType
        Private pricingExchangeRateField As ExchangeRateType
        Private paymentExchangeRateField As ExchangeRateType
        Private paymentAlternativeExchangeRateField As ExchangeRateType
        Private allowanceChargeField As AllowanceChargeType()
        Private taxTotalField As TaxTotalType()
        Private legalMonetaryTotalField As MonetaryTotalType
        Private creditNoteLineField As CreditNoteLineType()

        <System.Xml.Serialization.XmlArrayAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")>
        <System.Xml.Serialization.XmlArrayItemAttribute("UBLExtension", IsNullable:=False)>
        Public Property UBLExtensions As UBLExtensionType()
            Get
                Return Me.uBLExtensionsField
            End Get
            Set(ByVal value As UBLExtensionType())
                Me.uBLExtensionsField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property UBLVersionID As UBLVersionIDType
            Get
                Return Me.uBLVersionIDField
            End Get
            Set(ByVal value As UBLVersionIDType)
                Me.uBLVersionIDField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property CustomizationID As CustomizationIDType
            Get
                Return Me.customizationIDField
            End Get
            Set(ByVal value As CustomizationIDType)
                Me.customizationIDField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property ProfileID As ProfileIDType
            Get
                Return Me.profileIDField
            End Get
            Set(ByVal value As ProfileIDType)
                Me.profileIDField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property ProfileExecutionID As ProfileExecutionIDType
            Get
                Return Me.profileExecutionIDField
            End Get
            Set(ByVal value As ProfileExecutionIDType)
                Me.profileExecutionIDField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property ID As IDType
            Get
                Return Me.idField
            End Get
            Set(ByVal value As IDType)
                Me.idField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property CopyIndicator As CopyIndicatorType
            Get
                Return Me.copyIndicatorField
            End Get
            Set(ByVal value As CopyIndicatorType)
                Me.copyIndicatorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property UUID As UUIDType
            Get
                Return Me.uUIDField
            End Get
            Set(ByVal value As UUIDType)
                Me.uUIDField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property IssueDate As IssueDateType
            Get
                Return Me.issueDateField
            End Get
            Set(ByVal value As IssueDateType)
                Me.issueDateField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property IssueTime As IssueTimeType
            Get
                Return Me.issueTimeField
            End Get
            Set(ByVal value As IssueTimeType)
                Me.issueTimeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property TaxPointDate As TaxPointDateType
            Get
                Return Me.taxPointDateField
            End Get
            Set(ByVal value As TaxPointDateType)
                Me.taxPointDateField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property CreditNoteTypeCode As CreditNoteTypeCodeType
            Get
                Return Me.creditNoteTypeCodeField
            End Get
            Set(ByVal value As CreditNoteTypeCodeType)
                Me.creditNoteTypeCodeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("Note", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property Note As NoteType()
            Get
                Return Me.noteField
            End Get
            Set(ByVal value As NoteType())
                Me.noteField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property DocumentCurrencyCode As DocumentCurrencyCodeType
            Get
                Return Me.documentCurrencyCodeField
            End Get
            Set(ByVal value As DocumentCurrencyCodeType)
                Me.documentCurrencyCodeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property TaxCurrencyCode As TaxCurrencyCodeType
            Get
                Return Me.taxCurrencyCodeField
            End Get
            Set(ByVal value As TaxCurrencyCodeType)
                Me.taxCurrencyCodeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property PricingCurrencyCode As PricingCurrencyCodeType
            Get
                Return Me.pricingCurrencyCodeField
            End Get
            Set(ByVal value As PricingCurrencyCodeType)
                Me.pricingCurrencyCodeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property PaymentCurrencyCode As PaymentCurrencyCodeType
            Get
                Return Me.paymentCurrencyCodeField
            End Get
            Set(ByVal value As PaymentCurrencyCodeType)
                Me.paymentCurrencyCodeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property PaymentAlternativeCurrencyCode As PaymentAlternativeCurrencyCodeType
            Get
                Return Me.paymentAlternativeCurrencyCodeField
            End Get
            Set(ByVal value As PaymentAlternativeCurrencyCodeType)
                Me.paymentAlternativeCurrencyCodeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property AccountingCostCode As AccountingCostCodeType
            Get
                Return Me.accountingCostCodeField
            End Get
            Set(ByVal value As AccountingCostCodeType)
                Me.accountingCostCodeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property AccountingCost As AccountingCostType
            Get
                Return Me.accountingCostField
            End Get
            Set(ByVal value As AccountingCostType)
                Me.accountingCostField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property LineCountNumeric As LineCountNumericType
            Get
                Return Me.lineCountNumericField
            End Get
            Set(ByVal value As LineCountNumericType)
                Me.lineCountNumericField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")>
        Public Property BuyerReference As BuyerReferenceType
            Get
                Return Me.buyerReferenceField
            End Get
            Set(ByVal value As BuyerReferenceType)
                Me.buyerReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("InvoicePeriod", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property InvoicePeriod As PeriodType()
            Get
                Return Me.invoicePeriodField
            End Get
            Set(ByVal value As PeriodType())
                Me.invoicePeriodField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("DiscrepancyResponse", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property DiscrepancyResponse As ResponseType()
            Get
                Return Me.discrepancyResponseField
            End Get
            Set(ByVal value As ResponseType())
                Me.discrepancyResponseField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property OrderReference As OrderReferenceType
            Get
                Return Me.orderReferenceField
            End Get
            Set(ByVal value As OrderReferenceType)
                Me.orderReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("BillingReference", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property BillingReference As BillingReferenceType()
            Get
                Return Me.billingReferenceField
            End Get
            Set(ByVal value As BillingReferenceType())
                Me.billingReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("DespatchDocumentReference", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property DespatchDocumentReference As DocumentReferenceType()
            Get
                Return Me.despatchDocumentReferenceField
            End Get
            Set(ByVal value As DocumentReferenceType())
                Me.despatchDocumentReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ReceiptDocumentReference", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property ReceiptDocumentReference As DocumentReferenceType()
            Get
                Return Me.receiptDocumentReferenceField
            End Get
            Set(ByVal value As DocumentReferenceType())
                Me.receiptDocumentReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ContractDocumentReference", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property ContractDocumentReference As DocumentReferenceType()
            Get
                Return Me.contractDocumentReferenceField
            End Get
            Set(ByVal value As DocumentReferenceType())
                Me.contractDocumentReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("AdditionalDocumentReference", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property AdditionalDocumentReference As DocumentReferenceType()
            Get
                Return Me.additionalDocumentReferenceField
            End Get
            Set(ByVal value As DocumentReferenceType())
                Me.additionalDocumentReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("StatementDocumentReference", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property StatementDocumentReference As DocumentReferenceType()
            Get
                Return Me.statementDocumentReferenceField
            End Get
            Set(ByVal value As DocumentReferenceType())
                Me.statementDocumentReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("OriginatorDocumentReference", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property OriginatorDocumentReference As DocumentReferenceType()
            Get
                Return Me.originatorDocumentReferenceField
            End Get
            Set(ByVal value As DocumentReferenceType())
                Me.originatorDocumentReferenceField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("Signature", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property Signature As SignatureType()
            Get
                Return Me.signatureField
            End Get
            Set(ByVal value As SignatureType())
                Me.signatureField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property AccountingSupplierParty As SupplierPartyType
            Get
                Return Me.accountingSupplierPartyField
            End Get
            Set(ByVal value As SupplierPartyType)
                Me.accountingSupplierPartyField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property AccountingCustomerParty As CustomerPartyType
            Get
                Return Me.accountingCustomerPartyField
            End Get
            Set(ByVal value As CustomerPartyType)
                Me.accountingCustomerPartyField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property PayeeParty As PartyType
            Get
                Return Me.payeePartyField
            End Get
            Set(ByVal value As PartyType)
                Me.payeePartyField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property BuyerCustomerParty As CustomerPartyType
            Get
                Return Me.buyerCustomerPartyField
            End Get
            Set(ByVal value As CustomerPartyType)
                Me.buyerCustomerPartyField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property SellerSupplierParty As SupplierPartyType
            Get
                Return Me.sellerSupplierPartyField
            End Get
            Set(ByVal value As SupplierPartyType)
                Me.sellerSupplierPartyField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property TaxRepresentativeParty As PartyType
            Get
                Return Me.taxRepresentativePartyField
            End Get
            Set(ByVal value As PartyType)
                Me.taxRepresentativePartyField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("Delivery", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property Delivery As DeliveryType()
            Get
                Return Me.deliveryField
            End Get
            Set(ByVal value As DeliveryType())
                Me.deliveryField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("DeliveryTerms", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property DeliveryTerms As DeliveryTermsType()
            Get
                Return Me.deliveryTermsField
            End Get
            Set(ByVal value As DeliveryTermsType())
                Me.deliveryTermsField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("PaymentMeans", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property PaymentMeans As PaymentMeansType()
            Get
                Return Me.paymentMeansField
            End Get
            Set(ByVal value As PaymentMeansType())
                Me.paymentMeansField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("PaymentTerms", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property PaymentTerms As PaymentTermsType()
            Get
                Return Me.paymentTermsField
            End Get
            Set(ByVal value As PaymentTermsType())
                Me.paymentTermsField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property TaxExchangeRate As ExchangeRateType
            Get
                Return Me.taxExchangeRateField
            End Get
            Set(ByVal value As ExchangeRateType)
                Me.taxExchangeRateField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property PricingExchangeRate As ExchangeRateType
            Get
                Return Me.pricingExchangeRateField
            End Get
            Set(ByVal value As ExchangeRateType)
                Me.pricingExchangeRateField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property PaymentExchangeRate As ExchangeRateType
            Get
                Return Me.paymentExchangeRateField
            End Get
            Set(ByVal value As ExchangeRateType)
                Me.paymentExchangeRateField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property PaymentAlternativeExchangeRate As ExchangeRateType
            Get
                Return Me.paymentAlternativeExchangeRateField
            End Get
            Set(ByVal value As ExchangeRateType)
                Me.paymentAlternativeExchangeRateField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("AllowanceCharge", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property AllowanceCharge As AllowanceChargeType()
            Get
                Return Me.allowanceChargeField
            End Get
            Set(ByVal value As AllowanceChargeType())
                Me.allowanceChargeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("TaxTotal", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property TaxTotal As TaxTotalType()
            Get
                Return Me.taxTotalField
            End Get
            Set(ByVal value As TaxTotalType())
                Me.taxTotalField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property LegalMonetaryTotal As MonetaryTotalType
            Get
                Return Me.legalMonetaryTotalField
            End Get
            Set(ByVal value As MonetaryTotalType)
                Me.legalMonetaryTotalField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("CreditNoteLine", [Namespace]:="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")>
        Public Property CreditNoteLine As CreditNoteLineType()
            Get
                Return Me.creditNoteLineField
            End Get
            Set(ByVal value As CreditNoteLineType())
                Me.creditNoteLineField = value
            End Set
        End Property
    End Class

    Partial Public Class IdentifierType1
        Inherits IdentifierType
    End Class
End Namespace
