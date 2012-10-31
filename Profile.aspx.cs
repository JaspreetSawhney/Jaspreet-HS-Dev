using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using Twilio;
using Twilio.TwiML;
using Twilio.WebMatrix;
using RestSharp.Authenticators;
using RestSharp.Contrib;
using RestSharp.Serializers;
using RestSharp.Extensions;
using RestSharp.Validation;
using RestSharp;



namespace HairSlayer
{
    public partial class Profile : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
        string msg;
        string url;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (hashvalue.Value != "") { Show_DoUpload(); }
            string[] temp_hash = hashvalue.Value.Split('&');
            if (temp_hash.Length > 0)
            {
                for (int i = 0; i < temp_hash.Length; i++)
                {
                    string[] h = temp_hash[i].Split('=');
                    if (h[0] == "remoteImageURL") { loc.Text = h[1]; }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!includes.Functions.is_logged_in()) { Response.Redirect("Login.aspx?id=FyQ"); }
            //  ViewDoProfile.NavigateUrl = "prov.aspx?id=" + Request.Cookies["hr_main_ck"]["user_id"];
            //if (hashvalue.Value != "") { Show_DoUpload(); }
            if (Request.QueryString["xid"] != null)
            {
                if (Request.QueryString["xid"] == "x1") { Show_DoUpload(); }
            }
            if (!includes.Functions.isPremiumAccount())
            {
                string sql = "SELECT idDo FROM dos WHERE idMember = " + Request.Cookies["hr_main_ck"]["user_id"];
                DataTable dt = Worker.SqlTransaction(sql, connect_string);
                if (dt.Rows.Count > 2)
                {
                    pnlUpload.Visible = false;
                    doInfo.Visible = false;
                    NoUploads.Visible = true;
                }
                //Response.Write(dt.Rows.Count + " - " + Request.Cookies["hr_main_ck"]["membership"] + " -- " + Request.Cookies["hr_main_ck"]["user_id"] + "<br>" + sql);
            }
            if (!IsPostBack)
            {
                if (Request.Cookies["hr_main_ck"] != null)
                {
                    if (Request.QueryString["eid"] != null)
                    {
                        if (Request.QueryString["eid"] == "bmV3YWNj")
                        {
                            if (Request.Cookies["hr_main_ck"]["validate"] == "true") { Response.Redirect("Profile.aspx"); }
                        }
                    }
                }
                else
                {
                    Response.Redirect("index.aspx");
                }

                DeleteShopID.Value = "";
                PopulateInfo();

                string[] stylist = new[] { "Haircut", "Haircut w/Chemical Service", "Bang Trim", "Wash and Style", "Wash & Flatiron", "Press & Curl", "Up Do", "Twist", "Cornrows", "Deep Conditioning Treatments", "Permanent Waves", "Curls", "Spiral Perms", "Relaxers - Virgin", "Relaxers - Touch Up", "Relaxers - Touch Up w/Cut", "Relaxers - Halo Touch Up", "Weaves - Full Head", "Weaves - Half Head", "Weaves - Sewn in Tracks", "Weaves - Glue in Tracks", "Color - Virgin Color", "Color - Touch Up", "Color - Highlights", "Color - Corrective Color", "Color - Low Lights", "Color - Temporary Color Gloss", "Color - Clear Gloss", "Brow Wax", "Lip Wax", "Facial Wax", "Whole Face Wax Package" };
                string[] barber = new[] { "Haircut", "Haircut w/Beard Trim", "Child Haircut", "Designer Haircut", "Beard Trim", "Line Only", "Straight Razor Shave", "Scalp Treatments", "Shampoo" };
                switch (Request.Cookies["hr_main_ck"]["membership"])
                {
                    case "1":
                    case "4":
                    case "6":
                        rbtServices.DataSource = barber;
                        rbtServices.DataBind();
                        lblDoServices.Visible = true;
                        break;
                    case "2":
                    case "5":
                    case "7":
                        rbtServices.DataSource = stylist;
                        rbtServices.DataBind();
                        lblDoServices.Visible = true;
                        break;
                }

                string sql2 = "SELECT a.idService, a.service_name FROM services AS a JOIN shop AS b ON a.idShop = b.idShop WHERE b.idMember = " + Request.Cookies["hr_main_ck"]["user_id"];
                DataTable dt2 = Worker.SqlTransaction(sql2, connect_string);
                Service_Style.DataSource = dt2;
                Service_Style.DataValueField = "idService";
                Service_Style.DataTextField = "service_name";
                Service_Style.DataBind();
                Service_Style.Items.Insert(0, new ListItem("-Select Service-", ""));
            }

            BindServicesTable();
            //ServiceStatus.Text = "SELECT * FROM services WHERE idShop = " + ShopID.Value;
            //ServiceStatus.Visible = true;

            if (!IsPostBack)
            {

                if (Request.QueryString["eid"] != null)
                {
                    //bmV3YWNj
                    if (HairSlayer.includes.Functions.FromBase64(Request.QueryString["eid"]) == "newacc" && Request.Cookies["hr_main_ck"]["membership"] != "3")
                    {
                        btnNext.Visible = true;
                        btnSave.Visible = false;
                    }

                    if (HairSlayer.includes.Functions.FromBase64(Request.QueryString["eid"]) == "barBer" && includes.Functions.isPremiumAccount())
                    {
                        Show_Schedule();
                    }
                }

                if (Request.Cookies["hr_main_ck"]["membership"] == "3")
                {
                    Service.Visible = false;
                    Clients.Visible = false;
                    Schedule.Visible = false;
                    //pnlUploadPic.Visible = false;
                    //ViewProfile.Visible = false;   
                }

                if (Request.Cookies["hr_main_ck"]["membership"] == "1" || Request.Cookies["hr_main_ck"]["membership"] == "2")
                {
                    Schedule.Visible = false;
                }

            }

            ScriptManager curObj = ScriptManager.GetCurrent(Page);

            //if (curObj != null) { curObj.RegisterPostBackControl(btnUpload); }
        }

        protected bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected void Show_Schedule()
        {
            Clear_CSS();
            Schedule.CssClass = "info_active";
            pnlSchedule.Visible = true;
        }

        protected void Show_DoUpload()
        {
            Clear_CSS();
            Upload.CssClass = "info_active";
            pnlUploadDo.Visible = true;
        }

