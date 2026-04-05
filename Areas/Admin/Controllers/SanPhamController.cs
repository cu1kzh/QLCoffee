using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models.ViewModel;
using PagedList;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        public ActionResult Index(string id,string searchTerm,int? minPrice,int? maxPrice, int? page, string sortOrder)
        {
            SANPHAM sp = database.SANPHAMs.Find(id); //Create Object of SANPHAM
            //Search products based on keywords
            var model = new SearchProductVM(); //Declare model from SearchProductVM
            var sanphams = database.SANPHAMs.AsQueryable(); //Get data from Database
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                sanphams = sanphams.Where(p =>
                p.TenSP.Contains(searchTerm) ||
                p.PRODUCT.Desciption.Contains(searchTerm) ||
                p.PRODUCT.LOAISANPHAM.TenLoaiSP.Contains(searchTerm));
            }
            //Search product based on minPrice 
            if (minPrice.HasValue)
            {
                model.MinPrice = minPrice.Value;
                sanphams = sanphams.Where(p => p.GiaSP >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                model.MaxPrice = maxPrice.Value;
                sanphams = sanphams.Where(p => p.GiaSP <= maxPrice.Value);
            }
            switch (sortOrder)
            {
                case "name_asc": 
                    sanphams = sanphams.OrderBy(p => p.TenSP);
                    break;
                case "name_desc":
                    sanphams = sanphams.OrderByDescending(p => p.TenSP);
                    break;
                case "price_asc":
                    sanphams = sanphams.OrderBy(p => p.GiaSP);
                    break;
                case "price_desc":
                    sanphams = sanphams.OrderByDescending(p => p.GiaSP);
                    break;
                default:
                    sanphams = sanphams.OrderBy(p => p.TenSP);
                    break;
            }

            int pageNumber = page ?? 1;
            int pageSize = 8; //Products each page
            model.SanPhams = sanphams.ToPagedList(pageNumber, pageSize);
            model.SortOrder = sortOrder; //Continue
            
            return View(model);
        }
        public ActionResult Create()
        {

            var sanpham = new SANPHAM
            {
                NgaySX = DateTime.Now
            };
            ViewBag.IDPro = new SelectList(database.PRODUCTs, "IDPro", "NamePro", sanpham.IDPro);
            ViewBag.MaMau = new SelectList(database.MAUs, "MaMau", "TenMau", sanpham.MaMau);
            ViewBag.MaDungLuong = new SelectList(database.DUNGLUONGs, "MaDungLuong", "KichThuocDL", sanpham.MaDungLuong);
            return View(sanpham);
        }
        [HttpPost]

        public ActionResult Create(SANPHAM sanpham)
        {
            if (database.SANPHAMs.Any(sp => sp.MaSP == sanpham.MaSP))
            {
                ModelState.AddModelError("MaSP", "Khóa chính đã tồn tại!");
                ViewBag.IDPro = new SelectList(database.PRODUCTs, "IDPro", "NamePro", sanpham.IDPro);
                ViewBag.MaMau = new SelectList(database.MAUs, "MaMau", "TenMau", sanpham.MaMau);
                ViewBag.MaDungLuong = new SelectList(database.DUNGLUONGs, "MaDungLuong", "KichThuocDL", sanpham.MaDungLuong);
                return View(sanpham);
            }
      
            database.SANPHAMs.Add(sanpham);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            return View(database.SANPHAMs.Where(s => s.MaSP == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            var sanpham = new SANPHAM
            {
                NgaySX = DateTime.Now
            };
            ViewBag.IsEdit = true;
            ViewBag.IDPro = new SelectList(database.PRODUCTs, "IDPro", "NamePro");
            ViewBag.MaMau = new SelectList(database.MAUs, "MaMau", "TenMau");
            return View(database.SANPHAMs.Where(s => s.MaSP == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(string id, SANPHAM sanpham)
        {
            if (ModelState.IsValid)
            {
                database.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
                
            ViewBag.IDPro = new SelectList(database.PRODUCTs, "IDPro", "NamePro", sanpham.IDPro);
            ViewBag.MaMau = new SelectList(database.MAUs, "MaMau", "TenMau", sanpham.MaMau);
            return View(sanpham);
        }
        public ActionResult Delete(string id)
        {
            return View(database.SANPHAMs.Where(s => s.MaSP == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string id, SANPHAM sanpham)
        {
            sanpham = database.SANPHAMs.Where(s => s.MaSP == id).FirstOrDefault();
            database.SANPHAMs.Remove(sanpham);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update_Image(string idSanPham)
        {
            ViewBag.idSanPham = idSanPham;
            return View();

        }
        [HttpPost]
        public ActionResult Update_Image(string idSanPham, HttpPostedFileBase fileImage, SANPHAM sanpham)
        {
            sanpham = database.SANPHAMs.Where(s => s.MaSP == idSanPham).FirstOrDefault();
            if (fileImage == null) //nếu để trống thì báo lỗi
            {
                ViewBag.error = "File empty";
                ViewBag.idSanPham = idSanPham;
                return View();

            }
            if (fileImage.ContentLength == 0)//nếu thêm dữ liệu nhưng đó không có nội dung thì báo lỗi
            {
                ViewBag.error = "Don't have topic";
                ViewBag.idSanpham = idSanPham;
                return View();
            }
            //Xác định đường dẫn lưu file : Url tương đối => tuyệt đối 
            var urlTuongDoi = "/Data/Image/";
            var urlTuyetDoi = Server.MapPath(urlTuongDoi);
            //Lưu file (Chưa check same file)
            fileImage.SaveAs(urlTuyetDoi + fileImage.FileName);

            string fullDuongDan = urlTuyetDoi + fileImage.FileName;
            int i = 1;
            while (System.IO.File.Exists(fullDuongDan) == true)
            {
                string ten = Path.GetFileNameWithoutExtension(fullDuongDan);
                string duoi = Path.GetExtension(fileImage.FileName);
                fullDuongDan = urlTuyetDoi + ten + "-" + i + duoi;
                i++;
            }
            fileImage.SaveAs(fullDuongDan);

            mapSanPham map = new mapSanPham();
            map.UpdateImage(idSanPham, urlTuongDoi + fileImage.FileName);


            ViewBag.idSanPham = idSanPham;
            return RedirectToAction("Index");

        }

    }
}