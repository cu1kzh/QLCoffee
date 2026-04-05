using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Models.ViewModel
{
    public class ProListVM
    {
        public SANPHAM sanpham { get; set; } 
        public string SearchTerm { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        //public string SortTerm { get; set; }
        //public string SortOrder { get; set; }

        public SelectList ProductNames { get; set; }
        public string SelectedProName { get; set; }

        public SelectList Colors { get; set; }
        public string SelectedColor { get; set; }

        public PagedList.IPagedList<SANPHAM> ProductList { get; set; } 
        
    }
}