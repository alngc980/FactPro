Imports System
Imports System.Collections.Generic

Namespace CapaSUNAT.ViewModels
    Partial Public Class GUIA_REMISION
        Public Property id As Integer
        Public Property NumeroGuiaRemision As String
        Public Property NumeroSalida As String
        Public Property FechaEmision As Nullable(Of System.DateTime)
        Public Property FechaInicio As Nullable(Of System.DateTime)
        Public Property IdCliente As Nullable(Of Integer)
        Public Property Partida As String
        Public Property Llegada As String
        Public Property Placa As String
        Public Property Brevete As String
        Public Property idTransportista As Nullable(Of Integer)
        Public Property LicenciaConducir As String
        Public Property Comentario1 As String
        Public Property Comentario2 As String
        Public Property Despachador As String
        Public Property GuiaPorte As String
        Public Property Telefono As String
        Public Property NumDocRelacionado As String
        Public Property UsuarioCreacion As String
        Public Property FechaCreacion As Nullable(Of System.DateTime)
        Public Property UsuarioModificacion As String
        Public Property FechaModificacion As Nullable(Of System.DateTime)
        Public Property Activo As Nullable(Of Boolean)
        Public Property TipoDocRelacionado As String
        Public Property Observaciones As String
        Public Property NumGuiaRemisionBaja As String
        Public Property TipoDocGuiaRemBaja As String
        Public Property NumDocRemite As String
        Public Property TipoDocRemite As String
        Public Property RazonSocialRemite As String
        Public Property NumDocDestinatario As String
        Public Property TipoDocDestinatario As String
        Public Property RazonSocialDestinatario As String
        Public Property NumDocProveedor As String
        Public Property TipoDocProveedor As String
        Public Property RazonSocialProveedor As String
        Public Property MotivoTraslado As String
        Public Property DescMotivo As String
        Public Property IndicaMotivoTraslado As String
        Public Property Peso As Nullable(Of Decimal)
        Public Property UniMedPesoBruto As String
        Public Property NumBultosPallets As Nullable(Of Decimal)
        Public Property MovilTraslado As String
        Public Property FechaInicioTraslado As Nullable(Of System.DateTime)
        Public Property NumRucTransportista As String
        Public Property TipoDocTransportista As String
        Public Property RazonSocialTransportista As String
        Public Property TipoDocConductor As String
        Public Property NumDocConductor As String
        Public Property UbigeoPuntoLlegada As String
        Public Property NumContenedor As String
        Public Property UbigeoPuntoPartida As String
        Public Property CodPuertoEmbDes As String
    End Class
End Namespace
