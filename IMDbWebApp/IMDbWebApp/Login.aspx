<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IMDbWebApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inloggen | IMDb</title>
</head>
<body>
    <div class="menu">
        <ul>
            <li>Home</li>
            <li>Films</li>
            <li>Series</li>
            <li>Log in</li>
        </ul>
    </div>
    <form id="form1" runat="server">
    <div>
        <h1>Inloggen</h1>
        <asp:Label ID="lblEmail" runat="server" Text="E-mailadres"></asp:Label>
        <asp:TextBox ID="tbEmail" runat="server" type="email"></asp:TextBox>
        <asp:Label ID="lblPassw" runat="server" Text="Wachtwoord"></asp:Label>
        <asp:TextBox ID="tbPassw" runat="server" type="password"></asp:TextBox>
    </div>
    </form>
</body>
</html>
