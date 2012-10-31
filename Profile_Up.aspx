<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile_Up.aspx.cs" Inherits="HairSlayer.Profile_Up" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Profile - Upload My Do</title>
    <meta name="description" content="Hairslayer is the social network for barbers and hairstylist." />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script>
    <script type="text/javascript" src="js/functions.js"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <link href='http://fonts.googleapis.com/css?family=Arvo' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" type="text/css" href="stylesheets/reset.css" />
    <link rel="stylesheet" type="text/css" href="stylesheets/style.css" />
    <meta name="keywords" content="Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle" />
    <script language="javascript" src="js/prototype.js"></script>
    <script language="javascript" src="js/picup.js"></script>
    
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
            jQuery("#hashvalue").val(window.location.hash);
        });
    </script>

    <script type="text/javascript">

        window.name = "my_form";
        var currentParams = {}

        document.observe('dom:loaded', function () {

            $(document.body).addClassName('iphone');

            // We'll check the hash when the page loads in-case it was opened in a new page
            // due to memory constraints
            Picup.checkHash();

            // Set some starter params	
            currentParams = {
                'callbackURL': 'http://dev.hairslayer.com/upload.htm',
                'referrername': escape('Hair Slayer'),
                'referrerfavicon': escape('http://dev.hairslayer.com/favicon.ico'),
                'purpose': escape('Select your photo to upload.'),
                'debug': 'true',
                'returnThumbnailDataURL': 'false'
            };

            if (ismobilesafari()) {
                Picup.convertFileInput($('DoFileUpload'), currentParams);
            }
        });

        function ismobilesafari() {
            return navigator.userAgent.match(/(iPod|iPhone|iPad)/);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager19" runat="server" />
    <div id="login_popup">
        <img id="close_button" src="images/close.png" alt="" />
        <script type="text/javascript">
            (function () {
                if (typeof window.janrain !== 'object') window.janrain = {};
                window.janrain.settings = {};

                janrain.settings.tokenUrl = 'http://hairslayer.com/xchange.aspx';

                function isReady() { janrain.ready = true; };
                if (document.addEventListener) {
                    document.addEventListener("DOMContentLoaded", isReady, false);
                } else {
                    window.attachEvent('onload', isReady);
                }

                var e = document.createElement('script');
                e.type = 'text/javascript';
                e.id = 'janrainAuthWidget';

                if (document.location.protocol === 'https:') {
                    e.src = 'https://rpxnow.com/js/lib/hair-slayer-social-comments/engage.js';
                } else {
                    e.src = 'http://widget-cdn.rpxnow.com/js/lib/hair-slayer-social-comments/engage.js';
                }

                var s = document.getElementsByTagName('script')[0];
                s.parentNode.insertBefore(e, s);
            })();
        </script>
        <div id="janrainEngageEmbed">
        </div>
        <p id="or_login">
            Or</p>
        <label>
            Email:</label>
        <br />
        <asp:TextBox ID="email_login" runat="server"></asp:TextBox><br />
        <br />
        <label>
            Password:</label>
        <asp:TextBox ID="password_login" TextMode="Password" runat="server"></asp:TextBox><br />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Log in" OnClick="btnLogin_Click" />
        <a href="forgot.aspx" id="forgot_password">Forgot Password</a> <a href="signup.aspx"
            id="not_registered">Not registered yet?</a>
    </div>
    <div id="screen_cover">
    </div>
    <div id="fav_pic_gallery">
        <div id="fav_image_holder">
            <h2 id="my-faves">
                My Faves</h2>
            <asp:PlaceHolder ID="Faves" runat="server"></asp:PlaceHolder>
        </div>
        <p id="close_pics">
            CLOSE X</p>
    </div>
    <div id="wrapper">
        <div id="header">
            <a href="/index.aspx">
                <img src="images/logo.png" alt="hairslayer logo"></a>
            <asp:HyperLink ID="login" NavigateUrl="#logins" runat="server">Login</asp:HyperLink>
            <asp:LinkButton ID="logout" Visible="false" OnClick="logout_Click" runat="server">Logout</asp:LinkButton>
            <div id="header_search_bar">
                <asp:TextBox ID="looking_for" runat="server">What are you looking for?</asp:TextBox>
                <asp:TextBox ID="search_location" runat="server">City, State or Zip</asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click" />
            </div>
        </div>
        <div id="content_wrap">
            <div id="bread_crumb">
                <a href="index.aspx">Home</a> > <a href="#">Sign Up</a> > <a href="#">Providers</a></div>
            <div id="info_navigation">
                <asp:Button ID="About" runat="server" OnClick="About_Click" CssClass="menu-tab" Text="About" />
                <asp:Button ID="Styles" Visible="false" CssClass="menu-tab" runat="server" Text="My Styles"
                    OnClick="Styles_Click" />
                <asp:Button ID="Service" runat="server" OnClick="Service_Click" CssClass="menu-tab"
                    Text="My Services" />
                <asp:Button ID="Upload" runat="server" OnClick="Upload_Click" CssClass="info_active"
                    Text="Upload My Do" />
                <asp:Button ID="Schedule" runat="server" CssClass="menu-tab" Text="My Schedule" OnClick="Schedule_Click" />
            </div>
            <asp:Panel ID="pnlUploadDo" runat="server">
                <p>
                    <h2>
                        <asp:Label ID="doInfo" runat="server" Text="Before you upload your pic, please tell us a little more about your do."></asp:Label></h2>
                    <asp:Label ID="DoError" runat="server" ForeColor="#FF0000" Text="" Visible="false"></asp:Label>
                    <br />
                    <asp:Panel ID="NoUploads" runat="server" Visible="false">
                        <center>
                            <h2 id="nouploads-h2">
                                Only 3 styles can be uploaded with a free account.
                            </h2>
                            <h2 id="nouploads-h2">
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
                        <asp:Button ID="btnDoUpload" runat="server" CssClass="site-button" OnClick="btnDoUpload_Click"
                            Text="Upload" />
                    </asp:Panel>
                    <asp:Panel ID="pnlDoCrop" runat="server" Visible="false">
                        <asp:Image ID="DoImage1" runat="server" />
                        <asp:Image ID="DoImage2" runat="server" ImageUrl="images/spacer.gif" />
                        <cs:cropimage id="Dowci1" runat="server" cropbuttonid="btnCrop" image="DoImage1"
                            ratio="1/1" x="0" x2="100" y="0" y2="100" />
                        <br />
                        <asp:HyperLink ID="UploadAnotherStyle" runat="server" CssClass="site-button" NavigateUrl="~/Up.aspx"
                            Visible="false">Upload Another Style</asp:HyperLink>
                        <asp:HyperLink ID="ViewDoProfile" runat="server" CssClass="site-button" Target="_parent"
                            Visible="false">View Profile</asp:HyperLink>
                        <asp:Button ID="btnDoCrop" runat="server" CssClass="site-button" OnClick="btnDoCrop_Click"
                            Text="Crop Image" />
                        <asp:Button ID="btnDoGoBack" runat="server" CssClass="site-button" OnClick="btnDoGoBack_Click"
                            Style="margin-right: 40px;" Text="&lt; Go Back" Visible="false" />
                        <asp:Button ID="ReUpload" runat="server" CssClass="site-button" OnClick="ReUpload_Click"
                            Text="Upload Photo Again" Visible="false" />
                        <asp:Button ID="btnDoSave" runat="server" CssClass="site-button" OnClick="btnDoSave_Click"
                            Text="Save Image" Visible="false" />
                    </asp:Panel>
                    <asp:TextBox ID="loc" runat="server" BorderColor="#FFFFFF" ForeColor="#FFFFFF" Width="1px"></asp:TextBox>
                    <asp:HiddenField ID="tmp_fle_do" runat="server" />
                    <asp:HiddenField ID="fle_id_do" runat="server" />
                    <p>
                    </p>
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
            <uc1:navbar id="NavigationMenu" runat="server"></uc1:navbar>
        </div>
        <div class="cleared">
        </div>
        <div id="push">
        </div>
    </div>
    <div class="cleared">
    </div>
    <uc1:footer id="Footers" runat="server"></uc1:footer>
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
    <asp:PlaceHolder ID="KissHolder" runat="server"></asp:PlaceHolder>
    </form>
    <!-- begin olark code -->
    <script type='text/javascript'>/*{literal}<![CDATA[*/
    window.olark||(function(c){var f=window,d=document,l=f.location.protocol=="https:"?"https:":"http:",z=c.name,r="load";var nt=function(){f[z]=function(){(a.s=a.s||[]).push(arguments)};var a=f[z]._={},q=c.methods.length;while(q--){(function(n){f[z][n]=function(){f[z]("call",n,arguments)}})(c.methods[q])}a.l=c.loader;a.i=nt;a.p={0:+new Date};a.P=function(u){a.p[u]=new Date-a.p[0]};function s(){a.P(r);f[z](r)}f.addEventListener?f.addEventListener(r,s,false):f.attachEvent("on"+r,s);var ld=function(){function p(hd){hd="head";return["<",hd,"></",hd,"><",i,' onl' + 'oad="var d=',g,";d.getElementsByTagName('head')[0].",j,"(d.",h,"('script')).",k,"='",l,"//",a.l,"'",'"',"></",i,">"].join("")}var i="body",m=d[i];if(!m){return setTimeout(ld,100)}a.P(1);var j="appendChild",h="createElement",k="src",n=d[h]("div"),v=n[j](d[h](z)),b=d[h]("iframe"),g="document",e="domain",o;n.style.display="none";m.insertBefore(n,m.firstChild).id=z;b.frameBorder="0";b.id=z+"-loader";if(/MSIE[ ]+6/.test(navigator.userAgent)){b.src="javascript:false"}b.allowTransparency="true";v[j](b);try{b.contentWindow[g].open()}catch(w){c[e]=d[e];o="javascript:var d="+g+".open();d.domain='"+d.domain+"';";b[k]=o+"void(0);"}try{var t=b.contentWindow[g];t.write(p());t.close()}catch(x){b[k]=o+'d.write("'+p().replace(/"/g,String.fromCharCode(92)+'"')+'");d.close();'}a.P(2)};ld()};nt()})({loader: "static.olark.com/jsclient/loader0.js",name:"olark",methods:["configure","extend","declare","identify"]});
    /* custom configuration goes here (www.olark.com/documentation) */
    olark.identify('6556-878-10-8755');/*]]>{/literal}*/</script>
    <script>        olark.configure('system.allow_mobile_boot', true);</script>
    <!-- end olark code -->
    <script type="text/javascript">
        setTimeout(function () {
            var a = document.createElement("script");
            var b = document.getElementsByTagName('script')[0];
            a.src = document.location.protocol + "//dnn506yrbagrg.cloudfront.net/pages/scripts/0012/7909.js?" + Math.floor(new Date().getTime() / 3600000);
            a.async = true; a.type = "text/javascript"; b.parentNode.insertBefore(a, b)
        }, 1);
    </script>
    <script type="text/javascript" src="js/plugs.js"></script>
</body>
</html>
