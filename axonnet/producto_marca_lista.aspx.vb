
Imports System.Data.SqlClient
Imports CapaDatos.Conexion
Imports axonnet.funciones
Public Class producto_marca_lista
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Dim verificaTabla As String = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'producto_marca')
                                        BEGIN
                                            CREATE TABLE [dbo].[producto_marca] (
                                                [idMarca] INT NOT NULL IDENTITY(1,1),
                                                [Nombre] VARCHAR(100) COLLATE Modern_Spanish_CI_AS NULL,
                                                [Estado] BIT NULL DEFAULT ((1))
                                            ) ON [PRIMARY];
                                        END"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim script As String
            Dim mensaje As String = VerificarYCrearTabla(verificaTabla)
            If mensaje <> "" Then
                lblMensajeAtencion.Text = "Error en la ccreacion de la tabla:" & mensaje
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlAtencion", script, True)
            End If
        End If
        scomando = "SELECT  * FROM producto_marca ORDER BY Nombre"
        SqlMarca.SelectCommand = scomando

    End Sub
    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand

        If e.CommandName = "editar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_marca_lista_ABM.aspx?idMarca=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=MODIFICAR MARCA &Accion=E")
        ElseIf e.CommandName = "borrar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_marca_lista_ABM.aspx?idMarca=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=ELIMINAR MARCA &Accion=B")
        End If
    End Sub
    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("producto_marca_Lista_ABM.aspx?idCategoria=0&TituloForm=CATEGORIA MARCA&Accion=N")
    End Sub
    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If IsPostBack Then
            Dim filtro As String = ""
            'Dim dt As DataTable = ObtenerDatos()
            'Dim dv As New DataView(dt)
            'dv.RowFilter = String.Format("Nombre LIKE '%{0}%' OR Domicilio LIKE '%{0}%'", txtBuscar.Text)
            If Trim(txtBuscar.Text) <> "" Then
                filtro = " WHERE Nombre LIKE '%" & txtBuscar.Text & "%'"
            End If
            SqlMarca.SelectCommand = "SELECT * FROM producto_marca " & filtro & " ORDER BY Nombre"
            dgvData.DataBind()
        End If
    End Sub

End Class