<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Usuarios_ResetearPassword.aspx.vb" Inherits="axonnet.Usuarios_ResetearPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
        <title>Resetear Password  - Administracion</title>
        <link href="css/styles.css" rel="stylesheet" />
        <script src="https://use.fontawesome.com/releases/v6.3.0/js/all.js" crossorigin="anonymous"></script>

</head>
<body class="bg-primary">
    <form id="form1" runat="server">
     <div id="layoutAuthentication">
            <div id="layoutAuthentication_content" style="background-color: #FFFFFF">
                <main>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header">
                                        <div class="row">
                                            <div class="col-3">
                                                <img src="Imagenes/logowh130.fw.png" />
                                            </div>
                                            <div class="col-9">
                                                <h3 class="text-center font-weight-light my-4">Recuperar Contraseña</h3></div>
                                            </div>
                                        </div>                                    
                                    <div class="card-body">
                                        <div class="small mb-3 text-muted">Entre su email y le enviaremos el link para crear una nueva contraseña.</div>
                                        
                                            <div class="form-floating mb-3">
                                                <input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" />
                                                <label for="inputEmail">Email</label>
                                            </div>
                                            <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                                                <a class="small" href="login.aspx">Regresar a Login</a>
                                                <a class="btn btn-primary" href="login.html">Resetear Contraseña</a>
                                            </div>
                                        
                                    </div>
                                    <div class="card-footer text-center py-3">
                                        <div class="small"><a href="#">Registrarse</a></div>
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
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>    </form>
</body>
</html>
