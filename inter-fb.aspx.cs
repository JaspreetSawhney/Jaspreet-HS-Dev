using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class inter_fb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["em"] != null)
            {
                /*var client = new Facebook.FacebookClient(Request.QueryString["access_token"]);
                dynamic me = client.Get("me");
                string firstName = me.first_name;
                string lastName = me.last_name;
                string email = me.email;*/

                System.Data.DataTable dt = Worker.SqlTransaction("SELECT idMembership, idMember, CONCAT(firstname, ' ', lastname) AS fname, gender, CONCAT(city, ' ', state) AS location, latitude, longitude FROM member WHERE active = 1 AND email = '" + Request.QueryString["em"] + "'", (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
                if (dt.Rows.Count > 0)
                {
                    Response.Cookies["hr_main_ck"]["user_id"] = dt.Rows[0]["idMember"].ToString();
                    Response.Cookies["hr_main_ck"]["user_name"] = dt.Rows[0]["fname"].ToString();
                    Response.Cookies["hr_main_ck"]["gender"] = dt.Rows[0]["gender"].ToString();
                    Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                    Response.Cookies["hr_main_ck"]["membership"] = dt.Rows[0]["idMembership"].ToString();
                    Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                    Response.Cookies["hr_main_ck"]["lat"] = dt.Rows[0]["latitude"].ToString();
                    Response.Cookies["hr_main_ck"]["lng"] = dt.Rows[0]["longitude"].ToString();
                    Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                    Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);

                    if (dt.Rows[0]["idMembership"].ToString() != "3")
                    {
                        Response.Redirect("Profile.aspx?id=YmFyQmVy");
                    }
                    else
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx?jd=e01");
                }
            }
        }
    }
}