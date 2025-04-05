<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="Ventas_Emision.aspx.vb" Inherits="axonnet.Ventas_Emision" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="formMaster" runat="server" >
            <%If Session("sNombreUsuario") = "" Then
                Response.Redirect("login.aspx?idUsuario=0")
            End If %>

    <div class="container" autocomplete="off">
         <div class="card shadow-lg border-0 rounded-lg mt-xxl-auto"s>
                <div class="card-header bg-warning text-black">
                    <h4 class="text-center font-weight-light my-1"><strong><asp:Label runat="server" ID="lblTitulo" >COMPROBANTE DE VENTA</asp:Label></strong></h4>                                   
                </div>                       
                <div class="card-body justify-content-center"> 
                    <div class="row justify-content-center">                            
                            <asp:HiddenField runat="server" ID="hfidCompra" Value="0"/>
                            <div class="row">  
                                <div class="col-md-2">
                                        <div class="form-group">                                                    
                                                <div class="form-floating mb-3">
                                                    <asp:TextBox runat="server" class="form-control " id="txtFecha" TextMode="Date" Width="200"></asp:TextBox>
                                                    <label class="text-dark" for="txtFecha">Fecha Venta</label>                                   
                                                </div>
                                        </div>
                                    </div>
                                <div class="col-md-2">
                                     <div class="form-group">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList runat="server" CssClass="form-control" id="cboComprobante" AutoPostBack="true"></asp:DropDownList>
                                                <label for="cboQueEs">Comprobante </label>                                   
                                            </div>
                                      </div>
                                 </div>
                                <div class="col-md-4">
                                     <div class="form-group">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList runat="server" CssClass="form-control" id="cboTipoComprobante" AutoPostBack="true"></asp:DropDownList>
                                                <label for="cboTipoComprobante">Comprobante </label>                                   
                                            </div>
                                      </div>
                                 </div>
                                <div class="col-md-1">
                                    <div class="form-group">                                                    
                                         <div class="form-floating mb-3">
                                             <asp:label runat="server" class="form-control bg-secondary bg-opacity-50" id="lblLetra" Font-Bold="true" Font-Size="Larger"  Width="100"></asp:label>
                                             <label class="text-black" for="lblLetra">Letra</label>                                   
                                         </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">                                                    
                                         <div class="form-floating mb-3">
                                             <asp:TextBox runat="server" class="form-control text-end" id="txtPuntoVenta" Text="00000" onblur="formatearConCeros5(this)" MaxLength="5"  Width="100"  AutoCompleteType="Disabled"></asp:TextBox>
                                             <label class="text-dark" for="txtPuntoVenta"> Pto.Vta</label>                                   
                                         </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">                                                    
                                         <div class="form-floating mb-3">
                                             <asp:TextBox runat="server" class="form-control text-end" id="txtNumeroComprobante"  Text="00000000" onblur="formatearConCeros8(this)" MaxLength="8" Width="200" AutoCompleteType="Disabled"></asp:TextBox>
                                             <label class="text-dark" for="txtPuntoVenta"> Numero </label>                                   
                                         </div>
                                    </div>  
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">                                                    
                                         <div class="form-floating mb-3 align-content-center">
                                            <asp:DropDownList runat="server" CssClass="form-control bg-secondary text-white" id="cboDetalle" AutoPostBack="true"></asp:DropDownList>  
                                            <label class="text-white" for="cboDetalle">Comprobante</label>    
                                         </div>
                                    </div>  
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">                                                    
                                         <div class="form-floating mb-3">
                                             <asp:DropDownList runat="server" CssClass="form-control" id="cboCondicion"></asp:DropDownList>
                                             <label class="text-dark" for="cboCondicion"> Condicion Compra </label>                                   
                                         </div>
                                    </div>  
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">                                                    
                                         <div class="form-floating mb-3 align-content-center">
                                            <asp:DropDownList runat="server" CssClass="form-control" id="cboTipoCompra" AutoPostBack="true"></asp:DropDownList>  
                                            <label class="text-dark" for="cboTipoCompra">Tipo Compra</label>    
                                         </div>
                                    </div>  
                                </div>
                                 <div class="col-md-2">
                                                <div class="form-group">                                                    
                                                     <div class="form-floating mb-3">
                                                          <asp:DropDownList runat="server" CssClass="form-control" id="cboConIVaSinIVa" AutoPostBack="true"></asp:DropDownList>  
                                                         <label class="text-dark" for="cboConIVaSinIVa"> Pcio.Unitario </label>                                   
                                                     </div>
                                                </div>  
                                            </div>
                                <div class="col-md-6">
                                          <div class="form-group">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList runat="server" CssClass="form-control" id="cboCliente" Enabled="true" ></asp:DropDownList>
                                                <label for="cboCliente">Cliente</label>                                   
                                            </div>
                                         </div>
                                     </div>

                                <div class="container" display="disable">
                                        <div class="form-group">                                                    
                                                 <div class="form-floating mb-3">
                                                    <asp:Panel runat="server" ID="PanelDatos" CssClass="justify-content-center">

                                                        <hr class="border-danger" style="border-width: medium; border-style: solid" /> 
                                                        <div class="col-md-12">                                        
                                                                <div class="card-header bg-black text-white">
                                                                     <h6 class="text-center font-weight-light my-1"><strong>DETALLE</strong></h6>    
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                           
                                                                     <div class="col-md-2">
                                                                        <div class="form-group">                                                    
                                                                             <div class="form-floating mb-3">
                                                                                <asp:TextBox ID="txtCodigo" runat="server"  class="form-control text-end" ></asp:TextBox>
                                                                                 <label class="text-dark" for="txtCantidad"> CodBarras </label>   
                                                                             </div>   
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                    <div class="form-group">                                                    
                                                                         <div class="form-floating mb-3">
                                                                              <asp:HiddenField runat="server" ID="txtidproducto" />
                                                                             <asp:HiddenField runat="server" ID="txtDescripcion" />
                                                                            <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-control"  AutoPostBack="True" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged"></asp:DropDownList>
                                                                            <label class="text-dark" for="ddlProductos"> Producto </label>   
                                                                         </div>
                                                                     </div>

                                                                 </div>
                                                                    <div class="col-md-1">
                                                                    <div class="form-group">                                                    
                                                                         <div class="form-floating mb-3">
                                                                            <asp:TextBox ID="txtCantidad" runat="server"  class="form-control text-end" ></asp:TextBox>
                                                                             <label class="text-dark" for="txtCantidad"> Cant. </label>   
                                                                         </div>   
                                                                    </div>
                                                                </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">                                                    
                                                                         <div class="form-floating mb-3">
                                                                            <asp:TextBox ID="txtPrecioUnitario" runat="server"  class="form-control text-end" autocomplete="off" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)"></asp:TextBox>
                                                                              <label class="text-dark" for="txtPrecioUnitario"> Precio </label> 
                                                                            <asp:TextBox ID="txtAlicuotaIVA" runat="server"  class="form-control text-end" ReadOnly="True" Visible="false"></asp:TextBox>
                                                     
                                                                         </div>   
                                                                    </div>
                                                                    </div>
                                                                      <div class="col-md-1">
                                                                        <div class="form-group">                                                    
                                                                             <div class="form-floating mb-3">
                                                                                <asp:Button ID="btnAgregarProducto" runat="server" Text="+" cssclass="btn btn-success" Height="60" Font-Size="X-Large" />
                                                                             </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="table-responsive-md">
                                                                    <div class="datatable-container">
                                                                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" CssClass="datatable-table">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="IdProducto" HeaderText="ID" ItemStyle-Width="100" ItemStyle-CssClass="text-end" DataFormatString="{0:00000}" />
                                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion"/>
                                                                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-CssClass="text-end"/>
                                                                                <asp:BoundField DataField="Precio" HeaderText="Precio" ItemStyle-CssClass="text-end"/>        
                                                                                <asp:BoundField DataField="AlicuotaIVA" HeaderText="IVA %" ItemStyle-Width="100" ItemStyle-CssClass="text-end"/>  
                                                                                <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" ItemStyle-Width="100" ItemStyle-CssClass="text-end"/>                                     
                                                                                <asp:ButtonField ButtonType="Button" CommandName="editar" ItemStyle-HorizontalAlign="Center" >   
                                                                                             <ControlStyle CssClass="btn btn-primary fas fa-edit" ></ControlStyle>
                                                                                            <HeaderStyle Width="20px" />
                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                                                                        </asp:ButtonField> 
                                                                                <asp:ButtonField ButtonType="Button" CommandName="Anular" ItemStyle-HorizontalAlign="Center" >   
                                                                                             <ControlStyle CssClass="btn btn-danger fas fa-cancel" ></ControlStyle>
                                                                                            <HeaderStyle Width="20px" />
                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
				                                                                        </asp:ButtonField> 
                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                            <FooterStyle BackColor="#da0300" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="#da0300" Font-Bold="True" ForeColor="White" />
                                                                            <PagerSettings Mode="Numeric" Visible="true" />
                                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EFF3FB" />
                                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                        </asp:GridView>            
                                                                     </div>
                                                                </div>

                                                        </div>                                                                   
                                <hr class="bg-warning border-3">
                                <div class="container-fluid px-4">
