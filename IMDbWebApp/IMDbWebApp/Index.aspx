<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="IMDbWebApp.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:Label ID="lblWelcome" runat="server" Text="Welkom"></asp:Label>

    <h1>Films met de beste waardering</h1>

    <div id="favMovieContent" clientidmode="static" runat="server">
    </div>
</asp:Content>
