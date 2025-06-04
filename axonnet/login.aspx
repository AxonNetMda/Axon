<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="axonnet.Login" %>
<%@ Import Namespace="capaAdministracion.MisFunciones" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
        <title>Login - Sin limites</title>
        <link href="css/styles.css" rel="stylesheet" />
        <script src="https://use.fontawesome.com/releases/v6.3.0/js/all.js" crossorigin="anonymous"></script>
</head>
<body>
   <form runat="server">
        <div id="layoutAuthentication">
            <div id="layoutAuthentication_content">
                <main>
                    <div class="container">                       
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header bg-dark">
                                        <div class="row">
                                            <div class="col-3">
                                                <img src="Imagenes/logo.png" width="100" />
                                            </div>
                                            <div class="col-9">
                                                <h3 class="text-center font-weight-light my-4 text-white">Administracion Login</h3></div>
                                            </div>
                                        </div>
                                    <div class="card-body">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="cboSucursales" runat="server" CssClass="form-control" placeholder="Sucursal" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdSucursal"></asp:DropDownList>
<%--                                                <asp:TextBox runat="server" class="form-control" id="TextBox1" type="email" placeholder="name@example.com"></asp:TextBox>--%>
<%--                                                <input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" />--%>
                                                <label for="inputEmail">Sucursales</label>
                                            </div>
                                            <div class="form-floating mb-3">
                                                <asp:TextBox runat="server" class="form-control" id="txtEmail" type="email" placeholder="name@example.com"></asp:TextBox>
<%--                                                <input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" />--%>
                                                <label for="inputEmail">Email</label>
                                            </div>
                                            <div class="form-floating mb-3">
                                                <asp:TextBox runat="server" class="form-control" id="txtPassword" type="password" placeholder="Password"></asp:TextBox>
<%--                                                <input class="form-control" id="inputPassword" type="password" placeholder="Password" />--%>
                                                <label for="inputPassword">Password</label>
                                            </div>
                                            <div class="form-check mb-3">
                                                <asp:CheckBox runat="server" cssclass="form-check-inline" id="chkRecordarPassword" />
<%--                                                <input class="form-check-input" id="inputRememberPassword" type="checkbox" value="" />--%>
                                                <label class="form-check-label" for="inputRememberPassword">Recordar contraseña</label>
                                            </div>
                                            <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                                               
                                                <a class="small" href="password.html">Olvidaste la clave?</a>
                                                <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="btn btn-primary" />
                                            </div>
                                        
                                    </div>
                                    <div class="card-footer text-center py-3">
                                        <div class="small"><a href="Registrarse.aspx">Solicitar acceso al administrador? Sign up!</a></div>
                                        <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
            <div id="layoutAuthentication_footer">
                <footer class="py-4 bg-light mt-auto">
                    <div class="container-fluid px-4">
                        <div class="d-flex align-items-center justify-content-between small">
                            <div class="text-muted">Desarrollado por AXON NET  - Copyright &copy; Tienda On line <%=Date.Today.Year %></div>
                            <div>
                                <a href="#">Politica de Privacidad</a>
                                &middot;
                                <a href="#">Todos los derechos reservados </a>
                                
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>
    </form>    

    <%--Abrir modal para Validar respuesta                                                                                                                                                                      --%>
<div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="staticBackdropLabel">Modal title</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        Guardar cambios
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
        <button type="button" class="btn btn-primary" data-bs-target="#Mensaje" data-bs-toggle="modal">Guardar</button>

      </div>
    </div>
  
  </div>
</div>  
 </body>   
</html>
