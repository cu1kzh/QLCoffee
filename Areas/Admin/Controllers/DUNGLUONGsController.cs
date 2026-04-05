using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class DUNGLUONGsController : Controller
    {
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();

        // GET: Admin/DUNGLUONGs
        public ActionResult Index()
        {
            return View(db.DUNGLUONGs.ToList());
        }

        // GET: Admin/DUNGLUONGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGLUONG dUNGLUONG = db.DUNGLUONGs.Find(id);
            if (dUNGLUONG == null)
            {
                return HttpNotFound();
            }
            return View(dUNGLUONG);
        }

        // GET: Admin/DUNGLUONGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DUNGLUONGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDungLuong,KichThuocDL")] DUNGLUONG dUNGLUONG)
        {
            if (ModelState.IsValid)
            {
                db.DUNGLUONGs.Add(dUNGLUONG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dUNGLUONG);
        }

        // GET: Admin/DUNGLUONGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGLUONG dUNGLUONG = db.DUNGLUONGs.Find(id);
            if (dUNGLUONG == null)
            {
                return HttpNotFound();
            }
            return View(dUNGLUONG);
        }

        // POST: Admin/DUNGLUONGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDungLuong,KichThuocDL")] DUNGLUONG dUNGLUONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dUNGLUONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dUNGLUONG);
        }

        // GET: Admin/DUNGLUONGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGLUONG dUNGLUONG = db.DUNGLUONGs.Find(id);
            if (dUNGLUONG == null)
            {
                return HttpNotFound();
            }
            return View(dUNGLUONG);
        }

        // POST: Admin/DUNGLUONGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DUNGLUONG dUNGLUONG = db.DUNGLUONGs.Find(id);
            db.DUNGLUONGs.Remove(dUNGLUONG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
