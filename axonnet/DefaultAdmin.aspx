<%@ Page Title="Home Page" Language="VB" Debug="true" MasterPageFile="Administracion.Master" AutoEventWireup="true" CodeBehind="DefaultAdmin.aspx.vb" Inherits="axonnet.Default2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="formmaster" runat="server">
     <div class="container-fluid px-4">
        <h5 class="mt-4 justify-content-center">Bienvenidos a la Administracion de <%=Session("sNombreSucursal") %> - <%= Date.Today.ToLongDateString %></h5>
        <hr />
     </div>


</asp:Content>
