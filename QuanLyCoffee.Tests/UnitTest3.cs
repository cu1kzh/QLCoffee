using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLCoffee.Service.Decorator;

namespace QLCoffee.Tests
{
    [TestClass]
    public class DecoratorTests
    {
        [TestMethod]
        public void Test_OrderPrice_With_Shipping_And_Discount()
        {
            // Arrange
            double basePrice = 200000; // Tiền hàng: 200k
            double shippingFee = 35000; // Tiền ship: 35k
            double discount = 50000;    // Voucher giảm: 50k

            // Act: Xếp chồng các lớp Decorator
            IOrderPrice order = new BaseOrderPrice(basePrice);
            order = new ShippingFeeDecorator(order, shippingFee);
            order = new DiscountDecorator(order, discount);

            double finalPrice = order.CalculateTotal();

            // Assert: Kỳ vọng tổng tiền = 200k + 35k - 50k = 185k
            Assert.AreEqual(185000, finalPrice);
        }

        [TestMethod]
        public void Test_Discount_Does_Not_Drop_Below_Zero()
        {
            // Đơn 50k nhưng áp mã giảm 100k thì tổng tiền phải về 0 chứ không bị âm
            IOrderPrice order = new BaseOrderPrice(50000);
            order = new DiscountDecorator(order, 100000);

            Assert.AreEqual(0, order.CalculateTotal());
        }
    }
}