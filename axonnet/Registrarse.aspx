<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registrarse.aspx.vb" Async="true" Inherits="axonnet.Registrarse" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Registrate</title>
     <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet">
    <script src="Scripts/bootstrap.bundle.min.js"></script>
</head>
<body>

     <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand d-flex align-items-center flex-wrap" href="#">
                <img src="imagenes/logo.png" alt="Logo Empresa" class="img-fluid me-2" width="100"/>
                <span class="fw-bold d-block d-md-none text-white">Control de Stock</span>
                <span class="fw-bold d-none d-md-block text-white">REGISTRARSE COMO USUARIO</span>
            </a>          
        </div>
    </nav>
    <div class="container container-sm">
        <div class="container mt-5">
<%--        <h2 class="text-center">Registracion</h2>--%>
        <form runat="server">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <div class="mb-3">
                <label for="txtTelefono" class="form-label">Teléfono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
                    ErrorMessage="El teléfono es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <div class="mb-3">
                <label for="txtEmail" class="form-label">Ingrese su correo electrónico:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <asp:Button ID="btnEnviar" runat="server" CssClass="btn btn-primary" Text="Enviar" OnClick="btnEnviar_Click" />

            <!-- Botón Volver sin validación -->
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary" Text="Volver" PostBackUrl="~/index.aspx" CausesValidation="false"/>

            <asp:Label runat="server" ID="lblmensaje" text=""></asp:Label>
        </form>

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
</body>
</html>
