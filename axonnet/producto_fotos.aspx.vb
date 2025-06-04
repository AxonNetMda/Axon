Imports System.Data.SqlClient
Imports System.IO
Imports CapaDatos
Imports capaEntidad
Imports capaNegocio

Public Class producto_fotos
    Inherits System.Web.UI.Page
    Public Property sTitulo As String
    Public Property sAccion As String
    Public Property nidSubcategoria As Integer = 0
    Public Property nIdCategoria As Integer
    Dim HayError As Boolean = False
    Dim nidProducto As Integer
    Dim ImgCarpetaProductos As String = My.Settings.CarpetaProductos
    Dim ImgProductoDefault As String = My.Settings.imgProductoDefault
    Dim CarpetaImagen As String = ImgCarpetaProductos & "/" & ImgProductoDefault
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sNombreUsuario") Is Nothing Then
            Response.Redirect("~/login.aspx")
        End If

        If IsPostBack Then

        Else
            sTitulo = Request.QueryString("TituloForm")
            sAccion = Request.QueryString("Accion")
            nidSubcategoria = Request.QueryString("idSubcategoria")
            nIdCategoria = Request.QueryString("idCategoria")
            nidProducto = Request.QueryString("idproducto")
            lblTitulo.Text = sTitulo
            Dim i As Integer = 0
            Dim timestamp As String = DateTime.Now.Ticks.ToString()
            hNombreImagen1.Value = ImgProductoDefault
            hNombreImagen2.Value = ImgProductoDefault
            hNombreImagen3.Value = ImgProductoDefault
            hNombreImagen4.Value = ImgProductoDefault
            hNombreImagen5.Value = ImgProductoDefault
            hNombreImagen6.Value = ImgProductoDefault
            hNombreImagen7.Value = ImgProductoDefault
            hNombreImagen8.Value = ImgProductoDefault
            hNombreImagen9.Value = ImgProductoDefault
            hNombreImagen10.Value = ImgProductoDefault
            hNombreImagen11.Value = ImgProductoDefault
            hNombreImagen12.Value = ImgProductoDefault
            lblimagen1.Text = ImgProductoDefault
            lblimagen2.Text = ImgProductoDefault
            lblimagen3.Text = ImgProductoDefault
            lblimagen4.Text = ImgProductoDefault
            lblimagen5.Text = ImgProductoDefault
            lblimagen6.Text = ImgProductoDefault
            lblimagen7.Text = ImgProductoDefault
            lblimagen8.Text = ImgProductoDefault
            lblimagen9.Text = ImgProductoDefault
            lblimagen11.Text = ImgProductoDefault
            lblimagen11.Text = ImgProductoDefault
            lblimagen12.Text = ImgProductoDefault
            imagen1.ImageUrl = CarpetaImagen
            imagen2.ImageUrl = CarpetaImagen
            imagen3.ImageUrl = CarpetaImagen
            imagen4.ImageUrl = CarpetaImagen
            imagen5.ImageUrl = CarpetaImagen
            imagen6.ImageUrl = CarpetaImagen
            imagen7.ImageUrl = CarpetaImagen
            imagen8.ImageUrl = CarpetaImagen
            imagen9.ImageUrl = CarpetaImagen
            imagen10.ImageUrl = CarpetaImagen
            imagen11.ImageUrl = CarpetaImagen
            imagen12.ImageUrl = CarpetaImagen

            If sAccion <> "N" Then

                Dim listaprod As List(Of producto) = New CN_Producto().Listar(nidProducto)
                'Dim dia As DateTime = Convert.ToDateTime(listaprod(0).FechaUltimaCompra)
                Dim valorCelda As Integer
                If listaprod.Count > 0 Then
                    lblNombre.Text = listaprod(0).idProducto & " " & listaprod(0).Nombre
                    lblimagen1.Text = listaprod(0).NombreFoto1 & "?v=" & timestamp
                    lblimagen2.Text = listaprod(0).NombreFoto2 & "?v=" & timestamp
                    lblimagen3.Text = listaprod(0).NombreFoto3 & "?v=" & timestamp
                    lblimagen4.Text = listaprod(0).NombreFoto4 & "?v=" & timestamp
                    lblimagen5.Text = listaprod(0).NombreFoto5 & "?v=" & timestamp
                    lblimagen6.Text = listaprod(0).NombreFoto6 & "?v=" & timestamp
                    lblimagen7.Text = listaprod(0).NombreFoto7
                    lblimagen8.Text = listaprod(0).NombreFoto8
                    lblimagen9.Text = listaprod(0).NombreFoto9
                    lblimagen10.Text = listaprod(0).NombreFoto10
                    lblimagen11.Text = listaprod(0).NombreFoto11
                    lblimagen12.Text = listaprod(0).NombreFoto12
                    hNombreImagen1.Value = listaprod(0).NombreFoto1
                    hNombreImagen2.Value = listaprod(0).NombreFoto2
                    hNombreImagen3.Value = listaprod(0).NombreFoto3
                    hNombreImagen4.Value = listaprod(0).NombreFoto4
                    hNombreImagen5.Value = listaprod(0).NombreFoto5
                    hNombreImagen6.Value = listaprod(0).NombreFoto6
                    hNombreImagen7.Value = listaprod(0).NombreFoto7
                    hNombreImagen8.Value = listaprod(0).NombreFoto8
                    hNombreImagen9.Value = listaprod(0).NombreFoto9
                    hNombreImagen10.Value = listaprod(0).NombreFoto10
                    hNombreImagen11.Value = listaprod(0).NombreFoto11
                    hNombreImagen12.Value = listaprod(0).NombreFoto12
                    hRutaimagen1.Value = listaprod(0).rutaImagen
                    imagen1.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto1
                    imagen2.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto2
                    imagen3.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto3
                    imagen4.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto4
                    imagen5.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto5
                    imagen6.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto6
                    imagen7.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto7
                    imagen8.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto8
                    imagen9.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto9
                    imagen10.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto10
                    imagen11.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto11
                    imagen12.ImageUrl = "~/" & listaprod(0).rutaImagen & "\" & listaprod(0).NombreFoto12
                End If
            End If
        End If
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs)
        Dim script As String = ""
        Try

            If lblimagen1.Text = "" Then
                lblimagen1.Text = ImgProductoDefault
            Else

            End If
            lblMensajeAtencion.Text = ""

            Dim objproducto As producto = New producto() With {
               .idProducto = Convert.ToInt32(Request.QueryString("idProducto")),
               .Nombre = UCase(lblNombre.Text),
               .rutaImagen = hRutaimagen1.Value,
               .NombreFoto1 = GuardarImagen(fileUpload1, hNombreImagen1.Value, 1, ImgCarpetaProductos, imagen1),
               .NombreFoto2 = GuardarImagen(fileUpload2, hNombreImagen2.Value, 2, ImgCarpetaProductos, imagen2),
               .NombreFoto3 = GuardarImagen(fileUpload3, hNombreImagen3.Value, 3, ImgCarpetaProductos, imagen3),
               .NombreFoto4 = GuardarImagen(fileUpload4, hNombreImagen4.Value, 4, ImgCarpetaProductos, imagen4),
               .NombreFoto5 = GuardarImagen(fileUpload5, hNombreImagen5.Value, 5, ImgCarpetaProductos, imagen5),
               .NombreFoto6 = GuardarImagen(fileUpload6, hNombreImagen6.Value, 6, ImgCarpetaProductos, imagen6),
               .NombreFoto7 = GuardarImagen(fileUpload7, hNombreImagen7.Value, 7, ImgCarpetaProductos, imagen7),
               .NombreFoto8 = GuardarImagen(fileUpload8, hNombreImagen8.Value, 8, ImgCarpetaProductos, imagen8),
               .NombreFoto9 = GuardarImagen(fileUpload9, hNombreImagen9.Value, 9, ImgCarpetaProductos, imagen9),
               .NombreFoto10 = GuardarImagen(fileUpload10, hNombreImagen10.Value, 10, ImgCarpetaProductos, imagen10),
               .NombreFoto11 = GuardarImagen(fileUpload11, hNombreImagen11.Value, 11, ImgCarpetaProductos, imagen11),
               .NombreFoto12 = GuardarImagen(fileUpload12, hNombreImagen12.Value, 12, ImgCarpetaProductos, imagen12)}
            Dim idgenerado As Integer = 0
            If lblMensajeAtencion.Text <> "" Then
                script = "$(function() { showModalMensaje(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
                Exit Sub
            End If

            Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
                Dim query As String = ""
                Dim cmd As SqlCommand
                Dim mensaje As String = ""
                Dim resultado As Boolean = False
                mensaje = ""
                If Request.QueryString("Accion") = "F" Then
                    resultado = New CD_Producto().GuardarFotos(objproducto, mensaje)
                Else
                    'resultado = New CD_Producto().Eliminar(objproducto, mensaje)
                End If

                If mensaje = "" Then
                    Response.Redirect("producto_lista.aspx")
                Else
                    lblMensajeAtencion.Text = mensaje
                    Script = "$(function() { showModalMensaje(); }); "
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Function GuardarImagen(ByVal sFileupload As Object, ByVal sNombreImagen As String, ByVal Numerofoto As Integer, ByVal carpetadestino As String, ByVal img As Image) As String
        Dim targetDirectory As String = Server.MapPath("~/" & carpetadestino & "/")
        Dim nprod As Integer = Request.QueryString("idProducto")
        Dim nuevoNombreArchivo As String = ""
        If sNombreImagen = ImgProductoDefault Then

        Else

            ' Verificar si se ha seleccionado un archivo
            If Not sFileupload.HasFile Then
                Return sNombreImagen
                Exit Function
            Else
                Try


                    ' Obtener la extensión del archivo
                    Dim extension As String = Path.GetExtension(sFileupload.FileName).ToLower()

                    ' Validar la extensión de la imagen
                    If extension = ".jpg" OrElse extension = ".jpeg" OrElse extension = ".png" OrElse extension = ".gif" Then


                        ' Generar un nuevo nombre para la imagen
                        Dim nuevoNombre As String = nprod.ToString().PadLeft(5, "0"c) & "_" & Numerofoto.ToString().PadLeft(5, "0"c) & extension
                        sNombreImagen = nuevoNombre
                        ' Definir la ruta de la carpeta donde se guardará la imagen
                        ' Combinar la ruta de la carpeta con el nuevo nombre de la imagen
                        Dim rutaCompleta As String = Path.Combine(targetDirectory, nuevoNombre)

                        ' Eliminar imagen anterior si no es la imagen por defecto
                        Dim rutaAnterior As String = Path.Combine(targetDirectory, sNombreImagen)
                        If sNombreImagen <> ImgProductoDefault AndAlso File.Exists(rutaAnterior) Then
                            Try
                                File.Delete(rutaAnterior)
                            Catch ex As Exception
                                lblMensajeAtencion.Text = "No se pudo eliminar la imagen anterior: " & ex.Message
                            End Try
                        End If

                        If Not Directory.Exists(targetDirectory) Then
                            Directory.CreateDirectory(targetDirectory)
                        Else

                        End If

                        ' Combinar la ruta de la carpeta con el nuevo nombre de la imagen
                        'Dim rutaCompleta As String = Path.Combine(targetDirectory, nuevoNombre)

                        ' Guardar la imagen en la carpeta especificada
                        sFileupload.SaveAs(rutaCompleta)

                        ' Mostrar la imagen cargada en el control Image
                        img.ImageUrl = "~/" & carpetadestino & "/" & nuevoNombre

                        ' Mostrar un mensaje de éxito
                        lblMensajeAtencion.ForeColor = System.Drawing.Color.Red
                        lblMensajeAtencion.Text = ""
                    Else
                        ' Mostrar mensaje de error si el archivo no es una imagen válida
                        'lblMensajeAtencion.Text = "Error al procesar el archivo: " & ex.Message
                        lblMensajeAtencion.ForeColor = System.Drawing.Color.Red
                        sNombreImagen = ImgProductoDefault
                        lblMensajeAtencion.Text = "Solo se permiten archivos de imagen (.jpg, .jpeg, .png, .gif)."
                    End If
                Catch ex As Exception
                    sNombreImagen = ImgProductoDefault
                    lblMensajeAtencion.ForeColor = System.Drawing.Color.Red
                    lblMensajeAtencion.Text = "Ocurrió un error al cargar la imagen: Solo se permiten archivos de imagen (.jpg, .jpeg, .png, .gif). " & ex.Message
                Finally
                    lblMensajeAtencion.ForeColor = System.Drawing.Color.Red
                    ' Manejar cualquier error que ocurra
                    'sNombreImagen = "sinfoto.jpg"
                    lblMensajeAtencion.Text = ""
                End Try
            End If
            If lblMensajeAtencion.Text <> "" Then
                Dim Script As String = "$(function() { showModalMensaje(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
            End If

        End If
        Return sNombreImagen
    End Function
    Private Function ValidaExtension(ByVal sExtension As String) As Boolean
        Select Case sExtension
            Case ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".jfif"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Protected Sub btnDelImg1_Click(sender As Object, e As EventArgs) Handles btnDelImg1.Click
        imagen1.ImageUrl = CarpetaImagen
        lblimagen1.Text = ImgProductoDefault
        hNombreImagen1.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg2_Click(sender As Object, e As EventArgs) Handles btnDelImg2.Click
        imagen2.ImageUrl = CarpetaImagen
        lblimagen2.Text = ImgProductoDefault
        hNombreImagen2.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg3_Click(sender As Object, e As EventArgs) Handles btnDelImg3.Click
        imagen3.ImageUrl = CarpetaImagen
        lblimagen3.Text = ImgProductoDefault
        hNombreImagen3.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg4_Click(sender As Object, e As EventArgs) Handles btnDelImg4.Click
        imagen4.ImageUrl = CarpetaImagen
        lblimagen4.Text = ImgProductoDefault
        hNombreImagen4.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg5_Click(sender As Object, e As EventArgs) Handles btnDelImg5.Click
        imagen5.ImageUrl = CarpetaImagen
        lblimagen5.Text = ImgProductoDefault
        hNombreImagen5.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg6_Click(sender As Object, e As EventArgs) Handles btnDelImg6.Click
        imagen6.ImageUrl = CarpetaImagen
        lblimagen6.Text = ImgProductoDefault
        hNombreImagen6.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg7_Click(sender As Object, e As EventArgs) Handles btnDelImg7.Click
        imagen7.ImageUrl = CarpetaImagen
        lblimagen7.Text = ImgProductoDefault
        hNombreImagen7.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg8_Click(sender As Object, e As EventArgs) Handles btnDelImg8.Click
        imagen8.ImageUrl = CarpetaImagen
        lblimagen8.Text = ImgProductoDefault
        hNombreImagen8.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg9_Click(sender As Object, e As EventArgs) Handles btnDelImg9.Click
        imagen9.ImageUrl = CarpetaImagen
        lblimagen9.Text = ImgProductoDefault
        hNombreImagen9.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg10_Click(sender As Object, e As EventArgs) Handles btnDelImg10.Click
        imagen10.ImageUrl = CarpetaImagen
        lblimagen10.Text = ImgProductoDefault
        hNombreImagen10.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg11_Click(sender As Object, e As EventArgs) Handles btnDelImg11.Click
        imagen11.ImageUrl = CarpetaImagen
        lblimagen11.Text = ImgProductoDefault
        hNombreImagen11.Value = ImgProductoDefault
    End Sub
    Protected Sub btnDelImg12_Click(sender As Object, e As EventArgs) Handles btnDelImg12.Click
        imagen12.ImageUrl = CarpetaImagen
        lblimagen12.Text = ImgProductoDefault
        hNombreImagen12.Value = ImgProductoDefault
    End Sub
End Class