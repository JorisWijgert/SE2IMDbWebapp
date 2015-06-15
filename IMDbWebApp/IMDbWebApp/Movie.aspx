<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true" CodeBehind="Movie.aspx.cs" Inherits="IMDbWebApp.Movie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:Panel ID="pnlNotLoggedIn" runat="server" Visible="false">
        Voor dit gedeelte is een account vereist!
        Klik <a href="LogIn.aspx">hier</a> om in te loggen.
        Nog geen account? Klik dan <a href="Registrate.aspx">hier</a> om u gratis te registreren.
    </asp:Panel>
    <asp:Panel ID="pnlMovieInfo" runat="server" Visible="false">
        <asp:Label ID="lblTitle" runat="server" Text="Film"></asp:Label>
        <asp:Image ID="imgCover" runat="server" />
        Lengte:
        <asp:Label ID="lblLength" runat="server" Text="Lengte"></asp:Label>
        Releasedatum:
        <asp:Label ID="lblRlsDate" runat="server" Text="Releasedatum"></asp:Label>
        Gemiddeld cijfer:
        <asp:Label ID="lblGrade" runat="server" Text="Cijfer"></asp:Label>
        Samenvatting:
        <asp:Label ID="lblSummary" runat="server" Text="Samenvatting"></asp:Label>
        Genre:
        <asp:Label ID="lblGenre" runat="server" Text="Genre"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlUserActions" runat="server" Visible="false">
        <asp:Button ID="btnListAddRmv" runat="server" Text="Toevoegen aan lijst" OnClick="btnListAddRmv_Click" />
        Geef een cijfer aan deze film:
        <asp:RadioButtonList ID="rblGrade" runat="server" >
            <asp:ListItem Text="1" Value="1" />
            <asp:ListItem Text="2" Value="2" />
            <asp:ListItem Text="3" Value="3" />
            <asp:ListItem Text="4" Value="4" />
            <asp:ListItem Text="5" Value="5" />
            <asp:ListItem Text="6" Value="6" />
            <asp:ListItem Text="7" Value="7" />
            <asp:ListItem Text="8" Value="8" />
            <asp:ListItem Text="9" Value="9" />
            <asp:ListItem Text="10" Value="10" />
        </asp:RadioButtonList>
        <asp:Button ID="btnCommit" runat="server" Text="Bevestig" OnClick="btnCommit_Click" />
        <asp:Label ID="lblSelectAValue" runat="server" Text="Selecteer eerst een cijfer" Visible="false"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlSequel" runat="server" Visible="false">
        Vervolgfilm:
        <!-- hier komt de user control MovieViewer -->
    </asp:Panel>
    <asp:Panel ID="pnlCastActor" runat="server" Visible="false">
        De cast en acteurs
        <!-- hier komt de user control CastViewer -->
    </asp:Panel>
</asp:Content>
