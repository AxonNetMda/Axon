<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Adminitracion.Master" CodeBehind="Usuarios_lista.aspx.vb" Inherits="axonnet.Usuarios_lista" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="Microsoft.VisualBasic"%>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.UI"%>
<asp:Content ID="Content1" ContentPlaceHolderID="formMaster" runat="server">
   <script src="Scripts/WebForms/MSAjax/MicrosoftAjax.js"></script>
   <script src="Scripts/WebForms/MSAjax/MicrosoftAjaxWebForms.js"></script>
   <link href="css/styles.css" rel="stylesheet" />
    <link href="css/paraimagenes.css" rel="stylesheet" />
  
    <link href="css/check_css.css" rel="stylesheet" />


   <div class="container-fluid px-4">
        <h1 class="mt-4">Usuarios</h1>
        <ol class="breadcrumb mb-4">
            <li class="breadcrumb-item"><a href="Default.aspx">ADMINISTRACION</a></li>
            <li class="breadcrumb-item active">Usuarios</li>
        </ol>
        <div class="card mb-4">
            <div class="card-body">
                LISTADO DE USUARIOS
            </div>
            <div class="card mb-4">
                <div class="card-header">
                    <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-primary" Text="Nuevo Usuario" />
                </div>
            </div>                         
        </div>
        <div class="card mb-4">
             <div class="card-header">
                <i class="fas fa-table me-1"></i>
                USUARIOS
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
                                <asp:TextBox runat="server" ID="txtBuscar" class="datatable-input" placeholder="Buscar..." type="search" title="Buscar un USUARIO" aria-controls="datatablesSimple" AutoPostBack="True" OnTextChanged="txtBuscar_TextChanged" ></asp:TextBox>
                             </div>
                        </div>
                        <div>
                           <div class="table-responsive">
                                <div class="datatable-container">
                                    <asp:GridView ID="dgvData" runat="server" CssClass="datatable-table"  AutoGenerateColumns="False" DataKeyNames="idUsuario" DataSourceID="SqlUsuarios" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="idUsuario" HeaderText="idUsuario" InsertVisible="False" ReadOnly="True" SortExpression="idUsuario" ControlStyle-Width="50px" >
                                                <ControlStyle Width="50px"></ControlStyle>
											<HeaderStyle Width="50px" />
											<ItemStyle HorizontalAlign="Right" />
											</asp:BoundField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                            <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" SortExpression="Domicilio" Visible="false" />
                                            <asp:BoundField DataField="CodigoPostal" HeaderText="CodigoPostal" SortExpression="CodigoPostal" Visible="false" />
                                            <asp:BoundField DataField="Localidad" HeaderText="Localidad" SortExpression="Localidad" Visible="false" />
                                            <asp:BoundField DataField="Provincia" HeaderText="Provincia" SortExpression="Provincia" Visible="false" />
                                            <asp:BoundField DataField="Celular" HeaderText="Celular" SortExpression="Celular" />  
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />                                            
                                            <asp:BoundField DataField="idRol" HeaderText="idRol" SortExpression="idRol" Visible="false" />
                                            <asp:BoundField DataField="RolNombre" HeaderText="Rol" SortExpression="Rol" />                                                      
                                            <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ItemStyle-CssClass="checkbox-wrapper-40" >
                                            <HeaderStyle Width="80px" />
											</asp:CheckBoxField>
                                            <asp:BoundField DataField="FotoNombre" HeaderText="FotoNombre" SortExpression="FotoNombre" Visible="false" />
                                            <asp:BoundField DataField="FotoPath" HeaderText="FotoPath" SortExpression="FotoPath" Visible="false" />
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
                                            <asp:ButtonField ButtonType="Button" CommandName="blanqueraclave" ItemStyle-HorizontalAlign="Center">                                
                                                        <ControlStyle CssClass="btn btn-warning fas fa-unlock-alt" ></ControlStyle>
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                            </asp:ButtonField> 
                                    </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                     </asp:GridView>
                                    <asp:SqlDataSource ID="SqlUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:CADENA2 %>" SelectCommand="<% scomando %>"></asp:SqlDataSource>
                                </div>
                           </div>
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
        <h5 class="modal-title">ATENCION/h5><button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
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
<%--***************************************************************************************** --%>
<%--Ventana modal para editar un usuario                                                                                                                                                                      --%>
<div class="modal fade" id="MdlNuevo" tabindex="-1" role="dialog" aria-labelledby="myMdlNuevo" data-bs-backdrop="static" style="display:none">
     <style>
        /* Estilo para ocultar el input de tipo file */
        .input-file {
            display: none;
        }

        /* Estilo para el contenedor del botón personalizado */
        .custom-file-upload {
            display: inline-block;
            border: 1px solid #ccc;
            padding: 6px 12px;
            cursor: pointer;
            background-color: #f0f0f0;
            border-radius: 4px;
        }

        /* Estilo para el texto dentro del contenedor del botón personalizado */
        .custom-file-upload span {
            font-size: 14px;
            font-weight: bold;
        }
    </style>
	

  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
         <div class="modal-header" style="background-color:black; color:white">
            <h3><span class="fa fa-user"></span> ALTA DE USUARIO</h3>
         </div>
         <div class="modal-body">
            <div class="panel panel-default">                                
                <div class="panel-body">
                     <div class="row"> 
                           <div class="col-md-4">                                        
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">                                
                                        <ContentTemplate>
                                            <asp:FileUpload ID="fileUpload1" runat="server" accept="image/*"  onclick="document.getElementById('fileInput1').click()" /><br />
                                            <asp:Image ID="imgMostrar" runat="server" Width="200px" Height="200px" /><br />
                                            <asp:Label ID="lblNombreImagen" runat="server" Text="Nombre de la Imagen"></asp:Label>
                                        </ContentTemplate>
                             </asp:UpdatePanel>  
                            </div>
                           
                           <div class="col-md-8">                      
                                    <div class="form-floating mb-3">
                                        <asp:label runat="server" class="form-control" id="txtIdUsuario" placeholder="ID" Text="0" Width="100"></asp:label>
                                        <label for="inputNombre">ID</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtNombre" placeholder="Nombre"></asp:TextBox>
                                        <label for="inputNombreN">Nombre</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtDomicilio" placeholder="Domicilio"></asp:TextBox>
                                        <label for="inputDomicilio">Domicilio</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtCodigoPostal" placeholder="Codigo Postal" Width="100"></asp:TextBox>
                                        <label for="inputCodigopostal">Codigo Postal</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtLocalidad" placeholder="Localidad"></asp:TextBox>
                                        <label for="inputlocalidad">Localidad</label>
                                    </div>
                                     <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtProvincia" placeholder="Provincia"></asp:TextBox>
                                        <label for="inputprovincia">Provincia</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtCelular" placeholder="Celular"></asp:TextBox>
                                        <label for="inputcelular">Celular</label>
                                    </div>
                                   <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtEmail" placeholder="Email"></asp:TextBox>
                                        <label for="inputemail">Email</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:DropDownList runat="server" class="form-control" id="cboRol" placeholder="Rol"></asp:DropDownList>
                                        <label for="inputRol">Rol</label>
                                        <asp:HiddenField runat="server" ID="txtIdRol" Value="0" />
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:DropDownList runat="server" class="form-control" id="cboEstado" placeholder="Estado"></asp:DropDownList>
                                        <label for="inputEstado">Estado</label>
                                        <asp:HiddenField runat="server" ID="txtIdEstado" Value="0" />
                                    </div>
                                    <div>
                                         <asp:Label runat ="server" id="lblMessage" Text=""></asp:Label>
                                    </div>
                            </div>
                        </div> 
                     </div>
                </div>
           </div>  
            
        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
             <asp:Button runat="server" ID="BtnGuardar" cssclass="btn btn-success" Text="Guardar" onclick="BtnGuardar_Click"/>
         </div>    
        </div>
    </div>
 </div>
                

