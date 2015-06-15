<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true" CodeBehind="Errorpage.aspx.cs" Inherits="IMDbWebApp.Errorpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1>Error 404: Pagina niet gevonden</h1>
    U kunt het beste terugkeren naar de <a href="Index.aspx">Homepage</a> of naar één van de pagina's in de menubalk.
    <asp:Label ID="lblFout" runat="server" Text=""></asp:Label>
</asp:Content>