<%--                                    <div class="d-flex align-items-end justify-content-between small">--%>
<%--                                        <div class="form-group">                                                    
                                                 <div class="form-floating mb-3">
                                                     <asp:Label ID="lblMensajeFooter" runat="server" Text=""></asp:Label>
                                                 </div>
                                        </div>--%>
                                              <div class="row">
                                                         <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtNetoGravado" runat="server" ReadOnly="true" CssClass="form-control text-end" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true" Width="200" Text="0,00" AutoCompleteType="Disabled"></asp:textbox>
                                                                <label class="text-dark" for="txtNetoGravado"> Neto gravado </label>  
                                                             </div>
                                                             </div>
                                                   
                                                         <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtAlicuota21" runat="server" ReadOnly="true"  CssClass="form-control text-end" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true" Width="200" Text="0,00"></asp:textbox>
                                                                <label class="text-dark" for="txtAlicuota21"> ALicuota 10,50% </label>  
                                                             </div>
                                                        </div>
                                                         <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtAlicuota105" runat="server" ReadOnly="true"  CssClass="form-control text-end" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true" Width="200" Text="0,00"></asp:textbox>
                                                                <label class="text-dark" for="txtAlicuota105"> ALicuota 10,50% </label>  
                                                             </div>
                                                        </div>