<%--***************************************************************************************** --%>


    <script type="text/javascript" src="js/plugins/bootstrap/bootstrap-file-input.js"></script>
    <script src="js/datatables/dataTables.fixedHeader.min.js"></script>
    <script src="js/datatables/jquery.dataTables.min.js"></script>
    <script src="js/datatables/jquery3701.js"></script>
    <link href="css/datatables/fixedHeader.dataTables.min.css" rel="stylesheet" />
    <link href="css/datatables/jquery.dataTables.min.css" rel="stylesheet" />
         <script >
             document.getElementById('fileInput1').addEventListener('change', function (event) {
                 cargarImagen(event, 'imagen');
             });

             function cargaNombreImagen(event, imgId) {
                 var input = event.target;
                 if (input.files && input.files[0]) {
                     var reader = new FileReader();
                     reader.onload = function (e) {
                         var nombre = document.getElementsByTagName(imgId);
                         nombre = e.target.result;
                     }

                 } else {
                     alert("Por favor, seleccione un archivo de imagen.");
                 }

             }
           // const { filereader } = require("modernizr");
             function uploadFile() {
                 // Obtén el formulario que contiene el FileUpload
                 var form = document.getElementById('<%= Form().ClientID %>');
               if (form) {
                   form.submit();
               } else {
                   alert("Formulario no encontrado.");
               }
             }
             function showModalMensaje() {
                 $("#MMensaje").modal("show");
             }
           function showModalAtencion() {
               $("#MdlAtencion").modal("show");
           }
             function showModalEditar() {
                 $("#MdlEditar").modal("show");
             }
             function showModalNuevo() {
                 $("#MdlNuevo").modal("show");
             }



             $(document).ready(function (e) {
                     $('#myModal').on('show.bs.modal', function (e) {
                         var id = $(e.relatedTarget).data().id;
                         $(e.currentTarget).find('#lista').val(id);
                     });
             });

           


         </script>          
           
       
          
		  
        
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

