using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class oAk3j_5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ip"] != null && Request.QueryString["coa"] != null)
            {
                string val1 = HairSlayer.includes.Functions.FromBase64(Request.QueryString["ip"]).Replace("%3D", "="), val2 = HairSlayer.includes.Functions.FromBase64(Request.QueryString["coa"]).Replace("%3D", "=");
                string sql = "SELECT idMembership, idMember, CONCAT(firstname, ' ', lastname) AS fname, gender, CONCAT(city, ' ', state) AS location,email, password, latitude, longitude FROM member WHERE idMember = " + val1 + " AND email = '" + val2 + "'";

                System.Data.DataTable dt = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
                //Response.Write(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE member SET active = 1 WHERE idMember = " + val1;
                    
                    Worker.SqlInsert(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
                    Response.Write("<BR><BR><BR><BR><BR><BR><p>You have been authenticated.</p><p>Your browser will be directed shortly...</p>");

                    //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
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
                    //Response.Redirect("Profile.aspx?eid=bmV3YWNj");

                    if (dt.Rows[0]["idMembership"].ToString() == "3")
                    {
                        Response.Redirect("Invite.aspx");
                    }
                    else
                    {
                        sql = "SELECT shop_name FROM shop WHERE idMember = " + dt.Rows[0]["idMember"].ToString();
                        System.Data.DataTable dt2 = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
                        string shop = "";
                        if (dt2.Rows.Count > 0) { shop = dt2.Rows[0]["shop_name"].ToString(); }
                        HairSlayer.includes.Functions.Create_New_Calendar(dt.Rows[0]["idMember"].ToString(), dt.Rows[0]["email"].ToString(), HairSlayer.includes.Functions.FromBase64(dt.Rows[0]["password"].ToString()), shop, dt.Rows[0]["fname"].ToString());
                        Response.Redirect("Profile.aspx?eid=bmV3YWNj");
                    }
                }
            }
        }
    }
}