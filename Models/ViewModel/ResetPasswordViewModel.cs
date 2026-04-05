using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class ResetPasswordViewModel
    {
     
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!")]
        public string NewPassword { get; set; }

        
        [NotMapped]        
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác thực không đúng!")]  
        public string ConfirmPassword { get; set; }
    }
}