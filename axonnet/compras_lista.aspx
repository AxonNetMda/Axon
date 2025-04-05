<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="compras_lista.aspx.vb" Inherits="axonnet.compras_lista" %>
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
                Response.Redirect("login.aspx?idUsuario=0")
            End If %>
     <style>
        .gridDetalle {
            padding-left: 20px;
            border-left: 3px solid #cccccc;
        }
    </style>
<div class="container-fluid px-4">
       <ol class="breadcrumb mb-4">
            <li class="breadcrumb-item"><a href="Default.aspx">ADMINISTRACION</a></li>
            <li class="breadcrumb-item active"> Compras</li>
        </ol>
             <div class="card-header">
                <%--<i class="fas fa-table me-1"></i>&nbsp;&nbsp;&nbsp;--%>
                <srong><h1>COMPRAS</h1></srong>
                    <nav class="navbar navbar-expand-lg navbar-light bg-default">
                      <div class="container-fluid">
                         
                        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                          <span class="navbar-toggler-icon"></span>
                        </button>
                        <div class="collapse navbar-collapse" id="navbarNav">
                          <ul class="navbar-nav">
                              <li>
                                  <div class="form-group">
                                    <div class="form-floating mb-3">
                                            <asp:DropDownList runat="server" CssClass="form-control text-danger" id="cboSucursal" Width="300" Font-Bold="true" AutoPostBack="true"></asp:DropDownList>
                                            <label for="cboSucursal">Sucursal</label>                                   
                                    </div>
                                </div>
                              </li>

                            <li class="nav-item">
                                 <asp:Button runat="server" ID="btnReponueva" cssclass="btn btn-outline-dark align-bottom" PostBackUrl="Compras_Emision.aspx" Text="Registrar compra" aria-current="page" />&nbsp;
<%--                              <a class="btn btn-outline-dark" aria-current="page" href="Repisiciones_Emision.aspx">Nueva Reposicion</a>&nbsp;--%>
                            </li>                                
                            <li class="nav-item">
                              <a class="btn btn-success" href="Default.aspx">Volver</a>&nbsp;
                            </li>                           
                          </ul>
                        </div>
                      </div>
                    </nav>
            </div>
            <div class="card-body">
                    <%-- <div class="form-group">
                        <div class="form-floating mb-3">
                            <asp:DropDownList runat="server" CssClass="form-control text-danger" id="cboSucursal" Width="300" Font-Bold="true" AutoPostBack="true"></asp:DropDownList>
                            <label for="cboSucursal">Sucursal</label>                                   
                        </div>
                    </div>  --%>
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
                                <asp:TextBox runat="server" ID="txtBuscar" class="datatable-input" placeholder="Buscar..." type="search" title="Buscar en Reposicion" aria-controls="datatablesSimple" PageSize="10" AutoPostBack="true" ></asp:TextBox>
                             </div>
                        </div>
                        <div>
                            <div class="datatable-container">
                                <asp:GridView ID="gvCabecera" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCabecera_RowCommand" CssClass="datatable-table" AllowPaging="True" OnPageIndexChanging="gvCabecera_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="IdCompra" HeaderText="ID Compra" ItemStyle-Width="100" DataFormatString="{0:00000}" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100"/>
                                    <asp:BoundField DataField="Letra" HeaderText="Letra" ItemStyle-Width="50" Visible="false"/>
                                    <asp:BoundField DataField="PuntoVenta" HeaderText="Pto.Vta" ItemStyle-Width="100" DataFormatString="{0:00000}"  Visible="false"/>
                                     <asp:BoundField DataField="NumeroComprobante" HeaderText="Numero" ItemStyle-Width="80" DataFormatString="{0:00000000}"  Visible="false"/>
                                      <asp:BoundField DataField="ComprobanteFormateado" HeaderText="Comprobante" ItemStyle-Width="150" /> 
                                     <asp:BoundField DataField="RazonSocial" HeaderText="Proveedor"/> 
                                     <asp:BoundField DataField="ImporteTotal" HeaderText="Importe" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Right" DataFormatString = "{0:C2}" /> 
                                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" ItemStyle-Width="100" /> 
                                    <asp:TemplateField HeaderText="Estado" HeaderStyle-Width="100">
                                        <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado").ToString() %>' Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>                                           
                                            <asp:Button ID="btnToggleDetails" runat="server" CommandName="mostrar" Text="+" CommandArgument='<%# Eval("IdCompra") %>' CssClass="btn btn-success btn-sm" Visible='<%# IIf(Eval("Estado").ToString() = "0", False, True) %>'/>
                                            <asp:Button ID="btnAnular" runat="server" CommandName="Anular" Text="Anular"  CommandArgument='<%# Eval("IdCompra") %>'  CssClass="btn btn-danger btn-sm"  Visible='<%# IIf(Eval("Estado").ToString() = "0", False, True) %>' />
                                            <asp:Button ID="btnPrint" runat="server" CommandName="Imprimir" Text="Imprimir"   CommandArgument='<%# Eval("IdCompra") %>' CssClass="btn btn-warning btn-sm"  
                                                Visible='<%# IIf(Eval("Estado").ToString() = "0", False, True) %>' />
                                             <asp:PlaceHolder ID="phDetalle" runat="server" />     
                                        </ItemTemplate>                      
                                    </asp:TemplateField>
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

                            </div>
                        </div>
                   </div>
            </div>
            <div class="card-footer bg-body">
                <br />
              
            </div>
        </div>

<%--Abrir modal para Validar respuesta                                                                                                                                                                  --%>
<div class="modal fade" id="MdlAtencion" tabindex="-1" role="dialog" aria-labelledby="myMdlAtencion" data-bs-backdrop="static" style="display:none">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header text-bg-dark">
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
 <!-- ventana modal NUEVA REPOSICION-->    
        <div class="modal fade" id="MdlReposicion" tabindex="-1" role="dialog" aria-labelledby="myMdlReposicion" data-bs-backdrop="static" style="display:none">                         
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header" >
                            <h4 class="modal-title" id="myMdlReposicion">Ajuste de Stock</h4>
                        </div>
                               
                        <div class="modal-body" style="color:black;">                               
                                <div class="container-fluid">      
                                   
                                </div>
                        </div>   
                        <div class="modal-footer">
                            <asp:button runat="server" ID="btnAceptarMDLSave" class="btn btn-primary" data-bs-dismiss="modal" Text="Aceptar"></asp:button>  
                            <asp:button runat="server" ID="btnCerrarMDLEdit" class="btn btn-success" data-bs-dismiss="modal" Text="Cerrar"></asp:button>   
                        </div>
                    </div> <!-- modal content -->
                </div> <!-- modal dialog -->        
            </div>
        <!-- modal fade --> 

 <!-- ventana modal NUEVA REPOSICION-->   
<div class="modal fade" id="myModalRepo2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Contenido Externo</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <iframe id="iframeContent" src="" frameborder="0" style="width:100%; height:400px;"></iframe>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>



      <script >	

          function showModalReposicion() {
              $("#MdlReposicion").modal("show");
          }

          function showModalAtencion() {
              $("#MdlAtencion").modal("show");
          }
          function showModalFormaPAgo() {
              $("#MdlFormaPago").modal("show");
          }

          function openModal() {
              // Definir la URL de la página que deseas cargar en el iframe
              var url = "Repisiciones_Emision.aspx";

              // Establecer la URL en el iframe
              document.getElementById('iframeContent').src = url;

              // Mostrar la ventana modal usando Bootstrap
              $('#myModalRepo2').modal('show');
          }
      </script>

</asp:Content>
