using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;

namespace HairSlayer.UserControls
{
    public partial class MemberNavBar : System.Web.UI.UserControl
    {
        public event EventHandler My_DosButtonClicked;
        public event EventHandler My_FavesButtonClicked;
        //public event EventHandler Upload_My_DoButtonClicked;
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cookies["hr_main_ck"]["user_id"] = "1";
            if (HairSlayer.includes.Functions.is_logged_in()) { BindDataList(); }
        }

        protected void btnMy_Dos_Click(object sender, EventArgs e)
        {
            if (My_DosButtonClicked != null)
            {
                My_DosButtonClicked(this, EventArgs.Empty);
            }

            /*if (Session["NewUpload"] != null)
            {
                if (Convert.ToBoolean(Session["NewUpload"]))
                {
                    BindDataList();
                }
                Session["NewUpload"] = false;
            }*/
        }

        protected void btnMy_Faves_Click(object sender, EventArgs e)
        {
            if (My_FavesButtonClicked != null)
            {
                My_FavesButtonClicked(this, EventArgs.Empty);
            }
        }

        public static bool chkDirectory(string mainDirectory)
        {
            if (Directory.Exists(mainDirectory))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void newDirectory(string locationDirName)
        {
            //this will create a new Directory At Specified Location.
            Directory.CreateDirectory(locationDirName);
        }       

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblidDo = (Label)e.Item.FindControl("lblidDo");
                string idDo = lblidDo.Text;
                Label lblidMember = (Label)e.Item.FindControl("lblidMember");
                string idMember = lblidMember.Text;
                ImageButton img = (ImageButton)e.Item.FindControl("ImageButton1");
                img.ImageUrl = "../images/Users/" + idMember + "/" + idDo + ".jpg";
                img.PostBackUrl = "../Do.aspx?id=" + idDo;
            }
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Click")
            {
                Label lblidDo = (Label)e.Item.FindControl("lblidDo");
                string idDo = lblidDo.Text;
                //Int32 idDo= Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect("Do.aspx?id=" + idDo);
            }
        }

        public void BindDataList()
        {
            // Int32 idMember = 1;
            DataTable dt = new DataTable();
            MySqlConnection oCon = new MySqlConnection(connect_string);
            if (oCon.State == System.Data.ConnectionState.Closed)
            {
                oCon.Open();
            }
            DataSet ds = new DataSet();


            MySqlCommand cmd = new MySqlCommand("sp_PicsFaves", oCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_id", Convert.ToInt32(Request.Cookies["hr_main_ck"]["user_id"]));

            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            //MySqlDataAdapter da = new MySqlDataAdapter("select idDo,idMember from dos where idMembership IN (1,2,5,6) AND idMember=" + idMember, oCon);
            sda.Fill(ds);
            oCon.Close();

            DataList1.DataSource = ds.Tables[0];    //Table with dos 
            DataList1.DataBind();

            //FaveList.DataSource = ds.Tables[1];    //Table with faves 
            //FaveList.DataBind();
        }

        protected void FaveList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblidDo = (Label)e.Item.FindControl("lblidDo");
                string idDo = lblidDo.Text;
                Label lblidMember = (Label)e.Item.FindControl("lblidMember");
                string idMember = lblidMember.Text;
                ImageButton img = (ImageButton)e.Item.FindControl("ImageButton1");
                img.ImageUrl = "~/images/Users/" + idMember + "/" + idDo + ".jpg";
                img.PostBackUrl = "../Do.aspx?id=" + idDo;
            }
        }
        protected void FaveList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Click")
            {
                Label lblidDo = (Label)e.Item.FindControl("lblidDo");
                string idDo = lblidDo.Text;
                //Int32 idDo= Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect("Do.aspx?id=" + idDo);
            }
        }
    }
}