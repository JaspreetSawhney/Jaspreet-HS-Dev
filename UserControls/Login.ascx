<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="HairSlayer.UserControls.Login" %>

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
