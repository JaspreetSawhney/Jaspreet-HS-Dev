<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DoUpload.ascx.cs" Inherits="HairSlayer.UserControls.DoUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>

  <script language="javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="https://api.filepicker.io/v1/filepicker.js"></script>
    <script type="text/javascript">
        filepicker.setKey('AwKjJvjd1SyOaN3dRcwCkz');
        function myFunction() {
            filepicker.pick({ 'mimetype': "image/*" }, function (fpfile) {

                filepicker.convert(fpfile,
                { width: 180, height: 180, fit: 'crop', align: 'faces' },
                function (bigfp) {
                    $("#imgprofilepic").attr("src", bigfp.url);
                

                    document.getElementById("<%= hdnpic1.ClientID %>").value = bigfp.url;



                });
            });
        }
    </script>


    <h2><asp:Label ID="doInfo" runat="server" Text="Before you upload your pic, please tell us a little more about your do."></asp:Label></h2>
    <p>
    </p>
    <asp:Label ID="Error" runat="server" ForeColor="#FF0000" Text="" 
        Visible="false"></asp:Label>
    <br />
    <asp:Panel ID="pnlUpload" runat="server">
        <table cellspacing="4" style="width:auto;height:auto;">
            <tr>
                <td>
                    What is the name of your do?<br />
                    <asp:TextBox ID="DoName" runat="server" CssClass="long_input"></asp:TextBox>
                </td>
            </tr>
                      <tr>
                                                    <td>Enter Name<br />
                       
                          <asp:TextBox ID="txtname" runat="server" CssClass="long_input"></asp:TextBox>
                                                       
                                                    </td>
                            
                       </tr>
                        <tr>
                                                    <td>Enter Email<br />
                       
                          <asp:TextBox ID="txtemail" runat="server" CssClass="long_input"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    </td>
                            
                       </tr>
                          <tr>
                                                    <td>Enter PhoneNO<br />
                       
                          <asp:TextBox ID="txtphone" runat="server" CssClass="long_input"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtphone"  ValidationGroup="a" ErrorMessage="Invalid Phone((xxx)-xxx-xxxx)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>

                                                   
                                                    </td>
                            
                       </tr>
            <tr>
                <td>
                    Service Provider:
                    <asp:RadioButtonList ID="ServiceProvider" runat="server" CellPadding="2" 
                        CellSpacing="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                        <asp:ListItem Value="M">Barber</asp:ListItem>
                        <asp:ListItem Value="F">Stylist</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <br />
        <br />
            <img alt="choose pic" id="imgprofilepic" />
                    <input id="conversion1" onclick="myFunction()" />
           
                    <asp:HiddenField ID="hdnpic1" runat="server" />
     
                    <asp:Button ID="savepic" runat="server" Text="Upload Do" OnClick="savepic_Click" CssClass="button-red"   ValidationGroup="a" />
   
    <%--    <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUpload" runat="server" CssClass="button-red" 
            OnClick="btnUpload_Click" Text="Upload" />--%>
    </asp:Panel>
  <%--  <asp:Panel ID="pnlCrop" runat="server" Visible="false">
        <asp:Image ID="Image1" runat="server" />
        <asp:Image ID="Image2" runat="server" ImageUrl="/images/spacer.gif" />
        <cs:CropImage ID="wci1" runat="server" cropbuttonid="btnCrop" image="Image1" 
            ratio="1/1" x="0" x2="100" y="0" y2="100" />
        <br />
        <asp:Button ID="btnCrop" runat="server" CssClass="button-red" 
            OnClick="btnCrop_Click" Text="Crop Image" />
        <asp:Button ID="btnGoBack" runat="server" CssClass="button-red" 
            OnClick="btnGoBack_Click" Style="margin-right: 40px;" Text="&lt; Go Back" 
            Visible="false" />
        <asp:Button ID="ReUpload" runat="server" CssClass="button-red" 
            OnClick="ReUpload_Click" Text="Upload Photo Again" Visible="false" />
        <asp:Button ID="btnSave" runat="server" CssClass="button-red" 
            OnClick="btnSave_Click" Text="Save Image" Visible="false" />
    </asp:Panel>--%>
<%--    <asp:TextBox ID="loc" runat="server" BorderColor="#FFFFFF" ForeColor="#FFFFFF" 
        Width="1px"></asp:TextBox>
    <asp:HiddenField ID="tmp_fle" runat="server" />
    <asp:HiddenField ID="fle_id" runat="server" />--%>


