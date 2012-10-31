using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.TwiML;
using Twilio.WebMatrix;
using RestSharp;



namespace HairSlayer
{
    
    public partial class TwilioVoiceCall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string accountSid = "AC49ecef1b877e244ea13ada1b5fe92b85";
            //string applicationsid = "AP5556f6cd1af5cfa5317acae7a49eeb5c";
            string sid = "CA748c6019014ca0ac151908a81299f927";
            string authToken = "4eeeaf7ed6459dcd02ec7888149d5ea1";
            string recordingSid = "RE5dabfb2da314b39cd87dc151bf669e78";
           DateTime datecreated=DateTime.Now;
            TwilioRestClient client; 
            client = new TwilioRestClient(accountSid, authToken);
            string APIversuion = client.ApiVersion;
            string TwilioBaseURL = client.BaseUrl;
         
            Account account = client.GetAccount(accountSid);
            client.GetRecording(recordingSid);
            client.GetRecordingText(recordingSid);
            client.ListRecordings(sid, datecreated, 1, 2);
            client.ListQueues();
            client.ListIncomingPhoneNumbers();
            this.varDisplay.Items.Clear(); 
            if (this.txtcall.Text == "" || this.message.Text == "") 
            { this.varDisplay.Items.Add( "You must enter a phone number and a message."); } else { // Retrieve the values entered by the user. 
                string to = this.txtcall.Text; 
                string myMessage = this.message.Text;
                //string Url = "http://demo.twilio.com/Welcome/Call/";
            String Url = "http://twimlets.com/message?Message%5B0%5D=" + myMessage.Replace(" ", "%20"); 
                // Diplay the enpoint, API version, and the URL for the message. 
                this.varDisplay.Items.Add("Using Tilio endpoint " + TwilioBaseURL);
                this.varDisplay.Items.Add("Twilioclient API Version is " + APIversuion);
                this.varDisplay.Items.Add("The URL is " + Url); // Instantiate the call options that are passed // to the outbound call. 
                CallOptions options = new CallOptions(); // Set the call From, To, and URL values into a hash map. // This sample uses the sandbox number provided by Twilio // to make the call.
                options.From = "+14242165015";
                options.To = to;
                options.Url = Url; // Place the call.
                options.Record = true;
                var call = client.InitiateOutboundCall(options);
                this.varDisplay.Items.Add("Call status: " + call.Status); } } } }  
      

