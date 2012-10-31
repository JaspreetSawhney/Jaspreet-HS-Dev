using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Net;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.Mail;
using Twilio;
using RestSharp;

namespace HairSlayer.UserControls
{
    public partial class DoUpload : System.Web.UI.UserControl
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
        string url;
         string msg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] stylist = new[] { "Haircut", "Haircut w/Chemical Service", "Bang Trim", "Wash and Style", "Wash & Flatiron", "Press & Curl", "Up Do", "Twist", "Cornrows", "Deep Conditioning Treatments", "Permanent Waves", "Curls", "Spiral Perms", "Relaxers - Virgin", "Relaxers - Touch Up", "Relaxers - Touch Up w/Cut", "Relaxers - Halo Touch Up", "Weaves - Full Head", "Weaves - Half Head", "Weaves - Sewn in Tracks", "Weaves - Glue in Tracks", "Color - Virgin Color", "Color - Touch Up", "Color - Highlights", "Color - Corrective Color", "Color - Low Lights", "Color - Temporary Color Gloss", "Color - Clear Gloss", "Brow Wax", "Lip Wax", "Facial Wax", "Whole Face Wax Package" };
                string[] barber = new[] { "Haircut", "Haircut w/Beard Trim", "Child Haircut", "Designer Haircut", "Beard Trim", "Line Only", "Straight Razor Shave", "Scalp Treatments", "Shampoo" };
                
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

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Error.Visible = false;
        //        string fn = "";
        //        if (InputIsGood())
        //        {
        //            //if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
        //            if (FileUpload1.HasFile)
        //            {
        //                fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        //                string[] ar = new string[1];
        //                ar = fn.Split('.');
        //                System.Drawing.Image image;
        //                if (!isCompatibleImage(ar[1].ToLower()))
        //                {
        //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Sorry this file format is not supported choose another,Choose another');</script>");
        //                }
        //                else
        //                {
        //                    DataTable dt = new DataTable();

        //                    string Gender = ServiceProvider.SelectedValue;
        //                    string date = DateTime.Now.ToString();
        //                    string datetime = date.Substring(0, date.Length - 2);
        //                    MySqlConnection oCon = new MySqlConnection(connect_string);
        //                    if (oCon.State == System.Data.ConnectionState.Closed)
        //                    {
        //                        oCon.Open();
        //                    }

        //                    MySqlCommand cmd = new MySqlCommand();
        //                    cmd.Connection = oCon;
        //                    cmd.CommandText = "insert into dos (rmd_date,idMember,Gender,do_name,idStyleOwner,ratemydo,rmd_date) values(@rmd_date,@idMember,@Gender,@DoName,@idStyleOwner,@ratemydo,@rmd_date)";
        //                    cmd.Parameters.AddWithValue("@rmd_date", DateTime.Now);
        //                    cmd.Parameters.AddWithValue("@idMember", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
        //                    cmd.Parameters.AddWithValue("@Gender", Gender);
        //                    cmd.Parameters.AddWithValue("@DoName", DoName.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'"));
        //                    cmd.Parameters.AddWithValue("@idStyleOwner", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
        //                    cmd.Parameters.AddWithValue("@ratemydo", true);
        //                    cmd.Parameters.AddWithValue("@rmd_date", DateTime.Now);
        //                    //cmd.Parameters.AddWithValue("@idStyleOwner", idStyleOwner);
        //                    cmd.ExecuteNonQuery();
        //                    MySqlDataAdapter da = new MySqlDataAdapter("select Max(idDo),idMember from dos", oCon);
        //                    da.Fill(dt);

        //                    Int32 NewId = Convert.ToInt32(dt.Rows[0]["Max(idDo)"].ToString());
        //                    Int32 IdMember = Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]);

        //                    //Response.Write("DO ID: " + NewId);

        //                    /*foreach (ListItem li in rbtServices.Items)
        //                    {
        //                        if (li.Selected) { Worker.SqlInsert("INSERT INTO do_descriptions (idDo, Tag) VALUES (" + NewId + ", '" + li.Value + "')", connect_string); }
        //                    }*/

        //                    string mainDirectory = Server.MapPath("~/images/Users/" + IdMember + "/");
        //                    DirectoryInfo dir = new DirectoryInfo(mainDirectory);
        //                    if (chkDirectory(mainDirectory))
        //                    {

        //                    }
        //                    else
        //                    {
        //                        newDirectory(mainDirectory);
        //                        //Create The Directories and Add the Code for the task to be performed if All Directories Exists.
        //                    }

        //                    string SaveLocation = mainDirectory + NewId + ".jpg", temp_location = Server.MapPath("~/Req_Temp/") + NewId + ".jpg";
        //                    tmp_fle.Value = temp_location;
        //                    fle_id.Value = SaveLocation;
        //                    try
        //                    {
        //                        if (ar[1].ToLower() != "jpg")
        //                        {
        //                            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Req_Temp/") + fn);
        //                            System.Drawing.Image image1 = System.Drawing.Image.FromFile(Server.MapPath("~/Req_Temp/") + fn);
        //                            image1.Save(temp_location, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                            image1.Dispose();
        //                            File.Delete(Server.MapPath("~/Req_Temp/") + fn);
        //                        }
        //                        else
        //                        {
        //                            FileUpload1.PostedFile.SaveAs(temp_location);
        //                        }
        //                        Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
        //                        image = System.Drawing.Image.FromStream(file_stream);
        //                        file_stream.Close();
        //                        file_stream.Dispose();
        //                        int full_width = image.Width, full_height = image.Height;

        //                        if (full_width > 510)
        //                        {
        //                            //ResizeXLargeImage(fullSizeImg); 
        //                            //Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
        //                            //var image = System.Drawing.Image.FromStream(file_stream);

        //                            double rat = (510.0 / Convert.ToDouble(image.Width)) * image.Height;
        //                            int newHeight = Convert.ToInt32(rat);
        //                            var newWidth = 510;
        //                            //Response.Write(newHeight);
        //                            var thumbnailBitmap = new System.Drawing.Bitmap(newWidth, newHeight);

        //                            var thumbnailGraph = System.Drawing.Graphics.FromImage(thumbnailBitmap);
        //                            thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                            thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //                            thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        //                            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
        //                            thumbnailGraph.DrawImage(image, imageRectangle);
        //                            thumbnailBitmap.Save(temp_location.Replace(".jpg", "_x.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
        //                            thumbnailGraph.Dispose();
        //                            thumbnailBitmap.Dispose();
        //                            image.Dispose();

        //                            File.Copy(temp_location.Replace(".jpg", "_x.jpg"), temp_location, true);
        //                            File.Delete(temp_location.Replace(".jpg", "_x.jpg"));
        //                        }
        //                        else
        //                        {
        //                            image.Dispose();
        //                        }



        //                        if (full_height == full_width)
        //                        {
        //                            File.Copy(temp_location, SaveLocation, true);
        //                            File.Delete(temp_location);
        //                            doInfo.Visible = false;
        //                            Error.Text = "CONGRATULATIONS!<br/><br/>Your Do Has Been Submitted to Rate My Do&trade;."; ;
        //                            Error.Visible = true;
        //                            Error.ForeColor = System.Drawing.Color.Red;
        //                            pnlUpload.Visible = false;
        //                            pnlCrop.Visible = false;
        //                            Image1.ImageUrl = "../images/spacer.gif";
        //                            Image1.Width = Unit.Pixel(1);
        //                            Image1.Height = Unit.Pixel(1);
        //                            Image2.ImageUrl = "../images/Users/" + IdMember + "/" + NewId + ".jpg";
        //                            Response.Redirect("InviteProvider.aspx?id=" + NewId);
        //                        }
        //                        else
        //                        {
        //                            File.Copy(temp_location, temp_location.Replace(".jpg", "_x.jpg"), true);
        //                            Image1.ImageUrl = "/Req_Temp/" + NewId + "_x.jpg";
        //                            Error.Text = "Your Do image size is not compatible with our system. Please crop the image below.";
        //                            Error.Visible = true;
        //                            Error.ForeColor = System.Drawing.Color.Black;
        //                            Session["NewUpload"] = true;
        //                            //NavigationMenu.BindDataList();
        //                            pnlUpload.Visible = false;
        //                            pnlCrop.Visible = true;
        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        DeletePic(NewId);
        //                        Response.Write("Error: " + ex.Message + "</br>" + ex.InnerException + "</br>" + ex.Source);
        //                        includes.Functions.SendMail("errors@hairslayer", "5126805103@messaging.sprintpcs.com", "", "Error: " + ex.Message);
        //                    }
        //                }

        //            }    //End of Regular Upload
        //            else if (isMobileSafari())
        //            {
        //                //Response.Write(loc.Text);
        //                MobileSafariUpload();
        //                btnCrop.Visible = false;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        doInfo.Text = "There was an issue with your file upload. <br /><br /><a href=\"\">Click To Retry Your Upload</a>";
        //    }
        //}

        //protected void MobileSafariUpload()
        //{
        //    string fn = Server.UrlDecode(loc.Text);
        //    //Response.Write("Decoded String: " + fn);
        //    string[] ar = new string[1];
        //    ar = fn.Split('.');
        //    if (!isCompatibleImage(ar[ar.Length - 1].ToLower()))
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Sorry this file format is not supported choose another,Choose another');</script>");
        //    }
        //    else
        //    {
        //        DataTable dt = new DataTable();
               
        //        string Gender = ServiceProvider.SelectedValue;
        //        string date = DateTime.Now.ToString();
        //        string datetime = date.Substring(0, date.Length - 2);
        //        MySqlConnection oCon = new MySqlConnection(connect_string);
        //        if (oCon.State == System.Data.ConnectionState.Closed)
        //        {
        //            oCon.Open();
        //        }

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = oCon;
                
        //        cmd.CommandText = "insert into dos (rmd_date,idMember,Gender,do_name,idStyleOwner) values(@rmd_date,@idMember,@Gender,@DoName,@idStyleOwner)";
        //        cmd.Parameters.AddWithValue("@rmd_date", DateTime.Now);
        //        cmd.Parameters.AddWithValue("@idMember", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
        //        cmd.Parameters.AddWithValue("@Gender", Gender);
        //        cmd.Parameters.AddWithValue("@DoName", DoName.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'"));
        //        cmd.Parameters.AddWithValue("@idStyleOwner", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
        //        //cmd.Parameters.AddWithValue("@idStyleOwner", idStyleOwner);
        //        cmd.ExecuteNonQuery();
        //        MySqlDataAdapter da = new MySqlDataAdapter("select Max(idDo),idMember from dos", oCon);
        //        da.Fill(dt);

        //        Int32 NewId = Convert.ToInt32(dt.Rows[0]["Max(idDo)"].ToString());
        //        Int32 IdMember = Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]);

        //        //Response.Write("DO ID: " + NewId);

        //        /*foreach (ListItem li in rbtServices.Items)
        //        {
        //            if (li.Selected) { Worker.SqlInsert("INSERT INTO do_descriptions (idDo, Tag) VALUES (" + NewId + ", '" + li.Value + "')", connect_string); }
        //        }*/

        //        string mainDirectory = Server.MapPath("~/images/Users/" + IdMember + "/");
        //        DirectoryInfo dir = new DirectoryInfo(mainDirectory);
        //        if (chkDirectory(mainDirectory))
        //        {

        //        }
        //        else
        //        {
        //            newDirectory(mainDirectory);
        //            //Create The Directories and Add the Code for the task to be performed if All Directories Exists.
        //        }

        //        string SaveLocation = mainDirectory + NewId + ".jpg", temp_location = Server.MapPath("~/Req_Temp/") + NewId + ".jpg";
        //        tmp_fle.Value = temp_location;
        //        fle_id.Value = SaveLocation;
        //        try
        //        {
        //            if (ar[ar.Length - 1].ToLower() != "jpg")
        //            {
        //                string scrub_location = Server.MapPath("~/Req_Temp/") + "temp." + ar[ar.Length - 1].ToLower();
        //                System.Net.WebClient webClient = new WebClient();
        //                webClient.DownloadFile(fn, scrub_location);

        //                System.Drawing.Image image1 = System.Drawing.Image.FromFile(scrub_location);
        //                image1.Save(temp_location, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                image1.Dispose();
        //                File.Delete(scrub_location);
        //            }
        //            else
        //            {
        //                System.Net.WebClient webClient = new WebClient();

        //                webClient.DownloadFile(fn, temp_location);
        //            }

        //            Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
        //            var image = System.Drawing.Image.FromStream(file_stream);
        //            //var image = System.Drawing.Image.FromFile(temp_location);
        //            file_stream.Close();
        //            file_stream.Dispose();
        //            int full_width = image.Width, full_height = image.Height;

        //            if (full_width > 510)
        //            {
        //                //ResizeXLargeImage(fullSizeImg); 
        //                //Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
        //                //var image = System.Drawing.Image.FromStream(file_stream);

        //                double rat = (510.0 / Convert.ToDouble(image.Width)) * image.Height;
        //                int newHeight = Convert.ToInt32(rat);
        //                var newWidth = 510;
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



        //            if (full_height == full_width)
        //            {
        //                File.Copy(temp_location, SaveLocation, true);
        //                File.Delete(temp_location);
        //                Error.Text = "CONGRATULATIONS!<br/><br/>Your Do Has Been Submitted to Rate My Do&trade;.";
        //                Error.Visible = true;
        //                Error.ForeColor = System.Drawing.Color.Red;
        //                doInfo.Visible = false;
        //                //NavigationMenu.BindDataList();
        //                pnlUpload.Visible = false;
        //                pnlCrop.Visible = false;
        //                Image1.ImageUrl = "images/spacer.gif";
        //                Image1.Width = Unit.Pixel(1);
        //                Image1.Height = Unit.Pixel(1);
        //                Image2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";
        //                Response.Redirect("InviteProvider.aspx?id=" + NewId);
        //            }
        //            else
        //            {
        //                image = System.Drawing.Image.FromFile(temp_location);
        //                SafariCrop(image, temp_location);

        //                Error.Text = "Does everything look ok?";
        //                pnlUpload.Visible = false;
        //                pnlCrop.Visible = true;
        //                btnCrop.Visible = true;
        //                btnSave.Visible = true;
        //                ReUpload.Visible = true;
        //                btnGoBack.Visible = false;
        //                Image1.ImageUrl = "images/spacer.gif";
        //                Image1.Width = Unit.Pixel(1);
        //                Image1.Height = Unit.Pixel(1);
        //                Image2.ImageUrl = "Req_Temp/" + NewId + "_x.jpg";
        //                /*File.Copy(temp_location, SaveLocation, true);
        //                File.Delete(temp_location);
        //                Error.Text = "Your Do Was Uploaded Successfully.";
        //                Error.Visible = true;
        //                Error.ForeColor = System.Drawing.Color.Red;
        //                ViewProfile.Visible = true;
        //                UploadAnotherStyle.Visible = true;
        //                //NavigationMenu.BindDataList();
        //                pnlUpload.Visible = false;
        //                pnlCrop.Visible = false;
        //                Image1.ImageUrl = "images/spacer.gif";
        //                Image1.Width = Unit.Pixel(1);
        //                Image1.Height = Unit.Pixel(1);
        //                Image2.ImageUrl = "images/Users/" + IdMember + "/" + NewId + ".jpg";*/
        //                image.Dispose();
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            DeletePic(NewId);
        //            Response.Write("Error: " + ex.Message + "</br>" + ex.InnerException + "</br>" + ex.Source);
        //            includes.Functions.SendMail("errors@hairslayer", "5126805103@messaging.sprintpcs.com", "", "Error: " + ex.Message);
        //        }
        //    }
        //}

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

            newcropImage.Dispose();
            cropImage.Dispose();
            img.Dispose();
        }

        public byte[] GetBytesFromUrl(string url)
        {
            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            
            using (BinaryReader br = new BinaryReader(stream))
            {
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

            if (ServiceProvider.SelectedIndex == -1)
            {
                Error.Text = "You must service provider type for your do.";
                Error.Visible = true;
                return false;
            }

            /*if (rbtServices.SelectedIndex == -1)
            {
                Error.Text = "You must select 2 descriptions for your do.";
                Error.Visible = true;
                return false;
            }*/
            return true;
        }

        public static Boolean ThumbNailCallBack()
        {
            return true;
        }

        //public void ResizeXLargeImage(System.Drawing.Image image)
        //{
        //    double rat = (370.0 / Convert.ToDouble(image.Width)) * image.Height;
        //    int newHeight = Convert.ToInt32(370.0 / Convert.ToDouble(image.Width));
        //    var newWidth = 370;

        //    var thumbnailBitmap = new Bitmap(newWidth, newHeight);

        //    var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
        //    thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //    thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //    thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        //    var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);

        //    thumbnailGraph.DrawImage(image, imageRectangle);

        //    thumbnailBitmap.Save(fle_id.Value.Replace(".jpg", "_tmp.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

        //    File.Copy(fle_id.Value.Replace(".jpg", "_tmp.jpg"), fle_id.Value, true);
        //    File.Delete(fle_id.Value.Replace(".jpg", "_tmp.jpg"));

        //    thumbnailGraph.Dispose();
        //    thumbnailBitmap.Dispose();
        //    image.Dispose();

        //    /*System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(new_img);
        //    double rat = (370.0 / Convert.ToDouble(new_img.Width)) * new_img.Height;
        //    int new_height = Convert.ToInt32(370.0 / Convert.ToDouble(new_img.Width));

        //    System.Drawing.Image thumbnailImage = new_img.GetThumbnailImage(370, new_height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbNailCallBack), IntPtr.Zero);

        //    System.Drawing.Bitmap bm = new System.Drawing.Bitmap(thumbnailImage);
        //    bm.Save(fle_id.Value.Replace(".jpg", "_tmp.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

        //    new_img.Dispose();
        //    File.Copy(fle_id.Value.Replace(".jpg", "_tmp.jpg"), fle_id.Value, true);
        //    File.Delete(fle_id.Value.Replace(".jpg", "_tmp.jpg"));*/
        //}

        //protected void btnCrop_Click(object sender, EventArgs e)
        //{
        //    //crop_photo.Crop(Server.MapPath(Image1.ImageUrl));
        //    Error.Text = "Does everything look ok?";
        //    btnCrop.Visible = false;
        //    btnSave.Visible = true;
        //    btnGoBack.Visible = true;
        //    wci1.Crop(Server.MapPath(Image1.ImageUrl));
        //    Image1.Visible = false;
        //    Image2.Visible = true;
        //    Image2.ImageUrl = Image1.ImageUrl + "?rnd=" + (new Random()).Next();
        //}

        //protected void btnGoBack_Click(object sender, EventArgs e)
        //{
        //    btnCrop.Visible = true;
        //    btnSave.Visible = false;
        //    btnGoBack.Visible = false;
        //    Image1.Visible = true;
        //    Image2.Visible = false;
        //    File.Copy(tmp_fle.Value, tmp_fle.Value.Replace(".jpg", "_x.jpg"), true);
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    File.Copy(tmp_fle.Value.Replace(".jpg", "_x.jpg"), fle_id.Value, true);
        //    File.Delete(tmp_fle.Value);
        //    File.Delete(tmp_fle.Value.Replace(".jpg", "_x.jpg"));
        //    Image2.ImageUrl = "/images/Users/" + Request.Cookies["hr_main_ck"]["user_id"] + "/" + Path.GetFileName(fle_id.Value);
        //    Image1.ImageUrl = "/images/spacer.gif";
        //    Image1.Visible = true;
        //    Image1.Width = Unit.Pixel(1);
        //    Image1.Height = Unit.Pixel(1);
        //    doInfo.Visible = false;
        //    Error.Text = "CONGRATULATIONS!<br/><br/>Your Do Has Been Submitted to Rate My Do&trade;.";
        //    Error.Visible = true;
        //    Error.ForeColor = System.Drawing.Color.Red;
        //    btnSave.Visible = false;
        //    btnGoBack.Visible = false;
        //    ReUpload.Visible = false;
        //    Response.Redirect("InviteProvider.aspx?id=" + tmp_fle.Value.Replace(Server.MapPath("~/Req_Temp/"), "").Replace(".jpg", ""));
        //}

        //protected void ReUpload_Click(object sender, EventArgs e)
        //{
        //    pnlUpload.Visible = true;
        //    pnlCrop.Visible = false;
        //    btnSave.Visible = false;
        //    btnGoBack.Visible = false;
        //    ReUpload.Visible = false;
        //    DeletePic();
        //}

        //protected void DeletePic()
        //{
        //    File.Delete(tmp_fle.Value);
        //    File.Delete(tmp_fle.Value.Replace(".jpg", "_x.jpg"));
        //    int do_id = Convert.ToInt32(Path.GetFileName(fle_id.Value).Replace(".jpg", ""));
        //    MySqlConnection oConn = new MySqlConnection(connect_string);
        //    oConn.Open();
        //    MySqlCommand oComm = new MySqlCommand("sp_EraseDo", oConn);
        //    oComm.CommandType = CommandType.StoredProcedure;
        //    oComm.Parameters.AddWithValue("do_id", do_id);
        //    oComm.ExecuteNonQuery();
        //    oConn.Close();
        //}

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

        protected void ServiceProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServiceProvider.SelectedValue == "M")
            {                
                string[] barber = new[] { "Haircut", "Haircut w/Beard Trim", "Child Haircut", "Designer Haircut", "Beard Trim", "Line Only", "Straight Razor Shave", "Scalp Treatments", "Shampoo" };
                //rbtServices.DataSource = barber;
                //rbtServices.DataBind();
            }
            else
            {
                string[] stylist = new[] { "Haircut", "Haircut w/Chemical Service", "Bang Trim", "Wash and Style", "Wash & Flatiron", "Press & Curl", "Up Do", "Twist", "Cornrows", "Deep Conditioning Treatments", "Permanent Waves", "Curls", "Spiral Perms", "Relaxers - Virgin", "Relaxers - Touch Up", "Relaxers - Touch Up w/Cut", "Relaxers - Halo Touch Up", "Weaves - Full Head", "Weaves - Half Head", "Weaves - Sewn in Tracks", "Weaves - Glue in Tracks", "Color - Virgin Color", "Color - Touch Up", "Color - Highlights", "Color - Corrective Color", "Color - Low Lights", "Color - Temporary Color Gloss", "Color - Clear Gloss", "Brow Wax", "Lip Wax", "Facial Wax", "Whole Face Wax Package" };
                //rbtServices.DataSource = stylist;
                //rbtServices.DataBind();
                //rbtServices.RepeatColumns = 3;
            }
        }

        protected void savepic_Click(object sender, EventArgs e)
        {
            string rndm = random_nogeneration();
            string url = hdnpic1.Value;
                if (url == "")
         {
             Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Please choose image to upload ');</script>");
             return;
         }
            string idmember = Request.Cookies["hr_main_ck"]["user_id"];
            string idstyleowner = Request.Cookies["hr_main_ck"]["user_id"];
            string gender = Request.Cookies["hr_main_ck"]["gender"];
            string do_name = DoName.Text.Trim().Replace("<", "").Replace(">", "").Replace("''", "'");

            //string fileurl = url;

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
                string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                MySqlConnection oCon = new MySqlConnection(connect_string);
                if (oCon.State == System.Data.ConnectionState.Closed)
                {
                    oCon.Open();
                }

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = oCon;
                cmd.CommandText = "insert into dos (rmd_date,idMember,idStyleOwner,Gender,do_name,ratemydo,fileurl,verified,random_number) values('" + date + "','" + idmember + "','" + idstyleowner + "','" + gender + "','" + do_name + "',false,'" + url + "',false,'"+rndm+"')";

                cmd.ExecuteNonQuery();
                //email.Visible = true;
                if (txtemail.Text == "")
                {
                }
                else
                {
                    getdoid();
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
                    message.Body = msg;
                    smtpClient.Send(message);
                }
                if (txtphone.Text == "")
                {
                }
                else
                {
                    getdoid();
                    string Account_sid = "AC91195ee5f1c44a03a5659b1a76abf81d";
                    string Auth_token = "c8ad332d8af394c0205f4a4a0cc33476";
                    TwilioRestClient client = new TwilioRestClient(Account_sid, Auth_token);
                    client.SendSmsMessage("(415)-723-4000", txtphone.Text, msg);
                }
                DoName.Text = string.Empty;
                txtemail.Text = string.Empty;
                txtname.Text = string.Empty;
                txtphone.Text = string.Empty;
            }
        }
              public void getdoid()
        {
            string sql = "select idDo,random_number from dos where idDo in(select max(idDo) from dos)";
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            string doidd = dt.Rows[0]["idDo"].ToString();
            string rndm1 = dt.Rows[0]["random_number"].ToString();
            msg = "Do has been uploaded.Please click on the link to get verified... http://www.hairslayer.com/Verified.aspx?doid=" + doidd + "&rndno=" + rndm1;
              }

              Random r = new Random();
              public string random_nogeneration()
              {
                  string n = Convert.ToString(r.Next());
                  return n;
              }

        }
    }
