using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.OTP
{
    public class OTPManager
    {
        private IOTPStrategy _oTPStrategy;
        public OTPManager(IOTPStrategy oTPStrategy)
        {
            _oTPStrategy = oTPStrategy;
        }
        public string GenerateOTP()
        {
            return _oTPStrategy.GenerateOTP();
        }
    }
}