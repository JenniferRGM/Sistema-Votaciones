<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Resultados.aspx.cs" Inherits="Sistema_Votaciones.Resultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Resultados</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <h2>Resultados de las Elecciones</h2>
        <h3>El ganador es: <span id="lblGanador" runat="server"></span></h3>
    </div>
    <div class="table-container">
        <asp:GridView ID="gvResultados" runat="server" CssClass="tabla-resultados" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Cedula" HeaderText="Cédula" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Apellido1" HeaderText="Apellido1" />
                <asp:BoundField DataField="Apellido2" HeaderText="Apellido2" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="Partido" HeaderText="Partido" />
                <asp:BoundField DataField="CantidadVotos" HeaderText="Cantidad de Votos" />
                <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje (%)" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>


