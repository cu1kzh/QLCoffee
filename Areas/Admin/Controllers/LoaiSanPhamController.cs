using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: Admin/LoaiSanPham
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        public ActionResult Index()
        {
            return View(database.LOAISANPHAMs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LOAISANPHAM loaisanpham)
        {
            database.LOAISANPHAMs.Add(loaisanpham);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            return View(database.LOAISANPHAMs.Where(s => s.MaLoaiSP == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            return View(database.LOAISANPHAMs.Where(s => s.MaLoaiSP == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(string id, LOAISANPHAM loaisanpham)
        {
            database.Entry(loaisanpham).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            return View(database.LOAISANPHAMs.Where(s => s.MaLoaiSP == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string id, LOAISANPHAM loaisanpham)
        {
            loaisanpham = database.LOAISANPHAMs.Where(s => s.MaLoaiSP == id).FirstOrDefault();
            database.LOAISANPHAMs.Remove(loaisanpham);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(string id)
        {
            try
            {
                LOAISANPHAM loaiSanPham = database.LOAISANPHAMs.Find(id);
                if (loaiSanPham == null)
                {
                    return Json(new { success = false, message = "Loại sản phẩm không tồn tại!" });
                }

                database.LOAISANPHAMs.Remove(loaiSanPham);
                database.SaveChanges();
                return Json(new { success = true, message = "Xóa loại sản phẩm thành công!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Không thể xóa loại sản phẩm này vì có dữ liệu liên quan!" });
            }
        }
    }
}