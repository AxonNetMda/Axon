<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="Usuarios_Lista_ABM.aspx.vb" Inherits="axonnet.Usuarios_Lista_ABM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formMaster" runat="server">
     <link href="css/styles.css" rel="stylesheet" />
    <link href="css/paraimagenes.css" rel="stylesheet" />
  
    <link href="css/check_css.css" rel="stylesheet" />
    <style>
        #fileUpload1 {
            display: none;
        }
    </style>
    <div class="container-md"> <h4><%=sTitulo %> </h4>
        <hr />
    </div>
   
	<div class="container">
      
        <br />
		<div class="card-header bg-black text-white" >
            
		</div>
		<div class="card-body">
                     <div class="row"> 
                           <div class="col-md-2" style="background-color:silver"> 
                               <br />
                                <asp:Image ID="imagen" CssClass="img-thumbnail" runat="server" ImageUrl="assets/img/no-image.jpg" Width="200px" />
                                <br />
                                
<%--                                <input type="file" id="fileUpload1" accept="image/*" onchange="mostrarImagen(event)" runat="server" />  --%>
                                <p><asp:FileUpload  Type="file" id="fileUpload1" accept="image/*" onchange="mostrarImagen(event)" runat="server" Width="110" Font-Size="Small" CssClass="btn btn-default"/></p>
<%--                                <p><asp:Button ID="btnCargar" runat="server" Text="Sin Imagen" OnClientClick="document.getElementById('fileUpload1').click(); return false;" CssClass="btn btn-secondary btn-sm" /></p>--%>
                               <asp:HiddenField ID="hNombreImagen" runat="server" Value="no-imagen.jpg" />
                                <br />
                                <br />
                                <hr />
                                <asp:Button runat="server" ID="BtnGuardar" cssclass="btn btn-primary" Text="Guardar" onclick="BtnGuardar_Click"/>
                                <asp:button runat="server" ID="BtnCancelar" cssclass="btn btn-success" Text="Cancelar" PostBackUrl="~/Usuarios_lista.aspx?idUsuario=0&Titulo=''&Accion=''" />

                            </div>
                           <div class="col-md-10">  
                                <asp:Panel ID="PanelDatos" runat="server">
                                    <div class="form-floating mb-3">
                                        <asp:label runat="server" class="form-control text-capitalize" id="txtIdUsuario" placeholder="ID" Text="0" Width="100"/>
                                        <label for="inputNombre">ID</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                      
                                        <asp:TextBox runat="server" class="form-control text-capitalize" id="txtNombre" placeholder="Nombre"></asp:TextBox>
                                        <label for="inputNombre">Nombre</label>
                                       
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtDomicilio" placeholder="Domicilio"></asp:TextBox>
                                        <label for="inputDomicilio">Domicilio</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:TextBox runat="server" class="form-control" id="txtCodigoPostal" placeholder="Codigo Postal" Width="120"></asp:TextBox>
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

                                </asp:Panel>
                            </div>
                        </div> 
         
		</div>
		<div class="card-footer">
             <asp:Label runat="server" ID="lblMensaje" Text="" ></asp:Label> 
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
		//document.getElementById('fileInput1').addEventListener('change', function (event) {
		//	cargarImagen(event, 'imagen');
  //      });
		//function cargarImagen(event) {
		//	const file = event.target.files[0];
		//	if (file) {
		//		const reader = new FileReader();
		//		reader.onload = function (e) {
		//			document.getElementById('imagen').src = e.target.result;
		//		};
		//		reader.readAsDataURL(file);
		//	}
		//}



		function mostrarImagen(event) {
			const file = event.target.files[0];
			if (file) {
				const reader = new FileReader();
				reader.onload = function (e) {
					document.getElementById('<%= imagen.ClientID %>').src = e.target.result;
				 };
				 reader.readAsDataURL(file);
			 }
		 }
		
           function showModalMensaje() {
			   $("#MdlAtencion").modal("show");
             }

	</script>
</asp:Content>
