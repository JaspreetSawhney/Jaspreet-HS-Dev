
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="HairSlayer.UserControls.Footer" %>


<div id="footer">

<div id="footer_nav"> 
  <!-- <a href="#">Contact</a> 
  <span style="color:#999;">|</span> -->
     <%Response.Write(DateTime.Now.Year); %> &copy; Hairslayer.com -  <a href="about.aspx">About Hairslayer</a> | <a href="Terms.aspx">Terms</a> | <a href="PP.aspx">Privacy Policy</a> | <a href="http://hairslayer.totemapp.com/company" target="_blank">Press Page</a>

</div>

</div><!--ends the footer div-->
<div class="cleared"></div>
