Imports System
Imports System.Collections.Generic

Namespace CapaSUNAT.ViewModels
    Partial Public Class Cabecera
        Public Sub New()
            Detalles = New HashSet(Of Detalles)()
        End Sub

        Public Property Idcabecera As Integer
        Public Property Idmoneda As String
        Public Property Observaciones As String
        Public Property Idtipocomp As String
        Public Property Serie As String
        Public Property Numero As String
        Public Property Incligv As Boolean?
        Public Property Dscto As Decimal?
        Public Property Igv As Decimal?
        Public Property Total As Decimal?
        Public Property Fechaemision As DateTime?
        Public Property Numdoc As String
        Public Property Usuario As String
        Public Property Fechavencimiento As DateTime?
        Public Property Chser As Boolean?
        Public Property TCompra As Decimal?
        Public Property Activo As Boolean?
        Public Property Porcigv As Decimal?
        Public Property Idcliente As Integer?
        Public Property Guiaremision As String
        Public Property TotSubtotal As Decimal?
        Public Property TotDsctos As Decimal?
        Public Property TotTotal As Decimal?
        Public Property TotIgv As Decimal?
        Public Property TotIcbper As Decimal?
        Public Property TotISC As Decimal?
        Public Property TotOtros As Decimal?
        Public Property TotTributos As Decimal?
        Public Property TotNeto As Decimal?
        Public Property EmpresaDepartamento As String
        Public Property ID_EmpresaDepartamento As String
        Public Property EmpresaProvincia As String
        Public Property ID_EmpresaProvincia As String
        Public Property EmpresaDistrito As String
        Public Property ID_EmpresaDistrito As String
        Public Property EmpresaRazonSocial As String
        Public Property EmpresaRUC As String
        Public Property EmpresaDireccion As String
        Public Property EmisorSucursal As String
        Public Property Cab_Clte_ID_RazonSocial As String
        Public Property Cab_Clte_IdDistrito As String
        Public Property Cab_Clte_Distrito As String
        Public Property Cab_Clte_IdProvincia As String
        Public Property Cab_Clte_Provincia As String
        Public Property Cab_Clte_IdDepartamento As String
        Public Property Cab_Clte_Departamento As String
        Public Property Cab_Ref_Serie As String
        Public Property Cab_Ref_Numero As String
        Public Property Cab_Ref_TipoDeDocumento As String
        Public Property Cab_Ref_Motivo As String
        Public Property Cab_Ref_TipoNotaCredito As String
        Public Property Cab_Ref_TipoNotaDebito As String
        Public Property Cab_Ref_FechaEmision As DateTime
        Public Property ClienteRazonSocial As String
        Public Property ClienteDireccion As String
        Public Property ClienteUbigeo As String
        Public Property ClienteTipodocumento As String
        Public Property ClienteNumeroDocumento As String
        Public Property FormaPago As String
        Public Property NumeroCuotas As Integer
        Public Property MontoCuota As Decimal
        Public Property cLeyenda As String
        Public Property AfectacionIGV As Integer
        Public Overridable Property Detalles As ICollection(Of Detalles)
    End Class
End Namespace
