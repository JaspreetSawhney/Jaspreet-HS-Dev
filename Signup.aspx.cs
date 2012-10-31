using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace HairSlayer
{
    public partial class Signup : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmail.Attributes["onclick"] = "clearTextBox(this.id)";
                txtName.Attributes["onclick"] = "clearTextBox(this.id)";
                txtShopName.Attributes["onclick"] = "clearTextBox(this.id)";
                txtPassword.Attributes["onclick"] = "clearTextBox(this.id)";
                txtProviderEmail.Attributes["onclick"] = "clearTextBox(this.id)";
                txtProviderName.Attributes["onclick"] = "clearTextBox(this.id)";

                if (Request.QueryString["id"] != null)
                {
                    switch (Request.QueryString["id"])
                    {
                        case "1":
                            ProviderVisible();
                            break;
                        case "3":
                            MemberVisible();
                            break;
                        case "v3_Okj41a":
                            StudentPath();
                            break;
                    }
                }
            }

        }

        protected void StudentPath()
        {
            pnlSignUpType.Visible = false;
            lblBreadcrumb.Text = "> Student Registration";
            hdnType.Value = "Student";
            pnlShop.Visible = true;
            pnlMember.Visible = true;
            lblRegistration.Text = "Student Registration";
            txtShopName.Visible = false;
            txtSchoolName.Visible = true;
        }

        protected void ProviderVisible()
        {
            pnlSignUpType.Visible = false;
            lblBreadcrumb.Text = "> Provider Registration";
            hdnType.Value = "Provider";
            pnlShop.Visible = true;
            pnlMember.Visible = true;
            lblRegistration.Text = "Provider Registration";
            //pnlUserFacebook.Visible = false;
        }

        protected void MemberVisible()
        {
            pnlSignUpType.Visible = false;
            lblBreadcrumb.Text = "> Member Registration";
            hdnType.Value = "Member";
            pnlShop.Visible = false;
            pnlMember.Visible = true;
            lblRegistration.Text = "Member Registration";
            pnlUserFacebook.Visible = true;
            btnCreateAccount.Visible = true;
        }

        protected void btnMember_Click(object sender, EventArgs e)
        {
            MemberVisible();
            /*pnlSignUpType.Visible = false;
            lblBreadcrumb.Text = "> Member Registration";
            hdnType.Value = "Member";
            pnlShop.Visible = false;
            pnlMember.Visible = true;
            lblRegistration.Text = "Member Registration";
            pnlUserFacebook.Visible = true;*/
        }

        protected void btnProvider_Click(object sender, EventArgs e)
        {
            ProviderVisible();

            /*pnlSignUpType.Visible = false;
            lblBreadcrumb.Text = "> Provider Registration";
            hdnType.Value = "Provider";
            pnlShop.Visible = true;
            pnlMember.Visible = true;
            lblRegistration.Text = "Provider Registration";
            pnlUserFacebook.Visible = false;*/
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            DuplicateAccount.Visible = false;
            int membership_type = 0;
            MySqlConnection oConn = new MySqlConnection(connect_string);
            oConn.Open();

            MySqlCommand oComm = new MySqlCommand("sp_Register", oConn);
            oComm.CommandType = System.Data.CommandType.StoredProcedure;
            string pwd = "";
            if (txtPassword.Text == "fb__99$")
            {
                pwd = "RmFjZWJvb2s="; // "FACEBOOK";
                oComm.Parameters.AddWithValue("act", true);
            }
            else
            {
                pwd = HairSlayer.includes.Functions.ToBase64(txtPassword.Text.Trim());
                oComm.Parameters.AddWithValue("act", false);
            }
            string[] f_name = txtName.Text.Split(' ');
            oComm.Parameters.AddWithValue("first_name", f_name[0]);
           oComm.Parameters.AddWithValue("last_name", f_name[1]);
            oComm.Parameters.AddWithValue("e_mail", txtEmail.Text.Trim());
            oComm.Parameters.AddWithValue("pass_word", pwd);
            oComm.Parameters.AddWithValue("shopname", txtShopName.Text.Replace("Business Name", ""));
            if (hdnType.Value == "Member")
            {
                membership_type = 3;
                oComm.Parameters.AddWithValue("mem_type", membership_type);
            }
            else
            {
                //membership_type = Convert.ToInt32(ddlProviderType.SelectedValue);
                /******Initialize Membership Type as 0 For Providers*******/
                membership_type = 0;
                if (ddlProviderType.SelectedValue != "") { membership_type = Convert.ToInt32(ddlProviderType.SelectedValue); }
                oComm.Parameters.AddWithValue("mem_type", membership_type);
            }

            MySqlDataAdapter sda = new MySqlDataAdapter(oComm);

            System.Data.DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            oConn.Close();

            if (dt.Rows[0][0].ToString() != "-1")
            {
                pnlMember.Visible = false;
                pnlVerification.Visible = true;

                /*if (hdnType.Value == "Member")
                {                    
                    pnlMember.Visible = false;
                    //pnlInviteProvider.Visible = true;
                    pnlVerification.Visible = true;
                }
                else
                {                    
                    pnlMember.Visible = false;
                    //pnlInviteClients.Visible = true;
                    pnlVerification.Visible = true;
                }*/
                /***********Retrieve User ID***********/
                //lblBreadcrumb.Text = "idMember = " + dt.Rows[0][0].ToString();
                if (pwd == "RmFjZWJvb2s=")    //This is a facebook register
                {
                    Session["user_id"] = dt.Rows[0][0].ToString();
                    string sql = "UPDATE member SET active = 1 WHERE idMember = " + dt.Rows[0][0].ToString();
                    Worker.SqlInsert(sql, connect_string);
                    Response.Cookies["hr_main_ck"]["user_id"] = dt.Rows[0][0].ToString();
                    Response.Cookies["hr_main_ck"]["user_name"] = txtName.Text;
                    Response.Cookies["hr_main_ck"]["gender"] = "";
                    Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                    Response.Cookies["hr_main_ck"]["membership"] = membership_type.ToString();
                    Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                    Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                    Response.Cookies["hr_main_ck"]["validate"] = "false"; //"false";
                    Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);
                    if (membership_type == 3)
                    {
                        Response.Redirect("Invite.aspx");
                    }
                    else
                    {
                        if (ddlProviderType.SelectedValue == "1")
                        {
                            Response.Redirect("Feat.aspx?id=1");
                        }
                        else if (ddlProviderType.SelectedValue == "2")
                        {
                            Response.Redirect("Feat.aspx?id=2");
                        }
                    }
                }
                else
                {
                    if (membership_type == 3)
                    {
                        string site_url = "http://" + Request.Url.Host;
                        string body = "<p>Dear " + f_name[0] + ",</p><p>" +
                            "Thank you for your interest in Hair Slayer. </p>" +
                            @"<p>Please click the follow link in order to verify your account: <br> <a href=""" + site_url + @"/oAk3j_5.aspx?ip=" + HairSlayer.includes.Functions.ToBase64(dt.Rows[0][0].ToString()).Replace("=", "%3D") +
                            @"&coa=" + HairSlayer.includes.Functions.ToBase64(txtEmail.Text.Trim()).Replace("=", "%3D") + @""">http://www.hairslayer.com/signup.aspx</a></p><p>Thank You.</p>";
                        HairSlayer.includes.Functions.SendMail("auto-mailer@hairslayer.com", txtEmail.Text, "Welcome to HairSlayer.com " + f_name[0], body);
                    }
                    else
                    {
                        Session["user_id"] = dt.Rows[0][0].ToString();
                        if (ddlProviderType.SelectedValue == "1")
                        {
                            Response.Redirect("Feat.aspx?id=1");
                        }
                        else if (ddlProviderType.SelectedValue == "2")
                        {
                            Response.Redirect("Feat.aspx?id=2");
                        }
                    }
                }
            }
            else
            {
                DuplicateAccount.Visible = true;
            }
        }

        protected void btnSkip_Click(object sender, EventArgs e)
        {
            pnlInviteProvider.Visible = false;
            pnlVerification.Visible = true;
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            string site_url = "http://" + Request.Url.Host; 
            string body = "<p>Dear " + txtProviderName.Text + ",</p><p>" +
                txtName.Text + " has signed up for HairSlayer.com and would like for you to join in his/her experience. </p>" +
                @"<p>Simply click the follow link to connect more of your clients: <br> <a href=""" + site_url + @"/signup.aspx"">http://www.hairslayer.com/signup.aspx</a></p><p>Thank You.</p>";
            HairSlayer.includes.Functions.SendMail("auto-mailer@hairslayer.com", txtProviderEmail.Text.Trim(), "Join " + txtName.Text + " On HairSlayer.com", body);
            pnlInviteProvider.Visible = false;
            pnlVerification.Visible = true;
        }

        protected void ddlProviderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hdnType.Value == "Student")
            {
                if (ddlProviderType.SelectedValue == "1")
                {
                    btnStudentBarber.Visible = true;
                    btnStudentStylist.Visible = false;
                }
                else if (ddlProviderType.SelectedValue == "2")
                {
                    btnStudentBarber.Visible = false;
                    btnStudentStylist.Visible = true;
                }
            }
            else
            {
                if (ddlProviderType.SelectedValue == "1")
                {
                    btnCreateBarberAccount.Visible = true;
                    btnCreateStylistAccount.Visible = false;
                }
                else if (ddlProviderType.SelectedValue == "2")
                {
                    btnCreateStylistAccount.Visible = true;
                    btnCreateBarberAccount.Visible = false;
                }
            }
        }

        protected void btnStudentStylist_Click(object sender, EventArgs e)
        {
            DuplicateAccount.Visible = false;
            int membership_type = 7;
            MySqlConnection oConn = new MySqlConnection(connect_string);
            oConn.Open();

            MySqlCommand oComm = new MySqlCommand("sp_Register", oConn);
            oComm.CommandType = System.Data.CommandType.StoredProcedure;
            string pwd = "";
            if (txtPassword.Text == "fb__99$")
            {
                pwd = "RmFjZWJvb2s="; // "FACEBOOK";
                oComm.Parameters.AddWithValue("act", true);
            }
            else
            {
                pwd = HairSlayer.includes.Functions.ToBase64(txtPassword.Text.Trim());
                oComm.Parameters.AddWithValue("act", false);
            }
            string[] f_name = txtName.Text.Split(' ');
            oComm.Parameters.AddWithValue("first_name", f_name[0]);
            oComm.Parameters.AddWithValue("last_name", f_name[1]);
            oComm.Parameters.AddWithValue("e_mail", txtEmail.Text.Trim());
            oComm.Parameters.AddWithValue("pass_word", pwd);
            oComm.Parameters.AddWithValue("shopname", txtSchoolName.Text.Replace("School Name", ""));
            if (ddlProviderType.SelectedValue == "1") { membership_type = 6; }
                
            oComm.Parameters.AddWithValue("mem_type", membership_type);
            MySqlDataAdapter sda = new MySqlDataAdapter(oComm);

            System.Data.DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            oConn.Close();

            if (dt.Rows[0][0].ToString() != "-1")
            {
                pnlMember.Visible = false;
                pnlVerification.Visible = true;

                /***********Retrieve User ID***********/
                if (pwd == "RmFjZWJvb2s=")    //This is a facebook register
                {
                    Session["user_id"] = dt.Rows[0][0].ToString();
                    string sql = "UPDATE member SET active = 1 WHERE idMember = " + dt.Rows[0][0].ToString();
                    Worker.SqlInsert(sql, connect_string);
                    Response.Cookies["hr_main_ck"]["user_id"] = dt.Rows[0][0].ToString();
                    Response.Cookies["hr_main_ck"]["user_name"] = txtName.Text;
                    Response.Cookies["hr_main_ck"]["gender"] = "";
                    Response.Cookies["hr_main_ck"]["location"] = ""; // dt.Rows[0]["location"].ToString();
                    Response.Cookies["hr_main_ck"]["membership"] = membership_type.ToString();
                    Response.Cookies["hr_main_ck"]["last_visit"] = DateTime.Now.ToString();
                    Response.Cookies["hr_main_ck"]["exp"] = DateTime.Now.AddDays(7).ToString();
                    Response.Cookies["hr_main_ck"]["validate"] = "false"; //"false";
                    Response.Cookies["hr_main_ck"].Expires = DateTime.Now.AddDays(7);

                    pnlVerification.Visible = true;
                    pnlMember.Visible = false;
                }
                else
                {
                    string site_url = "http://" + Request.Url.Host;
                    string body = "<p>Dear " + f_name[0] + ",</p><p>" +
                        "Thank you for your interest in Hair Slayer. </p>" +
                        @"<p>Please click the follow link in order to verify your account: <br> <a href=""" + site_url + @"/oAk3j_5.aspx?ip=" + HairSlayer.includes.Functions.ToBase64(dt.Rows[0][0].ToString()).Replace("=", "%3D") +
                        @"&coa=" + HairSlayer.includes.Functions.ToBase64(txtEmail.Text.Trim()).Replace("=", "%3D") + @""">http://www.hairslayer.com/signup.aspx</a></p><p>Thank You.</p>";
                    HairSlayer.includes.Functions.SendMail("auto-mailer@hairslayer.com", txtEmail.Text, "Welcome to HairSlayer.com " + f_name[0], body);
                }
            }
            else
            {
                DuplicateAccount.Visible = true;
            }
        }

    }
}