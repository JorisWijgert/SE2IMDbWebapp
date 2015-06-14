<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true" CodeBehind="Movies.aspx.cs" Inherits="IMDbWebApp.Movies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1>Lijst van alle films</h1>
    <div id="movieContent" clientidmode="static" runat="server">

    </div>
</asp:Content>
