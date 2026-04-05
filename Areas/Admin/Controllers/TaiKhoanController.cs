using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: Admin/TaiKhoan
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        public ActionResult Index()
        {
            return View(database.TAIKHOANs.ToList());
        }
        public ActionResult Create()
        {
            //set up data of dropdown (Customer)
            var cus = database.KHACHHANGs.Select(kh => new SelectListItem
            {
                Value = kh.MaKH,
                Text = kh.HoTenKH

            }).ToList();
            cus.Insert(0, new SelectListItem { Value = null, Text = "--Chọn khách hàng--" });
            //set up data of dropdown (Staff)
            var staff = database.NHANVIENs.Select(kh => new SelectListItem
            {
                Value = kh.MaNV,
                Text = kh.TenNV

            }).ToList();
            //ADD data to the top of the list
            staff.Insert(0, new SelectListItem { Value = null, Text = "--Chọn nhân viên--" });

            //var customers = database.KHACHHANGs.ToList(); // Dữ liệu cho khách hàng
            //var employees = database.NHANVIENs.ToList(); // Dữ liệu cho nhân viên
            //
            ViewBag.MaKH = new SelectList(cus, "Value", "Text"); // Thay thế với giá trị thực tế
            ViewBag.MaNV = new SelectList(staff, "Value", "Text");
            return View();
        }
        [HttpPost]
        public ActionResult Create(TAIKHOAN taikhoan)
        {
            database.TAIKHOANs.Add(taikhoan);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            return View(database.TAIKHOANs.Where(s => s.TenDN == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            return View(database.TAIKHOANs.Where(s => s.TenDN == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(string id, TAIKHOAN taikhoan)
        {
            taikhoan.PhanQuyen = taikhoan.PhanQuyen.Trim();
            database.Entry(taikhoan).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            return View(database.TAIKHOANs.Where(s => s.TenDN == id).FirstOrDefault());
        }
        [HttpPost]
       
        public ActionResult DeleteConfirmed(string id)
        {
            var taikhoan = database.TAIKHOANs.FirstOrDefault(s => s.TenDN == id);

            if (taikhoan == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra nếu tài khoản có hóa đơn liên quan
            bool hasInvoices = database.HOADONs.Any(hd => hd.TenDN == id);
            if (hasInvoices)
            {
                TempData["ErrorMessage"] = "Không thể xóa tài khoản vì đã có hóa đơn liên quan!";
                return RedirectToAction("Index");
            }

            // Nếu không có dữ liệu liên quan, tiến hành xóa
            database.TAIKHOANs.Remove(taikhoan);
            database.SaveChanges();

            TempData["SuccessMessage"] = "Xóa tài khoản thành công!";
            return RedirectToAction("Index");
        }
    }
}