using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HairSlayer
{
    public partial class Cron : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            int REMINDER_TIME_FRAME_IN_HOURS = 12, sms_sent = 0, email_sent = 0;
            string sql = "SELECT customer_phone, customer_email, reminder_email, reminder_sms, bd.id from a_shed.service_booking_bookings As b JOIN a_shed.service_booking_bookings_details as bd ON b.id = bd.id WHERE DATE_ADD(bd.date, INTERVAL bd.start HOUR_SECOND) BETWEEN NOW() AND DATE_ADD(NOW(), INTERVAL " + REMINDER_TIME_FRAME_IN_HOURS + " HOUR) AND (reminder_email = 0 OR reminder_sms = 0)";
            string sid = "AC91195ee5f1c44a03a5659b1a76abf81d", token = "c8ad332d8af394c0205f4a4a0cc33476";

            DataTable dt = Worker.SqlTransaction(sql, connect_string);

            foreach (DataRow dr in dt.Rows)
            {
                DataTable options = Worker.SqlTransaction("SELECT * FROM a_shed.service_booking_options WHERE calendar_id = 1", connect_string);
                if (dr["reminder_email"].ToString() == "3")
                {
                    string reminder_body = options.Select("key = 'reminder_body'")[0]["value"].ToString();
                    string reminder_subject = options.Select("key = 'reminder_subject'")[0]["value"].ToString();

                    Response.Write(Mail_Merge(reminder_body, dr["id"].ToString()));
                    //includes.Functions.SendMail("info@hairslayer.com", dr["customer_email"].ToString(), reminder_subject, Mail_Merge(reminder_body, dr["id"].ToString()));
                    email_sent = 1;
                }

                if (dr["reminder_sms"].ToString() == "0")
                {
                    string reminder_body = options.Select("key = 'reminder_sms_message'")[0]["value"].ToString();

                    //Response.Write(Text_Merge(reminder_body, dr["id"].ToString()));
                    string uri = "https://api.twilio.com/2010-04-01/Accounts/" + sid + "/SMS/Messages";
                    var data = "From=+14155992671&To=+1" + dr["customer_phone"].ToString().Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "") + "&Body=" + Text_Merge(reminder_body, dr["id"].ToString());
                    var authstring = Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", sid, token)));

                    ServicePointManager.Expect100Continue = false;
                    Byte[] postbytes = Encoding.ASCII.GetBytes(data);
                    var client = new WebClient();

                    client.Headers.Add("Authorization", String.Format("Basic {0}", authstring));
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    byte[] resp = client.UploadData(uri, "POST", postbytes);


                    //Response.Write(Encoding.ASCII.GetString(resp));

                    sms_sent = 1;
                }

                Worker.SqlInsert("UPDATE a_shed.service_booking_bookings_details set reminder_sms = " + sms_sent + ", reminder_email = " + email_sent + " WHERE id = " + dr["id"].ToString(), connect_string);
            }
        }

        protected string Mail_Merge(string message_body, string booking_id)
        {
            string[] subs = { "{Name}", "{Email}", "{Phone}", "{Country}", "{City}", "{State}", "{Zip}", "{Address}", "{Notes}", "{CCType}", "{CCExp}", "{PaymentMethod}", "{Deposit}", "{Total}", "{Tax}", "{BookingID}", "{Services}", "{CancelURL}" };
            string sql = "SELECT serv.name As Services, customer_name As Name, customer_email As Email, customer_phone As Phone, customer_city As City, customer_state As State, customer_zip As Zip, customer_address As Address, customer_notes As Notes, booking_deposit As Deposit, booking_tax As Tax, booking_total As Total, payment_method As PaymentMethod, b.id As BookingID, customer_country As Country, cc_type As CCType, cc_exp As CCExp " +
                "FROM a_shed.service_booking_bookings As b JOIN a_shed.service_booking_bookings_details As bd ON b.id = bd.id JOIN a_shed.service_booking_services As serv ON bd.service_id = serv.id where b.id = " + booking_id;
            DataTable dtx = Worker.SqlTransaction(sql, connect_string);

            foreach (string s in subs)
            {
                if (s != "{CancelURL}") { message_body = message_body.Replace(s, dtx.Rows[0][s.Replace("{", "").Replace("}", "")].ToString()); }
            }
            message_body = message_body.Replace("{CancelURL}", "");

            return message_body;
        }

        protected string Text_Merge(string message_body, string booking_id)
        {
            string[] subs = { "{Name}", "{Email}", "{Phone}", "{Country}", "{City}", "{State}", "{Zip}", "{Address}", "{Notes}", "{CCType}", "{CCExp}", "{PaymentMethod}", "{Deposit}", "{Total}", "{Tax}", "{BookingID}", "{Services}", "{CancelURL}" };
            string sql = "SELECT serv.name As Services, customer_name As Name, customer_email As Email, customer_phone As Phone, customer_city As City, customer_state As State, customer_zip As Zip, customer_address As Address, customer_notes As Notes, booking_deposit As Deposit, booking_tax As Tax, booking_total As Total, payment_method As PaymentMethod, b.id As BookingID, customer_country As Country, cc_type As CCType, cc_exp As CCExp " +
                "FROM a_shed.service_booking_bookings As b JOIN a_shed.service_booking_bookings_details As bd ON b.id = bd.id JOIN a_shed.service_booking_services As serv ON bd.service_id = serv.id where b.id = " + booking_id;

            DataTable dtx = Worker.SqlTransaction(sql, connect_string);
            foreach (string s in subs)
            {
                if (s != "{CancelURL}") { message_body = message_body.Replace(s, dtx.Rows[0][s.Replace("{", "").Replace("}", "")].ToString()); }
            }

            return message_body;
        }

        protected string to_SHA1(string s)
        {
            byte[] data = new byte[256];
            byte[] result;

            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);
            return result.ToString();
        }
    }
}