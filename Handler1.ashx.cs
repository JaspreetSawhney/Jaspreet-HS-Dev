using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.TwiML;

namespace HairSlayer
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            context.Response.ContentType = "application/xml";

            TwilioResponse twiml = new TwilioResponse();
            string source = context.Request["src"] ?? String.Empty;

            if (source== string.Empty)
            {
                twiml.Play("http://example.com/greeting.mp3");
                twiml.Redirect("http://example.com/status.ashx?scr=mainmenu");
            }
            else
            {
                twiml.Say("To check the status of an order, press one");
                twiml.Say("To receive automated status updates via text message, press two");
                twiml.Say("To speak with a customer service representative, press three");
            }

           

            context.Response.Write(twiml.ToString());
            twiml.EndGather();
            twiml.Say("Goodbye");
            twiml.Hangup();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}