using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer.UserControls
{
    public partial class WatermarkImage : System.Web.UI.UserControl
    {
        private string line1_text;
        private string line2_text;
        private int width;
        private int height;
        private string image_URL;
        private string rating;
        private string member_type;

        protected void Page_Load(object sender, EventArgs e)
        {
            string dyn_image = @"<table border=""0"" width=""" + width + @""" cellpadding=""0"" cellspacing=""0""><tr>" +
                @"<td height=""" + height + @""" valign=""bottom"" background=""" + image_URL + @"""><div id=""watermark-text"">" + 
                @"<div id=""watermark-left""><p>" + line1_text + @"<br /><span id=""line-2"">" + line2_text + "</span></p></div>" +
                @"<div id=""watermark-right"">&nbsp;</div>" +
                "</div></td></tr></table>";

            //lblImage.Text = dyn_image;
        }

        protected string GetRateImage(string rating, string shop_type)
        {
            string tmp = "";

            try
            {
                Convert.ToDouble(rating);
                switch (rating)
                {
                    case "1":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_1.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_1.png"" id=""clipper_shear"" />";
                        }
                        break;
                    case "1.5":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_1_5.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_1_5.png"" id=""clipper_shear"" />";
                        }
                        break;
                    case "2":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_2.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_2.png"" id=""clipper_shear"" />";
                        }
                        break;
                    case "2.5":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_2_5.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_2_5.png"" id=""clipper_shear"" />";
                        }
                        break;
                    case "3":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_3.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_3.png"" id=""clipper_shear"" />";
                        }
                        break;
                    case "3.5":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_3_5.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_3_5.png"" id=""clipper_shear"" />";
                        }
                        break;
                    case "4":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_4.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_4.png"" id=""clipper_shear"" />";
                        }
                        break;
                    case "4.5":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_4_5.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_4_5.png"" id=""clipper_shear"" />";
                        }
                        break;
                    case "5":
                        if (shop_type == "1" || shop_type == "4")
                        {
                            tmp = @"<div id=""rate_label"">Barber Rating</div><img src=""images/br_5.png"" id=""clipper_shear"" />";
                        }
                        else
                        {
                            tmp = @"<div id=""rate_label"">Stylist Rating</div><img src=""images/st_5.png"" id=""clipper_shear"" />";
                        }
                        break;
                    default:
                        tmp = @"<img src=""images/rating_spacer.png"" id=""clipper_shear"" />";
                        break;
                }
            }
            catch
            {
                tmp = @"<img src=""images/rating_spacer.png"" id=""clipper_shear"" />";
            }
            return tmp;
        }

        public string Line1Text
        {
            get { return line1_text; }
            set { line1_text = value; }
        }

        public string Line2Text
        {
            get { return line2_text; }
            set { line2_text = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public string ImageURL
        {
            get { return image_URL; }
            set { image_URL = value; }
        }

        public string Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public string Membership
        {
            get { return member_type; }
            set { member_type = value; }
        }
    }
}