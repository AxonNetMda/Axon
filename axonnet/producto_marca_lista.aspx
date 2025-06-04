<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="producto_marca_lista.aspx.vb" Inherits="axonnet.producto_marca_lista" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="Microsoft.VisualBasic"%>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.UI"%>
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
            <li class="breadcrumb-item active">Marcas</li>
        </ol>
        <div class="card mb-4">           
            <div class="card mb-4">
                <div class="card-header">
                    <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-primary" Text="Nueva Marca" />
                </div>
            </div>                         
        </div>
        <div class="card mb-4">
             <div class="card-header">
                <i class="fas fa-table me-1"></i>
                MARCAS
            </div>
             <div class="card-body">
                   <div class="datatable-wrapper datatable-loading no-footer sortable searchable fixed-columns">
                        <div class="datatable-top">
                             <div class="dropdown">
                                    <label>
                                        <asp:DropDownList ID="cboMostrar" runat="server" CssClass="dropdown datatable-selector" Width="80px" autopostback="true">
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
                                <asp:TextBox runat="server" ID="txtBuscar" class="datatable-input" placeholder="Buscar..." type="search" title="Buscar un MARCA" aria-controls="datatablesSimple" AutoPostBack="True" OnTextChanged="txtBuscar_TextChanged" ></asp:TextBox>
                             </div>
                        </div>
                        <div>
                           <div class="table-responsive">
                                <div class="datatable-container">
                                    <asp:GridView ID="dgvData" runat="server" CssClass="datatable-table" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="idMarca" DataSourceID="SqlMarca" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="idMarca" HeaderText="idMarca" InsertVisible="False" ReadOnly="True" SortExpression="idMarca" ControlStyle-Width="50px" >
                                                <ControlStyle Width="50px"></ControlStyle>
											<HeaderStyle Width="50px" />
											<ItemStyle HorizontalAlign="Right" />
											</asp:BoundField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                            <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ItemStyle-CssClass="checkbox-wrapper-40" >
                                            <HeaderStyle Width="80px" />
											</asp:CheckBoxField>
                                            <asp:ButtonField ButtonType="Button" CommandName="editar" ItemStyle-HorizontalAlign="Center">                                
                                                                 <ControlStyle CssClass="btn btn-primary fas fa-edit" ></ControlStyle>
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
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <PagerSettings Mode="Numeric" Visible="true" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                     </asp:GridView>
                                    <asp:SqlDataSource ID="SqlMarca" runat="server" ConnectionString="<%$ ConnectionStrings:CADENA2 %>" SelectCommand="<% scomando %>"></asp:SqlDataSource>
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
  
       <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/umd/simple-datatables.min.js" crossorigin="anonymous"></script>
        <script src="js/datatables-simple-demo.js"></script>




      <!-- START PRELOADS -->
        <audio id="audio-alert" src="audio/alert.mp3" preload="auto"></audio>
        <audio id="audio-fail" src="audio/fail.mp3" preload="auto"></audio>
        <!-- END PRELOADS -->          
        
    <!-- START SCRIPTS -->
        <!-- START PLUGINS -->
        <script type="text/javascript" src="js/plugins/jquery/jquery.min.js"></script>
        <script type="text/javascript" src="js/plugins/jquery/jquery-ui.min.js"></script>
        <script type="text/javascript" src="js/plugins/bootstrap/bootstrap.min.js"></script>
        
        <script type="text/javascript" src="js/plugins/jquery/jquery-migrate.min.js"></script>
        <!-- END PLUGINS -->

        <!-- START THIS PAGE PLUGINS-->        
        <script type='text/javascript' src='js/plugins/icheck/icheck.min.js'></script>
        <script type="text/javascript" src="js/plugins/mcustomscrollbar/jquery.mCustomScrollbar.min.js"></script>  
        
        <script type="text/javascript" src="js/plugins/form/jquery.form.js"></script>
        
        <script type="text/javascript" src="js/plugins/cropper/cropper.min.js"></script>
        <!-- END THIS PAGE PLUGINS-->        

        <!-- START TEMPLATE -->
        <script type="text/javascript" src="js/settings.js"></script>        
        
        <script type="text/javascript" src="js/plugins.js"></script>        
        <script type="text/javascript" src="js/actions.js"></script>
        
        <script type="text/javascript" src="js/demo_edit_profile.js"></script>
        <!-- END TEMPLATE -->
</asp:Content>
