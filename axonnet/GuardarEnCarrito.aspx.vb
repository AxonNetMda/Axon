Imports System.Data.SqlClient
Imports CapaDatos

Public Class GuardarEnCarrito
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            ' Obtener los parámetros de la solicitud
            Dim idProducto As Integer = Convert.ToInt32(Request.Form("idProducto"))
            Dim precio As Decimal = Convert.ToDecimal(Request.Form("precio"))
            Dim sucursal As String = Request.Form("sucursal")
            Dim referencia As String = Request.Form("referencia")
            Dim cantidad As Integer = Convert.ToInt32(Request.Form("cantidad"))

            ' Calcular el subtotal
            Dim subtotal As Decimal = cantidad * precio

            ' Insertar los datos en la tabla Carrito
            Using conn As New SqlConnection(Conexion.conectar.Cadena)
                Dim query As String = "INSERT INTO Carrito (IdProducto, Cantidad, PrecioUnitario, Subtotal, Sucursal, Referencia) VALUES (@IdProducto, @Cantidad, @PrecioUnitario, @Subtotal, @Sucursal, @Referencia)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto)
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad)
                    cmd.Parameters.AddWithValue("@PrecioUnitario", precio)
                    cmd.Parameters.AddWithValue("@Subtotal", subtotal)
                    cmd.Parameters.AddWithValue("@Sucursal", sucursal)
                    cmd.Parameters.AddWithValue("@Referencia", referencia)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' Devolver una respuesta al cliente
            Response.Write("OK")
        End If
    End Sub


End Class