using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace HairSlayer.includes
{
    public class Thumbnails
    {
        public static Boolean ThumbNailCallBack()
        {
            return true;
        }

        public static string CreateThumbnail(string target_file, int width, int height, string watermark)
        {
            //Get the image name – yourimage.jpg – from the query String
            //string imageUrl = @"C:\inetpub\wwwroot\DelariTech\hairslayer\Users\" + target_file, tst = "";
            string imageUrl = HttpContext.Current.Server.MapPath("~") + @"images\Users\" + target_file, tst = "";
  
            Image fullSizeImg = Image.FromFile(imageUrl);
 
            int CurrentimgHeight = fullSizeImg.Height;
            int CurrentimgWidth = fullSizeImg.Width;
            int crop_start = 0;
            Bitmap bmpImage = new Bitmap(fullSizeImg);

            if (CurrentimgHeight > CurrentimgWidth)
            {
                Rectangle recCropper = new Rectangle(0, crop_start, CurrentimgWidth, CurrentimgWidth);
                crop_start = (CurrentimgHeight - CurrentimgWidth) / 2;
                fullSizeImg = (Image)(bmpImage.Clone(recCropper, PixelFormat.Format24bppRgb));
            }
            else if (CurrentimgWidth > CurrentimgHeight)
            {
                Rectangle recCropper = new Rectangle(crop_start, 0, CurrentimgHeight, CurrentimgHeight);
                crop_start = (CurrentimgWidth - CurrentimgHeight) / 2;
                fullSizeImg = (Image)(bmpImage.Clone(recCropper, PixelFormat.Format24bppRgb));
            }
            


            //This will only work for jpeg images

            if (height > 0 && width > 0)
            {
                //Image.GetThumbnailImageAbort dummyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                System.Drawing.Image thumbnailImage = fullSizeImg.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbNailCallBack), IntPtr.Zero);
    
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(thumbnailImage);
		
		        SizeF StringSizeF; Single DesiredWidth; Font wmFont; Single RequiredFontSize; Single Ratio;
		
	            //Set the watermark font	 
		
                wmFont = new Font("Verdana", 14, FontStyle.Bold);
                DesiredWidth = width * Convert.ToSingle(.5);
	
	            //use the MeasureString method to posi  tion the watermark in the centre of the image
	
	            StringSizeF = g.MeasureString(watermark, wmFont);
	            Ratio = StringSizeF.Width / wmFont.SizeInPoints;
	            RequiredFontSize = DesiredWidth / Ratio;
	            wmFont = new Font("Verdana", RequiredFontSize, FontStyle.Bold);
                //Sets the interpolation mode for a high quality image
		
		        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
		        g.DrawImage(fullSizeImg, 0, 0, width, height);
		        g.SmoothingMode = SmoothingMode.HighQuality;
		 
		        SolidBrush letterBrush = new SolidBrush(Color.FromArgb(50, 255, 255, 255));
		        SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0));
	
	            //'Enter the watermark text 

		        g.DrawString(watermark, wmFont, shadowBrush, 75, Convert.ToSingle((height * .5) - 36));               
		        g.DrawString(watermark, wmFont, letterBrush, 77, Convert.ToSingle((height * .5) - 38));

                //thumbnailImage.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
                string file_name = "j_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("AM", "").Replace("PM", "") + ".jpg";
                //string out_path = @"C:\inetpub\wwwroot\DelariTech\hairslayer\Req_Fle\" + file_name;
                string out_path = HttpContext.Current.Server.MapPath("~") + @"Req_Fle\" + file_name;
                /*Bitmap bm = new Bitmap(thumbnailImage);
                bm.Save(out_path, ImageFormat.Jpeg);*/

                thumbnailImage.Save(out_path, ImageFormat.Jpeg);

                fullSizeImg.Dispose();

                /*string out_path2 = @"C:\inetpub\wwwroot\DelariTech\hairslayer\Req_Fle\" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("AM", "").Replace("PM", "") + ".txt";
                System.IO.File.WriteAllText(out_path2, "Testing valid path & permissions.");*/


                return "Req_Fle/" + file_name;
            }
            else
            {
                //fullSizeImg.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
                /*string out_path = @"Req_Fle\" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("AM", "").Replace("PM", "") + ".jpg";
                fullSizeImg.Save(out_path, ImageFormat.Jpeg);
                fullSizeImg.Dispose();*/

                return tst;
            }

        }

        public static string CreateThumbnail(string target_file, int width, int height)
        {
            //Get the image name – yourimage.jpg – from the query String

            string imageUrl = HttpContext.Current.Server.MapPath("~") + @"images\Users\" + target_file, tst = "";

            Image fullSizeImg = Image.FromFile(imageUrl);

            int CurrentimgHeight = fullSizeImg.Height;
            int CurrentimgWidth = fullSizeImg.Width;
            int crop_start = 0;
            Bitmap bmpImage = new Bitmap(fullSizeImg);

            if (CurrentimgHeight > CurrentimgWidth)
            {
                Rectangle recCropper = new Rectangle(0, crop_start, CurrentimgWidth, CurrentimgWidth);
                crop_start = (CurrentimgHeight - CurrentimgWidth) / 2;
                fullSizeImg = (Image)(bmpImage.Clone(recCropper, bmpImage.PixelFormat));
            }
            else if (CurrentimgWidth > CurrentimgHeight)
            {
                Rectangle recCropper = new Rectangle(crop_start, 0, CurrentimgHeight, CurrentimgHeight);
                crop_start = (CurrentimgWidth - CurrentimgHeight) / 2;
                fullSizeImg = (Image)(bmpImage.Clone(recCropper, bmpImage.PixelFormat));
            }



            //This will only work for jpeg images

            if (height > 0 && width > 0)
            {
                //Image.GetThumbnailImageAbort dummyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                System.Drawing.Image thumbnailImage = fullSizeImg.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbNailCallBack), IntPtr.Zero);

                string file_name = "j_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("AM", "").Replace("PM", "") + ".jpg";
                //string out_path = @"C:\inetpub\wwwroot\DelariTech\hairslayer\Req_Fle\" + file_name;
                string out_path = HttpContext.Current.Server.MapPath("~") + @"Req_Fle\" + file_name;
                Bitmap bm = new Bitmap(thumbnailImage);
                bm.Save(out_path, ImageFormat.Jpeg);

                fullSizeImg.Dispose();


                return "Req_Fle/" + file_name;
            }
            else
            {

                return tst;
            }

        }

        public static string CreateThumbnail(string target_file, int width, int height, int idDo)
        {
            //Get the image name – yourimage.jpg – from the query String

            string imageUrl = HttpContext.Current.Server.MapPath("~") + @"images\Users\" + target_file, tst = "";

            Image fullSizeImg = Image.FromFile(imageUrl);

            int CurrentimgHeight = fullSizeImg.Height;
            int CurrentimgWidth = fullSizeImg.Width;
            int crop_start = 0;
            Bitmap bmpImage = new Bitmap(fullSizeImg);

            if (CurrentimgHeight > CurrentimgWidth)
            {
                Rectangle recCropper = new Rectangle(0, crop_start, CurrentimgWidth, CurrentimgWidth);
                crop_start = (CurrentimgHeight - CurrentimgWidth) / 2;
                fullSizeImg = (Image)(bmpImage.Clone(recCropper, bmpImage.PixelFormat));
            }
            else if (CurrentimgWidth > CurrentimgHeight)
            {
                Rectangle recCropper = new Rectangle(crop_start, 0, CurrentimgHeight, CurrentimgHeight);
                crop_start = (CurrentimgWidth - CurrentimgHeight) / 2;
                fullSizeImg = (Image)(bmpImage.Clone(recCropper, bmpImage.PixelFormat));
            }



            //This will only work for jpeg images

            if (height > 0 && width > 0)
            {
                //Image.GetThumbnailImageAbort dummyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                System.Drawing.Image thumbnailImage = fullSizeImg.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbNailCallBack), IntPtr.Zero);

                string file_name = "j_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("AM", "").Replace("PM", "") + "-" + idDo + ".jpg";
                //string out_path = @"C:\inetpub\wwwroot\DelariTech\hairslayer\Req_Fle\" + file_name;
                string out_path = HttpContext.Current.Server.MapPath("~") + @"Req_Fle\" + file_name;
                Bitmap bm = new Bitmap(thumbnailImage);
                bm.Save(out_path, ImageFormat.Jpeg);

                fullSizeImg.Dispose();


                return "Req_Fle/" + file_name;
            }
            else
            {

                return tst;
            }

        }

        public static string CreateThumbnailBW(string target_file, int width, int height)
        {
            //Get the image name – yourimage.jpg – from the query String

            string imageUrl = HttpContext.Current.Server.MapPath("~") + @"\images\Users\" + target_file, tst = "";

            Image fullSizeImg = Image.FromFile(imageUrl);

            int CurrentimgHeight = fullSizeImg.Height;
            int CurrentimgWidth = fullSizeImg.Width;
            int crop_start = 0;
            Bitmap bmpImage = new Bitmap(fullSizeImg);

            if (CurrentimgHeight > CurrentimgWidth)
            {
                Rectangle recCropper = new Rectangle(0, crop_start, CurrentimgWidth, CurrentimgWidth);
                crop_start = (CurrentimgHeight - CurrentimgWidth) / 2;
                fullSizeImg = (Image)(bmpImage.Clone(recCropper, bmpImage.PixelFormat));
            }
            else if (CurrentimgWidth > CurrentimgHeight)
            {
                Rectangle recCropper = new Rectangle(crop_start, 0, CurrentimgHeight, CurrentimgHeight);
                crop_start = (CurrentimgWidth - CurrentimgHeight) / 2;
                fullSizeImg = (Image)(bmpImage.Clone(recCropper, bmpImage.PixelFormat));
            }



            //This will only work for jpeg images

            if (height > 0 && width > 0)
            {
                //Image.GetThumbnailImageAbort dummyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                System.Drawing.Image thumbnailImage = fullSizeImg.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbNailCallBack), IntPtr.Zero);

                string file_name = "j_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("AM", "").Replace("PM", "") + ".jpg";
                //string out_path = @"C:\inetpub\wwwroot\DelariTech\hairslayer\Req_Fle\" + file_name;
                string out_path = HttpContext.Current.Server.MapPath("~") + @"\Req_Fle\" + file_name;
                Bitmap bm = new Bitmap(thumbnailImage);
                bm.Save(out_path, ImageFormat.Jpeg);

                fullSizeImg.Dispose();


                return "Req_Fle/" + file_name;
            }
            else
            {

                return tst;
            }

        }
    }
}