using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;

namespace HairSlayer
{
    public partial class InviteProvider : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Skip.OnClientClick = "rpxSocial(" + Request.QueryString["id"] + ")";
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataTable dt = Worker.SqlTransaction("SELECT CONCAT(mem.firstname, ' ', mem.lastname, '(', sh.shop_name, ')') as provider FROM member As mem JOIN shop As sh ON mem.idMember = sh.idMember WHERE CONCAT(mem.firstname, ' ', mem.lastname) LIKE '%" + prefixText + "%' ORDER by mem.firstname", System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"].ConnectionString);

            return ToArray(dt);
        }

        private string[] ToArray(DataTable dt)
        {
            var ar = new string[dt.Rows.Count];
            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ar[index] = dr["provider"].ToString();
                index++;
            }

            return ar;
        }

        protected void Invite_Click(object sender, EventArgs e)
        {
            if (isValid() == "")
            {
                string users_name = includes.Functions.GetUserName();
                if (ProviderEmail.Text != "")
                {
                    string email_body = "<p>Congratulations " + ProviderName.Text + ",</p><p>" + users_name + @" has submitted on of your styles to the Rate My Do contest at <a href=""http://www.hairslayer.com"">HairSlayer.com</a></p>" +
                        @"<p><a href=""http://www.hairslayer.com/do.aspx?id=" + Request.QueryString["id"] + @""">Click Here to View Style</a></p>";
                    includes.Functions.SendMail("ratemydo@hairslayer.com", ProviderEmail.Text, users_name + " has just tagged your hairstyle on HairSlayer.com", email_body);
                }

                if (ProviderPhone.Text != "")
                {
                    string text_msg = "Congratulations " + ProviderName.Text + "," + Environment.NewLine + Environment.NewLine + 
                        users_name + " has submitted your style to the Rate My Do contest at HairSlayer.com" + Environment.NewLine + Environment.NewLine +
                        @"<a href=""http://www.hairslayer.com/do.aspx?id=" + Request.QueryString["id"] + @""">Click Here to View Style</a>";
                    //includes.Functions.SendText(ProviderPhone.Text.Replace("-", ""), text_msg);
                }
                pnlComplete.Visible = true;
                pnlInvite.Visible = false;
            }
        }

        protected string isValid()
        {
            string errors = "";
            if (ProviderName.Text == "")
            {
                errors += "Provider Name is required. ";
            }

            if (ProviderEmail.Text != "")
            {
                try
                {
                    MailAddress m = new MailAddress(ProviderEmail.Text);
                }
                catch (FormatException)
                {
                    errors += "Invalid Email. ";
                }
            }

            if (ProviderPhone.Text != "")
            {
                if (ProviderPhone.Text.Replace("-", "").Length != 10)
                {
                    errors += "Phone Number must be exactly 10 digits. ";
                }
                else if (!isDigit(ProviderPhone.Text.Replace("-", "")))
                {
                    errors += "Invalid Phone Number. ";
                }
            }

            return errors;
        }

        protected bool isDigit(string d)
        {
            foreach (char c in d.ToCharArray())
            {
                try
                {
                    var digit = Convert.ToInt16(c);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        protected void Skip_Click(object sender, EventArgs e)
        {
            pnlComplete.Visible = true;
            pnlInvite.Visible = false;
        }
    }
}