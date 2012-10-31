using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer.UserControls
{
    public partial class RateImage : System.Web.UI.UserControl
    {
        private string rating;
        private string gender;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (gender.ToUpper() == "M")
            {
                switch (rating)
                {
                    case "1":
                        imgRating.ImageUrl = "images/b_1.png";
                        break;
                    case "1.5":
                        imgRating.ImageUrl = "images/b_1_5.png";
                        break;
                    case "2":
                        imgRating.ImageUrl = "images/b_2.png";
                        break;
                    case "2.5":
                        imgRating.ImageUrl = "images/b_2_5.png";
                        break;
                    case "3":
                        imgRating.ImageUrl = "images/b_3.png";
                        break;
                    case "3.5":
                        imgRating.ImageUrl = "images/b_3_5.png";
                        break;
                    case "4":
                        imgRating.ImageUrl = "images/b_4.png";
                        break;
                    case "4.5":
                        imgRating.ImageUrl = "images/b_4_5.png";
                        break;
                    case "5":
                        imgRating.ImageUrl = "images/b_5.png";
                        break;
                }
            }
            else if (gender.ToUpper() == "F")
            {
                switch (rating)
                {
                    case "1":
                        imgRating.ImageUrl = "images/s_1.png";
                        break;
                    case "1.5":
                        imgRating.ImageUrl = "images/s_1_5.png";
                        break;
                    case "2":
                        imgRating.ImageUrl = "images/s_2.png";
                        break;
                    case "2.5":
                        imgRating.ImageUrl = "images/s_2_5.png";
                        break;
                    case "3":
                        imgRating.ImageUrl = "images/s_3.png";
                        break;
                    case "3.5":
                        imgRating.ImageUrl = "images/s_3_5.png";
                        break;
                    case "4":
                        imgRating.ImageUrl = "images/s_4.png";
                        break;
                    case "4.5":
                        imgRating.ImageUrl = "images/s_4_5.png";
                        break;
                    case "5":
                        imgRating.ImageUrl = "images/s_5.png";
                        break;
                }
            }
        }

        public string Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
    }
}