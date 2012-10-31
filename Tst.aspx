<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tst.aspx.cs" Inherits="HairSlayer.Tst" %>

<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type ="text/javascript" >
        function UploadComplete(sender, args) {


            __doPostBack('Button1', '');
        }
   </script>
</head>
<body>
    <form id="form1" runat="server">
   
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Do It Do It" />
   
    </form>
</body>
</html>
