using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class User : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            /********TEST CRITERIA*********/
            //DELETE BEFORE PRODUCTION LAUNCH
            //Response.Cookies["hr_main_ck"]["user_name"] = "Buddy Cheeks";
            //lblTest.Text = HairSlayer.includes.Thumbnails.CreateThumbnail(@"1\2.jpg", 100, 100);
            /********TEST CRITERIA*********/

            populateProfile();
            /*if (Request.QueryString["id"] == Request.Cookies["hr_main_ck"]["user_id"].ToString())
            {
                
                btnChangeProfilePic.Visible = true;
            }*/
            //Image1.ImageUrl = HairSlayer.includes.Thumbnails.CreateThumbnail(@"1001\1.jpg", 220, 220, "I DID IT MOM!");
            //MemberMenu.My_DosButtonClicked += new EventHandler(MemberMenu_My_DosButtonClicked);
            //MemberMenu.My_FavesButtonClicked += new EventHandler(MemberMenu_My_FavesButtonClicked);
            //MemberMenu.Upload_My_DoButtonClicked += new EventHandler(MemberMenu_Upload_My_DoButtonClicked);

            /*GalleryBrowser1.Layout = "GridShow";
            GalleryBrowser1.GalleryFolder = @"~/1/";
            GalleryBrowser1.AllowShowComment = false;
            GalleryBrowser1.BorderWidth = Unit.Pixel(1);*/
        }

        protected void populateProfile()
        {
            if (System.IO.File.Exists(Server.MapPath("~/images/Users/" + Request.QueryString["id"] + "/profile.jpg")))
            {
                WImage1.ImageURL = "/images/Users/" + Request.QueryString["id"] + "/profile.jpg";
            }
            else
            {
                WImage1.ImageURL = "/images/profile_bg.gif";
                WImage1.Height = 440;
                WImage1.Width = 700;
            }
            WImage1.Line1Text = Request.Cookies["hr_main_ck"]["user_name"].ToString();
            System.Data.DataTable dt = Worker.SqlTransaction("SELECT comment, comment_date, CONCAT(firstname, ' ', lastname) AS comment_owner FROM comments JOIN member ON comments.idMember = member.idMember JOIN dos ON comments.idDo = dos.idDo WHERE dos.idMember = " + Request.QueryString["id"] + " LIMIT 0,10", connect_string);
            
            CommentsGrid.DataSource = dt;
            CommentsGrid.DataBind();

            dt.Clear();
            dt = Worker.SqlTransaction("SELECT idMember, idDo FROM dos WHERE idMember = " + Request.QueryString["id"], connect_string);
            stylesGrid.PicSource = dt;

        }

        private void MemberMenu_My_DosButtonClicked(object sender, EventArgs e)
        {
            // ... do something when event is fired
            lblTest.Text = "MY DO!";
        }

        private void MemberMenu_My_FavesButtonClicked(object sender, EventArgs e)
        {
            // ... do something when event is fired
            lblTest.Text = "MY FAVE!";
        }

        private void MemberMenu_Upload_My_DoButtonClicked(object sender, EventArgs e)
        {
            // ... do something when event is fired
            lblTest.Text = "MY UPLOAD!";
        }

    }
}