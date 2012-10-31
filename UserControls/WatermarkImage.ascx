<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WatermarkImage.ascx.cs" Inherits="HairSlayer.UserControls.WatermarkImage" %>
<div id="featured_profile_wrap">
    <img id="featured_profile_img" src="<%=ImageURL %>" alt="" />
    <div id="info_bar_prof"></div>
    <h1 id="stylist_name_prof"> <%=Line1Text %> <a href="#bio" id="view_bio_button" class="bio-link">(Bio)</a><h1>
    <h2 id="stylist_shop_name_prof"><%=Line2Text %></h2>
    <% Response.Write(GetRateImage(Rating, Membership)); %>
</div>
