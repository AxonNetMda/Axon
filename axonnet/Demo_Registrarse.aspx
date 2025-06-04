<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Demo_Registrarse.aspx.vb" Inherits="axonnet.Demo_Registrarse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Registrate</title>
     <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link href="../Content/bootstrap.css" rel="stylesheet"/>
    <script src="Scripts/bootstrap.bundle.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="layoutAuthentication">
            <div id="layoutAuthentication_content">
                <main>
                    <div class="container">                       
                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <div class="card shadow-lg border-0 rounded-lg mt-6">
                                    <div class="card-header">
                                        <div class="row">
                                            <div class="col-3">
                                                <img src="../Imagenes/logo.png" width="100" />
                                            </div>
                                            <div class="col-9">
                                                <h3 class="text-center font-weight-light my-4">Registrarse Demo</h3>

                                            </div>
                                            </div>
                                    </div>
                                    <div class="card-body">
                                            <div class="form-floating mb-3">
                                                <label for="txtNombre" class="form-label">Nombre</label>
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-floating mb-3">
                                                <label for="txtTelefono" class="form-label">Teléfono</label>
                                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
                                                    ErrorMessage="El teléfono es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-floating mb-3">
                                                <label for="txtEmail" class="form-label">Ingrese su correo electrónico</label>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                    ErrorMessage="El email es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>

                                            <asp:Button ID="btnEnviar" runat="server" CssClass="btn btn-primary" Text="Enviar" OnClick="btnEnviar_Click" />

                                            <!-- Botón Volver sin validación -->
                                            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary" Text="Volver" PostBackUrl="~/index.aspx" CausesValidation="false"/>

                                            <asp:Label runat="server" ID="lblmensaje" text=""></asp:Label>
       
                                    </div>
                                    <div class="card-footer">

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>

    <!-- Modal de Confirmación -->
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalLabel">Verificación Exitosa</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>Su correo ha sido verificado con éxito. Será redirigido en breve.</p>
                </div>
            </div>
        </div>
    </div>

    <script>
        function mostrarModal() {
            var myModal = new bootstrap.Modal(document.getElementById('modalConfirmacion'));
            myModal.show();
            setTimeout(function () {
                window.location.href = 'index.aspx';
            }, 3000);
        }
    </script>
    </form>
</body>
</html>
