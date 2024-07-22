<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Votaciones.aspx.cs" Inherits="Sistema_Votaciones.Votaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Registrar Votaciones</title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/inputmask/5.0.6/inputmask.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            var selector = $("#<%= txtFechaNacimiento.ClientID %>");
            Inputmask("9999-99-99", { alias: "yyyy-mm-dd", placeholder: "YYYY-MM-DD" }).mask(selector);
            Inputmask("99-99-9999", { alias: "dd-mm-yyyy", placeholder: "DD-MM-YYYY" }).mask(selector);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="container">
        <h2 class="text-center">Registrar Votaciones</h2>
        <div class="form-container">
            <div class="form-group">
                <label for="IDVotante">ID Votante:</label>
                <asp:TextBox ID="txtIDVotante" runat="server" CssClass="form-control" placeholder="Ingrese el ID Votante"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="Nombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el Nombre"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="Apellido1">Apellido1:</label>
                <asp:TextBox ID="txtApellido1" runat="server" CssClass="form-control" placeholder="Ingrese el Primer Apellido"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="Apellido2">Apellido2:</label>
                <asp:TextBox ID="txtApellido2" runat="server" CssClass="form-control" placeholder="Ingrese el Segundo Apellido"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="FechaNacimiento">Fecha de Nacimiento:</label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD or DD-MM-YYYY"></asp:TextBox>
                <asp:CustomValidator ID="cvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento" ErrorMessage="Fecha inválida. Use el formato YYYY-MM-DD o DD-MM-YYYY." OnServerValidate="cvFechaNacimiento_ServerValidate"></asp:CustomValidator>
            </div>
            <div class="form-group">
                <label for="Email">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingrese el Email"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="Direccion">Dirección:</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ingrese la Dirección"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="Candidato">Candidato:</label>
                <asp:DropDownList ID="ddlCandidatos" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group text-center">
                <asp:Button ID="btnRegistrarVoto" runat="server" CssClass="btn" Text="Registrar Voto" OnClick="btnRegistrarVoto_Click" />
            </div>
        </div>
    </div>
</asp:Content>

