<%@ Page Title="Home Page" Language="VB" Debug="true" MasterPageFile="Administracion.Master" AutoEventWireup="true" CodeBehind="DefaultAdmin.aspx.vb" Inherits="axonnet.Default2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="formmaster" runat="server">
     <%If Session("sNombreUsuario") = "" Then
             Response.Redirect("~/login.aspx?idUsuario=0")
         Else
             If Request.QueryString("sNombreUsuario") = "DEMO" Then
                 Session("email") = "demo@axonnet.store"
                 Session("rolnombre") = "DEMO"
                 Session("rol") = 6
                 Session("sNombreUsuario") = "DEMO"
                 Session("Idusuario") = 15
                 Session("sNombreEmpresa") = "DEMO"
             Else

             End If



         End If %>
  
     <div class="container-fluid px-4">
                        <h5 class="mt-4 justify-content-center">Bienvenidos a la Administracion - <%= Date.Today.ToLongDateString %></h5>
                        <hr />
                       <%-- <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item active">Panel de  principal</li>
                        </ol>--%>
         <div class="container-fluid px-4">
                           <div class="row">
                               <% 
                                   Dim Dolar As Decimal = 1230
                                   Dim Euro As Decimal = 1463.36
                                   Dim Real As Decimal = 185.5

                                   %>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-default text-black mb-4">
                                    <div class="card-header text-black mb-4">Cotizaciones</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4 align-items-center">
                                                <div class="sb-nav-link-icon">
                                                    <p>DOLAR <img src="img/flags/us.png"  class="img-thumbnail" width="50"/></p>                                                    
                                                    <p><%= FormatCurrency(Dolar, 2) %></p>
                                                </div>
                                             </div>
                                            <div class="col-md-4">
                                                <div class="sb-nav-link-icon">
                                                    <p>EURO</p>
                                                    <img src="img/flags/_European Union.png"  class="img-thumbnail" width="50"/>
                                                    <p><%= formatcurrency(Euro, 2) %></p>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                            <div class="sb-nav-link-icon">
                                                 <p>REAL</p>
                                                <img src="img/flags/br.png"  class="img-thumbnail" width="50"/>
                                            <p><%= FormatCurrency(Real, 2) %></p>
                                            </div>
                                                </div>

                                       
                                       <%-- <div class="col-md-6">
                                            
                                        </div>--%>
                                        </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">                                                                              
                                        <label class="small text-black stretched-link" >Monedas</label>
                                        <div class="small text-black"><%--<i class="fas fa-angle-right"></i>--%></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-warning text-black mb-4">
                                    <div class="card-header  bg-dark text-white mb-4">TOTAL VENTAS</div>
                                    <div class="card-body">                                        
                                       <div class="row">
                                        <div class="col-md-6">
                                            <div class="sb-nav-link-icon">
                                                <img src="Imagenes/DetalledeVentas.png"  class="img-thumbnail" width="100"/>
                                            </div>
                                        </div>
                                        <div class="col-md-6  text-black">
                                            <p>VENTAS</p>
                                            <p>$ 0,00</p>
                                            <p></p>
                                        </div>
                                        </div>                                    

                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">                                                                               
                                        <a class="small text-black stretched-link" href="#">Ver info</a>
                                        <div class="small text-black"><i class="fas fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>                        
                           </div>

        </div>

     </div>


</asp:Content>
