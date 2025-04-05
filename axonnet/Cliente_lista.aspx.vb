Public Class Cliente_lista
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Dim verificaTabla As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            verificaTabla = "CREATE TABLE [dbo].[cliente] (
            [idcliente] int NOT NULL IDENTITY(1,1) ,
            [nombre] varchar(100) COLLATE Modern_Spanish_CI_AS NULL ,
            [apellido] varchar(100) COLLATE Modern_Spanish_CI_AS NULL ,
            [correo] varchar(100) COLLATE Modern_Spanish_CI_AS NULL ,
            [clave] varchar(150) COLLATE Modern_Spanish_CI_AS NULL ,
            [Domicilio] varchar(100) COLLATE Modern_Spanish_CI_AS NULL ,
            [CodigoPostal] nchar(10) COLLATE Modern_Spanish_CI_AS NULL ,
            [Localidad] varchar(100) COLLATE Modern_Spanish_CI_AS NULL ,
            [Provincia] varchar(100) COLLATE Modern_Spanish_CI_AS NULL ,
            [Pais] varchar(100) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('ARGENTINA') ,
            [TelefonoMovil] bit NULL DEFAULT ((1)) ,
            [TelefonoPais] varchar(50) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ((54)) ,
            [TelefonoArea] varchar(50) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('-') ,
            [TelefonoNumero] varchar(50) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('-') ,
            [Saldo] decimal(18,2) NULL DEFAULT ((0)) ,
            [restablecer] bit NULL DEFAULT ((0)) ,
            [fechaAlta] datetime NULL ,
            [Estado] bit NULL DEFAULT ((1)) ,
            [idTipoResponsable] int NULL DEFAULT ((5)) ,
            [idTipoDocumento] int NULL DEFAULT ((96)) ,
            [Numerodocumento] varchar(20) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('-') 
            )
            ON [PRIMARY]"
            Dim script As String
            Dim mensaje As String = VerificarYCrearTabla(verificaTabla)
            If mensaje <> "" Then
                lblMensajeAtencion.Text = "Error en la ccreacion de la tabla:" & mensaje
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlAtencion", script, True)
            End If

        End If

        scomando = "SELECT  *, COALESCE(apellido, '') + ' ' + COALESCE(nombre, '') as NombreCompleto, 
                    COALESCE(cliente.TelefonoPais, '') + ' ' + COALESCE(cliente.TelefonoArea, '') + ''+ COALESCE(cliente.TelefonoNumero, '')  as TelefonoNumeroCompleto 
                    FROM cliente where idcliente>1 ORDER BY Apellido, Nombre"
        SqlProveedor.SelectCommand = scomando
        dgvData.PageSize = cboMostrar.SelectedValue
    End Sub
    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand

        If e.CommandName = "editar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("cliente_lista_ABM.aspx?idCliente=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=MODIFICAR CLIENTE &Accion=E")
        ElseIf e.CommandName = "borrar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("cliente_lista_ABM.aspx?idcliente=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=ELIMINAR CLIENTE &Accion=B")
        End If
    End Sub
    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("cliente_Lista_ABM.aspx?idCliente=0&TituloForm=CLIENTE NUEVO&Accion=N")
    End Sub
    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If IsPostBack Then
            Dim filtro As String = ""
            If Trim(txtBuscar.Text) <> "" Then
                filtro = " WHERE RazonSocial LIKE '%" & txtBuscar.Text & "%'"
            End If
            SqlProveedor.SelectCommand = "SELECT *, COALESCE(apellido, '') + ' ' + COALESCE(nombre, '') as NombreCompleto FROM cliente " & filtro & " ORDER BY Nombrecompleto"
            dgvData.DataBind()
        End If
    End Sub

End Class