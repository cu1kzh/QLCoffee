using QLCoffee.Areas.Admin.AdminService.MauBuilder;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class MauController : Controller
    {
        private QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        private readonly MauBuilder _mauBuilder;
        private readonly MauDirector _mauDirector;

        public MauController()
        {
            // Khởi tạo Builder và Director
            _mauBuilder = new MauBuilder();
            _mauDirector = new MauDirector(_mauBuilder);
        }

        // GET: Admin/Mau
        public ActionResult Index()
        {
            return View(database.MAUs.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MAU mau)
        {
            var maxId = database.MAUs.Max(m => (int?)m.MaMau) ?? 0;
            var newId = maxId + 1;

            // Sử dụng Director để tạo đối tượng MAU đầy đủ
            MAU newMau = _mauDirector.BuildCompleteMau(
                newId,
                mau.TenMau,
                mau.RGB,
                new HashSet<SANPHAM>() // Khởi tạo collection rỗng
            );

            database.MAUs.Add(newMau);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(database.MAUs.Where(s => s.MaMau == id).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            return View(database.MAUs.Where(s => s.MaMau == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, MAU mau)
        {
            // Sử dụng Builder trực tiếp để tạo đối tượng cập nhật
            MAU updatedMau = _mauBuilder
                .SetMaMau(id)
                .SetTenMau(mau.TenMau)
                .SetRGB(mau.RGB)
                .Build();

            // Reset builder để sử dụng lại sau này
            _mauBuilder.Reset();

            database.Entry(updatedMau).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(database.MAUs.Where(s => s.MaMau == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, MAU mau)
        {
            mau = database.MAUs.Where(s => s.MaMau == id).FirstOrDefault();
            database.MAUs.Remove(mau);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}




//    public class MauController : Controller
//    {
//        // GET: Admin/Mau
//        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
//        public ActionResult Index()
//        {
//            return View(database.MAUs.ToList());
//        }
//        public ActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Create(MAU mau)
//        {
//            var maxId = database.MAUs.Max(m => (int?)m.MaMau) ?? 0;
//            mau.MaMau= maxId + 1;
//            database.MAUs.Add(mau);
//            database.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        public ActionResult Details(int id)
//        {
//            return View(database.MAUs.Where(s => s.MaMau == id).FirstOrDefault());
//        }
//        public ActionResult Edit(int id)
//        {
//            return View(database.MAUs.Where(s => s.MaMau == id).FirstOrDefault());
//        }
//        [HttpPost]
//        public ActionResult Edit(int id, MAU mau)
//        {
//            database.Entry(mau).State = System.Data.Entity.EntityState.Modified;
//            database.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        public ActionResult Delete(int id)
//        {
//            return View(database.MAUs.Where(s => s.MaMau == id).FirstOrDefault());
//        }
//        [HttpPost]
//        public ActionResult Delete(int id, MAU mau)
//        {
//            mau = database.MAUs.Where(s => s.MaMau == id).FirstOrDefault();
//            database.MAUs.Remove(mau);
//            database.SaveChanges();
//            return RedirectToAction("Index");
//        }
//    }
//}