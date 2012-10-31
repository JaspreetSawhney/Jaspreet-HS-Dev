<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Up.aspx.cs" Inherits="HairSlayer.Up" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="stylesheets/reset.css" />
    <style>
        body  
        {
            font-family: Arial;
            line-height: 28px;
            }
            
        h2
        {
            font-weight: bold;
            }            
        .site-button 
        {
            padding: 10px 10px 10px 10px;
    -moz-border-radius: 8px;
    border-radius: 8px;
    height: 32px;
    width: 130px;
    color: #FFFFFF;
    background-color: #999999;
    font-size: 14px;
    font-weight: bold;
    border-width: 0px;
}

.text-field 
{
    width: 396px;
    height: 44px;
    font-size: 24px;
    padding-left: 5px;
    margin: 0px 5px 12px 0px;
    -moz-border-radius: 5px;
    border-radius: 5px;
    }
    
    a.feature-link-button
    {
    background-color: #999999; 
    text-decoration: none;
    color: #FFFFFF; 
    border-radius: 5px; 
    padding: 5px 10px 5px 10px; 
    font-weight: bold;
    margin-left: auto;
    margin-right: auto;
    margin-top: 40px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">   
     <p>
        <h2><asp:Label ID="doInfo" runat="server" Text="Before you upload your pic, please tell us a little more about your do."></asp:Label></h2>
     </p>
        <asp:Label ID="Error" ForeColor="#FF0000" Visible="false" runat="server" Text=""></asp:Label><br />
     <asp:Panel ID="NoUploads" Visible="false" runat="server">
        <center><h2 id="nouploads-h2">Only 3 styles can be uploaded with a free account. </h2>        
        <h2 id="nouploads-h2">To enjoy unlimited styles please upgrade your plan.</h2>
        <br />
        <a href="Feature.aspx" target="_parent" class="feature-link-button">Upgrade Now</a></center>
     </asp:Panel>
     <asp:Panel ID="pnlUpload" runat="server">   
        <table cellspacing="4" width="700">
            <tr>
                <td colspan="2">What is the name of your do?<br />
                <asp:TextBox ID="DoName" CssClass="text-field" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblDoServices" runat="server" Text="Description: "></asp:Label></td>
                <td>
                    <asp:CheckBoxList RepeatColumns="2" ID="rbtServices" runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td colspan="2">Please select the service associated with this style.<br />
                    <asp:DropDownList ID="Service_Style" CssClass="text-field" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br /><br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="btnUpload" runat="server" CssClass="site-button" 
                        OnClick="btnUpload_Click" Text="Upload" />
     </asp:Panel>
     <asp:Panel ID="pnlCrop" Visible="false" runat="server">
        <asp:Image ID="Image1" runat="server" />
        <asp:Image ID="Image2" ImageUrl="images/spacer.gif" runat="server" />
        <cs:CropImage ID="wci1" runat="server" Image="Image1" Ratio="1/1" X="0" Y="0" X2="100" Y2="100" CropButtonID="btnCrop" />
        <br />
        <asp:HyperLink ID="UploadAnotherStyle" Visible="false" CssClass="site-button" NavigateUrl="~/Up.aspx" runat="server">Upload Another Style</asp:HyperLink>
         <asp:HyperLink ID="ViewProfile" Visible="false" CssClass="site-button" Target="_parent" runat="server">View Profile</asp:HyperLink>
        <asp:Button ID="btnCrop" runat="server" OnClick="btnCrop_Click" CssClass="site-button" Text="Crop Image" />
        <asp:Button ID="btnGoBack" runat="server" Visible="false" CssClass="site-button" 
             style="margin-right: 40px;" Text="< Go Back" onclick="btnGoBack_Click" />
        <asp:Button ID="btnSave" runat="server" Visible="false" CssClass="site-button" 
             Text="Save Image" onclick="btnSave_Click" />
     </asp:Panel>
     <asp:HiddenField ID="tmp_fle" runat="server" />
     <asp:HiddenField ID="fle_id" runat="server" />
</div>
    </form>
    <script type="text/javascript">
        var _kmq = _kmq || [];
        function _kms(u) {
            setTimeout(function () {
                var s = document.createElement('script'); var f = document.getElementsByTagName('script')[0]; s.type = 'text/javascript'; s.async = true;
                s.src = u; f.parentNode.insertBefore(s, f);
            }, 1);
        }
        _kms('//i.kissmetrics.com/i.js'); _kms('//doug1izaerwt3.cloudfront.net/f2d10930ca3b5d9e0df142b6be03660643228f2b.1.js');
    </script>
</body>
</html>
