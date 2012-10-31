using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Net;
using System.Xml;
using System.Data;

namespace HairSlayer
{
    public partial class Search : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
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

                if (Request.QueryString["id"] != null)
                {
                    looking_for.Text = HairSlayer.includes.Functions.FromBase64(Request.QueryString["id"]);
                    if (Session["Search_Where"] != null) { search_location.Text = Session["Search_Where"].ToString(); }
                    Session["Search_Where"] = "";
                    AutoSearch();
                }
            }
        }

        protected void showFilters(bool on_off)
        {
            AllStyles.Visible = on_off;
            Male.Visible = on_off;
            //MaleButton.Visible = on_off;
            Female.Visible = on_off;
            //FemaleButton.Visible = on_off;
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
        }

        protected string[] HostIpToGeo()
        {

            /**********GEO CODE ADDRESS*********************/
            string pst = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + search_location.Text.Trim().Replace("<", "").Replace(">", "") + "&sensor=false";
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

        protected void AutoSearch()
        {
            looking_for.Text = looking_for.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'");
            bool what_search = true;
            
            //string sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE a.do_name LIKE '%" + txtSearch.Text + "%' OR c.Tag LIKE '%" + txtSearch.Text + "%' ";
            string sql = "";
            if (looking_for.Text.Replace("What are you looking for?", "") == "" && search_location.Text.Trim().Replace("City, State or Zip", "") != "")
            {
                sql = GetWhereSQLQuery();
                /*sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE ";
                int SEARCH_RADIUS = 51;
                string[] lat_long = HostIpToGeo();
                sql += " (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                what_search = false;
            }
            else
            {
                //sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE (a.do_name LIKE '%" + looking_for.Text + "%' OR c.Tag LIKE '%" + looking_for.Text + "%' OR d.shop_name LIKE '%" + looking_for.Text + "%') ";
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember LEFT JOIN member as mem ON a.idStyleOwner = mem.idMember WHERE (CONCAT(mem.firstname, ' ', mem.lastname) LIKE '%" + looking_for.Text + "%' OR a.do_name LIKE '%" + looking_for.Text + "%' OR c.Tag LIKE '%" + looking_for.Text + "%' OR d.shop_name LIKE '%" + looking_for.Text + "%')";
                if (search_location.Text.Trim().Replace("City, State or Zip", "") != "")
                {
                    sql += GetWhereWhatSQLQuery();
                    /*int SEARCH_RADIUS = 51;
                    string[] lat_long = HostIpToGeo();
                    sql += " AND (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                }
                else
                {
                    search_location.Text = "City, State or Zip";
                }
            }
             sql +=  "  ORDER BY overall DESC";

            //string sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN do_rating As b ON a.idDo = b.idDo JOIN do_descriptions As c ON a.idDo = c.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE a.do_name LIKE '%" + txtSearch.Text + "%' OR c.Tag LIKE '%" + txtSearch.Text + "%'  ORDER BY overall DESC";
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            if (dt.Rows.Count > 0)
            {
                showFilters(true);
                //SearchGrid.DataSource = dt;
                //SearchGrid.DataBind();
                string temp_res = "";
                foreach (DataRow dr in dt.Rows)
                {
                    temp_res += @"<div class=""search_result""> " + Environment.NewLine + 
                    @"<div class=""search_thumb_cover""></div>" + Environment.NewLine + 
                    @"<a href=""do.aspx?id=" + dr["idDo"].ToString() + @"""><img src=""images/Users/" + dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + @".jpg"" alt="""" height=""120"" width=""120"" class=""search_thumb""></a>" + Environment.NewLine +
	                "<h1>" + dr["do_name"].ToString() + @"</h1>" + Environment.NewLine +
                    @"<h2>Style by: <a href=""prov.aspx?id=" + dr["idStyleOwner"].ToString() + @""">" + dr["shop_name"].ToString() + "</a></h2>" + Environment.NewLine + 
			        @"<div class=""rating_search"">" + Environment.NewLine +  
                    @"<h3>Rating:</h3>" + Environment.NewLine + HairSlayer.includes.Functions.RateIcon(dr["overall"].ToString(), dr["gender"].ToString()) + Environment.NewLine + 
                    "</div>" + Environment.NewLine + "</div>" + Environment.NewLine;
                }
                
                SearchGrid.Controls.Add(new LiteralControl(temp_res));
                //Search_Grid.Text = temp_res;
            }
            if (what_search)
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            }
            else
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + search_location.Text.Trim() + @"""";

            }
            RowsReturned.Visible = true;
        }

        protected Dictionary<string, string> state_sub()
        {
            Dictionary<string, string> state_sub = new Dictionary<string, string>();
            state_sub.Add("Alabama", "AL");
            state_sub.Add("Arizona", "AZ");
            state_sub.Add("Alaska", "AK");
            state_sub.Add("Arkansas", "AR");
            state_sub.Add("California", "CA");
            state_sub.Add("Colorado", "CO");
            state_sub.Add("Connecticut", "CT");
            state_sub.Add("Delaware", "DE");
            state_sub.Add("District of Columbia", "DC");
            state_sub.Add("Florida", "FL");
            state_sub.Add("Georgia", "GA");
            state_sub.Add("Hawaii", "HI");
            state_sub.Add("Idaho", "ID");
            state_sub.Add("Illinois", "IL");
            state_sub.Add("Indiana", "IN");
            state_sub.Add("Iowa", "IA");
            state_sub.Add("Kansas", "KS");
            state_sub.Add("Kentucky", "KY");
            state_sub.Add("Louisiana", "LA");
            state_sub.Add("Maine", "ME");
            state_sub.Add("Maryland", "MD");
            state_sub.Add("Massachusetts", "MA");
            state_sub.Add("Michigan", "MI");
            state_sub.Add("Minnesota", "MN");
            state_sub.Add("Mississippi", "MS");
            state_sub.Add("Missouri", "MO");
            state_sub.Add("Montana", "MT");
            state_sub.Add("Nebraska", "NE");
            state_sub.Add("Nevada", "NV");
            state_sub.Add("New Hampshire", "NH");
            state_sub.Add("New Jersey", "NJ");
            state_sub.Add("New Mexico", "NM");
            state_sub.Add("New York", "NY");
            state_sub.Add("North Carolina", "NC");
            state_sub.Add("North Dakota", "ND");
            state_sub.Add("Ohio", "OH");
            state_sub.Add("Oklahoma", "OK");
            state_sub.Add("Oregon", "OR");
            state_sub.Add("Pennsylvania", "PA");
            state_sub.Add("Puerto Rico", "PR");
            state_sub.Add("Rhode Island", "RI");
            state_sub.Add("South Carolina", "SC");
            state_sub.Add("South Dakota", "SD");
            state_sub.Add("Tennessee", "TN");
            state_sub.Add("Texas", "TX");
            state_sub.Add("Utah", "UT");
            state_sub.Add("Vermont", "VT");
            state_sub.Add("Virginia", "VA");
            state_sub.Add("Washington", "WA");
            state_sub.Add("West Virginia", "WV");
            state_sub.Add("Wisconsin", "WI");
            state_sub.Add("Wyoming", "WY");

            return state_sub;
        }

        protected string GetWhereSQLQuery()
        {
            int tmpx = 0;
            Dictionary<string, string> sub_strings = state_sub();
            string [] state_list = { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", 
                    "South Dakota ", "Tennessee ", "Texas ", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
            string sql = "", where = search_location.Text.Trim();

            if (sub_strings.ContainsKey(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(where.ToLower())))
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE d.state = '" + sub_strings[System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(where.ToLower())] + "'";
            }
            else if (sub_strings.ContainsValue(where.ToUpper()))
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE d.state = '" + where.ToUpper() + "'";
            }
            else if (!int.TryParse(where.Trim(), out tmpx) && !where.Contains(","))
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE d.city LIKE '%" + where + "%'";
            }
            else
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE ";
                int SEARCH_RADIUS = 120;
                string[] lat_long = HostIpToGeo();
                sql += " (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";
            }
            return sql;
        }

        protected string GetWhereSQLQuery(string gender)
        {
            int tmpx = 0;
            string gender_sql = "";
            if (gender == "male")
            {
                gender_sql = " a.Gender = 'M' AND";
            }
            if (gender == "female")
            {
                gender_sql = " a.Gender = 'F' AND";
            }

            Dictionary<string, string> sub_strings = state_sub();
            string[] state_list = { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", 
                    "South Dakota ", "Tennessee ", "Texas ", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
            string sql = "", where = search_location.Text.Trim();

            if (sub_strings.ContainsKey(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(where.ToLower())))
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE" + gender_sql + " d.state = '" + sub_strings[System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(where.ToLower())] + "'";
            }
            else if (sub_strings.ContainsValue(where.ToUpper()))
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE" + gender_sql + " d.state = '" + where.ToUpper() + "'";
            }
            else if (!int.TryParse(where.Trim(), out tmpx) && !where.Contains(","))
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE" + gender_sql + " d.city LIKE '%" + where + "%'";
            }
            else
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE" + gender_sql + " ";
                int SEARCH_RADIUS = 120;
                string[] lat_long = HostIpToGeo();
                sql += " (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";
            }
            return sql;
        }

        protected string GetWhereWhatSQLQuery()
        {
            Dictionary<string, string> sub_strings = state_sub();
            string[] state_list = { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", 
                    "South Dakota ", "Tennessee ", "Texas ", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
            string sql = "", where = search_location.Text.Trim();

            if (sub_strings.ContainsKey(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(where.ToLower())))
            {
                sql = " AND d.state = '" + sub_strings[System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(where.ToLower())] + "'";
            }
            else if (sub_strings.ContainsValue(where.ToUpper()))
            {
                sql = "AND d.state = '" + where.ToUpper() + "'";
            }
            else
            {
                int SEARCH_RADIUS = 51;
                string[] lat_long = HostIpToGeo();
                sql += " AND (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";
            }
            return sql;
        }

        protected string GetWhereWhatSQLQuery(string gender)
        {
            string gender_sql = "";
            if (gender == "male")
            {
                gender_sql = " a.Gender = 'M' AND";
            }
            if (gender == "female")
            {
                gender_sql = " a.Gender = 'F' AND";
            }
            Dictionary<string, string> sub_strings = state_sub();
            string[] state_list = { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", 
                    "South Dakota ", "Tennessee ", "Texas ", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
            string sql = "", where = search_location.Text.Trim();

            if (sub_strings.ContainsKey(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(where.ToLower())))
            {
                sql = gender_sql + " AND d.state = '" + sub_strings[System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(where.ToLower())] + "'";
            }
            else if (sub_strings.ContainsValue(where.ToUpper()))
            {
                sql = gender_sql + "AND d.state = '" + where.ToUpper() + "'";
            }
            else
            {
                int SEARCH_RADIUS = 51;
                string[] lat_long = HostIpToGeo();
                sql += gender_sql + " AND (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";
            }
            return sql;
        }

        protected void btnLogout_Click(object sender, ImageClickEventArgs e)
        {
            Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(-10).ToString();
            logout.Visible = false;
            login.Visible = true;
            Response.Redirect("index.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool what_search = true;
            looking_for.Text = looking_for.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'");
            string sql = "";
            if (looking_for.Text.Replace("What are you looking for?", "") == "" && search_location.Text.Trim().Replace("City, State or Zip", "") != "")
            {
                sql = GetWhereSQLQuery();
                /*sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE ";
                int SEARCH_RADIUS = 51;
                string[] lat_long = HostIpToGeo();
                sql += " (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                what_search = false;
            }
            else
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember LEFT JOIN member as mem ON a.idStyleOwner = mem.idMember WHERE (CONCAT(mem.firstname, ' ', mem.lastname) LIKE '%" + looking_for.Text + "%' OR a.do_name LIKE '%" + looking_for.Text + "%' OR c.Tag LIKE '%" + looking_for.Text + "%' OR d.shop_name LIKE '%" + looking_for.Text + "%')";
                if (search_location.Text.Trim().Replace("City, State or Zip", "") != "")
                {
                    sql += GetWhereWhatSQLQuery();
                    /*int SEARCH_RADIUS = 51;
                    string[] lat_long = HostIpToGeo();
                    sql += " AND (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                }
            }

            sql += " ORDER BY overall DESC";
            //Label1.Text = sql;
            //Label1.Visible = true;
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            if (dt.Rows.Count > 0)
            {
                showFilters(true);
                string temp_res = "";
                foreach (DataRow dr in dt.Rows)
                {
                    temp_res += @"<div class=""search_result""> " + Environment.NewLine +
                    @"<div class=""search_thumb_cover""></div>" + Environment.NewLine +
                    @"<a href=""do.aspx?id=" + dr["idDo"].ToString() + @"""><img src=""images/Users/" + dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + @".jpg"" alt="""" height=""120"" width=""120"" class=""search_thumb""></a>" + Environment.NewLine +
                    "<h1>" + dr["do_name"].ToString() + @"</h1>" + Environment.NewLine +
                    @"<h2>Style by: <a href=""prov.aspx?id=" + dr["idStyleOwner"].ToString() + @""">" + dr["shop_name"].ToString() + "</a></h2>" + Environment.NewLine +
                    @"<div class=""rating_search"">" + Environment.NewLine +
                    @"<h3>Rating:</h3>" + Environment.NewLine + HairSlayer.includes.Functions.RateIcon(dr["overall"].ToString(), dr["gender"].ToString()) + Environment.NewLine +
                    "</div>" + Environment.NewLine + "</div>" + Environment.NewLine;
                }

                SearchGrid.Controls.Add(new LiteralControl(temp_res));
            }
            //RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            if (what_search)
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            }
            else
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + search_location.Text.Trim() + @"""";
            }
            RowsReturned.Visible = true;
        }

        protected void PopulateAllStyles()
        {
            bool what_search = true;
            looking_for.Text = looking_for.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'");
            string sql = "";
            if (looking_for.Text.Replace("What are you looking for?", "") == "" && search_location.Text.Trim().Replace("City, State or Zip", "") != "")
            {
                sql = GetWhereSQLQuery();
                /*sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE ";
                int SEARCH_RADIUS = 51;
                string[] lat_long = HostIpToGeo();
                sql += " (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                what_search = false;
            }
            else
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember LEFT JOIN member as mem ON a.idStyleOwner = mem.idMember WHERE (CONCAT(mem.firstname, ' ', mem.lastname) LIKE '%" + looking_for.Text + "%' OR a.do_name LIKE '%" + looking_for.Text + "%' OR c.Tag LIKE '%" + looking_for.Text + "%' OR d.shop_name LIKE '%" + looking_for.Text + "%')";
                if (search_location.Text.Trim().Replace("City, State or Zip", "") != "")
                {
                    sql += GetWhereWhatSQLQuery();
                    /*int SEARCH_RADIUS = 51;
                    string[] lat_long = HostIpToGeo();
                    sql += " AND (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                }
            }

            sql += " ORDER BY overall DESC";

            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            if (dt.Rows.Count > 0)
            {
                string temp_res = "";
                foreach (DataRow dr in dt.Rows)
                {
                    temp_res += @"<div class=""search_result""> " + Environment.NewLine +
                    @"<div class=""search_thumb_cover""></div>" + Environment.NewLine +
                    @"<a href=""do.aspx?id=" + dr["idDo"].ToString() + @"""><img src=""images/Users/" + dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + @".jpg"" alt="""" height=""120"" width=""120"" class=""search_thumb""></a>" + Environment.NewLine +
                    "<h1>" + dr["do_name"].ToString() + @"</h1>" + Environment.NewLine +
                    @"<h2>Style by: <a href=""prov.aspx?id=" + dr["idStyleOwner"].ToString() + @""">" + dr["shop_name"].ToString() + "</a></h2>" + Environment.NewLine +
                    @"<div class=""rating_search"">" + Environment.NewLine +
                    @"<h3>Rating:</h3>" + Environment.NewLine + HairSlayer.includes.Functions.RateIcon(dr["overall"].ToString(), dr["gender"].ToString()) + Environment.NewLine +
                    "</div>" + Environment.NewLine + "</div>" + Environment.NewLine;
                }

                SearchGrid.Controls.Add(new LiteralControl(temp_res));
            }
            //RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            if (what_search)
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            }
            else
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + search_location.Text.Trim() + @"""";
            }
            RowsReturned.Visible = true;
        }

        protected void PopulateFemaleStyles()
        {
            bool what_search = true;
            looking_for.Text = looking_for.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'");
            string sql = "";
            if (looking_for.Text.Replace("What are you looking for?", "") == "" && search_location.Text.Trim().Replace("City, State or Zip", "") != "")
            {
                sql = GetWhereSQLQuery("female");
                /*sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE ";
                int SEARCH_RADIUS = 51;
                string[] lat_long = HostIpToGeo();
                sql += " (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                what_search = false;
            }
            else
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember LEFT JOIN member as mem ON a.idStyleOwner = mem.idMember WHERE mem.idMembership IN (2,5) AND (CONCAT(mem.firstname, ' ', mem.lastname) LIKE '%" + looking_for.Text + "%' OR a.do_name LIKE '%" + looking_for.Text + "%' OR c.Tag LIKE '%" + looking_for.Text + "%' OR d.shop_name LIKE '%" + looking_for.Text + "%')";
                if (search_location.Text.Trim().Replace("City, State or Zip", "") != "")
                {
                    sql += GetWhereWhatSQLQuery("female");
                    /*int SEARCH_RADIUS = 51;
                    string[] lat_long = HostIpToGeo();
                    sql += " AND (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                }
            }

            sql += " ORDER BY overall DESC";
            //Label1.Text = sql;
            //Label1.Visible = true;
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            if (dt.Rows.Count > 0)
            {
                string temp_res = "";
                foreach (DataRow dr in dt.Rows)
                {
                    temp_res += @"<div class=""search_result""> " + Environment.NewLine +
                    @"<div class=""search_thumb_cover""></div>" + Environment.NewLine +
                    @"<a href=""do.aspx?id=" + dr["idDo"].ToString() + @"""><img src=""images/Users/" + dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + @".jpg"" alt="""" height=""120"" width=""120"" class=""search_thumb""></a>" + Environment.NewLine +
                    "<h1>" + dr["do_name"].ToString() + @"</h1>" + Environment.NewLine +
                    @"<h2>Style by: <a href=""prov.aspx?id=" + dr["idStyleOwner"].ToString() + @""">" + dr["shop_name"].ToString() + "</a></h2>" + Environment.NewLine +
                    @"<div class=""rating_search"">" + Environment.NewLine +
                    @"<h3>Rating:</h3>" + Environment.NewLine + HairSlayer.includes.Functions.RateIcon(dr["overall"].ToString(), dr["gender"].ToString()) + Environment.NewLine +
                    "</div>" + Environment.NewLine + "</div>" + Environment.NewLine;
                }

                SearchGrid.Controls.Add(new LiteralControl(temp_res));
                Response.Cookies["hairslayer_preference"].Value = "female";
                Response.Cookies["hairslayer_preference"].Expires = DateTime.Now.AddYears(1);
            }
            //RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            if (what_search)
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            }
            else
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + search_location.Text.Trim() + @"""";
            }
            RowsReturned.Visible = true;
        }

        protected void PopulateMaleStyles()
        {
            bool what_search = true;
            looking_for.Text = looking_for.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'");
            string sql = "";
            if (looking_for.Text.Replace("What are you looking for?", "") == "" && search_location.Text.Trim().Replace("City, State or Zip", "") != "")
            {
                sql = GetWhereSQLQuery("male");
                /*sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember WHERE ";
                int SEARCH_RADIUS = 51;
                string[] lat_long = HostIpToGeo();
                sql += " (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                what_search = false;
            }
            else
            {
                sql = "SELECT DISTINCT overall, a.idDo, a.idMember, a.do_name, d.shop_name, a.Gender, a.idStyleOwner FROM dos As a JOIN  do_descriptions As c ON a.idDo = c.idDo LEFT JOIN do_rating As b ON a.idDo = b.idDo LEFT JOIN shop As d ON a.idStyleOwner = d.idMember LEFT JOIN member as mem ON a.idStyleOwner = mem.idMember WHERE mem.idMembership IN (1,4) AND (CONCAT(mem.firstname, ' ', mem.lastname) LIKE '%" + looking_for.Text + "%' OR a.do_name LIKE '%" + looking_for.Text + "%' OR c.Tag LIKE '%" + looking_for.Text + "%' OR d.shop_name LIKE '%" + looking_for.Text + "%')";
                if (search_location.Text.Trim().Replace("City, State or Zip", "") != "")
                {
                    sql += GetWhereWhatSQLQuery("male");
                    /*int SEARCH_RADIUS = 51;
                    string[] lat_long = HostIpToGeo();
                    sql += " AND (sqrt(pow(69.1*(d.latitude-" + lat_long[0] + "),2)+pow(53.0*(d.longitude-" + lat_long[1] + "),2))<" + SEARCH_RADIUS + ") ";*/
                }
            }

            sql += " ORDER BY overall DESC";
            //Label1.Text = sql;
            //Label1.Visible = true;
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            if (dt.Rows.Count > 0)
            {
                string temp_res = "";
                foreach (DataRow dr in dt.Rows)
                {
                    temp_res += @"<div class=""search_result""> " + Environment.NewLine +
                    @"<div class=""search_thumb_cover""></div>" + Environment.NewLine +
                    @"<a href=""do.aspx?id=" + dr["idDo"].ToString() + @"""><img src=""images/Users/" + dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + @".jpg"" alt="""" height=""120"" width=""120"" class=""search_thumb""></a>" + Environment.NewLine +
                    "<h1>" + dr["do_name"].ToString() + @"</h1>" + Environment.NewLine +
                    @"<h2>Style by: <a href=""prov.aspx?id=" + dr["idStyleOwner"].ToString() + @""">" + dr["shop_name"].ToString() + "</a></h2>" + Environment.NewLine +
                    @"<div class=""rating_search"">" + Environment.NewLine +
                    @"<h3>Rating:</h3>" + Environment.NewLine + HairSlayer.includes.Functions.RateIcon(dr["overall"].ToString(), dr["gender"].ToString()) + Environment.NewLine +
                    "</div>" + Environment.NewLine + "</div>" + Environment.NewLine;
                }

                SearchGrid.Controls.Add(new LiteralControl(temp_res));
                Response.Cookies["hairslayer_preference"].Value = "male";
                Response.Cookies["hairslayer_preference"].Expires = DateTime.Now.AddYears(1);
            }
            //RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            if (what_search)
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + looking_for.Text.Trim() + @"""";
            }
            else
            {
                RowsReturned.Text = dt.Rows.Count + @" results found for """ + search_location.Text.Trim() + @"""";
            }
            RowsReturned.Visible = true;
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
            }
            oDtr.Close();
            oConn.Close();
        }

        protected void MetroArea_SelectedIndexChanged(object sender, EventArgs e)
        {

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