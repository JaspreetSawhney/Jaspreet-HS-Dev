using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace HairSlayer
{
    public partial class Do : System.Web.UI.Page
    {
        string connect_string = (ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
        string Doid = "";
        string memberid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null) { Response.Redirect("index.aspx"); }
            if (HairSlayer.includes.Functions.is_logged_in())
            {
                //pnlComments.Visible = true;
                GetStyle.Visible = true;
                CommentSubmit.Visible = true;
                RatingsPanelLoggedIn.Visible = true;
                //btnlikestyle.Visible = true;
                LikeStyle.Visible = true;
                LikeStyle.NavigateUrl = "javascript:addFave('" + Request.QueryString["id"] + "','" + Request.Cookies["hr_main_ck"]["user_id"] + "');";

                DataTable dtb = Worker.SqlTransaction("SELECT * FROM rate_log WHERE idDo = " + Request.QueryString["id"] + " AND idMember = " + Request.Cookies["hr_main_ck"]["user_id"], connect_string);
                if (dtb.Rows.Count > 0)
                {
                    pnlRatings.Visible = false;
                }

            }
            else
            {
                PopulateNoLoginScreen();
            }

            CommentSubmit.OnClientClick = "rpxSocial(" + Request.QueryString["id"] + ")";
            
            Doid = Request.QueryString["id"];
            MySqlConnection oCon = new MySqlConnection(connect_string);
            if (oCon.State == System.Data.ConnectionState.Closed)
            {
                oCon.Open();
            }
            MySqlDataAdapter adp = new MySqlDataAdapter("select idMember, idStyleOwner, do_name, Gender from dos where idDo=" + Doid, oCon);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                memberid = dt.Rows[0]["idMember"].ToString();
                Star.Value = dt.Rows[0]["Gender"].ToString();
                DoName.Text = dt.Rows[0]["do_name"].ToString();
                style_picture.AlternateText = dt.Rows[0]["do_name"].ToString();
                if (dt.Rows[0]["idStyleOwner"] != DBNull.Value)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/images/Users/" + dt.Rows[0]["idStyleOwner"].ToString() + "/profile.jpg")))
                    {
                        featured_image.ImageUrl = "/images/Users/" + dt.Rows[0]["idStyleOwner"].ToString() + "/profile.jpg";
                        lnkProvider.NavigateUrl = "Prov.aspx?id=" + dt.Rows[0]["idStyleOwner"].ToString();
                    }
                    else
                    {
                        featured_image.ImageUrl = "/images/profile.gif";
                    }
                    GetStyle.NavigateUrl = "schedule.aspx?id=aj_x" + dt.Rows[0]["idStyleOwner"].ToString();
                    
                    //GetStyle.Visible = true;

                    DataTable dtx = Worker.SqlTransaction("SELECT DISTINCT idDo, a.idMember, firstname, lastname  FROM dos As a JOIN member As b ON a.idMember = b.idMember WHERE a.idMember = " + dt.Rows[0]["idStyleOwner"].ToString() + " ORDER BY rmd_date DESC ", connect_string); // LIMIT 0,6

                    /************************Create Full Gallery****************************/
                    string img_spot = "", full_gallery = "<ul>";
                    foreach (DataRow dr in dtx.Rows)
                    {
                        img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                        full_gallery += @"<li><div class=""thumb_wrap""><a href=""do.aspx?id=" + dr["idDo"].ToString() + @""" class=""full-gallery""><div class=""cover_thumbnail""></div><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 150, 150, Convert.ToInt32(dr["idDo"])) + @""" /></div></a></li>";
                    }
                    full_gallery += "</ul>";
                    FullGallery.Text = full_gallery;

                    //@"<ul><li><div class=""thumb_wrap""><a href=""#"" class=""full-gallery""><div class=""cover_thumbnail""></div><img src=""http://hair.delaritech.com/images/Users/1/3.jpg"" /></div></a></li></ul>";
                    /************************Create Full Gallery****************************/

                     DataTable dtn = dtx.Clone();
                     if (dtx.Rows.Count > 6) { pnlViewAll.Visible = true; }
                    int max = dtx.Rows.Count;
                    if (max > 5) { max = 6; }
                    for (int i = 0; i < max; i++)
                    {
                        dtn.ImportRow(dtx.Rows[i]);
                    }

                    string short_gallery = @"<ul id=""side_bar_img_list"">" + Environment.NewLine;
                    foreach (DataRow dr in dtn.Rows)
                    {
                        img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                        short_gallery += @"<li><a href=""do.aspx?id=" + dr["idDo"].ToString() + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 150, 150, Convert.ToInt32(dr["idDo"])) + @""" alt="""" /></a></li>" + Environment.NewLine;
                    }
                    short_gallery += "</ul>" + Environment.NewLine;

                    Sidebar_Image_Grid.Controls.Add(new LiteralControl(short_gallery));
                    //stylesGrid.PicSource = dtn;
                    StyledBy.Text = dtx.Rows[0]["firstname"] + " " + dtx.Rows[0]["lastname"];
                    stylist_name.Text = StyledBy.Text;
                    if (dt.Rows[0]["Gender"].ToString() == "M")
                    {
                        StyleBy2.Text += " barber info"; //dtx.Rows[0]["firstname"] + " " + dtx.Rows[0]["lastname"];
                    }
                    else if (dt.Rows[0]["Gender"].ToString() == "F")
                    {
                        StyleBy2.Text += " stylist info";
                    }

                    if (!IsPostBack)
                    {
                        System.Web.UI.HtmlControls.HtmlHead head = (System.Web.UI.HtmlControls.HtmlHead)Page.Header;
                        System.Web.UI.HtmlControls.HtmlMeta keywords = new System.Web.UI.HtmlControls.HtmlMeta();
                        keywords.Name = "keywords";
                        keywords.Content = "Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle, " + dtx.Rows[0]["firstname"] + " " + dtx.Rows[0]["lastname"] + ", " + DoName.Text;
                        head.Controls.Add(keywords);
                    }
                    dtx.Dispose();
                    dtn.Dispose();
                }
            }
            style_picture.ImageUrl = "/images/Users/" + memberid + "/" + Doid + ".jpg";
            recentcomment();
        }

        protected void PopulateNoLoginScreen()
        {
            GetStyleNoLog.Visible = true;
            LikeStyleNoLog.Visible = true;
            NoLogCommentSubmit.Visible = true;
            RatingsPanelNotLoggedIn.Visible = true;
        }

        protected void btnsbmit_Click(object sender, EventArgs e)
        {
            Comment_Error.Visible = false;
            if (comment_box.Text.Replace("<", "").Replace(">", "").Replace("'", "''") == "")
            {
                Comment_Error.Visible = true;
            }
            else
            {
                MySqlConnection oCon = new MySqlConnection(connect_string);
                if (oCon.State == System.Data.ConnectionState.Closed)
                {
                    oCon.Open();
                }
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = oCon;
                cmd.CommandText = "insert into comments (comment_date,idDo,comment,idMember) values(@comment_date,@idDo,@comment,@idMember)";
                cmd.Parameters.AddWithValue("@comment_date", DateTime.Now);
                cmd.Parameters.AddWithValue("@idDo", Request.QueryString["id"]);
                cmd.Parameters.AddWithValue("@comment", comment_box.Text.Replace("<", "").Replace(">", "").Replace("'", "''"));
                cmd.Parameters.AddWithValue("@idMember", Request.Cookies["hr_main_ck"]["user_id"]);
                cmd.ExecuteNonQuery();
                oCon.Close();
                comment_box.Text = "";
                recentcomment();
            }
        }

        public void recentcomment()
        {
            string comment = "";
            MySqlConnection oCon = new MySqlConnection(connect_string);
            if (oCon.State == System.Data.ConnectionState.Closed)
            {
                oCon.Open();
            }

            MySqlDataAdapter adp = new MySqlDataAdapter("select DISTINCT comment, firstname, lastname from comments As a JOIN member As b ON a.idMember = b.idMember where idDo=" + Doid + " ORDER BY comment_date DESC LIMIT 0,20", oCon);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //comment = dt.Rows[0]["comment"].ToString();
                CommentsGrid.DataSource = dt;
                CommentsGrid.DataBind();
            }
            //lblcmt.Text = comment;
        }

        protected void btnlikestyle_Click(object sender, EventArgs e)
        {
            if (HairSlayer.includes.Functions.is_logged_in())
            {
                //Worker.SqlInsert("INSERT INTO faves (idMember, idDo) VALUES (" + Request.Cookies["hr_main_ck"]["user_id"] + "," + Request.QueryString["id"] + ")", connect_string);
                MySqlConnection oConn = new MySqlConnection(connect_string);
                oConn.Open();

                MySqlCommand oComm = new MySqlCommand("sp_AddFave", oConn);
                oComm.CommandType = System.Data.CommandType.StoredProcedure;
                oComm.Parameters.AddWithValue("do_id", Convert.ToInt32(Request.QueryString["id"]));
                oComm.Parameters.AddWithValue("mem_id", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));
                oComm.ExecuteNonQuery();
                oConn.Close();
                btnlikestyle.Visible = false;
                NavigationMenu.BindDataList();
            }
            else
            {
                Response.Redirect("SignUp.aspx");
            }
        }
    }
}