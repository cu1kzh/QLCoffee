using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Areas.Admin.AdminService.MauBuilder
{
    public class MauBuilder : IMauBuilder
    {
        // Đối tượng MAU đang được xây dựng
        private MAU _mau = new MAU();

        public IMauBuilder SetMaMau(int maMau)
        {
            _mau.MaMau = maMau;
            return this;
        }

        public IMauBuilder SetTenMau(string tenMau)
        {
            _mau.TenMau = tenMau;
            return this;
        }

        public IMauBuilder SetRGB(string rgb)
        {
            _mau.RGB = rgb;
            return this;
        }

        public IMauBuilder SetSanPhams(ICollection<SANPHAM> sanPhams)
        {
            if (sanPhams != null)
            {
                _mau.SANPHAMs = sanPhams;
            }
            return this;
        }

        public MAU Build()
        {
            // Trả về đối tượng đã hoàn thiện
            return _mau;
        }

        // Phương thức để reset builder
        public void Reset()
        {
            _mau = new MAU();
        }
    }
}