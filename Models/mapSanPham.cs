using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models
{
    public class mapSanPham
    {
        public bool UpdateImage(string idSanPham, string Image_url)
        {
            try
            {
                QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();
                var sp = db.SANPHAMs.Find(idSanPham);
                sp.PRODUCT.Img1 = Image_url;
                db.SaveChanges();
                return true;
            }

            catch
            { 
                return false;
            }
        }

    }
}