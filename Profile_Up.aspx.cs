using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;

namespace HairSlayer
{
    public partial class Profile_Up : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!includes.Functions.is_logged_in()) { Response.Redirect("Login.aspx?id=FyQ"); }
            ViewDoProfile.NavigateUrl = "prov.aspx?id=" + Request.Cookies["hr_main_ck"]["user_id"];
            if (!includes.Functions.isPremiumAccount())
            {
                string sql = "SELECT idDo FROM dos WHERE idMember = " + Request.Cookies["hr_main_ck"]["user_id"];
                DataTable dt = Worker.SqlTransaction(sql, connect_string);
                if (dt.Rows.Count > 2)
                {
                    pnlDoUpload.Visible = false;
                    doInfo.Visible = false;
                    NoUploads.Visible = true;
                }
                //Response.Write(dt.Rows.Count + " - " + Request.Cookies["hr_main_ck"]["membership"] + " -- " + Request.Cookies["hr_main_ck"]["user_id"] + "<br>" + sql);
            }

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

        protected void btnDoUpload_Click(object sender, EventArgs e)
        {
            DoError.Visible = false;
            string fn = "";
            if (InputIsGood())
            {
                //if ((DoFileUpload.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                if (DoFileUpload.HasFile)
                {
                    fn = System.IO.Path.GetFileName(DoFileUpload.PostedFile.FileName);
                    string[] ar = new string[1];
                    ar = fn.Split('.');
                    System.Drawing.Image image;
                    if (!isCompatibleImage(ar[1].ToLower()))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Sorry this file format is not supported choose another,Choose another');</script>");
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        // Int32 idMember = 1;
                        string Gender = "M";
                        if (Request.Cookies["hr_main_ck"]["membership"] == "2" || Request.Cookies["hr_main_ck"]["membership"] == "5") { Gender = "F"; }
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
                            if (ar[1].ToLower() != "jpg")
                            {
                                DoFileUpload.PostedFile.SaveAs(Server.MapPath("~/Req_Temp/") + fn);
                                System.Drawing.Image image1 = System.Drawing.Image.FromFile(Server.MapPath("~/Req_Temp/") + fn);
                                image1.Save(temp_location, System.Drawing.Imaging.ImageFormat.Jpeg);
                                image1.Dispose();
                                File.Delete(Server.MapPath("~/Req_Temp/") + fn);
                            }
                            else
                            {
                                DoFileUpload.PostedFile.SaveAs(temp_location);
                            }
                            Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
                            image = System.Drawing.Image.FromStream(file_stream);
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
                                ViewDoProfile.Visible = true;
                                UploadAnotherStyle.Visible = true;
                                //NavigationMenu.BindDataList();
                                pnlDoUpload.Visible = false;
                                pnlDoCrop.Visible = false;
                                DoImage1.ImageUrl = "images/spacer.gif";
                                DoImage1.Width = Unit.Pixel(1);
                                DoImage1.Height = Unit.Pixel(1);
                                DoImage2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";
                            }
                            else
                            {
                                File.Copy(temp_location, temp_location.Replace(".jpg", "_x.jpg"), true);
                                DoImage1.ImageUrl = "Req_Temp/" + NewId + "_x.jpg";
                                DoError.Text = "Your Do image size is not compatible with our system. Please crop the image below.";
                                DoError.Visible = true;
                                DoError.ForeColor = System.Drawing.Color.Black;
                                Session["NewUpload"] = true;
                                //NavigationMenu.BindDataList();
                                pnlDoUpload.Visible = false;
                                pnlDoCrop.Visible = true;
                            }

                        }
                        catch (Exception ex)
                        {
                            DeletePic(NewId);
                            Response.Write("Error: " + ex.Message + "</br>" + ex.InnerException + "</br>" + ex.Source);
                            includes.Functions.SendMail("errors@hairslayer", "2817943002@messaging.sprintpcs.com", "", "Error: " + ex.Message);
                        }
                    }

                }    //End of Regular Upload
                else if (isMobileSafari())
                {
                    //Response.Write(loc.Text);
                    MobileSafariUpload();
                    btnDoCrop.Visible = false;
                }
            }

        }

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
                        ViewDoProfile.Visible = true;
                        UploadAnotherStyle.Visible = true;
                        //NavigationMenu.BindDataList();
                        pnlDoUpload.Visible = false;
                        pnlDoCrop.Visible = false;
                        DoImage1.ImageUrl = "images/spacer.gif";
                        DoImage1.Width = Unit.Pixel(1);
                        DoImage1.Height = Unit.Pixel(1);
                        DoImage2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";
                    }
                    else
                    {
                        image = System.Drawing.Image.FromFile(temp_location);
                        SafariCrop(image, temp_location);

                        DoError.Text = "Does everything look ok?";
                        pnlDoUpload.Visible = false;
                        pnlDoCrop.Visible = true;
                        btnDoCrop.Visible = true;
                        btnDoSave.Visible = true;
                        ReUpload.Visible = true;
                        btnDoGoBack.Visible = false;
                        DoImage1.ImageUrl = "images/spacer.gif";
                        DoImage1.Width = Unit.Pixel(1);
                        DoImage1.Height = Unit.Pixel(1);
                        DoImage2.ImageUrl = "Req_Temp/" + NewId + "_x.jpg";
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

            if (rbtServices.SelectedIndex == -1)
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

        protected void btnDoCrop_Click(object sender, EventArgs e)
        {
            //crop_photo.Crop(Server.MapPath(Image1.ImageUrl));
            DoError.Text = "Does everything look ok?";
            btnDoCrop.Visible = false;
            btnDoSave.Visible = true;
            btnDoGoBack.Visible = true;
            Dowci1.Crop(Server.MapPath(DoImage1.ImageUrl));
            DoImage1.Visible = false;
            DoImage2.Visible = true;
            DoImage2.ImageUrl = DoImage1.ImageUrl + "?rnd=" + (new Random()).Next();
        }

        protected void btnDoGoBack_Click(object sender, EventArgs e)
        {
            btnDoCrop.Visible = true;
            btnDoSave.Visible = false;
            btnDoGoBack.Visible = false;
            DoImage1.Visible = true;
            DoImage2.Visible = false;
            File.Copy(tmp_fle_do.Value, tmp_fle_do.Value.Replace(".jpg", "_x.jpg"), true);
        }

        protected void btnDoSave_Click(object sender, EventArgs e)
        {
            File.Copy(tmp_fle_do.Value.Replace(".jpg", "_x.jpg"), fle_id_do.Value, true);
            File.Delete(tmp_fle_do.Value);
            File.Delete(tmp_fle_do.Value.Replace(".jpg", "_x.jpg"));
            DoImage2.ImageUrl = "images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/" + Path.GetFileName(fle_id_do.Value);
            DoImage1.ImageUrl = "images/spacer.gif";
            DoImage1.Visible = true;
            DoImage1.Width = Unit.Pixel(1);
            DoImage1.Height = Unit.Pixel(1);
            DoError.Text = "Your Do Was Uploaded Successfully.";
            DoError.Visible = true;
            DoError.ForeColor = System.Drawing.Color.Red;
            btnDoSave.Visible = false;
            btnDoGoBack.Visible = false;
            ReUpload.Visible = false;
            ViewDoProfile.Visible = true;
            UploadAnotherStyle.Visible = true;
        }

        protected void ReUpload_Click(object sender, EventArgs e)
        {
            pnlDoUpload.Visible = true;
            pnlDoCrop.Visible = false;
            btnDoSave.Visible = false;
            btnDoGoBack.Visible = false;
            ReUpload.Visible = false;
            DeletePic();
        }

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

        protected void About_Click(object sender, EventArgs e)
        {
            Response.Redirect("profile.aspx");
        }

        protected void Styles_Click(object sender, EventArgs e)
        {
            Response.Redirect("profile.aspx");
        }

        protected void Service_Click(object sender, EventArgs e)
        {
            Response.Redirect("profile.aspx");
        }

        protected void Schedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("profile.aspx");
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            Response.Redirect("profile_upload.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

        }

        protected string HostIpToLocation()
        {
            string sourceIP = string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"])
            ? Request.ServerVariables["REMOTE_ADDR"]
            : Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string url = "http://api.ipinfodb.com/v2/ip_query.php?key={0}&ip={1}&timezone=true";

            url = String.Format(url, "90d4f5e07ed75a6ed8ad13221d88140001aebf6730eec98978151d2a455d5e95", sourceIP);

            System.Net.HttpWebRequest httpWRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse httpWResponse = (System.Net.HttpWebResponse)httpWRequest.GetResponse();

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