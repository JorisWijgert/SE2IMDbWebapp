<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="movieViewerForMovies.ascx.cs" Inherits="IMDbWebApp.movieViewerForMovies" %>

<a class="movContainer" href="#" id="movContainLink" runat="server">
    <div class="imageLeft" >
        <img id="movCImage" runat="server" src="#"/>
    </div>
    <div class="movTitle" runat="server" id="movCTitle">
        Titel
    </div>
    <div class="movAbout" runat="server" id="movCAbout">
        Film over...
    </div>
    <div class="movGrade" runat="server" id="movCGrade">
        8,5
    </div>
</a>