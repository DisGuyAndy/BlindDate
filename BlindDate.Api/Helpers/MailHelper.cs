using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;
using BlindDate.Common.Models;
using System.Text;
using System.Net;

namespace BlindDate.Api.Helpers
{

    public class MailHelper
    {
        SmtpClient client = new SmtpClient();
        public MailHelper()
        {
            NetworkCredential basicCredential = new NetworkCredential("blinddate@syndicatecommunity.co.za", "lKH9&uC-DExc");
            client.Port = 26;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Host = "mail.syndicatecommunity.co.za";
            client.Credentials = basicCredential;
        }
        //email = blinddate@syndicatecommunity.co.za
        //pw = lKH9&uC-DExc
        public ResponseBase SendOTPMail(string to, string DisplayUserName, string OTP)
        {
            var res = new ResponseBase() { Success = false };
            try
            {
                MailMessage mail = new MailMessage("blinddate@syndicatecommunity.co.za", to);
                mail.Subject = "Blind Date Verify Email Address";
                mail.IsBodyHtml = true;
                StringBuilder body = new StringBuilder();
                body.AppendLine($"Good Day {DisplayUserName}");
                body.AppendLine("");
                body.AppendLine("Thank you for registering with Blind Date");
                body.AppendLine($"Please use the following otp to validate your email address: {OTP}");
                mail.Body = body.ToString();
                client.Send(mail);
                res.Success = true;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }
            return res;
        }
    }
}