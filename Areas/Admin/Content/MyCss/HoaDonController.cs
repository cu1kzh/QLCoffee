using QLCoffee.Areas.Admin.AdminService;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    //Lợi ích khi dùng Simple Factory
    //Tách biệt việc khởi tạo đối tượng HOADON giúp Controller gọn gàng hơn.
    //Dễ bảo trì & mở rộng, nếu sau này có thay đổi trong việc tạo hóa đơn(ví dụ: tạo mã tự động), bạn chỉ cần sửa trong Factory.
    //Tái sử dụng code, có thể gọi HoaDonFactory.CreateHoaDon() ở nhiều nơi khác mà không cần lặp lại logic khởi tạo.
    public class HoaDonController : Controller
    {
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();

        public ActionResult Index()
        {
            return View(database.HOADONs.ToList());
        }

        public ActionResult Details(string id)
        {
            var chitietHD = database.CHITIET_HOADON.Where(ct => ct.MaHD == id);
            return View(chitietHD);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string maHD, DateTime ngayHD, int tongGiaTriHD, string ghiChu, string trangThai, string maShipper, string tenDN, string hoTenKH, string sdt)
        {
            if (string.IsNullOrWhiteSpace(maHD) || tongGiaTriHD <= 0)
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                return View();
            }

            var hoaDon = HoaDonFactory.CreateHoaDon(maHD, ngayHD, tongGiaTriHD, ghiChu, trangThai, maShipper, tenDN, hoTenKH, sdt);
            database.HOADONs.Add(hoaDon);
            database.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult UpdateStatus(string id, string trangThai)
        {
            var hoaDon = database.HOADONs.FirstOrDefault(hd => hd.MaHD == id);
            if (hoaDon == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hóa đơn" });
            }

            hoaDon.TrangThaiDH = trangThai;

            try
            {
                database.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



    }

}