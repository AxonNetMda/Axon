<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="Suculsales_Lista.aspx.vb" Inherits="axonnet.Suculsales_Lista" %>

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
    <div id="layoutSidenav_content"> 
        <div class="container-fluid px-4">
                       
                        <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item"><a href="DefaultAdmin.aspx">ADMINISTRACION</a></li>
                            <li class="breadcrumb-item active">Sucursales</li>
                        </ol>
                        
                       
                        <div class="card mb-4">
                            <div class="card-header">
                                <i class="fas fa-table me-1"></i>
                                SUCURSALES
                            </div>
                            <div class="card mb-4">
                                <div class="card-header">
                                    <asp:Button runat="server" ID="BtnNuevo" cssclass="btn btn-primary btn-sm" text="Nueva Sucursal"/>  
                                </div>
                            </div>
                            <div class="card-body">

                                 <div class="datatable-wrapper datatable-loading no-footer sortable searchable fixed-columns">
                                        <div class="datatable-top">
                                            <div class="dropdown">
                                                <label>
                                                    <asp:DropDownList ID="cboMostrar" runat="server" CssClass="dropdown datatable-selector" Width="80px">
                                                        <asp:ListItem Selected="True">5</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                    </asp:DropDownList>
                                                        Mostar
                                                </label>
                                            </div>                                           
                                        <div class="datatable-search">
                                            <asp:TextBox runat="server" ID="txtBuscar" class="datatable-input" placeholder="Buscar..." type="search" title="Buscar una sucursal" aria-controls="datatablesSimple"></asp:TextBox>
                                         </div>
                                    </div>
                                </div>

                            <div class="table-responsive">                                   
                                <asp:GridView ID="dgvData" CssClass="datatable-table" runat="server" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" DataSourceID="sqlSucursales" AutoGenerateColumns="False" DataKeyNames="IdSucursal" AllowPaging="True" PagerStyle-VerticalAlign="Middle" PagerStyle-HorizontalAlign="Center" EnableTheming="True">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="IdSucursal" HeaderText="IdSucursal" InsertVisible="False" ReadOnly="True" SortExpression="IdSucursal" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
<%--                                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
                                        <asp:BoundField DataField="Localidad" HeaderText="Localidad" SortExpression="Localidad" />
                                        <asp:BoundField DataField="Provincia" HeaderText="Provincia" SortExpression="Provincia" />
                                        <asp:BoundField DataField="CodigoPostal" HeaderText="CodigoPostal" SortExpression="CodigoPostal" />--%>
                                        <asp:CheckBoxField  DataField="EsDepositoCentral" HeaderText="Central" SortExpression="Central"  ControlStyle-CssClass="checkbox-wrapper-40" />  
                                        <asp:CheckBoxField  DataField="PuedeComprar" HeaderText="Compra" SortExpression="Compra" ControlStyle-CssClass="checkbox-wrapper-40" />  
                                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
                                        <asp:BoundField DataField="Celular" HeaderText="Celular" SortExpression="Celular" />
                                        <asp:BoundField DataField="Instagram" HeaderText="Instagram" SortExpression="Instagram" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado"  ControlStyle-CssClass="checkbox-wrapper-40"/>       
<%--                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnEditar" CssClass="btn btn-primary fa fa-edit" CommandName="editar"/>
                                                <asp:button runat="server" ID="btnBorrar" CssClass="btn btn-primary fa fa-trash" OnClick="Borrar_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                         <asp:ButtonField ButtonType="Button" CommandName="editar" ItemStyle-HorizontalAlign="Center" >   
                                                 <ControlStyle CssClass="btn btn-info fas fa-edit" ></ControlStyle>
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                         </asp:ButtonField>  
                                            
                                        <asp:ButtonField ButtonType="Button" CommandName="borrar" ItemStyle-HorizontalAlign="Center">                                
                                                        <ControlStyle CssClass="btn btn-danger fas fa-cancel" ></ControlStyle>
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField> 
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="pagination" />

                                </asp:GridView>
                                <asp:SqlDataSource ID="sqlSucursales" runat="server" ConnectionString="<%$ ConnectionStrings:CADENA2 %>" SelectCommand="<%=scomando %>"></asp:SqlDataSource>
                            </div>
<%--                            <div class="datatable-bottom">
                                <div class="datatable-info">Mostrar 1 to 10 de 60</div>
                                <nav class="datatable-pagination"><ul class="datatable-pagination-list"><li class="datatable-pagination-list-item datatable-hidden datatable-disabled"><a data-page="1" class="datatable-pagination-list-item-link">‹</a></li><li class="datatable-pagination-list-item datatable-active"><a data-page="1" class="datatable-pagination-list-item-link">1</a></li><li class="datatable-pagination-list-item"><a data-page="2" class="datatable-pagination-list-item-link">2</a></li><li class="datatable-pagination-list-item"><a data-page="3" class="datatable-pagination-list-item-link">3</a></li><li class="datatable-pagination-list-item"><a data-page="4" class="datatable-pagination-list-item-link">4</a></li><li class="datatable-pagination-list-item"><a data-page="5" class="datatable-pagination-list-item-link">5</a></li><li class="datatable-pagination-list-item"><a data-page="6" class="datatable-pagination-list-item-link">6</a></li><li class="datatable-pagination-list-item"><a data-page="2" class="datatable-pagination-list-item-link">›</a></li></ul></nav>
                            </div>--%>

                            </div>
                        </div>
                    </div>
       
    </div>




