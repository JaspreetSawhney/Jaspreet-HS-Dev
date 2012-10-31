using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer.UserControls
{
    public partial class InteractiveImage : System.Web.UI.UserControl
    {
        private string line1_text;
        private string line2_text;
        private int width;
        private int height;
        private string image_URL;
        private string css_class;
        private int do_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            string resolve_url = HairSlayer.includes.Thumbnails.CreateThumbnail(image_URL.Replace("/", @"\"), 220, 220, do_id);
            string dyn_image = @"<table border=""0"" width=""" + width + @""" cellpadding=""0"" cellspacing=""0""><tr>" +
                @"<td height=""" + height + @""" valign=""bottom"" onMouseOver=""document.getElementById('" + css_class + @"').style.display = 'block';"" onMouseOut=""document.getElementById('" + css_class + @"').style.display = 'none';"" background=""" + resolve_url + @""">" +
                @"<div id=""" + css_class + @"""><div id=""interactive-left""><p>" + line1_text + @"<br /><span id=""line-2"">by " + line2_text + @"</span></p></div>" +
                @"<div id=""interactive-right"">";
            if (HairSlayer.includes.Functions.is_logged_in()) { dyn_image += @"<p><a href=""javascript:addToFaves(" + do_id + @")"">Add</a></p>"; }
            dyn_image += @"<p><a href=""do.aspx?id=" + do_id + @""">View</a></p></div></div></td></tr></table>";

            lblImage.Text = dyn_image;
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
            get { return "images/Users/" + image_URL; }
            set { image_URL = value; }
        }

        public string CssClass
        {
            get { return css_class; }
            set { css_class = value; }
        }

        public int DoID
        {
            get { return do_id; }
            set { do_id = value; }
        }
    }
}