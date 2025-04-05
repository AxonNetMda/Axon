﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="producto_marca_lista_ABM.aspx" Inherits="axonnet.producto_marca_lista_ABM" %>
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
  
	<div class="container">
       <div class="card shadow-lg border-0 rounded-lg mt-10">
            <br />
		    <div class="card-header bg-warning text-black">
                <h4 class="text-center font-weight-light my-1"><strong><asp:Label runat="server" ID="lblTitulo" ></asp:Label><%=sTitulo %></strong></h4>                                   
            </div> 
		<div class="card-body">
		   <div class="card-body justify-content-center"> 
                           <div class="col-md-10">  
                                <asp:Panel ID="PanelDatos" runat="server">
                                    <div class="form-floating mb-3">
                                        <asp:label runat="server" cssclass="form-control text-capitalize" id="txtIdMarca" placeholder="ID" Text="0" Width="100"/>
                                        <label for="txtIdMarca">ID</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                      
                                        <asp:TextBox runat="server" class="form-control text-capitalize" id="txtNombre" placeholder="Nombre"></asp:TextBox>
                                        <label for="txtNombre">Nombre</label>
                                       
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:DropDownList runat="server" class="form-control" id="cboEstado" placeholder="Estado" Width="200"></asp:DropDownList>
                                        <label for="cboEstado">Estado</label>
                                        <asp:HiddenField runat="server" ID="txtIdEstado" Value="0" />
                                    </div>
                                    <div>
                                         <asp:Label runat ="server" id="lblMessage" Text=""></asp:Label>
                                    </div>

                                </asp:Panel>
                            </div>
                  		    <div class="card-footer">
                                <footer class="py-4 bg-light mt-auto">
                                    <div class="container-fluid px-4">
                                        <div class="d-flex align-items-end justify-content-between small">
                                            <asp:Button runat="server" ID="BtnGuardar" cssclass="btn btn-primary" Text="Guardar" onclick="BtnGuardar_Click"/>
                                            <asp:button runat="server" ID="BtnCancelar" cssclass="btn btn-success" Text="Cancelar" PostBackUrl="~/Producto_marca_lista.aspx?idMarca=0&Titulo=''"/>
                                        </div>
                                    </div>
                                </footer>
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
        <h5 class="modal-title">ATENCION</h5><button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button> 
      </div>
      <div class="modal-body text-black">
            <asp:Label runat="server" id="lblMensajeAtencion" CssClass="form-label" Text=""></asp:Label>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>

      </div>
    </div>
  
  </div>
</div>   
    <script>
	
           function showModalMensaje() {
			   $("#MdlAtencion").modal("show");
             }

    </script>

</asp:Content>
