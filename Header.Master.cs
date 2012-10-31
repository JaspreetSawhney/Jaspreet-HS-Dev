using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using MySql.Data.MySqlClient;

namespace HairSlayer
{
    public partial class Header : System.Web.UI.MasterPage
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            //lblLocation.Text = HostIpToLocation();
            if (!IsPostBack)
            {
                if (HairSlayer.includes.Functions.is_logged_in())
                {
                    logout.Visible = true;
                    login.Visible = false;
                    PopulatePicGrids();
                }
                else
                {
                    logout.Visible = false;
                    login.Visible = true;
                }
            }
            else
            {
                if (Session["pic_grid"] != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)Session["pic_grid"];
                    string img_spot = "", tmp_table = "<ul>";
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                        tmp_table += @"<li><div class=""thumb_wrap""><a href=""do.aspx?id=" + dr["idDo"].ToString() + @""" class=""full-gallery""><div class=""cover_thumbnail""></div><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 150, 150, Convert.ToInt32(dr["idDo"])) + @""" /></div></a></li>";
                    }
                    tmp_table += "</ul>";
                    Faves.Controls.Clear();
                    Faves.Controls.Add(new LiteralControl(tmp_table));
                }
            }
        }

        protected void PopulatePicGrids()
        {
            System.Data.DataTable dt = Worker.SqlTransaction("SELECT dos.idMember, dos.idDo FROM dos JOIN faves on dos.idDo = faves.idDo WHERE faves.idMember = " + Request.Cookies["hr_main_ck"]["user_id"] + " ORDER BY faves.tag_date", connect_string);
            string img_spot = "", tmp_table = "<ul>";
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                tmp_table += @"<li><div class=""thumb_wrap""><a href=""do.aspx?id=" + dr["idDo"].ToString() + @""" class=""full-gallery""><div class=""cover_thumbnail""></div><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 150, 150, Convert.ToInt32(dr["idDo"])) + @""" /></div></a></li>";
            }
            tmp_table += "</ul>";
            Faves.Controls.Clear();
            Faves.Controls.Add(new LiteralControl(tmp_table));
            Session["pic_grid"] = dt;
        }

        protected string HostIpToLocation()
        {
            string sourceIP = string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"])
            ? Request.ServerVariables["REMOTE_ADDR"]
            : Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; 
            string url = "http://api.ipinfodb.com/v2/ip_query.php?key={0}&ip={1}&timezone=true";

            url = String.Format(url, "90d4f5e07ed75a6ed8ad13221d88140001aebf6730eec98978151d2a455d5e95", sourceIP);

            HttpWebRequest httpWRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWResponse = (HttpWebResponse)httpWRequest.GetResponse();

            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
            
            xdoc.Load(httpWResponse.GetResponseStream());

            string city = "", state = "";

            foreach (System.Xml.XmlNode x in xdoc.SelectSingleNode("Response").ChildNodes)
            {
                //Response.Write(x.Name + "<br>");
                if (x.Name == "City") { city = x.InnerText + ", "; }
                if (x.Name == "RegionName") { state = x.InnerText; }
            }

            return city + state;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["Search_Where"] = search_location.Text.Trim().Replace("City, State Or Zip", "");
            Response.Redirect("Search.aspx?id=" + HairSlayer.includes.Functions.ToBase64(looking_for.Text.Trim()));
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //LoginError.Visible = false;
            string sql = "SELECT * FROM member WHERE email = @email AND password = @password AND active = 1";
            MySqlConnection oConn = new MySqlConnection(connect_string);

            oConn.Open();
            MySqlCommand oComm = new MySqlCommand(sql, oConn);
            oComm.Parameters.AddWithValue("@email", email_login.Text.Trim());
            oComm.Parameters.AddWithValue("@password", HairSlayer.includes.Functions.ToBase64(password_login.Text.Trim()));
            MySqlDataReader oDtr = oComm.ExecuteReader();
            if (oDtr.Read())
            {
                Response.Cookies["hr_main_ck"]["user_id"] = oDtr["idMember"].ToString();
                Response.Cookies["hr_main_ck"]["user_name"] = oDtr["firstname"].ToString() + " " + oDtr["lastname"].ToString();
                Response.Cookies["hr_main_ck"]["gender"] = oDtr["gender"].ToString();
                Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                Response.Cookies["hr_main_ck"]["membership"] = oDtr["idMembership"].ToString();
                Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
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
                Response.Redirect("Login.aspx?id=FyQ");
                //LoginError.Text = "Invalid Login.<br />";
                //LoginError.Visible = true;
            }
            oDtr.Close();
            oConn.Close();
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(-10).ToString();
            logout.Visible = false;
            login.Visible = true;
            Response.Redirect("index.aspx");
        }
    }
}