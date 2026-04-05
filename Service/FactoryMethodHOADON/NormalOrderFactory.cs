using QLCoffee.Models.ViewModel;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Controllers.FactoryMethodHOADON
{
    public class NormalOrderFactory : IOrderFactory
    {
        public HOADON CreateOrder(string newOrderId, string login, CheckoutVM model, Cart cart)
        {
            return new HOADON
            {
                MaHD = newOrderId,
                TenDN = login,
                NgayTD = DateTime.Now.Date,
                TongGiaTriHD = cart.TotalValue(),
                TrangThaiDH = "Đang xử lý",
                GhiChu = model.DiaChiDH,
                HoTenKH = model.HoTenKH,
                SDT = model.SoDienThoai,
                CHITIET_HOADON = cart.Items.Select(item => new CHITIET_HOADON
                {
                    MaHD = newOrderId,
                    MaSP = item.MaSP,
                    Soluong = item.Quantity,                   
                    TongTien = item.TotalPrice
                }).ToList()
            };
        }
    }
}