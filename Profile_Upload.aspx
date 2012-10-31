<%@ Page Title="Profile - Upload My Do" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Profile_Upload.aspx.cs" Inherits="HairSlayer.Profile_Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="keywords" content="Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle" />
    <script language="javascript" src="js/prototype.js"></script>
    <script language="javascript" src="js/picup.js"></script>
    <script language="javascript" src="js/functions.js"></script>
    
    <script language="JavaScript">
<!--
        function autoResize(id) {
            var newheight;
            var newwidth;

            if (document.getElementById) {
                newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
                newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
            }

            document.getElementById(id).height = (newheight + 40) + "px";
            document.getElementById(id).width = (newwidth) + "px";
        }
        //-->

        jQuery(document).ready(function () {
            jQuery("#Main_Body_hashvalue").val(window.location.hash);
        });
    </script>

    <script type="text/javascript">
        //$('tmp_fle').val('cover up spot');


        window.name = "my_form";
        //Picup.convertFileInput("MainBody_FileUpload1", null);
        var currentParams = {}

        //document.getElementByName('tmp_fle').value = 'Show';
        //document.getElementById('loc').value = "Testing";
        //$('loc').value = "Testing";

        document.observe('dom:loaded', function () {

            $(document.body).addClassName('iphone');

            // We'll check the hash when the page loads in-case it was opened in a new page
            // due to memory constraints
            Picup.checkHash();

            // Set some starter params	
            currentParams = {
                'callbackURL': 'http://hairslayer.com/upload.htm',
                'referrername': escape('Hair Slayer'),
                'referrerfavicon': escape('http://hairslayer.com/favicon.ico'),
                'purpose': escape('Select your photo to upload.'),
                'debug': 'true',
                'returnThumbnailDataURL': 'false'
            };

            if (ismobilesafari()) {
                Picup.convertFileInput($('MainBody_DoFileUpload'), currentParams);
            }
        });

        function ismobilesafari() {
            return navigator.userAgent.match(/(iPod|iPhone|iPad)/);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
<div id="content_wrap">
        <div id="bread_crumb">
            <a href="index.aspx">Home</a> > <a href="#">Sign Up</a> > <a href="#">Providers</a></div>
        <div id="info_navigation">
            <asp:Button ID="About" runat="server" OnClick="About_Click" CssClass="menu-tab" Text="About" />
            <asp:Button ID="Styles" Visible="false" CssClass="menu-tab" runat="server" 
                Text="My Styles" onclick="Styles_Click" />
            <asp:Button ID="Service" runat="server" OnClick="Service_Click" CssClass="menu-tab" Text="My Services" />
            <asp:Button ID="Upload" runat="server" OnClick="Upload_Click" CssClass="info_active" Text="Upload My Do" />
            <asp:Button ID="Schedule" runat="server" CssClass="menu-tab" Text="My Schedule" OnClick="Schedule_Click" />
        </div>

        <asp:Panel ID="pnlUploadDo" runat="server">
        <p>
        <h2><asp:Label ID="doInfo" runat="server" Text="Before you upload your pic, please tell us a little more about your do."></asp:Label></h2>
            <p>
            </p>
            <asp:Label ID="DoError" runat="server" ForeColor="#FF0000" Text="" 
                Visible="false"></asp:Label>
            <br />
            <asp:Panel ID="NoUploads" runat="server" Visible="false">
                <center>
                    <h2 ID="nouploads-h2">
                        Only 3 styles can be uploaded with a free account.
                    </h2>
                    <h2 ID="nouploads-h2">
                        To enjoy unlimited styles please upgrade your plan.</h2>
                    <br />
                    <a class="feature-link-button" href="Feature.aspx" target="_parent">Upgrade Now</a></center>
            </asp:Panel>
            <asp:Panel ID="pnlDoUpload" runat="server">
                <table cellspacing="4" width="700">
                    <tr>
                        <td colspan="2">
                            What is the name of your do?<br />
                            <asp:TextBox ID="DoName" runat="server" CssClass="text-field"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblDoServices" runat="server" Text="Description: "></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="rbtServices" runat="server" RepeatColumns="2">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Please select the service associated with this style.<br />
                            <asp:DropDownList ID="Service_Style" runat="server" CssClass="text-field">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:FileUpload ID="DoFileUpload" runat="server" />
                <asp:Button ID="btnDoUpload" runat="server" CssClass="site-button" 
                    OnClick="btnDoUpload_Click" Text="Upload" />
            </asp:Panel>
            <asp:Panel ID="pnlDoCrop" runat="server" Visible="false">
                <asp:Image ID="DoImage1" runat="server" />
                <asp:Image ID="DoImage2" runat="server" ImageUrl="images/spacer.gif" />
                <cs:CropImage ID="Dowci1" runat="server" CropButtonID="btnCrop" 
                    Image="DoImage1" Ratio="1/1" X="0" X2="100" Y="0" Y2="100" />
                <br />
                <asp:HyperLink ID="UploadAnotherStyle" runat="server" CssClass="site-button" 
                    NavigateUrl="~/Up.aspx" Visible="false">Upload Another Style</asp:HyperLink>
                <asp:HyperLink ID="ViewDoProfile" runat="server" CssClass="site-button" 
                    Target="_parent" Visible="false">View Profile</asp:HyperLink>
                <asp:Button ID="btnDoCrop" runat="server" CssClass="site-button" 
                    OnClick="btnDoCrop_Click" Text="Crop Image" />
                <asp:Button ID="btnDoGoBack" runat="server" CssClass="site-button" 
                    onclick="btnDoGoBack_Click" style="margin-right: 40px;" Text="&lt; Go Back" 
                    Visible="false" />
                <asp:Button ID="ReUpload" runat="server" CssClass="site-button" 
                    onclick="ReUpload_Click" Text="Upload Photo Again" Visible="false" />
                <asp:Button ID="btnDoSave" runat="server" CssClass="site-button" 
                    onclick="btnDoSave_Click" Text="Save Image" Visible="false" />
            </asp:Panel>
            <asp:TextBox ID="loc" runat="server" BorderColor="#FFFFFF" ForeColor="#000000" 
                Width="170px"></asp:TextBox>
            <asp:HiddenField ID="tmp_fle_do" runat="server" />
            <asp:HiddenField ID="fle_id_do" runat="server" />
        </p>
        </asp:Panel>
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Button ID="btnBack" Visible="false" runat="server" Text="< Back" CssClass="back-button"
                        OnClick="btnBack_Click" />
                </td>
                <td align="right">
                    <asp:Button ID="btnNext" runat="server" Text="Next >" CssClass="next-button" OnClick="btnNext_Click"
                        Visible="False" />
                </td>
            </tr>
        </table>        
        <asp:HiddenField ID="Locale" Value="1" runat="server" />
    </div>
    <asp:HiddenField ID="hashvalue" Value="" runat="server" />
    <div id="sidebar">
        <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
    </div>
    <div class="cleared"></div>
</asp:Content>
