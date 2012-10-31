<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectRMD.aspx.cs" Inherits="HairSlayer.SelectRMD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="stylesheets/ratemydo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="start-wrapper">
        <div id="select-do">
        <center>
            <h3 class="do-select">Select From Your Current Dos</h3>
            <img src="/images/icon-user.png" />
            <asp:Button ID="SelectDo" CssClass="do-select-button" runat="server" 
                Text="Select Do" onclick="SelectDo_Click" />
        </center>
        </div>
        <div id="upload-do">
            <center>
            <h3 class="do-select">Upload A New Do</h3>
            <img src="/images/icon-camera.png" />
            <asp:Button ID="UploadDo" CssClass="do-select-button" runat="server" 
                    Text="Upload Do" onclick="UploadDo_Click" />
            </center>
        </div>
    </div>
    </form>
</body>
</html>
