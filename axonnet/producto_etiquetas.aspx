<%@ Page Title="Etiquetas" Language="vb"  MasterPageFile="~/Administracion.Master" CodeBehind="producto_etiquetas.aspx.vb" Inherits="axonnet.producto_etiquetas" %>
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
         <div class="card shadow-lg border-0 rounded-lg mt-10">
                <div class="card-header bg-warning text-black">
                    <h4 class="text-center font-weight-light my-1"><strong><asp:Label runat="server" ID="lblTitulo" text="IMPRESION ETIQUETAS"></asp:Label></strong></h4>                                   
                </div>                       
                <div class="card-body justify-content-center"> 
                    <div class="row justify-content-center">                            
                            <asp:HiddenField runat="server" ID="txtidProducto"/>
                            <div class="row">  
                                    <div class="col-md-2">
                                          <asp:Image ID="imgQR" runat="server" Width="150px" Height="150px" Alt="Código QR" />
                                    </div>                                    
                                    <div class="col-md-8">
                                        <div class="form-group">                                                    
                                                <div class="form-floating mb-3">
                                                    <asp:label runat="server" class="form-control " id="txtIdProd" Width="200"></asp:label>
                                                    <label class="text-dark" for="txtIdProd">Id</label>                                   
                                                </div>
                                        </div>

                                        <div class="form-group">
                                                <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control text-capitalize" id="txtNombre"></asp:TextBox>
                                                    <label for="txtNombre">Nombre del producto</label>                                   
                                                </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-floating mb-3">
                                                 <asp:TextBox runat="server" class="form-control" id="txtCantidad" placeholder="1" Width="100" onkeypress="return soloNumeros(event);"></asp:TextBox>
                                                 <label for="txtCantidad">Cantidad</label> 

                                            </div>
                                       </div>
                                        <div class="form-group">
                                            <div class="form-floating mb-3">
                                                 <asp:TextBox runat="server" class="form-control" id="txtPrecio" placeholder="0.00" Width="100"></asp:TextBox>
                                                 <label for="txtPrecio">Precio</label>                                   
                                            </div>
                                       </div>
                                    </div>

                            </div>       
                            <hr class="border-success" style="border-width: medium; border-style: solid" /> 
                       <div class="card-footer">
                             <footer class="py-4 bg-light mt-auto">
                                <div class="container-fluid px-4">
                                    <div class="d-flex align-items-end justify-content-between small">
                                        <asp:button runat="server" id="BtnCancelar" cssclass="btn btn-success" Text="Cancelar" PostBackUrl="producto_lista.aspx?idproducto=0&Titulo=''&Accion=''" />
                                        <asp:button runat="server" Id="btnImprimir" cssclass="btn btn-primary" Text="imprimir " onclick="btnImprimir_Click" />
                                    </div>
                                </div>
                            </footer>         
                      </div>     
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

       function soloNumeros(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

</asp:Content>
