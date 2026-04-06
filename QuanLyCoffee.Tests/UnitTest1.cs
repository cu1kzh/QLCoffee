using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLCoffee.Models;
using QLCoffee.Service.State;
using System;

namespace QLCoffee.Tests
{
    [TestClass]
    public class OrderStateTests
    {
        [TestMethod]
        public void Test_PendingState_Should_Proceed_To_Processing()
        {
            // 1. Giả lập đơn hàng mới (Chờ xác nhận)
            var order = new HOADON { MaHD = "HD0001", TrangThaiDH = "Chờ xác nhận" };
            var state = OrderStateManager.GetState(order.TrangThaiDH);

            // 2. Thực hiện hành động chuyển tiếp
            state.Proceed(order);

            // 3. Kiểm tra xem DB có đổi sang "Đang chuẩn bị hàng" không
            Assert.AreEqual("Đang xử lý", order.TrangThaiDH);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_DeliveredOrder_Cannot_Be_Cancelled()
        {
            // 1. Giả lập đơn đã giao
            var order = new HOADON { MaHD = "HD0001", TrangThaiDH = "Đã giao hàng" };
            var state = OrderStateManager.GetState(order.TrangThaiDH);

            // 2. Thử hủy đơn đã giao (Kỳ vọng sẽ văng ra lỗi)
            state.Cancel(order);
        }
    }
}