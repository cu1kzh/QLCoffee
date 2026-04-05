using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Areas.Admin.AdminService.MauBuilder
{
    public class MauDirector
    {
        private readonly IMauBuilder _builder;

        public MauDirector(IMauBuilder builder)
        {
            _builder = builder;
        }

        // Tạo mẫu cơ bản chỉ với mã và tên
        public MAU BuildBasicMau(int maMau, string tenMau)
        {
            return _builder
                .SetMaMau(maMau)
                .SetTenMau(tenMau)
                .Build();
        }

        // Tạo mẫu đầy đủ với tất cả thông tin
        public MAU BuildCompleteMau(int maMau, string tenMau, string rgb, ICollection<SANPHAM> sanPhams)
        {
            return _builder
                .SetMaMau(maMau)
                .SetTenMau(tenMau)
                .SetRGB(rgb)
                .SetSanPhams(sanPhams)
                .Build();
        }
    }
}