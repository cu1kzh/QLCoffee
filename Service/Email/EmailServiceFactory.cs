using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Email
{
    public class EmailServiceFactory
    {
       public static IEmailService CreateEmailService()
        {
            return new GmailService();
        }
    }
}