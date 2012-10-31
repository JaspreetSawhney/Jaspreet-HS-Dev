<%@ Page Title="Hairslayer.com - About Hairslayer" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="HairSlayer.About" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="ThumbGrid" Src="~/UserControls/ThumbGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
    <div id="content_wrap">
        <h1 class="feature_title">About Hairslayer</h1>
        <div id="about-body">
        <p>Hair Slayer a Social Recommendation Network exclusively for Hairstylists, Barbers and their Clients.  
        We are located in Houston, TX. </p>
        
        <p>We have a social game attached to our site called "Rate My Do ™".  A social photo contest where Hairstylists, 
        Barbers and their Clients submit a style and our community votes for the Do Of The Day™! Each daily winner is automatically entered in the “Hair Slayer of the month contest”</p>
        
        <p><a href="features.aspx">See the benefits of joining our site today!</a></p>
        </div>
        <div id="member-wrap">
            <div id="clients">
                <h1>Clients</h1>
                <a href="http://youtu.be/wHsshqIEycE" target="_blank"><img src="images/client-vid.png" border="0" /></a>
                <a href="http://youtu.be/wHsshqIEycE" target="_blank">http://youtu.be/wHsshqIEycE</a>
            </div>
            <div id="providers">
                <h1>Professionals</h1>
                <a href="http://youtu.be/QRBSHJoRWdo" target="_blank"><img src="images/provider-vid.png" border="0" /></a>
                <a href="http://youtu.be/QRBSHJoRWdo" target="_blank">http://youtu.be/QRBSHJoRWdo</a>
            </div>
        </div>
    </div>
    <div id="sidebar">
                <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
    </div>
    <div class="clear"></div>

</asp:Content>
