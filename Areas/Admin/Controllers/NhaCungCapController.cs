using QLCoffee.Areas.Admin.AdminService.DependencyInjectionNCC;
using QLCoffee.Areas.Admin.AdminService.ICommandNCC;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly IQuanLyQuanCoffeeEntities _database;

        // Constructor không tham số để tránh lỗi khi tạo instance
        public NhaCungCapController() : this(new QuanLyQuanCoffeeEntitiesService(new QuanLyQuanCoffeeEntities()))
        {
        }

        // Constructor có tham số sử dụng Dependency Injection
        public NhaCungCapController(IQuanLyQuanCoffeeEntities database)
        {
            _database = database;
        }

        // Action Index để hiển thị danh sách nhà cung cấp
        public ActionResult Index()
        {
            var nhacungcaps = _database.NHACUNGCAPs.ToList();
            return View(nhacungcaps);
        }

        
        [HttpPost]
        public ActionResult Create(NHACUNGCAP nhacungcap)
        {
            var command = new CreateNhaCungCapCommand(_database, nhacungcap);
            command.Execute();
            return RedirectToAction("Index");
        }
        [HttpGet]  // Thêm phương thức GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(NHACUNGCAP nhacungcap)
        {
            var command = new EditNhaCungCapCommand(_database, nhacungcap);
            command.Execute();
            return RedirectToAction("Index");
        }
        public ActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            var command = new DeleteNhaCungCapCommand(_database, id);
            command.Execute();
            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(string id)
        {
            try
            {
                var command = new DeleteNhaCungCapCommand(_database, id);
                command.Execute();
                return Json(new { success = true, message = "Xóa nhà cung cấp thành công!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Không thể xóa nhà cung cấp vì có dữ liệu liên quan!" });
            }
        }
    }
}