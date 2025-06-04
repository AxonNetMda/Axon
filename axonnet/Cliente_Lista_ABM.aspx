<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="Cliente_Lista_ABM.aspx.vb" Inherits="axonnet.Cliente_Lista_ABM" %>
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
         
     <br />
     <div class="container">
            <div class="row justify-content-center">
         <ol class="breadcrumb mb-4">
            <li class="breadcrumb-item"><a href="Default.aspx">ADMINISTRACION</a></li><li class="breadcrumb-item"><a href="cliente_lista.aspx">Clientes Listado</a></li>
            <li class="breadcrumb-item active">Ficha Cliente</li>
        </ol>

                <div class="col-lg-12">                                     
                    <%-- <form class="form-horizontal">--%>
                    <div class="card shadow-lg border-0 rounded-lg mt-8 ">
                        <div class="card-header  bg-warning text-black">
                            <h3 class="text-center font-weight-light my-1"><strong><asp:Label runat="server" ID="lblTitulo" ></asp:Label></strong></h3>                                   
                        </div>                       
                        <div class="card-body justify-content-center"> 
                           <asp:Panel runat="server" ID="PanelDatos" CssClass="justify-content-center">
                               <asp:HiddenField runat="server" ID="txtidCliente"/>
                               <asp:HiddenField runat="server" ID="txtidResponsable"/>
                               <asp:HiddenField runat="server" ID="txtIdTipoDocumento"/>
                                 <div class="row justify-content-center">                                       
                                     <div class="row">
                                         <div class="col-md-6">
                                              <h5><strong><i class="fas fa-user"></i> Datos cliente</strong></h5>
                                             <div class="form-group">
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtApellido"></asp:TextBox>
                                                        <label for="txtApellido">Apellido</label>                                   
                                                    </div>
                                                </div>
                                             <div class="form-group">
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtNombre"></asp:TextBox>
                                                        <label for="txtNombre">Nombre Comercial</label>                                   
                                                    </div>
                                                </div>
                                             <div class="form-group">
                                                        <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtDireccion"></asp:TextBox>
                                                        <label for="txtDireccion">Direccion</label>                                   
                                                    </div>
                                                </div> 
                                             <div class="form-group">
                                                        <div class="form-floating mb-3">
                                                        <asp:DropDownList runat="server" CssClass="form-control" id="cboPais" Width="100"></asp:DropDownList>
                                                        <label for="cboPais">Pais</label>                                   
                                                    </div>
                                                </div>    
                                             <div class="form-group">
                                                        <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtLocalidad"></asp:TextBox>
                                                        <label for="txtlocalidad">Localidad</label>                                   
                                                    </div>
                                                </div>  
                                             <div class="form-group">
                                                        <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtCodigoPostal" Width="200"></asp:TextBox>
                                                        <label for="txtCodigoPostal">Codigo Postal</label>                                   
                                                    </div>
                                                </div>                        
                                             
                                             <div class="form-group">
                                                     <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtProvincia"></asp:TextBox>
                                                        <label for="txtProvincia">Provincia</label>                                   
                                                    </div>
                                                </div>          
                                             <div class="form-group row align-content-start border-1 border-dark">
                                                 <h5><strong><i class="fas fa-phone"></i>Telefono/Movil/Correo</strong></h5>
                                                 <div class="col-md-2">
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList runat="server" CssClass="form-control" id="cboMovil" Width="100"></asp:DropDownList>
                                                            <label for="cboMovil">Telefono</label> 
                                                        </div>
                                                 </div>
                                                 <div class="col-md-3">
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtTelefonoPais"  Width="150"></asp:TextBox>
                                                        <label for="txtTelefono">Pais</label>  
                                                    </div>
                                                </div>
                                                 <div class="col-md-2">
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtTelefonoArea" Width="100"></asp:TextBox>
                                                        <label for="txtTelefonoArea">Area</label>     
                                                    </div>
                                                </div>
                                                 <div class="col-md-3">
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox runat="server" class="form-control" id="txtTelefonoNumero" Width="200"></asp:TextBox>
                                                            <label for="txtTelefonoNumero">Numero</label>                                
                                                    </div>
                                                 </div>
                                                 <div class="form-group">
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtEmail" TextMode="Email"></asp:TextBox>
                                                        <label for="txtEmail">Correo</label>                                   
                                                    </div>
                                                </div>
                                            </div>                                              
                                         </div>
                                         <div class="col-md-6">
                                           
                                            <h5><strong><i class="fas fa-coins"></i> Datos Fiscales</strong></h5>
                                             <div class="form-group">
                                                  <div class="form-floating mb-3">
                                                      <asp:DropDownList runat="server" CssClass="form-control" id="cboResponsables" ></asp:DropDownList>
                                                      <label for="cboResponsables">Tipo de Responsable</label>
                                                     </div>         
                                             </div>
                                            <div class="form-group row align-content-start border-1 border-dark">
                                               
                                             
                                                 <div class="col-md-4">
                                                    <div class="form-floating mb-3">
                                                            <asp:DropDownList runat="server" CssClass="form-control" id="cboTipoDocumento" ></asp:DropDownList>
                                                            <label for="cboTipoDocumento">Tipo Documento</label>                                   
                                                    </div>
                                                </div>
                                                 <div class="col-md-4">
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtNumeroDocumento"></asp:TextBox>
                                                        <label for="txtNumeroDocumento">Numero</label>                                   
                                                    </div>
                                                </div>
                                                 <div class="col-md-4">
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtFechaAlta" TextMode="Date"></asp:TextBox>
                                                        <label for="txtInicioActividades">Fecha Alta</label>                                   
                                                    </div>
                                                </div>
                                            </div>  
                                             <hr />
                                            <div class="form-group">
                                                <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control text-end" id="txtSaldo" Width="200"></asp:TextBox>
                                                    <label for="txtWhatsApp">Saldo</label>                                   
                                                </div>
                                            </div>  
                                            <div class="form-group">
                                                <div class="form-floating mb-3">
                                                        <asp:DropDownList runat="server" class="form-control" id="cboEstado" placeholder="Estado" Width="200"></asp:DropDownList>
                                                        <label for="cboEstado">Estado</label> 
                                                    <asp:HiddenField runat="server" ID="txtIdEstado" />
                                                </div>
                                            </div>
                                       </div> 
                                    </div> 
                                </div>
                            </asp:Panel> 
                            </div>  

                        </div>                                
                        <footer class="py-4 bg-light mt-auto">
                            <div class="container-fluid px-4">
                                <div class="d-flex align-items-end justify-content-between small">
                                    <asp:button runat="server" id="BtnCancelar" cssclass="btn btn-success" Text="Cancelar" PostBackUrl="~/cliente_lista.aspx" />
                                    <asp:button runat="server" Id="BtnGuardar" cssclass="btn btn-primary" Text="Aceptar " OnClick="BtnGuardar_Click" />
                                </div>
                            </div>
                        </footer>

                        </div>                  
                </div>                       
              </div>                                        
            
    <%--Abrir modal para Validar respuesta                                                                                                                                                                      --%>
<div class="modal fade" id="MdlAtencion" tabindex="-1" role="dialog" aria-labelledby="myMdlAtencion" data-bs-backdrop="static" style="display:none">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">ATENCION</h5><button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button> 
      </div>
      <div class="modal-body text-black">
            <asp:Label runat="server" id="lblMensajeAtencion" CssClass="form-label" Text=""></asp:Label>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>

      </div>
    </div>
  
  </div>
</div>   
<script>
    function showModalAtencion() {
        $("#MdlAtencion").modal("show");
    }
</script>
</asp:Content>
