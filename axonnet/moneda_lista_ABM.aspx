<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="moneda_lista_ABM.aspx.vb" Inherits="axonnet.moneda_lista_ABM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="css/check_css.css" rel="stylesheet" />
  <link href="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/style.min.css" rel="stylesheet" />
  <link href="css/styles.css" rel="stylesheet" />
  <script src="https://use.fontawesome.com/releases/v6.3.0/js/all.js" crossorigin="anonymous"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formMaster" runat="server">
       <br />
     <div class="container">
           <div class="row justify-content-center">
                <div class="col-lg-8">                                     
                    <%-- <form class="form-horizontal">--%>
                    <div class="card shadow-lg border-0 rounded-lg mt-8 ">
                        <div class="card-header  bg-warning text-black">
                            <h3 class="text-center font-weight-light my-4"><strong><asp:Label runat="server" ID="lblTitulo" ></asp:Label></strong></h3>                                   
                        </div>                       
                        <div class="card-body"> 
                           <asp:Panel runat="server" ID="PanelDatos" CssClass="align-content-center">
                               <asp:HiddenField runat="server" ID="txtidMoneda"/>
                                 <div class="row justify-content-center">                                       
                                        <div class="form-group">
                                            <div class="form-floating mb-3">
                                                <asp:TextBox runat="server" class="form-control" id="txtNombre"></asp:TextBox>
                                                <label for="txtNombre">Moneda</label>                                   
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-floating mb-3">
                                                <asp:TextBox runat="server" class="form-control" id="txtFechaCotizacion" TextMode="Date"></asp:TextBox>
                                                <label for="txtFechaCotizacion">Fecha Cotizacion</label>                                   
                                            </div>
                                        </div>

                                        <div class="form-group">
                                                <div class="form-floating mb-3">
                                                <asp:TextBox runat="server" class="form-control" id="txtCotizacion"></asp:TextBox>
                                                <label for="txtCotizacion">Cotizacion</label>                                   
                                            </div>
                                        </div>   
                                     <div class="form-group">
                                            <div class="form-floating mb-3">
                                                    <asp:checkbox runat="server" class="form-control checkbox-wrapper-40" id="chkMonedaPrincipal" text="Moneda principal"></asp:checkbox>
                                                    <label for="chkMonedaPrincipal">Moneda principal</label> 
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="form-floating mb-3">
                                                    <asp:DropDownList runat="server" class="form-control" id="cboEstado" placeholder="Estado"></asp:DropDownList>
                                                    <label for="cboEstado">Estado</label> 
                                                <asp:HiddenField runat="server" ID="txtIdEstado" />
                                            </div>
                                        </div>
                                 </div>                               
                            </asp:Panel> 
                        </div>                                
                        <footer class="py-4 bg-light mt-auto">
                            <div class="container-fluid px-4">
                                <div class="d-flex align-items-end justify-content-between small">
                                    <asp:button runat="server" id="BtnCancelar" cssclass="btn btn-success" Text="Cancelar" PostBackUrl="~/monedas_Lista.aspx" />
                                    <asp:button runat="server" Id="BtnGuardar" cssclass="btn btn-primary" Text="Aceptar "  onclick="BtnGuardar_Click" />
                                </div>
                            </div>
                        </footer>

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
</asp:Content>
