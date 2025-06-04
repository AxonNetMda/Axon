<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="monedas_lista.aspx.vb" Inherits="axonnet.monedas_lista" %>
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
        <div class="container-fluid px-4">
                        <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item"><a href="DefaultAdmin.aspx">ADMINISTRACION</a></li>
                            <li class="breadcrumb-item active">Monedas</li>
                        </ol>
                        <div class="card mb-4">
                            <div class="card-header">
                                <i class="fas fa-table me-1"></i>
                                MONEDAS
                            </div>
                            <div class="container-fluid">
                                <div class="card-header bg-white">
                                    <asp:Button runat="server" ID="BtnNuevo" cssclass="btn btn-primary btn-sm" text="Nueva Moneda"/>  
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
                                            <asp:TextBox runat="server" ID="txtBuscar" class="datatable-input" placeholder="Buscar..." type="search" title="Buscar Moneda" aria-controls="datatablesSimple"></asp:TextBox>
                                         </div>
                                    </div>
                                </div>

                            <div class="table-responsive">                                   
                                <asp:GridView ID="dgvData" CssClass="datatable-table" runat="server" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" DataSourceID="sqlMonedas" AutoGenerateColumns="False" DataKeyNames="IdMoneda" AllowPaging="True" PagerStyle-VerticalAlign="Middle" PagerStyle-HorizontalAlign="Center" EnableTheming="True">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="IdMoneda" HeaderText="IdMoneda" InsertVisible="False" ReadOnly="True" SortExpression="IdMoneda" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                        <asp:BoundField DataField="Cotizacion" HeaderText="Cotizacion" SortExpression="Cotizacion" >
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaCotizacion" HeaderText="FechaCotizacion" SortExpression="FechaCotizacion" DataFormatString="{0:dd/MM/yyyy}" >
                                        <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="MonedaPrincipal" HeaderText="MonedaPrincipal" SortExpression="MonedaPrincipal"  ControlStyle-CssClass="checkbox-wrapper-40"></asp:CheckBoxField>
                                        <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado"  ControlStyle-CssClass="checkbox-wrapper-40"></asp:CheckBoxField>
                                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-primary fa fa-edit" CommandName="editar" Text="" HeaderStyle-Width="50" ></asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-danger fa fa-cancel" CommandName="borrar" Text="" HeaderStyle-Width="50"></asp:ButtonField>
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
                                <asp:SqlDataSource ID="sqlMonedas" runat="server" ConnectionString="<%$ ConnectionStrings:CADENA2 %>" SelectCommand="select * from moneda order by Nombre"/>
                            </div>
<%--                            <div class="datatable-bottom">
                                <div class="datatable-info">Mostrar 1 to 10 de 60</div>
                                <nav class="datatable-pagination"><ul class="datatable-pagination-list"><li class="datatable-pagination-list-item datatable-hidden datatable-disabled"><a data-page="1" class="datatable-pagination-list-item-link">‹</a></li><li class="datatable-pagination-list-item datatable-active"><a data-page="1" class="datatable-pagination-list-item-link">1</a></li><li class="datatable-pagination-list-item"><a data-page="2" class="datatable-pagination-list-item-link">2</a></li><li class="datatable-pagination-list-item"><a data-page="3" class="datatable-pagination-list-item-link">3</a></li><li class="datatable-pagination-list-item"><a data-page="4" class="datatable-pagination-list-item-link">4</a></li><li class="datatable-pagination-list-item"><a data-page="5" class="datatable-pagination-list-item-link">5</a></li><li class="datatable-pagination-list-item"><a data-page="6" class="datatable-pagination-list-item-link">6</a></li><li class="datatable-pagination-list-item"><a data-page="2" class="datatable-pagination-list-item-link">›</a></li></ul></nav>
                            </div>--%>

                            </div>
                        </div>
                    </div>
       
    </div>


<%--


        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/umd/simple-datatables.min.js" crossorigin="anonymous"></script>
        <script src="js/datatables-simple-demo.js"></script>--%>

</asp:Content>
