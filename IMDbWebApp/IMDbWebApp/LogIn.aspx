<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="IMDbWebApp.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div runat="server" id="logOut">
        U bent nu uitgelogd van het account
        <asp:Label ID="lblAccount" runat="server" Text="aap@aap.com"></asp:Label>
        U kunt terugkeren naar de <a href="index.aspx">homepage</a>
    </div>
    <div runat="server" id="logIn">
        <asp:Label ID="lblEmailTxt" runat="server" Text="E-mailadres" AssociatedControlID="tbEmail"></asp:Label>
        <asp:TextBox ID="tbEmail" runat="server" type="email"></asp:TextBox>
        <asp:Label ID="lblPassword" runat="server" Text="Wachtwoord" AssociatedControlID="tbPassword"></asp:Label>
        <asp:TextBox ID="tbPassword" runat="server" type="password"></asp:TextBox>
        <asp:Button ID="btnLogin" runat="server" Text="Inloggen" OnClick="btnLogin_Click" />
        <asp:Label ID="lblResult" runat="server" Text="Resultaat"></asp:Label>
        Nog geen account? U kunt <a href="Registrate.aspx">hier</a> registreren!
    </div>
</asp:Content>
