using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class Invite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtProviderEmail.Attributes["onclick"] = "clearTextBox(this.id)";
                txtProviderName.Attributes["onclick"] = "clearTextBox(this.id)";
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (pnlProvider.Visible)
            {
                pnlProvider.Visible = false;
                pnlFriends.Visible = true;
                btnBack.Visible = true;
                btnNext.Text = "Done";
                Friends.CssClass = "menu-tab2-active";
                Provider.CssClass = "menu-tab2";

                if (txtProviderEmail.Text != "" && txtProviderName.Text != "" && txtProviderEmail.Text != "Barber/Stylist Email" && txtProviderName.Text != "Barber/Stylist Name")
                {
                    InviteProvider();
                }

                /*****CAUSES PAGE TO REDIRECT TO PROFILE AFTER SUBMISSION*****/
                Response.Redirect("Profile.aspx");
            }
            else if (pnlFriends.Visible)
            {
                    Response.Redirect("Profile.aspx");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            btnBack.Visible = false;
            btnNext.Text = "Next >";
            pnlFriends.Visible = false;
            pnlProvider.Visible = true;
            Friends.CssClass = "menu-tab2";
            Provider.CssClass = "menu-tab2-active";
        }

        protected void InviteProvider()
        {
            string body = "<p>Dear " + txtProviderName.Text + ",</p><p>" +
                Request.Cookies["hr_main_ck"]["user_name"] + " has signed up for HairSlayer.com and would like for you to join in his/her experience. </p>" +
                @"<p>Simply click the follow link to connect more of your clients: <br> <a href=""http://www.hairslayer.com/signup.aspx"">http://www.hairslayer.com/signup.aspx</a></p><p>Thank You.</p>";
            HairSlayer.includes.Functions.SendMail("auto-mailer@hairslayer.com", txtProviderEmail.Text.Trim(), "Join " + Request.Cookies["hr_main_ck"]["user_name"] + " On HairSlayer.com", body);
        }

        protected void Skip_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
    }
}