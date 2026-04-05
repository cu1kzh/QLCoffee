using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class LoginService
    {
        private readonly HttpSessionStateBase session;
        public LoginService(HttpSessionStateBase session)
        {
            this.session = session;

        }
        public void SetUserName(string userName)
        { 
            session["TenDN"] = userName;

        }
        public string GetUserName()
        {
            return session["TenDN"] as string;
        }
        public void ClearCart()
        {
            session["TenDN"] = null;
        }
    }
}