﻿using System;
using System.IO;
using System.Text;
using System.Net;
using System.Web;

namespace HairSlayer
{
    public partial class IPN : System.Web.UI.Page
    {
        string connect_string = (System.Configuration.ConfigurationManager.ConnectionStrings["hslayer_connection"]).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            strRequest += "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://url:port#"));
            //req.Proxy = proxy;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            if (strResponse == "VERIFIED")
            {
                //try
                //{
                    //string hairslayer_pp_email = HttpContext.Current.Request["receiver_email"];
                    //string item_number = Request.Form["item_number1"];  //will hold user_id
                    string item_number = Request.Form["custom"]; 
                    string payment_status = Request.Form["payment_status"];
                    string transaction_type = Request.Form["txn_type"];

                    switch (payment_status)
                    {
                        case "Failed":
                            Worker.SqlInsert("UPDATE member SET Active = 0 WHERE idMember = " + item_number, connect_string);
                            break;
                        case "Denied":
                            Worker.SqlInsert("UPDATE member SET Active = 0 WHERE idMember = " + item_number, connect_string);
                            break;
                        case "Voided":
                            Worker.SqlInsert("UPDATE member SET Active = 0 WHERE idMember = " + item_number, connect_string);
                            break;
                        case "Completed":
                            Worker.SqlInsert("UPDATE member SET Active = 1 WHERE idMember = " + item_number, connect_string);
                            break;
                    }
                //}
                //catch
                //{ }
                //check the payment_status is Completed
                //check that txn_id has not been previously processed
                //check that receiver_email is your Primary PayPal email
                //check that payment_amount/payment_currency are correct
                //process payment
            }
            else if (strResponse == "INVALID")
            {
                //log for manual investigation
            }
            else
            {
                //log response/ipn data for manual investigation
            }
        }
    }
}