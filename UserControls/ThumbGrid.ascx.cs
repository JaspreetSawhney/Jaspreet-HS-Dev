using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer.UserControls
{
    public partial class ThumbGrid : System.Web.UI.UserControl
    {
        private System.Data.DataTable dt;
        private string exit_page;
        private string grid_title;


        protected void Page_Load(object sender, EventArgs e)
        {
            lblGridTitle.Text = grid_title;
            if (dt != null)
            {
                //lblPicGrid.Text = @"<table border=""0"" cellpading=""0"" cellspacing=""0"">";
                int cutoff = 6;
                if (dt.Rows.Count < 6) 
                { 
                    cutoff = dt.Rows.Count;
                    for (int x = dt.Rows.Count; x < 6; x++)
                    {
                        switch (x)
                        {
                            case 0:
                                Pic1.Text = "&nbsp;";
                                break;
                            case 1:
                                Pic2.Text = "&nbsp;";
                                break;
                            case 2:
                                Pic3.Text = "&nbsp;";
                                break;
                            case 3:
                                Pic4.Text = "&nbsp;";
                                break;
                            case 4:
                                Pic5.Text = "&nbsp;";
                                break;
                            case 5:
                                Pic6.Text = "&nbsp;";
                                break;
                        }
                    }
                }
                for (int i = 0; i < cutoff; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Pic1.Text = @"<a href=""do.aspx?id=" + dt.Rows[i]["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(dt.Rows[i]["idMember"] + @"\" + dt.Rows[i]["idDo"] + ".jpg", 75, 75, Convert.ToInt32(dt.Rows[i]["idDo"])) +  @""" border=""0"" class=""picCol""></a>";
                            break;
                        case 1:
                            Pic2.Text = @"<a href=""do.aspx?id=" + dt.Rows[i]["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(dt.Rows[i]["idMember"] + @"\" + dt.Rows[i]["idDo"] + ".jpg", 75, 75, Convert.ToInt32(dt.Rows[i]["idDo"])) + @""" border=""0"" class=""picCol""></a>";
                            break;
                        case 2:
                            Pic3.Text = @"<a href=""do.aspx?id=" + dt.Rows[i]["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(dt.Rows[i]["idMember"] + @"\" + dt.Rows[i]["idDo"] + ".jpg", 75, 75, Convert.ToInt32(dt.Rows[i]["idDo"])) + @""" border=""0"" class=""picCol""></a>";
                            break;
                        case 3:
                            Pic4.Text = @"<a href=""do.aspx?id=" + dt.Rows[i]["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(dt.Rows[i]["idMember"] + @"\" + dt.Rows[i]["idDo"] + ".jpg", 75, 75, Convert.ToInt32(dt.Rows[i]["idDo"])) + @""" border=""0"" class=""picCol""></a>";
                            break;
                        case 4:
                            Pic5.Text = @"<a href=""do.aspx?id=" + dt.Rows[i]["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(dt.Rows[i]["idMember"] + @"\" + dt.Rows[i]["idDo"] + ".jpg", 75, 75, Convert.ToInt32(dt.Rows[i]["idDo"])) + @""" border=""0"" class=""picCol""></a>";
                            break;
                        case 5:
                            Pic6.Text = @"<a href=""do.aspx?id=" + dt.Rows[i]["idDo"] + @"""><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(dt.Rows[i]["idMember"] + @"\" + dt.Rows[i]["idDo"] + ".jpg", 75, 75, Convert.ToInt32(dt.Rows[i]["idDo"])) + @""" border=""0"" class=""picCol""></a>";
                            break;
                    }
                }
            }
        }

        public System.Data.DataTable PicSource
        {
            set { dt = value; }
        }

        public string ExitPage
        {
            get { return exit_page; }
            set { exit_page = value; }
        }

        public string GridTitle
        {
            get { return grid_title; }
            set { grid_title = value; }
        }
    }
}