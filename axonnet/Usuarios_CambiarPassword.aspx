<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="Usuarios_CambiarPassword.aspx.vb" Inherits="axonnet.Usuarios_CambiarPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formMaster" runat="server">
     <link href="css/styles.css" rel="stylesheet" />
    <link href="css/paraimagenes.css" rel="stylesheet" />
  
    <link href="css/check_css.css" rel="stylesheet" />
 <main>
         <div id="layoutAuthentication">
 
            <div id="layoutAuthentication_content" >
                    <div class="container">

                        <div class="row justify-content-center">               
                           

                            <div class="col-lg-5">
                                <h3 class="text-center font-weight-light my-4">Cambiar Contraseña</h3>
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header bg-warning">
                                      
                                       <h2><asp:label runat="server" ID="lblNombreUsuario" cssclass="text-center font-weight-light my-4" Text="" /></h2>
                                    </div>                                            
                                        </div>                                    
                                    <div class="card-body">
                                        <div class="small mb-3 text-muted"></div>
                                        
                                            <div class="form-floating mb-3 shadow-lg border-0 rounded-lg mt-5">
                                                <asp:textbox runat="server" cssclass="form-control " id="txtActual" TextMode="Password" Text="" placeholder="Contraseña actual" />
<%--                                                <input class="form-control" id="inputActual" type="text" placeholder="Contraseña actual" />--%>
                                                <label for="txtActual">Contraseña Actual</label>
                                            </div>
                                         <div class="form-floating mb-3 shadow-lg border-0 rounded-lg mt-5">
                                                <asp:textbox runat="server" cssclass="form-control" id="txtNueva" TextMode="Password" Text="" placeholder="Contraseña" />
                                                <label for="textNueva">Contraseña</label>
                                            </div>
                                         <div class="form-floating mb-3 shadow-lg border-0 rounded-lg mt-5">
                                                 <asp:textbox runat="server" cssclass="form-control" id="txtConfirmar" TextMode="Password" Text="" placeholder="Confirmar Contraseña" />
                                                <label for="txtConfirmar">confirmar Constraseña</label>
                                            </div>
                                            <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
<%--                                                <a class="small" href="Login.aspx">Regresar</a>--%>
                                                 <asp:button runat="server" ID="BtnVolver" cssclass="btn btn-success" text="Regresar" PostBackUrl="~/Usuarios_lista.aspx"/>
                                                <asp:button runat="server" ID="BtnAceptar" cssclass="btn btn-primary" text="Aceptar"/>
                                            </div>
                                        
                                    </div>
                                    <div class="card-footer text-center py-3">
                                        <div class="small">Axon NET</div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
               
            </div>
            <div id="layoutAuthentication_footer">
                <footer class="py-4 bg-light mt-auto">
                    <div class="container-fluid px-4">
                        <div class="d-flex align-items-center justify-content-between small">
                            <div class="text-muted">Copyright &copy; Your Website 2023</div>
                            <div>
                                <a href="#">Privacy Policy</a>
                                &middot;
                                <a href="#">Terms &amp; Conditions</a>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
      

<%--Abrir modal para Validar respuesta                                                                                                                                                                      --%>
<div class="modal fade" id="MdlAtencion" tabindex="-1" role="dialog" aria-labelledby="myMdlAtencion" data-bs-backdrop="static" style="display:none">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header bg-black text-white">
        <h5 class="modal-title">ATENCION<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
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
 </main>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>    

</asp:Content>
