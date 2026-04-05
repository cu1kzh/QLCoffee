using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace QLCoffee.Models.ViewModel
{
    public class SearchProductVM
    {
        public string SearchTerm { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string SortOrder { get; set; }
        public PagedList.IPagedList<SANPHAM> SanPhams { get; set; }


    }
}