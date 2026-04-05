using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.OTP
{
    public class RandomOTPStrategy : IOTPStrategy
    {
        public string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();//Tạo OTP có 6 số

        }
    }
}