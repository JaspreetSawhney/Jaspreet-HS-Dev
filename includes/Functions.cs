using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Mail;
using System.Text;


namespace HairSlayer.includes
{
    public class Functions : System.Web.UI.Page
    {
        public static void authenticate()
        {
            //COOKIES WILL NEED TO TRACK
            // idMember, last visit, idMembership, location
            if (HttpContext.Current.Request.Cookies["hr_main_ck"] == null)
            {
                System.IO.FileInfo oInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);
                if (oInfo.Name.ToUpper() != "INDEX.ASPX")
                {
                    HttpContext.Current.Response.Redirect("index.aspx");
                }
            }
            else
            {
                HttpContext.Current.Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);
                HttpContext.Current.Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                HttpContext.Current.Response.Redirect("user.aspx?id=" + HttpContext.Current.Request.Cookies["hr_main_ck"]["user_id"].ToString());
            }
        }

        public static bool is_logged_in()
        {
            if (HttpContext.Current.Request.Cookies["hr_main_ck"] == null)
            {
                return false;
            }
            else
            {
                if (DateTime.Compare(Convert.ToDateTime(HttpContext.Current.Request.Cookies["hr_main_ck"]["exp"]), DateTime.Now) < 0)
                {
                    return false;
                }
                return true;
            }
        }

        public static void authenticate(bool login_user, string user_id)
        {
            if (login_user)
            {
                HttpContext.Current.Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);
                HttpContext.Current.Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                HttpContext.Current.Response.Cookies["hr_main_ck"]["user_id"] = user_id;
                HttpContext.Current.Response.Cookies["hr_main_ck"]["location"] = user_id;
                HttpContext.Current.Response.Cookies["hr_main_ck"]["membership_type"] = user_id;
                HttpContext.Current.Response.Cookies["hr_main_ck"]["lat"] = "";
                HttpContext.Current.Response.Cookies["hr_main_ck"]["long"] = "";
            }
        }

        public static string ToBase64(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            return Convert.ToBase64String(encoding.GetBytes(str));
        }

        public static string FromBase64(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        public static void SendMail(string from, string to, string subject, string body)
        {
            /*************SEND MAIL*****************/
            //SmtpClient smtpClient = new SmtpClient();
            //MailMessage message = new MailMessage();
            ////smtpClient.Host = "mail.delaritech.com";
            ////smtpClient.Credentials = new System.Net.NetworkCredential("reports@delaritech.com", "deF12ault");
            //smtpClient.Host = "smtp.sendgrid.net";
            //smtpClient.Credentials = new System.Net.NetworkCredential("hairslayer", "H@irslayer1");
            //smtpClient.Port = 25;
            //message.IsBodyHtml = true;

            ////'''''Sending email to the applicant
            //message.From = new MailAddress(from);
            //message.To.Add(to);


            //message.Subject = subject;
            //message.Body = body;
            //smtpClient.Send(message);
            /*************SEND MAIL*****************/
        }

        public static void SendText(string to, string text_message)
        {
            string sid = "AC91195ee5f1c44a03a5659b1a76abf81d", token = "c8ad332d8af394c0205f4a4a0cc33476";
            string uri = "https://api.twilio.com/2010-04-01/Accounts/" + sid + "/SMS/Messages";
            //var data = "From=+14155992671&To=+1" + to.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "") + "&Body=" + text_message;

            var data = "From=" + HttpUtility.UrlEncode("+14155992671") +
            "&To=" + HttpUtility.UrlEncode("+1" + to.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "")) +
            "&Body=" + HttpUtility.UrlEncode(text_message);
            var authstring = Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", sid, token)));

            //ServicePointManager.Expect100Continue = false;
            //Byte[] postbytes = Encoding.ASCII.GetBytes(data.Replace("&", "%26"));
            Byte[] postbytes = Encoding.ASCII.GetBytes(data);
            var client = new System.Net.WebClient();

            client.Headers.Add("Authorization", String.Format("Basic {0}", authstring));
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] resp = client.UploadData(uri, "POST", postbytes);
        }

        public static string main_anonymous_sidebar()
        {
            string temp_ul = @"<div id=""main_sidebar_nav""><ul>" +
                "<li>Search For A Style</li>" +
                "</ul></div>";
            return temp_ul;
        }

        public static string sidebar_menu(bool logged_in)
        {
            string temp_ul = "";

            if (logged_in)
            {
                temp_ul = @"<div id=""main_sidebar_nav""><ul>" +
                    @"<li><href=""contact.aspx"">About HairSlayer</a></li>" +
                    @"<li><href=""about.aspx"">Contact Us</a></li>" +
                    "</ul></div>";
            }
            else
            {
                temp_ul = @"<div id=""main_sidebar_nav""><ul>" +
                    @"<li><href=""#"">Upload My Do</a></li>" +
                    @"<li><href=""#"">User Sign Up</a></li>" +
                    @"<li><href=""#"">Stylist / Barber Sign Up</a></li>" +
                    @"<li><href=""about.aspx"">About HairSlayer</a></li>" +
                    @"<li><href=""contact.aspx"">Contact Us</a></li>" +
                    "</ul></div>";
            }
            return temp_ul;
        }

        public static string watermark(string target_file, string watermark)
        {
            string out_text = "";

            string imageUrl = @"C:\inetpub\wwwroot\DelariTech\hairslayer\Users\" + target_file, tst = "";

            Image fullSizeImg = Image.FromFile(imageUrl);

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(fullSizeImg);

            SizeF StringSizeF; Single DesiredWidth; Font wmFont; Single RequiredFontSize; Single Ratio;

            //Set the watermark font	 

            wmFont = new Font("Verdana", 14, FontStyle.Bold);
            DesiredWidth = fullSizeImg.Width * Convert.ToSingle(.5);

            //use the MeasureString method to posi  tion the watermark in the centre of the image

            StringSizeF = g.MeasureString(watermark, wmFont);
            Ratio = StringSizeF.Width / wmFont.SizeInPoints;
            RequiredFontSize = DesiredWidth / Ratio;
            wmFont = new Font("Verdana", RequiredFontSize, FontStyle.Bold);
            //Sets the interpolation mode for a high quality image

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(fullSizeImg, new Rectangle(0, fullSizeImg.Height - 25, fullSizeImg.Width, 25));

            Color color = Color.Black;
            SolidBrush brush = new SolidBrush(color);
            Point atPoint = new Point(10, 10);
            Pen pen = new Pen(brush);
            g.FillRectangle(brush, new Rectangle(0, fullSizeImg.Height - 25, fullSizeImg.Width, 25));


            g.DrawImage(fullSizeImg, 0, 0, fullSizeImg.Width, fullSizeImg.Height);
            g.SmoothingMode = SmoothingMode.HighQuality;

            SolidBrush letterBrush = new SolidBrush(Color.FromArgb(50, 255, 255, 255));
            SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0));

            //'Enter the watermark text 

            g.DrawString(watermark, wmFont, shadowBrush, 75, Convert.ToSingle((fullSizeImg.Height * .5) - 36));
            g.DrawString(watermark, wmFont, letterBrush, 77, Convert.ToSingle((fullSizeImg.Height * .5) - 38));

            //thumbnailImage.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            string file_name = "j_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("AM", "").Replace("PM", "") + ".jpg";
            string out_path = @"C:\inetpub\wwwroot\DelariTech\hairslayer\Req_Fle\" + file_name;
            /*Bitmap bm = new Bitmap(thumbnailImage);
            bm.Save(out_path, ImageFormat.Jpeg);*/

            fullSizeImg.Save(out_path, ImageFormat.Jpeg);

            fullSizeImg.Dispose();


            return "Req_Fle/" + file_name;
        }

        public static string RateIcon(string rating, string gender)
        {
            string out_p = "";
            switch (rating)
            {
                case "1":
                    if (gender == "M")
                    {
                        out_p = "b_1.png";
                    }
                    else
                    {
                        out_p = "s_1.png";
                    }
                    break;
                case "1.5":
                    if (gender == "M")
                    {
                        out_p = "b_1_5.png";
                    }
                    else
                    {
                        out_p = "s_1_5.png";
                    }
                    break;
                case "2":
                    if (gender == "M")
                    {
                        out_p = "b_2.png";
                    }
                    else
                    {
                        out_p = "s_2.png";
                    }
                    break;
                case "2.5":
                    if (gender == "M")
                    {
                        out_p = "b_2_5.png";
                    }
                    else
                    {
                        out_p = "s_2_5.png";
                    }
                    break;
                case "3":
                    if (gender == "M")
                    {
                        out_p = "b_3.png";
                    }
                    else
                    {
                        out_p = "s_3.png";
                    }
                    break;
                case "3.5":
                    if (gender == "M")
                    {
                        out_p = "b_3_5.png";
                    }
                    else
                    {
                        out_p = "s_3_5.png";
                    }
                    break;
                case "4":
                    if (gender == "M")
                    {
                        out_p = "b_4.png";
                    }
                    else
                    {
                        out_p = "s_4.png";
                    }
                    break;
                case "4.5":
                    if (gender == "M")
                    {
                        out_p = "b_4_5.png";
                    }
                    else
                    {
                        out_p = "s_4_5.png";
                    }
                    break;
                case "5":
                    if (gender == "M")
                    {
                        out_p = "b_5.png";
                    }
                    else
                    {
                        out_p = "s_5.png";
                    }
                    break;
            }
            if (out_p == "") { return ""; }
            return @"<img class=""rating_image"" src=""images/" + out_p + @""" border=""0"" alt=""rating"" />";
        }

        public static int Create_New_Calendar(string user_id, string user_name, string pass, string shop_name, string full_name)
        {
            // RETURN 0 - No Errors
            // RETURN 1 No Errors
            // RETURN 2 - User Already Exists
            // RETURN 3 No Errors
            // RETURN 0 No Errors
            string conn_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
            string db_prefix = "a_shed";
            string sql = "SELECT * FROM " + db_prefix + ".service_booking_users WHERE username='" + user_name + "' LIMIT 1"; //, conn_string = "DATA SOURCE=10.179.34.216;UID=ash_user;PWD=diM_3281@99;DATABASE=a_sched";
            System.Data.DataTable dt = Worker.SqlTransaction(sql, conn_string);
            if (dt.Rows.Count > 0)
            {
                return 2;
            }
            dt.Dispose();


            sql = "INSERT INTO " + db_prefix + ".service_booking_users (`id`, `role_id`, `username`, `password`, `created`, `status`) VALUES(" + user_id + ", 2, '" + user_name + "', '" + pass + "', NOW(), 'T')";
            Worker.SqlInsert(sql, conn_string);
            
            //NEED TO GET CALENDAR NAME
            string calendar_name = shop_name;
            if (calendar_name == "") { calendar_name = user_name; }
            sql = "INSERT INTO " + db_prefix + ".service_booking_calendars (`id`, `user_id`, `calendar_title`) VALUES(" + user_id + "," + user_id + ",'" + calendar_name.Replace("'", "''") + "')";
            Worker.SqlInsert(sql, conn_string);

            sql = "SELECT * FROM " + db_prefix + ".service_booking_options WHERE `calendar_id`=1";
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
                        string bod = "Options Error for " + user_name + "<br><br>" + ex.Message;
                        SendMail("errors@hairslayer.com", "icunningham@delaritech.com", "Error in Calendar Creation User: " + user_name, bod);
                    }
                }
            }

            string worktimes = "INSERT INTO " + db_prefix + ".service_booking_working_times (`calendar_id`, `monday_from`, `monday_to`, `monday_dayoff`, `tuesday_from`, `tuesday_to`, `tuesday_dayoff`, `wednesday_from`, `wednesday_to`, `wednesday_dayoff`, `thursday_from`, `thursday_to`, `thursday_dayoff`, `friday_from`, `friday_to`, `friday_dayoff`, `saturday_from`, `saturday_to`, `saturday_dayoff`, `sunday_from`, `sunday_to`, `sunday_dayoff`) VALUES (" +
                user_id + ",'09:00:00','18:00:00','F','09:00:00','18:00:00','F','09:00:00','18:00:00','F','09:00:00','18:00:00','F','09:00:00','18:00:00','F',NULL,NULL,'T',NULL,NULL,'T')";
            Worker.SqlInsert(worktimes, conn_string);

            Worker.SqlInsert("INSERT INTO " + db_prefix + ".service_booking_employees (`id`, `calendar_id`, `name`, `email`, `phone`, `notes`, `send_email`) VALUES (" + 
                user_id + "," + user_id + ",'" + full_name + "','" + user_name + "','','',0)", conn_string);
            return 0;
            /* if ($db) {		
		
			//check username exist
			$sql = "SELECT * FROM ".DEFAULT_PREFIX."service_booking_users WHERE username='".$data['username']."' LIMIT 1";
			$result = mysql_query($sql, $connection);
			$user = mysql_fetch_assoc($result);
			if(is_array($user) && $user['id'] > 0)
			{
				//username have exists
				return 2;
			}
			else {			
				//$sql = "INSERT INTO ".DEFAULT_PREFIX."service_booking_users (`role_id`, `username`, `password`, `created`, `status`) VALUES(2, '".$data['username']."', '".$data['password']."', NOW(), '".$data['status']."')";
				$sql = "INSERT INTO ".DEFAULT_PREFIX."service_booking_users (`id`, `role_id`, `username`, `password`, `created`, `status`) VALUES(".$data['user_id'].", 2, '".$data['username']."', '".$data['password']."', NOW(), '".$data['status']."')";
				if(mysql_query($sql, $connection)){
					//$insert_id = mysql_insert_id();
					$insert_id = $data['user_id'];
					
					//create calendar for new user
					if(create_calendar($insert_id, $data, $connection)) {
						//completed create user and calendar for user
						return 1;
					} else {
						//create calander for user failed
						return 4;
					}
					
					return 1;
				} else {
					//an error accurred
					return 3;
				}
			}*/
        }

        public static bool isPremiumAccount()
        {
            if (HttpContext.Current.Request.Cookies["hr_main_ck"]["membership"] == "4" || HttpContext.Current.Request.Cookies["hr_main_ck"]["membership"] == "5" || HttpContext.Current.Request.Cookies["hr_main_ck"]["membership"] == "6" || HttpContext.Current.Request.Cookies["hr_main_ck"]["membership"] == "7")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isPremiumAccount(string account_id)
        {
            if (account_id == "4" || account_id == "5" || account_id == "6" || account_id == "7")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string generateUserID()
        {
            //if (includes.Functions.is_logged_in())
            if (!includes.Functions.is_logged_in() && HttpContext.Current.Request.Cookies["hr_anonymous"] == null)
            {
                string guid = DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "").Replace("AM", "").Replace("PM", "");
                HttpContext.Current.Response.Cookies["hr_anonymous"].Value = guid;
                HttpContext.Current.Response.Cookies["hr_anonymous"].Expires = DateTime.Now.AddMonths(2);
                return guid;
            }
            else if (is_logged_in())
            {
                return HttpContext.Current.Request.Cookies["hr_main_ck"]["user_id"];
            }
            else
            {
                return HttpContext.Current.Request.Cookies["hr_anonymous"].Value;
            }
        }

        public static string NotLoggedPopUp()
        {
            string popupHTML = @"<div id=""log-intro"">" + Environment.NewLine +
                @"Please <a href=""Login.aspx"">sign in</a> or <a href=""Signup.aspx"">register</a> to save style." + Environment.NewLine +
                @"<br />" + Environment.NewLine +
                @"<div id=""lead-wrap"">" + Environment.NewLine +
                @"<a href=""Login.aspx"" id=""log_pop_button"" class=""log_button"">Sign In</a>" + Environment.NewLine +
                @"<a href=""Signup.aspx"" class=""reg_button"">Register</a>" + Environment.NewLine +
                @"</div>" + Environment.NewLine +
                @"<p id=""log-intro-close"">CLOSE X</p>" + Environment.NewLine +
                @"</div>";

            return popupHTML;
        }

        public static string GetUserName()
        {
            string id = HttpContext.Current.Request.Cookies["hr_main_ck"]["user_id"];
            System.Data.DataTable dt = Worker.SqlTransaction("SELECT CONCAT(firstname, ' ', lastname) as membername FROM member WHERE idMember = " + id, System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"].ConnectionString);

            return dt.Rows[0]["membername"].ToString();
        }

        public static bool NewUser()
        {
            if (HttpContext.Current.Request.Cookies["hr_main_ck"] != null) { return false; }
            return true;
        }
    }
}