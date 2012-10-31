<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="HairSlayer.User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="WImage" Src="~/UserControls/WatermarkImage.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ThumbGrid" Src="~/UserControls/ThumbGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <asp:Label ID="lblTest" Font-Size="24" runat="server" Text=""></asp:Label>
    <div id="main">
        <uc1:WImage ID="WImage1" Height="452" Width="718" LayerText="John Smithson" ImageURL="images/demo.jpg" runat="server"></uc1:WImage>
        <asp:Panel ID="pnlComments" style="margin-top: 15px;" runat="server">
        <div id="user_comments">Comments</div>
            <asp:GridView ID="CommentsGrid" AutoGenerateColumns="false" GridLines="None" ShowHeader="false" ShowFooter="false" Width="100%" CssClass="user_comment_text" runat="server">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="user_comment_text"><%# Eval("comment") %></span><br />
                            <span class="user_comment_detail">Comment By <%# Eval("comment_owner") %> on <%# Eval("comment_date") %></span><br /><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
    <div id="sidebar">
        <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
        <uc1:ThumbGrid ID="stylesGrid" GridTitle="My Styles" runat="server"></uc1:ThumbGrid>
        <uc1:ThumbGrid ID="awardsGrid" GridTitle="My Awards" style="margin: 20px 0px 0px 0px;" runat="server"></uc1:ThumbGrid>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
