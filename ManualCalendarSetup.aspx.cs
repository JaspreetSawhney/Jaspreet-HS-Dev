using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class ManualCalendarSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string conn_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
            string db_prefix = "a_shed", user_id = calendarid.Text;
            string sql = "SELECT * FROM " + db_prefix + ".service_booking_options WHERE `calendar_id`=1";
            System.Data.DataTable dt2 = Worker.SqlTransaction(sql, conn_string);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["group"] != DBNull.Value)
                    {
                        sql = "INSERT INTO " + db_prefix + ".service_booking_options (`calendar_id`, `key`, `tab_id`, `group`, `value`, `description`, `label`, `type`, `order`) VALUES( " +
                        user_id + ",'" + dt2.Rows[i]["key"] + "','" + dt2.Rows[i]["tab_id"] + "','" + dt2.Rows[i]["group"] + "','" +
                        dt2.Rows[i]["value"].ToString().Replace("'", "''") + "','" + dt2.Rows[i]["description"].ToString().Replace("'", "''") + "','" +
                        dt2.Rows[i]["label"] + "','" + dt2.Rows[i]["type"] + "','" + dt2.Rows[i]["order"] + "')";
                    }
                    else
                    {
                        sql = "INSERT INTO " + db_prefix + ".service_booking_options (`calendar_id`, `key`, `tab_id`, `group`, `value`, `description`, `label`, `type`, `order`) VALUES( " +
                        user_id + ",'" + dt2.Rows[i]["key"] + "','" + dt2.Rows[i]["tab_id"] + "',NULL,'" +
                        dt2.Rows[i]["value"].ToString().Replace("'", "''") + "','" + dt2.Rows[i]["description"].ToString().Replace("'", "''") + "','" +
                        dt2.Rows[i]["label"] + "','" + dt2.Rows[i]["type"] + "','" + dt2.Rows[i]["order"] + "')";
                    }

                    try
                    {
                        Worker.SqlInsert(sql, conn_string);
                    }
                    catch (Exception ex)
                    {
                        string bod = "Options Error for <br><br>" + ex.Message;
                        //SendMail("errors@hairslayer.com", "icunningham@delaritech.com", "Error in Calendar Creation User: " + user_name, bod);
                    }
                }
            }

            string worktimes = "INSERT INTO " + db_prefix + ".service_booking_working_times (`calendar_id`, `monday_from`, `monday_to`, `monday_dayoff`, `tuesday_from`, `tuesday_to`, `tuesday_dayoff`, `wednesday_from`, `wednesday_to`, `wednesday_dayoff`, `thursday_from`, `thursday_to`, `thursday_dayoff`, `friday_from`, `friday_to`, `friday_dayoff`, `saturday_from`, `saturday_to`, `saturday_dayoff`, `sunday_from`, `sunday_to`, `sunday_dayoff`) VALUES (" +
                user_id + ",'09:00:00','18:00:00','F','09:00:00','18:00:00','F','09:00:00','18:00:00','F','09:00:00','18:00:00','F','09:00:00','18:00:00','F',NULL,NULL,'T',NULL,NULL,'T')";
            Worker.SqlInsert(worktimes, conn_string);

            Response.Write("Setup Complete!");
        }
    }
}