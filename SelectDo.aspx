<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectDo.aspx.cs" Inherits="HairSlayer.SelectDo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="stylesheets/ratemydo.css" type="text/css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script>

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
    <h3 class="do-select">
        <asp:Label ID="Prompt" runat="server" Text="Select your Do"></asp:Label></h3>
    <div id="do-wrapper">
        <asp:PlaceHolder ID="DoList" runat="server"></asp:PlaceHolder>
    </div>
    <asp:Button ID="DoSelectSubmit" CssClass="button-red" runat="server" Text="Submit Do" 
        onclick="Submit_Click" />

    <asp:HiddenField ID="SelectedDo" runat="server" />
       
    </form>
</body>
</html>
