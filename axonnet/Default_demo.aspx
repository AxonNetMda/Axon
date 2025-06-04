<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Demo.Master" AutoEventWireup="true" CodeBehind="Default_demo.aspx.vb" Inherits="axonnet.Default_demo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="formMaster" runat="server">
    <br />
<div class="container ">
        <div class="row">
            <br />
            <div class="col-md-4">
                <asp:ImageButton ID="ImageButton1" runat="server" Height="100px" Width="100px" ImageUrl="~/imagenesaxon/logosf.jpg" />
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblQuees" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-md-4">
                <div>
                   
            <asp:Label runat="server" ID="LabelMobile"  Visible="true"
                Font-Size="xx-Large" Font-Bold="true"
                Text="" />
           
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
           
        </div>
            </div>
        </div>
</div>
</asp:Content>
