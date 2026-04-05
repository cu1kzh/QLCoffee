using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Controllers.RepositoryQLDL
{
    //Tách biệt logic truy vấn khỏi Controller, giúp dễ bảo trì.
    //Có thể thay đổi nguồn dữ liệu mà không ảnh hưởng Controller.
    public class ProductRepository : IProductRepository
    {
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();

        public IQueryable<SANPHAM> GetProducts(string searchTerm)
        {
            var query = db.SANPHAMs.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.PRODUCT.NamePro.Contains(searchTerm) ||
                                         p.PRODUCT.Desciption.Contains(searchTerm) ||
                                         p.PRODUCT.LOAISANPHAM.TenLoaiSP.Contains(searchTerm));
            }
            return query;
        }

        public SANPHAM GetProductById(string id)
        {
            return db.SANPHAMs.Find(id);
        }
    }
}