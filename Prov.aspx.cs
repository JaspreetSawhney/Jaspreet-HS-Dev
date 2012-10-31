using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using Twilio;

namespace HairSlayer
{
    public partial class Prov : System.Web.UI.Page
    {
        Int32 Doid = 0;
        string phoneno;
        string connect_string = (ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    try
                    {
                        hdnProv.Value = Request.QueryString["id"];
                    }
                    catch
                    { }
                }
                BindDataList();
                BindCommentGrid();
                btnsbmit.OnClientClick = "rpxSocial(" + Request.QueryString["id"] + ")";

               

                if (HairSlayer.includes.Functions.is_logged_in())
                {
                    RatingsPanelLoggedIn.Visible = true;
                    if (Request.QueryString["id"] != Request.Cookies["hr_main_ck"]["user_id"])
                    {
                        DataTable dt = Worker.SqlTransaction("SELECT * FROM mem_shop_rating WHERE idProvider = " + Request.QueryString["id"] + " AND idMember = " + Request.Cookies["hr_main_ck"]["user_id"], connect_string);
                        if (dt.Rows.Count > 0) {
                            
                            pnlRateProvider.Visible = false; }
                             string sql = "SELECT idMembership FROM member WHERE idMember = " + Request.QueryString["id"];
                             DataTable dt1 = Worker.SqlTransaction(sql, connect_string);
                             if (dt1.Rows.Count > 0)
                             {
                                 if ((dt1.Rows[0]["idMembership"].ToString() == "2") || (dt1.Rows[0]["idMembership"].ToString() == "1"))
                                 {
                                     lblContactInfo.Visible = false;
                                     // PopulateMember();
                                 }
                             }
                        
                    }
                    else
                    {
                        pnlRateProvider.Visible = false;
                        pnlComments.Visible = false;
                        ContactBarber.Visible = false;
                        //mustlogin.Text = "You Must Login To Contact This Stylish/Barber";
                        //mustlogin.Visible = true;
                 
                    }
                }
                else
                {

                    PopulateNoLoginScreen();
                }
            }
            NavigationMenu.My_DosButtonClicked += new EventHandler(MemberMenu_My_DosButtonClicked);
            NavigationMenu.My_FavesButtonClicked += new EventHandler(MemberMenu_My_FavesButtonClicked);
        }

        protected void PopulateNoLoginScreen()
        {
            string sql = "SELECT idMembership FROM member WHERE idMember = " + Request.QueryString["id"];
            DataTable dt = Worker.SqlTransaction(sql, connect_string);
            if (dt.Rows.Count > 0)
            {
                if ((dt.Rows[0]["idMembership"].ToString() == "2") || (dt.Rows[0]["idMembership"].ToString() == "1"))
                {
                    lblContactInfo.Visible = false;
                    ContactBarber.Visible = false;
                    mustlogin.Text = "You Must Login To Contact This Stylish/Barber";
                    mustlogin.Visible = true;
                }
                else
                {
                    ContactBarber.Visible = false;
                    //mustlogin.Text = "You Must Login To Contact This Stylish/Barber";
                    //mustlogin.Visible = true;
                }
            }
            SubmitCommentNonLogin.Visible = true;
            RatingsPanelNotLoggedIn.Visible = true;
        }

        public void bindProfileGrid()
        {
            
        }

        private void MemberMenu_My_DosButtonClicked(object sender, EventArgs e)
        {
            // ... do something when event is fired
            // ...
            //lnkDoPortfolio_ModalPopup.Show();
        }

        private void MemberMenu_My_FavesButtonClicked(object sender, EventArgs e)
        {
            // ... do something when event is fired
            // ...
        }

        public void BindDataList()
        {
            string aaaa = Request.QueryString["id"];
            string sql = "SELECT idMembership FROM member WHERE idMember = " + Request.QueryString["id"];
            DataTable dt = Worker.SqlTransaction(sql, connect_string);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["idMembership"].ToString() == "3")
                {
                   // lblContactInfo.Visible = false;
                    PopulateMember();
                }
                else
                {
                   
                    PopulateProvider();
                }
            }
        }

        public void PopulateProvider()
        {
            // Int32 idMember = 1;
            DataTable dt = new DataTable();
            MySqlConnection oCon = new MySqlConnection(connect_string);
            if (oCon.State == System.Data.ConnectionState.Closed)
            {
                oCon.Open();
            }
            DataSet ds = new DataSet();


            MySqlCommand cmd = new MySqlCommand("sp_Provider", oCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_id", Request.QueryString["id"]);

            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
           
            sda.Fill(ds);


            stylegridlist.Text = @"<div id=""side_bar_grid""><h2 id=""stylist_name_side"">My Styles</h2><ul id=""side_bar_img_list"">";

            int max_pic_count = 6;
            if (ds.Tables[1].Rows.Count < 6) { max_pic_count = ds.Tables[1].Rows.Count; }
            for (int i = 0; i < max_pic_count; i++)
            {
                stylegridlist.Text += @"<li><a href=""do.aspx?id=" + ds.Tables[1].Rows[i]["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(ds.Tables[1].Rows[i]["idMember"] + @"\" + ds.Tables[1].Rows[i]["idDo"] + @".jpg", 75, 75, Convert.ToInt32(ds.Tables[1].Rows[i]["idDo"])) + @""" alt=""""></a></li>";
            }
            stylegridlist.Text += "</ul></div>";

            if (ds.Tables[1].Rows.Count > 6) { pnlViewAll.Visible = true; }

            string img_spot = "";
            FullGallery.Text = "<ul>";
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                FullGallery.Text += @"<li><div class=""thumb_wrap""><a href=""do.aspx?id=" + dr["idDo"].ToString() + @""" class=""full-gallery""><div class=""cover_thumbnail""></div><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 150, 150, Convert.ToInt32(dr["idDo"])) + @""" /></div></a></li>";
            }
            FullGallery.Text += "</ul>";
            


            /**********Populate Info**************/
            if (ds.Tables[0].Rows.Count > 0)
            {
                GetDirections.NavigateUrl = "http://maps.google.com/?q=" + ds.Tables[0].Rows[0]["address"].ToString().Replace(" ", "+") + @",+" + ds.Tables[0].Rows[0]["city"] + @",+" + ds.Tables[0].Rows[0]["state"] + @"+" + ds.Tables[0].Rows[0]["zip"];
                /***************LOCATION CODE*/
                System.Web.UI.HtmlControls.HtmlGenericControl javScript = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
                javScript.Attributes.Add("type", "text/javascript");
                javScript.InnerHtml = @"$(document).ready(function(){ " + Environment.NewLine +
                    "var geocoder;  var map; " + Environment.NewLine +
                    "geocoder = new google.maps.Geocoder(); " + Environment.NewLine +
                    "var latlng = new google.maps.LatLng(-34.397, 150.644); " + Environment.NewLine +
                    "var myOptions = { " + Environment.NewLine +
                    " zoom: 13, " + Environment.NewLine +
                    " center: latlng, " + Environment.NewLine + Environment.NewLine +
                    " mapTypeId: google.maps.MapTypeId.ROADMAP, " + Environment.NewLine +
                    " zoomControl:false, " + Environment.NewLine +
                    "  disableDefaultUI: true " + Environment.NewLine +
                    " 	} " + Environment.NewLine +
                    @" map = new google.maps.Map(document.getElementById(""map""), myOptions); " + Environment.NewLine +
                    " var codeAddress = function() { " + Environment.NewLine +
                    @"var address = """ + ds.Tables[0].Rows[0]["address"].ToString() + @", " + ds.Tables[0].Rows[0]["city"] + @", " + ds.Tables[0].Rows[0]["state"] + @" " + ds.Tables[0].Rows[0]["zip"] + @"""; " + Environment.NewLine +
                    "geocoder.geocode( { 'address': address}, function(results, status) { " + Environment.NewLine +
                    "if (status == google.maps.GeocoderStatus.OK) { " + Environment.NewLine +
                    "  map.setCenter(results[0].geometry.location); " + Environment.NewLine +
                    "  var marker = new google.maps.Marker({ " + Environment.NewLine +
                    "      map: map, " + Environment.NewLine +
                    "      position: results[0].geometry.location " + Environment.NewLine +
                    "  }); " + Environment.NewLine +
                    "} else { " + Environment.NewLine +
                    @"  alert(""Geocode was not successful for the following reason: "" + status); " + Environment.NewLine +
                    "} " + Environment.NewLine +
                    "}); " + Environment.NewLine +
                    "} " + Environment.NewLine +
                    "codeAddress(); " + Environment.NewLine +
                    "});";

                MapLocation.Controls.Add(javScript);

                lblContactInfo.Text = "<p>" + ds.Tables[0].Rows[0]["firstname"] + " " + ds.Tables[0].Rows[0]["lastname"] + "</p>";

                if (ds.Tables[0].Rows[0]["idMembership"].ToString() == "3") { pnlProfileInfo.Visible = false; }
                Star.Value = "M";
                switch (ds.Tables[0].Rows[0]["idMembership"].ToString())
                {
                    case "1":
                    case "4":
                    case "6":
                        Star.Value = "M";
                        break;
                    case "2":
                    case "5":
                    case "7":
                        Star.Value = "F";
                        break;
                }

                /***************************SOCIAL PROFILES*****************************/
                //The Social Links should only be visible if the provider has a premium account and the database values are not null
                if (includes.Functions.isPremiumAccount(ds.Tables[0].Rows[0]["idMembership"].ToString()))
                {
                   // phoneno = ds.Tables[0].Rows[0]["phone"].ToString();
                    lblContactInfo.Text += "<p>" + FormatPhone(ds.Tables[0].Rows[0]["phone"].ToString()) + "</p>";
                    if (ds.Tables[0].Rows[0]["facebook"] != DBNull.Value && ds.Tables[0].Rows[0]["facebook"] != "") 
                    { 
                        Facebook.NavigateUrl = ds.Tables[0].Rows[0]["facebook"].ToString();
                        Facebook.Visible = true;
                    }
                    if (ds.Tables[0].Rows[0]["twitter"] != DBNull.Value && ds.Tables[0].Rows[0]["twitter"] != "") 
                    { 
                        Twitter.NavigateUrl = ds.Tables[0].Rows[0]["twitter"].ToString();
                        Twitter.Visible = true;
                    }
                    if (ds.Tables[0].Rows[0]["instagram"] != DBNull.Value && ds.Tables[0].Rows[0]["instagram"] != "")
                    {
                        Instagram.NavigateUrl = "http://web.stagram.com/n/" + ds.Tables[0].Rows[0]["instagram"].ToString();
                        Instagram.Visible = true;
                    }
                }
                phoneno = ds.Tables[0].Rows[0]["phone"].ToString();
                Session["phoneno"] = phoneno;
                lblContactInfo.Text += "<p>" + ds.Tables[0].Rows[0]["shop_name"] + "</p><p>" +
                    ds.Tables[0].Rows[0]["address"] + "</p><p>" +
                    ds.Tables[0].Rows[0]["city"] + "," + ds.Tables[0].Rows[0]["state"] + " " + ds.Tables[0].Rows[0]["zip"] + "</p>";

                if (ds.Tables[0].Rows[0]["bio"] != DBNull.Value) { BioPlaceholder.Controls.Add(new LiteralControl(ds.Tables[0].Rows[0]["bio"].ToString())); }
                /***************************SOCIAL PROFILES*****************************/

                WImage1.Line1Text = ds.Tables[0].Rows[0]["firstname"] + " " + ds.Tables[0].Rows[0]["lastname"];
                WImage1.Line2Text = ds.Tables[0].Rows[0]["shop_name"].ToString();
                WImage1.Width = 700;
                WImage1.Height = 442;
                WImage1.Rating = ds.Tables[0].Rows[0]["overall"].ToString();
                WImage1.Membership = ds.Tables[0].Rows[0]["idMembership"].ToString();

                if (Request.QueryString["id"] != null)
                {

                    if (File.Exists(Server.MapPath("~/images/Users/" + Request.QueryString["id"] + "/profile.jpg")))
                    {
                        WImage1.ImageURL = "/images/Users/" + Request.QueryString["id"] + "/profile.jpg";
                    }
                    else
                    {
                        WImage1.ImageURL = "/images/profile_bg.gif";
                    }
                }

                if (includes.Functions.isPremiumAccount(ds.Tables[0].Rows[0]["idMembership"].ToString()))
                {
                    btnAppointment.Visible = true;
                }

                string metaservices = "";
                lblServicesOffered.Text = @"<table id=""services_offered_prof"">";
                int maxServices = ds.Tables[0].Rows.Count;
                if (ds.Tables[0].Rows.Count > 4) 
                { 
                    maxServices = 4;
                    string temp_services = @"<table id=""services_offered_prof"">";
                    for (int z = 0; z < maxServices; z++)
                    {
                        if (includes.Functions.isPremiumAccount(ds.Tables[0].Rows[0]["idMembership"].ToString()))
                        {
                            temp_services += @"<tr><td><a href=""schedule.aspx?id=aj_x" + hdnProv.Value + @"&sid=" + ds.Tables[0].Rows[z]["idService"] + @""">" + ds.Tables[0].Rows[z]["service_name"] + @"</a></td><td class=""price_prof""><a href=""schedule.aspx?id=aj_x" + hdnProv.Value + @"&sid=" + ds.Tables[0].Rows[z]["idService"] + @"""> $" + ds.Tables[0].Rows[z]["price"] + "</a></td></tr>";
                        }
                        else
                        {
                            temp_services += @"<tr><td>" + ds.Tables[0].Rows[z]["service_name"] + @"</td><td class=""price_prof""> $" + ds.Tables[0].Rows[z]["price"] + "</td></tr>";
                        }
                        metaservices += ", " + ds.Tables[0].Rows[z]["service_name"]; 
                    }
                    temp_services += "</table>";
                    ServicesPlaceholder.Controls.Add(new LiteralControl(temp_services));
                    view_full_services.Visible = true;
                }
                for (int i = 0; i < maxServices; i++)
                {
                    if (ds.Tables[0].Rows[i]["idService"] != DBNull.Value) 
                    {
                        if (includes.Functions.isPremiumAccount(ds.Tables[0].Rows[0]["idMembership"].ToString()))
                        {
                            btnAppointment.Visible = true;
                            lblServicesOffered.Text += @"<tr><td><a href=""schedule.aspx?id=aj_x" + hdnProv.Value + @"&sid=" + ds.Tables[0].Rows[i]["idService"] + @""">" + ds.Tables[0].Rows[i]["service_name"] + @" - </a></td><td class=""price_prof""><a href=""schedule.aspx?id=aj_x" + hdnProv.Value + @"&sid=" + ds.Tables[0].Rows[i]["idService"] + @"""> $" + ds.Tables[0].Rows[i]["price"] + "</a></td></tr>";
                        }
                        else
                        {
                            lblServicesOffered.Text += @"<tr><td>" + ds.Tables[0].Rows[i]["service_name"] + @" - </td><td class=""price_prof""> $" + ds.Tables[0].Rows[i]["price"] + "</td></tr>";
                        }
                    }
                }
                lblServicesOffered.Text += "</table>";

                if (!IsPostBack)
                {
                    System.Web.UI.HtmlControls.HtmlMeta keywords = new System.Web.UI.HtmlControls.HtmlMeta();
                    keywords.Name = "keywords";
                    keywords.Content = "Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle, " + ds.Tables[0].Rows[0]["firstname"] + " " + ds.Tables[0].Rows[0]["lastname"] + ", " + ds.Tables[0].Rows[0]["shop_name"] + metaservices;
                    Header.Controls.Add(keywords);
                }

            }
            /**********Populate Info**************/

            /**********Populate Comments**************/
            //CommentsGrid.DataSource = ds.Tables[2];
            //CommentsGrid.DataBind();
            /**********Populate Comments**************/
            
         }

        public void PopulateMember()
        {
            pnlProfileInfo.Visible = false;
            MySqlConnection oCon = new MySqlConnection(connect_string);
            if (oCon.State == System.Data.ConnectionState.Closed)
            {
                oCon.Open();
            }
            DataSet ds = new DataSet();


            MySqlCommand cmd = new MySqlCommand("sp_Member", oCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_id", Request.QueryString["id"]);

            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);

            sda.Fill(ds);

            stylegridlist.Text = @"<div id=""side_bar_grid""><h2 id=""stylist_name_side"">My Styles</h2><ul id=""side_bar_img_list"">";

            int max_pic_count = 6;
            if (ds.Tables[1].Rows.Count < 6) { max_pic_count = ds.Tables[1].Rows.Count; }
            for (int i = 0; i < max_pic_count; i++)
            {
                stylegridlist.Text += @"<li><a href=""do.aspx?id=" + ds.Tables[1].Rows[i]["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(ds.Tables[1].Rows[i]["idMember"] + @"\" + ds.Tables[1].Rows[i]["idDo"] + @".jpg", 75, 75, Convert.ToInt32(ds.Tables[1].Rows[i]["idDo"])) + @""" alt=""""></a></li>";
            }
            

            if (ds.Tables[1].Rows.Count > 6) { pnlViewAll.Visible = true; }

            string img_spot = "";
            FullGallery.Text = "<ul>";
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                FullGallery.Text += @"<li><div class=""thumb_wrap""><a href=""do.aspx?id=" + dr["idDo"].ToString() + @""" class=""full-gallery""><div class=""cover_thumbnail""></div><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 150, 150, Convert.ToInt32(dr["idDo"])) + @""" /></div></a></li>";
            }
            FullGallery.Text += "</ul>";

            stylegridlist.Text += "</ul></div>";

            /**********Populate Info**************/
            if (ds.Tables[0].Rows.Count > 0)
            {
                Star.Value = "M";
                switch (ds.Tables[0].Rows[0]["idMembership"].ToString())
                {
                    case "1":
                    case "4":
                    case "6":
                        Star.Value = "M";
                        break;
                    case "2":
                    case "5":
                    case "7":
                        Star.Value = "F";
                        break;
                }

                WImage1.Line1Text = ds.Tables[0].Rows[0]["firstname"] + " " + ds.Tables[0].Rows[0]["lastname"];
                //WImage1.Line2Text = ds.Tables[0].Rows[0]["shop_name"].ToString();
                WImage1.Width = 700;
                WImage1.Height = 442;
                //WImage1.Rating = ds.Tables[0].Rows[0]["overall"].ToString();
                WImage1.Membership = ds.Tables[0].Rows[0]["idMembership"].ToString();
                

                
                if (Request.QueryString["id"] != null)
                {

                    if (File.Exists(Server.MapPath("~/images/Users/" + Request.QueryString["id"] + "/profile.jpg")))
                    {
                        WImage1.ImageURL = "/images/Users/" + Request.QueryString["id"] + "/profile.jpg";
                    }
                    else
                    {
                        WImage1.ImageURL = "/images/profile_bg.gif";
                    }
                }
                
                string metaservices = "";
                

                if (!IsPostBack)
                {
                    System.Web.UI.HtmlControls.HtmlMeta keywords = new System.Web.UI.HtmlControls.HtmlMeta();
                    keywords.Name = "keywords";
                    keywords.Content = "Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle, " + ds.Tables[0].Rows[0]["firstname"] + " " + ds.Tables[0].Rows[0]["lastname"] + ", " + metaservices;
                    Header.Controls.Add(keywords);
                }

            }

        }

        public string FormatPhone(string phn)
        {
            if (phn.Length == 10)
            {
                return phn.Substring(0, 3) + "-" + phn.Substring(3, 3) + "-" + phn.Substring(6, 4);
            }
            else
            {
                return phn;
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblidDo = (Label)e.Item.FindControl("lblidDo");
                string idDo = lblidDo.Text;
                Label lblidMember = (Label)e.Item.FindControl("lblidMember");
                string idMember = lblidMember.Text;
                ImageButton img = (ImageButton)e.Item.FindControl("ImageButton1");
                img.ImageUrl = "./Users/" + Request.QueryString["id"] + "/" + idDo + ".jpg";
                img.PostBackUrl = "./Do.aspx?id=" + idDo;
            }
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Click")
            {
                Label lblidDo = (Label)e.Item.FindControl("lblidDo");
                string idDo = lblidDo.Text;
                //Int32 idDo= Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect("Do.aspx?id=" + idDo);
            }
        }

        protected void btnAppointment_Click(object sender, EventArgs e)
        {
            Response.Redirect("schedule.aspx?id=aj_x" + hdnProv.Value);
        }

        protected void btnsbmit_Click(object sender, EventArgs e)
        {
            MySqlConnection oCon = new MySqlConnection(connect_string);
            if (oCon.State == System.Data.ConnectionState.Closed)
            {
                oCon.Open();
            }
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = oCon;
            cmd.CommandText = "insert into providercomments (ProviderId,UserId,Comment,Date,Doid) values(@ProviderId,@UserID,@Comment,@Date,@Doid)";
            cmd.Parameters.AddWithValue("@ProviderId", Request.QueryString["id"]);
            cmd.Parameters.AddWithValue("@UserId", Request.Cookies["hr_main_ck"]["user_id"]);
            cmd.Parameters.AddWithValue("@Doid", Doid);
            cmd.Parameters.AddWithValue("@Date", System.DateTime.Now);
            cmd.Parameters.AddWithValue("@Comment", txtcomment.Text);
            cmd.ExecuteNonQuery();
            oCon.Close();
            txtcomment.Text = "";

            BindCommentGrid();
        }

        public void BindCommentGrid()
        {
            /*MySqlConnection oCon = new MySqlConnection(connect_string);
            if (oCon.State == System.Data.ConnectionState.Closed)
            {
                oCon.Open();
            }
            DataSet ds = new DataSet();*/
            DataTable dt = new DataTable();

            string query = "select providercomments.*, member.firstname, member.lastname from providercomments, member where member.idMember=providercomments.UserId and providercomments.ProviderId=" + Request.QueryString["id"];
            //MySqlCommand cmd = new MySqlCommand(query, oCon);
            //cmd.CommandType = CommandType.Text;          

            //MySqlDataAdapter sda = new MySqlDataAdapter(cmd);           
            //sda.Fill(ds);

            dt = Worker.SqlTransaction(query, connect_string);
            if (dt.Rows.Count > 0)
            {
                CommentsGrid.DataSource = dt; //cmd.ExecuteReader();
                CommentsGrid.DataBind();
            }

            if (Request.Cookies["hr_main_ck"] != null)
            {
                if (Request.Cookies["hr_main_ck"]["user_id"] == Request.QueryString["id"]) { pnlComments.Visible = false; }
            }
        }

        protected void CommentsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            int tempInt;
            if (int.TryParse(e.CommandArgument.ToString(), out tempInt) == true)
            {
                index = Convert.ToInt32(e.CommandArgument);
                Panel pnlreply = (Panel)CommentsGrid.Rows[index].FindControl("pnlReply");
                if (e.CommandName == "Reply")
                {                    
                    pnlreply.Visible = true;
                }
                if (e.CommandName == "CancelReply")
                {
                    pnlreply.Visible = false;
                }
                if (e.CommandName == "submitReply")
                {
                    // Convert the row index stored in the CommandArgument
                    // casting of property to an Integer.
                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    int memID = int.Parse(Request.QueryString["id"]);

                    // Retrieve the row that contains the button clicked.
                    // by the user from the Rows collection.      
                    GridViewRow row = CommentsGrid.Rows[rowIndex];

                    //fetch the text box from the row. hfProviderComID
                    TextBox commentTextbox = row.FindControl("txtreply") as TextBox;
                    HiddenField hfCommentID = row.FindControl("hfProviderComID") as HiddenField;

                    //create and open the MysqlConnection to save the data to the database.
                    MySqlConnection oCon = new MySqlConnection(connect_string);
                    if (oCon.State == System.Data.ConnectionState.Closed)
                    {
                        oCon.Open();
                    }

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = oCon;
                    cmd.CommandText = "insert into reply (CommentId,Reply,postedByID) values(@CommentId,@Reply,@postedByID)";
                    cmd.Parameters.AddWithValue("@CommentId", hfCommentID.Value);
                    cmd.Parameters.AddWithValue("@Reply", commentTextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@postedByID", memID);    
                    cmd.ExecuteNonQuery();
                    BindCommentGrid();
                }
            }
        }

        public void BindInternalGrid(int commentID)
        {
            //MySqlConnection oCon = new MySqlConnection(connect_string);
            //if (oCon.State == System.Data.ConnectionState.Closed)
            //{
            //    oCon.Open();
            //}
            //DataSet ds = new DataSet();
            //string query = "select reply.*, member.firstname, member.lastname from reply, member where member.idMember=reply.postedByID and reply.CommentId=" + commentID;
            //MySqlCommand cmd = new MySqlCommand(query, oCon);
            //cmd.CommandType = CommandType.Text;

            //MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            //sda.Fill(ds);

            // .DataSource = cmd.ExecuteReader();
            //CommentsGrid.DataBind();

            //string getReplyForComments = "select providercomments.*, member.firstname, member.lastname from providercomments, member where member.idMember=providercomments.UserId and providercomments.ProviderId=" + idMember; ;
            //commentReplyGridView
        }

        protected void CommentsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfCommentID = e.Row.FindControl("hfProviderComID") as HiddenField;
                GridView internalGrid = e.Row.FindControl("commentReplyGridView") as GridView;
                LinkButton replyLinkButton = e.Row.FindControl("lnkReply") as LinkButton;

                int memID = 3;
                if (Request.Cookies["hr_main_ck"] != null)
                {
                    try
                    {
                        memID = int.Parse(Request.Cookies["hr_main_ck"]["membership"]);
                    }
                    catch
                    {
                        memID = 3;
                    }
                }

                if ((memID == 4 || memID == 5) && Request.Cookies["hr_main_ck"]["user_id"] == Request.QueryString["id"])
                {
                    replyLinkButton.Visible = true;
                }
                else
                {
                    replyLinkButton.Visible = false;
                }

                MySqlConnection oCon = new MySqlConnection(connect_string);
                if (oCon.State == System.Data.ConnectionState.Closed)
                {
                    oCon.Open();
                }
                DataSet ds = new DataSet();
                string query = "select reply.*, member.firstname, member.lastname from reply, member where member.idMember=reply.postedByID and reply.CommentId=" + hfCommentID.Value;
                MySqlCommand cmd = new MySqlCommand(query, oCon);
                cmd.CommandType = CommandType.Text;

                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(ds);

                internalGrid.DataSource = cmd.ExecuteReader();
                internalGrid.DataBind();               
            }
        }

        protected void ContactBarber_Click(object sender, EventArgs e)
        {
            BindDataList();
           

            string sql = "SELECT idMembership FROM member WHERE idMember = " + Request.QueryString["id"];
            DataTable dt = Worker.SqlTransaction(sql, connect_string);
            if (dt.Rows.Count > 0)
            {
                if ((dt.Rows[0]["idMembership"].ToString() == "2") || (dt.Rows[0]["idMembership"].ToString() == "1"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "AddResource('440','415');", true);
                }
                else
                {
       
                    phoneno = (string)Session["phoneno"];
                    string accountSid = "AC49ecef1b877e244ea13ada1b5fe92b85";
                    //string applicationsid = "AP5556f6cd1af5cfa5317acae7a49eeb5c";
                    string sid = "CA748c6019014ca0ac151908a81299f927";
                    string authToken = "4eeeaf7ed6459dcd02ec7888149d5ea1";
                    string recordingSid = "RE5dabfb2da314b39cd87dc151bf669e78";
                    DateTime datecreated = DateTime.Now;
                    TwilioRestClient client;
                    client = new TwilioRestClient(accountSid, authToken);
                    string APIversuion = client.ApiVersion;
                    string TwilioBaseURL = client.BaseUrl;

                    Account account = client.GetAccount(accountSid);
                    client.GetRecording(recordingSid);
                    client.GetRecordingText(recordingSid);
                    client.ListRecordings(sid, datecreated, 1, 2);
                    client.ListQueues();
                    client.ListIncomingPhoneNumbers();
                    //string Url = "http://demo.twilio.com/Welcome/Call/";
                    String Url = "http://twimlets.com/message";
                    CallOptions options = new CallOptions(); // Set the call From, To, and URL values into a hash map. // This sample uses the sandbox number provided by Twilio // to make the call.
                    options.From = "+14242165015";
                    options.To = phoneno;
                    options.Url = Url; // Place the call.
                    options.Record = true;
                    
                    var call = client.InitiateOutboundCall(options);
                   
                    Session.Remove("phoneno");
                }
             
            }
            }
                
                     
        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BindDataList();
            string clientidd = Request.Cookies["hr_main_ck"]["user_id"];
            phoneno = (string)Session["phoneno"];
            string msg = "A new client has requested your services. Click here to get their info: https://www.Hairslayer.com/Prov.aspx?id=" + clientidd;

                    //string Account_sid = ConfigurationManager.AppSettings["AC91195ee5f1c44a03a5659b1a76abf81d"];
                    //string Auth_token = ConfigurationManager.AppSettings["c8ad332d8af394c0205f4a4a0cc33476"];

                    TwilioRestClient client = new TwilioRestClient("AC91195ee5f1c44a03a5659b1a76abf81d", "c8ad332d8af394c0205f4a4a0cc33476");

                    client.SendSmsMessage("(415)-723-4000", phoneno, msg);
                    Session.Remove("phoneno");

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('The Provider has been notified and will contact you shortly');</script>");

            
             

            }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BindDataList();
           
        }
       
    }
}