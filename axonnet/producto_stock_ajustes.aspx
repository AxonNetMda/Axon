<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="producto_stock_ajustes.aspx.vb" Inherits="axonnet.producto_stock_ajustes" %>
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


     <div class="container-fluid px-4">
        <ol class="breadcrumb mb-4">
            <li class="breadcrumb-item"><a href="Default.aspx">ADMINISTRACION</a></li>
            <li class="breadcrumb-item active"> Consulta Stock</li>
        </ol>
        <div class="card mb-4">
             <div class="card-header">
                <i class="fas fa-table me-1"></i>
                <srong>STOCK DE PRODUCTOS</srong>
            </div>
             <div class="card-body">
                              <div class="row">
                                  <div class="col-md-2">
                                       <div class="form-floating mb-3">
                                            <asp:HiddenField runat="server" ID="txtidProducto" Value="0" />
                                            <asp:Label ID="lblIdSucursal" runat="server" Text="" cssclass="form-control text-end" BackColor="#cccccc" Font-Bold="true" ></asp:Label>
                                            <label for="lblIdSucursal">id Suc.</label> 
                                       </div>
                                    </div>
                                  <div class="col-md-10">
                                      <div class="form-floating mb-3">
                                            <asp:Label ID="lblSucursal" runat="server" Text="" cssclass="form-control" BackColor="#cccccc"  Font-Bold="true"></asp:Label>
                                          <label for="lblSucursal">Sucursal</label> 
                                      </div>    
                                  </div>
                              </div>
                          <div class="row">
                              <div class="col-md-2">
                                   <div class="form-floating mb-3">
                                        <div class="form-floating mb-3">      
                                            <asp:label runat ="server" ID="lblIdProducto"  CssClass="form-control text-end" BackColor="#cccccc"  Font-Bold="true"></asp:label>
                                            <label for="lblIdProducto">Producto</label> 
                                        </div>
                                   </div>
                              </div>
                                <div class="col-md-10">
                                    <div class="form-floating mb-3">
                                        <div class="form-floating mb-3">    
                                            <asp:label runat ="server" ID="lblProductoNombre"  CssClass="form-control" BackColor="#cccccc"  Font-Bold="true"></asp:label>
                                            <label for="lblProductoNombre">Producto</label>  
                                        </div> 
                                    </div>
                                </div>
                          </div>

                  <div class="row">
                        <div class="col-md-2">
                             <div class="form-floating mb-3">                                      
                                <asp:label runat ="server" ID="lblStockActual"  CssClass="form-control bg-light" Width="150px"></asp:label>
                                <label for="lblMDLStock">Stock Actual</label>  
                            </div>  
                        </div>
                        <div class="col-md-2">
                             <div class="form-floating mb-3"> 
                                <asp:textbox ID="txtStockCritico" CssClass="form-control text-end" runat="server" Text="0" ForeColor="black" Width="150px"></asp:textbox> 
                                <label for="txtMDLStockCritico">Stock Critico</label> 
                            </div>
                        </div>
                      <div class="col-md-2">
                             <div class="form-floating mb-3"> 
                                <asp:textbox ID="txtCantidad" CssClass="form-control text-end" runat="server" TextMode="Number" Text="0" ForeColor="black" Width="150px"></asp:textbox> 
                                <label for="txtCantidad">Cantidad</label> 
                            </div>
                      </div>
                      <div class="col-md-2">
                           <div class="form-floating mb-3"> 
                                <asp:textbox ID="txtPrecio" CssClass="form-control text-end" runat="server" Text="0,00" ForeColor="black" Width="150px" ReadOnly="true"></asp:textbox> 
                                <label for="txtPrecio">Precio</label> 
                            </div>
                      </div>
                    </div>   
             </div>
             <div class="card-footer">
                  <asp:button runat="server" ID="btnAceptar" class="btn btn-primary" Text="Aceptar" ></asp:button>  
                  <asp:button runat="server" ID="btnCerrar" class="btn btn-success"  Text="Cerrar" PostBackUrl="~/producto_stock_listado.aspx"></asp:button>   
             </div>
        </div>
    </div>
            
<%-- Abrir modal para Validar respuesta --%>
<div class="modal fade" id="MdlAtencion" tabindex="-1" role="dialog" aria-labelledby="myMdlAtencion" data-bs-backdrop="static" style="display:none">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">ATENCION</h5><button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
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
			   $("#MdlEditar").modal("show");

        </script>    
</asp:Content>
