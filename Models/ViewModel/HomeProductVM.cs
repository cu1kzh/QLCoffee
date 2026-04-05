using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QLCoffee.Models.ViewModel
{
    public class HomeProductVM
    {
        public SANPHAM sanpham { get; set; }
        //Tiêu chí để search sản phẩm theo tên, mã sp
        public string SearchTerm { get; set; }
        //Các thuộc tính hỗ trợ phân trang
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        //Danh sách sản phẩm nổi bật
        public List<SANPHAM> FeaturedProducts { get; set; } //
        //Danh sách sản phẩm mới đã phân trang
        public PagedList.IPagedList<SANPHAM> NewProducts { get; set; } //

    }
}