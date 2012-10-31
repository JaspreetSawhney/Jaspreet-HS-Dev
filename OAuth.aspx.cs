using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class OAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["z1"] != null)
            {
                if (Request.QueryString["z1"] == "cGF5cGFs")
                {
                    Response.Redirect("Profile.aspx?eid=bmV3YWNj");
                }
                else
                {
                    Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(-10).ToString();
                }
            }
            else
            {
                Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(-10).ToString();
            }
        }
    }
}