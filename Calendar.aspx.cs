using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["hr_main_ck"]["membership"] == "3") { Response.Redirect("Profile.aspx"); }
                System.Data.DataTable dt = Worker.SqlTransaction("SELECT email, password FROM member WHERE idMember = " + Request.Cookies["hr_main_ck"]["user_id"], (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
                Calendar1.Text = @"<iframe src=""script/index.php?controller=Admin&action=login&lrg1=" + dt.Rows[0]["email"] + "&lrg2=" + HairSlayer.includes.Functions.FromBase64(dt.Rows[0]["password"].ToString()) + @""" frameborder=""0"" scrolling=""no"" height=""810"" width=""100%""></iframe>";
            }
        }
    }
}