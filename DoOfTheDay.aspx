<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true"
    CodeBehind="DoOfTheDay.aspx.cs" Inherits="HairSlayer.DoOfTheDay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WImage" Src="~/UserControls/WatermarkImage.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="stylesheets/ratemydo.css" type="text/css" />
    <link rel="stylesheet" href="stylesheets/doofday.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
    <div class="body-section">
        <div id="submit-dos">
            <div class="top-arrow"></div>
            <div class="rmd-popup">
	            <div class="heading">Rate My Do&trade;</div>
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
        	        <li><asp:TextBox ID="Submit_Stylist_Name" Text="Stylist Name" runat="server"></asp:TextBox></li>
        	        <li><asp:TextBox ID="Submit_Stylist_Email" Text="Stylist Email" runat="server"></asp:TextBox></li>
        	        <li><asp:Button ID="RateMyDoSubmission" runat="server" Text="Submit" /></li>
                        <div class="clear"></div>
                    </ul>
                </div>
            </div>
        </div>
        <div class="left-column floating-left">
            <center>
            <h1>Do of the Day</h1>
            <div id="sponsor">
            <h4>Sponsored by: <a href="#">http://www.hairslayer.com</a></h4> 
            </div>
            </center>
            <!-- Gallery -->
            <div class="gallery">
                <!-- Female -->
                <div class="f-container floating-left">
                    <div class="share">
                        <p><img src="images/share.jpg" border="0"></p>
                    </div>
                    <div class="female">
                        <asp:Image ID="do_of_day_female" ImageUrl="images/dooftheday.png" CssClass="rate_pic" runat="server" />
                        <div class="caption">
                            <p>
                            <label>Styled by:</label>  <asp:Label ID="stylist_name_female" runat="server" Text=""></asp:Label><br>
                            <label>Style Name:</label> <asp:Label ID="style_name_female" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="cleared"></div>
                    <asp:HyperLink ID="AddToFavesFemale" CssClass="button button-red rounded-corners" runat="server">Add to my faves</asp:HyperLink>
                </div>
                <!-- Male -->
                <div class="m-container floating-right">
                    <div class="share">
                        <p><img src="images/share.jpg" border="0"></p>
                    </div>
                    <div class="male">
                        <asp:Image ID="do_of_day_male" ImageUrl="images/dooftheday.png" CssClass="rate_pic" runat="server" />
                        <div class="caption">
                            <p>
                            <label>Styled by:</label>  <asp:Label ID="stylist_name_male" runat="server" Text=""></asp:Label><br>
                            <label>Style Name:</label> <asp:Label ID="style_name_male" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="cleared"></div>
                    <asp:HyperLink ID="AddToFavesMale" CssClass="button button-red rounded-corners" runat="server">Add to my faves</asp:HyperLink>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div id="sidebar">
        <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
    </div>
</asp:Content>
