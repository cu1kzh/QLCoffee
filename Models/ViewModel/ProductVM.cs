using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace QLCoffee.Models.ViewModel
{
    public class ProductVM
    {  
        public SANPHAM sanpham { get; set; }
        public int quantity { get; set; } 
        public int estimatedValue { get; set; } 
         
        // Thuộc tính hổ trợ lớp phân trang 
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } = 3;

        //8 san pham cung danh mục
        public string SearchTerm { get; set; }
        public List<SANPHAM> RelatedProduct { get; set; }

        public PagedList.IPagedList<SANPHAM> TopProduct { get; set; }

    }
}