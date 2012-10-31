using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class WebForm : System.Web.UI.Page
    {
        string conn_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string user_id = User.Text.Trim();
            string sql = "SELECT * FROM a_sched.service_booking_options WHERE `calendar_id`=1";
            System.Data.DataTable dt2 = Worker.SqlTransaction(sql, conn_string);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    string group = "'" + dt2.Rows[i]["group"] + "'";
                    if (dt2.Rows[i]["group"] == DBNull.Value) { group = "NULL"; }
                    sql = "INSERT INTO a_sched.service_booking_options (`calendar_id`, `key`, `tab_id`, `group`, `value`, `description`, `label`, `type`, `order`) VALUES( " +
                    user_id + ",'" + dt2.Rows[i]["key"] + "','" + dt2.Rows[i]["tab_id"] + "'," + group + ",'" +
                    dt2.Rows[i]["value"].ToString().Replace("'", "''") + "','" + dt2.Rows[i]["description"].ToString().Replace("'", "''") + "','" +
                    dt2.Rows[i]["label"] + "','" + dt2.Rows[i]["type"] + "','" + dt2.Rows[i]["order"] + "')";
                    Worker.SqlInsert(sql, conn_string);
                    Response.Write(sql + "<br><br>");
                }
            }
        }
    }
}