using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace QLCoffee.Areas.Admin.AdminService
{
    public static class HoaDonFactory

//Lợi ích khi dùng Simple Factory
//Tách biệt việc khởi tạo đối tượng HOADON giúp Controller gọn gàng hơn.
//Dễ bảo trì & mở rộng, nếu sau này có thay đổi trong việc tạo hóa đơn(ví dụ: tạo mã tự động), bạn chỉ cần sửa trong Factory.
//Tái sử dụng code, có thể gọi HoaDonFactory.CreateHoaDon() ở nhiều nơi khác mà không cần lặp lại logic khởi tạo.
    {
        public static HOADON CreateHoaDon(string maHD, DateTime ngayHD, int tongGiaTriHD, string ghiChu, string trangThai, string maShipper, string tenDN, string hoTenKH, string sdt)
        {
            return new HOADON
            {
                MaHD = maHD,
                NgayTD = ngayHD,
                TongGiaTriHD = tongGiaTriHD,
                GhiChu = ghiChu,
                TrangThaiDH = trangThai,
                MaShipper = maShipper,
                TenDN = tenDN,
                HoTenKH = hoTenKH,
                SDT = sdt
            };
        }
    }
}