<%--                                                        <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtAlicuota21" runat="server" ReadOnly="true" " CssClass="form-control text-end" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true"  Width="200" Text="0,00"></asp:textbox>
                                                                <label class="text-dark" for="txtAlicuota21"> ALicuota 21,00% </label>  
                                                             </div>
                                                        </div>--%>

                                                        <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtImpuestosInternos" runat="server" ReadOnly="true"  CssClass="form-control text-end" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true"  Width="200" Text="0,00"></asp:textbox>
                                                                <label class="text-dark" for="txtImpuestosInternos"> Imp.Internos </label>  
                                                             </div>
                                                        </div>
                                                        <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtPercIIBB" runat="server" ReadOnly="true"  CssClass="form-control text-end" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true"  Width="200" Text="0,00"></asp:textbox>
                                                                <label class="text-dark" for="txtPercIIBB"> Perc.IIBB </label>  
                                                             </div>
                                                        </div>
                                                        <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtPrecIVA" runat="server" ReadOnly="true"  CssClass="form-control text-end" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true"  Width="200" Text="0,00"></asp:textbox>
                                                                <label class="text-dark" for="txtPrecIVA"> Perc.IVA </label>  
                                                             </div>
                                                        </div>
                                                        <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtNoGravado" runat="server" ReadOnly="true"  CssClass="form-control text-end" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true"  Width="200" Text="0,00"></asp:textbox>
                                                                <label class="text-dark" for="txtNoGravado">No gravado</label>  
                                                             </div>
                                                        </div>
                                                         <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:textbox ID="txtDescuento" runat="server" ReadOnly="true"  CssClass="form-control text-end" ForeColor="Green" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true"  Width="200" Text="0,00"></asp:textbox>
                                                                <label class="text-dark text-success" for="txtDescuento">Descuento</label>  
                                                             </div>
                                                        </div>
                                                     <div class="col-md-2">                                                    
                                                             <div class="form-floating mb-2">
                                                                <asp:TextBox runat="server" class="form-control text-end" id="txtImporteFactura" onkeydown="reemplazarPuntoDecimal(event)" onblur="formatearNumero(this)" AutoPostBack="true"  Text="0,00" Width="200"></asp:TextBox>
                                                                <label class="text-dark" for="txtImporteFactura">Total</label>  
                                                             </div>
                                                        </div>
                                                    <div class="col-md-2">                                                    
                                                          <div class="form-floating mb-3">
                                                                <asp:Label ID="lblTotal" runat="server" CssClass="form-control text-end bg-dark text-white" Font-Bold="true" Font-Size="Large" Width="200" Text="0,00"></asp:Label>
                                                                <label class="text-white" for="lblTotal"> TOTAL </label>  
                                                          </div>
                                                    </div>
                            </div>   

                                         

