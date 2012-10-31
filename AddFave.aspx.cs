using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace HairSlayer
{
    public partial class AddFave : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idDo"] != null)
            {
                if (Request.QueryString["idDo"] != Request.QueryString["idMember"])
                {
                    MySqlConnection oConn = new MySqlConnection(connect_string);
                    oConn.Open();

                    MySqlCommand oComm = new MySqlCommand("sp_AddFave", oConn);
                    oComm.CommandType = System.Data.CommandType.StoredProcedure;
                    oComm.Parameters.AddWithValue("do_id", Request.QueryString["idDo"]);
                    oComm.Parameters.AddWithValue("mem_id", Request.QueryString["idMember"]);
                    oComm.ExecuteNonQuery();
                    oConn.Close();
                    DataTable dt = Worker.SqlTransaction("SELECT dos.idMember, dos.idDo FROM dos JOIN faves on dos.idDo = faves.idDo WHERE faves.idMember = " + Request.QueryString["idMember"] + " ORDER BY faves.tag_date", connect_string);
                    string img_spot = "", tmp_table = "<ul>";
                    foreach (DataRow dr in dt.Rows)
                    {
                        img_spot = dr["idMember"].ToString() + "/" + dr["idDo"].ToString() + ".jpg";
                        tmp_table += @"<li><div class=""thumb_wrap""><a href=""do.aspx?id=" + dr["idDo"].ToString() + @""" class=""full-gallery""><div class=""cover_thumbnail""></div><img src=""" + HairSlayer.includes.Thumbnails.CreateThumbnail(img_spot.Replace("/", @"\"), 150, 150, Convert.ToInt32(dr["idDo"])) + @""" /></div></a></li>";
                    }
                    tmp_table += "</ul>";
                    Response.Write(tmp_table);
                }
            }
        }
    }
}