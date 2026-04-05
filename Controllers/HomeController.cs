using QLCoffee.Models;
using QLCoffee.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using QLCoffee.Controllers.RepositoryQLDL;

namespace QLCoffee.Controllers
{
    public class HomeController : Controller
    {
        QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();

        private readonly IProductRepository _productRepository;

        public HomeController()
        {
            _productRepository = new ProductRepository(); // Hoặc dùng Dependency Injection
        }

        public ActionResult TrangChu(string searchTerm, int? page)
        {
            var model = new HomeProductVM();
            var sp = _productRepository.GetProducts(searchTerm);

            int pageNumber = page ?? 1;
            int pageSize = 4; // Số sản phẩm mỗi trang

            model.FeaturedProducts = sp.OrderByDescending(p => p.CHITIET_HOADON.Count()).Take(4).ToList();
            model.NewProducts = sp.OrderBy(p => p.CHITIET_HOADON.Count()).Take(20).ToPagedList(pageNumber, pageSize);
            return View(model);
        }
        //Tách biệt logic truy vấn khỏi Controller, giúp dễ bảo trì.
        //Có thể thay đổi nguồn dữ liệu mà không ảnh hưởng Controller.
        public ActionResult ProductDetail(string id,int? quantity, int? page, string searchTerm) //PageList - Not Search
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sp = db.SANPHAMs.Find(id); //1

            if (sp == null)
            {
                return HttpNotFound();
            }
            
            //Lấy tất cả sản phẩm cùng danh mục
            var sanphams = db.SANPHAMs.Where(s => s.PRODUCT.MaLoaiSP == sp.PRODUCT.MaLoaiSP && s.MaSP != sp.MaSP).AsQueryable();
            ProductVM model = new ProductVM();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                sanphams = sanphams.Where(p => p.PRODUCT.NamePro.Contains(searchTerm) ||
                    p.PRODUCT.Desciption.Contains(searchTerm) ||
                    p.PRODUCT.LOAISANPHAM.TenLoaiSP.Contains(searchTerm));
            }
            //Đoạn code liên quan tới phân trang 
            //Lấy số trang hiện tại (Mặc định)
            int pageNumber = page ?? 1;
            int pageSize = 4;

            model.sanpham = sp;
            model.RelatedProduct = sanphams.OrderByDescending(s => s.MaSP).Take(8).ToList();
            model.TopProduct = sanphams.OrderBy(s => s.CHITIET_HOADON.Count()).Take(8).ToPagedList(pageNumber, pageSize);
            if(quantity.HasValue)
            {
                model.quantity = quantity.Value;
            }
            return View(model);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _ProductList(string id, string searchTerm/*,int? minPrice,int? maxPrice,string sortOrder,*/ ,int? page,string selectedProName,string selectedColor)
        {
            //Declare sp as an object of SANPHAMs
            SANPHAM sp = db.SANPHAMs.Find(id);

            //Get List of product names and colors from database
            var productNames = db.PRODUCTs.Select(p => p.NamePro).Distinct().ToList();
            var colors = db.MAUs.Select(m => m.MaMau).Distinct().ToList();

            //Declare model
            //Continue to step 3 on the ChatGPT Page //Try your bestttttt !

            var sanphams = db.SANPHAMs.AsQueryable(); 

            if (!string.IsNullOrEmpty(searchTerm))
            {               
                sanphams = sanphams.Where(p =>
                p.TenSP.Contains(searchTerm) ||
                p.PRODUCT.Desciption.Contains(searchTerm) ||
                p.PRODUCT.LOAISANPHAM.TenLoaiSP.Contains(searchTerm));
            }

            //If you want to export ProductList, You need use OrderBy. Then PageList can be used
            sanphams = sanphams.OrderBy(p => p.MaSP);

            //Sort products based on productName and color (Select an option from the dropdown list)
            if (!string.IsNullOrEmpty(selectedProName))
            {
                sanphams = sanphams.Where(p => p.PRODUCT.NamePro == selectedProName);
            }
            if (!string.IsNullOrEmpty(selectedColor))
            {
                sanphams = sanphams.Where(p => p.MAU.TenMau == selectedColor);
            }

            var model = new ProListVM
            {
                ProductNames = new SelectList(db.PRODUCTs.Select(p => p.NamePro).Distinct()),
                Colors = new SelectList(db.MAUs.Select(m => m.TenMau).Distinct()),
                SelectedProName = selectedProName,
                SelectedColor = selectedColor,          
                SearchTerm = searchTerm
              
            };

            int pageNumber = page ?? 1;
            int pageSize = 4; //Products of each page
            model.ProductList = sanphams.ToPagedList(pageNumber, pageSize); 
            /*model.SortOrder = sortOrder;*/ //Continue
            return View(model); 
        }

        public ActionResult _PVProList()
        {
            return View();
        }

        public ActionResult _PVNewProduct() //RelatedProduct and NewProduct are a group
        {
            return View();
        }
        public ActionResult _PVFeatuedProduct() //_PVFeatuedProduct and _PVTopProduct are a group
        {
            return View();
        }
        public ActionResult _PVRelatedProduct() 
        {
            return View();
        }
        public ActionResult _PVTopProduct()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}