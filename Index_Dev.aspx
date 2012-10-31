<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Dev.aspx.cs" Inherits="HairSlayer.Index_Dev" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="IImage" Src="~/UserControls/InteractiveImage.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hairslayer.com - Find the Style, Find the Stylist</title>
    <meta name="description" content="Hairslayer is the social network for barbers and hairstylist." />
    <meta name="keywords" content="Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script>
    <script type="text/javascript" src="script.js"></script>
    <script type="text/javascript" src="js/functions.js"></script>
    <link rel="stylesheet" type="text/css" href="stylesheets/reset.css" />
    <link rel="stylesheet" type="text/css" href="stylesheets/style.css" />
    <link rel="stylesheet" type="text/css" href="stylesheets/home.css" />
    <script type="text/javascript">        var _vouchfor_config = _vouchfor_config || {}; _vouchfor_config.campaignTag = "Hair-Slayer"; _vouchfor_config.offer = " $10 Off "; _vouchfor_config.position = "left-pos"; _vouchfor_config.host = "http://vouchfor.com"; _vouchfor_config.analytics = "https://ssl.google-analytics.com/__utm.gif?utms=1&utmhn=vouchfor.com&utmp=%2FHair-Slayer%2Fwidget%2Fvisit&utmac=UA-23418569-1&utmt=event&utme=5%28widget*open%20widget*www.hairslayer.com%29%28833778%29"; (function () { var vfjs = document.createElement('script'); vfjs.type = 'text/javascript'; vfjs.async = true; vfjs.src = 'http://vouchfor.com/js/vouchfor-widget/vouchfor-widget.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(vfjs, s); })();</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
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
        <asp:TextBox ID="email_login" runat="server"></asp:TextBox>
        <br />
        <br />
        <label>
            Password:</label>
        <asp:TextBox ID="password_login" TextMode="Password" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Log in" OnClick="btnLogin_Click" />
        <a href="forgot.aspx" id="forgot_password">Forgot Password</a>
        <a href="signup.aspx" id="not_registered">Not registered yet?</a>
    </div>
    
    <div id="screen_cover"></div>

    <div id="fav_pic_gallery">
        <div id="image_holder">
            <h2 id="my-faves">My Faves</h2>
            <asp:PlaceHolder ID="Faves" runat="server"></asp:PlaceHolder>
        </div>
        <p id="close_pics">CLOSE X</p>
    </div>

    <div id="log-intro">
                Please <a href="Login.aspx">sign in</a> or <a href="Signup.aspx">register</a> to save style.
                <br />
                <div id="lead-wrap">
                <a href="Login.aspx" class="log_button">Sign In</a>
                <a href="Signup.aspx" class="reg_button">Register</a>
                </div>
                <p id="log-intro-close">
                    CLOSE X</p>
    </div>
    <div id="wrapper">
        <div id="header">
            <a href="/index.aspx">
                <img src="images/logo.png" alt="hairslayer logo"></a>
            <asp:HyperLink ID="login" NavigateUrl="#logins" runat="server">Login</asp:HyperLink>
            <asp:LinkButton ID="logout" Visible="false" runat="server" onclick="logout_Click">Logout</asp:LinkButton>
        </div>
        <div id="search_wrap_home">
            <img id="mission" src="images/mission.png">
            <asp:TextBox ID="looking_for" runat="server">What are you looking for?</asp:TextBox>
            <asp:TextBox ID="search_location" Width="190px" runat="server">City, State or Zip</asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click" />
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="content_wrap">
                    <p>Click on image to get stylist info</p>
                    <br>
                    <asp:LinkButton ID="AllStyles" CssClass="gender" runat="server" 
                        onclick="AllStyles_Click">All Styles</asp:LinkButton>
                    <asp:LinkButton ID="Male" CssClass="gender" runat="server" onclick="Male_Click">Male</asp:LinkButton>
                    <asp:ImageButton ID="MaleButton" CssClass="gender_2" Visible="false" ImageUrl="images/arrow_down.png" 
                        runat="server" onclick="MaleButton_Click" />
                    <asp:LinkButton ID="Female" CssClass="gender" runat="server" 
                        onclick="Female_Click">Female</asp:LinkButton>
                    <asp:ImageButton ID="FemaleButton" CssClass="gender_2" Visible="false" ImageUrl="images/arrow_down.png" 
                        runat="server" onclick="FemaleButton_Click" />
                    <asp:Label ID="StyleGrid" runat="server" Text=""></asp:Label>
                </div>

                <div id="sidebar_home">
                    <div id="sidebar_wrap">
                    <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
                    <div id="punchtab_widget"></div>
                    </div>
                    
                </div>

                <div class="cleared"></div>

                <asp:HiddenField ID="do_id" runat="server" />
                <asp:Button ID="btnDoID" BackColor="#FFFFFF" Width="1" Height="1" ForeColor="#FFFFFF"
                    BorderStyle="None" runat="server" Text="" OnClick="btnDoID_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    <div id="push"></div>
    </div>
    <div class="cleared"></div>
    
    <uc1:Footer ID="Footers" runat="server"></uc1:Footer>
    </form>
    
    <!-- begin olark code -->
    <script type='text/javascript'>/*{literal}<![CDATA[*/
    window.olark||(function(c){var f=window,d=document,l=f.location.protocol=="https:"?"https:":"http:",z=c.name,r="load";var nt=function(){f[z]=function(){(a.s=a.s||[]).push(arguments)};var a=f[z]._={},q=c.methods.length;while(q--){(function(n){f[z][n]=function(){f[z]("call",n,arguments)}})(c.methods[q])}a.l=c.loader;a.i=nt;a.p={0:+new Date};a.P=function(u){a.p[u]=new Date-a.p[0]};function s(){a.P(r);f[z](r)}f.addEventListener?f.addEventListener(r,s,false):f.attachEvent("on"+r,s);var ld=function(){function p(hd){hd="head";return["<",hd,"></",hd,"><",i,' onl' + 'oad="var d=',g,";d.getElementsByTagName('head')[0].",j,"(d.",h,"('script')).",k,"='",l,"//",a.l,"'",'"',"></",i,">"].join("")}var i="body",m=d[i];if(!m){return setTimeout(ld,100)}a.P(1);var j="appendChild",h="createElement",k="src",n=d[h]("div"),v=n[j](d[h](z)),b=d[h]("iframe"),g="document",e="domain",o;n.style.display="none";m.insertBefore(n,m.firstChild).id=z;b.frameBorder="0";b.id=z+"-loader";if(/MSIE[ ]+6/.test(navigator.userAgent)){b.src="javascript:false"}b.allowTransparency="true";v[j](b);try{b.contentWindow[g].open()}catch(w){c[e]=d[e];o="javascript:var d="+g+".open();d.domain='"+d.domain+"';";b[k]=o+"void(0);"}try{var t=b.contentWindow[g];t.write(p());t.close()}catch(x){b[k]=o+'d.write("'+p().replace(/"/g,String.fromCharCode(92)+'"')+'");d.close();'}a.P(2)};ld()};nt()})({loader: "static.olark.com/jsclient/loader0.js",name:"olark",methods:["configure","extend","declare","identify"]});
    /* custom configuration goes here (www.olark.com/documentation) */
    olark.identify('6556-878-10-8755');/*]]>{/literal}*/</script>
    <!-- end olark code -->
    <script>        olark.configure('system.allow_mobile_boot', true);</script>
    <script type="text/javascript">
        setTimeout(function () {
            var a = document.createElement("script");
            var b = document.getElementsByTagName('script')[0];
            a.src = document.location.protocol + "//dnn506yrbagrg.cloudfront.net/pages/scripts/0012/7909.js?" + Math.floor(new Date().getTime() / 3600000);
            a.async = true; a.type = "text/javascript"; b.parentNode.insertBefore(a, b)
        }, 1);
    </script>
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
    <script type="text/javascript" src="js/plugs.js"></script>
</body>
</html>
