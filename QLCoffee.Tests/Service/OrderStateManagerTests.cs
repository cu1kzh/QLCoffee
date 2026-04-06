using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLCoffee.Service.OrderState;

namespace QLCoffee.Tests.Service
{
    /// <summary>
    /// Unit Tests cho State Pattern
    /// Kiểm tra logic chuyển đổi trạng thái của đơn hàng
    /// </summary>
    [TestClass]
    public class OrderStateManagerTests
    {
        // ============================
        // Tests cho Pending State
        // ============================

        [TestMethod]
        public void PendingState_Confirm_ShouldTransitionToProcessing()
        {
            // Arrange
            var manager = new OrderStateManager("Chờ xác nhận");

            // Act
            manager.ConfirmOrder();

            // Assert
            Assert.AreEqual("Đang xử lý", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void PendingState_Cancel_ShouldTransitionToCancelled()
        {
            // Arrange
            var manager = new OrderStateManager("Chờ xác nhận");

            // Act
            manager.CancelOrder();

            // Assert
            Assert.AreEqual("Đã hủy", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void PendingState_CanCancel_ShouldReturnTrue()
        {
            // Arrange
            var manager = new OrderStateManager("Chờ xác nhận");

            // Act & Assert
            Assert.IsTrue(manager.CanCancelOrder());
        }

        [TestMethod]
        public void PendingState_CanConfirm_ShouldReturnTrue()
        {
            // Arrange
            var manager = new OrderStateManager("Chờ xác nhận");

            // Act & Assert
            Assert.IsTrue(manager.CanConfirmOrder());
        }

        [TestMethod]
        public void PendingState_CanAssignShipper_ShouldReturnFalse()
        {
            // Arrange
            var manager = new OrderStateManager("Chờ xác nhận");

            // Act & Assert
            Assert.IsFalse(manager.CanAssignShipper());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PendingState_AssignShipper_ShouldThrowException()
        {
            // Arrange
            var manager = new OrderStateManager("Chờ xác nhận");

            // Act
            manager.AssignShipperToOrder();
        }

        // ============================
        // Tests cho Processing State
        // ============================

        [TestMethod]
        public void ProcessingState_AssignShipper_ShouldTransitionToShipping()
        {
            // Arrange
            var manager = new OrderStateManager("Đang xử lý");

            // Act
            manager.AssignShipperToOrder();

            // Assert
            Assert.AreEqual("Đang giao", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void ProcessingState_Cancel_ShouldTransitionToCancelled()
        {
            // Arrange
            var manager = new OrderStateManager("Đang xử lý");

            // Act
            manager.CancelOrder();

            // Assert
            Assert.AreEqual("Đã hủy", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void ProcessingState_CanAssignShipper_ShouldReturnTrue()
        {
            // Arrange
            var manager = new OrderStateManager("Đang xử lý");

            // Act & Assert
            Assert.IsTrue(manager.CanAssignShipper());
        }

        [TestMethod]
        public void ProcessingState_CanCancel_ShouldReturnTrue()
        {
            // Arrange
            var manager = new OrderStateManager("Đang xử lý");

            // Act & Assert
            Assert.IsTrue(manager.CanCancelOrder());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProcessingState_Confirm_ShouldThrowException()
        {
            // Arrange
            var manager = new OrderStateManager("Đang xử lý");

            // Act
            manager.ConfirmOrder();
        }

        // ============================
        // Tests cho Shipping State
        // ============================

        [TestMethod]
        public void ShippingState_Complete_ShouldTransitionToDelivered()
        {
            // Arrange
            var manager = new OrderStateManager("Đang giao");

            // Act
            manager.CompleteOrder();

            // Assert
            Assert.AreEqual("Hoàn thành", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void ShippingState_CanCancel_ShouldReturnFalse()
        {
            // Arrange
            var manager = new OrderStateManager("Đang giao");

            // Act & Assert
            Assert.IsFalse(manager.CanCancelOrder());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShippingState_Cancel_ShouldThrowException()
        {
            // Arrange & Act
            var manager = new OrderStateManager("Đang giao");

            // Act - Đây là điểm quan trọng: KHÔNG thể hủy khi đang giao
            manager.CancelOrder();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShippingState_AssignShipper_ShouldThrowException()
        {
            // Arrange
            var manager = new OrderStateManager("Đang giao");

            // Act - Shipper đã gán rồi
            manager.AssignShipperToOrder();
        }

        // ============================
        // Tests cho Delivered State
        // ============================

        [TestMethod]
        public void DeliveredState_IsDelivered_ShouldReturnTrue()
        {
            // Arrange
            var manager = new OrderStateManager("Hoàn thành");

            // Act & Assert
            Assert.IsTrue(manager.IsOrderDelivered());
        }

        [TestMethod]
        public void DeliveredState_CanCancel_ShouldReturnFalse()
        {
            // Arrange
            var manager = new OrderStateManager("Hoàn thành");

            // Act & Assert
            Assert.IsFalse(manager.CanCancelOrder());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeliveredState_Cancel_ShouldThrowException()
        {
            // Arrange
            var manager = new OrderStateManager("Hoàn thành");

            // Act
            manager.CancelOrder();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeliveredState_Complete_ShouldThrowException()
        {
            // Arrange
            var manager = new OrderStateManager("Hoàn thành");

            // Act
            manager.CompleteOrder();
        }

        // ============================
        // Tests cho Cancelled State
        // ============================

        [TestMethod]
        public void CancelledState_CanCancel_ShouldReturnFalse()
        {
            // Arrange
            var manager = new OrderStateManager("Đã hủy");

            // Act & Assert
            Assert.IsFalse(manager.CanCancelOrder());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CancelledState_Cancel_ShouldThrowException()
        {
            // Arrange
            var manager = new OrderStateManager("Đã hủy");

            // Act
            manager.CancelOrder();
        }

        // ============================
        // Tests cho State Transitions
        // ============================

        [TestMethod]
        public void FullWorkflow_Pending_Processing_Shipping_Delivered()
        {
            // Arrange
            var manager = new OrderStateManager();  // Mặc định Pending

            // Act & Assert
            Assert.AreEqual("Chờ xác nhận", manager.GetCurrentStateName());

            manager.ConfirmOrder();
            Assert.AreEqual("Đang xử lý", manager.GetCurrentStateName());

            manager.AssignShipperToOrder();
            Assert.AreEqual("Đang giao", manager.GetCurrentStateName());

            manager.CompleteOrder();
            Assert.AreEqual("Hoàn thành", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void Workflow_Pending_Cancelled()
        {
            // Arrange
            var manager = new OrderStateManager("Chờ xác nhận");

            // Act
            manager.CancelOrder();

            // Assert
            Assert.AreEqual("Đã hủy", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void Workflow_Processing_Cancelled()
        {
            // Arrange
            var manager = new OrderStateManager();
            manager.ConfirmOrder();

            // Act
            manager.CancelOrder();

            // Assert
            Assert.AreEqual("Đã hủy", manager.GetCurrentStateName());
        }

        // ============================
        // Tests cho OrderStateFactory
        // ============================

        [TestMethod]
        public void OrderStateFactory_CreateFromString_Pending()
        {
            // Act
            var state = OrderStateFactory.CreateStateFromString("Chờ xác nhận");

            // Assert
            Assert.AreEqual("Chờ xác nhận", state.GetStateName());
        }

        [TestMethod]
        public void OrderStateFactory_CreateFromString_Processing()
        {
            // Act
            var state = OrderStateFactory.CreateStateFromString("Đang xử lý");

            // Assert
            Assert.AreEqual("Đang xử lý", state.GetStateName());
        }

        [TestMethod]
        public void OrderStateFactory_CreateFromString_Shipping()
        {
            // Act
            var state = OrderStateFactory.CreateStateFromString("Đang giao");

            // Assert
            Assert.AreEqual("Đang giao", state.GetStateName());
        }

        [TestMethod]
        public void OrderStateFactory_CreateFromString_Delivered()
        {
            // Act
            var state = OrderStateFactory.CreateStateFromString("Hoàn thành");

            // Assert
            Assert.AreEqual("Hoàn thành", state.GetStateName());
        }

        [TestMethod]
        public void OrderStateFactory_CreateFromString_Cancelled()
        {
            // Act
            var state = OrderStateFactory.CreateStateFromString("Đã hủy");

            // Assert
            Assert.AreEqual("Đã hủy", state.GetStateName());
        }

        [TestMethod]
        public void OrderStateFactory_CreateFromString_UnknownState_DefaultsToPending()
        {
            // Act
            var state = OrderStateFactory.CreateStateFromString("Trạng thái không hợp lệ");

            // Assert
            Assert.AreEqual("Chờ xác nhận", state.GetStateName());
        }

        [TestMethod]
        public void OrderStateFactory_CreateFromString_NullOrEmpty_DefaultsToPending()
        {
            // Act
            var state1 = OrderStateFactory.CreateStateFromString(null);
            var state2 = OrderStateFactory.CreateStateFromString("");

            // Assert
            Assert.AreEqual("Chờ xác nhận", state1.GetStateName());
            Assert.AreEqual("Chờ xác nhận", state2.GetStateName());
        }

        // ============================
        // Tests cho Initial State
        // ============================

        [TestMethod]
        public void OrderStateManager_DefaultConstructor_ShouldInitializeToPending()
        {
            // Act
            var manager = new OrderStateManager();

            // Assert
            Assert.AreEqual("Chờ xác nhận", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void OrderStateFactory_GetInitialState_ShouldReturnPending()
        {
            // Act
            var state = OrderStateFactory.GetInitialState();

            // Assert
            Assert.AreEqual("Chờ xác nhận", state.GetStateName());
        }

        // ============================
        // Edge Cases & Error Handling
        // ============================

        [TestMethod]
        public void MultipleTransitions_ShouldMaintainCorrectState()
        {
            // Arrange
            var manager = new OrderStateManager();

            // Act & Assert - Confirm multiple times
            manager.ConfirmOrder();
            Assert.AreEqual("Đang xử lý", manager.GetCurrentStateName());

            try
            {
                manager.ConfirmOrder();  // Không thể confirm lại
                Assert.Fail("Should have thrown exception");
            }
            catch (InvalidOperationException)
            {
                // Expected
            }

            // State vẫn là Processing
            Assert.AreEqual("Đang xử lý", manager.GetCurrentStateName());
        }

        [TestMethod]
        public void CancelFromPending_ThenTryOtherActions_ShouldFail()
        {
            // Arrange
            var manager = new OrderStateManager("Chờ xác nhận");

            // Act
            manager.CancelOrder();
            Assert.AreEqual("Đã hủy", manager.GetCurrentStateName());

            // Assert - Các hành động khác sẽ fail
            try
            {
                manager.ConfirmOrder();
                Assert.Fail("Should have thrown exception");
            }
            catch (InvalidOperationException)
            {
                // Expected
            }
        }
    }
}
