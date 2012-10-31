using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HairSlayer
{
    public partial class Cax : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["hsh"] != null)
            {
                System.Data.DataTable dt = Worker.SqlTransaction("SELECT created from a_shed.service_booking_bookings WHERE id = " + Request.QueryString["bid"], connect_string);
                if (Request.QueryString["hsh"] == to_SHA1(Request.QueryString["bid"] + dt.Rows[0]["created"].ToString() + "h@irslayer99").Replace("-", "").ToLower())
                {
                    Worker.SqlInsert("DELETE FROM a_shed.service_booking_bookings_details WHERE booking_id = " + Request.QueryString["bid"], connect_string);
                    Worker.SqlInsert("DELETE FROM a_shed.service_booking_bookings WHERE id = " + Request.QueryString["bid"], connect_string);
                    Response.Write("<h2>Your Appointment Has Been Canceled.</h2>");
                }
            }
        }

        protected string to_SHA1(string s)
        {
            byte[] data = new byte[256];
            byte[] result;

            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);

            return BitConverter.ToString(result);
        }
    }
}