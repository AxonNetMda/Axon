<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="producto_lista_precios_suc.aspx.vb" Inherits="axonnet.producto_lista_precios_suc" %>
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

     <div class="container-fluid px-4">
        <ol class="breadcrumb mb-4">
            <li class="breadcrumb-item"><a href="Default.aspx">ADMINISTRACION</a></li>
            <li class="breadcrumb-item active"> Consulta Stock</li>
        </ol>
        <%--<div class="card mb-4">
            <div class="card-header">
                <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-primary" Text="Nuevo producto" />
            </div>                             
        </div>--%>
        <div class="card mb-4">
             <div class="card-header">
                <i class="fas fa-table me-1"></i>
                <srong>LISTA DE PRECIOS DE PRODUCTOS POR SUCURSAL</srong>
            </div>
             <div class="card-body">
                     <div class="form-group">
                        <div class="form-floating mb-3">
                            <asp:DropDownList runat="server" CssClass="form-control text-danger" id="cboSucursal" Width="300" Font-Bold="true" AutoPostBack="true"></asp:DropDownList>
                            <label for="cboSucursal">Sucursal</label>                                   
                        </div>
                    </div>  
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
                            <div runat="server" id="divPrecios"></div>
                        </div>
                        <div>
                           <div class="table-responsive">
                                <div class="datatable-container">
                                    <asp:GridView ID="dgvData" runat="server" CssClass="datatable-table" AutoGenerateColumns="False" DataKeyNames="idProducto" DataSourceID="SqlProducto" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnRowCreated="dgvData_RowCreated">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="idProducto" HeaderText="idProducto" InsertVisible="False" ReadOnly="True" SortExpression="idProducto" ControlStyle-Width="50px" >
                                                <ControlStyle Width="50px"></ControlStyle>
											<HeaderStyle Width="50px" />
											<ItemStyle HorizontalAlign="Right" />
											</asp:BoundField>
                                            <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" SortExpression="NombreProducto" />
                                            <asp:BoundField DataField="PrecioCosto" HeaderText="Costo" SortExpression="PrecioCosto" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150" DataFormatString="{0:C2}" Visible="false"/>
                                            <asp:BoundField DataField="PrecioLista" HeaderText="Efectivo" SortExpression="PrecioLista" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150" DataFormatString="{0:C2}"/>
                                            <asp:BoundField DataField="PrecioTarjeta" HeaderText="Tarjeta" SortExpression="PrecioTarjeta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150" DataFormatString="{0:C2}"/>

                                            <asp:ButtonField ButtonType="Button" CommandName="Ajuste" ItemStyle-HorizontalAlign="Center" >  
                                                 <ControlStyle CssClass="btn btn-default fas fa-pen-to-square" ></ControlStyle>
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField> 
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
                                    <asp:SqlDataSource ID="sqlProducto" runat="server" ConnectionString="<%$ ConnectionStrings:CADENA2 %>" SelectCommand="<% scomando %>"></asp:SqlDataSource>
                                </div>
                           </div>
                        </div>
                        
                    </div>
             </div>
            <div class="card-footer bg-body">
                <div class="row">
                    <div class="form-floating mb-3">                                     
                        <asp:Label ID="lblReferencia1" runat="server" cssclass="form-control-color bg-danger text-white text-center" BackColor="#ff9686" Width ="150" Font-Bold="true" Font-Size="Small"  Text=" NO HAY STOCK "></asp:Label>
                        <asp:Label ID="lblReferencia2" runat="server" cssclass="form-control-color bg-warning text-black text-center" Width ="150" Font-Bold="true" Font-Size="Smaller"  Text=" STOCK CRITICO "></asp:Label>
                         <asp:Label ID="lblReferencia3" runat="server" cssclass="form-control-color bg-white border-secondary text-black text-center" Width ="150" Font-Bold="true"  Text=" HAY STOCK "></asp:Label>
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
 <!-- ventana modal Ajuste Stock -->    
        <div class="modal fade" id="MdlAjuste" tabindex="-1" role="dialog" aria-labelledby="myMdlAjuste" data-bs-backdrop="static" style="display:none">                         
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header" >
                            <h4 class="modal-title" id="myMdlAjuste">Ajuste de Stock</h4>
                        </div>
                          <asp:UpdatePanel ID="udpAjuste" runat="server" UpdateMode="Conditional">                                                                           
                                                <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtMDLCantidad" EventName="TextChanged" />                                            
                                                </Triggers>
                                                <ContentTemplate>
                                                    <div class="modal-body" style="color:black;">
                                                            <div class="form-floating mb-3">     
                                                                <asp:HiddenField runat="server" ID="txtidProducto" Value="0" />
                                                                <asp:Label ID="lblMDLIdSucursal" runat="server" Text="" cssclass="form-control"></asp:Label>
                                                                 <label for="lblMDLIdSucursal">Sucursal</label>
                                                                </div>
                                                             <div class="form-floating mb-3">    
                                                                <asp:Label ID="lblMDLSucursal" runat="server" Text="" cssclass="form-control"></asp:Label>
                                                                <label for="lblMDLSucursal">Sucursal</label>   
                                                            </div>
                                                            <div class="form-floating mb-3">      
                                                                 <asp:label runat ="server" ID="lblMDLIdProducto"  CssClass="form-control"></asp:label>
                                                                <label for="lblMDLIdProducto">Producto</label> 
                                                            </div>
                                                            <div class="form-floating mb-3">    
                                                                <asp:label runat ="server" ID="lblMDLProductoNombre"  CssClass="form-control"></asp:label>
                                                                <label for="lblMDLProductoNombre">Producto</label>  
                                                            </div> 
                                                            <div class="form-floating mb-3">                                      
                                                                <asp:label runat ="server" ID="lblMDLStock"  CssClass="form-control bg-light" Width="150px"></asp:label>
                                                                <label for="lblMDLStock">Stock Actual</label>  
                                                            </div>  
                                                            <br />
                                                        <div class="form-floating mb-3"> 
                                                                <asp:textbox ID="txtMDLStockCritico" CssClass="form-control text-end" runat="server" Text="0" ForeColor="black" Width="150px"></asp:textbox> 
                                                                <label for="txtMDLStockCritico">Stock Critico</label> 
                                                            </div>
                                                           <div class="form-floating mb-3"> 
                                                                <asp:textbox ID="txtMDLCantidad" CssClass="form-control text-end" runat="server" TextMode="Number" Text="0" ForeColor="black" Width="150px"></asp:textbox> 
                                                                <label for="txtMDLCantidad">Cantidad</label> 
                                                            </div>
                                                    </div>   
                                                </ContentTemplate>  
                          </asp:UpdatePanel>     
                        <div class="modal-footer">
                            <asp:button runat="server" ID="btnAceptarMDLSave" class="btn btn-primary" data-bs-dismiss="modal" Text="Aceptar" ></asp:button>  
                            <asp:button runat="server" ID="btnCerrarMDLEdit" class="btn btn-success" data-bs-dismiss="modal" Text="Cerrar"></asp:button>   
                        </div>
                    </div><!-- modal content -->
                </div><!-- modal dialog -->        
            </div>
        <!-- modal fade --> 
      <script >	

          function showModalAjuste() {
              $("#MdlAjuste").modal("show");
          }

          function showModalAtencion() {
              $("#MdlEditar").modal("show");
          }
          function showModalFormaPAgo() {
              $("#MdlFormaPago").modal("show");
          }
      </script>
</asp:Content>
