Imports System.Data.SqlClient

Public Class ConexionN


    Public Function conectar() As SqlConnection
        Dim scn As SqlConnection
        scn = New SqlConnection("SERVER=01A-SDTI-07;database=DBFacturadorPrueba;Integrated security=true")
        'scn = New SqlConnection("SERVER=SERVER;database=DBFacturadorPrueba;Integrated security=true")
        Return scn ' "SERVER=SERVER;database=DBFacturadorPrueba;Integrated security=true"
    End Function

End Class
