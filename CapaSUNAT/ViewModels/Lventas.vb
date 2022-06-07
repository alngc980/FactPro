Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations


Public Class Lventas
    Public Property idventa As Integer
    Public Property idclientev As Integer
    '<DataType(DataType.Date)>
    '<DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)>
    Public Property fecha_venta As Date
    Public Property Monto_total As Decimal
    Public Property Tipo_de_pago As String
    Public Property Estado As String
    Public Property TotalIgv As Decimal
    Public Property Serie As String
    Public Property Correlativo As String
    Public Property Id_usuario As Integer
    '<DataType(DataType.Date)>
    '<DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)>
    Public Property Fecha_de_pago As Date
    Public Property ACCION As String
    Public Property Saldo As Decimal
    Public Property Pago_con As Decimal
    Public Property Porcentaje_IGV As Decimal
    Public Property Id_caja As Integer
    Public Property Referencia_tarjeta As String
    Public Property Vuelto As Decimal
    Public Property Efectivo As Decimal
    Public Property Credito As Decimal
    Public Property Tarjeta As Decimal
    Public Property CodigoComprobante As String
    Public Property Idcomprobante As Integer
    Public Property contadorProductos As Integer
    Public Property EmpresaRUCemisor As String
    Public Property EmpresaRUCcliente As String
    Public Property EmpresaRazonsocialEmisora As String
    Public Property EmpresaRazonsocialCliente As String
    Public Property Ubigeo As String
    Public Property DptoempresaEmisora As String
    Public Property ProvempresaEmisora As String
    Public Property DistmpresaEmisora As String
    Public Property DireccionEmpresaEmisora As String
    Public Property DireccionCliente As String
    Public Property TotSubtotal As Decimal
    Public Overridable Property Detalles As ICollection(Of Ldetalleventas)
    Public Property Cab_Ref_Motivo As String
    Public Property Cab_Ref_Serie As String
    Public Property Cab_Ref_TipoComprobante As String
    Public Property Cab_Ref_Numero As String
    Public Property CodigoTipoNotacredito As String
    Public Property CodigoTipoIdentificacion As String
    Public Property Id_mesa As Integer
    Public Property Numero_personas As Integer
    Public Property NombreLlevar As String
    Public Property Nota As String
    Public Property Tiposolicitud As String
    Public Property Totalletras As String
End Class
