using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class SelectRMD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SelectDo_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectDo.aspx");
        }

        protected void UploadDo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Rmd.aspx");
        }
    }
}