        protected void PopulateInfo()
        {
            string sql = "";
            if (Request.Cookies["hr_main_ck"]["membership"] == "3")
            {
                Service.Visible = false;
                pnlServices.Visible = false;
                Clients.Visible = false;
                Schedule.Visible = false;
                //Upload.Visible = false;
                lblDoServices.Visible = false;
                lblServices.Visible = false;
                Service_Style.Visible = false;

                sql = "SELECT * FROM member WHERE idMember = " + Request.Cookies["hr_main_ck"]["user_id"];
                DataTable dt = Worker.SqlTransaction(sql, connect_string);
                pnlServicePreference.Visible = true;
                pnlShop.Visible = false;
                Bio.Visible = false;
                Bio_Label.Visible = false;
                Firstname.Text = dt.Rows[0]["firstname"].ToString();
                Lastname.Text = dt.Rows[0]["lastname"].ToString();
                Email.Text = dt.Rows[0]["email"].ToString();
                City.Text = dt.Rows[0]["city"].ToString();
                Zip.Text = dt.Rows[0]["zip"].ToString();
                //ShopGeoLocation.Value = dt.Rows[0]["password"].ToString() + "Y2hhcnZhcg==";
                PlaceHolder ks_holder = (PlaceHolder)Master.FindControl("KissHolder");
                ks_holder.Controls.Add(new LiteralControl("<script>_kmq.push(['identify', '" + Firstname.Text + " " + Lastname.Text + " - " + Email.Text + "']);</script>"));
                if (dt.Rows[0]["state"] != DBNull.Value)
                {
                    ddlState.SelectedValue = dt.Rows[0]["state"].ToString();
                }


                if (File.Exists(Server.MapPath("~/images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/profile.jpg")))
                {
                    //  imgprofilepic.ImageUrl = "images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/profile.jpg";
                }
            }
            else
            {
                /*string[] stylist = new[] { "Haircut", "Haircut w/Chemical Service", "Bang Trim", "Wash and Style", "Wash & Flatiron", "Press & Curl", "Up Do", "Twist", "Cornrows", "Deep Conditioning Treatments", "Permanent Waves", "Curls", "Spiral Perms", "Relaxers - Virgin", "Relaxers - Touch Up", "Relaxers - Touch Up w/Cut", "Relaxers - Halo Touch Up", "Weaves - Full Head", "Weaves - Half Head", "Weaves - Sewn in Tracks", "Weaves - Glue in Tracks", "Color - Virgin Color", "Color - Touch Up", "Color - Highlights", "Color - Corrective Color", "Color - Low Lights", "Color - Temporary Color Gloss", "Color - Clear Gloss", "Brow Wax", "Lip Wax", "Facial Wax", "Whole Face Wax Package" };
                string[] barber = new[] { "Haircut", "Haircut w/Beard Trim", "Child Haircut", "Designer Haircut", "Beard Trim", "Line Only", "Straight Razor Shave", "Scalp Treatments", "Shampoo" };

                switch (Request.Cookies["hr_main_ck"]["membership"])
                {
                    case "1":
                    case "4":
                        Style.DataSource = barber;
                        Style.DataBind();
                        Style.Items.Insert(0, new ListItem("-Select Service-", ""));
                        break;
                    case "2":
                    case "5":
                        Style.DataSource = stylist;
                        Style.DataBind();
                        Style.Items.Insert(0, new ListItem("-Select Service-", ""));
                        break;
                }*/
                sql = "SELECT firstname,lastname,email, gender, a.city As a_city, a.state As a_state, a.zip As a_zip, shop_name, b.address As b_address, b.city As b_city, b.state As b_state, b.zip As b_zip, phone, idShop, twitter, instagram, facebook, website, bio FROM member As a LEFT JOIN shop As b ON a.idMember = b.idMember WHERE a.idMember = " + Request.Cookies["hr_main_ck"]["user_id"];
                DataTable dt = Worker.SqlTransaction(sql, connect_string);
                pnlServicePreference.Visible = false;
                pnlShop.Visible = true;

                Firstname.Text = dt.Rows[0]["firstname"].ToString();
                Lastname.Text = dt.Rows[0]["lastname"].ToString();
                Email.Text = dt.Rows[0]["email"].ToString();
                PlaceHolder ks_holder = (PlaceHolder)Master.FindControl("KissHolder");
                ks_holder.Controls.Add(new LiteralControl("<script>_kmq.push(['identify', '" + Firstname.Text + " " + Lastname.Text + " - " + Email.Text + "']);</script>"));
                /*City.Text = dt.Rows[0]["a_city"].ToString();
                Zip.Text = dt.Rows[0]["a_zip"].ToString();
                if (dt.Rows[0]["a_state"] != DBNull.Value)
                {
                    ddlState.SelectedValue = dt.Rows[0]["a_state"].ToString();
                }*/

                if (dt.Rows[0]["idShop"] != DBNull.Value)
                {
                    ShopID.Value = dt.Rows[0]["idShop"].ToString();
                }

                if (Request.Cookies["hr_main_ck"]["membership"] != "0" && Request.Cookies["hr_main_ck"]["membership"] != "3")
                {
                    if (Request.Cookies["hr_main_ck"]["membership"] == "1" || Request.Cookies["hr_main_ck"]["membership"] == "4")
                    {
                        ddlServicePreference.SelectedValue = "M";
                    }
                    else
                    {
                        ddlServicePreference.SelectedValue = "F";
                    }
                }

                Shopname.Text = dt.Rows[0]["shop_name"].ToString();
                City.Text = dt.Rows[0]["b_city"].ToString();
                Zip.Text = dt.Rows[0]["b_zip"].ToString();
                Address.Text = dt.Rows[0]["b_address"].ToString();
                Phone.Text = dt.Rows[0]["phone"].ToString();
                Website.Text = dt.Rows[0]["website"].ToString();

                if (dt.Rows[0]["bio"] != DBNull.Value)
                {
                    Bio.Text = dt.Rows[0]["bio"].ToString();
                }

                if (dt.Rows[0]["b_state"] != DBNull.Value)
                {
                    ddlState.SelectedValue = dt.Rows[0]["b_state"].ToString();
                }

                if (dt.Rows[0]["twitter"] != DBNull.Value)
                {
                    Twitter.Text = dt.Rows[0]["twitter"].ToString();
                }
                if (dt.Rows[0]["facebook"] != DBNull.Value)
                {
                    Facebook.Text = dt.Rows[0]["facebook"].ToString();
                }

                if (dt.Rows[0]["instagram"] != DBNull.Value)
                {
                    Instagram.Text = dt.Rows[0]["instagram"].ToString();
                }

                if (File.Exists(Server.MapPath("~/images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/profile.jpg")))
                {
                    //imgProfilePic.ImageUrl = "images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/profile.jpg";
                }
            }
        }

        public static bool chkDirectory(string mainDirectory)
        {
            if (Directory.Exists(mainDirectory))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void newDirectory(string locationDirName)
        {
            //this will create a new Directory At Specified Location.
            Directory.CreateDirectory(locationDirName);
        }

        public static Boolean ThumbNailCallBack()
        {
            return true;
        }

        public void ResizeXLargeImage(System.Drawing.Image new_img)
        {
            //System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(new_img);
            //double rat = (710.0 / Convert.ToDouble(new_img.Width)) * new_img.Height;
            //int new_height = Convert.ToInt32(710.0 / Convert.ToDouble(new_img.Width));

            //System.Drawing.Image thumbnailImage = new_img.GetThumbnailImage(710, new_height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbNailCallBack), IntPtr.Zero);

            //System.Drawing.Bitmap bm = new System.Drawing.Bitmap(thumbnailImage);
            //bm.Save(tmp_fle.Value.Replace(".jpg", "_tmp.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            //new_img.Dispose();
            //File.Copy(tmp_fle.Value.Replace(".jpg", "_tmp.jpg"), tmp_fle.Value, true);
            //File.Delete(tmp_fle.Value.Replace(".jpg", "_tmp.jpg"));
        }

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    Error.Visible = false;
        //    string fn = "";
        //     //if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
        //    bool socialUpload = photoid.Value == "" ? false : true;
        //    if (FileUpload1.HasFile || socialUpload)
        //    {
        //        string[] ar = new string[2];
        //        if (!socialUpload)
        //        {
        //            fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        //            ar = fn.Split('.');
        //            if (ar[1] != "jpg")
        //            {
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Sorry this file format is not supported choose another,Choose another');</script>");
        //            }
        //        }

        //            string IdMember = Request.Cookies["hr_main_ck"]["user_id"];

        //            string mainDirectory = Server.MapPath("~/images/Users/" + IdMember + "/");
        //            DirectoryInfo dir = new DirectoryInfo(mainDirectory);
        //            if (chkDirectory(mainDirectory))
        //            {

        //            }
        //            else
        //            {
        //                newDirectory(mainDirectory);
        //                //Create The Directories and Add the Code for the task to be performed if All Directories Exists.
        //            }

        //            string SaveLocation = mainDirectory + @"profile.jpg", temp_location = Server.MapPath("~/Req_Temp/") + "profile" + IdMember + ".jpg";

        //            tmp_fle.Value = temp_location;
        //            fle_id.Value = SaveLocation;

        //            if (socialUpload)
        //            {
        //                System.Net.WebClient webClient = new System.Net.WebClient();
        //                webClient.DownloadFile(photoid.Value, temp_location);
        //            }
        //            else
        //            {
        //                FileUpload1.PostedFile.SaveAs(temp_location);
        //            }

        //            //System.Drawing.Image upImg = System.Drawing.Image.FromFile(temp_location);
        //            //int o_width = upImg.Width, o_height = upImg.Height;

        //            Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
        //            var image = System.Drawing.Image.FromStream(file_stream);
        //            file_stream.Close();
        //            file_stream.Dispose();
        //            int full_width = image.Width, full_height = image.Height;


        //            if (full_width > 710)
        //            {
        //                double rat = (710.0 / Convert.ToDouble(image.Width)) * image.Height;
        //                int newHeight = Convert.ToInt32(rat);
        //                var newWidth = 710;
        //                full_height = newHeight; full_width = newWidth;
        //                //Response.Write(newHeight);
        //                var thumbnailBitmap = new System.Drawing.Bitmap(newWidth, newHeight);

        //                var thumbnailGraph = System.Drawing.Graphics.FromImage(thumbnailBitmap);
        //                thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //                thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        //                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
        //                thumbnailGraph.DrawImage(image, imageRectangle);
        //                thumbnailBitmap.Save(temp_location.Replace(".jpg", "_x.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
        //                thumbnailGraph.Dispose();
        //                thumbnailBitmap.Dispose();
        //                image.Dispose();

        //                File.Copy(temp_location.Replace(".jpg", "_x.jpg"), temp_location, true);
        //                File.Delete(temp_location.Replace(".jpg", "_x.jpg"));
        //            }
        //            else
        //            {
        //                image.Dispose();
        //            }
        //            double img_ratio = Convert.ToDouble(full_width) / Convert.ToDouble(full_height);
        //            if (img_ratio >= 1.5 && img_ratio <= 2.0)
        //            {
        //                File.Copy(temp_location, SaveLocation, true);
        //                File.Delete(temp_location);
        //                Image1.ImageUrl = "images/spacer.gif";
        //                Image1.Width = Unit.Pixel(1);
        //                Image1.Height = Unit.Pixel(1);
        //                Image2.ImageUrl = "images/spacer.gif";
        //                pnlUpload.Visible = false;
        //                pnlCrop.Visible = false;
        //                Error.Text = "Your Photo Was Uploaded Successfully.";
        //                Error.Visible = true;
        //                imgProfilePic.ImageUrl = "images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/" + Path.GetFileName(fle_id.Value) + "?rnd=" + (new Random()).Next();
        //                UploadProfile.Visible = false;
        //                //Response.Redirect("profile.aspx");
        //            }
        //            else
        //            {
        //                File.Copy(temp_location, temp_location.Replace(".jpg", "_x.jpg"), true);
        //                Image1.ImageUrl = "Req_Temp/profile" + IdMember + "_x.jpg";
        //                Image1.Width = Unit.Pixel(full_width);
        //                Image1.Height = Unit.Pixel(full_height);
        //                Error.Text = "Your Do image size is not compatible with our system. Please crop the image below.";
        //                wci1.X = 0;
        //                wci1.Y = 0;
        //                wci1.X2 = 105;
        //                wci1.Y2 = 66;
        //                Error.Visible = true;
        //                Error.ForeColor = System.Drawing.Color.Black;
        //                pnlUpload.Visible = false;
        //                pnlCrop.Visible = true;
        //                btnCrop.Visible = true;
        //                Session["upload_start"] = true;
        //            }

        //            //upImg.Dispose();
        //            //imgProfilePic.ImageUrl = "Req_Temp/profile" + IdMember + "_x.jpg";
        //            try
        //            {
        //                /*FileUpload1.PostedFile.SaveAs(SaveLocation);
        //                Error.Text = "Your Photo Was Uploaded Successfully.";
        //                Error.Visible = true;
        //                imgProfilePic.ImageUrl = "images/Users/profile.jpg";*/
        //            }
        //            catch (Exception ex)
        //            {
        //                Response.Write("Error: " + ex.Message);
        //            }

        //    }
        //    else
        //    {
        //        Error.Text = "You must select a photo for upload. " + photoid.Value + "--   id";
        //        Error.Visible = true;
        //    }

        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save_Data();
        }

