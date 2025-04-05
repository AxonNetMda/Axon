<%@ Page Title="Home Page" Language="VB" Debug="true" MasterPageFile="~/Administracion.Master" AutoEventWireup="true" CodeBehind="Default_Admin.aspx.vb" Inherits="axonnet.Default_Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="formmaster" runat="server">
     <%If Session("sNombreUsuario") = "" Then
             Response.Redirect("~/login.aspx?idUsuario=0")
         Else
             If Request.QueryString("sNombreUsuario") = "DEMO" Then
                 Session("email") = "demo@axonnet.store"
                 Session("rolnombre") = "DEMO"
                 Session("rol") = 6
                 Session("sNombreUsuario") = "DEMO"
                 Session("Idusuario") = 15
                 Session("sNombreEmpresa") = "DEMO"
             Else

             End If



         End If %>
  
     <div class="container-fluid px-4">
                        <h1 class="mt-4 justify-content-center">Bienvenidos a la Administracion</h1>
                        <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item active">Panel de  principal</li>
                        </ol>
     </div>


</asp:Content>
