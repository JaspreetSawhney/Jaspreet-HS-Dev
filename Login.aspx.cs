using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace HairSlayer
{
    public partial class Login : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["id"] == "FyQ") 
                {
                    Error.Text = "Invalid Login.<br />";
                    Error.Visible = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Error.Visible = false;
           // string sql = "SELECT * FROM member WHERE email = @email AND password = @password";
            string sql = "select * from member where email='jaspreets.krishnais@gmail.com' and password='MTIzNDU2'";
            MySqlConnection oConn = new MySqlConnection(connect_string);

            oConn.Open();
            MySqlCommand oComm = new MySqlCommand(sql, oConn);
            oComm.Parameters.AddWithValue("@email", User.Text.Trim());
            string pass = HairSlayer.includes.Functions.ToBase64(Pass.Text.Trim());
            oComm.Parameters.AddWithValue("@password", pass);
            MySqlDataReader oDtr = oComm.ExecuteReader();
            if (oDtr.Read())
            {
                Response.Cookies["hr_main_ck"]["user_id"] = oDtr["idMember"].ToString();
                Response.Cookies["hr_main_ck"]["user_name"] = oDtr["firstname"].ToString() + " " + oDtr["lastname"].ToString();
                Response.Cookies["hr_main_ck"]["gender"] = oDtr["gender"].ToString();
                Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                Response.Cookies["hr_main_ck"]["membership"] = oDtr["idMembership"].ToString();
                Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                Response.Cookies["hr_main_ck"]["lat"] = oDtr["latitude"].ToString();
                Response.Cookies["hr_main_ck"]["lng"] = oDtr["longitude"].ToString();
                Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);
                string mem_type = oDtr["idMembership"].ToString();
                oDtr.Close();

                if (mem_type != "3")
                {
                    Response.Redirect("Profile.aspx?eid=YmFyQmVy");
                }
                else
                {
                    Response.Redirect("Index.aspx");
                }
            }
            else
            {
                Error.Text = "Invalid Login.<br />";
                Error.Visible = true;
            }
            oDtr.Close();
            oConn.Close();
        }
    }
}