using QLCoffee.Areas.Admin.AdminService;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly QuanLyQuanCoffeeEntities database = DatabaseSingleton.Instance.Database;

        public ActionResult Index()
        {
            return View(database.NHANVIENs.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NHANVIEN nhanvien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    database.NHANVIENs.Add(nhanvien);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            return View(nhanvien);
        }

        public ActionResult Details(string id)
        {
            return View(database.NHANVIENs.FirstOrDefault(s => s.MaNV == id));
        }

        public ActionResult Edit(string id)
        {
            return View(database.NHANVIENs.FirstOrDefault(s => s.MaNV == id));
        }

        
        [HttpPost]
        public ActionResult Edit(string id, NHANVIEN nhanvien)
        {
            var existingNhanVien = database.NHANVIENs.Find(id);
            if (existingNhanVien == null)
            {
                return HttpNotFound();
            }

            // Cập nhật các thuộc tính từ dữ liệu gửi lên form
            existingNhanVien.TenNV = nhanvien.TenNV;
            existingNhanVien.SoDT = nhanvien.SoDT;
            existingNhanVien.GioiTinh = nhanvien.GioiTinh;
            existingNhanVien.DiaChi = nhanvien.DiaChi;
            existingNhanVien.NgaySinh = nhanvien.NgaySinh;
            existingNhanVien.ChucVu = nhanvien.ChucVu;

            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            return View(database.NHANVIENs.FirstOrDefault(s => s.MaNV == id));
        }

        [HttpPost]
        public ActionResult Delete(string id, NHANVIEN nhanvien)
        {
            nhanvien = database.NHANVIENs.FirstOrDefault(s => s.MaNV == id);
            if (nhanvien != null)
            {
                database.NHANVIENs.Remove(nhanvien);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

//namespace QLCoffee.Areas.Admin.Controllers
//{
//    public class NhanVienController : Controller
//    {
//        // GET: Admin/NhanVien
//        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
//        public ActionResult Index()
//        {
//            return View(database.NHANVIENs.ToList());
//        }
//        public ActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Create(NHANVIEN nhanvien)
//        {
//            database.NHANVIENs.Add(nhanvien);
//            database.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        public ActionResult Details(string id)
//        {
//            return View(database.NHANVIENs.Where(s => s.MaNV == id).FirstOrDefault());
//        }
//        public ActionResult Edit(string id)
//        {
//            return View(database.NHANVIENs.Where(s => s.MaNV == id).FirstOrDefault());
//        }
//        [HttpPost]
//        public ActionResult Edit(string id, NHANVIEN nhanvien)
//        {
//            database.Entry(nhanvien).State = System.Data.Entity.EntityState.Modified;
//            database.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        public ActionResult Delete(string id)
//        {
//            return View(database.NHANVIENs.Where(s => s.MaNV == id).FirstOrDefault());
//        }
//        [HttpPost]
//        public ActionResult Delete(string id, NHANVIEN nhanvien)
//        {
//            nhanvien = database.NHANVIENs.Where(s => s.MaNV == id).FirstOrDefault();
//            database.NHANVIENs.Remove(nhanvien);
//            database.SaveChanges();
//            return RedirectToAction("Index");
//        }
//    }
//}