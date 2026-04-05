using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        public ActionResult Index()
        {
            return View(database.KHACHHANGs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(KHACHHANG khachhang)
        {
            database.KHACHHANGs.Add(khachhang);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            return View(database.KHACHHANGs.Where(s => s.MaKH == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            return View(database.KHACHHANGs.Where(s => s.MaKH == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(string id, KHACHHANG khachhang)
        {
            database.Entry(khachhang).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            return View(database.KHACHHANGs.Where(s => s.MaKH == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string id, KHACHHANG khachhang)
        {
            khachhang = database.KHACHHANGs.Where(s => s.MaKH == id).FirstOrDefault();
            database.KHACHHANGs.Remove(khachhang);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}