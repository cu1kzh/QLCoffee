using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace QLCoffee.Service.Email
{
    public class GmailService : IEmailService
    {
        public void SendEmail(string to, string subject,string body)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                string userName = ConfigurationManager.AppSettings["SmtpUser"];
                string password = ConfigurationManager.AppSettings["SmtpPass"];
                bool enableSs1 = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);


                //set SMPT client
                SmtpClient smtp = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = enableSs1
                };

                //Create Email
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(to);

                //Send email
                smtp.Send(mail);
                }
            catch(Exception ex) {
                throw new Exception("Gửi email thất bại: " + ex.Message);
            }

           //MailMessage mail = new MailMessage();
           //mail.From = new MailAddress(fromEmail);
           // mail.To.Add(to);
           // mail.Subject = subject;
           // mail.Body = body;

           // SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
           // smtp.Credentials = new NetworkCredential(fromEmail, password);
           // smtp.EnableSsl = true;

            //smtp.Send(mail);
        }
    }
}