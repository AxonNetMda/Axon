Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms
Imports capaAdministracion.funciones
Imports capaAdministracion.MisFunciones
Imports CapaDatos
Imports capaEntidad

Public Class Usuarios_lista
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'If IsPostBack AndAlso fileUpload1.HasFile Then
        '    Try
        '        ' Guardar el archivo en una carpeta del servidor
        '        Dim fileName As String = Path.GetFileName(fileUpload1.PostedFile.FileName)
        '        Dim filePath As String = Server.MapPath("~/Imagenes/") & fileName
        '        fileUpload1.SaveAs(filePath)

        '        ' Actualizar el control de imagen para mostrar la imagen seleccionada
        '        imgMostrar.ImageUrl = "Imagenes/" & fileName

        '        ' Actualizar el control de etiqueta con el nombre de la imagen seleccionada
        '        lblNombreImagen.Text = "Nombre de la Imagen: " & fileName

        '        ' Mensaje de éxito
        '        lblNombreImagen.ForeColor = System.Drawing.Color.Green
        '    Catch ex As Exception
        '        ' Manejar el error
        '        lblNombreImagen.Text = "Error al subir la imagen: " & ex.Message
        '        lblNombreImagen.ForeColor = System.Drawing.Color.Red
        '    End Try
        '    'LoadImages()
        'Else
        'End If
        scomando = "SELECT  usuario.idUsuario, usuario.Nombre, usuario.Domicilio, usuario.CodigoPostal, usuario.localidad, usuario.provincia, 
                            usuario.celular,  usuario.Email, usuario.Clave, usuario.idRol, rol.Nombre AS RolNombre,usuario.Estado, 
		                    isnull(FotoNombre,'no-image.jpg') as FotoNombre, isnull(FotoPath,'Imagenes/') as FotoPath
	                        FROM usuario INNER JOIN rol ON usuario.idRol = rol.idRol"
        SqlUsuarios.SelectCommand = scomando
        If Not IsPostBack Then
            '    ' Establecer la imagen actual desde el servidor
            '    imgMostrar.ImageUrl = "Imagenes/no-image.jpg" ' Ruta de la imagen actual
            '    sMensaje = ""
            '    Dim i As Integer = 0
            '    Dim listarol As List(Of rol) = New CD_Rol().Listar
            '    For Each item As rol In listarol
            '        cboRol.Items.Insert(i, New ListItem(item.Nombre, item.idRol))
            '        i += 1
            '    Next

            '    cboRol.DataTextField = "Nombre"
            '    cboRol.DataValueField = "idRol"
            '    cboRol.SelectedIndex = 0

            '    cboEstado.Items.Insert(0, New ListItem("Activo", 1))
            '    cboEstado.Items.Insert(1, New ListItem("No Activo", 0))
            '    cboEstado.DataTextField = "Activo"
            '    cboEstado.DataValueField = 1
            '    cboEstado.SelectedIndex = 0

            '    'dgvData.DataSource = sComando
            '    'dgvData.DataBind()
            'BindGridView()
        Else

        End If


        'dgvData.DataSource = SqlUsuarios
        'dgvData.DataBind()
    End Sub


    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand

        If e.CommandName = "editar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("Usuarios_lista_ABM.aspx?idUsuario=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=MODIFICAR USUARIO &Accion=E")

        ElseIf e.CommandName = "borrar" Then
            'Dim Script As String = ""
            'modalMensaje = "Error al eliminar este registro"
            'Script = "$(function() { showModalAtencion(); }); "
            'ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("Usuarios_lista_ABM.aspx?idUsuario=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=eLIMINAR USUARIO &Accion=B")
        ElseIf e.CommandName = "blanqueraclave" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("Usuarios_CambiarPassword.aspx?idUsuario=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text))
        End If
    End Sub
    Private Sub limpiar()
        txtIdUsuario.Text = 0
        txtNombre.Text = ""
        txtEmail.Text = ""
        cboRol.SelectedIndex = 0
        cboEstado.SelectedIndex = 0
    End Sub
    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("Usuarios_lista_ABM.aspx?idUsuario=0&TituloForm=USUARIO NUEVO&Accion=N")
        Exit Sub
        limpiar()
        Dim Script As String = ""
        Script = "$(function() { showModalNuevo(); }); "
        ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
    End Sub

    Private Sub LoadImages()
        Dim query As String = "SELECT * FROM usuario order by nombre"
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("cadena2").ConnectionString

        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)
                Dim i As Integer = 0
                ' Añadir una columna de URL de imagen al DataTable
                'dt.Columns.Add("Foto", GetType(String))
                For Each row As DataRow In dt.Rows

                    If IsDBNull(dt.Rows(i).Item("FotoNombre").ToString) Then
                        row("fotonombre") = "Imagenes/no-image.jpg"
                    Else
                        row("fotonombre") = "GetImage.aspx?id=" & row("Idusuario")
                    End If
                    i += 1
                Next

                'dgvData.DataSource = dt
                'dgvData.DataBind()
                conn.Close()
            End Using
        End Using
    End Sub

    Protected Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs)
        'If fileUpload1.PostedFile IsNot Nothing AndAlso fileUpload1.PostedFile.ContentLength > 0 Then
        '    Dim fileName As String = Path.GetFileName(fileUpload1.PostedFile.FileName)
        '    Dim fileType As String = fileUpload1.PostedFile.ContentType
        '    Dim fileContent As Byte() = Nothing

        '    Using binaryReader As New BinaryReader(fileUpload1.PostedFile.InputStream)
        '        fileContent = binaryReader.ReadBytes(fileUpload1.PostedFile.ContentLength)
        '    End Using

        '    GuardarImagenEnBaseDeDatos(Val(txtIdUsuario.Text), fileName, fileType, fileContent)
        'End If
        If Trim(txtNombre.Text) = "" Then
            lblMensajeAtencion.Text = "El nombre del usuario no puede ser vacio"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
        End If
        If Trim(txtDomicilio.Text) = "" Then
            lblMensajeAtencion.Text = "El domicilio del usuario no puede ser vacio"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
        End If
        Dim objusuario As usuario = New usuario() With {
            .idusuario = Convert.ToInt32(txtIdUsuario.Text),
            .Nombre = txtNombre.Text,
            .Email = txtEmail.Text,
            .Clave = "",
            .FotoNombre = fileUpload1.FileName,
            .Domicilio = txtDomicilio.Text,
            .CodigoPostal = txtCodigoPostal.Text,
            .Localidad = txtLocalidad.Text,
            .Provincia = txtProvincia.Text,
            .FotoPath = "\imagenes\",
            .oRol = New rol() With {.idRol = Convert.ToInt32(cboRol.SelectedValue)},
            .Estado = Convert.ToInt32(cboEstado.SelectedValue)}
        Dim idusuariogenerado As Integer = 0


        Using conn As New SqlConnection(Conexion.conectar.Cadena)
            Dim query As String = ""
            Dim cmd As SqlCommand
            If Convert.ToInt32(txtIdUsuario.Text) = 0 Then
                query = "INSERT INTO usuario (Nombre, Clave, FotoNombre, FotoPath, Email, idRol, Estado) VALUES (@Nombre, @Clave, @FotoNombre, @fotopath, @Email, @idRol, @Estado)"
                cmd = New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Nombre", objusuario.Nombre)
                cmd.Parameters.AddWithValue("@Clave", objusuario.Clave)
                cmd.Parameters.AddWithValue("@FotoNombre", objusuario.FotoNombre)
                cmd.Parameters.AddWithValue("@FotoPath", objusuario.FotoPath)
                cmd.Parameters.AddWithValue("@Email", objusuario.Email)
                cmd.Parameters.AddWithValue("@idRol", objusuario.oRol.idRol)
                cmd.Parameters.AddWithValue("@Estado", Convert.ToBoolean(objusuario.Estado))
            Else
                query = "UPDATE usuario  set Nombre=@Nombre, Clave=@Clave, FotoNombre=@FotoNombre, FotoPath=@FotoPath, Email=@Email, idRol=@idRol, Estado=@Estado WHERE idUsuario= @idUsuario"
                cmd = New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IdUsuario", objusuario.idusuario)
                cmd.Parameters.AddWithValue("@Nombre", objusuario.Nombre)
                cmd.Parameters.AddWithValue("@Clave", objusuario.Clave)
                cmd.Parameters.AddWithValue("@FotoNombre", objusuario.FotoNombre)
                cmd.Parameters.AddWithValue("@FotoPath", objusuario.FotoPath)
                cmd.Parameters.AddWithValue("@Email", objusuario.Email)
                cmd.Parameters.AddWithValue("@idRol", objusuario.oRol.idRol)
                cmd.Parameters.AddWithValue("@Estado", objusuario.Estado)
            End If
            conn.Open()
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try

        End Using
    End Sub

    Private Sub GuardarImagenEnBaseDeDatos(ByVal idUsuario As Integer, ByVal Fotonombre As String, ByVal tipo As String, ByVal Foto As Byte())
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("cadena2").ToString()
        Using conn As New SqlConnection(connectionString)
            Dim query As String = ""
            Dim cmd As SqlCommand
            If ID = 0 Then
                query = "INSERT INTO usuario (FotoNombre, Foto) VALUES (@FotoNombre, @foto)"
                cmd = New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@FotoNombre", Fotonombre)
                cmd.Parameters.AddWithValue("@Tipo", tipo)
                cmd.Parameters.AddWithValue("@Foto", Foto)
            Else
                query = "INSERT INTO Imagenes (FotoNombre=@FotoNombre, Foto=@Foto WHERE idUsuario= @id)"
                cmd = New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario)
                cmd.Parameters.AddWithValue("@FotoNombre", Fotonombre)
                cmd.Parameters.AddWithValue("@Tipo", tipo)
                cmd.Parameters.AddWithValue("@Foto", Foto)
            End If


            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Dim id As Integer
        'If Integer.TryParse(txtIdUsuario.Text, id) AndAlso fileUpload1.HasFile Then
        '    Dim fileName As String = Path.GetFileName(fileUpload1.PostedFile.FileName)
        '    Dim fileType As String = fileUpload1.PostedFile.ContentType
        '    Dim fileContent As Byte() = Nothing

        '    Using binaryReader As New BinaryReader(fileUpload1.PostedFile.InputStream)
        '        fileContent = binaryReader.ReadBytes(fileUpload1.PostedFile.ContentLength)
        '    End Using

        '    'ActualizarArchivoEnBaseDeDatos(id, fileName, fileType, fileContent)
        '    imgMostrar.ImageUrl = "ObtenerImagen.ashx?id=" & id
        'End If
    End Sub
    Private Sub ActualizarArchivoEnBaseDeDatos(ByVal id As Integer, ByVal nombre As String, ByVal tipo As String, ByVal datos As Byte())
        Dim connectionString As String = "tu_conexion_a_la_base_de_datos"

        Using conn As New SqlConnection(connectionString)
            Dim query As String = "UPDATE Imagenes SET Nombre = @Nombre, Tipo = @Tipo, Datos = @Datos WHERE Id = @Id"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Id", id)
            cmd.Parameters.AddWithValue("@Nombre", nombre)
            cmd.Parameters.AddWithValue("@Tipo", tipo)
            cmd.Parameters.AddWithValue("@Datos", datos)

            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function ObtenerUltimoId() As Integer
        Dim ultimoId As Integer
        Dim connectionString As String = Conexion.conectar.Cadena

        Using conn As New SqlConnection(connectionString)
            Dim query As String = "SELECT MAX(Id) FROM Imagenes"
            Dim cmd As New SqlCommand(query, conn)

            conn.Open()
            ultimoId = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return ultimoId
    End Function
    Private Sub BindGridView()
        dgvData.DataSource = ObtenerDatos()
        dgvData.DataBind()
    End Sub
    Private Function ObtenerDatos() As DataTable
        If IsPostBack Then
            Dim dt As New DataTable()
            dt.Columns.Add("idUsuario")
            dt.Columns.Add("Nombre")
            dt.Columns.Add("Domicilio")
            dt.Columns.Add("CodigoPostal")
            dt.Columns.Add("Localidad")
            dt.Columns.Add("Provincia")
            dt.Columns.Add("Celular")
            dt.Columns.Add("Email")
            dt.Columns.Add("idRol")
            dt.Columns.Add("RolNombre")
            dt.Columns.Add("Estado")
            dt.Columns.Add("FotoNombre")
            dt.Columns.Add("FotoPath")


            For i = 0 To dgvData.Rows.Count - 1
                dt.Rows.Add(dgvData.Rows(i).Cells(0).Text, dgvData.Rows(i).Cells(1).Text, dgvData.Rows(i).Cells(2).Text, dgvData.Rows(i).Cells(3).Text, dgvData.Rows(i).Cells(4).Text,
                    dgvData.Rows(i).Cells(5).Text, dgvData.Rows(i).Cells(6).Text, dgvData.Rows(i).Cells(7).Text, dgvData.Rows(i).Cells(8).Text, dgvData.Rows(i).Cells(9).Text,
                    dgvData.Rows(i).Cells(10).Text, dgvData.Rows(i).Cells(11).Text, dgvData.Rows(i).Cells(12).Text)
            Next i
            Return dt
        End If
    End Function
    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If IsPostBack Then
            Dim filtro As String = ""
            'Dim dt As DataTable = ObtenerDatos()
            'Dim dv As New DataView(dt)
            'dv.RowFilter = String.Format("Nombre LIKE '%{0}%' OR Domicilio LIKE '%{0}%'", txtBuscar.Text)
            If Trim(txtBuscar.Text) <> "" Then
                filtro = " WHERE usuario.Nombre LIKE '%" & txtBuscar.Text & "%' OR usuario.Domicilio LIKE '%" & txtBuscar.Text & "%'"
            End If
            SqlUsuarios.SelectCommand = scomando & filtro
            dgvData.DataBind()
        End If
    End Sub
End Class