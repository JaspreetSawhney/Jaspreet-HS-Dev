using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace HairSlayer
{
    public partial class RateProv : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["rating"] != null && Request.QueryString["id"] != null && Request.QueryString["idMember"] != null)
                {
                    MySqlConnection oCon = new MySqlConnection(connect_string);
                    MySqlCommand cmd = new MySqlCommand();
                    oCon.Open();


                    int total1 = Convert.ToInt32(Request.QueryString["rating"]);

                    string sql = "SELECT rates, rating FROM shop_rating WHERE idMember = " + Request.QueryString["id"];
                    DataTable dta = Worker.SqlTransaction(sql, connect_string);
                    double overall_rating = total1;
                    if (dta.Rows.Count > 0)
                    {
                        overall_rating = Convert.ToDouble(Convert.ToInt32(dta.Rows[0]["rating"]) + total1) / Convert.ToDouble(Convert.ToInt32(dta.Rows[0]["rates"]) + 1);
                        overall_rating = Math.Floor(overall_rating * 2 + 0.5) / 2;
                    }

                    cmd = new MySqlCommand("sp_RateProvider", oCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("nrating", total1);
                    cmd.Parameters.AddWithValue("mem_id", Convert.ToInt32(Request.QueryString["id"]));
                    cmd.Parameters.AddWithValue("rater_id", Convert.ToInt32(Request.QueryString["idMember"]));
                    cmd.Parameters.AddWithValue("overall_rate", overall_rating);

                    cmd.ExecuteNonQuery();
                    oCon.Close();

                    Response.Write("Success");
                }
                else
                {
                    Response.Write("Error: Invalid Input");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}