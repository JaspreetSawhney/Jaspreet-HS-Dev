<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TwilioVoiceCall.aspx.cs" Inherits="HairSlayer.TwilioVoiceCall" %>

<body>
  
   <%-- <script type="text/javascript">
        var httpReq = null;
        function callASHX() {
            httpReq = XMLHttpRequest();
            httpReq.onreadystatechange = XMLHttpRequestCompleted;
            httpReq.open("GET", "Handler1.ashx?Name=" +
                document.getElementById('<%=txtcall.ClientID%>').value, true);
            httpReq.send(null);
        }

        // initialize XMLHttpRequest object
        function XMLHttpRequest() {
            var xmlHttp;
            try {
                // Opera 8.0+, Firefox, Safari
                xmlHttp = new XMLHttpRequest();
            }
            catch (e) {
                // IEBrowsers
                try {
                    xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
                }
                catch (e) {
                    try {
                        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
                    }
                    catch (e) {
                        return false;
                    }
                }
            }
            return xmlHttp;
        }

        function XMLHttpRequestCompleted() {
            if (httpReq.readyState == 4) {
                try {
                    alert(httpReq.responseText);
                }
                catch (e) {
                }
            }
        }
</script>--%>
<form id="form1" runat="server">
    <div> <asp:BulletedList ID="varDisplay" runat="server" BulletStyle="NotSet"> </asp:BulletedList> </div> 
    <asp:TextBox ID="txtcall" runat="server"></asp:TextBox>
        <asp:TextBox ID="message" runat="server"></asp:TextBox>
   <%-- <span>Your Number: <input type="text" name="called" /></span>--%>
<%--    <input type="submit" value="Connect me!" />--%>
   <asp:Button ID="btn" runat="server" OnClick="btn_Click" />
</form>
</body>