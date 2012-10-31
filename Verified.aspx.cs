using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace HairSlayer
{
    public partial class Verified : System.Web.UI.Page
    {
        int dos;
        string rndm1;
        string connect_string = (ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            
      
           dos = Convert.ToInt32(Request.QueryString["doid"]);
           rndm1 = Request.QueryString["rndno"];
           getverifiedimage();
             
            
      
        }
        public void getverifiedimage()
        {
            string sql = "select fileurl,do_name,firstname,lastname from dos a join member b on a.idmember=b.idmember where idDo=" + dos + " and random_number=" + rndm1;
       // string sql="select fileurl,idmember from dos where idDo="+ dos + " and random_number="+ rndm1;
        System.Data.DataTable dt = Worker.SqlTransaction(sql, connect_string);
     
        if (dt.Rows.Count > 0)
        {
            rate_my_do_image.ImageUrl = dt.Rows[0]["fileurl"].ToString();
            stylist_name.Text = dt.Rows[0]["firstname"].ToString()+"  " + dt.Rows[0]["lastname"].ToString();
            doname1.Text = dt.Rows[0]["do_name"].ToString();
            string sql1 = "update dos set verified=1  where idDo=" + dos;
            Worker.SqlTransaction(sql1, connect_string);
            lblverified.Text = "Verified";
            
         
        }
        else
        {
            lblverified.Text = "Invalid Url";
            rate_my_do_image.ImageUrl = "images/ratemydo.png";
        }
       
              
        }
    }
}