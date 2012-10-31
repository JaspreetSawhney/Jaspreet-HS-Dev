<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true"
    CodeBehind="Invite.aspx.cs" Inherits="HairSlayer.Invite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
    <div id="main">
        <div id="breadcrumb"> <br />Home > My Profile</div>
        <div id="menu-tabs">
                    <asp:Button ID="Provider" runat="server" CssClass="menu2-tab-active" 
                        Text="Invite Barber/Stylist" />
                    <asp:Button ID="Friends" CssClass="menu-tab2" runat="server" 
                        Text="Invite Friends" />
        </div>
        <asp:Panel ID="pnlProvider" runat="server">
        <h1 id="invite-heading">Invite Your Barber/Stylist</h1>
        <asp:TextBox ID="txtProviderName" Text="Barber/Stylist Name" CssClass="text-field" runat="server"></asp:TextBox> <br />
        <asp:TextBox ID="txtProviderEmail" Text="Barber/Stylist Email" CssClass="text-field" runat="server"></asp:TextBox><br />
        </asp:Panel>
        <asp:Panel ID="pnlFriends" Visible="false" runat="server">
        <h2>Invite Your Friends (Optional)</h2>
        </asp:Panel>
        <div id="nav-buttons">
            <div style="float: left; width: 50%;">
                <asp:Button ID="btnBack" Visible="false" runat="server" Text="< Back" CssClass="back-button" onclick="btnBack_Click" />
                &nbsp; <asp:Button ID="Skip" runat="server" Text="Skip" CssClass="next-button" 
                    onclick="Skip_Click" />
            </div>
            <div style="float: right; width: 50%;">
                <asp:Button ID="btnNext" runat="server" Text="Done" CssClass="next-button" onclick="btnNext_Click" />
            </div>
        </div>

        <div id="sidebar">
        &nbsp;
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>
