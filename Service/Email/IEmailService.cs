using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Email
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}