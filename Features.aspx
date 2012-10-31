<%@ Page Title="Hairslayer.com - Features" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Features.aspx.cs" Inherits="HairSlayer.Features" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="ThumbGrid" Src="~/UserControls/ThumbGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="keywords" content="Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
    <div id="customer-contact">
        <p class="contact-title">Have any awesome ideas you'd like us to add to HairSlayer? Any questions? We'd love to hear from you.</p>
        <p>Please contact us in order to cancel and downgrade your HairSlayer Premium account.</p>
        <center><a href="javascript:void(0);" onclick="olark('api.box.expand')"><img src="images/Contact_US_button.png" style="margin-top: 40px;" /></a></center>
        <p id="customer-contact-close">CLOSE X</p>
    </div>
    <h2 class="page-back">&lt; <a href="javascript:history.go(-1)">Back</a></h2>
    <div id="trial-text">
        <h1>
            Try any plan free for 30 days.</h1>
        Cancel at any time. Get more clients today!
    </div>
    <table id="feat-table" align="center" class="feature-table" border="0">
        <tr>
            <th>
                Features
            </th>
            <th>
                Basic Plan  Free
            </th>
            <th>
                Premium Plan - $29/Month
            </th>
        </tr>
        <tr>
            <td>
                Contact info:
            </td>
            <td>
                Address with Google Map
            </td>
            <td>
                Address with Google Map<br />
                Phone <br />
                Bio
            </td>
        </tr>
        <tr>
            <td>
                Google Maps & Directions
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                Services Offered List
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                Client Reviews
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                Work Hours
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                Online Portfolio
            </td>
            <td>
                3 Styles
            </td>
            <td>
                Unlimited Styles
            </td>
        </tr>
        <tr>
            <td>
                Online & Mobile Appointments
            </td>
            <td>
                <img src="images/x.png" />
            </td>
            <td>
                Email and Text message notifications
            </td>
        </tr>
        <tr>
            <td>
                Facebook and Twitter Page Links
            </td>
            <td>
                <img src="images/x.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                Respond to Client Reviews
            </td>
            <td>
                <img src="images/x.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                Pre-Payments for Appointments
            </td>
            <td>
                <img src="images/x.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                Client Referral Tracking
            </td>
            <td>
                <img src="images/x.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                Customer Loyalty Program
            </td>
            <td>
                <img src="images/x.png" />
            </td>
            <td>
                <img src="images/feat-chk.png" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <a href="signup.aspx?id=1" class="feature-link-button">Sign Up</a>
            </td>
            <td>
                <a href="signup.aspx?id=1" class="feature-link-button">Sign Up</a>
            </td>
        </tr>
    </table>

    <table id="benefits" class="benefits-table" border="0">
        <tr>
            <td colspan="3" align="center"><h1 class="benh1">Benefits</h1></td>
        </tr>
        <tr>
            <td align="left" valign="top" width="33%">
                <div id="benefits-title">
                    Promote Yourself</div>
                <p>Showcase your skills and talents! Promote yourself to a community that is totally
                interested in hair and looking for talented barbers/hairstylist like YOU!</p>
            </td>
            <td align="left" valign="top" width="33%">
                <div id="benefits-title">
                    Have Your Own Mini-Website</div>
                <ul class="benefits-list">
                    <li>Online Portfolio</li>
                    <li>Gather Customer Reviews</li>
                    <li>Schedule Appointments</li>
                    <li>Reward Customers</li>
                    <li>Social Mobile Marketing</li>
                    <li>Email Marketing</li>
                </ul>
            </td>
            <td align="left" valign="top" width="34%">
                <div id="benefits-title">
                    Attract More Clients</div>
                <p>Attract more clients with social game: Rate My Do! Have community rate your styles.
                Win the "Do of the Day" competition and be featured on our homepage, Facebook, Twitter
                and Google</p>
            </td>
        </tr>
    </table>
    <div id="faq-title">
        Some Frequently Asked Questions:
    </div>
    <div id="faq">
        <p>
            <h1>
                1. Can I change plans at any time?</h1>
            Changing plans is really simple. You can upgrade or downgrade your plan at any time.
            If you are downgrading from a plan that offers extra features (i.e. Multiple Styles)
            you may have to remove some of those extras before downgrading.</p>
        <p>
            <h1>
                2. How does the 30-day free trial work?</h1>
            If you cancel your account within 30 days of signing up your credit card will not be billed. 
                You can cancel your premium account at any time by simply <a href="#contact" id="customer-contact-us" style="color: #FF0000;">Contact Us</a> if you’d like to cancel your premium account.</p>
        <p>
            <h1>
                3. Is there a minimum commitment?</h1>
            Hair Slayer is a month to month service. There is no contract or long term obligation.
            You are billed on a monthly basis, and if you cancel you will not be billed again.</p>
        <p>
            <h1>
                4. What are my payment options?</h1>
            We currently accept Visa and Mastercard through pay via PayPal.</p>
        <p>
            <h1>
                5. How reliable is your service?</h1>
            Hair Slayer hosts all of your pages on Rackspace Managed Cloud and we do regular
            (hourly) backups of all your data. Optimized for mobile browsing.</p>
        <p>
            <h1>
                6. Where can I review the terms of service and privacy policy?</h1>
            Here are our <a href="terms.aspx">Terms of Service</a> and <a href="pp.aspx">Privacy
                Policy</a>.</p>
    </div>
    <h2 class="page-back">&lt; <a href="javascript:history.go(-1)">Back</a></h2>
    <asp:HiddenField ID="Prov" runat="server" />
</asp:Content>
