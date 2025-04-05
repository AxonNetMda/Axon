<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="Empresa_Datos.aspx.vb" Inherits="axonnet.Empresa_Datos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="Scripts/WebForms/MSAjax/MicrosoftAjax.js"></script>
   <script src="Scripts/WebForms/MSAjax/MicrosoftAjaxWebForms.js"></script>
   <link href="css/styles.css" rel="stylesheet" />
    <link href="css/paraimagenes.css" rel="stylesheet" />
    <link href="css/check_css.css" rel="stylesheet" />
    <script type="text/javascript" src="js/plugins/bootstrap/bootstrap-file-input.js"></script>
    <script src="js/datatables/dataTables.fixedHeader.min.js"></script>
    <script src="js/datatables/jquery.dataTables.min.js"></script>
    <script src="js/datatables/jquery3701.js"></script>
    <link href="css/datatables/fixedHeader.dataTables.min.css" rel="stylesheet" />
    <link href="css/datatables/jquery.dataTables.min.css" rel="stylesheet" />  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="formMaster" runat="server">
     <%If Session("sNombreUsuario") = "" Then
                Response.Redirect("~/login.aspx?idUsuario=0")
            End If %>
  
    <div class="container">
       <div class="card-body">                                                                                                           
            <h2>Datos de la Empresas</h2>
            <div class="col-12">
                <label for="txtRazonSocial">Razón Social:</label>
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-text text-uppercase"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtNombreComercial">Nombre Comercial:</label>
                <asp:TextBox ID="txtNombreComercial" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtDireccion">Dirección:</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtCodigoPostal">Código Postal:</label>
                <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">  
                <label for="txtLocalidad">Localidad:</label>
                <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtProvincia">Provincia:</label>
                <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtTelefono">Teléfono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtCelular">Celular:</label>
                <asp:TextBox ID="txtCelular" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtWhatsApp">WhatsApp:</label>
                <asp:TextBox ID="txtWhatsApp" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtNumeroDocumento">Número Documento:</label>
                <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtInicioActividades">Inicio de Actividades:</label>
                <asp:TextBox ID="txtInicioActividades" runat="server" TextMode="Date" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="txtIngresosBrutos">Ingresos Brutos:</label>
                <asp:TextBox ID="txtIngresosBrutos" runat="server" CssClass="form-text"></asp:TextBox>
            </div>
            <div class="col-12">
                <label for="chkEstado">Estado:</label>
                <asp:CheckBox ID="chkEstado" runat="server" CssClass="form-check-inline" />
            </div>
            <div class="col-12">            
                <label for="txtRedondeo">Redondeo:</label>
                <asp:TextBox ID="txtRedondeo" runat="server" CssClass="form-text text-end"></asp:TextBox>
            </div>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>
