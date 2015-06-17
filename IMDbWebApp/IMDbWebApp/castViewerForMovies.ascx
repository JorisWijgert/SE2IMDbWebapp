<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="castViewerForMovies.ascx.cs" Inherits="IMDbWebApp.castViewerForMovies" %>

<div class="castContainer">
    <div id="role">
        <asp:Label ID="lblRole" runat="server" Text="Rol"></asp:Label>
    </div>
    <div id="ActorNames">
        Gespeeld door:
    <asp:Label ID="lblFtName" runat="server" Text="Voornaam"></asp:Label>
        <asp:Label ID="lblBtwnName" runat="server" Text="tussenvoegsel"></asp:Label>
        <asp:Label ID="lblSurname" runat="server" Text="Achternaam"></asp:Label>
    </div>
    <div id="ActorData">
        Geboortedatum:
    <asp:Label ID="lblBirthDate" runat="server" Text="Geboortedatum"></asp:Label>
        Geboorteplaats:
    <asp:Label ID="lblBirthPlace" runat="server" Text="Geboorteplaats"></asp:Label>, 
    <asp:Label ID="lblBirthCountry" runat="server" Text="Geboorteland"></asp:Label>
        Biografie:
    <asp:Label ID="lblBio" runat="server" Text="Biografie"></asp:Label>
    </div>
</div>
