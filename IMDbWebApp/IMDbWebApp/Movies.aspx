<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Movies.aspx.cs" Inherits="IMDbWebApp.Movies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Films</title>
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
    <div id="dbconnection">
        <asp:SqlDataSource 
    ID="SqlDataSource1" Runat="server" 
    SelectCommand="select * from film"
    ConnectionString="<%$ ConnectionStrings:OracleConnectionString%>" />
    </div>
    </form>
</body>
</html>
