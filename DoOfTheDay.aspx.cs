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
    public partial class DoOfTheDay : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["hslayer_connection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetDosofDay();
        }

        protected void GetDosofDay()
        {
            DataTable dt_Male = Worker.SqlTransaction("SELECT a.idDo, b.idMember,do_name, b.idStyleOwner, firstname, lastname, b.Gender FROM do_of_the_day AS a JOIN dos As b ON a.idDo = b.idDo JOIN member As c ON b.idMember = c.idMember WHERE a.gender = 'M' AND a.date = DATE_SUB(CONCAT(CURDATE(), ' 00:00:00'), INTERVAL 1 DAY)", connectionString);

            DataTable dt_Female = Worker.SqlTransaction("SELECT a.idDo, b.idMember,do_name, b.idStyleOwner, firstname, lastname, b.Gender FROM do_of_the_day AS a JOIN dos As b ON a.idDo = b.idDo JOIN member As c ON b.idMember = c.idMember WHERE a.gender = 'F' AND a.date = DATE_SUB(CONCAT(CURDATE(), ' 00:00:00'), INTERVAL 1 DAY)", connectionString);

            if (dt_Male.Rows.Count > 0)
            {
                do_of_day_male.ImageUrl = "images/Users/" + dt_Male.Rows[0]["idMember"] + "/" + dt_Male.Rows[0]["idDo"] + ".jpg";
                style_name_male.Text = dt_Male.Rows[0]["do_name"].ToString();
                stylist_name_male.Text = dt_Male.Rows[0]["firstname"] + " " + dt_Male.Rows[0]["lastname"];
                if (Request.Cookies["hr_main_ck"] != null)
                {
                    AddToFavesMale.Visible = true;
                    AddToFavesMale.NavigateUrl = "javascript:addFave('" + dt_Male.Rows[0]["idDo"].ToString() + "','" + Request.Cookies["hr_main_ck"]["user_id"] + "');";
                }
            }

            if (dt_Female.Rows.Count > 0)
            {
                do_of_day_female.ImageUrl = "images/Users/" + dt_Female.Rows[0]["idMember"] + "/" + dt_Female.Rows[0]["idDo"] + ".jpg";
                style_name_female.Text = dt_Female.Rows[0]["do_name"].ToString();
                stylist_name_female.Text = dt_Female.Rows[0]["firstname"] + " " + dt_Female.Rows[0]["lastname"];
                if (Request.Cookies["hr_main_ck"] != null)
                {
                    AddToFavesFemale.Visible = true;
                    AddToFavesFemale.NavigateUrl = "javascript:addFave('" + dt_Female.Rows[0]["idDo"].ToString() + "','" + Request.Cookies["hr_main_ck"]["user_id"] + "');";
                }
            }
        }
    }
}