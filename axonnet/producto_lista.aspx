 <%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="producto_lista.aspx.vb" Inherits="axonnet.producto_lista" %>
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
            <li class="breadcrumb-item active">Productos</li>
        </ol>
        <div class="card mb-4">
            <div class="card-header">
                <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-primary" Text="Nuevo producto" />
            </div>                             
        </div>
        <div class="card mb-4">
             <div class="card-header">
                <i class="fas fa-table me-1"></i>
                LISTADO DE PRODUCTOS
                  <div class="col-md-3">
                            <div id="divSucursal" runat="server" class="form-floating mb-3">
                                <asp:DropDownList runat="server" CssClass="form-control text-danger" id="cboSucursal" Width="300" Font-Bold="true" AutoPostBack="true"></asp:DropDownList>
                                <label for="cboSucursal">Sucursal</label>                                
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
                                    <asp:GridView ID="dgvData" runat="server" CssClass="datatable-table"  AutoGenerateColumns="False" DataKeyNames="idProducto" DataSourceID="SqlProducto" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" >
                                        <AlternatingRowStyle BackColor="White"/>
                                        <Columns>
                                            <asp:BoundField DataField="idProducto" HeaderText="idProducto" InsertVisible="False" ReadOnly="True" SortExpression="idProducto" ControlStyle-Width="50px" >
                                                <ControlStyle Width="50px"></ControlStyle>
											<HeaderStyle Width="50px" />
											<ItemStyle HorizontalAlign="Right" />
											</asp:BoundField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                            <asp:BoundField DataField="NombreMarca" HeaderText="Marca" SortExpression="NombreMarca" />
                                            <asp:BoundField DataField="RazonSocial" HeaderText="Proveedor" SortExpression="RazonSocial" />                                           
                                            <asp:BoundField DataField="StockCritico" HeaderText="Stock Critico" SortExpression="StockCritico" >
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PrecioVenta" HeaderText="Precio" SortExpression="PrecioVenta" DataFormatString="{0:C2}" >
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:CheckBoxField DataField="MostrarCatalogo" HeaderText="Catalogo" SortExpression="Catalogo" ItemStyle-CssClass="checkbox-wrapper-40 " >
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle CssClass="checkbox-wrapper-40"></ItemStyle>
											</asp:CheckBoxField>
                                            <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ItemStyle-CssClass="checkbox-wrapper-60" >
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle CssClass="checkbox-wrapper-40"></ItemStyle>
											</asp:CheckBoxField>
                                             
                                            <asp:ButtonField ButtonType="Button" CommandName="editar" ItemStyle-HorizontalAlign="Center" >   
                                                 <ControlStyle CssClass="btn btn-info fas fa-edit" ></ControlStyle>
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField>  
<%--                                            <asp:ButtonField ButtonType="Button" CommandName="inventario" ItemStyle-HorizontalAlign="Center" >  
                                                 <ControlStyle CssClass="btn btn-warning fas fa-rectangle-list" ></ControlStyle>
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField>                                           --%>
                                            <asp:ButtonField ButtonType="Button" CommandName="barcode" ItemStyle-HorizontalAlign="Center" >  
                                                 <ControlStyle CssClass="btn btn-warning fas fa-qrcode" ></ControlStyle>
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField> 
                                            <asp:ButtonField ButtonType="Button" CommandName="fotos" ItemStyle-HorizontalAlign="Center" >  
                                                 <ControlStyle CssClass="btn btn-success fas fa-camera" ></ControlStyle>
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField> 
                                            <asp:ButtonField ButtonType="Button" CommandName="borrar" ItemStyle-HorizontalAlign="Center">                                
                                                        <ControlStyle CssClass="btn btn-danger fas fa-trash" ></ControlStyle>
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
