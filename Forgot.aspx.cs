using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class Forgot : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = Worker.SqlTransaction("SELECT password, firstname FROM member WHERE email = '" + ResetEmail.Text.Trim().Replace("<", "").Replace(">", "") + "'", connect_string);
            if (dt.Rows.Count > 0)
            {
                string body = "Hello " + dt.Rows[0]["firstname"] + "!<br><br>We received a request for a password reminder at " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                    "<br><br>Your password is: " + includes.Functions.FromBase64(dt.Rows[0]["password"].ToString()) + @"<br><br>If you did not make this request please notify <a href=""mailto:info@hairslayer.com"">info@hairslayer.com</a> immediately.";
                includes.Functions.SendMail("info@hairslayer.com", ResetEmail.Text.Trim().Replace("<", "").Replace(">", ""), "Your Login Credentials", body);
                EmailError.Text = "Please check your email for further instructions.<br><br>";
                EmailError.Visible = true;
                ResetEmail.Text = "";
            }
            else
            {
                EmailError.Text = "That account does not exist.<br><br>";
                EmailError.Visible = true;
            }
        }
    }
}