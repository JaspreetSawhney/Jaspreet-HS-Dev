using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HairSlayer
{
    public partial class DeleteService : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Worker.SqlInsert("DELETE FROM services WHERE idService = " + Request.QueryString["id"], connect_string);
                Worker.SqlInsert("DELETE FROM a_sched.service_booking_services WHERE id = " + Request.QueryString["id"], connect_string);
                Worker.SqlInsert("DELETE FROM a_sched.service_booking_employees_services WHERE service_id = " + Request.QueryString["id"], connect_string);

                BindServicesTable(Worker.SqlTransaction("SELECT * FROM services WHERE idShop = " + Request.QueryString["shop"], connect_string), Request.QueryString["shop"]);
            }
            

        }

        protected void BindServicesTable(DataTable dt, string shopid)
        {
            string tmp_table = @"<table id=""services"">" + Environment.NewLine;
            double tmp_price = 0;
            foreach (DataRow dr in dt.Rows)
            {
                tmp_price = Convert.ToDouble(dr["price"].ToString());
                tmp_table += "<tr>" + Environment.NewLine + "<td>" + dr["service_name"] + "</td>" + Environment.NewLine + "<td>$" + tmp_price.ToString("N2") + "</td>" + Environment.NewLine + @"<td><a href=""javascript:delService('" + dr["idService"] + "','" + shopid + @"');"" class=""remove_style"">remove</a></td>" + Environment.NewLine + "</tr>" + Environment.NewLine;
            }
            tmp_table += "</table>";
            Response.Write(tmp_table);
            //dr["idService"].ToString()
        }
    }
}