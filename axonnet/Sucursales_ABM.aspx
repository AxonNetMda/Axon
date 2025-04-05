<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="Sucursales_ABM.aspx.vb" Inherits="axonnet.Sucursales_lista" %>
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
  <link href="css/check_css.css" rel="stylesheet" />
     <div class="container">
           <div class="row justify-content-center">
                <div class="col-lg-12">
                                     
                    <%-- <form class="form-horizontal">--%>
                    <div class="card shadow-lg border-0 rounded-lg mt-8 ">
                        <div class="card-header  bg-warning text-black">
                            <h3 class="text-center font-weight-light my-4"><strong><asp:Label runat="server" ID="lblTitulo" ></asp:Label></strong></h3>                                   
                        </div>                       
                        <div class="card-body"> 
                           <asp:Panel runat="server" ID="PanelDatos" CssClass="align-content-center">
                               <asp:HiddenField runat="server" ID="txtidSucursal"/>
                                 <div class="row justify-content-center">
                                     <div class="col-md-6">
                                          <%--<div class="form-group">
                                                <div class="form-floating mb-3">
                                                    <asp:label runat="server" class="form-control" id="lblidSucursal" Width="100" Text="0"></asp:label>
                                                    <label for="lblidSucursal">Id</label>                                   
                                                </div>--%>
                                        
                                            <div class="form-group">
                                                 <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control" id="txtNombre"></asp:TextBox>
                                                    <label for="inputSucursal">Nombre de la Sucursal</label>                                   
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                 <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control" id="txtDireccion"></asp:TextBox>
                                                    <label for="txtDireccion">Domicilio</label>                                   
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                 <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control" id="txtLocalidad"></asp:TextBox>
                                                    <label for="txtLocalidad">Localidad</label>                                   
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                 <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control" id="txtCodigoPostal"></asp:TextBox>
                                                    <label for="txtCodigoPostal">Codigo Postal</label>                                   
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                 <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtProvincia"></asp:TextBox>
                                                        <label for="txtProvincia">Provincia</label>                                   
                                                </div>
                                              </div>
                                         <div class="form-group">
                                                 <div class="form-floating mb-3">
                                                        <asp:checkbox runat="server" class="checkbox-wrapper-40" id="chkEsCentral" Text="Es deposito Central"></asp:checkbox>
    <%--                                                    <label for="chkEsCentral">Provincia</label>                                   --%>
                                                </div>
                                              </div>
                                     </div>
                                     <div class="col-md-6">
                                         <div class="form-group">
                                            <div class="form-floating mb-3">
                                                <asp:TextBox runat="server" class="form-control" id="txtTelefono"></asp:TextBox>
                                                <label for="txtTelefono">Telefono</label>                                   
                                            </div>
                                        </div>
                                        <div class="form-group">
                                                <div class="form-floating mb-3">
                                                <asp:TextBox runat="server" class="form-control" id="txtCelular"></asp:TextBox>
                                                <label for="txCelular">Celular</label>                                   
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control" id="txtEmail"></asp:TextBox>
                                                    <label for="txtEmail">Email</label>                                   
                                                </div>
                                        </div>
                                        <div class="form-group">
                                          <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control" id="txtInstagram"></asp:TextBox>
                                                    <label for="txtInstagran">Instagram</label>                                   
                                                </div>
                                            </div>
                                        <div class="form-group">
                                            <div class="form-floating mb-3">
                                                    <asp:DropDownList runat="server" class="form-control" id="cboEstado" placeholder="Estado"></asp:DropDownList>
                                                    <label for="cboEstado">Estado</label> 
                                                <asp:HiddenField runat="server" ID="txtIdEstado" />
                                            </div>
                                        </div>
                                     <div class="form-group">
                                             <div class="form-floating mb-3">
                                                    <asp:checkbox runat="server" class="checkbox-wrapper-40" id="chkPuedeComprar" Text="Puede realizar compras"></asp:checkbox>
<%--                                                    <label for="chkEsCentral">Provincia</label>                                   --%>
                                            </div>
                                          </div>
                                 </div>
                                </div>
                            </asp:Panel> 
                            </div>          
                      
                        <footer class="py-4 bg-light mt-auto">
                            <div class="container-fluid px-4">
                                <div class="d-flex align-items-end justify-content-between small">
                                    <asp:button runat="server" id="BtnCancelar" cssclass="btn btn-success" Text="Cancelar" PostBackUrl="Suculsales_Lista.aspx" />
                                    <asp:button runat="server" Id="BtnGuardar" cssclass="btn btn-primary" Text="Aceptar "  onclick="BtnGuardar_Click" />
                                </div>
                            </div>
                        </footer>

                        </div>
                  
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
        function showModalMensaje() {
            $("#MdlAtencion").modal("show");
        }
    </script>

</asp:Content>
