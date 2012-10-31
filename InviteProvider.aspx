<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InviteProvider.aspx.cs"
    Inherits="HairSlayer.InviteProvider" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.maskedinput.js"></script>
    <script type="text/javascript" src="js/script.js"></script>
    <link rel="stylesheet" href="stylesheets/ratemydo.css" type="text/css" />
    <style>
        body 
        {
            font-family: Arial;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ProviderPhone').mask("999-999-9999");
        });
    </script>

    <script type="text/javascript">
        var rpxJsHost = (("https:" == document.location.protocol) ? "https://" : "http://static.");
        document.write(unescape("%3Cscript src='" + rpxJsHost + "rpxnow.com/js/lib/rpx.js' type='text/javascript'%3E%3C/script%3E"));
    </script>

    <script type="text/javascript">
        function rpxSocial(share_link) {
            var lk_url = "http://hairslayer.com/Do.aspx?id=" + share_link.toString();
            var comm = "I have just entered my style into the Rate My Do contest at HairSlayer.com.  <a href='http://www.hairslayer.com/ratemydo.aspx?id='" + share_link + ">Click Here to vote for me.</a>";
            var raw_lnk = "http://" + window.location.host;
            RPXNOW.init({ appId: 'nnnfajpekbheghbofhbi', xdReceiver: raw_lnk + '/rpx_xdcomm.html' });
            RPXNOW.loadAndRun(['Social'], function () {
                var activity = new RPXNOW.Social.Activity(
            "Share your comment with your social network",
            comm,
            lk_url);
                activity.setDescription("Hairslayer.com - Find the Style, Find the Stylist");
                RPXNOW.Social.publishActivity(activity);
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager19" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlInvite" runat="server">
            <div class="title">
                Take A Moment To Invite Your Stylist/Barber to HairSlayer</div>
            <div>
                Provider Name:<br />
                <asp:TextBox ID="ProviderName" CssClass="long_input" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="ProviderName_AutoCompleteExtender" runat="server" CompletionListCssClass="comp-list"
                    Enabled="True" ServicePath="HS-Service.asmx" ServiceMethod="GetProviderName"
                    TargetControlID="ProviderName" UseContextKey="True">
                </asp:AutoCompleteExtender>
            </div>
            <div>
                Provider Email:<br />
                <asp:TextBox ID="ProviderEmail" CssClass="long_input" runat="server"></asp:TextBox>
            </div>
            <div>
                Provider Cell Phone:<br />
                <asp:TextBox ID="ProviderPhone" CssClass="long_input" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="Invite" runat="server" CssClass="button-red" Text="Send Invitation"
                    OnClick="Invite_Click" />
                &nbsp; &nbsp;
                <asp:Button ID="Skip" runat="server" CssClass="button-red" Text="Skip" 
                    onclick="Skip_Click" />
            </div>
            </asp:Panel>
            <asp:Panel ID="pnlComplete" Visible="false" runat="server">
                <h3>CONGRATULATIONS!</h3>
                <h3>Your Do Has Been Submitted to Rate My Do&trade;.</h3>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
