<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresarCandidatos.aspx.cs" Inherits="Sistema_Votaciones.IngresarCandidatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Ingresar Candidatos</title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/inputmask/5.0.6/inputmask.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var selector = $("#<%= txtFechaNacimiento.ClientID %>");
            Inputmask("9999-99-99", { alias: "yyyy-mm-dd", placeholder: "YYYY-MM-DD" }).mask(selector);
            Inputmask("99-99-9999", { alias: "dd-mm-yyyy", placeholder: "DD-MM-YYYY" }).mask(selector);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="text-center">Ingresar Candidatos</h2>
        <div class="form-container">
            <div class="form-group">
                <label for="txtCedula">Cédula:</label>
                <asp:TextBox ID="txtCedula" runat="server" CssClass="form-control" placeholder="Ingrese la Cédula"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtNombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el Nombre"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtApellido1">Primer Apellido:</label>
                <asp:TextBox ID="txtApellido1" runat="server" CssClass="form-control" placeholder="Ingrese el Primer Apellido"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtApellido2">Segundo Apellido:</label>
                <asp:TextBox ID="txtApellido2" runat="server" CssClass="form-control" placeholder="Ingrese el Segundo Apellido"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDireccion">Dirección:</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ingrese la Dirección"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtFechaNacimiento">Fecha de Nacimiento:</label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD or DD-MM-YYYY"></asp:TextBox>
                <asp:CustomValidator ID="cvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento"
                    ErrorMessage="Fecha inválida. Use el formato YYYY-MM-DD o DD-MM-YYYY."
                    OnServerValidate="cvFechaNacimiento_ServerValidate"></asp:CustomValidator>
            </div>
            <div class="form-group">
                <label for="ddlPartidos">Partido:</label>
                <asp:DropDownList ID="ddlPartidos" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="text-center">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Ingresar Candidato" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnConsultar" runat="server" CssClass="btn" Text="Consultar Candidatos" OnClick="btnConsultar_Click" />
            </div>
        </div>
        <asp:GridView ID="gvCandidatos" runat="server" AutoGenerateColumns="False" CssClass="table">
            <Columns>
                <asp:BoundField DataField="Cedula" HeaderText="Cédula" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Apellido1" HeaderText="Apellido1" />
                <asp:BoundField DataField="Apellido2" HeaderText="Apellido2" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="Partido" HeaderText="Partido" />
                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Registro" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
