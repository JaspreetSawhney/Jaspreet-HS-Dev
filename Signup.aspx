<%@ Page Title="HairSlayer.com Signup" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="HairSlayer.Signup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script language="javascript" src="js/fb.js"></script>
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
    

    
    <div id="main">
    <div id="breadcrumb"><br />Home > Sign Up <asp:Label ID="lblBreadcrumb" runat="server" Text=""></asp:Label></div> 
        <asp:Panel ID="pnlSignUpType" runat="server">
            <asp:Button ID="btnMember" runat="server" CssClass="btn-member" Text="Member" 
                onclick="btnMember_Click" />

            <asp:Button ID="btnProvider" runat="server" CssClass="btn-provider" 
                Text="Barber/Stylist" onclick="btnProvider_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlMember" Visible="false" runat="server">
            <p><asp:Label ID="lblRegistration" CssClass="reg-title" runat="server" Text=""></asp:Label></p>
            <asp:Label ID="DuplicateAccount" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Text="<p>Account already reagistered/email already in use by a member<p>"></asp:Label>
            <asp:Panel ID="pnlUserFacebook" runat="server"><p><a href="#" onclick="reg_user(); return false;"><img src="images/fb-register.gif" alt="Facebook Login" /></a></p>&nbsp;<br /></asp:Panel>
            
            <asp:TextBox ID="txtName" Text="Full Name" CssClass="text-field" runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="reqName" CssClass="sign-error-text" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required."></asp:RequiredFieldValidator> <br />
            <asp:Panel ID="pnlShop" Visible="false" runat="server">
                <asp:TextBox ID="txtShopName" CssClass="text-field" Text="Business Name" runat="server"></asp:TextBox> 
                <asp:TextBox ID="txtSchoolName" CssClass="text-field" Visible="false" Text="School Name" runat="server"></asp:TextBox> <br />
                <asp:DropDownList ID="ddlProviderType" CssClass="text-field" AutoPostBack="true" runat="server" 
                    onselectedindexchanged="ddlProviderType_SelectedIndexChanged">
                    <asp:ListItem Value="">Select Your Specialty</asp:ListItem>
                    <asp:ListItem Value="1">Barber</asp:ListItem>
                    <asp:ListItem Value="2">Stylist</asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>
            <asp:TextBox ID="txtEmail" CssClass="text-field" Text="Email" runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="reqEmail" CssClass="sign-error-text" ControlToValidate="txtEmail" runat="server" ErrorMessage="Email is required."></asp:RequiredFieldValidator> <br />
            <asp:TextBox ID="txtPassword" CssClass="text-field" TextMode="Password"  runat="server"></asp:TextBox>
            <input id="password-clear" class="text-field" type="text" value="Password" />
               <br />
            <div id="disclaimer">
            By clicking the button you agree to the terms below
            <hr />
            These Terms of Service ("Terms") govent your access to and use of the services and Hair Slayers websites (the "Services"), and any information, text, graphics, photos or other materials.
                <hr />
                <asp:Button ID="btnCreateAccount" CssClass="btn-create-account" Visible="false" runat="server" 
                    Text="Create my account" onclick="btnCreateAccount_Click" />
                <asp:Button ID="btnCreateBarberAccount" CssClass="btn-create-account" Visible="false" runat="server" 
                    Text="Create my account" onclick="btnCreateAccount_Click" />
                <asp:Button ID="btnCreateStylistAccount" CssClass="btn-create-account" Visible="false" runat="server" 
                    Text="Create my account" onclick="btnCreateAccount_Click" />
                <asp:Button ID="btnStudentStylist" CssClass="btn-create-account" 
                    Visible="false" runat="server" 
                    Text="Create my account" onclick="btnStudentStylist_Click" />
                <asp:Button ID="btnStudentBarber" CssClass="btn-create-account" Visible="false" runat="server" 
                    Text="Create my account" OnClick="btnStudentStylist_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlInviteProvider" Visible="false" runat="server">
        <p class="reg-title">Invite Your Barber / Stylist</p>
        <asp:TextBox ID="txtProviderName" CssClass="text-field" Text="Barber/Stylist Name" runat="server"></asp:TextBox> <br />
        <asp:TextBox ID="txtProviderEmail" CssClass="text-field" Text="Barber/Stylist Email" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnSkip" CssClass="btn-member" runat="server" Text="Skip" 
                onclick="btnSkip_Click" />
        <asp:Button ID="btnContinue" CssClass="btn-provider" runat="server" 
                Text="Add Barber/Stylist" onclick="btnContinue_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlInviteClients" Visible="false" runat="server">
        
        </asp:Panel>
        <asp:Panel ID="pnlVerification" Visible="false" runat="server">
            <h2 id="sign-verification">You Are Almost Finished</h2>
            <p id="sign-text">A verification email has been sent to your email address.</p>  
            <p id="sign-text">Please click on the activation link in the email in order to 
            activate your account.</p>
        </asp:Panel>
        <asp:HiddenField ID="hdnType" runat="server" />
    </div>

    <div id="sidebar">
        <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
    </div>
</asp:Content>
