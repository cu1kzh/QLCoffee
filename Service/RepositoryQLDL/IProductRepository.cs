using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCoffee.Controllers.RepositoryQLDL
{
    public interface IProductRepository
    {
        //Tách biệt logic truy vấn khỏi Controller, giúp dễ bảo trì.
        //Có thể thay đổi nguồn dữ liệu mà không ảnh hưởng Controller.
        IQueryable<SANPHAM> GetProducts(string searchTerm);
        SANPHAM GetProductById(string id);
    }
}
