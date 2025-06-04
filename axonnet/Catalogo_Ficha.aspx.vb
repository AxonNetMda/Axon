Imports CapaDatos
Imports capaEntidad

Public Class Catalogo_Ficha
    Inherits System.Web.UI.Page
    Public Property _IdProducto As Integer
    Public Property _RutaImagen As String
    Public Property _NombreProducto As String
    Public Property _PrecioAntes As Decimal
    Public Property _PrecioActual As Decimal
    Public Property _Notas As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim precioAntes As Decimal
            Dim IdProducto As String = Request.QueryString("IdProducto")
            Dim lista As List(Of producto) = New CD_Producto().Listar(IdProducto)
            If lista.Count > 0 Then
                _IdProducto = IdProducto
                _RutaImagen = lista(0).rutaImagen & "/" & lista(0).NombreFoto1
                If lista(0).Notas = "" AndAlso lista(0).Notas = "Sin Datos" Then
                    _Notas = lista(0).Nombre
                Else
                    _Notas = lista(0).Notas
                End If
                precioAntes = lista(0).PrecioVenta + (lista(0).PrecioVenta * 0.1)
                _NombreProducto = lista(0).Nombre
                _PrecioAntes = FormatCurrency(precioAntes / 1.1, 2)
                _PrecioActual = FormatCurrency(lista(0).PrecioVenta / 1.1, 2)
            End If
        End If
    End Sub

End Class