using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class Feats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["hr_main_ck"] != null) { Prov.Value = Request.Cookies["hr_main_ck"]["user_id"]; }
        }

        protected void Month1_Click(object sender, ImageClickEventArgs e)
        {
            string sql = "SELECT idMembership, idMember, CONCAT(firstname, ' ', lastname) AS fname, gender, CONCAT(city, ' ', state) AS location,email, password, latitude, longitude FROM member WHERE idMember = " + Prov.Value;

            System.Data.DataTable dt = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());

            if (dt.Rows.Count > 0)
            {
                Response.Cookies["hr_main_ck"]["user_id"] = dt.Rows[0]["idMember"].ToString();
                Response.Cookies["hr_main_ck"]["user_name"] = dt.Rows[0]["fname"].ToString();
                Response.Cookies["hr_main_ck"]["gender"] = dt.Rows[0]["gender"].ToString();
                Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                string new_member_type = "4";
                if (dt.Rows[0]["idMembership"].ToString() == "2") { new_member_type = "5"; }
                Response.Cookies["hr_main_ck"]["membership"] = new_member_type;
                Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                Response.Cookies["hr_main_ck"]["lat"] = dt.Rows[0]["latitude"].ToString();
                Response.Cookies["hr_main_ck"]["lng"] = dt.Rows[0]["longitude"].ToString();
                Response.Cookies["hr_main_ck"]["validate"] = "false";
                Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);
                Worker.SqlInsert("UPDATE member SET active = 1, idMembership = " + new_member_type + " WHERE idMember = " + Prov.Value, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());

                Response.Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=JX6S9TNKYXQ4C&custom=" + dt.Rows[0]["idMember"].ToString());
            }
        }

        protected void Month6_Click(object sender, ImageClickEventArgs e)
        {
            string sql = "SELECT idMembership, idMember, CONCAT(firstname, ' ', lastname) AS fname, gender, CONCAT(city, ' ', state) AS location,email, password, latitude, longitude FROM member WHERE idMember = " + Prov.Value;

            System.Data.DataTable dt = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());

            if (dt.Rows.Count > 0)
            {
                Response.Cookies["hr_main_ck"]["user_id"] = dt.Rows[0]["idMember"].ToString();
                Response.Cookies["hr_main_ck"]["user_name"] = dt.Rows[0]["fname"].ToString();
                Response.Cookies["hr_main_ck"]["gender"] = dt.Rows[0]["gender"].ToString();
                Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                string new_member_type = "4";
                if (dt.Rows[0]["idMembership"].ToString() == "2") { new_member_type = "5"; }
                Response.Cookies["hr_main_ck"]["membership"] = new_member_type;
                Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                Response.Cookies["hr_main_ck"]["lat"] = dt.Rows[0]["latitude"].ToString();
                Response.Cookies["hr_main_ck"]["lng"] = dt.Rows[0]["longitude"].ToString();
                Response.Cookies["hr_main_ck"]["validate"] = "false";
                Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);
                Worker.SqlInsert("UPDATE member SET active = 1, idMembership = " + new_member_type + " WHERE idMember = " + Prov.Value, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());

                Response.Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NLEGXSTSGC77W&custom=" + dt.Rows[0]["idMember"].ToString());
            }
        }

        protected void Month12_Click(object sender, ImageClickEventArgs e)
        {
            string sql = "SELECT idMembership, idMember, CONCAT(firstname, ' ', lastname) AS fname, gender, CONCAT(city, ' ', state) AS location,email, password, latitude, longitude FROM member WHERE idMember = " + Prov.Value;

            System.Data.DataTable dt = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());

            if (dt.Rows.Count > 0)
            {
                Response.Cookies["hr_main_ck"]["user_id"] = dt.Rows[0]["idMember"].ToString();
                Response.Cookies["hr_main_ck"]["user_name"] = dt.Rows[0]["fname"].ToString();
                Response.Cookies["hr_main_ck"]["gender"] = dt.Rows[0]["gender"].ToString();
                Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                string new_member_type = "4";
                if (dt.Rows[0]["idMembership"].ToString() == "2") { new_member_type = "5"; }
                Response.Cookies["hr_main_ck"]["membership"] = new_member_type;
                Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                Response.Cookies["hr_main_ck"]["lat"] = dt.Rows[0]["latitude"].ToString();
                Response.Cookies["hr_main_ck"]["lng"] = dt.Rows[0]["longitude"].ToString();
                Response.Cookies["hr_main_ck"]["validate"] = "false";
                Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);

                Worker.SqlInsert("UPDATE member SET active = 1, idMembership = " + new_member_type + " WHERE idMember = " + Prov.Value, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());

                Response.Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KKY43AYKRGP6Q&custom=" + dt.Rows[0]["idMember"].ToString());
            }

        }
    }
}