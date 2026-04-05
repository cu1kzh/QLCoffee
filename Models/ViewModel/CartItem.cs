using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class CartItem //Đê lưu thông tin mỗi mặt hàng có trong giỏ
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public string ProImage { get; set; }

        public SANPHAM Sanpham { get; set; }

        public int TotalPrice => Quantity * UnitPrice;

        public decimal GiaSP { get; internal set; }
    }
}