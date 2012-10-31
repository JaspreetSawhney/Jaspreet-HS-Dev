<%@ Page Title="Hairslayer.com - Features" Language="C#" MasterPageFile="~/Header.Master"
    AutoEventWireup="true" CodeBehind="Feat.aspx.cs" Inherits="HairSlayer.Feat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="keywords" content="Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
    <div id="paypal_full">
        <center><img src="images/bnr_paymentsBy_150x40.gif" />
        <p class="plan-select">Select a plan below</p>
        <table border="0" align="center">
            <tr>
                <td width="126" valign="top" align="center">$29/Month<br />
                    &nbsp;<br /> 
                    <asp:ImageButton ID="Month1" ImageUrl="images/paypal-buy-now-button.jpg" runat="server" OnClick="Month1_Click" />
                </td>
                <td width="60" valign="middle" align="center">Or</td>
                <td width="126" valign="top" align="center">$139/6 months<br />
                    20% savings
                    <br />
                    <asp:ImageButton ID="Month6" ImageUrl="images/paypal-buy-now-button.jpg" runat="server" OnClick="Month6_Click" />
                </td>
                <td width="60" valign="middle" align="center">Or</td>
                <td width="126" valign="top" align="center">$244/12 months<br />
                    30% savings
                    <br /> 
                    <asp:ImageButton ID="Month12" ImageUrl="images/paypal-buy-now-button.jpg" runat="server" OnClick="Month12_Click" />
                    </td>
            </tr>
        </table>
        <br />
        <img src="images/100guarantee.jpg" width="170" height="170" /></center><br />
        <p id="close_paypal">CLOSE X</p>
    </div>
    <div id="customer-contact">
        <p class="contact-title">Have any awesome ideas you'd like us to add to HairSlayer? Any questions? We'd love to hear from you.</p>
        <p>Please contact us in order to cancel and downgrade your HairSlayer Premium account.</p>
        <center><a href="javascript:void(0);" onclick="olark('api.box.expand')"><img src="images/Contact_US_button.png" style="margin-top: 40px;" /></a></center>
        <p id="customer-contact-close">CLOSE X</p>
    </div>
    <div id="trial-text">
        <h1>
            Try any plan free for 30 days.</h1>
        Cancel at any time. Get more clients today!
    </div>
    <table id="feat-table" class="feature-table" border="0">
        <tr>
            <th>
                Features
            </th>
            <th>
                Basic Plan  Free
            </th>
            <th>
                Premium Plan
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
                <asp:Button ID="Free" runat="server" Text="Free Trial" Visible="false" OnClick="Free_Click" />
                <asp:Button ID="FreeStylist" runat="server" Text="Free Trial" Visible="false" OnClick="Free_Click" />
            </td>
            <td>
                 <a href="#paypal" id="paypal_select"><img src="images/PPeCheck.gif" /></a>
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
    <asp:HiddenField ID="Prov" runat="server" />
</asp:Content>
