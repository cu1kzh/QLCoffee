using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class CheckoutVM
    {
       
        public List<CartItem> CartItems { get; set; }
        [Display(Name = "Mã hóa đơn")]
        public string MaHD { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        public System.DateTime NgayTD { get; set; }
        [Display(Name = "Tổng giá trị")]
        public int TongGiaTriHD { get; set; }
        [Display(Name = "Địa chỉ đơn hàng")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng.")] 
        public string DiaChiDH { get; set; }
       
        [Display(Name = "Trạng thái đơn hàng")]     
        public string TrangThaiDH { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string TenDN { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại phải có 10 số.")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Họ tên khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [RegularExpression("^[^\\d]+$", ErrorMessage = "Họ tên không được chứa số.")]
        public string HoTenKH { get; set; }

        public List<CHITIET_HOADON> CHITIET_HOADONs { get; set; }

        public CheckoutVM()
        {
            CartItems = new List<CartItem>();
        }
    }
}


