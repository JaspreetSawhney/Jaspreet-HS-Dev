using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace HairSlayer
{
    public partial class Index : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
        bool DeleteCleanup = true;

        protected void Page_Init(object sender, EventArgs e)
        {
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (includes.Functions.NewUser()) { Response.Redirect("http://promo.hairslayer.com/vs1"); }
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

            if (Request.Cookies["hairslayer_preference"] == null)
            {
                PopulateAllStyles();
            }
            else
            {
                if (Request.Cookies["hairslayer_preference"].Value == "female")
                {
                    PopulateFemaleStyles();
                }
                else if (Request.Cookies["hairslayer_preference"].Value == "male")
                {
                    PopulateMaleStyles();
                }
                else if (Request.Cookies["hairslayer_preference"].Value == "all")
                {
                    PopulateAllStyles();
                }
            }

        }

        protected void PopulateAllStyles()
        {
            int SEARCH_RADIUS = 51;
            //string[] lat_long = HostIpToGeo();

            /**********************PULL BASED ON DO RATING****************************/
            //string sql = "SELECT DISTINCT a.idDo, b.idMember, shop_name, '' As ln2, b.do_name  FROM do_rating AS a JOIN dos AS b ON a.idDo = b.idDo JOIN shop AS c on b.idStyleOwner = c.idMember";
            /**********************PULL BASED ON DO RATING****************************/

            /**********************PULL BASED ON DO UPLOAD DATE****************************/
            string sql = "SELECT DISTINCT b.idDo, b.idMember, shop_name, '' As ln2, b.do_name  FROM dos AS b JOIN shop AS c on b.idStyleOwner = c.idMember";
            /**********************PULL BASED ON DO UPLOAD DATE****************************/

            //if (MetroArea.SelectedValue != "") { sql += ""; }//" AND "; }
            //sql += " WHERE (sqrt(pow(69.1*(c.latitude-" + lat_long[0] + "),2)+pow(53.0*(c.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ")";
            //sql += " ORDER BY a.overall DESC";
            sql += " ORDER BY b.rmd_date DESC";
            //Label1.Text = sql;
            

            System.Data.DataTable dt = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
            dt.Columns.Add(new DataColumn("RandomNum", Type.GetType("System.Int32")));

            Random random = new Random();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RandomNum"] = random.Next(1000);
            }

            DataView dv = new DataView(dt);
            dv.Sort = "RandomNum";


            /**************************Populate Styles**************************************/
            string style_list = @"<ul id=""style_list"">";
            //foreach (DataRow dr in dt.Rows)
            foreach (DataRowView drv in dv)
            {
                DataRow dr = drv.Row;
                string img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~") + @"images\Users\" + img_spot))
                {
                    style_list += @"<li><div class=""featured_style""><a href=""do.aspx?id=" + dr["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 220, 220, Convert.ToInt32(dr["idDo"])) + @""" border=""0""></a>" +
                            @"<span class=""cover""></span>";

                    if (HairSlayer.includes.Functions.is_logged_in())
                    {
                        style_list += @"<a href=""javascript:oajxp('" + dr["idDo"] + "','" + Request.Cookies["hr_main_ck"]["user_id"] + @"');"" class=""add_style""><img src=""images/add_hs.png"" alt=""""></a>";
                    }
                    else
                    {
                        style_list += @"<a href=""#"" id=""MainBody_LikeStyleNoLog"" class=""add_style""><img src=""images/add_hs.png"" alt=""""></a>";
                    }

                    style_list += @"<a href=""do.aspx?id=" + dr["idDo"] + @"""  class=""view_style"" ><img src=""images/view_hs.png"" alt=""""></a>" +
                            @"<div class=""view_info""></div>" +
                            @"<span class=""style_info"">" +
                            @"<p class=""hair_title"">" + dr["do_name"] + "</p>" +
                            @"<p class=""stylist_name"">" + dr["shop_name"] + "</p>" +
                            "</span>" +
                            "</div>" +
                            "</li>";
                }
                else
                {
                    includes.Functions.SendMail("errors@hairslayer.com", "icunningham@delaritech.com", "Error with Index Style Grid", "Pic Does Not Exist: " + img_spot);
                    try
                    {
                        if (DeleteCleanup) { Worker.DeleteDo(Convert.ToInt32(dr["idDo"])); }
                    }
                    catch
                    { }
                }
            }
            style_list += "</ul>";
            if (dt.Rows.Count > 0)
            {
                StyleGrid.Text = style_list;
            }
            else
            {
                StyleGrid.Text = "There are no styles available in your area.";
            }
        }

        protected void PopulateFemaleStyles()
        {
            int SEARCH_RADIUS = 51;
            //string[] lat_long = HostIpToGeo();

            /**********************PULL BASED ON DO RATING****************************/
            //string sql = "SELECT DISTINCT a.idDo, b.idMember, shop_name, '' As ln2, b.do_name  FROM do_rating AS a JOIN dos AS b ON a.idDo = b.idDo JOIN shop AS c on b.idStyleOwner = c.idMember";
            /**********************PULL BASED ON DO RATING****************************/

            /**********************PULL BASED ON DO UPLOAD DATE****************************/
            string sql = "SELECT DISTINCT b.idDo, b.idMember, shop_name, '' As ln2, b.do_name  FROM dos AS b JOIN shop AS c on b.idStyleOwner = c.idMember JOIN member As mem ON c.idMember = mem.idMember WHERE mem.idMembership IN (2,5)";
            /**********************PULL BASED ON DO UPLOAD DATE****************************/

            //if (MetroArea.SelectedValue != "") { sql += ""; }//" AND "; }
            //sql += " WHERE (sqrt(pow(69.1*(c.latitude-" + lat_long[0] + "),2)+pow(53.0*(c.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ")";
            //sql += " ORDER BY a.overall DESC";
            sql += " ORDER BY b.rmd_date DESC";
            //Label1.Text = sql;


            System.Data.DataTable dt = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
            dt.Columns.Add(new DataColumn("RandomNum", Type.GetType("System.Int32")));

            Random random = new Random();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RandomNum"] = random.Next(1000);
            }

            DataView dv = new DataView(dt);
            dv.Sort = "RandomNum";


            /**************************Populate Styles**************************************/
            string style_list = @"<ul id=""style_list"">";
            //foreach (DataRow dr in dt.Rows)
            foreach (DataRowView drv in dv)
            {
                DataRow dr = drv.Row;
                string img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~") + @"images\Users\" + img_spot))
                {
                    style_list += @"<li><div class=""featured_style""><a href=""do.aspx?id=" + dr["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 220, 220, Convert.ToInt32(dr["idDo"])) + @""" border=""0""></a>" +
                            @"<span class=""cover""></span>";

                    if (HairSlayer.includes.Functions.is_logged_in())
                    {
                        style_list += @"<a href=""javascript:oajxp('" + dr["idDo"] + "','" + Request.Cookies["hr_main_ck"]["user_id"] + @"');"" class=""add_style""><img src=""images/add_hs.png"" alt=""""></a>";
                    }
                    else
                    {
                        style_list += @"<a href=""#"" id=""MainBody_LikeStyleNoLog"" class=""add_style""><img src=""images/add_hs.png"" alt=""""></a>";
                    }

                    style_list += @"<a href=""do.aspx?id=" + dr["idDo"] + @"""  class=""view_style"" ><img src=""images/view_hs.png"" alt=""""></a>" +
                            @"<div class=""view_info""></div>" +
                            @"<span class=""style_info"">" +
                            @"<p class=""hair_title"">" + dr["do_name"] + "</p>" +
                            @"<p class=""stylist_name"">" + dr["shop_name"] + "</p>" +
                            "</span>" +
                            "</div>" +
                            "</li>";
                }
                else
                {
                    includes.Functions.SendMail("errors@hairslayer.com", "icunningham@delaritech.com", "Error with Index Style Grid", "Pic Does Not Exist: " + img_spot);
                    try
                    {
                        if (DeleteCleanup) { Worker.DeleteDo(Convert.ToInt32(dr["idDo"])); }
                    }
                    catch
                    { }
                }
            }
            style_list += "</ul>";
            if (dt.Rows.Count > 0)
            {
                StyleGrid.Text = style_list;
                Response.Cookies["hairslayer_preference"].Value = "female";
                Response.Cookies["hairslayer_preference"].Expires = DateTime.Now.AddYears(1);
            }
            else
            {
                StyleGrid.Text = "There are no styles available in your area.";
            }


        }

        protected void PopulateMaleStyles()
        {
            int SEARCH_RADIUS = 51;
            //string[] lat_long = HostIpToGeo();

            /**********************PULL BASED ON DO RATING****************************/
            //string sql = "SELECT DISTINCT a.idDo, b.idMember, shop_name, '' As ln2, b.do_name  FROM do_rating AS a JOIN dos AS b ON a.idDo = b.idDo JOIN shop AS c on b.idStyleOwner = c.idMember";
            /**********************PULL BASED ON DO RATING****************************/

            /**********************PULL BASED ON DO UPLOAD DATE****************************/
            string sql = "SELECT DISTINCT b.idDo, b.idMember, shop_name, '' As ln2, b.do_name  FROM dos AS b JOIN shop AS c on b.idStyleOwner = c.idMember JOIN member As mem ON c.idMember = mem.idMember WHERE mem.idMembership IN (1,4)";
            /**********************PULL BASED ON DO UPLOAD DATE****************************/

            //if (MetroArea.SelectedValue != "") { sql += ""; }//" AND "; }
            //sql += " WHERE (sqrt(pow(69.1*(c.latitude-" + lat_long[0] + "),2)+pow(53.0*(c.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ")";
            //sql += " ORDER BY a.overall DESC";
            sql += " ORDER BY b.rmd_date DESC";
            //Label1.Text = sql;


            System.Data.DataTable dt = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
            dt.Columns.Add(new DataColumn("RandomNum", Type.GetType("System.Int32")));

            Random random = new Random();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RandomNum"] = random.Next(1000);
            }

            DataView dv = new DataView(dt);
            dv.Sort = "RandomNum";


            /**************************Populate Styles**************************************/
            string style_list = @"<ul id=""style_list"">";
            //foreach (DataRow dr in dt.Rows)
            foreach (DataRowView drv in dv)
            {
                DataRow dr = drv.Row;
                string img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~") + @"images\Users\" + img_spot))
                {
                    style_list += @"<li><div class=""featured_style""><a href=""do.aspx?id=" + dr["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 220, 220, Convert.ToInt32(dr["idDo"])) + @""" border=""0""></a>" +
                            @"<span class=""cover""></span>";

                    if (HairSlayer.includes.Functions.is_logged_in())
                    {
                        style_list += @"<a href=""javascript:oajxp('" + dr["idDo"] + "','" + Request.Cookies["hr_main_ck"]["user_id"] + @"');"" class=""add_style""><img src=""images/add_hs.png"" alt=""""></a>";
                    }
                    else
                    {
                        style_list += @"<a href=""#"" id=""MainBody_LikeStyleNoLog"" class=""add_style""><img src=""images/add_hs.png"" alt=""""></a>";
                    }

                    style_list += @"<a href=""do.aspx?id=" + dr["idDo"] + @"""  class=""view_style"" ><img src=""images/view_hs.png"" alt=""""></a>" +
                            @"<div class=""view_info""></div>" +
                            @"<span class=""style_info"">" +
                            @"<p class=""hair_title"">" + dr["do_name"] + "</p>" +
                            @"<p class=""stylist_name"">" + dr["shop_name"] + "</p>" +
                            "</span>" +
                            "</div>" +
                            "</li>";
                }
                else
                {
                    includes.Functions.SendMail("errors@hairslayer.com", "icunningham@delaritech.com", "Error with Index Style Grid", "Pic Does Not Exist: " + img_spot);
                    try
                    {
                        if (DeleteCleanup) { Worker.DeleteDo(Convert.ToInt32(dr["idDo"])); }
                    }
                    catch
                    { }
                }
            }
            style_list += "</ul>";
            if (dt.Rows.Count > 0)
            {
                StyleGrid.Text = style_list;
                Response.Cookies["hairslayer_preference"].Value = "male";
                Response.Cookies["hairslayer_preference"].Expires = DateTime.Now.AddYears(1);
            }
            else
            {
                StyleGrid.Text = "There are no styles available in your area.";
            }
        }

        protected void PopulatePicGrids()
        {
            /*DataTable dt = new DataTable();
            MySqlConnection oCon = new MySqlConnection(connect_string);
            if (oCon.State == System.Data.ConnectionState.Closed)
            {
                oCon.Open();
            }
            DataSet ds = new DataSet();


            MySqlCommand cmd = new MySqlCommand("sp_PicsFaves", oCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_id", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));

            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            oCon.Close();*/

            DataTable dt = Worker.SqlTransaction("SELECT dos.idMember, dos.idDo FROM dos JOIN faves on dos.idDo = faves.idDo WHERE faves.idMember = " + Request.Cookies["hr_main_ck"]["user_id"] + " ORDER BY faves.tag_date", connect_string);
            string img_spot = "", tmp_table = "<ul>";
            foreach (DataRow dr in dt.Rows)
            {
                img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                tmp_table += @"<li><div class=""thumb_wrap""><a href=""do.aspx?id=" + dr["idDo"].ToString() + @""" class=""full-gallery""><div class=""cover_thumbnail""></div><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 150, 150, Convert.ToInt32(dr["idDo"])) + @""" /></div></a></li>";
            }
            tmp_table += "</ul>";
            Faves.Controls.Clear();
            Faves.Controls.Add(new LiteralControl(tmp_table));
        }

        protected string[] HostIpToGeo()
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

            string zip = "";

            foreach (System.Xml.XmlNode x in xdoc.SelectSingleNode("Response").ChildNodes)
            {
                if (x.Name == "ZipPostalCode") { zip = x.InnerText; }
            }

            /**********GEO CODE ADDRESS*********************/
            string pst = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + zip + "&sensor=false";
            PostSubmitter post = new PostSubmitter(pst);
            post.PostItems = new System.Collections.Specialized.NameValueCollection();

            post.Type = PostSubmitter.PostTypeEnum.Post;
            string out_put = post.Post();
            XmlDocument xmlStuff = new XmlDocument();
            xmlStuff.Load(new System.IO.StringReader(out_put));
            double lat = 0, lng = 0;

            if (xmlStuff.SelectSingleNode("GeocodeResponse").FirstChild.InnerText == "OK")
            {

                foreach (XmlNode xn in xmlStuff.SelectSingleNode("GeocodeResponse").LastChild.ChildNodes)
                {
                    if (xn.Name == "geometry")
                    {
                        lat = Convert.ToDouble(xn.FirstChild.FirstChild.InnerText);
                        lng = Convert.ToDouble(xn.FirstChild.LastChild.InnerText);
                    }
                }
            }
            /**********GEO CODE ADDRESS*********************/

            string[] geo = { "", "" };
            geo[0] = lat.ToString();
            geo[1] = lng.ToString();
            return geo;
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
                Response.Cookies["hr_main_ck"]["lat"] = oDtr["latitude"].ToString();
                Response.Cookies["hr_main_ck"]["lng"] = oDtr["longitude"].ToString();
                Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);
                string mem_type = oDtr["idMembership"].ToString();
                oDtr.Close();
                //Response.Redirect("bad.htm" + mem_type);
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

        protected void MetroArea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["Search_Where"] = search_location.Text.Trim().Replace("City, State Or Zip", "");
            Response.Redirect("Search.aspx?id=" + HairSlayer.includes.Functions.ToBase64(looking_for.Text.Trim()));
        }

        protected void btnDoID_Click(object sender, EventArgs e)
        {
            if (do_id.Value != "") { Worker.SqlInsert("INSERT INTO faves (idMember, idDo) VALUES (" + Request.Cookies["hr_main_ck"]["user_id"] + "," + do_id.Value + ")", connect_string); }
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(-10).ToString();
            logout.Visible = false;
            login.Visible = true;
            Response.Redirect("index.aspx");
        }

        protected void Male_Click(object sender, EventArgs e)
        {
            PopulateMaleStyles();
        }

        protected void MaleButton_Click(object sender, ImageClickEventArgs e)
        {
            PopulateMaleStyles();
        }

        protected void Female_Click(object sender, EventArgs e)
        {
            PopulateFemaleStyles();
        }

        protected void FemaleButton_Click(object sender, ImageClickEventArgs e)
        {
            PopulateFemaleStyles();
        }

        protected void AllStyles_Click(object sender, EventArgs e)
        {
            PopulateAllStyles();
            Response.Cookies["hairslayer_preference"].Value = "all";
            Response.Cookies["hairslayer_preference"].Expires = DateTime.Now.AddYears(1);
        }
    }
}