using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Configuration;
using System.Web.Script.Services;

namespace HairSlayer
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] GetProviderName(string prefixText, int count, string contextKey)
        {
            DataTable dt = Worker.SqlTransaction("SELECT CONCAT(mem.firstname, ' ', mem.lastname, '(', sh.shop_name, ')') as provider FROM member As mem JOIN shop As sh ON mem.idMember = sh.idMember WHERE CONCAT(mem.firstname, ' ', mem.lastname) LIKE '%" + prefixText + "%' ORDER by mem.firstname", ConfigurationManager.ConnectionStrings["hslayer_connection"].ConnectionString);

            return ToArray(dt);
        }

        private string[] ToArray(DataTable dt)
        {
            var ar = new string[dt.Rows.Count];
            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ar[index] = dr["provider"].ToString();
                index++;
            }

            return ar;
        }

    }
}
