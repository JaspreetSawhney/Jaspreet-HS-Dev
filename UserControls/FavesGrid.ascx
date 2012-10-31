<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FavesGrid.ascx.cs" Inherits="HairSlayer.UserControls.FavesGrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ModalPopupExtender ID="ModalPopupExtender1" CancelControlID="btnExit" PopupControlID="pnlFaves"
    BackgroundCssClass="pop_up" DropShadow="true" TargetControlID="favImage1" runat="server">
</asp:ModalPopupExtender>
<div id="grid_title">
    <asp:LinkButton ID="btnMyFaves" runat="server" OnClick="btnMyFaves_Click">MY FAVES</asp:LinkButton></div>
<table id="thumb_grid" border="0">
    <tr>
        <td width="60" height="60">
            <asp:ImageButton ID="favImage1" runat="server" OnClick="favImage1_Click" />
        </td>
        <td width="60">
            <asp:ImageButton ID="favImage2" runat="server" OnClick="favImage2_Click" />
        </td>
        <td width="60">
            <asp:ImageButton ID="favImage3" runat="server" OnClick="favImage3_Click" Style="width: 14px" />
        </td>
    </tr>
    <tr>
        <td height="60">
            <asp:ImageButton ID="favImage4" runat="server" OnClick="favImage4_Click" />
        </td>
        <td>
            <asp:ImageButton ID="favImage5" runat="server" OnClick="favImage5_Click" Style="width: 14px" />
        </td>
        <td>
            <asp:ImageButton ID="favImage6" runat="server" OnClick="favImage6_Click" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlFaves" runat="server">
    <asp:ImageButton ID="btnExit" ImageUrl="~/images/exit.gif" runat="server" /><br />
    <br />
    <asp:ListView ID="favItemsListView" GroupItemCount="4" runat="server">
        <LayoutTemplate>
            <table cellpadding="2" runat="server" id="tblProducts" style="height: 320px">
                <tr runat="server" id="groupPlaceholder">
                </tr>
            </table>
        </LayoutTemplate>
        <GroupTemplate>
            <tr runat="server" id="productRow" style="height: 80px">
                <td runat="server" id="itemPlaceholder">
                </td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <td id="Td1" valign="top" align="center" style="width: 100" runat="server">
                <asp:Image ID="ProductImage" runat="server" ImageUrl='<%#"~/images/thumbnails/" + Eval("ThumbnailPhotoFileName") %>' Height="49" /><br />
                <asp:HyperLink ID="ProductLink" runat="server" Target="_blank" Text='<% #Eval("Name")%>' NavigateUrl='<%#"ShowProduct.aspx?ProductID=" + Eval("ProductID") %>' />
            </td>
        </ItemTemplate>
    </asp:ListView>
</asp:Panel>
