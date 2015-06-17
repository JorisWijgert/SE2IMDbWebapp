<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true" CodeBehind="Registrate.aspx.cs" Inherits="IMDbWebApp.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div id="fields" runat="server">
        <p>Alle velden zijn verplicht!</p>
        <asp:Label ID="lblFirstName" AssociatedControlID="tbFirstName" runat="server" Text="Voornaam"></asp:Label>
        <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
        <asp:Label ID="lblLastName" AssociatedControlID="tbLastName" runat="server" Text="Achternaam"></asp:Label>
        <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
        <!-- Bron: http://stackoverflow.com/questions/5145593/two-radio-buttons-asp-net-c-sharp -->
        <asp:RadioButtonList ID="rblGender" runat="server">
            <asp:ListItem Text="Man" Value="M" Selected="True" />
            <asp:ListItem Text="Vrouw" Value="V" />
        </asp:RadioButtonList>
        <asp:Label ID="lblBirthYear" AssociatedControlID="tbBirthYear" runat="server" Text="Geboortejaar" type="number"></asp:Label>
        <asp:TextBox ID="tbBirthYear" runat="server"></asp:TextBox>
        <asp:Label ID="lblCountry" AssociatedControlID="tbCountry" runat="server" Text="Land"></asp:Label>
        <asp:TextBox ID="tbCountry" runat="server"></asp:TextBox>
        <asp:Label ID="lblPostCode" AssociatedControlID="tbPostCode" runat="server" Text="Postcode"></asp:Label>
        <asp:TextBox ID="tbPostCode" runat="server"></asp:TextBox>
        <asp:Label ID="lblEmail" AssociatedControlID="tbEmail" runat="server" Text="E-mailadres" ></asp:Label>
        <asp:TextBox ID="tbEmail" runat="server" type="email"></asp:TextBox>
        <asp:Label ID="lblPassword" AssociatedControlID="tbPassword" runat="server" Text="Wachtwoord"></asp:Label>
        <asp:TextBox ID="tbPassword" type="password" runat="server"></asp:TextBox>
        <asp:Button ID="btnCommit" runat="server" Text="Registreren" OnClick="btnCommit_Click" />
    </div>
    <div id="errorLabels" runat="server">
        <asp:Label ID="lblErrorMessage" runat="server" Text="Registreren niet gelukt"></asp:Label>
    </div>
    <div id="success" runat="server">
            Registreren gelukt! U kunt nu proberen <a href="LogIn.aspx">in te loggen</a>.
        </div>
</asp:Content>
