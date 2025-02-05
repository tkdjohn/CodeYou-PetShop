using FluentAssertions;

namespace PetShop.DomainEntities.Tests {

    public class OrderProductTests {
        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(3, 4, 7)]
        [InlineData(1, -1, 0)]
        [InlineData(2, -4, 0)]
        [InlineData(5, -2, 3)]
        public void AddQuantityShouldAdd(int initialQty, int addQty, int expectedQty) {
            // Arrange
            var orderProduct = new OrderProduct {
                OrderId = 1,
                OrderProductId = 2,
                ProductId = 3,
                UnitPrice = 1.23M,
                OrderQuantity = initialQty
            };

            // Act
            var actual = orderProduct.AddQuantity(addQty);

            // Assert
            actual.Should().Be(expectedQty);
            orderProduct.OrderQuantity.Should().Be(expectedQty);
        }

        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(3, 4, 4)]
        [InlineData(1, -1, 0)]
        [InlineData(11, -4, 0)]
        public void SetQuantityShouldSet(int initialQty, int setQty, int expectedQty) {
            // Arrange
            var orderProduct = new OrderProduct {
                OrderId = 1,
                OrderProductId = 2,
                ProductId = 3,
                UnitPrice = 1.23M,
                OrderQuantity = initialQty
            };

            // Act
            var actual = orderProduct.SetQuantity(setQty);

            // Assert
            actual.Should().Be(expectedQty);
            orderProduct.OrderQuantity.Should().Be(expectedQty);
        }
    }
}
