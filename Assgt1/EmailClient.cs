using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
namespace Assgt1
{
    public static class EmailClient
    {

        public static void sendEmail(MailAddress to,string subject, string body)
        {
            MailAddress from = new MailAddress("richardsmithelectronics@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;

            //un-comment later on, probably, if I get around to it
            //message.CC.Add("");

            //a little letter of discouragement
            //you know for security and stuff
            //success.Body = "Congratulations! Your account was created successfully, \r\nUnless you're a robot, in which case you can p**s off";
            message.Body = body;

            SmtpClient tempClient = new SmtpClient("smtp.gmail.com", 587);
            tempClient.EnableSsl = true;
            tempClient.UseDefaultCredentials = false;

            //credentials are always the same since the mail client acts on behalf of the site
            tempClient.Credentials = new System.Net.NetworkCredential("richardsmithelectronics@gmail.com", "hardPass1");
            tempClient.DeliveryMethod = SmtpDeliveryMethod.Network;


            tempClient.Send(message);
        }
    }
}