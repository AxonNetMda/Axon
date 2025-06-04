<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="producto_inventario.aspx.vb" Inherits="axonnet.producto_inventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <style>
        .filtros-container {
    display: flex;
    align-items: center; /* Alinea verticalmente */
    gap: 10px; /* Espaciado entre elementos */
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formMaster" runat="server">
    <div class="container-fluid px-4">
        <ol class="breadcrumb mb-4">
            <li class="breadcrumb-item"><a href="Default.aspx">ADMINISTRACION</a></li>
            <li class="breadcrumb-item active"> Productos Inventario</li>
        </ol>
     
        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-table me-1"></i>                 
                <strong>INVENTARIO DE PRODUCTOS</strong>
                 <div class="row">
                      <div class="col-md-12"  id="divMostrarComboProducto" runat="server" >
                           <div class="row">
                            <%-- vengo del menu inventario --%>
                               <div class="col-md-6">
                                <div class="form-floating mb-1">
                                    <asp:DropDownList runat="server" CssClass="form-control text-danger" id="cboSucursal" Width="90%" Font-Bold="true" AutoPostBack="true"></asp:DropDownList>
                                    <label for="cboSucursal">Sucursal</label>                                
                                </div>
                                   </div>
                               <div class="col-md-6">
                                <div  class="form-floating mb-1">
                                    <asp:DropDownList runat="server" CssClass="form-control text-danger" id="cboProducto" Width="90%" Font-Bold="true" AutoPostBack="true"></asp:DropDownList>
                                    <label for="cboProducto">Producto</label>                                   
                                </div>
                                </div>
                            </div>
                      </div>
                      <div class="col-md-12" id="divNombreSucursalMostrar" runat="server" >
                          <div class="row">
                            <div class="col-md-6">
                                <div class="form-floating mb-1">
                                    <%-- vengo de producto_stock_listado.aspx --%>
                                    <asp:Label runat="server" ID="lblidSucursal" CssClass="form-control text-danger" Text="0" Width="10%" Visible="false"></asp:Label>
                               <%--      <label for="lblidSucursal">Sucursal</label>
                               </div>
                                <div class="form-floating mb-3">--%>
                                    <asp:Label runat="server" ID="lblNombreSucursal" CssClass="form-control text-danger" Text=""  Width="90%"></asp:Label>
                                    <label for="lblNombreSucursal">Sucursal</label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-floating mb-1">
                                    <asp:Label runat="server" ID="lblIdProducto" CssClass="form-control text-danger"  Width="10%" Visible="false"></asp:Label>
                                 <%--    <label for="lblIdProducto">Producto</label>  
                               </div>
                                <div class="form-floating mb-3">--%>
                                    <asp:Label runat="server" ID="lblNompreProducto" CssClass="form-control text-danger"  Width="90%"></asp:Label>
                                    <label for="lblNompreProducto">Nombre producto</label>     
                                </div>
                            </div>
                          </div>

                      </div> 
                     </div>
                    <div class="row">
                      <div class="col-md-6 ">
                           <div class="row filtros-container">
                                <div class="col-md-2 m-2">
                                       <div class="form-floating mb-3">
                                            <asp:textbox runat="server" CssClass="form-control text-danger" TextMode="Date"  id="fecDesde" Width="150" Font-Bold="true" ></asp:textbox>
                                            <label for="fecHasta">Fecha desde</label>                                   
                                        </div>
                                </div>
                                <div class="col-md-2  m-2">
                                      <div class="form-floating mb-3">
                                        <asp:textbox runat="server" CssClass="form-control text-danger" TextMode="Date"  id="fecHasta" Width="150" Font-Bold="true" ></asp:textbox>
                                        <label for="fecHasta">Fecha hasta</label>                                   
                                    </div>
                                </div>
                               <div class="col-md-2  mb-1">
                                   <div class="form-floating mb-3">
                                     <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-warning" Width="100" Text="Buscar" />
                                   </div>
                               </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>

                 </div>
             </div>  
            <div class="card-body">
                    
                   <div class="datatable-wrapper datatable-loading no-footer sortable searchable fixed-columns">
                        <div class="datatable-top">
                             <div class="dropdown">
                                    <label>
                                        <asp:DropDownList ID="cboMostrar" runat="server" CssClass="dropdown datatable-selector" Width="80px" AutoPostBack="true">
                                            <asp:ListItem >5</asp:ListItem>
                                            <asp:ListItem Selected="True">10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                        </asp:DropDownList>
                                            Mostar
                                    </label>
                                </div>                                           
                             <div class="datatable-search">
                                <asp:TextBox runat="server" ID="txtBuscar" class="datatable-input" placeholder="Buscar..." type="search" title="Buscar un PRODUCTO" aria-controls="datatablesSimple" AutoPostBack="true" ></asp:TextBox>
                             </div>
                        </div>
                        <div>
                           <div class="table-responsive">
                                <div class="datatable-container">
                                    <asp:GridView ID="dgvData" runat="server" CssClass="datatable-table" AutoGenerateColumns="False"  DataSourceID="SqlProducto" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="fecFecha" HeaderText="Fecha" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"  />                                              											
                                            <asp:BoundField DataField="Detalle" HeaderText="Detalle" SortExpression="Nombre" ReadOnly="True"/>
                                            <asp:BoundField DataField="ClienteProveedor" HeaderText="Razon Social" ReadOnly="True"  SortExpression="NombreOperacion" />            
                                             <asp:BoundField DataField="PrecioUnitario" HeaderText="P.U" ReadOnly="True"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"/>
                                            <asp:BoundField DataField="Entrada" HeaderText="Entrada" ReadOnly="True"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"/>
                                            <asp:BoundField DataField="Salida" HeaderText="Salida" ReadOnly="True"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                                             <asp:BoundField DataField="Saldo" HeaderText="Saldo" ReadOnly="True"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                                            
                                    </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings Mode="Numeric" Visible="true" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                     </asp:GridView>
                                    <asp:SqlDataSource ID="sqlProducto" runat="server"  ConnectionString="<%$ ConnectionStrings:CADENA2 %>" SelectCommand="<% scomando %>"></asp:SqlDataSource>
                                </div>
                           </div>
                        </div>
                        
                    </div>
             </div>
            <div class="card-footer bg-body">
                <div class="row">
                    <div class="form-floating mb-3">                                     
                        <asp:Button ID="btnVolver" runat="server" cssclass="btn btn-info" Width ="150" Font-Bold="true" Font-Size="Small"  Text=" Volver " PostBackUrl="~/DefaultAdmin.aspx" ></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    


<%--Abrir modal para Validar respuesta                                                                                                                                                                  --%>
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

</asp:Content>
