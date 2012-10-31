<%@ Page Title="HairSlayer.com - Upload My Do" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="HairSlayer.Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>

<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/style.css" />
    <script language="javascript" src="js/upload_do.js"></script>
    <script>
function uploadError(sender, args) {
    document.getElementById('lblStatus').innerText = args.get_fileName(),
	"<span style='color:red;'>" + args.get_errorMessage() + "</span>";
}

function StartUpload(sender, args) {
    document.getElementById('lblStatus').innerText = 'Uploading Started.';
}

function UploadComplete(sender, args) {
    var filename = args.get_fileName();
    var contentType = args.get_contentType();
    var text = "Size of " + filename + " is " + args.get_length() + " bytes";
    if (contentType.length > 0) {
        text += " and content type is '" + contentType + "'.";
    }
    document.getElementById('upload-status').innerText = text;
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">

 <div id="main">
    <div id="bread_crumb"><a href="#">Home</a> > <a href="#">Upload My Do</a></div>
    <h2 id="do-upload">Upload My Do</h2>
     <p>
        <asp:Label ID="doInfo" runat="server" Text="Before you upload your pic, please tell us a little more about your do."></asp:Label>
     </p>
        <asp:Label ID="Error" ForeColor="#FF0000" Visible="false" runat="server" Text=""></asp:Label><br />
     <asp:Panel ID="NoUploads" Visible="false" runat="server">
        <h2 id="nouploads-h2">You Have Reached Your Upload Capacity</h2>
        <h2 id="nouploads-h2">Upgrade To Our Premium Account In Order To Experience Unlimited Uploads</h2>
     </asp:Panel>
     <asp:Panel ID="pnlUpload" runat="server">   
        <table width="700">
            <tr>
                <td colspan="2">What is the name of your do?<br />
                <asp:TextBox ID="DoName" CssClass="text-field" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="125">Service Provider: </td>
                <td>
                    <asp:RadioButtonList AutoPostBack="true" RepeatDirection="Horizontal" 
                        ID="rbtProvider" runat="server" 
                        onselectedindexchanged="rbtProvider_SelectedIndexChanged">
                    <asp:ListItem>Barber</asp:ListItem>
                    <asp:ListItem>Stylist</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblDoServices" Visible="false" runat="server" Text="Description: "></asp:Label></td>
                <td>
                    <asp:CheckBoxList RepeatColumns="2" ID="rbtServices" runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
        <br /><br />
        <asp:FileUpload ID="FileUpload1" Visible="false" runat="server" />
                    <asp:Button ID="btnUpload" runat="server" CssClass="site-button" 
                        OnClick="btnUpload_Click" Text="Upload" />
     </asp:Panel>
     <asp:Panel ID="pnlCrop" Visible="false" runat="server">
        <asp:Image ID="Image1" runat="server" />
        <asp:Image ID="Image2" ImageUrl="images/spacer.gif" runat="server" />
        <cs:CropImage ID="wci1" runat="server" Image="Image1" Ratio="1/1" X="0" Y="0" X2="100" Y2="100" CropButtonID="btnCrop" />
        <br />
         <asp:HyperLink ID="ViewProfile" Visible="false" CssClass="site-button" runat="server">View Profile</asp:HyperLink>
        <asp:Button ID="btnCrop" runat="server" OnClick="btnCrop_Click" CssClass="site-button" Text="Crop Image" />
        <asp:Button ID="btnGoBack" runat="server" Visible="false" CssClass="site-button" 
             style="margin-right: 40px;" Text="< Go Back" onclick="btnGoBack_Click" />
        <asp:Button ID="btnSave" runat="server" Visible="false" CssClass="site-button" 
             Text="Save Image" onclick="btnSave_Click" />
     </asp:Panel>
     <asp:HiddenField ID="tmp_fle" runat="server" />
     <asp:HiddenField ID="fle_id" runat="server" />
</div>
<div id="sidebar">
    <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
</div>
</asp:Content>
