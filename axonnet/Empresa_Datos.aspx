<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="Empresa_Datos.aspx.vb" Inherits="axonnet.Empresa_Datos" %>
<%@ Import Namespace="capaEntidad" %>
<%@ Import Namespace="capaDatos.conexion" %>
<%@ Import Namespace="capaNegocio" %>
<%@ Import Namespace="System.data" %>
<%@ Import Namespace="System.data.sqlclient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="css/check_css.css" rel="stylesheet" />
  <link href="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/style.min.css" rel="stylesheet" />
  <link href="css/styles.css" rel="stylesheet" />
  <script src="https://use.fontawesome.com/releases/v6.3.0/js/all.js" crossorigin="anonymous"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="formMaster" runat="server">
 <div id="layoutSidenav_content">
           <div class="row justify-content-center">
                <div class="col-lg-8">                                     
                    <%-- <form class="form-horizontal">--%>
                    <div class="card shadow-lg border-0 rounded-lg mt-8 ">
                        <div class="card-header  bg-warning text-black">
                            <h3 class="text-center font-weight-light my-2"><strong><asp:Label runat="server" ID="lblTitulo" >Datos Empresa</asp:Label></strong></h3>                                   
                        </div>                                            
                        <div class="card-body"> 
                            <asp:Panel runat="server" ID="PanelDatos" CssClass="align-content-center">
                               <asp:HiddenField runat="server" ID="txtidMoneda"/>
                                <div class="form-group">
                                     <div class="form-floating mb-3">                                        
                                        <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control text-uppercase"></asp:TextBox>
                                        <label for="txtRazonSocial">Razón Social</label>
                                     </div>
                                </div>
                                <div class="form-group">
                                     <div class="form-floating mb-3">   
                                        <asp:TextBox ID="txtNombreComercial" runat="server" CssClass="form-control text-uppercase"></asp:TextBox>
                                        <label for="txtNombreComercial">Nombre Comercial:</label>
                                     </div>
                                </div>
                                <div class="form-group">
                                     <div class="form-floating mb-3">  
                                         <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label for="txtDireccion">Dirección:</label>                                        
                                     </div>
                                </div>
                                <div class="form-group">
                                     <div class="form-floating mb-3">
                                         <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control"></asp:TextBox>                                       
                                        <label for="txtCodigoPostal">Código Postal:</label>
                                        </div>
                                </div>
                                <div class="form-group">
                                     <div class="form-floating mb-3">  
                                        <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label for="txtLocalidad">Localidad:</label>
                                     </div>
                                </div>
                              <div class="form-group">
                                     <div class="form-floating mb-3">

                                         <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control"></asp:TextBox>
                                         <label for="txtProvincia">Provincia:</label>
                                      </div>
                                </div>
                                 <div class="form-group">
                                     <div class="form-floating mb-3">
                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label for="txtTelefono">Teléfono:</label>
                                      </div>  
                                </div>
                                 <div class="form-group">
                                     <div class="form-floating mb-3">
                                       <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                         <label for="txtCelular">Celular:</label>
                                       </div> 
                                </div>
                                 <div class="form-group">
                                     <div class="form-floating mb-3">
                                        <asp:TextBox ID="txtWhatsApp" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label for="txtWhatsApp">WhatsApp:</label>
                                         </div>
                                </div>
                                <div class="form-group">
                                     <div class="form-floating mb-3"> 
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>                                         
                                        <label for="txtEmail">Email:</label>
                                    </div>
                                </div>
                                <div class="form-group">
                                     <div class="form-floating mb-3"> 
                                          <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-control"></asp:TextBox>                                       
                                        <label for="txtNumeroDocumento">Número Documento:</label>
                                    </div>
                                </div>
                                <div class="form-group">
                                     <div class="form-floating mb-3">   
                                        <asp:TextBox ID="txtInicioActividades" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                        <label for="txtInicioActividades">Inicio de Actividades:</label>
                                     </div>
                                </div>
                                <div class="form-group">
                                     <div class="form-floating mb-3">   
                                        <asp:TextBox ID="txtIngresosBrutos" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label for="txtIngresosBrutos">Ingresos Brutos:</label>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                     <div class="form-floating mb-3">   
                                        <asp:TextBox ID="txtRedondeo" runat="server" CssClass="form-control text-end"></asp:TextBox>
                                        <label for="txtRedondeo">Redondeo:</label>
                                     </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-control-sm">
                                        <label class="form-check-inline" for="chkEstado">Activo </label>
                                        <asp:CheckBox ID="chkEstado" runat="server" CssClass="form-check-inline" />
                                    </div>
                                </div>
                                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                                <asp:Button ID="BtnVolver" CssClass="btn btn-success" runat="server" Text="Volver" PostBackUrl="~/DefaultAdmin.aspx"/>
                             </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
   
 </div>   
</asp:Content>