        protected void Save_Data()
        {
            Status.Visible = false;
            if (ProfileDataGood())
            {
                if (Request.QueryString["eid"] != null)
                {
                    if (HairSlayer.includes.Functions.FromBase64(Request.QueryString["eid"]) == "newacc")
                    {
                        btnNext.Visible = true;
                    }
                }
                /**********GEO CODE ADDRESS*********************/
                string pst = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + Address.Text.Trim().Replace(" ", "+") + "," + City.Text.Trim().Replace(" ", "+") + "," + ddlState.SelectedValue + "&sensor=false";
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
                DataTable dtz = Worker.SqlTransaction("SELECT sp_UpdateEmail('" + Email.Text.Trim() + "', " + Request.Cookies["hr_main_ck"]["user_id"] + ")", connect_string);

                string user_id = Request.Cookies["hr_main_ck"]["user_id"];
                string sql = "UPDATE member SET city = @city, state = @state, zip = @zip, gender = @gender, latitude = @latitude, longitude = @longitude, firstname = @firstname, lastname = @lastname WHERE idMember = " + user_id;
                MySqlConnection oConn = new MySqlConnection(connect_string);
                oConn.Open();
                MySqlCommand oComm = new MySqlCommand(sql, oConn);
                oComm.Parameters.AddWithValue("@city", City.Text.Trim());
                oComm.Parameters.AddWithValue("@state", ddlState.SelectedValue);
                oComm.Parameters.AddWithValue("@zip", Zip.Text.Trim());
                oComm.Parameters.AddWithValue("@gender", ddlServicePreference.SelectedValue);
                oComm.Parameters.AddWithValue("@latitude", lat);
                oComm.Parameters.AddWithValue("@longitude", lng);
                oComm.Parameters.AddWithValue("@firstname", Firstname.Text);
                oComm.Parameters.AddWithValue("@lastname", Lastname.Text);
                oComm.ExecuteNonQuery();
                oComm.Dispose();

                Response.Cookies["hr_main_ck"]["user_id"] = user_id;
                Response.Cookies["hr_main_ck"]["user_name"] = Firstname.Text + " " + Lastname.Text;
                Response.Cookies["hr_main_ck"]["gender"] = ddlServicePreference.SelectedValue;
                Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                if (Request.QueryString["eid"] != null)
                {
                    string m_type = "4";
                    if (ddlServicePreference.SelectedValue == "F") { m_type = "5"; }
                    if (HairSlayer.includes.Functions.FromBase64(Request.QueryString["eid"]) == "newacc") { Response.Cookies["hr_main_ck"]["membership"] = m_type; }
                }
                Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                Response.Cookies["hr_main_ck"]["lat"] = lat.ToString();
                Response.Cookies["hr_main_ck"]["lng"] = lng.ToString();
                Response.Cookies["hr_main_ck"]["validate"] = "true";


                if (pnlShop.Visible)
                {
                    //sql = "UPDATE shop SET twitter = @twitter, facebook = @facebook, address = @address, city = @city, state = @state, zip = @zip, phone = @phone, website = @website, shop_name = @shopname, latitude = @latitude, longitude = @longitude WHERE idMember = " + Request.Cookies["hr_main_ck"]["user_id"];
                    oComm = new MySqlCommand("spShopSetup", oConn);
                    oComm.CommandType = System.Data.CommandType.StoredProcedure;
                    oComm.Parameters.AddWithValue("@mem_id", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
                    oComm.Parameters.AddWithValue("@twitt", Twitter.Text.Trim());
                    oComm.Parameters.AddWithValue("@fbook", Facebook.Text.Trim());
                    oComm.Parameters.AddWithValue("@addr", Address.Text.Trim());
                    oComm.Parameters.AddWithValue("@city1", City.Text.Trim());
                    oComm.Parameters.AddWithValue("@state1", ddlState.SelectedValue);
                    oComm.Parameters.AddWithValue("@zip1", Zip.Text.Trim());
                    oComm.Parameters.AddWithValue("@phone1", Phone.Text.Trim());
                    oComm.Parameters.AddWithValue("@website1", Website.Text.Trim());
                    oComm.Parameters.AddWithValue("@shopname", Shopname.Text.Trim());
                    oComm.Parameters.AddWithValue("@lat", lat);
                    oComm.Parameters.AddWithValue("@lng", lng);
                    oComm.Parameters.AddWithValue("@bio1", Bio.Text.Trim().Replace(">", "").Replace("<", ""));
                    oComm.Parameters.AddWithValue("@instag", Instagram.Text.Trim());

                    MySqlDataAdapter sda = new MySqlDataAdapter(oComm);

                    System.Data.DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);

                    ShopID.Value = dt.Rows[0][0].ToString();

                    if (Request.QueryString["eid"] != null)
                    {
                        if (HairSlayer.includes.Functions.FromBase64(Request.QueryString["eid"]) == "newacc")
                        {
                            sql = "SELECT shop_name, password FROM shop JOIN member ON shop.idMember = member.idMember WHERE shop.idMember = " + Request.Cookies["hr_main_ck"]["user_id"];
                            System.Data.DataTable dt2 = Worker.SqlTransaction(sql, (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
                            string shop = "";
                            if (dt2.Rows.Count > 0) { shop = dt2.Rows[0]["shop_name"].ToString(); }
                            Status.Text = sql;
                            Status.Visible = true;
                            HairSlayer.includes.Functions.Create_New_Calendar(Request.Cookies["hr_main_ck"]["user_id"], Email.Text, HairSlayer.includes.Functions.FromBase64(dt2.Rows[0]["password"].ToString()), shop, Firstname.Text + " " + Lastname.Text);
                        }
                    }
                }
                oConn.Close();
                //Status.Text = "Profile succesfully updated.";
                //Status.Visible = true;
                //if (!btnNext.Visible) { btnViewProfile.Visible = true; /*btnFindStyles.Visible = true;*/ }
            }
        }

        protected bool ProfileDataGood()
        {
            if (Request.Cookies["hr_main_ck"]["membership"] != "3")
            {
                bool res = true;
                if (Address.Text.Trim() == "") { res = false; }
                if (City.Text.Trim() == "") { res = false; }
                if (ddlState.SelectedValue == "") { res = false; }
                if (Zip.Text.Trim() == "") { res = false; }

                if (!res)
                {
                    Status.Text = "Please Provide Your Complete Address.";
                    Status.Visible = true;
                }

                if (ddlServicePreference.SelectedValue == "")
                {
                    res = false;
                    Status.Text = "Please Select Your Shop Type.";
                    Status.Visible = true;
                }

                return res;
            }
            return true;
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            ServiceStatus.Visible = false;
            if (ServicesInputGood())
            {
                DataTable dt = Worker.SqlTransaction("SELECT service_name FROM services WHERE service_name = '" + Style.Text.Trim().Replace(">", "").Replace("<", "") + "' AND idShop = " + ShopID.Value, connect_string);

                if (dt.Rows.Count > 0)
                {
                    ServiceStatus.Text = "This service has already been added.<br />";
                    ServiceStatus.Visible = true;
                }
                else
                {
                    /*****OPTIMIZE ENTIRE QUERY SHOULD BE A STORED PROCEDURE**************************/
                    Worker.SqlInsert("INSERT INTO services (service_name,price,idShop) VALUES ('" + Style.Text.Trim().Replace(">", "").Replace("<", "") + "'," + StylePrice.Text.Trim().Replace(">", "").Replace("<", "") + "," + ShopID.Value + ")", connect_string);

                    DataTable dt2 = Worker.SqlTransaction("SELECT idService FROM services WHERE idShop = " + ShopID.Value + " AND service_name = '" + Style.Text.Trim().Replace(">", "").Replace("<", "") + "'", connect_string);
                    Worker.SqlInsert("INSERT INTO a_shed.service_booking_services (id, calendar_id, name, description, price, length, total) VALUES (" + dt2.Rows[0]["idService"] + "," + Request.Cookies["hr_main_ck"]["user_id"] + ",'" + Style.Text.Trim().Replace(">", "").Replace("<", "") + "',''," + StylePrice.Text.Trim().Replace(">", "").Replace("<", "") + "," + ServiceLength.Text.Trim().Replace(">", "").Replace("<", "") + "," + ServiceLength.Text.Trim().Replace(">", "").Replace("<", "") + ")", connect_string);
                    //ServiceStatus.Text = "INSERT INTO a_sched.service_booking_services (id, calendar_id, name, description, price, length, total) VALUES (" + dt2.Rows[0]["idService"] + "," + Request.Cookies["hr_main_ck"]["user_id"] + ",'" + Style.Text.Trim().Replace(">", "").Replace("<", "") + "',''," + StylePrice.Text.Trim().Replace(">", "").Replace("<", "") + "," + ServiceLength.Text.Trim().Replace(">", "").Replace("<", "") + "," + ServiceLength.Text.Trim().Replace(">", "").Replace("<", "") + ")";
                    //ServiceStatus.Visible = true;

                    Worker.SqlInsert("INSERT INTO a_shed.service_booking_employees_services (employee_id, service_id) VALUES (" + Request.Cookies["hr_main_ck"]["user_id"] + "," + dt2.Rows[0]["idService"] + ")", connect_string);
                    /*****OPTIMIZE ENTIRE QUERY SHOULD BE A STORED PROCEDURE**************************/
                    Style.Text = "";
                    StylePrice.Text = "";
                    ServiceLength.Text = "";
                    BindServicesTable();
                }
            }
        }

        protected bool ServicesInputGood()
        {
            Style.Text = Style.Text.Trim().Replace("<", "").Replace(">", "").Replace("'", "''");
            StylePrice.Text = StylePrice.Text.Trim().Replace("<", "").Replace(">", "").Replace("'", "''").Replace(" ", "").Replace("$", "");
            ServiceLength.Text = ServiceLength.Text.Trim().Replace("<", "").Replace(">", "").Replace("'", "''");

            if (Style.Text.Trim() == "")
            {
                ServiceStatus.Text = "Please enter service name<br>";
                ServiceStatus.Visible = true;
                return false;
            }

            if (StylePrice.Text.Trim() == "")
            {
                ServiceStatus.Text = "Please enter a price<br>";
                ServiceStatus.Visible = true;
                return false;
            }
            else
            {
                try
                {
                    Convert.ToDouble(StylePrice.Text.Trim());
                }
                catch
                {
                    ServiceStatus.Text = "Style price is invalid.<br>";
                    ServiceStatus.Visible = true;
                    return false;
                }
            }

            if (ServiceLength.Text.Trim() == "")
            {
                ServiceStatus.Text = "Please enter length of service<br>";
                ServiceStatus.Visible = true;
                return false;
            }
            else
            {
                try
                {
                    Convert.ToInt32(ServiceLength.Text.Trim());
                }
                catch
                {
                    ServiceStatus.Text = "Service length must be numeric.<br>";
                    ServiceStatus.Visible = true;
                    return false;
                }
            }
            return true;
        }

        protected void Delete_Service(object sender, EventArgs e)
        {
            //Worker.SqlInsert("DELETE FROM services WHERE idService = " + ((Button)sender).ID, connect_string);
            ServiceStatus.Text = "DELETE FROM services WHERE idService = " + ((Button)sender).ID;
            ServiceStatus.Visible = true;
            RefreshServices();
        }

        protected void Delete_Button()
        {
            Worker.SqlInsert("DELETE FROM services WHERE idService = " + DeleteShopID.Value, connect_string);
            //Response.Redirect("Profile.aspx?akg=e01");
            RefreshServices();
            DeleteShopID.Value = "";
        }

        protected void RefreshServices()
        {
            //ServiceStatus.Text += "<br />Ran refresh!!!!!!";
            //ServiceStatus.Visible = true;

            //if (pnlYourServices.Controls.Count > 1) { pnlYourServices.Controls.Clear(); }

            pnlYourServices.Controls.Clear();

            Session["services_datatable"] = Worker.SqlTransaction("SELECT * FROM services WHERE idShop = " + ShopID.Value + " ORDER BY service_name", connect_string);
            DataTable dt = (DataTable)Session["services_datatable"];
            foreach (DataRow dr in dt.Rows)
            {
                Button service_button = new Button();
                service_button.CssClass = "service-button";
                service_button.ID = dr["idService"].ToString();
                double tmp = Convert.ToDouble(dr["price"].ToString());
                service_button.Text = "[X] REMOVE"; //dr["service_name"].ToString() + " - $" + tmp.ToString("N2");
                //service_button.Click += new EventHandler(Delete_Service);
                service_button.OnClientClick = "Del_ID(" + dr["idService"].ToString() + ")";

                string temp = @"<span id=""service-title"">" + dr["service_name"].ToString() + " - $" + tmp.ToString("N2") + "</span>";
                pnlYourServices.Controls.Add(new LiteralControl(temp));
                pnlYourServices.Controls.Add(service_button);
                pnlYourServices.Controls.Add(new LiteralControl(@"<br />"));
            }

        }

        //protected void btnCrop_Click(object sender, EventArgs e)
        //{
        //    //crop_photo.Crop(Server.MapPath(Image1.ImageUrl));
        //    Error.Text = "Does everything look ok?";
        //    btnCrop.Visible = false;
        //    btnSave2.Visible = true;
        //    btnGoBack.Visible = true;
        //    wci1.Crop(Server.MapPath(Image1.ImageUrl));
        //    Image1.Visible = false;
        //    Image2.Visible = true;
        //    Image2.ImageUrl = Image1.ImageUrl + "?rnd=" + (new Random()).Next();
        //}

        //protected void btnGoBack_Click(object sender, EventArgs e)
        //{
        //    btnCrop.Visible = true;
        //    btnSave2.Visible = false;
        //    btnGoBack.Visible = false;
        //    Image1.Visible = true;
        //    Image2.Visible = false;
        //    File.Copy(tmp_fle.Value, tmp_fle.Value.Replace(".jpg", "_x.jpg"), true);
        //}

        protected void btnSave2_Click(object sender, EventArgs e)
        {
            //File.Copy(tmp_fle.Value.Replace(".jpg", "_x.jpg"), fle_id.Value, true);
            //File.Delete(tmp_fle.Value);
            //File.Delete(tmp_fle.Value.Replace(".jpg", "_x.jpg"));
            //Image2.ImageUrl = "images/spacer.gif";
            //Image1.ImageUrl = "images/spacer.gif";
            //Image1.Visible = true;
            //Image1.Width = Unit.Pixel(1);
            //Image1.Height = Unit.Pixel(1);
            //Error.Text = "Your Do Was Uploaded Successfully.";
            //Error.Visible = false;
            //Error.ForeColor = System.Drawing.Color.Red;
            ////btnSave.Visible = false;
            //btnGoBack.Visible = false;
            //imgProfilePic.ImageUrl = "images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/" + Path.GetFileName(fle_id.Value) + "?rnd=" + (new Random()).Next();
            //UploadProfile.Visible = false;
            //pnlUpload.Visible = true;
            //pnlCrop.Visible = false;
            //btnSave2.Visible = false;
            //FileUpload1.Dispose();
            //Response.Redirect("profile.aspx");
        }

        protected void Service_Click(object sender, EventArgs e)
        {
            Clear_CSS();
            Service.CssClass = "info_active";
            pnlServices.Visible = true;
        }

        protected void About_Click(object sender, EventArgs e)
        {
            Clear_CSS();
            About.CssClass = "info_active";
            pnlAbout.Visible = true;
        }

        protected void Clear_CSS()
        {
            About.CssClass = "menu-tab";
            Styles.CssClass = "menu-tab";
            Schedule.CssClass = "menu-tab";
            Service.CssClass = "menu-tab";
            Clients.CssClass = "menu-tab";
            Upload.CssClass = "menu-tab";

            pnlAbout.Visible = false;
            pnlClients.Visible = false;
            pnlSchedule.Visible = false;
            pnlServices.Visible = false;
            pnlStyles.Visible = false;
            pnlUploadDo.Visible = false;
        }

        protected void btnStartUpload_Click(object sender, EventArgs e)
        {
            UploadProfile.Visible = true;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            UploadProfile.Visible = false;
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            switch (Locale.Value)
            {
                case "2":
                    //Services --> About
                    Clear_CSS();
                    About.CssClass = "info_active";
                    pnlAbout.Visible = true;
                    Locale.Value = "1";
                    btnBack.Visible = false;
                    break;
                case "3":
                    //Upload --> Services
                    Clear_CSS();
                    Service.CssClass = "info_active";
                    pnlServices.Visible = true;
                    Locale.Value = "2";
                    btnNext.Visible = true;   //This is the last tab until Clients is added back
                    break;
                case "4":
                    //Schedule --> Upload
                    Clear_CSS();
                    Upload.CssClass = "info_active";
                    pnlUploadDo.Visible = true;
                    btnNext.Visible = true;
                    Locale.Value = "3";
                    break;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            switch (Locale.Value)
            {
                case "1":
                    Status.Visible = false;
                    if (ProfileDataGood())
                    {
                        Save_Data();
                        Clear_CSS();
                        Service.CssClass = "info_active";
                        pnlServices.Visible = true;
                        Locale.Value = "2";
                        btnBack.Visible = true;
                    }
                    break;
                case "2":
                    Clear_CSS();
                    Upload.CssClass = "info_active";
                    pnlUploadDo.Visible = true;
                    Locale.Value = "3";
                    break;
                case "3":
                    Clear_CSS();
                    Schedule.CssClass = "info_active";
                    pnlSchedule.Visible = true;
                    Locale.Value = "4";
                    btnNext.Visible = false;
                    break;
            }
            //case "3":
            //Clear_CSS();
            //Clients.CssClass = "menu-tab-active";
            //pnlClients.Visible = true;
            //Locale.Value = "4";
            //btnNext.Visible = false;
            //break;
        }

        protected void btnFindStyles_Click(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx");
        }

        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["hr_main_ck"]["membership"] == "3") { Response.Redirect("Profile.aspx"); }
            System.Data.DataTable dt = Worker.SqlTransaction("SELECT email, password FROM member WHERE idMember = " + Request.Cookies["hr_main_ck"]["user_id"], (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
            string lnk = "script/index.php?controller=Admin&action=login&lrg1=" + dt.Rows[0]["email"] + "&lrg2=" + HairSlayer.includes.Functions.FromBase64(dt.Rows[0]["password"].ToString());
            Response.Write(@"<script>window.open (""" + lnk + @""",""ScheduleWindow"");</script>");
        }

        protected void Schedule_Click(object sender, EventArgs e)
        {
            Clear_CSS();
            Schedule.CssClass = "info_active";
            pnlSchedule.Visible = true;
        }

        protected void btnViewProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Prov.aspx?id=" + Request.Cookies["hr_main_ck"]["user_id"]);
        }

        protected void ViewProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Prov.aspx?id=" + Request.Cookies["hr_main_ck"]["user_id"]);
        }

        protected void BindServicesTable()
        {
            if (ShopID.Value != "0")
            {
                DataTable dt = Worker.SqlTransaction("SELECT * FROM services WHERE idShop = " + ShopID.Value + " ORDER BY service_name", connect_string);
                ServicesHolder.Controls.Clear();
                string tmp_table = @"<table id=""services"">" + Environment.NewLine;
                double tmp_price = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    tmp_price = Convert.ToDouble(dr["price"].ToString());
                    tmp_table += "<tr>" + Environment.NewLine + "<td>" + dr["service_name"] + "</td>" + Environment.NewLine + "<td>$" + tmp_price.ToString("N2") + "</td>" + Environment.NewLine + @"<td><a href=""javascript:delService('" + dr["idService"] + "','" + dr["idShop"] + @"');"" class=""remove_btn"">remove</a></td>" + Environment.NewLine + "</tr>" + Environment.NewLine;
                }
                tmp_table += "</table>";
                ServicesHolder.Controls.Add(new LiteralControl(tmp_table));
                //dr["idService"].ToString()
            }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            Clear_CSS();
            Upload.CssClass = "info_active";
            pnlUploadDo.Visible = true;
        }

        #region Do Upload
        public bool isCompatibleImage(string frmt)
        {
            if (frmt.ToLower() == "png" || frmt.ToLower() == "bmp" || frmt.ToLower() == "gif" || frmt.ToLower() == "jpeg" || frmt.ToLower() == "jpg")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //protected void btnDoUpload_Click(object sender, EventArgs e)
        //{
        //    //Response.Write(photoid.Value);
        //    DoError.Visible = false;
        //    string fn = "";
        //    string[] ar = new string[2];
        //    if (InputIsGood())
        //    {
        //        //if ((DoFileUpload.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
        //        bool socialUpload = photoid.Value == "" ? false : true;
        //        if (DoFileUpload.HasFile || socialUpload)
        //        {
        //            System.Drawing.Image image;
        //            if (!socialUpload)
        //            {
        //                fn = System.IO.Path.GetFileName(DoFileUpload.PostedFile.FileName);
        //                ar = fn.Split('.');
        //                if (!isCompatibleImage(ar[1].ToLower()))
        //                {
        //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Sorry this file format is not supported choose another,Choose another');</script>");
        //                    return;
        //                }
        //            }

        //                DataTable dt = new DataTable();
        //                // Int32 idMember = 1;
        //                string Gender = "M";
        //                if (Request.Cookies["hr_main_ck"]["membership"] == "2" || Request.Cookies["hr_main_ck"]["membership"] == "5") { Gender = "F"; }
        //                //Int32 idStyleOwner = 2;
        //                string date = DateTime.Now.ToString();
        //                string datetime = date.Substring(0, date.Length - 2);
        //                MySqlConnection oCon = new MySqlConnection(connect_string);
        //                if (oCon.State == System.Data.ConnectionState.Closed)
        //                {
        //                    oCon.Open();
        //                }

        //                MySqlCommand cmd = new MySqlCommand();
        //                cmd.Connection = oCon;
        //                string do_name = DoName.Text.Trim().Replace("<", "").Replace(">", "");
        //                //cmd.CommandText = "insert into dos (rmd_date,idMember,Gender,idStyleOwner) values(@rmd_date,@idMember,@Gender,@idStyleOwner)";
        //                cmd.CommandText = "insert into dos (rmd_date,idMember,Gender,do_name,idStyleOwner) values(@rmd_date,@idMember,@Gender,@DoName,@idStyleOwner)";
        //                cmd.Parameters.AddWithValue("@rmd_date", DateTime.Now);
        //                cmd.Parameters.AddWithValue("@idMember", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
        //                cmd.Parameters.AddWithValue("@Gender", Gender);
        //                cmd.Parameters.AddWithValue("@DoName", do_name);
        //                cmd.Parameters.AddWithValue("@idStyleOwner", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
        //                //cmd.Parameters.AddWithValue("@idStyleOwner", idStyleOwner);
        //                cmd.ExecuteNonQuery();
        //                MySqlDataAdapter da = new MySqlDataAdapter("select idDo,idMember from dos WHERE idMember = " + Request.Cookies["hr_main_ck"]["user_id"] + " AND idStyleOwner = " + Request.Cookies["hr_main_ck"]["user_id"] + " AND do_name = '" + DoName.Text.Trim().Replace("<", "").Replace(">", "").Replace("'", "''") + "'", oCon);
        //                da.Fill(dt);
        //                //Response.Write(dt.Rows[0]["idDo"].ToString());
        //                Int32 NewId = Convert.ToInt32(dt.Rows[0]["idDo"].ToString());
        //                Int32 IdMember = Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]);

        //                //Response.Write("DO ID: " + NewId);

        //                foreach (ListItem li in rbtServices.Items)
        //                {
        //                    if (li.Selected) { Worker.SqlInsert("INSERT INTO do_descriptions (idDo, Tag) VALUES (" + NewId + ", '" + li.Value + "')", connect_string); }
        //                }

        //                if (Service_Style.SelectedValue != "")
        //                {
        //                    Worker.SqlInsert("INSERT INTO do_service (idDo,idService) VALUES (" + NewId + "," + Service_Style.SelectedValue + ")", connect_string);
        //                }

        //                string mainDirectory = Server.MapPath("~/images/Users/" + IdMember + "/");
        //                DirectoryInfo dir = new DirectoryInfo(mainDirectory);
        //                if (chkDirectory(mainDirectory))
        //                {

        //                }
        //                else
        //                {
        //                    newDirectory(mainDirectory);
        //                    //Create The Directories and Add the Code for the task to be performed if All Directories Exists.
        //                }

        //                string SaveLocation = mainDirectory + NewId + ".jpg", temp_location = Server.MapPath("~/Req_Temp/") + NewId + ".jpg";
        //                tmp_fle_do.Value = temp_location;
        //                fle_id_do.Value = SaveLocation;
        //                try
        //                {
        //                    if (socialUpload)
        //                    {
        //                        System.Net.WebClient webClient = new System.Net.WebClient();
        //                        webClient.DownloadFile(photoid.Value, temp_location);
        //                    }
        //                    else if (ar[1].ToLower() != "jpg")
        //                    {
        //                        DoFileUpload.PostedFile.SaveAs(Server.MapPath("~/Req_Temp/") + fn);
        //                        System.Drawing.Image image1 = System.Drawing.Image.FromFile(Server.MapPath("~/Req_Temp/") + fn);
        //                        image1.Save(temp_location, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                        image1.Dispose();
        //                        File.Delete(Server.MapPath("~/Req_Temp/") + fn);
        //                    }
        //                    else
        //                    {
        //                        DoFileUpload.PostedFile.SaveAs(temp_location);
        //                    }
        //                    Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
        //                    image = System.Drawing.Image.FromStream(file_stream);
        //                    file_stream.Close();
        //                    file_stream.Dispose();
        //                    int full_width = image.Width, full_height = image.Height;

        //                    if (full_width > 510)
        //                    {
        //                        //ResizeXLargeDoImage(fullSizeImg); 
        //                        //Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
        //                        //var image = System.Drawing.Image.FromStream(file_stream);

        //                        double rat = (510.0 / Convert.ToDouble(image.Width)) * image.Height;
        //                        int newHeight = Convert.ToInt32(rat);
        //                        var newWidth = 510;
        //                        //Response.Write(newHeight);
        //                        var thumbnailBitmap = new System.Drawing.Bitmap(newWidth, newHeight);

        //                        var thumbnailGraph = System.Drawing.Graphics.FromImage(thumbnailBitmap);
        //                        thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                        thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //                        thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        //                        var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
        //                        thumbnailGraph.DrawImage(image, imageRectangle);
        //                        thumbnailBitmap.Save(temp_location.Replace(".jpg", "_x.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
        //                        thumbnailGraph.Dispose();
        //                        thumbnailBitmap.Dispose();
        //                        image.Dispose();

        //                        File.Copy(temp_location.Replace(".jpg", "_x.jpg"), temp_location, true);
        //                        File.Delete(temp_location.Replace(".jpg", "_x.jpg"));
        //                    }
        //                    else
        //                    {
        //                        image.Dispose();
        //                    }



        //                    if (full_height == full_width)
        //                    {
        //                        File.Copy(temp_location, SaveLocation, true);
        //                        File.Delete(temp_location);
        //                        DoError.Text = "Your Do Was Uploaded Successfully.";
        //                        DoError.Visible = true;
        //                        DoError.ForeColor = System.Drawing.Color.Red;
        //                        ViewDoProfile.Visible = true;
        //                        UploadAnotherStyle.Visible = true;
        //                        //NavigationMenu.BindDataList();
        //                        pnlDoUpload.Visible = false;
        //                        pnlDoCrop.Visible = false;
        //                        DoImage1.ImageUrl = "images/spacer.gif";
        //                        DoImage1.Width = Unit.Pixel(1);
        //                        DoImage1.Height = Unit.Pixel(1);
        //                        DoImage2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";
        //                    }
        //                    else
        //                    {
        //                        File.Copy(temp_location, temp_location.Replace(".jpg", "_x.jpg"), true);
        //                        DoImage1.ImageUrl = "Req_Temp/" + NewId + "_x.jpg";
        //                        DoError.Text = "Your Do image size is not compatible with our system. Please crop the image below.";
        //                        DoError.Visible = true;
        //                        DoError.ForeColor = System.Drawing.Color.Black;
        //                        Session["NewUpload"] = true;
        //                        //NavigationMenu.BindDataList();
        //                        pnlDoUpload.Visible = false;
        //                        pnlDoCrop.Visible = true;
        //                    }

        //                }
        //                catch (Exception ex)
        //                {
        //                    DeletePic(NewId);
        //                    Response.Write("Error: " + ex.Message + "</br>" + ex.InnerException + "</br>" + ex.Source);
        //                    includes.Functions.SendMail("errors@hairslayer", "2817943002@messaging.sprintpcs.com", "", "Error: " + ex.Message);
        //                }


        //        }    //End of Regular Upload
        //        else if (isMobileSafari())
        //        {
        //            //Response.Write(loc.Text);
        //            MobileSafariUpload();
        //            btnDoCrop.Visible = false;
        //        }
        //        else
        //        {
        //            DoError.Text = "Please select a photo for upload.";
        //            DoError.Visible = true;
        //            DoError.ForeColor = System.Drawing.Color.Red;
        //        }
        //    }

        //}

        protected void MobileSafariUpload()
        {
            string fn = Server.UrlDecode(loc.Text);
            //Response.Write("Decoded String: " + fn);
            string[] ar = new string[1];
            ar = fn.Split('.');
            if (!isCompatibleImage(ar[ar.Length - 1].ToLower()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Sorry this file format is not supported choose another,Choose another');</script>");
            }
            else
            {
                DataTable dt = new DataTable();
                // Int32 idMember = 1;
                string Gender = "M";
                if (Request.Cookies["hr_main_ck"]["membership"] == "2" || Request.Cookies["hr_main_ck"]["membership"] == "5" || Request.Cookies["hr_main_ck"]["membership"] == "7") { Gender = "F"; }
                //Int32 idStyleOwner = 2;
                string date = DateTime.Now.ToString();
                string datetime = date.Substring(0, date.Length - 2);
                MySqlConnection oCon = new MySqlConnection(connect_string);
                if (oCon.State == System.Data.ConnectionState.Closed)
                {
                    oCon.Open();
                }

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = oCon;
                //cmd.CommandText = "insert into dos (rmd_date,idMember,Gender,idStyleOwner) values(@rmd_date,@idMember,@Gender,@idStyleOwner)";
                cmd.CommandText = "insert into dos (rmd_date,idMember,Gender,do_name,idStyleOwner) values(@rmd_date,@idMember,@Gender,@DoName,@idStyleOwner)";
                cmd.Parameters.AddWithValue("@rmd_date", DateTime.Now);
                cmd.Parameters.AddWithValue("@idMember", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@DoName", DoName.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'"));
                cmd.Parameters.AddWithValue("@idStyleOwner", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
                //cmd.Parameters.AddWithValue("@idStyleOwner", idStyleOwner);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter("select Max(idDo),idMember from dos", oCon);
                da.Fill(dt);

                Int32 NewId = Convert.ToInt32(dt.Rows[0]["Max(idDo)"].ToString());
                Int32 IdMember = Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]);

                //Response.Write("DO ID: " + NewId);

                foreach (ListItem li in rbtServices.Items)
                {
                    if (li.Selected) { Worker.SqlInsert("INSERT INTO do_descriptions (idDo, Tag) VALUES (" + NewId + ", '" + li.Value + "')", connect_string); }
                }

                if (Service_Style.SelectedValue != "")
                {
                    Worker.SqlInsert("INSERT INTO do_service (idDo,idService) VALUES (" + NewId + "," + Service_Style.SelectedValue + ")", connect_string);
                }

                string mainDirectory = Server.MapPath("~/images/Users/" + IdMember + "/");
                DirectoryInfo dir = new DirectoryInfo(mainDirectory);
                if (chkDirectory(mainDirectory))
                {

                }
                else
                {
                    newDirectory(mainDirectory);
                    //Create The Directories and Add the Code for the task to be performed if All Directories Exists.
                }

                string SaveLocation = mainDirectory + NewId + ".jpg", temp_location = Server.MapPath("~/Req_Temp/") + NewId + ".jpg";
                tmp_fle_do.Value = temp_location;
                fle_id_do.Value = SaveLocation;
                try
                {
                    if (ar[ar.Length - 1].ToLower() != "jpg")
                    {
                        string scrub_location = Server.MapPath("~/Req_Temp/") + "temp." + ar[ar.Length - 1].ToLower();
                        System.Net.WebClient webClient = new System.Net.WebClient();
                        webClient.DownloadFile(fn, scrub_location);

                        System.Drawing.Image image1 = System.Drawing.Image.FromFile(scrub_location);
                        image1.Save(temp_location, System.Drawing.Imaging.ImageFormat.Jpeg);
                        image1.Dispose();
                        File.Delete(scrub_location);
                    }
                    else
                    {
                        System.Net.WebClient webClient = new System.Net.WebClient();

                        webClient.DownloadFile(fn, temp_location);
                    }

                    Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
                    var image = System.Drawing.Image.FromStream(file_stream);
                    //var image = System.Drawing.Image.FromFile(temp_location);
                    file_stream.Close();
                    file_stream.Dispose();
                    int full_width = image.Width, full_height = image.Height;

                    if (full_width > 510)
                    {
                        //ResizeXLargeDoImage(fullSizeImg); 
                        //Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
                        //var image = System.Drawing.Image.FromStream(file_stream);

                        double rat = (510.0 / Convert.ToDouble(image.Width)) * image.Height;
                        int newHeight = Convert.ToInt32(rat);
                        var newWidth = 510;
                        //Response.Write(newHeight);
                        var thumbnailBitmap = new System.Drawing.Bitmap(newWidth, newHeight);

                        var thumbnailGraph = System.Drawing.Graphics.FromImage(thumbnailBitmap);
                        thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                        thumbnailGraph.DrawImage(image, imageRectangle);
                        thumbnailBitmap.Save(temp_location.Replace(".jpg", "_x.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                        thumbnailGraph.Dispose();
                        thumbnailBitmap.Dispose();
                        image.Dispose();

                        File.Copy(temp_location.Replace(".jpg", "_x.jpg"), temp_location, true);
                        File.Delete(temp_location.Replace(".jpg", "_x.jpg"));
                    }
                    else
                    {
                        image.Dispose();
                    }



                    if (full_height == full_width)
                    {
                        File.Copy(temp_location, SaveLocation, true);
                        File.Delete(temp_location);
                        DoError.Text = "Your Do Was Uploaded Successfully.";
                        DoError.Visible = true;
                        DoError.ForeColor = System.Drawing.Color.Red;
                        //ViewDoProfile.Visible = true;
                        //UploadAnotherStyle.Visible = true;
                        ////NavigationMenu.BindDataList();
                        //pnlDoUpload.Visible = false;
                        //pnlDoCrop.Visible = false;
                        //DoImage1.ImageUrl = "images/spacer.gif";
                        //DoImage1.Width = Unit.Pixel(1);
                        //DoImage1.Height = Unit.Pixel(1);
                        //DoImage2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";
                    }
                    else
                    {
                        image = System.Drawing.Image.FromFile(temp_location);
                        SafariCrop(image, temp_location);

                        DoError.Text = "Does everything look ok?";
                        pnlDoUpload.Visible = false;
                        //pnlDoCrop.Visible = true;
                        //btnDoCrop.Visible = true;
                        //btnDoSave.Visible = true;
                        //ReUpload.Visible = true;
                        //btnDoGoBack.Visible = false;
                        //DoImage1.ImageUrl = "images/spacer.gif";
                        //DoImage1.Width = Unit.Pixel(1);
                        //DoImage1.Height = Unit.Pixel(1);
                        //DoImage2.ImageUrl = "Req_Temp/" + NewId + "_x.jpg";
                        /*File.Copy(temp_location, SaveLocation, true);
                        File.Delete(temp_location);
                        DoError.Text = "Your Do Was Uploaded Successfully.";
                        DoError.Visible = true;
                        DoError.ForeColor = System.Drawing.Color.Red;
                        ViewDoProfile.Visible = true;
                        UploadAnotherStyle.Visible = true;
                        //NavigationMenu.BindDataList();
                        pnlDoUpload.Visible = false;
                        pnlDoCrop.Visible = false;
                        DoImage1.ImageUrl = "images/spacer.gif";
                        DoImage1.Width = Unit.Pixel(1);
                        DoImage1.Height = Unit.Pixel(1);
                        DoImage2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";*/
                        image.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    DeletePic(NewId);
                    Response.Write("Error: " + ex.Message + "</br>" + ex.InnerException + "</br>" + ex.Source);
                    includes.Functions.SendMail("errors@hairslayer", "2817943002@messaging.sprintpcs.com", "", "Error: " + ex.Message);
                }
            }
        }

        protected void SafariCrop(System.Drawing.Image img, string temp_location)
        {
            int newHeight = 0, height_offset = 0, width_offset = 0;
            var newWidth = 0;
            //Response.Write(newHeight);

            if (img.Height > img.Width)
            {
                height_offset = Convert.ToInt32((img.Height - img.Width) / 2);
                newHeight = img.Width;
                newWidth = newHeight;
                //height_offset = height_offset - (height_offset * 2);
            }
            else if (img.Height < img.Width)
            {
                width_offset = Convert.ToInt32((img.Width - img.Height) / 2);
                newWidth = img.Height;
                newHeight = newWidth;
                //width_offset = width_offset - (width_offset * 2);
            }
            else
            {
                return;
            }

            var cropImage = new System.Drawing.Bitmap(img);
            var newcropImage = cropImage.Clone(new Rectangle(width_offset, height_offset, newWidth, newHeight), System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            newcropImage.Save(temp_location.Replace(".jpg", "_x.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);


            /*var thumbnailBitmap = new System.Drawing.Bitmap(newWidth, newHeight);

            var thumbnailGraph = System.Drawing.Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            var imageRectangle = new System.Drawing.Rectangle(width_offset, height_offset, newWidth, newHeight);
            //thumbnailGraph.DrawImage(img, imageRectangle);
            thumbnailGraph.DrawImage(img, width_offset, height_offset, newWidth, newHeight);
            thumbnailBitmap.Save(temp_location.Replace(".jpg", "_x.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();*/
            newcropImage.Dispose();
            cropImage.Dispose();
            img.Dispose();

            //File.Copy(temp_location.Replace(".jpg", "_x.jpg"), temp_location, true);
            //File.Delete(temp_location.Replace(".jpg", "_x.jpg"));
        }

        protected bool isMobileSafari()
        {
            if (Request.UserAgent.Contains("iPad") || Request.UserAgent.Contains("iPod") || Request.UserAgent.Contains("iPhone"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool InputIsGood()
        {
            if (DoName.Text.Trim() == "")
            {
                DoError.Text = "You must enter a name for your do.";
                DoError.Visible = true;
                return false;
            }

            if (rbtServices.SelectedIndex == -1 && Request.Cookies["hr_main_ck"]["membership"] != "3")
            {
                DoError.Text = "You must select 2 descriptions for your do.";
                DoError.Visible = true;
                return false;
            }
            return true;
        }

        public void ResizeXLargeDoImage(System.Drawing.Image image)
        {
            double rat = (370.0 / Convert.ToDouble(image.Width)) * image.Height;
            int newHeight = Convert.ToInt32(370.0 / Convert.ToDouble(image.Width));
            var newWidth = 370;

            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);

            thumbnailGraph.DrawImage(image, imageRectangle);

            thumbnailBitmap.Save(fle_id_do.Value.Replace(".jpg", "_tmp.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            File.Copy(fle_id_do.Value.Replace(".jpg", "_tmp.jpg"), fle_id_do.Value, true);
            File.Delete(fle_id_do.Value.Replace(".jpg", "_tmp.jpg"));

            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();

            /*System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(new_img);
            double rat = (370.0 / Convert.ToDouble(new_img.Width)) * new_img.Height;
            int new_height = Convert.ToInt32(370.0 / Convert.ToDouble(new_img.Width));

            System.Drawing.Image thumbnailImage = new_img.GetThumbnailImage(370, new_height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbNailCallBack), IntPtr.Zero);

            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(thumbnailImage);
            bm.Save(fle_id_do.Value.Replace(".jpg", "_tmp.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            new_img.Dispose();
            File.Copy(fle_id_do.Value.Replace(".jpg", "_tmp.jpg"), fle_id_do.Value, true);
            File.Delete(fle_id_do.Value.Replace(".jpg", "_tmp.jpg"));*/
        }

        //protected void btnDoCrop_Click(object sender, EventArgs e)
        //{
        //    //crop_photo.Crop(Server.MapPath(Image1.ImageUrl));
        //    DoError.Text = "Does everything look ok?";
        //    btnDoCrop.Visible = false;
        //    btnDoSave.Visible = true;
        //    btnDoGoBack.Visible = true;
        //    Dowci1.Crop(Server.MapPath(DoImage1.ImageUrl));
        //    DoImage1.Visible = false;
        //    DoImage2.Visible = true;
        //    DoImage2.ImageUrl = DoImage1.ImageUrl + "?rnd=" + (new Random()).Next();
        //}

        //protected void btnDoGoBack_Click(object sender, EventArgs e)
        //{
        //    btnDoCrop.Visible = true;
        //    btnDoSave.Visible = false;
        //    btnDoGoBack.Visible = false;
        //    DoImage1.Visible = true;
        //    DoImage2.Visible = false;
        //    File.Copy(tmp_fle_do.Value, tmp_fle_do.Value.Replace(".jpg", "_x.jpg"), true);
        //}

        //protected void btnDoSave_Click(object sender, EventArgs e)
        //{
        //    File.Copy(tmp_fle_do.Value.Replace(".jpg", "_x.jpg"), fle_id_do.Value, true);
        //    File.Delete(tmp_fle_do.Value);
        //    File.Delete(tmp_fle_do.Value.Replace(".jpg", "_x.jpg"));
        //    DoImage2.ImageUrl = "images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/" + Path.GetFileName(fle_id_do.Value);
        //    DoImage1.ImageUrl = "images/spacer.gif";
        //    DoImage1.Visible = true;
        //    DoImage1.Width = Unit.Pixel(1);
        //    DoImage1.Height = Unit.Pixel(1);
        //    DoError.Text = "Your Do Was Uploaded Successfully.";
        //    DoError.Visible = true;
        //    DoError.ForeColor = System.Drawing.Color.Red;
        //    btnDoSave.Visible = false;
        //    btnDoGoBack.Visible = false;
        //    ReUpload.Visible = false;
        //    ViewDoProfile.Visible = true;
        //    UploadAnotherStyle.Visible = true;
        //}

        //protected void ReUpload_Click(object sender, EventArgs e)
        //{
        //    pnlDoUpload.Visible = true;
        //    pnlDoCrop.Visible = false;
        //    btnDoSave.Visible = false;
        //    btnDoGoBack.Visible = false;
        //    ReUpload.Visible = false;
        //    DeletePic();
        //}

        protected void DeletePic()
        {
            File.Delete(tmp_fle_do.Value);
            File.Delete(tmp_fle_do.Value.Replace(".jpg", "_x.jpg"));
            int do_id = Convert.ToInt32(Path.GetFileName(fle_id_do.Value).Replace(".jpg", ""));
            MySqlConnection oConn = new MySqlConnection(connect_string);
            oConn.Open();
            MySqlCommand oComm = new MySqlCommand("sp_EraseDo", oConn);
            oComm.CommandType = CommandType.StoredProcedure;
            oComm.Parameters.AddWithValue("do_id", do_id);
            oComm.ExecuteNonQuery();
            oConn.Close();
        }

        protected void DeletePic(int do_id)
        {
            File.Delete(Server.MapPath("~/Req_Temp/") + do_id + ".jpg");
            File.Delete(Server.MapPath("~/Req_Temp/") + do_id + "_x.jpg");
            MySqlConnection oConn = new MySqlConnection(connect_string);
            oConn.Open();
            MySqlCommand oComm = new MySqlCommand("sp_EraseDo", oConn);
            oComm.CommandType = CommandType.StoredProcedure;
            oComm.Parameters.AddWithValue("do_id", do_id);
            oComm.ExecuteNonQuery();
            oConn.Close();
        }
        #endregion

        protected void UploadAnotherStyle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx?xid=x1");
        }

        protected void uploadimage_Click(object sender, EventArgs e)
        {
            //File.Copy(tmp_fle.Value.Replace(".jpg", "_x.jpg"), fle_id.Value, true);
            //File.Delete(tmp_fle.Value);
            //File.Delete(tmp_fle.Value.Replace(".jpg", "_x.jpg"));
            //Image2.ImageUrl = "images/spacer.gif";
            //Image1.ImageUrl = "images/spacer.gif";
            //Image1.Visible = true;
            //Image1.Width = Unit.Pixel(1);
            //Image1.Height = Unit.Pixel(1);
            //Error.Text = "Your Do Was Uploaded Successfully.";
            //Error.Visible = false;
            //Error.ForeColor = System.Drawing.Color.Red;
            ////btnSave.Visible = false;
            //btnGoBack.Visible = false;
            // imgprofilepic.ImageUrl = "images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/" + Path.GetFileName(fle_id.Value) + "?rnd=" + (new Random()).Next();
            //UploadProfile.Visible = false;
            //pnlUpload.Visible = true;
            //pnlCrop.Visible = false;
            //btnSave2.Visible = false;
            //FileUpload1.Dispose();
            //Response.Redirect("profile.aspx");
        }

        protected void savepic_Click(object sender, EventArgs e)
        {
          
         url = hdnpic1.Value;
       string rndm=  random_nogeneration();

         if (url == "")
         {
             Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Please choose image to upload ');</script>");
             return;
         
         }
            string idmember =Request.Cookies["hr_main_ck"]["user_id"];
            string idstyleowner = Request.Cookies["hr_main_ck"]["user_id"];
            string gender = Request.Cookies["hr_main_ck"]["gender"];
            string do_name=DoName.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'");
   
            string fileurl=url;

            // string sql = "select * from member where email='jaspreets.krishnais@gmail.com' and password='MTIzNDU2'";
            string sql = "select * from member where idmember=" + Request.Cookies["hr_main_ck"]["user_id"];
            MySqlConnection oConn = new MySqlConnection(connect_string);

            oConn.Open();
            MySqlCommand oComm = new MySqlCommand(sql, oConn);

            MySqlDataReader oDtr = oComm.ExecuteReader();
            if (oDtr.Read())
            {
                Response.Cookies["hr_main_ck"]["user_id"] = oDtr["idMember"].ToString();
                Response.Cookies["hr_main_ck"]["email"] = oDtr["email"].ToString();
                Response.Cookies["hr_main_ck"]["Gender"] = oDtr["gender"].ToString();
                oDtr.Close();
                string date =DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string datetime = date.Substring(0, date.Length - 2);
                MySqlConnection oCon = new MySqlConnection(connect_string);
                if (oCon.State == System.Data.ConnectionState.Closed)
                {
                    oCon.Open();
                }

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = oCon;
                cmd.CommandText = "insert into dos (rmd_date,idMember,idStyleOwner,Gender,do_name,ratemydo,fileurl,verified,random_number) values('"+date+"','"+idmember+"','"+idstyleowner+"','"+gender+"','"+do_name+"',false,'"+url+"',false,'"+rndm+"')";
              
                cmd.ExecuteNonQuery();
                lblstatus.Text = "Pic Uploaded";
                lblstatus.Visible = true;
                string msg1 = getdoid();

                if (txtemail.Text == "")
                {
                  

                }

                else
                {
                   
                    SmtpClient smtpClient = new SmtpClient();
                    MailMessage message = new MailMessage();
                    //smtpClient.Host = "mail.delaritech.com";
                    //smtpClient.Credentials = new System.Net.NetworkCredential("reports@delaritech.com", "deF12ault");
                    smtpClient.Host = "smtp.sendgrid.net";
                    smtpClient.Credentials = new System.Net.NetworkCredential("hairslayer", "H@irslayer1");
                    smtpClient.Port = 25;
                    message.IsBodyHtml = true;

                    //'''''Sending email to the applicant
                    message.From = new MailAddress("jaspreets.krishnais@gmail.com");
                    message.To.Add(txtemail.Text);


                    message.Subject = "Helllooo";
                    message.Body = msg1;
                    smtpClient.Send(message);
                }
                if (txtphone.Text == "")
                {

                }
                else
                {
                    string Account_sid ="AC91195ee5f1c44a03a5659b1a76abf81d";
                    string Auth_token = "c8ad332d8af394c0205f4a4a0cc33476";
                    TwilioRestClient client = new TwilioRestClient(Account_sid, Auth_token);
                    client.SendSmsMessage("(415)-723-4000", txtphone.Text, msg1);
                }
                DoName.Text = string.Empty;
                txtemail.Text = string.Empty;
                txtname.Text = string.Empty;
                txtphone.Text = string.Empty;
            }
        }
        public string getdoid()
        {
            string sql = "select idDo,random_number from dos where idDo in(select max(idDo) from dos)";
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
          string doidd=dt.Rows[0]["idDo"].ToString();
            string rndm1 = dt.Rows[0]["random_number"].ToString();
           msg = "Do has been uploaded.Please click on the link to get verified... http://www.hairslayer.com/Verified.aspx?doid=" + doidd +"&rndno="+rndm1;
             return msg;
        
        }
        Random r = new Random();
        public string random_nogeneration()
        {
            string n =Convert.ToString( r.Next());
            return n;
        }
    }
}