Imports System
Imports System.Collections.Generic

Namespace CapaSUNAT.ViewModels
    Partial Public Class Detalles
        Public Property Idcab As Integer
        Public Property Codcom As String
        Public Property DescripcionProducto As String
        Public Property UnidadMedida As String
        Public Property Cantidad As Decimal
        Public Property Precio As Decimal
        Public Property Total As Decimal
        Public Property mtoValorVentaItem As Decimal
        Public Property porIgvItem As Decimal
        Public Property Activo As Boolean?
        Public Property Usuario As String
        Public Property Igv As Decimal?
        Public Property Descuento As Decimal?
    End Class
End Namespace
