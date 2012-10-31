using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;

namespace HairSlayer
{
    public partial class Upload : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewProfile.NavigateUrl = "prov.aspx?id=" + Request.Cookies["hr_main_ck"]["user_id"];
            if (Request.Cookies["hr_main_ck"]["membership"] != "4" && Request.Cookies["hr_main_ck"]["membership"] != "5")
            {
                string sql = "SELECT idDo FROM dos WHERE idMember = " + Request.Cookies["hr_main_ck"]["user_id"];
                DataTable dt = Worker.SqlTransaction(sql, connect_string);
                if (dt.Rows.Count > 2) 
                { 
                    pnlUpload.Visible = false;
                    doInfo.Visible = false;
                    NoUploads.Visible = true;
                }
            }
        }

        protected void rbtProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] stylist = new[] { "Haircut", "Haircut w/Chemical Service", "Bang Trim", "Wash and Style", "Wash & Flatiron", "Press & Curl", "Up Do", "Twist", "Cornrows", "Deep Conditioning Treatments", "Permanent Waves", "Curls", "Spiral Perms", "Relaxers - Virgin", "Relaxers - Touch Up", "Relaxers - Touch Up w/Cut", "Relaxers - Halo Touch Up", "Weaves - Full Head", "Weaves - Half Head", "Weaves - Sewn in Tracks", "Weaves - Glue in Tracks", "Color - Virgin Color", "Color - Touch Up", "Color - Highlights", "Color - Corrective Color", "Color - Low Lights", "Color - Temporary Color Gloss", "Color - Clear Gloss", "Brow Wax", "Lip Wax", "Facial Wax", "Whole Face Wax Package" };
            string[] barber = new[] { "Haircut", "Haircut w/Beard Trim", "Child Haircut", "Designer Haircut", "Beard Trim", "Line Only", "Straight Razor Shave", "Scalp Treatments", "Shampoo" };
            switch (rbtProvider.SelectedValue)
            {
                case "Barber":
                    rbtServices.DataSource = barber;
                    rbtServices.DataBind();
                    lblDoServices.Visible = true;
                    break;
                case "Stylist":
                    rbtServices.DataSource = stylist;
                    rbtServices.DataBind();
                    lblDoServices.Visible = true;
                    break;
            }
            btnUpload.Visible = true;
            FileUpload1.Visible = true;
            doInfo.Visible = false;
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Error.Visible = false;
            string fn = "";
            if (rbtServices.SelectedIndex != -1)
            {
                //if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                if (FileUpload1.HasFile)
                {
                    fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string[] ar = new string[1];
                    ar = fn.Split('.');
                    if (ar[1] != "jpg")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>alert('Sorry this file format is not supported choose another,Choose another');</script>");
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        // Int32 idMember = 1;
                        string Gender = "M";
                        if (rbtProvider.SelectedValue == "Stylist") { Gender = "F"; }
                        Int32 idStyleOwner = 2;
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

                        string SaveLocation = mainDirectory + NewId + ".jpg", temp_location = Server.MapPath("~/Req_Fle/") + NewId + ".jpg";
                        tmp_fle.Value = temp_location;
                        fle_id.Value = SaveLocation;
                        try
                        {
                            FileUpload1.PostedFile.SaveAs(temp_location);
                            Stream file_stream = new FileStream(temp_location, FileMode.OpenOrCreate);
                            var image = System.Drawing.Image.FromStream(file_stream);
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
                                NavigationMenu.BindDataList();
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
                                Image1.ImageUrl = "Req_Fle/" + NewId + "_x.jpg";
                                Error.Text = "Your Do image size is not compatible with our system. Please crop the image below.";
                                Error.Visible = true;
                                Error.ForeColor = System.Drawing.Color.Black;
                                Session["NewUpload"] = true;
                                NavigationMenu.BindDataList();
                                pnlUpload.Visible = false;
                                pnlCrop.Visible = true;
                            }

                        }
                        catch (Exception ex)
                        {
                            Response.Write("Error: " + ex.Message + "</br>" + ex.InnerException + "</br>" + ex.Source);
                        }
                    }
                }
            }
            else
            {
                Error.Text = "You must select the services for your do.";
                Error.Visible = true;
            }

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
            ViewProfile.Visible = true;
        }
    }
}