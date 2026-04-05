using QLCoffee.Models;
using QLCoffee.Models.ViewModel;
using QLCoffee.Service.Email;
using QLCoffee.Service.OTP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;


namespace QLCoffee.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        public ActionResult Login()
        {
            return View();
        }
        //POST: Account Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthenLogin(Models.TAIKHOAN taikhoan)
        {
            try
            {
                var check_ID = database.TAIKHOANs.Where(s => s.TenDN == taikhoan.TenDN).FirstOrDefault();
                var check_Pass = database.TAIKHOANs.Where(s => s.MatKhau == taikhoan.MatKhau).FirstOrDefault();
                var user = database.TAIKHOANs.Where(s => s.TenDN == taikhoan.TenDN).FirstOrDefault();
                if (check_ID == null || check_Pass == null)
                {
                    if (check_ID == null)
                    {

                        ViewBag.ErrorID = "Tên Đăng Nhập Sai";

                    }
                    if (check_Pass == null)
                    {

                        ViewBag.ErrorPass = "Mật Khẩu Đăng Nhập Sai";

                    }
                    return View("Login");
                }
                else
                {
                    if (user.PhanQuyen == "A")
                    {
                        //Luu thong tin xac thuc cua nguoi dung vao cookie
                        Session["TenDN"] = user.TenDN;
                        Session["PhanQuyen"] = user.PhanQuyen;
                       
                        return RedirectToAction("Index", "TaiKhoan", new {area = "Admin"});
                    }
                    else if (user.PhanQuyen == "C")
                    {
                        Session["TenDN"] = user.TenDN;
                        Session["PhanQuyen"] = user.PhanQuyen;
                       
                        return RedirectToAction("TrangChu", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Tên đăng nhập không tồn tại";
                        return View("Login");
                    }
                }
            } 
            catch
            {
                return View("Login");
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult AuthenRegister(Models.TAIKHOAN taikhoan)
        {
            var check_TenDN = database.TAIKHOANs.Where(s => s.TenDN == taikhoan.TenDN).FirstOrDefault();
            try
            {
                if (check_TenDN == null) {
                    var check_PhanQuyen = database.TAIKHOANs.Where(s => s.PhanQuyen == taikhoan.PhanQuyen).FirstOrDefault();
                    if (check_PhanQuyen == null)
                    {
                        taikhoan.PhanQuyen = "C";
                    }

                    database.TAIKHOANs.Add(taikhoan);
                    database.SaveChanges();   /*Lưu lại data*/
                    return RedirectToAction("Login","Login"); /*điều hướng về trang login*/
                }
                else
                {
                    ViewBag.ErrorMessage = "Tài khoản đã tồn tại.";
                    return View("Register", taikhoan);
                }
                
            }
            catch
            {
                ViewBag.ErrorMessage = "Trang đăng ký lỗi vui lòng thử lại.";
                return View("Register", taikhoan);
            }
        }
        public ActionResult Logout()
        {
            //Detele all session
            Session.Clear();          
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(OTPViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = database.TAIKHOANs.Where(s => s.TenDN == model.UserName && s.Email == model.Email).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrorMessage = "Email hoặc tài khoản không tồn tại.";
                return View();
            }


            IOTPStrategy otpStrategy = new RandomOTPStrategy();
            OTPManager otpManager = new OTPManager(otpStrategy);
            string otp = otpManager.GenerateOTP();

            Session["OTP"] = otp;
            Session["Email"] = model.Email;
            Session["UserName"] = model.UserName;
            Session["OTP_Expiration"] = DateTime.Now.AddMinutes(5);

            IEmailService emailService = EmailServiceFactory.CreateEmailService();
            emailService.SendEmail(model.Email, "Mã OTP đặt lại mật khẩu", $"Mã OTP của bạn là: {otp}");

            return RedirectToAction("VerifyOTP");
        }

        public ActionResult VerifyOTP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyOTP(string otp)
        {

            string sessionOTP = Session["OTP"] as string;
            DateTime? expiration = Session["OTP_Expiration"] as DateTime?;

            if (sessionOTP == null || expiration == null || DateTime.Now > expiration)
            {
                ViewBag.ErrorMessage = "Mã OTP hết hạn!";
                return View();
            }

            if (otp != sessionOTP)
            {
                ViewBag.ErrorMessage = "Mã OTP không hợp lệ";
                return View();
            }

            return RedirectToAction("ResetPassword");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                

                string email = Session["Email"] as string;
                string username = Session["UserName"] as string;
                string password = model.NewPassword;
                string ConfirmPassword = model.ConfirmPassword;


                if (string.IsNullOrEmpty(email))
                {
                    return RedirectToAction("ForgotPassword");
                }
            
                if (string.IsNullOrEmpty(password))
                {
                    return RedirectToAction("ResetPassword");
                }

                var user = database.TAIKHOANs.Where(s => s.TenDN == username && s.Email == email).FirstOrDefault(); //Search user name and email 

                if (user != null)
                {

                    user.MatKhau = password; // Nên hash mật khẩu trước khi lưu

                    //user.RePass = user.MatKhau;

                    database.Entry(user).State = EntityState.Modified;

                    database.SaveChanges();

                    Session.Clear();

                    return RedirectToAction("Login");

                }
            
           
            
                ModelState.AddModelError("", "Mật khẩu sai yêu cầu!");
                return View();
            
                
        }
        //hash password method
        //private string HashPassword(string password)
        //{
        //    using (var sha25 = System.Security.Cryptography.SHA256.Create())
        //    {
                
        //            var hashedBytes = sha25.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                
        //    }
        //}
    }
}