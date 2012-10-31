<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rmd.aspx.cs" Inherits="HairSlayer.Rmd" %>

<%@ Register TagPrefix="uc1" TagName="DoUpload" Src="~/UserControls/DoUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="stylesheets/ratemydo.css" type="text/css" />
</head>
<body style="font-family: Arial;">
    <form id="form1" runat="server">
    <div>
        <uc1:DoUpload ID="DoUpload" runat="server"></uc1:DoUpload>
    </div>
    </form>
</body>
</html>
