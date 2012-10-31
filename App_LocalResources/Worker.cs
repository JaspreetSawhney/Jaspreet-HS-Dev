using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MySql.Data.MySqlClient;


    public class Worker
    {

        public static DataTable SqlTransaction(string sql_statement, string connection_string)
        {
            DataTable dt = new DataTable("dt_items");
            MySqlConnection oConn = new MySqlConnection(connection_string);
            oConn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sql_statement, oConn);
            sda.Fill(dt);
            oConn.Close();
            return dt;
        }

        public static int SqlInsert(string sql_statement, string connection_string)
        {
            int rows_returned = 0;
            MySqlConnection oConn = new MySqlConnection(connection_string);
            oConn.Open();
            MySqlCommand oComm = new MySqlCommand(sql_statement, oConn);
            rows_returned = oComm.ExecuteNonQuery();
            oConn.Close();
            return rows_returned;
        }

        public static bool CreateCSV(DataTable dt, string location)
        {
            StreamWriter sw = new StreamWriter(location, false);

            foreach (DataRow dr in dt.Rows)
            {
                string temp_out = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        temp_out += @",""" + dr[i].ToString() + @"""";
                    }
                    else
                    {
                        temp_out += @"""" + dr[i].ToString() + @"""";
                    }
                }
                sw.WriteLine(temp_out);
            }
            sw.Close();
            return true;
        }

        public static void DeleteDo(int do_id)
        {
            MySqlConnection oConn = new MySqlConnection((System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString());
            oConn.Open();
            MySqlCommand cmd = new MySqlCommand("sp_DeleteDo", oConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_id", do_id);
            cmd.ExecuteNonQuery();
            oConn.Close();
        }
    }
