<%@ Page Title="Hairslayer.com - Forgot Password" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="HairSlayer.Forgot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="keywords" content="Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle" />
 <style>
     strong 
     {
         font-weight: bold;
         }
         
    fieldset 
    {
        width: 360px;
        border-radius: 10px;
        background-color: #FFFFFF;
        padding: 20px 20px 20px 20px;
        text-align: right;
        margin-left: auto;
        margin-right: auto;
        margin-top: 15px;
        }
        
    input[type=text]#MainBody_ResetEmail {
	height:34px;
	border-radius:8px;
	border-style:solid;
	border-color:#000000;
	border-width:1px;
	outline:none;
	font-size:15px;
    padding:5px;
	font-size:22px;
	color:#999999;
	margin-bottom: 10px;
        }        
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
<div align="center">
<strong>Please enter your username or email address. <br />You will receive details about your login credentials via email.</strong><br />

<fieldset>
    <asp:Label ID="EmailError" runat="server" Font-Size="12px" Visible="false" ForeColor="#FF0000" Text=""></asp:Label>
    Email: <asp:TextBox ID="ResetEmail" Width="80%" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
        onclick="btnSubmit_Click" />
</fieldset>
</div>
</asp:Content>
