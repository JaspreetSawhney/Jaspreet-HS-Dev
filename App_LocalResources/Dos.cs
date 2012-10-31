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

public class Do
{
    private int idDo;
    private DateTime rmd_date;
    private int idMember;
    private int idStyleOwner;
    private char gender;

    public int Do_ID
    {
        get { return idDo; }
    }

    public DateTime UploadDate
    {
        get { return rmd_date; }
    }

    public int StyleOwner
    {
        get { return idStyleOwner; }
    }

    public int Member_ID
    {
        get { return idMember; }
    }

    public char Gender
    {
        get { return gender; }
    }

    public Do[] GetTopDos(string Gender)
    {
        System.Collections.ArrayList do_list = new System.Collections.ArrayList();

        return (Do[]) do_list.ToArray(typeof(Do));
    }
}