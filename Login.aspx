<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HairSlayer.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">

<div id="main">
<br /><h2>Member Login</h2>
<div id="lg-field1">
    <asp:Label ID="Error" runat="server" ForeColor="Red" Visible="false" Text=""></asp:Label>
    <strong>Email </strong> <br /><asp:TextBox ID="User" CssClass="text-field" runat="server"></asp:TextBox></div>
<div id="lg-field2"><strong>Password </strong> <br /><asp:TextBox ID="Pass" TextMode="Password" CssClass="text-field" runat="server"></asp:TextBox></div>
<div id="lg-field3"><asp:Button ID="btnLogin" runat="server" CssClass="site-button" 
        Text="Login" onclick="btnLogin_Click" /></div>
</div>
</asp:Content>
