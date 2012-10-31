<%@ Page Title="HairSlayer.com - Rate My Do" Language="C#" MasterPageFile="~/Header.Master"
    AutoEventWireup="true" CodeBehind="RateMyDo.aspx.cs" Inherits="HairSlayer.RateMyDo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WImage" Src="~/UserControls/WatermarkImage.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DoUpload" Src="~/UserControls/DoUpload.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 36px;
        }
    </style>
    <link rel="stylesheet" href="stylesheets/ratemydo.css" type="text/css" />
    <script type="text/javascript" src="js/ratemydo.js"></script>
    <!--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>-->
    <script type="text/javascript">
        $(window).load(function () {
            $('.flexslider').flexslider();
        });
        function show_rating(option) {
            if (option == 1) {
                $(".hover-popup").fadeIn();
            }
            else {
                $(".hover-popup").fadeOut();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
    <div id="submit-rmd" class="rounded-div">
            <div id="rmd-title">Rate My Do&trade;</div>
            <div id="rmd-close"><a href="" id="rmd-close-x">[X Close Window]</a></div>
            <div id="rmd-content">
                <asp:Panel ID="RMDUpload" runat="server">
                    <iframe scrolling="auto" src="SelectRMD.aspx" height="100%" width="100%" frameborder="0"></iframe>
                </asp:Panel>
            </div>
        </div>
    <!-- Body Section -->
    <div class="body-section">
        <!-- Left Column -->
        <div class="left-column floating-left">
            <div class="hover-popup">
                <div class="inner">
                    <label>
                        <strong>RATING DISTRIBUTION</strong></label>
                    <table>
                        <tr>
                            <td class="style1">
                                5 Scissors
                            </td>
                            <td class="style1">
                            </td>
                            <td class="style1">
                                <div class="rating-border">
                                    <div style="width: 85%;">
                                    </div>
                                </div>
                            </td>
                            <td class="style1">&nbsp;</td>
                            <td class="style1">
                                Excellent
                            </td>
                        </tr>
                        <tr>
                            <td class="rating">
                                4 Scissors
                            </td>
                            <td class="star">
                            </td>
                            <td class="rating-bar">
                                <div class="rating-border">
                                    <div style="width: 65%;">
                                    </div>
                                </div>
                            </td>
                            <td class="number">&nbsp;</td>
                            <td class="standard">
                                Very Good
                            </td>
                        </tr>
                        <tr>
                            <td class="rating">
                                3 Scissors
                            </td>
                            <td class="star">
                            </td>
                            <td class="rating-bar">
                                <div class="rating-border">
                                    <div style="width: 55%;">
                                    </div>
                                </div>
                            </td>
                            <td class="number">&nbsp;</td>
                            <td class="standard">
                                Good
                            </td>
                        </tr>
                        <tr>
                            <td class="rating">
                                2 Scissors
                            </td>
                            <td class="star">
                            </td>
                            <td class="rating-bar">
                                <div class="rating-border">
                                    <div style="width: 35%;">
                                    </div>
                                </div>
                            </td>
                            <td class="number">&nbsp;</td>
                            <td class="standard">
                                Average
                            </td>
                        </tr>
                        <tr>
                            <td class="rating">
                                1 Scissors
                            </td>
                            <td class="star">
                            </td>
                            <td class="rating-bar">
                                <div class="rating-border">
                                    <div style="width: 20%;">&nbsp;
                                    </div>
                                </div>
                            </td>
                            <td class="number">&nbsp;</td>
                            <td class="standard">
                                Normal
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="heading">
                <h1 class="floating-left">Rate My Do</h1>
                <div class="select-style floating-right">
                    <ul>
                        <li><asp:LinkButton ID="AllStyles" runat="server" onclick="AllStyles_Click">All Styles</asp:LinkButton></li>
                        <li><asp:LinkButton ID="Male" runat="server" onclick="Male_Click">Male</asp:LinkButton></li>
                        <li><asp:LinkButton ID="Female" runat="server" onclick="Female_Click">Female</asp:LinkButton></li>
                    </ul>
                </div>
                <div class="cleared"></div>
            </div>
            <!-- Gallery -->
            <div class="gallery">
                <div class="share">
                    <p>
                        <!--Share-Bar BEGINS-->
            <style>.FBConnectButton_Small{background-position:-5px -232px !important;border-left:1px solid #1A356E}.FBConnectButton_Text{margin-left:12px !important;padding:2px 3px 3px !important}</style>
                <div id="ShareBar">
                <div><div id="fb-root"></div><script src="http://connect.facebook.net/en_US/all.js#appId=178131218926675&xfbml=1"></script><fb:like href="" send="false" layout="box_count" width="80" show_faces="false" action="like" font=""></fb:like></div><div style="float:left; margin:10px 0 0 10px;"><a name="fb_share" type="box_count" id="fb_share_button" href="http://www.facebook.com/sharer.php">Share</a><script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share";; type="text/javascript"></script></div><div style="float:left; margin:8px 0 0 8px;"><%  string pic = "http://" + Request.Url.Host + rate_my_do_image.ImageUrl, dest_url = "http://" + Request.Url.Host + System.Web.HttpContext.Current.Request.Url.AbsolutePath + "?" + DoID.Value, descr = "Hairslayer.com featured style"; Response.Write(@"<a href=""http://pinterest.com/pin/create/button/?url=" + System.Web.HttpUtility.UrlEncode(dest_url) + @"&media=" + System.Web.HttpUtility.UrlEncode(pic)  + "&description=" + System.Web.HttpUtility.UrlEncode(descr) + @""" id=""pinterest-share"" class=""pin-it-button"" count-layout=""horizontal""><img border=""0"" src=""//assets.pinterest.com/images/PinExt.png"" title=""Pin It"" /></a>"); %></div><div style="float:left; margin:8px 0 0 8px;"><script type="text/javascript" src="http://apis.google.com/js/plusone.js"></script><g:plusone size="tall"></g:plusone></div><div style="float:left; margin:8px 0 0 8px;"><a href="http://twitter.com/share";; class="twitter-share-button" id="twitter_share_button" data-count="vertical">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script></div></div>
            <!--Share-Bar ENDS-->
                    </p>
                </div>
                <div class="rate_style">
                    <asp:Image ID="rate_my_do_image" CssClass="rate_pic" runat="server" />
                    <asp:ImageButton ID="right_scroll" ImageUrl="images/right_scroll.png" 
                        CssClass="right-scroll" runat="server" onclick="right_scroll_Click" />
                    <div class="view_info">
                    </div>
                    <span class="style_info">
                        <p class="hair_title">
                            <asp:Label ID="style_name" runat="server"></asp:Label></p>
                        <p class="stylist_name"><asp:Label ID="stylist_name" runat="server" Text=""></asp:Label></p>
                    </span>
                </div>
            </div>
            <asp:HiddenField ID="DoID" runat="server" />
            <div class="cleared"></div>
            <!-- Rate the Stylist -->
            <div class="rating-container rounded-corners">
                <h2 style="width: 175px; margin-bottom: 4px;"><span onmouseover="show_rating(1);" onmouseout="show_rating(0);">Rate the Style</span></h2>
                <div class="cleared"></div>
                <asp:Panel ID="pnlRatings" runat="server">                
                <div id="rating-radio" class="floating-left">
                    <ul>
                        <li><a href="javascript:rate_do(1,<% Response.Write(DoID.Value); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating1"); } else { Response.Write("shear-rating1"); } %>">1</a></li>
                        <li><a href="javascript:rate_do(2,<% Response.Write(DoID.Value); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating2"); } else { Response.Write("shear-rating2"); } %>">2</a></li>
                        <li><a href="javascript:rate_do(3,<% Response.Write(DoID.Value); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating3"); } else { Response.Write("shear-rating3"); } %>">3</a></li>
                        <li><a href="javascript:rate_do(4,<% Response.Write(DoID.Value); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating4"); } else { Response.Write("shear-rating4"); } %>">4</a></li>
                        <li><a href="javascript:rate_do(5,<% Response.Write(DoID.Value); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating5"); } else { Response.Write("shear-rating5"); } %>">5</a></li>
                    </ul>
                </div>
                </asp:Panel>
                <div class="floating-right">
                    <asp:HyperLink ID="GetStyle" CssClass="button button-red rounded-corners" Visible="false" runat="server">Get Style</asp:HyperLink>
                    <asp:HyperLink ID="AddToFaves" CssClass="button button-red rounded-corners" runat="server">Add to my faves</asp:HyperLink>                    
                </div>
                <div class="cleared"></div>
            </div>
        </div>
    </div>
    <div id="sidebar">
        <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
    </div>
    <asp:HiddenField ID="Star" runat="server" />
    
</asp:Content>
