using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer.UserControls
{
    public partial class FavesGrid : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            favImage1.ImageUrl = "~/1/2.jpg";
        }

        protected void favImage1_Click(object sender, ImageClickEventArgs e)
        {
            //ModalPopupExtender1.TargetControlID = "favImage1";
        }

        protected void favImage2_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void favImage3_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void favImage4_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void favImage5_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void favImage6_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnMyFaves_Click(object sender, EventArgs e)
        {

        }
    }
}