<%--                                    </div>--%>
                                </div>
                                                          

                                                     </asp:panel> 
                    </div>
                </div>
                                <div class="card-footer">
                                     <footer class="py-4 bg-light mt-auto">
                                        <div class="container-fluid px-4">
                                            <div class="d-flex align-items-end justify-content-between small">
                                                <asp:button runat="server" id="btnVolver" cssclass="btn btn-success" Text="Cancelar" PostBackUrl="~/compras_lista.aspx?idproducto=0&Titulo=''&Accion=''" />
                                                <asp:button runat="server" Id="BtnGuardar" cssclass="btn btn-primary" Text="Aceptar " />
                                            </div>
                                        </div>
                                    </footer>         
                              </div>     
         </div>
                          </div>
                        </div>
                    </div>
             </div>
        </div>
                                                 
                                       
            
<div class="modal fade" id="MdlAtencion" tabindex="-1" role="dialog" aria-labelledby="myMdlAtencion" data-bs-backdrop="static" style="display:none">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">ATENCION</h5><button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body bg-warning">
            <asp:Label runat="server" id="lblMensajeAtencion" CssClass="form-label"></asp:Label>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-success" data-bs-dismiss="modal">Cerrar</button>

      </div>
    </div>
  
  </div>
</div>   
 <div class="modal fade" id="MdlAceptarGuardar" tabindex="-1" role="dialog" aria-labelledby="myMdlAceptarGuardar" data-bs-backdrop="static" style="display:none">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header bg-dark text-white">
        <h5 class="modal-title">ATENCION</h5><button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
            <asp:Label runat="server" id="lblMEnsajeCompra" CssClass="form-label"></asp:Label>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-success" data-bs-dismiss="modal">Cerrar</button>
        <asp:Button runat="server" ID="BtnGrabarCompra" CssClass="btn btn-primary" data-bs-dismiss="modal" Text="Guardar" />
      </div>
    </div>
  
  </div>
</div>   
 
      <script>	
          function showModalAtencion() {
              $("#MdlAtencion").modal("show");
          }
          function showModalGuardar() {
              $("#MdlAceptarGuardar").modal("show");
          }
          document.getElementById('<%= txtNetoGravado.ClientID %>').setAttribute('autocomplete', 'off');
      </script>

    <script type="text/javascript">

        function formatDecimal(textbox) {
            // Obtener el valor del TextBox
            var valor = textbox.value;
            //formatDecimal
            // Verificar si es un número
            if (!isNaN(valor) && valor !== "") {
                // Convertir a float y formatear con dos decimales
                var numeroDecimal = parseFloat(valor).toFixed(2);

                // Formatear el número con separador de miles
                numeroDecimal = numeroDecimal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

                // Asignar el valor formateado al TextBox
                textbox.value = numeroDecimal;
            }
        }
        


             function formatearConCeros5(textbox) {
                    // Obtener el valor actual
                    var valor = textbox.value;

                    // Verificar si es un número
                    if (!isNaN(valor) && valor !== "") {
                        // Convertir a número entero y agregar ceros a la izquierda (5 dígitos en este ejemplo)
                        textbox.value = ("00000" + valor).slice(-5); // Ajusta el número de ceros según tu necesidad
                    }
             }


      function formatearConCeros8(textbox) {
            // Obtener el valor actual
            var valor = textbox.value;

            // Verificar si es un número
            if (!isNaN(valor) && valor !== "") {
                // Convertir a número entero y agregar ceros a la izquierda (5 dígitos en este ejemplo)
                textbox.value = ("00000000" + valor).slice(-8); // Ajusta el número de ceros según tu necesidad
            }
      }

              // Interceptar el punto del teclado numérico y reemplazarlo por una coma
              function reemplazarPuntoDecimal(event) {
                  if (event.key === '.' || event.key === 'Decimal') {
                      // Evitar que el punto sea insertado
                      event.preventDefault();

                      // Insertar una coma en la posición del cursor
                      var textbox = event.target;
                      var start = textbox.selectionStart;
                      var end = textbox.selectionEnd;
                      var valor = textbox.value;

                      textbox.value = valor.substring(0, start) + ',' + valor.substring(end);

                      // Colocar el cursor después de la coma
                      textbox.setSelectionRange(start + 1, start + 1);
                  }
              }

              // Formatear el número cuando el TextBox pierde el foco
              function formatearNumero(textbox) {
                  var value = textbox.value.trim();

                  // Verificar si el valor no está vacío
                  if (value !== "") {
                      // Convertir el valor a un número flotante reemplazando la coma por punto
                      var number = parseFloat(value.replace(/\./g, '').replace(',', '.'));

                      // Verificar si es un número válido
                      if (!isNaN(number)) {
                          // Formatear el número con separador de miles y decimales con coma
                          textbox.value = number.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                      } else {
                          // Limpiar el valor si no es un número válido
                          textbox.value = "";
                      }
                  }
              }
</script>
</asp:Content>
