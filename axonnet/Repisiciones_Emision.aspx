<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="Repisiciones_Emision.aspx.vb" Inherits="axonnet.Repisiciones_Emision" %>
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
             <li class="breadcrumb-item"><a href="Reposiciones_lista.aspx">Reposiciones Lista</a></li>
            <li class="breadcrumb-item active"> Reposiciones</li>
        </ol>
        <%--<div class="card mb-4">
            <div class="card-header">
                
            </div>                             
        </div>--%>
        <div class="card mb-4">
             <div class="card-header">
                <i class="fas fa-table me-1"></i>
                <srong>EMISION DE REPOSICIONES A SUCURSALES</srong>&nbsp;&nbsp;&nbsp;
            </div>
             <div class="card-body">
                 <div class="row"> 
                     <div class="col-md-6" id="sotck">
                         
                            <div class="datatable-wrapper datatable-loading no-footer sortable searchable fixed-columns">
                            <div class="datatable-top">
                             <div class="dropdown">
                                    <label>
                                        <asp:DropDownList ID="cboMostrar" runat="server" CssClass="dropdown datatable-selector" AutoPostBack="true">
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
                                       <asp:GridView ID="dgvData" runat="server" CssClass="datatable-table"  AutoGenerateColumns="False" DataKeyNames="idProducto" DataSourceID="SqlProducto" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="idProducto" HeaderText="idProducto" InsertVisible="False" ReadOnly="True" SortExpression="idProducto" ControlStyle-Width="50px" >
                                                    <ControlStyle Width="50px"></ControlStyle>
											    <HeaderStyle Width="50px" />
											    <ItemStyle HorizontalAlign="Right" />
											    </asp:BoundField>
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                                 <asp:BoundField DataField="StockCritico" HeaderText="Critico" SortExpression="Critico" />
                                                <asp:BoundField DataField="StockActual" HeaderText="Stock" SortExpression="StockActual" />
                                                <asp:BoundField DataField="PrecioVenta" HeaderText="Precio" SortExpression="PrecioVenta" DataFormatString="{0:N2}" >
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100" />
                                                </asp:BoundField>
                                                <asp:ButtonField ButtonType="Button" CommandName="Agregar" ItemStyle-HorizontalAlign="Center" >   
                                                     <ControlStyle CssClass="btn btn-info fas fa-plus btn-sm" ></ControlStyle>
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
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-floating mb-3">
                                        <asp:DropDownList runat="server" CssClass="form-control text-danger" id="cboSucursal" Width="300" Font-Bold="true" AutoPostBack="true"></asp:DropDownList>
                                        <label for="cboSucursal">Sucursal</label>                                   
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-floating mb-3">
                                        <asp:Button runat="server" ID="BtnNuevaRepo" CssClass="btn btn-success align-content-end" Text="Nueva reposicion" />
                                        <asp:Button runat="server" ID="BtnGuardarRepo" CssClass="btn btn-success align-content-end" Text="Guardar Reposicion" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                        </div>  
                        <div>
                            <div class="table-responsive">
                                <div class="datatable-container">
                                    <asp:GridView ID="dgvRepo" runat="server" CssClass="datatable-table" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="idProducto" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="idProducto" ControlStyle-Width="50px" >
                                                <ControlStyle Width="50px"></ControlStyle>
											<HeaderStyle Width="50px" />
											<ItemStyle HorizontalAlign="Right" />
											</asp:BoundField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" ItemStyle-Wrap="true" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80">
                                                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                            </asp:BoundField>
                                                <asp:BoundField DataField="PrecioVenta" HeaderText="Precio" SortExpression="PrecioVenta" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="Referencia" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:ButtonField ButtonType="Button" CommandName="mas" ItemStyle-HorizontalAlign="Center" >  
                                                 <ControlStyle CssClass="btn btn-primary btn-sm fas fa-plus" ></ControlStyle>
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField> 
                                             <asp:ButtonField ButtonType="Button" CommandName="menos" ItemStyle-HorizontalAlign="Center" >  
                                                 <ControlStyle CssClass="btn btn-warning btn-sm fas fa-minus" ></ControlStyle>
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField> 
                                            <asp:ButtonField ButtonType="Button" CommandName="Eliminar" ItemStyle-HorizontalAlign="Center" >  
                                                 <ControlStyle CssClass="btn btn-danger btn-sm fas fa-cancel" ></ControlStyle>
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField>                                             
                                    </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings Mode="Numeric" Visible="true" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                     </asp:GridView>
                                </div>
                           </div>
                        </div>
                    </div>
                 </div> 
             </div>
            <div class="card-footer bg-body">
                <div class="row">
                    <div class="form-floating mb-3">  
                        <asp:Textbox runat="server" CssClass="form-control" id="txtNotas" TextMode="MultiLine" Width="300"></asp:Textbox>
                        <label for="txtNotas">Notas</label>                                   

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
        <button type="button" class="btn btn-success" data-bs-dismiss="modal">Cerrar</button>

      </div>
    </div>
  
  </div>
