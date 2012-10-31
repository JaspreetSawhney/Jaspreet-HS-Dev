using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class xchange : System.Web.UI.Page
    {
        string user_email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Collections.Specialized.NameValueCollection nvc = Request.Form;

            string token = nvc["token"];

            Rpx xch = new Rpx("aab10eafde9a29f011760907b4a387f03ca560f1", "https://rpxnow.com");
            XmlElement xelement = xch.AuthInfo(token);
            foreach (XmlNode x1 in xelement.ChildNodes)
            {
                Traverse_Nodes(x1);
            }
            System.Data.DataTable dt = Worker.SqlTransaction("SELECT idMembership, idMember, CONCAT(firstname, ' ', lastname) AS fname, gender, CONCAT(city, ' ', state) AS location, latitude, longitude FROM member WHERE active = 1 AND email = '" + user_email + "'", (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
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
                Response.Redirect("Signup.aspx");
            }
        }

        protected void Traverse_Nodes(XmlNode target)
        {
            if (target.Name == "email")
            {
                user_email = target.InnerText;
            }
            else
            {
                if (target.HasChildNodes)
                {
                    foreach (XmlNode x2 in target.ChildNodes)
                    {
                        Traverse_Nodes(x2);
                    }                    
                }
            }
        }
    }
}