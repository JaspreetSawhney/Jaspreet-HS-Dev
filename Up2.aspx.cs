using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace HairSlayer
{
    public partial class Up2 : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            ViewProfile.NavigateUrl = "prov.aspx?id=" + Request.Cookies["hr_main_ck"]["user_id"];
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Error.Visible = false;
            string fn = "";
            if (InputIsGood())
            {
                //if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                if (FileUpload1.HasFile)
                {
                    fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
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
                        tmp_fle.Value = temp_location;
                        fle_id.Value = SaveLocation;
                        try
                        {
                            if (ar[1].ToLower() != "jpg")
                            {
                                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Req_Temp/") + fn);
                                System.Drawing.Image image1 = System.Drawing.Image.FromFile(Server.MapPath("~/Req_Temp/") + fn);
                                image1.Save(temp_location, System.Drawing.Imaging.ImageFormat.Jpeg);
                                image1.Dispose();
                                File.Delete(Server.MapPath("~/Req_Temp/") + fn);
                            }
                            else
                            {
                                FileUpload1.PostedFile.SaveAs(temp_location);
                            }
                            Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
                            image = System.Drawing.Image.FromStream(file_stream);
                            file_stream.Close();
                            file_stream.Dispose();
                            int full_width = image.Width, full_height = image.Height;

                            if (full_width > 510)
                            {
                                //ResizeXLargeImage(fullSizeImg); 
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
                                Error.Text = "Your Do Was Uploaded Successfully.";
                                Error.Visible = true;
                                Error.ForeColor = System.Drawing.Color.Red;
                                ViewProfile.Visible = true;
                                UploadAnotherStyle.Visible = true;
                                //NavigationMenu.BindDataList();
                                pnlUpload.Visible = false;
                                pnlCrop.Visible = false;
                                Image1.ImageUrl = "images/spacer.gif";
                                Image1.Width = Unit.Pixel(1);
                                Image1.Height = Unit.Pixel(1);
                                Image2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";
                            }
                            else
                            {
                                File.Copy(temp_location, temp_location.Replace(".jpg", "_x.jpg"), true);
                                Image1.ImageUrl = "Req_Temp/" + NewId + "_x.jpg";
                                Error.Text = "Your Do image size is not compatible with our system. Please crop the image below.";
                                Error.Visible = true;
                                Error.ForeColor = System.Drawing.Color.Black;
                                Session["NewUpload"] = true;
                                //NavigationMenu.BindDataList();
                                pnlUpload.Visible = false;
                                pnlCrop.Visible = true;
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
                    btnCrop.Visible = false;
                }
            }

        }

        protected void MobileSafariUpload()
        {
            string fn = Server.UrlDecode(loc.Text);
            //Response.Write("Decoded String: " + fn);
            string[] ar = new string[1];
            ar = fn.Split('.');
            if (!isCompatibleImage(ar[ar.Length-1].ToLower()))
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
                tmp_fle.Value = temp_location;
                fle_id.Value = SaveLocation;
                try
                {
                    if (ar[ar.Length - 1].ToLower() != "jpg")
                    {
                        string scrub_location = Server.MapPath("~/Req_Temp/") + "temp." + ar[ar.Length - 1].ToLower();
                        System.Net.WebClient webClient = new WebClient();
                        webClient.DownloadFile(fn, scrub_location);

                        System.Drawing.Image image1 = System.Drawing.Image.FromFile(scrub_location);
                        image1.Save(temp_location, System.Drawing.Imaging.ImageFormat.Jpeg);
                        image1.Dispose();
                        File.Delete(scrub_location);
                    }
                    else
                    {
                        System.Net.WebClient webClient = new WebClient();

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
                        //ResizeXLargeImage(fullSizeImg); 
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
                        Error.Text = "Your Do Was Uploaded Successfully.";
                        Error.Visible = true;
                        Error.ForeColor = System.Drawing.Color.Red;
                        ViewProfile.Visible = true;
                        UploadAnotherStyle.Visible = true;
                        //NavigationMenu.BindDataList();
                        pnlUpload.Visible = false;
                        pnlCrop.Visible = false;
                        Image1.ImageUrl = "images/spacer.gif";
                        Image1.Width = Unit.Pixel(1);
                        Image1.Height = Unit.Pixel(1);
                        Image2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";
                    }
                    else
                    {
                        image = System.Drawing.Image.FromFile(temp_location);
                        SafariCrop(image, temp_location);

                        Error.Text = "Does everything look ok?";
                        pnlUpload.Visible = false;
                        pnlCrop.Visible = true;
                        btnCrop.Visible = true;
                        btnSave.Visible = true;
                        ReUpload.Visible = true;
                        btnGoBack.Visible = false;
                        Image1.ImageUrl = "images/spacer.gif";
                        Image1.Width = Unit.Pixel(1);
                        Image1.Height = Unit.Pixel(1);
                        Image2.ImageUrl = "Req_Temp/" + NewId + "_x.jpg";
                        /*File.Copy(temp_location, SaveLocation, true);
                        File.Delete(temp_location);
                        Error.Text = "Your Do Was Uploaded Successfully.";
                        Error.Visible = true;
                        Error.ForeColor = System.Drawing.Color.Red;
                        ViewProfile.Visible = true;
                        UploadAnotherStyle.Visible = true;
                        //NavigationMenu.BindDataList();
                        pnlUpload.Visible = false;
                        pnlCrop.Visible = false;
                        Image1.ImageUrl = "images/spacer.gif";
                        Image1.Width = Unit.Pixel(1);
                        Image1.Height = Unit.Pixel(1);
                        Image2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";*/
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

        public byte[] GetBytesFromUrl(string url)
        {
            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            //int i;
            using (BinaryReader br = new BinaryReader(stream))
            {
                //i = (int)(stream.Length);
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();
            return b;
        }

        public void WriteBytesToFile(string fileName, byte[] content)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(content);
            }
            finally
            {
                fs.Close();
                w.Close();
            }

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
                Error.Text = "You must enter a name for your do.";
                Error.Visible = true;
                return false;
            }

            if (rbtServices.SelectedIndex == -1)
            {
                Error.Text = "You must select 2 descriptions for your do.";
                Error.Visible = true;
                return false;
            }
            return true;
        }

        public static Boolean ThumbNailCallBack()
        {
            return true;
        }

        public void ResizeXLargeImage(System.Drawing.Image image)
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

            thumbnailBitmap.Save(fle_id.Value.Replace(".jpg", "_tmp.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            File.Copy(fle_id.Value.Replace(".jpg", "_tmp.jpg"), fle_id.Value, true);
            File.Delete(fle_id.Value.Replace(".jpg", "_tmp.jpg"));

            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();

            /*System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(new_img);
            double rat = (370.0 / Convert.ToDouble(new_img.Width)) * new_img.Height;
            int new_height = Convert.ToInt32(370.0 / Convert.ToDouble(new_img.Width));

            System.Drawing.Image thumbnailImage = new_img.GetThumbnailImage(370, new_height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbNailCallBack), IntPtr.Zero);

            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(thumbnailImage);
            bm.Save(fle_id.Value.Replace(".jpg", "_tmp.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            new_img.Dispose();
            File.Copy(fle_id.Value.Replace(".jpg", "_tmp.jpg"), fle_id.Value, true);
            File.Delete(fle_id.Value.Replace(".jpg", "_tmp.jpg"));*/
        }

        protected void btnCrop_Click(object sender, EventArgs e)
        {
            //crop_photo.Crop(Server.MapPath(Image1.ImageUrl));
            Error.Text = "Does everything look ok?";
            btnCrop.Visible = false;
            btnSave.Visible = true;
            btnGoBack.Visible = true;
            wci1.Crop(Server.MapPath(Image1.ImageUrl));
            Image1.Visible = false;
            Image2.Visible = true;
            Image2.ImageUrl = Image1.ImageUrl + "?rnd=" + (new Random()).Next();
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            btnCrop.Visible = true;
            btnSave.Visible = false;
            btnGoBack.Visible = false;
            Image1.Visible = true;
            Image2.Visible = false;
            File.Copy(tmp_fle.Value, tmp_fle.Value.Replace(".jpg", "_x.jpg"), true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            File.Copy(tmp_fle.Value.Replace(".jpg", "_x.jpg"), fle_id.Value, true);
            File.Delete(tmp_fle.Value);
            File.Delete(tmp_fle.Value.Replace(".jpg", "_x.jpg"));
            Image2.ImageUrl = "images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/" + Path.GetFileName(fle_id.Value);
            Image1.ImageUrl = "images/spacer.gif";
            Image1.Visible = true;
            Image1.Width = Unit.Pixel(1);
            Image1.Height = Unit.Pixel(1);
            Error.Text = "Your Do Was Uploaded Successfully.";
            Error.Visible = true;
            Error.ForeColor = System.Drawing.Color.Red;
            btnSave.Visible = false;
            btnGoBack.Visible = false;
            ReUpload.Visible = false;
            ViewProfile.Visible = true;
            UploadAnotherStyle.Visible = true;
        }

        protected void ReUpload_Click(object sender, EventArgs e)
        {
            pnlUpload.Visible = true;
            pnlCrop.Visible = false;
            btnSave.Visible = false;
            btnGoBack.Visible = false;
            ReUpload.Visible = false;
            DeletePic();
        }

        protected void DeletePic()
        {
            File.Delete(tmp_fle.Value);
            File.Delete(tmp_fle.Value.Replace(".jpg", "_x.jpg"));
            int do_id = Convert.ToInt32(Path.GetFileName(fle_id.Value).Replace(".jpg", ""));
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
    }
}