</div>   

<%--Abrir modal para Validar respuesta                                                                                                                                                                  --%>
<div class="modal fade" id="MdlGuardarREpo" tabindex="-1" role="dialog" aria-labelledby="myMdlGuardarREpo" data-bs-backdrop="static" style="display:none">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">ATENCION</h5><button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
            <asp:Label runat="server" id="lblMensajeRepo" CssClass="form-label" Text=""></asp:Label>
      </div>
      <div class="modal-footer">
        <asp:Button runat="server" ID="BtnMDLCerrarRepo"  class="btn btn-success btn-sm" data-bs-dismiss="modal" Text="Cerrar"/>
        <asp:Button ID="btnMDLGuardarReposicion" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" />
      </div>
    </div>
  
  </div>
</div>   

 <!-- ventana modal NUEVA REPOSICION-->    
        <div class="modal fade" id="MdlMdlReposicion" tabindex="-1" role="dialog" aria-labelledby="myMdlReposicion" data-bs-backdrop="static" style="display:none">                         
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header bg-dark text-white" >
                            <h4 class="modal-title" id="myMdlReposicion">Atencion</h4>
                        </div>
                               
                        <div class="modal-body" style="color:black;">
                                <div class="form-floating mb-3">                                     
                                    <asp:Label ID="lblSucursal" runat="server" Text="<%#cboSucursal.Text %>" cssclass="form-control"></asp:Label>
                                    <label for="lblSucursal">Sucursal</label>   
                                </div>
                            <div class="form-group">
                             <div class="form-floating mb-3">
                                    <asp:DropDownList runat="server" CssClass="form-control text-danger" id="cboASucursal" Width="300" Font-Bold="true" AutoPostBack="true"></asp:DropDownList>
                                    <label for="cboASucursal"> A Sucursal</label>                                   
                                </div>
                            </div>  
                                <div class="form-floating mb-3">      
                                     <asp:label runat ="server" ID="lblIdProducto"  CssClass="form-control"></asp:label>
                                    <label for="lblIdProducto">Producto</label> 
                                    <asp:label runat ="server" ID="lblProductoMDL"  CssClass="form-control"></asp:label>
                                    <label for="lblProductoMDL">Producto</label>  
                                </div> 
                                <div class="form-floating mb-3">                                      
                                    <asp:label runat ="server" ID="lblStock"  CssClass="form-control"></asp:label>
                                    <label for="lblStock">Stock Actual</label>  
                                </div>  
                                <br />
<%--                             <div class="form-floating mb-3"> 
                                    <asp:DropDownList ID="cboAjuste" CssClass="form-control" runat="server"></asp:DropDownList> 
                                    <label for="cboAjuste">Ajuste</label> 
                                </div>--%>
                               <div class="form-floating mb-3"> 
                                    <asp:textbox ID="txtCantidadMDL" CssClass="form-control text-end" runat="server" TextMode="Number" Text="0" ForeColor="black" Width="150px"></asp:textbox> 
                                    <label for="txtCantidad">Cantidad</label> 
                                </div>
                        </div>   
                        <div class="modal-footer">
                            <asp:button runat="server" ID="btnAceptarMDLSave" class="btn btn-primary" data-bs-dismiss="modal" Text="Aceptar"></asp:button>  
                            <asp:button runat="server" ID="btnCerrarMDLEdit" class="btn btn-success" data-bs-dismiss="modal" Text="Cerrar"></asp:button>   
                        </div>
                    </div><!-- modal content -->
                </div><!-- modal dialog -->        
            </div>
        <!-- modal fade --> 
      <script >	

		  function showModalReposicion() {
              $("#MdlGuardarREpo").modal("show");
           }

		   function showModalAtencion() {
               $("#MdlAtencion").modal("show");
           }
		   function showModalFormaPAgo() {
			   $("#MdlFormaPago").modal("show");
          }

          function showModalGuardarRepo() {
              $("#MdlEditar").modal("show");
          }
      </script>


</asp:Content>
