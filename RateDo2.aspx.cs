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
    public partial class RateDo2 : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                if (Request.QueryString["rating"] != null && Request.QueryString["idDo"] != null && Request.QueryString["idMember"] != null)
                {
                    MySqlConnection oCon = new MySqlConnection(connect_string);
                    MySqlCommand cmd = new MySqlCommand();
                    oCon.Open();
                    long member = 47; //ID of Anonymous User
                    if (Request.QueryString["idMember"] != "") { member = Convert.ToInt64(Request.QueryString["idMember"]); }
                    cmd = new MySqlCommand("sp_RateDo", oCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("rating", Convert.ToInt32(Request.QueryString["rating"]));
                    cmd.Parameters.AddWithValue("do_id", Convert.ToInt32(Request.QueryString["idDo"]));
                    cmd.Parameters.AddWithValue("mem_id", member);

                    cmd.ExecuteNonQuery();
                    oCon.Close();

                    Response.Write("Success");
                }
                else
                {
                    Response.Write("Error: Invalid Input");
                }
            /*}
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }*/
        }
    }
}