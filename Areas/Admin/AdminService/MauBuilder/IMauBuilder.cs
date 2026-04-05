using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCoffee.Areas.Admin.AdminService.MauBuilder
{
    public interface IMauBuilder
    {
        IMauBuilder SetMaMau(int maMau);
        IMauBuilder SetTenMau(string tenMau);
        IMauBuilder SetRGB(string rgb);
        IMauBuilder SetSanPhams(ICollection<SANPHAM> sanPhams);
        MAU Build();
    }
}