<%-- ******************* MODAL nueva sucursal *********************************** --%>
        <div class="modal fade modal-lg" id="MdlNuevaSucural" tabindex="-1" role="dialog" aria-labelledby="myMdlNuevaSucural" data-bs-backdrop="static" style="display:none">                         
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header" style="color:white; background-color:black" >
                            <h4 class="modal-title" id="myMdlNuevaSucural">NUEVA SUCURSAL  </h4>
                        </div>
                        <div class="modal-body" style="color:black;">
                                  
                                      <asp:UpdatePanel runat="server">
                                            <Triggers>
                                               <asp:AsyncPostBackTrigger ControlID="txtNombre" EventName="TextChanged" />   
                                               <asp:AsyncPostBackTrigger ControlID="txtDireccion" EventName="TextChanged" />
                                               <asp:AsyncPostBackTrigger ControlID="txtLocalidad" EventName="TextChanged" /> 
                                               <asp:AsyncPostBackTrigger ControlID="txtProvincia" EventName="TextChanged" />
                                               <asp:AsyncPostBackTrigger ControlID="txtCodigoPostal" EventName="TextChanged" /> 
                                               <asp:AsyncPostBackTrigger ControlID="txtTelefono" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtCelular" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtInstagram" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtEmail" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="chkHabilitado" EventName="checkedChanged" />
                                          </Triggers>
                                          <ContentTemplate>
                                              <div class="row">
                                              <div class="col-md-6" style="border-block-color:lightslategray;border-width:thin ">
                                                     <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtNombre" placeholder="Nombre de la sucursal"></asp:TextBox>
                                                        <label for="inputNombre">Nombre</label>
                                                   </div>
                                                   <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtDireccion" placeholder="Direccion"></asp:TextBox>
                                                        <label for="inputDireccion">Direccion</label>
                                                   </div>
                                                   <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtLocalidad" placeholder="Nombre de la localidad"></asp:TextBox>
                                                        <label for="inputLocalidad">Localidad</label>
                                                   </div>
                                                   <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtProvincia" placeholder="Nombre de la provincia"></asp:TextBox>
                                                        <label for="inputProvincia">Provincia</label>
                                                   </div>
                                                   <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtCodigoPostal" placeholder="Codigo Postal"></asp:TextBox>
                                                        <label for="inputCodigoPostal">Codigo Postal</label>
                                                   </div>
                                               </div>
                                              <div class="col-md-6">
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtTelefono" placeholder="Telefono"></asp:TextBox>
                                                        <label for="inputTelefono">Telefono</label>
                                                    </div>
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtCelular" placeholder="Celular"></asp:TextBox>
                                                        <label for="inputCelular">Celular</label>
                                                    </div>
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtInstagram" placeholder="Instagram"></asp:TextBox>
                                                        <label for="inputInstagram">Instagram</label>
                                                    </div>
                                                    <div class="form-floating mb-3">
                                                        <asp:TextBox runat="server" class="form-control" id="txtEmail" TextMode="Email" placeholder="Email"></asp:TextBox>
                                                        <label for="inputEmail">Email</label>
                                                    </div>
                                                    <div class="form-floating mb-3">
                                                        <asp:CheckBox runat="server"  ID="chkHabilitado" CssClass="form-checkbox-inline" AutoPostBack="true"  text="   Habilitado" Width="200px" />
                                                    </div>
                                                   <div class="form-floating mb-3">
                                                        <asp:button runat="server" ID="btnAceptarFPMDL" class="btn btn-primary" data-bs-dismiss="modal" Text="Aceptar" ></asp:button>  
                                                        <asp:button runat="server" ID="Button2" class="btn btn-success" data-bs-dismiss="modal" Text="Cerrar"></asp:button>   
                                                    </div>
                                               </div>
                                            </div>
                                         </ContentTemplate>
                                        
                                      </asp:UpdatePanel>
                                    </div>
                          </div>                     
                        <div class="modal-footer">
                            alta de sucursal
                        </div>
                    </div><!-- modal content -->
                </div><!-- modal dialog -->        
            
   
<%--******************** FIN MODAL NUEVA SUCURSAL ****************--%>
       <script >	

           function showModalEditar() {
               $("#MdlEditar").modal("show");
           }

           function showModalAtencion() {
               $("#MdlEditar").modal("show");
           }
           function showModalFormaPAgo() {
               $("#MdlFormaPago").modal("show");
           }
       </script>
<%--        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/umd/simple-datatables.min.js" crossorigin="anonymous"></script>
        <script src="js/datatables-simple-demo.js"></script>--%>
</asp:Content>
