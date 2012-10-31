using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace HairSlayer
{
    public partial class SelectDo : System.Web.UI.Page
    {
        string connect_string = (ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { SelectedDo.Value = "-1"; }
            //Submit.OnClientClick = "rpxSocial(" + SelectedDo.Value + ")";
            var user_id = Request.Cookies["hr_main_ck"]["user_id"];
            DataTable dt = Worker.SqlTransaction("SELECT * FROM dos WHERE (idStyleOwner = " + user_id + " OR idMember = " + user_id + ") AND ratemydo <> 1", connect_string);

            string ul_holder = "<ul id=\"do_img_list\">";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ul_holder += "<li><img src="+ dr["fileurl"].ToString() +" class=\"non-select\" id=\"pic" + dr["idDo"].ToString() + "\"></li>";
                }
                ul_holder += "</ul>";
            }
            else
            {
                ul_holder = "<h3 class=\"do-select\">You Do Not Have Any Dos</h3>";
                DoSelectSubmit.Visible = false;
            }
            DoList.Controls.Add(new LiteralControl(ul_holder));
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (SelectedDo.Value != "-1")
            {
                var user_id = Request.Cookies["hr_main_ck"]["user_id"];
                Worker.SqlInsert("UPDATE dos SET ratemydo = 1, rmd_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE idDo = " + SelectedDo.Value + " AND (idStyleOwner = " + user_id + " OR idMember = " + user_id + ")", connect_string);
                DoList.Visible = false;
                Prompt.Text = "Your Do Has Been Submitted to the Rate My Do&trade; Competition.";
                DoSelectSubmit.Visible = false;
            }
            else
            {
                Prompt.Text = "Please Select A Do.";
            }
        }
    }
}