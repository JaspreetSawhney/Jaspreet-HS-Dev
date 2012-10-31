<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true"
    CodeBehind="Do.aspx.cs" Inherits="HairSlayer.Do" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WImage" Src="~/UserControls/WatermarkImage.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ThumbGrid" Src="~/UserControls/ThumbGrid.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="stylesheets/ratemydo.css" type="text/css" />
    <script type="text/javascript" src="http://assets.pinterest.com/js/pinit.js"></script>
    <script type="text/javascript">
        var rpxJsHost = (("https:" == document.location.protocol) ? "https://" : "http://static.");
        document.write(unescape("%3Cscript src='" + rpxJsHost + "rpxnow.com/js/lib/rpx.js' type='text/javascript'%3E%3C/script%3E"));
    </script>

    <script type="text/javascript">
        function rpxSocial(share_link) {
            var lk_url = "http://hairslayer.com/Do.aspx?id=" + share_link.toString();
            var comm = document.getElementById('MainBody_comment_box').value + " - via HairSlayer.com";
            var raw_lnk = "http://"  + window.location.host;
            RPXNOW.init({ appId: 'nnnfajpekbheghbofhbi', xdReceiver: raw_lnk + '/rpx_xdcomm.html' });
            RPXNOW.loadAndRun(['Social'], function () {
                var activity = new RPXNOW.Social.Activity(
            "Share your comment with your social network",
            comm,
            lk_url);
                activity.setDescription("Hairslayer.com - Find the Style, Find the Stylist");
                RPXNOW.Social.publishActivity(activity);
            });
        }
    </script>

    <script type="text/javascript">
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
    <div id="fb-root">
    </div>
    <script>        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=349936575038539";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));</script>
    
            <div id="full_style_gallery">
                <div id="image_holder">
                    <asp:Label ID="FullGallery" runat="server" Text=""></asp:Label>
                </div>
                <p id="close_gallery">
                    CLOSE X</p>
            </div>
            <% Response.Write(HairSlayer.includes.Functions.NotLoggedPopUp()); %>
            <div id="content_wrap">
                <div id="bread_crumb">
                    <a href="index.aspx">Home</a> &gt; <a href="#">Styles</a></div>
                <asp:Image ID="style_picture" AlternateText="" runat="server" /><br />
                <h1 id="hairstyle_title">
                    <asp:Label ID="DoName" runat="server" Text=""></asp:Label>
                </h1>
                <h2 id="hairstylist_title">
                    Style by:
                    <asp:Label ID="StyledBy" runat="server" Text=""></asp:Label></h2>
                <hr id="break_line">

                <div id="social_share_do">
                    <asp:HyperLink ID="GetStyle" CssClass="share_button" Visible="false" runat="server">Get This Style</asp:HyperLink>
                    <asp:HyperLink ID="LikeStyle" CssClass="share_button" Visible="false" runat="server">Add To My Faves</asp:HyperLink>
                    <asp:HyperLink ID="GetStyleNoLog" CssClass="share_button" NavigateUrl="#" Visible="false" runat="server">Get This Style</asp:HyperLink>
                    <asp:HyperLink ID="LikeStyleNoLog" CssClass="share_button" NavigateUrl="#" Visible="false" runat="server">Add To My Faves</asp:HyperLink>
                    <asp:Button ID="btnlikestyle" Visible="false" runat="server" CssClass="social_share-button"
                        Text="Add To My Faves" OnClick="btnlikestyle_Click" />
                </div>

                
                <div style="clear: both;"></div>

                <div class="hover-popup">
                <div class="inner">
                    <label>
                        <strong>RATING DISTRIBUTION</strong></label>
                    <table>
                        <tr>
                            <td class="style1">
                                5
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
                                4 
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
                                3
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
                                2 
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
                                1 
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
                
                <asp:Panel ID="pnlRatings" runat="server">
                <div class="rating-container rounded-corners">
                <h2 style="width: 175px; margin-bottom: 4px;"><span onmouseover="show_rating(1);" onmouseout="show_rating(0);">Rate the Style</span></h2>
                <div class="cleared"></div>    
                <asp:Panel ID="RatingsPanelLoggedIn" Visible="false" runat="server">
                <div id="rating-radio" class="floating-left">
                    <ul>
                        <li><a href="javascript:rate_do2(1,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating1"); } else { Response.Write("shear-rating1"); } %>">1</a></li>
                        <li><a href="javascript:rate_do2(2,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating2"); } else { Response.Write("shear-rating2"); } %>">2</a></li>
                        <li><a href="javascript:rate_do2(3,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating3"); } else { Response.Write("shear-rating3"); } %>">3</a></li>
                        <li><a href="javascript:rate_do2(4,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating4"); } else { Response.Write("shear-rating4"); } %>">4</a></li>
                        <li><a href="javascript:rate_do2(5,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating5"); } else { Response.Write("shear-rating5"); } %>">5</a></li>
                    </ul>
                </div>
                </asp:Panel>
                <asp:Panel ID="RatingsPanelNotLoggedIn" Visible="false" runat="server">
                <div id="rating-radio" class="floating-left">
                    <ul>
                        <li><a href="" id="rating_nonlogged" class="<% if (Star.Value == "M") { Response.Write("clipper-rating1"); } else { Response.Write("shear-rating1"); } %>">1</a></li>
                        <li><a href="" id="rating_nonlogged2" class="<% if (Star.Value == "M") { Response.Write("clipper-rating2"); } else { Response.Write("shear-rating2"); } %>">2</a></li>
                        <li><a href="" id="rating_nonlogged3" class="<% if (Star.Value == "M") { Response.Write("clipper-rating3"); } else { Response.Write("shear-rating3"); } %>">3</a></li>
                        <li><a href="" id="rating_nonlogged4" class="<% if (Star.Value == "M") { Response.Write("clipper-rating4"); } else { Response.Write("shear-rating4"); } %>">4</a></li>
                        <li><a href="" id="rating_nonlogged5" class="<% if (Star.Value == "M") { Response.Write("clipper-rating5"); } else { Response.Write("shear-rating5"); } %>">5</a></li>
                    </ul>
                </div>
                </asp:Panel>
                <div class="cleared"></div>
                </div>
                </asp:Panel>
                <asp:HiddenField ID="Star" runat="server" />
                <div id="comments_wrap">
                <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlComments" runat="server">
                        <label id="your_comment">
                            Your Comment</label><br />
                        <br />
                        <asp:TextBox ID="comment_box" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <br />
                        <asp:Button ID="CommentSubmit" Visible="false" runat="server" OnClick="btnsbmit_Click" Text="Submit" CssClass="site-button" />
                        <asp:Button ID="NoLogCommentSubmit" Visible="false" runat="server" Text="Submit" CssClass="site-button" />
                        <asp:Label ID="Comment_Error" runat="server" Visible="false" ForeColor="#FF0000"
                            Text="Your comment can't be blank."></asp:Label>
                    </asp:Panel>
                    <h2 id="recent_comments">
                        Recent Comments</h2>
                    <asp:GridView ID="CommentsGrid" AutoGenerateColumns="false" BorderWidth="0" GridLines="None"
                        ShowHeader="false" ShowFooter="false" Width="100%" CssClass="user_comment_text"
                        runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="user_comment">
                                        <p>
                                            <%# Eval("comment") %></p>
                                        <h3>
                                            <%# Eval("firstname") %>
                                            <%# Eval("lastname") %><h3>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            </div>
            <div id="sidebar">
                <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
                <div id="side_bar_grid">
                    <h2 id="stylist_name_side">
                        <asp:Label ID="StyleBy2" runat="server" Text="Click on image to get "></asp:Label></h2>
                    <div id="style-by-pic-link">
                        <asp:HyperLink ID="lnkProvider" runat="server">
                            <asp:Image ID="featured_image" Height="148" Width="235" runat="server" /></asp:HyperLink>
                        <div class="view_info_lk"></div>
                            <span class="style_info_lk">
                                <p class="stylist_name_lk">
                                    <asp:Label ID="stylist_name" runat="server" Text=""></asp:Label></p>
                            </span>
                    </div>
                    <asp:PlaceHolder ID="Sidebar_Image_Grid" runat="server"></asp:PlaceHolder>
                </div>
                <asp:Panel ID="pnlViewAll" Visible="false" runat="server">
                <a href="#gallery" id="view_more_button">View All Styles
                    <img src="/images/arrow_down.png" alt="" /></a>
                </asp:Panel>
            </div>
            <div class="clear">
            </div>
            <script type="text/javascript" src="//assets.pinterest.com/js/pinit.js"></script>


            <!--Share-Bar BEGINS-->
            <style>.FBConnectButton_Small{background-position:-5px -232px !important;border-left:1px solid #1A356E}.FBConnectButton_Text{margin-left:12px !important;padding:2px 3px 3px !important}#ShareBar{bottom: 3%;width:69px;Left: 0 !important;overflow:hidden;position: fixed;z-index: 100000;text-align:center;line-height:normal;_position: absolute;font-size:9px;}#ShareBar a,#ShareBar a:hover,#ShareBar a:visited{text-decoration:none;font-size:9px;}</style>
            <div id="ShareBar">
            <div style="float:left; margin:10px 0 0 10px;"><div id="fb-root"></div><script src="http://connect.facebook.net/en_US/all.js#appId=178131218926675&xfbml=1"></script><fb:like href="" send="false" layout="box_count" width="80" show_faces="false" action="like" font=""></fb:like></div><div style="float:left; margin:10px 0 0 10px;"><a name="fb_share" type="box_count" id="fb_share_button" href="http://www.facebook.com/sharer.php">Share</a><script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share";; type="text/javascript"></script></div><div style="float:left; margin:8px 0 0 8px;"><%  string pic = "http://" + Request.Url.Host + style_picture.ImageUrl, dest_url = "http://" + Request.Url.Host + System.Web.HttpContext.Current.Request.Url.AbsolutePath + "?" + Request.QueryString["id"], descr = "Hairslayer.com featured style"; Response.Write(@"<a href=""http://pinterest.com/pin/create/button/?url=" + System.Web.HttpUtility.UrlEncode(dest_url) + @"&media=" + System.Web.HttpUtility.UrlEncode(pic)  + "&description=" + System.Web.HttpUtility.UrlEncode(descr) + @""" id=""pinterest-share"" class=""pin-it-button"" count-layout=""horizontal""><img border=""0"" src=""//assets.pinterest.com/images/PinExt.png"" title=""Pin It"" /></a>"); %></div><div style="float:left; margin:8px 0 0 8px;"><script type="text/javascript" src="http://apis.google.com/js/plusone.js"></script><g:plusone size="tall"></g:plusone></div><div style="float:left; margin:8px 0 0 8px;"><a href="http://twitter.com/share";; class="twitter-share-button" id="twitter_share_button" data-count="vertical">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script></div><div style="float:left; margin:8px 0 0 8px; color:#000000; font-family:Arial, Helvetica, sans-serif; background-color:#29a2f4; padding:2px 3px; width: 45px;"><a href="http://share-bar.resellscripts.info";; title="Share Bar" target="_blank"><font color="#ffffff">Share</font><font color="#000000">Bar</font></a></div></div>
            <!--Share-Bar ENDS-->
</asp:Content>
