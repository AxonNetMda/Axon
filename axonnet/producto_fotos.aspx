<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Administracion.Master" CodeBehind="producto_fotos.aspx.vb" Inherits="axonnet.producto_fotos" %>
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
    <div class="container"> 
        <div class="card shadow-lg border-0 rounded-lg mt-10 ">
            <div class="card-header bg-warning text-black">
                <h4 class="text-center font-weight-light my-4"><strong><asp:Label runat="server" ID="lblTitulo" Text="FOTOS" ></asp:Label></strong></h4>
                <h5 class="text-center font-weight-light my-4"><strong><asp:Label runat="server" ID="lblNombre" Text="FOTOS" ></asp:Label></strong></h5>
            </div>                       
            <div class="card-body justify-content-center"> 
                <div class="row justify-content-center">
                    <div class="col-md-2"> 
                        <div class="card-header bg-black text-white">
                            <h6 class="text-center my-1"><strong>Foto N° 1</strong></h6>                                   
                        </div> 
                        <asp:Image ID="imagen1" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                        <br /><hr />
                        <asp:FileUpload  Type="file" id="fileUpload1" accept="image/*" onchange="mostrarImagen1(event)" runat="server" Width="98" Font-Size="Small" OnClientClick="return validarExtension(this);"/>
                        <asp:Button runat="server" ID="btnDelImg1" CssClass="btn btn-secondary btn-sm " Text="Eliminar"  />
                        <asp:HiddenField ID="hNombreImagen1" runat="server" Value="sinfoto.png" />
                        <asp:HiddenField ID="hRutaimagen1" runat="server" Value="productos" />   
                        <asp:Label ID="lblimagen1" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                        <br />
                    </div>
                    <div class="col-md-2">    
                          <div class="card-header bg-black text-white">
                            <h6 class="text-center my-1"><strong>Foto N° 2</strong></h6>                                   
                        </div> 
               
                        <asp:Image ID="imagen2" CssClass="img-thumbnail" runat="server" ImageUrl="<%=CarpetaImagen %>" Width="200px" />
                        <br /><hr />
                        <asp:FileUpload  Type="file" id="fileUpload2" accept="image/*" onchange="mostrarImagen2(event)" runat="server" Width="98" Font-Size="Small"/>
                        <asp:Button runat="server" ID="btnDelImg2" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                        <asp:HiddenField ID="hNombreImagen2" runat="server" Value="sinfoto.png" />
                        <asp:HiddenField ID="hRutaimagen2" runat="server" Value="productos" />   
                        <asp:Label ID="lblimagen2" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                    </div>
                    <div class="col-md-2">                  
                         <div class="card-header bg-black text-white">
                            <h6 class="text-center my-1"><strong>Foto N° 3</strong></h6>                                   
                        </div> 

                        <asp:Image ID="imagen3" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                        <br /><hr />
                        <asp:FileUpload  Type="file" id="fileUpload3" accept="image/*" onchange="mostrarImagen3(event)" runat="server" Width="98" Font-Size="Small"/>
                        <asp:Button runat="server" ID="btnDelImg3" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                        <asp:HiddenField ID="hNombreImagen3" runat="server" Value="sinfoto.png" />
                        <asp:HiddenField ID="hRutaimagen3" runat="server" Value="productos" />   
                        <asp:Label ID="lblimagen3" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                    </div>
                    <div class="col-md-2">                  
                         <div class="card-header bg-black text-white">
                            <h6 class="text-center my-1"><strong>Foto N° 4</strong></h6>                                   
                        </div> 

                        <asp:Image ID="imagen4" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                         <br /><hr />
                        <asp:FileUpload  Type="file" id="fileUpload4" accept="image/*" onchange="mostrarImagen4(event)" runat="server" Width="98" Font-Size="Small"/>
                        <asp:Button runat="server" ID="btnDelImg4" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                        <asp:HiddenField ID="hNombreImagen4" runat="server" Value="sinfoto.png" />
                        <asp:HiddenField ID="hRutaimagen4" runat="server" Value="productos" />   
                        <asp:Label ID="lblimagen4" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                    </div>
                    <div class="col-md-2">                  
                         <div class="card-header bg-black text-white">
                            <h6 class="text-center my-1"><strong>Foto N° 5</strong></h6>                                   
                        </div> 

                        <asp:Image ID="imagen5" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                         <br /><hr />
                        <asp:FileUpload  Type="file" id="fileUpload5" accept="image/*" onchange="mostrarImagen5(event)" runat="server" Width="98" Font-Size="Small"/>
                        <asp:Button runat="server" ID="btnDelImg5" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                        <asp:HiddenField ID="hNombreImagen5" runat="server" Value="sinfoto.png" />
                        <asp:HiddenField ID="hRutaimagen5" runat="server" Value="productos" />   
                        <asp:Label ID="lblimagen5" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                    </div>
                    <div class="col-md-2"> 
                        <div class="card-header bg-black text-white">
                            <h6 class="text-center my-1"><strong>Foto N° 6</strong></h6>                                   
                        </div> 

                        <asp:Image ID="imagen6" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                         <br /><hr />
                        <asp:FileUpload  Type="file" id="fileUpload6" accept="image/*" onchange="mostrarImagen6(event)" runat="server" Width="98" Font-Size="Small"/>
                         <asp:Button runat="server" ID="btnDelImg6" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                       <asp:HiddenField ID="hNombreImagen6" runat="server" Value="sinfoto.png" />
                        <asp:HiddenField ID="hRutaimagen6" runat="server" Value="productos" />   
                        <asp:Label ID="lblimagen6" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                    </div>
                    <div class="col-md-2">                  
                         <div class="card-header bg-black text-white">
                            <h6 class="text-center my-1"><strong>Foto N° 7</strong></h6>                                   
                        </div> 

                        <asp:Image ID="imagen7" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                         <br /><hr />
                        <asp:FileUpload  Type="file" id="fileUpload7" accept="image/*" onchange="mostrarImagen7(event)" runat="server" Width="98" Font-Size="Small"/>
                        <asp:Button runat="server" ID="btnDelImg7" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                       <asp:HiddenField ID="hNombreImagen7" runat="server" Value="sinfoto.png" />
                        <asp:HiddenField ID="hRutaimagen7" runat="server" Value="productos" />   
                        <asp:Label ID="lblimagen7" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                    </div>
                    <div class="col-md-2">                  
                        <div class="card-header bg-black text-white">
                            <h6 class="text-center my-1"><strong>Foto N° 8</strong></h6>                                   
                        </div> 

                        <asp:Image ID="imagen8" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                         <br /><hr />
                        <asp:FileUpload  Type="file" id="fileUpload8" accept="image/*" onchange="mostrarImagen8(event)" runat="server" Width="98" Font-Size="Small"/>
                        <asp:Button runat="server" ID="btnDelImg8" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                        <asp:HiddenField ID="hNombreImagen8" runat="server" Value="sinfoto.png" />
                        <asp:HiddenField ID="hRutaimagen8" runat="server" Value="productos" />   
                        <asp:Label ID="lblimagen8" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                    </div>
                    <div class="col-md-2">                  
                             <div class="card-header bg-black text-white">
                                <h6 class="text-center my-1"><strong>Foto N° 9</strong></h6>                                   
                            </div> 

                          <asp:Image ID="imagen9" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                             <br /><hr />
                            <asp:FileUpload  Type="file" id="fileUpload9" accept="image/*" onchange="mostrarImagen9(event)" runat="server" Width="98" Font-Size="Small"/>
                            <asp:Button runat="server" ID="btnDelImg9" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                            <asp:HiddenField ID="hNombreImagen9" runat="server" Value="sinfoto.png" />
                            <asp:HiddenField ID="hRutaimagen9" runat="server" Value="productos" />   
                            <asp:Label ID="lblimagen9" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                        </div>
                    <div class="col-md-2">                  
                            <div class="card-header bg-black text-white">
                                <h6 class="text-center my-1"><strong>Foto N° 10</strong></h6>                                   
                            </div> 

                            <asp:Image ID="imagen10" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                             <br /><hr />
                            <asp:FileUpload  Type="file" id="fileUpload10" accept="image/*" onchange="mostrarImagen10(event)" runat="server" Width="98" Font-Size="Small"/>
                              <asp:Button runat="server" ID="btnDelImg10" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                          <asp:HiddenField ID="hNombreImagen10" runat="server" Value="sinfoto.png" />
                            <asp:HiddenField ID="hRutaimagen10" runat="server" Value="productos" />   
                            <asp:Label ID="lblimagen10" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                        </div>
                    <div class="col-md-2">                  
                            <div class="card-header bg-black text-white">
                                <h6 class="text-center my-1"><strong>Foto N° 11</strong></h6>                                   
                            </div> 

                            <asp:Image ID="imagen11" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                             <br /><hr />
                            <asp:FileUpload  Type="file" id="fileUpload11" accept="image/*" onchange="mostrarImagen11(event)" runat="server" Width="98" Font-Size="Small"/>
                             <asp:Button runat="server" ID="btnDelImg11" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                           <asp:HiddenField ID="hNombreImagen11" runat="server" Value="sinfoto.png" />
                            <asp:HiddenField ID="hRutaimagen11" runat="server" Value="productos" />   
                            <asp:Label ID="lblimagen11" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                        </div>
                    <div class="col-md-2">                  
                            <div class="card-header bg-black text-white">
                                <h6 class="text-center my-1"><strong>Foto N° 12</strong></h6>                                   
                            </div> 

                            <asp:Image ID="imagen12" CssClass="img-thumbnail" runat="server" ImageUrl="productos/sinfoto.png" Width="200px" />
                             <br /><hr />
                            <asp:FileUpload  Type="file" id="fileUpload12" accept="image/*" onchange="mostrarImagen12(event)" runat="server" Width="98" Font-Size="Small"/>
                            <asp:Button runat="server" ID="btnDelImg12" CssClass="btn btn-secondary btn-sm " Text="Eliminar" />
                           <asp:HiddenField ID="hNombreImagen12" runat="server" Value="sinfoto.png" />
                            <asp:HiddenField ID="hRutaimagen12" runat="server" Value="productos" />   
                            <asp:Label ID="lblimagen12" runat="server" Font-Size="Small" Text="sinfoto.png" visible="true" CssClass="text-white"/>
                        </div>
                </div>
            </div>
            <div class="card-footer justify-content-center">
                 <footer class="py-4 bg-light mt-auto">
                        <div class="container-fluid px-4">
                            <div class="d-flex align-items-end justify-content-between small">
                                <asp:button runat="server" id="BtnCancelar" cssclass="btn btn-success" Text="Cancelar" PostBackUrl="producto_lista.aspx?idproducto=0&Titulo=''&Accion=''" />
                                <asp:button runat="server" Id="BtnGuardar" cssclass="btn btn-primary" Text="Aceptar " onclick="BtnGuardar_Click" />
                            </div>
                        </div>
                    </footer>
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
    function validarExtension(sender) {
        var fileInput = sender;
        var filePath = fileInput.value;
        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif|\.bmp)$/i;

        if (!allowedExtensions.exec(filePath)) {
            alert('Por favor seleccione un archivo de imagen válido (JPG, JPEG, PNG, GIF, BMP).');
            fileInput.value = '';
            return false;
        }
        return true;
    }

    function mostrarImagen1(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen1.ClientID %>').src = e.target.result;
                };
                reader.readAsDataURL(file);

                // Guardar el nombre del archivo en el TextBox oculto
                document.getElementById('<%= lblimagen1.ClientID %>').value = file.name;

                // Guardar el nombre del archivo en el TextBox oculto
              document.getElementById('<%= hNombreImagen1.ClientID %>').value = file.name;

          }
    }
    function borrarImagen1(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen1.ClientID %>').src = 'imagenessitio/imgSinImagen.png';
            };
            reader.readAsDataURL(file);

            // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= lblimagen1.ClientID %>').value = 'imgSinImagen.png';

                // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= hNombreImagen1.ClientID %>').value = 'imgSinImagen.png';

        }
    }

    function mostrarImagen2(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen2.ClientID %>').src = e.target.result;
            };
            reader.readAsDataURL(file);

            // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= lblimagen2.ClientID %>').value = file.name;

                // Guardar el nombre del archivo en el TextBox oculto
             document.getElementById('<%= hNombreImagen2.ClientID %>').value = file.name;

         }
     }

	function mostrarImagen3(event) {
			const file = event.target.files[0];
			if (file) {
				const reader = new FileReader();
				reader.onload = function (e) {
					document.getElementById('<%= imagen3.ClientID %>').src = e.target.result;
				 };
                reader.readAsDataURL(file);

                // Guardar el nombre del archivo en el TextBox oculto
                document.getElementById('<%= lblimagen3.ClientID %>').value = file.name;

                // Guardar el nombre del archivo en el TextBox oculto
                document.getElementById('<%= hNombreImagen3.ClientID %>').value = file.name;
			 }
                }

      function mostrarImagen4(event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('<%= imagen4.ClientID %>').src = e.target.result;
			};
            reader.readAsDataURL(file);

            // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= lblimagen4.ClientID %>').value = file.name;

            // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= hNombreImagen4.ClientID %>').value = file.name;
            }
    }

     function mostrarImagen5(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen5.ClientID %>').src = e.target.result;
			};
        reader.readAsDataURL(file);

        // Guardar el nombre del archivo en el TextBox oculto
        document.getElementById('<%= lblimagen5.ClientID %>').value = file.name;
        // Guardar el nombre del archivo en el TextBox oculto
        document.getElementById('<%= hNombreImagen5.ClientID %>').value = file.name;

        }
    }

 function mostrarImagen6(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen6.ClientID %>').src = e.target.result;
			};
        reader.readAsDataURL(file);

        // Guardar el nombre del archivo en el TextBox oculto
          document.getElementById('<%= lblimagen6.ClientID %>').value = file.name;
        // Guardar el nombre del archivo en el TextBox oculto
        document.getElementById('<%= hNombreImagen6.ClientID %>').value = file.name;

        }
    }

       function mostrarImagen7(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen7.ClientID %>').src = e.target.result;
			};
        reader.readAsDataURL(file);

        // Guardar el nombre del archivo en el TextBox oculto
              document.getElementById('<%= lblimagen7.ClientID %>').value = file.name;
       // Guardar el nombre del archivo en el TextBox oculto
        document.getElementById('<%= hNombreImagen7.ClientID %>').value = file.name;

        }
      }

    function mostrarImagen8(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen8.ClientID %>').src = e.target.result;
			};
        reader.readAsDataURL(file);

        // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= lblimagen8.ClientID %>').value = file.name;
        // Guardar el nombre del archivo en el TextBox oculto
        document.getElementById('<%= hNombreImagen8.ClientID %>').value = file.name;

        }
    }

     function mostrarImagen9(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen9.ClientID %>').src = e.target.result;
			};
        reader.readAsDataURL(file);

            // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= lblimagen9.ClientID %>').value = file.name;
        // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= hNombreImagen9.ClientID %>').value = file.name;      }
    }

    function mostrarImagen10(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen10.ClientID %>').src = e.target.result;
            };
            reader.readAsDataURL(file);

            // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= lblimagen10.ClientID %>').value = file.name;
        // Guardar el nombre del archivo en el TextBox oculto
                 document.getElementById('<%= hNombreImagen10.ClientID %>').value = file.name;
             }
    }
    function mostrarImagen11(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen11.ClientID %>').src = e.target.result;
            };
            reader.readAsDataURL(file);

            // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= lblimagen11.ClientID %>').value = file.name;
        // Guardar el nombre del archivo en el TextBox oculto
             document.getElementById('<%= hNombreImagen11.ClientID %>').value = file.name;
         }
     }
    function mostrarImagen12(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagen12.ClientID %>').src = e.target.result;
            };
            reader.readAsDataURL(file);

            // Guardar el nombre del archivo en el TextBox oculto
            document.getElementById('<%= lblimagen12.ClientID %>').value = file.name;
        // Guardar el nombre del archivo en el TextBox oculto
                document.getElementById('<%= hNombreImagen12.ClientID %>').value = file.name;
            }
        }
           function showModalMensaje() {
			   $("#MdlAtencion").modal("show");
             }

</script>
</asp:Content>
