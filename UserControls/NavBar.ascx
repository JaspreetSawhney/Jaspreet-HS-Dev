<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavBar.ascx.cs" Inherits="HairSlayer.UserControls.MemberNavBar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:UpdatePanel ID="UpdatePanel21" runat="server">
<ContentTemplate>
<div id="member_navigation">
<asp:Panel ID="pnlMember" runat="server">
<ul id="sidebar_links">
    <li><a href="Index.aspx">Search for Styles</a></li>
    <li><a href="dooftheday.aspx">Do of the Day&trade;</a></li>
    <li><a href="ratemydo.aspx">Rate My Do&trade;</a></li>
    <% if (HairSlayer.includes.Functions.is_logged_in()) { %>
    <li>
        <a href="" id="join-ratemydo">Submit to Rate My Do&trade;</a>
        <div id="rmd-popup">
            <div class="top-arrow"></div>
            <div class="rmd-popup">
	    <div class="heading">Rate My Do</div>
        <div class="rmdp-content">
    	<ul>
        	<li>
            	<div class="upload-file">
                	<a href="#" class="button-red">Upload File</a>
                </div>
            </li>
        	<li><div class="take-photo">
            	<a href="#" class="button-red">Take Photo</a>
            </div></li>
        	<li><input type="text" name="stylist_name" placeholder="Stylist Name"></li>
        	<li><input type="text" name="stylist_email" placeholder="Stylist Email"></li>
        	<li><input type="submit" value="Submit"</li>
            <div class="clear"></div>
        </ul>
        </div>
        </div>
        </div>
    </li>
    <% } %>
    <%if (HairSlayer.includes.Functions.is_logged_in())
      { %>
           <% if (Request.Cookies["hr_main_ck"]["membership"] != "3")
              { %>  
    <li><asp:LinkButton ID="btnMy_Dos" runat="server" onclick="btnMy_Dos_Click">My Do's</asp:LinkButton>
        <asp:ModalPopupExtender ID="lnkDoPortfolio_ModalPopup" runat="server" DynamicServicePath=""
                        Enabled="True" TargetControlID="btnMy_Dos" PopupControlID="pnlPopupPortfolio"
                        CancelControlID="btnClosePortfolio">
                    </asp:ModalPopupExtender>
        <asp:Panel ID="pnlPopupPortfolio" ScrollBars="Auto" runat="server" Width="600" Style="background-color: #fff;
        border: 5px solid #d9e1f4; background-color: #000000; display: none; vertical-align: middle;">
        <table class="style1">
            <tr>
                <td>
                    <span style="color: #999999; font-weight: bold;">My Dos</span>
                </td>
            </tr>
            <tr>
                <td>                 
                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="4" CellPadding="4" Width="200px" Height="200px"
                        OnItemDataBound="DataList1_ItemDataBound" OnItemCommand="DataList1_ItemCommand">
                        <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="125px" Width="125px" CommandName="Click" />
                            <asp:Label ID="lblidDo" runat="server" Visible="False" Text='<%#Eval("idDo") %>'></asp:Label>
                            <asp:Label ID="lblidMember" runat="server" Visible="False" Text='<%#Eval("idMember") %>'></asp:Label>
                            
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Button ID="btnClosePortfolio" runat="server" Text="Close" Font-Italic="False" />
    </asp:Panel>
    </li>  
    <% } %>
    <li><a href="#pics" id="view_pics_button">My Faves</a></li>
    <li><a href="Profile.aspx">My Profile</a></li>
    <% }
      else
      { %>
    <li><a href="signup.aspx?id=3">User Registration</a></li>
    <li><a href="signup.aspx?id=1">Barber/Stylist Registration</a></li>
    <% }
      %>

    <% if (System.IO.Path.GetFileName(Request.Path).ToLower() == "signup.aspx" && Request.QueryString["id"] != null) { 
           if (Request.QueryString["id"] == "1") {
           %>
    <li><a href="Features.aspx" style="color: #FF0000;">Features/Pricing Page</a></li>
    <%     } 
       } %>
</ul>
</asp:Panel>
</div>
</ContentTemplate>
</asp:UpdatePanel>



   
