<%@ Page Title="Hairslayer.com - Provider Profile" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true"
    CodeBehind="Prov.aspx.cs" Inherits="HairSlayer.Prov" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="ThumbGrid" Src="~/UserControls/ThumbGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WImage" Src="~/UserControls/WatermarkImage.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="stylesheets/ratemydo.css" type="text/css" />
    <script type="text/javascript">
        var rpxJsHost = (("https:" == document.location.protocol) ? "https://" : "http://static.");
        document.write(unescape("%3Cscript src='" + rpxJsHost + "rpxnow.com/js/lib/rpx.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
       <script type ="text/javascript" >

           function AddResource(w, h) {
               var baseText = null;
               var popUp = document.getElementById("addUpdateusefulLinks");
               popUp.style.display = 'block';
               popUp.style.top = "130px";
               popUp.style.left = "200px";
               popUp.style.width = w + "px";
               popUp.style.height = h + "px";
               return false;
               if (baseText == null) baseText = popUp.innerHTML;
               popUp.innerHTML = baseText;
             }

    </script>

    <script type="text/javascript">
        function rpxSocial(share_link) {
            var lk_url = "http://www.hairslayer.com/Prov.aspx?id=" + share_link.toString();
            var comm = document.getElementById('MainBody_txtcomment').value + " - via HairSlayer.com";
            var raw_lnk = "http://" + window.location.host;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
 
    <div id="full_style_gallery">
        <div id="image_holder">
            <asp:Label ID="FullGallery" runat="server" Text=""></asp:Label>
        </div>
        <p id="close_gallery">CLOSE X</p>
    </div>

    <% Response.Write(HairSlayer.includes.Functions.NotLoggedPopUp()); %>

    <div id="full_services_list">
        <div id="service_holder">
            ALL SERVICES
            <asp:PlaceHolder ID="ServicesPlaceholder" runat="server"></asp:PlaceHolder>
        </div>
        <p id="close_serv">CLOSE X</p>
    </div>

    <div id="full_bio">
        <div id="bio_holder">
            BIOGRAPHY<br /><br />
            <asp:PlaceHolder ID="BioPlaceholder" runat="server"></asp:PlaceHolder>
        </div>
        <p id="close_bio">CLOSE X</p>
    </div>

    <div id="fb-root"></div>
    <script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=349936575038539";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));</script>
    
    <div id="content_wrap">
        <div id="bread_crumb">
            <a href="index.aspx">Home</a> &gt; <a href="#">Stylist Profile</a>
        </div>
        <uc1:WImage ID="WImage1" runat="server"></uc1:WImage>
        <div class="fb-like" data-send="true" data-layout="button_count" data-width="450" data-show-faces="true" data-action="recommend"></div>
        
        <asp:Panel ID="pnlProfileInfo" runat="server">
        <div id="profile_information">
        
        <div id="services_offered">
		    <h3>Services Offered</h3>
            <asp:Label ID="lblServicesOffered" runat="server" Text=""></asp:Label>
            <asp:HyperLink ID="view_full_services" Visible="false" NavigateUrl="#services" runat="server">View All Services <img src="images/arrow_down.png"></asp:HyperLink>
        </div>

        <div id="contact_info">
            <h3>Contact Info</h3>
            <asp:Label ID="lblContactInfo" runat="server" Text=""></asp:Label>
           
                        <asp:Button ID="ContactBarber"  runat="server" Height="33px"              onclick="ContactBarber_Click" Text="Contact Stylish/Barber" Width="176px"/>
                 

                <asp:Label ID="mustlogin" runat="server" Visible=false></asp:Label>
            <asp:HyperLink ID="Facebook" ImageUrl="images/social-facebook_sm.png" ToolTip="Facebook Profile" Target="_blank" Width="20px" CssClass="social_icon_prof" Visible="false" runat="server"></asp:HyperLink>
            <asp:HyperLink ID="Twitter" ImageUrl="images/social-twitter_sm.png" ToolTip="Twitter Profile" Target="_blank" Width="20px" CssClass="social_icon_prof" Visible="false" runat="server"></asp:HyperLink>
            <asp:HyperLink ID="Instagram" ImageUrl="images/instagram.png" ToolTip="Instagram Profile" Target="_blank" Width="20px" CssClass="social_icon_prof" Visible="false" runat="server"></asp:HyperLink>
        </div>
        
            <div id="location">
		        <h3>Location</h3>
                <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                <asp:PlaceHolder ID="MapLocation" runat="server"></asp:PlaceHolder>
                <div id="map"></div>
                <asp:HyperLink ID="GetDirections" Target="_blank" runat="server">Get Directions</asp:HyperLink>
            </div>
        </div>  <!-- end profile-information -->
            &nbsp;</asp:Panel>

        <div style="clear:both;"></div>

        <asp:Panel ID="pnlRateProvider" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                    <div class="rating-container rounded-corners">
                        <h2 id="provider_rate">Rate this provider</h2>
                        <div class="cleared"></div>
                        <asp:HiddenField ID="Star" runat="server" />
                        <asp:Panel ID="RatingsPanelLoggedIn" Visible="false" runat="server">
                            <div id="rating-radio" class="floating-left">
                            <ul>
                                <li><a href="javascript:rate_prov(1,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating1"); } else { Response.Write("shear-rating1"); } %>">1</a></li>
                                <li><a href="javascript:rate_prov(2,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating2"); } else { Response.Write("shear-rating2"); } %>">2</a></li>
                                <li><a href="javascript:rate_prov(3,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating3"); } else { Response.Write("shear-rating3"); } %>">3</a></li>
                                <li><a href="javascript:rate_prov(4,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating4"); } else { Response.Write("shear-rating4"); } %>">4</a></li>
                                <li><a href="javascript:rate_prov(5,<% Response.Write(Request.QueryString["id"]); %>,<% Response.Write(HairSlayer.includes.Functions.generateUserID()); %>);" class="<% if (Star.Value == "M") { Response.Write("clipper-rating5"); } else { Response.Write("shear-rating5"); } %>">5</a></li>
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
                        <asp:Label ID="RatingSubmitted" runat="server" Visible="false" CssClass="rating-submitted" Text="Rating Submitted..."></asp:Label>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel>
        
        <div id="comments_wrap_profile"> 
        <h2 id="recent_comments">Recent Reviews</h2>
        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
            <ContentTemplate>
        <asp:GridView ID="CommentsGrid" AutoGenerateColumns="false" BorderWidth="0" GridLines="None"
            ShowHeader="false" ShowFooter="false" Width="100%" CssClass="user_comment_text"
            runat="server" OnRowCommand="CommentsGrid_RowCommand" OnRowDataBound="CommentsGrid_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>

                        <div class="user_comment">
                            <!-- <div class="single-comment"> -->
                            <asp:HiddenField ID="hfProviderComID" runat="server" Value='<%# Eval("idProviderComments") %>' />
                            <p>
                                <%# Eval("comment") %>
                            </p>
                            <h3><%# Eval("firstname") %> <%# Eval("lastname") %></h3>
                            
                            <div style="padding-left: 50px">
                                <asp:GridView ID="commentReplyGridView" AutoGenerateColumns="false" BorderWidth="0"
                                    GridLines="None" ShowHeader="false" ShowFooter="false" Width="100%" CssClass="user_comment_text"
                                    runat="server">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <span class="user_comment_detail">
                                                    <br />
                                                    <h3><%# Eval("firstname") %> <%# Eval("lastname") %></h3></span><br />
                                                <span class="user_comment_text">
                                                    <%# Eval("Reply")%>
                                                </span>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:LinkButton ID="lnkReply" runat="server" Text="Reply" CommandName="Reply" CommandArgument='<%#Container.DataItemIndex%>'>
                            </asp:LinkButton><br />
                            <asp:Panel ID="pnlReply" runat="server" Visible="false">
                                <asp:TextBox ID="txtreply" runat="server" Style="border: 0px solid #ffffff; background-color: #eeeeee;"
                                    TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox>
                                <br />
                                <asp:Button ID="btnsubmitReply" Text="Submit" CssClass="site-button" runat="server"
                                    CommandName="submitReply" CommandArgument='<%#Container.DataItemIndex%>' />
                                <asp:Button ID="btnCancelReply" Text="Cancel" CssClass="site-button" runat="server"
                                    CommandName="CancelReply" CommandArgument='<%#Container.DataItemIndex%>' />
                            </asp:Panel>
                            <!-- </div> -->
                            <div id="comment-hr">&nbsp;</div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <br />
        <asp:Panel ID="pnlComments" runat="server">
            Post a review:
            <br />
            <asp:TextBox ID="txtcomment" runat="server" Style="border: 0px solid #FFFFFF; background-color: #EEEEEE;"
                TextMode="MultiLine" Width="549px" Height="100px"></asp:TextBox>
            <br />
            <asp:Button ID="btnsbmit" runat="server" Visible="false" OnClick="btnsbmit_Click" Text="Submit" CssClass="site-button" />
            <asp:Button ID="SubmitCommentNonLogin" runat="server" Visible="false" Text="Submit" CssClass="site-button" />
            <br />
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="sidebar">
                <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
                <asp:Label ID="stylegridlist" runat="server" Text=""></asp:Label>
                <asp:Panel ID="pnlViewAll" Visible="false" runat="server">
                    <a href="#gallery" id="view_more_button">View All Styles <img src="images/arrow_down.png"></a>
                </asp:Panel>
                <br />&nbsp;<br />
                <div style="clear: both;"></div>
                <div id="appointment_wrap">
                <asp:Button ID="btnAppointment" runat="server" Visible="false" Text="Request Appointment"
                    OnClick="btnAppointment_Click" />
                </div>
                <asp:HiddenField ID="hdnProv" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
     <div id='addUpdateusefulLinks' style="display:none;position: absolute; top: 351px; left: 293px; width: 102px;">
     <div style="background-color: #CACACA; display: block; float: left; padding: 10px;
        width: 526px; display: block; float: left;margin-top:20px; height: 405px;">
        <div style="display: block; float: left; background-color: #fff; width: 526px; height: 401px;">
            <div style="display: block; float: left; padding: 7px; height: 377px; width: 511px;">


         <table class="style5">
            
             <tr>
                 <td>
                    Name
                 </td>
                 <td>
                     <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                    
             
                 </td>
             </tr>
             <tr>
                 <td>
                     Desired Service</td>
                 <td>
                      <asp:TextBox ID="txtservice" runat="server"></asp:TextBox>
                 </td>
             </tr>
            
            
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
                  
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Send" 
                         />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                         onclick="btnCancel_Click" />
                 </td>
             </tr>
         </table>

            

         </div>
         </div>

       </div>
     </div>
</asp:Content>
