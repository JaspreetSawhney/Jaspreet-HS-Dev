using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class Schedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar.Text = @"<iframe src=""script/preview.php?cid=" + Request.QueryString["id"].Replace("aj_x", "") + @""" frameborder=""0"" scrolling=""auto"" height=""770"" width=""100%""></iframe> ";
            }
        }
    }
}