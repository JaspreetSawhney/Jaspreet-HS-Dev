using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace HairSlayer
{
    public partial class RateMyDo : System.Web.UI.Page
    {
        string connect_string = (ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    try
                    {
                        Convert.ToInt32(Request.QueryString["id"]);
                        GetSingleDo();
                    }
                    catch
                    {
                        getalldosbydatabase();
                        //GetAllDos();
                    }
                }
                else
                {
                    getalldosbydatabase();
                   // GetAllDos();
                }
            }
        }
        public void getalldosbydatabase()
        {
            //Have to find some way to order the rate my do queue
            string sql = "SELECT DISTINCT idDo, a.idMember,do_name, a.idStyleOwner, firstname, lastname, a.Gender,fileurl  FROM dos As a JOIN member As b ON a.idMember = b.idMember WHERE ratemydo = 1 " +
                " AND a.rmd_date BETWEEN '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "' AND idDo NOT IN (SELECT idDo FROM rate_my_do_log As rmd WHERE rmd.idMember = " + includes.Functions.generateUserID() + ")";
            //Response.Write(sql);
            if (Request.Cookies["hairslayer_preference"] != null)
            {
                if (Request.Cookies["hairslayer_preference"].Value == "female")
                {
                    sql += " AND a.Gender = 'F' ";
                }
                else if (Request.Cookies["hairslayer_preference"].Value == "male")
                {
                    sql += " AND a.Gender = 'M' ";
                }
            }
            sql += " ORDER BY idDo "; //LIMIT 0,1";
            //Response.Write(sql + "<BR>");
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);

            if (dt.Rows.Count > 0)
            {
                rate_my_do_image.ImageUrl = dt.Rows[0]["fileurl"].ToString();
                style_name.Text = dt.Rows[0]["do_name"].ToString();
                stylist_name.Text = dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"].ToString();
                DoID.Value = dt.Rows[0]["idDo"].ToString();
                Star.Value = dt.Rows[0]["Gender"].ToString();
                GetStyle.Visible = true;
                GetStyle.NavigateUrl = "schedule.aspx?id=aj_x" + dt.Rows[0]["idStyleOwner"].ToString();
                if (Request.Cookies["hr_main_ck"] != null)
                {
                    AddToFaves.Visible = true;
                    AddToFaves.NavigateUrl = "javascript:addFave('" + dt.Rows[0]["idDo"].ToString() + "','" + Request.Cookies["hr_main_ck"]["user_id"] + "');";
                }

                AddToFaves.NavigateUrl = "javascript:addFave('" + dt.Rows[0]["idDo"].ToString() + "','12');";
                ToggleRatingsControls(true);
            }
            else
            {
                rate_my_do_image.ImageUrl = "images/ratemydo.png";
                style_name.Text = "";
                stylist_name.Text = "";
                DoID.Value = "";
                Star.Value = "";
                ToggleRatingsControls(false);
            }
        
        }

        protected void GetAllDos()
        {
            //Have to find some way to order the rate my do queue
            string sql = "SELECT DISTINCT idDo, a.idMember,do_name, a.idStyleOwner, firstname, lastname, a.Gender  FROM dos As a JOIN member As b ON a.idMember = b.idMember WHERE ratemydo = 1 " +
                " AND a.rmd_date BETWEEN '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "' AND idDo NOT IN (SELECT idDo FROM rate_my_do_log As rmd WHERE rmd.idMember = " + includes.Functions.generateUserID() + ")";
            //Response.Write(sql);
            if (Request.Cookies["hairslayer_preference"] != null)
            {
                if (Request.Cookies["hairslayer_preference"].Value == "female")
                {
                    sql += " AND a.Gender = 'F' ";
                }
                else if (Request.Cookies["hairslayer_preference"].Value == "male")
                {
                    sql += " AND a.Gender = 'M' ";
                }
            }
            sql += " ORDER BY idDo "; //LIMIT 0,1";
            //Response.Write(sql + "<BR>");
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            
            if (dt.Rows.Count > 0)
            {
                rate_my_do_image.ImageUrl = "images/Users/" + dt.Rows[0]["idMember"] + "/" + dt.Rows[0]["idDo"] + ".jpg";
                style_name.Text = dt.Rows[0]["do_name"].ToString();
                stylist_name.Text = dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"].ToString();
                DoID.Value = dt.Rows[0]["idDo"].ToString();
                Star.Value = dt.Rows[0]["Gender"].ToString();
                GetStyle.Visible = true;
                GetStyle.NavigateUrl = "schedule.aspx?id=aj_x" + dt.Rows[0]["idStyleOwner"].ToString();
                if (Request.Cookies["hr_main_ck"] != null)
                {
                    AddToFaves.Visible = true;
                    AddToFaves.NavigateUrl = "javascript:addFave('" + dt.Rows[0]["idDo"].ToString() + "','" + Request.Cookies["hr_main_ck"]["user_id"] + "');";
                }

                AddToFaves.NavigateUrl = "javascript:addFave('" + dt.Rows[0]["idDo"].ToString() + "','12');";
                ToggleRatingsControls(true);
            }
            else
            {
                rate_my_do_image.ImageUrl = "images/ratemydo.png";
                style_name.Text = "";
                stylist_name.Text = "";
                DoID.Value = "";
                Star.Value = "";
                ToggleRatingsControls(false);
            }
        }

        protected void GetAllDos(string gender)
        {
            //Have to find some way to order the rate my do queue
            string sql = "SELECT DISTINCT idDo, a.idMember,do_name, a.idStyleOwner, firstname, lastname, a.Gender  FROM dos As a JOIN member As b ON a.idMember = b.idMember WHERE ratemydo = 1" +
            " AND a.rmd_date BETWEEN '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "' AND idDo NOT IN (SELECT idDo FROM rate_my_do_log As rmd WHERE rmd.idMember = " + includes.Functions.generateUserID() + ")";

            if (gender.ToUpper() != "ALL")
            {
                sql += " AND a.Gender = '" + gender + "' ";
            }
            sql += " ORDER BY idDo "; //LIMIT 0,1";

            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            //Response.Write(sql + " " + dt.Rows.Count + "<BR>");
            if (dt.Rows.Count > 0)
            {
                rate_my_do_image.ImageUrl = "images/Users/" + dt.Rows[0]["idMember"] + "/" + dt.Rows[0]["idDo"] + ".jpg";
                style_name.Text = dt.Rows[0]["do_name"].ToString();
                stylist_name.Text = dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"].ToString();
                DoID.Value = dt.Rows[0]["idDo"].ToString();
                Star.Value = dt.Rows[0]["Gender"].ToString();
                GetStyle.Visible = true;
                GetStyle.NavigateUrl = "schedule.aspx?id=aj_x" + dt.Rows[0]["idStyleOwner"].ToString();
                if (Request.Cookies["hr_main_ck"] != null)
                {
                    AddToFaves.Visible = true;
                    AddToFaves.NavigateUrl = "javascript:addFave('" + dt.Rows[0]["idDo"].ToString() + "','" + Request.Cookies["hr_main_ck"]["user_id"] + "');";
                }

                AddToFaves.NavigateUrl = "javascript:addFave('" + dt.Rows[0]["idDo"].ToString() + "','12');";
                ToggleRatingsControls(true);
            }
            else
            {
                rate_my_do_image.ImageUrl = "images/ratemydo.png";
                style_name.Text = "";
                stylist_name.Text = "";
                DoID.Value = "";
                Star.Value = "";
                ToggleRatingsControls(false);
            }
        }

        protected void GetSingleDo()
        {
            string sql = "SELECT DISTINCT idDo, a.idMember,do_name,idStyleOwner, firstname, lastname,fileurl  FROM dos As a JOIN member As b ON a.idMember = b.idMember WHERE idDo = " + Request.QueryString["id"];
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
            rate_my_do_image.ImageUrl = dt.Rows[0]["fileurl"].ToString();
            style_name.Text = dt.Rows[0]["do_name"].ToString();
            stylist_name.Text = dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"].ToString();
            DoID.Value = dt.Rows[0]["idDo"].ToString();
            GetStyle.NavigateUrl = "schedule.aspx?id=aj_x" + dt.Rows[0]["idStyleOwner"].ToString();            
        }

        protected void ToggleRatingsControls(bool status)
        {
            GetStyle.Visible = status;
            AddToFaves.Visible = status;
            right_scroll.Visible = status;
            pnlRatings.Visible = status;
        }

        protected void AllStyles_Click(object sender, EventArgs e)
        {
            Response.Cookies["hairslayer_preference"].Value = "all";
            Response.Cookies["hairslayer_preference"].Expires = DateTime.Now.AddYears(1);
            GetAllDos("ALL");
        }

        protected void Male_Click(object sender, EventArgs e)
        {
            Response.Cookies["hairslayer_preference"].Value = "male";
            Response.Cookies["hairslayer_preference"].Expires = DateTime.Now.AddYears(1);
            GetAllDos("M");
        }

        protected void Female_Click(object sender, EventArgs e)
        {
            Response.Cookies["hairslayer_preference"].Value = "female";
            Response.Cookies["hairslayer_preference"].Expires = DateTime.Now.AddYears(1);
            GetAllDos("F");
        }

        protected void right_scroll_Click(object sender, ImageClickEventArgs e)
        {
            string sql = "SELECT DISTINCT idDo, a.idMember,do_name, firstname, lastname, idStyleOwner, a.Gender  FROM dos As a JOIN member As b ON a.idMember = b.idMember WHERE ratemydo = 1" +
            " AND a.rmd_date BETWEEN '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "' ";

            if (Request.Cookies["hairslayer_preference"] != null)
            {
                if (Request.Cookies["hairslayer_preference"].Value == "female")
                {
                    sql += " AND a.Gender = 'F' ";
                }
                else if (Request.Cookies["hairslayer_preference"].Value == "male")
                {
                    sql += " AND a.Gender = 'M' ";
                }
            }
            sql += " AND idDo > " + DoID.Value + " ORDER BY idDo LIMIT 0,1";
            System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);

            if (dt.Rows.Count > 0)
            {
                rate_my_do_image.ImageUrl = "images/Users/" + dt.Rows[0]["idMember"] + "/" + dt.Rows[0]["idDo"] + ".jpg";
                style_name.Text = dt.Rows[0]["do_name"].ToString();
                stylist_name.Text = dt.Rows[0]["firstname"] + " " + dt.Rows[0]["lastname"].ToString();
                DoID.Value = dt.Rows[0]["idDo"].ToString();
                GetStyle.NavigateUrl = "schedule.aspx?id=aj_x" + dt.Rows[0]["idStyleOwner"].ToString();
                ToggleRatingsControls(true);
                Star.Value = dt.Rows[0]["Gender"].ToString();
            }
            else
            {
                rate_my_do_image.ImageUrl = "images/ratemydo.png";
                DoID.Value = "";
                Star.Value = "";
                style_name.Text = "";
                stylist_name.Text = "";
                ToggleRatingsControls(false);
            }
        }
    }
}