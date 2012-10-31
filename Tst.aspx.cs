using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Xml;

namespace HairSlayer
{
    public partial class Tst : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "SELECT DISTINCT idDo, a.idMember,do_name, a.idStyleOwner, firstname, lastname, a.Gender  FROM dos As a JOIN member As b ON a.idMember = b.idMember WHERE ratemydo = 1 " +
                "AND a.rmd_date BETWEEN '" + DateTime.Now.ToString("yyyy-mm-dd") + "' AND '" + DateTime.Now.AddDays(1).ToString("yyyy-mm-dd") + "' AND idDo NOT IN (SELECT idDo FROM rate_my_do_log As rmd WHERE rmd.idMember = )";
            Response.Write(sql);
            /**********GEO CODE ADDRESS********************
                string pst = "http://maps.googleapis.com/maps/api/geocode/xml?address=14435+Cabot+Lodge+Ln,Cypress,TX&sensor=false";
                PostSubmitter post = new PostSubmitter(pst);
                post.PostItems = new System.Collections.Specialized.NameValueCollection();

                post.Type = PostSubmitter.PostTypeEnum.Post;                
                string out_put = post.Post();
                XmlDocument xmlStuff = new XmlDocument();
                xmlStuff.Load(new System.IO.StringReader(out_put));

                if (xmlStuff.SelectSingleNode("GeocodeResponse").FirstChild.InnerText == "OK")
                {

                    foreach (XmlNode xn in xmlStuff.SelectSingleNode("GeocodeResponse").LastChild.ChildNodes)
                    {
                        if (xn.Name == "geometry")
                        {
                            Response.Write(xn.FirstChild.FirstChild.InnerText + ", " + xn.FirstChild.LastChild.InnerText);
                        }
                    }
                }*/
            //HostIpToLocation();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            includes.Functions.SendText("5126805103", "Lets Get It Man!");
        }

    }
}