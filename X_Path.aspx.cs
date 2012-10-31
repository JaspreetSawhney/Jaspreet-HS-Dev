using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace HairSlayer
{
    public partial class X_Path : System.Web.UI.Page
    {
        string connect_string = (ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["key"] != null && Request.QueryString["app"] != null)
            {
                if (ValidateKey(Request.QueryString["key"]))
                {
                    MySqlConnection oCon = new MySqlConnection(connect_string);
                    oCon.Open();
                    DataTable dt = new DataTable("XMLData");
                    //GRAB VARIABLES
                    //NEED MemberID, DoID, Search_Location, Search_Description
                    switch (Request.QueryString["app"])
                    {
                        case "spPopulateStyles":
                            /*MySqlCommand cmd = new MySqlCommand(Request.QueryString["app"], oCon);
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (Request.QueryString["gender"] != null)
                            {
                                cmd.Parameters.AddWithValue("gender", Request.QueryString["gender"]);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("gender", "");
                            }
                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);

                            sda.Fill(dt);*/
                            string sql = "";
                            if (Request.QueryString["gender"] != null)
                            {
                                sql = "SELECT DISTINCT b.idDo, b.idMember, shop_name, '' As ln2, b.do_name FROM dos AS b JOIN shop AS c on b.idStyleOwner = c.idMember WHERE mem.idMembership IN ('" + Request.QueryString["gender"] + "') ORDER BY b.rmd_date DESC;";
                            }
                            else
                            {
                                sql = "SELECT DISTINCT b.idDo, b.idMember, shop_name, '' As ln2, b.do_name FROM dos AS b JOIN shop AS c on b.idStyleOwner = c.idMember ORDER BY b.rmd_date DESC;";
                            }
                            dt = Worker.SqlTransaction(sql, connect_string);

                            dt.Columns.Add(new DataColumn("RandomNum", Type.GetType("System.Int32")));

                            Random random = new Random();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i]["RandomNum"] = random.Next(1000);
                            }

                            DataView dv = new DataView(dt);
                            dv.Sort = "RandomNum";

                            dt = dv.ToTable();
                            break;
                    }


                    if (Request.QueryString["mid"] != null)
                    {
                        MySqlCommand cmd = new MySqlCommand(Request.QueryString["app"], oCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("m_id", Request.QueryString["mid"]);

                        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);

                        sda.Fill(dt);
                    }

                    if (Request.QueryString["did"] != null)
                    {
                        //
                    }

                    System.Xml.XmlWriter xml;
                    System.IO.TextWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer);

                    Response.Write(writer.ToString());
                    writer.Close();
                    oCon.Close();
                }
            }
        }

        protected bool ValidateKey(string ky)
        {
            return true;
        }
    }
}