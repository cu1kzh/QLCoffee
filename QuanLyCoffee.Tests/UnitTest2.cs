using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLCoffee.Models;
using QLCoffee.Service.Observer;

namespace QLCoffee.Tests
{
    // 1. Tạo một Người nghe
    public class MockObserver : IOrderObserver
    {
        public bool WasNotified { get; private set; } = false;

        public void Update(HOADON order, string message)
        {
            WasNotified = true; // Đánh dấu là đã nhận được tin
        }
    }

    [TestClass]
    public class ObserverTests
    {
        [TestMethod]
        public void Test_Notifier_Should_Alert_All_Observers()
        {
            // Arrange (Chuẩn bị)
            var order = new HOADON { MaHD = "HD9999" };
            var notifier = new OrderNotifier();
            var mockObserver = new MockObserver(); // Tạo gián điệp

            notifier.Attach(mockObserver); // Gài gián điệp vào danh sách nhận tin

            // Act (Hành động)
            notifier.Notify(order, "Test tin nhắn");

            // Assert (Kiểm tra xem gián điệp có bắt được tin không)
            Assert.IsTrue(mockObserver.WasNotified);
        }
    }
}