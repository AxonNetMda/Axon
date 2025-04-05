Imports System.Data.SqlClient
Imports CapaDatos.Conexion

Partial Class Empresa_Datos
    Inherits System.Web.UI.Page

    Private connectionString As String = conectar.Cadena

    Private Sub Empresa_Datos_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarDatos()
        End If
    End Sub
    Private Sub CargarDatos()
        Dim query As String = "SELECT * FROM Empresa_Datos WHERE idEmpresa = @idEmpresa"
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@idEmpresa", 1) ' ID de empresa a cargar (puede venir de un parámetro)
                con.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    txtRazonSocial.Text = reader("RazonSocial").ToString()
                    txtNombreComercial.Text = reader("NombreComercial").ToString()
                    txtDireccion.Text = reader("Direccion").ToString()
                    txtCodigoPostal.Text = reader("CodigoPostal").ToString()
                    txtLocalidad.Text = reader("Localidad").ToString()
                    txtProvincia.Text = reader("Provincia").ToString()
                    txtTelefono.Text = reader("Telefono").ToString()
                    txtCelular.Text = reader("Celular").ToString()
                    txtWhatsApp.Text = reader("WhatsApp").ToString()
                    txtEmail.Text = reader("Email").ToString()
                    txtNumeroDocumento.Text = reader("NumeroDocumento").ToString()
                    txtInicioActividades.Text = Convert.ToDateTime(reader("InicioActividades")).ToString("yyyy-MM-dd")
                    txtIngresosBrutos.Text = reader("IngresosBrutos").ToString()
                    chkEstado.Checked = Convert.ToBoolean(reader("Estado"))
                    txtRedondeo.Text = reader("Redondeo").ToString()
                End If
            End Using
        End Using
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim query As String = "UPDATE Empresa_Datos SET RazonSocial=@RazonSocial, NombreComercial=@NombreComercial, Direccion=@Direccion, CodigoPostal=@CodigoPostal, Localidad=@Localidad, Provincia=@Provincia, Telefono=@Telefono, Celular=@Celular, WhatsApp=@WhatsApp, Email=@Email, NumeroDocumento=@NumeroDocumento, InicioActividades=@InicioActividades, IngresosBrutos=@IngresosBrutos, Estado=@Estado, Redondeo=@Redondeo WHERE idEmpresa=@idEmpresa"
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@idEmpresa", 1) ' ID de empresa a actualizar
                cmd.Parameters.AddWithValue("@RazonSocial", txtRazonSocial.Text)
                cmd.Parameters.AddWithValue("@NombreComercial", txtNombreComercial.Text)
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text)
                cmd.Parameters.AddWithValue("@CodigoPostal", txtCodigoPostal.Text)
                cmd.Parameters.AddWithValue("@Localidad", txtLocalidad.Text)
                cmd.Parameters.AddWithValue("@Provincia", txtProvincia.Text)
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text)
                cmd.Parameters.AddWithValue("@Celular", txtCelular.Text)
                cmd.Parameters.AddWithValue("@WhatsApp", txtWhatsApp.Text)
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
                cmd.Parameters.AddWithValue("@NumeroDocumento", txtNumeroDocumento.Text)
                cmd.Parameters.AddWithValue("@InicioActividades", Convert.ToDateTime(txtInicioActividades.Text))
                cmd.Parameters.AddWithValue("@IngresosBrutos", txtIngresosBrutos.Text)
                cmd.Parameters.AddWithValue("@Estado", chkEstado.Checked)
                cmd.Parameters.AddWithValue("@Redondeo", Convert.ToDecimal(txtRedondeo.Text))
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class