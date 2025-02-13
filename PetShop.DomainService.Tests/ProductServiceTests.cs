using Microsoft.Extensions.Logging;
using Moq;
using PetShop.Data;
using PetShop.DomainEntities;

namespace PetShop.DomainService.Tests {
    [TestClass]
    public sealed class ProductServiceTests {
        public required TestContext TestContext { get; set; }

        private readonly ILogger<ProductService> logger = new Logger<ProductService>(new LoggerFactory());
        private readonly Mock<IProductRepository> productRepositoryMoq = new();
        private readonly ProductService productService;

        public ProductServiceTests() {
            productService = new ProductService(logger,productRepositoryMoq.Object);
        }

        [TestMethod]
        public async Task GetProductById_CallsRepo() {
            // Arrange

            var expectedProduct = new Product {
                ProductId = 10,
                Name = "test product",
                Description = "description",
                Quantity = 1123,
                Price = 9.78M
            };

            // It.IsAny<int> means the mock will return expectedProduct when the repository's
            // GetAsync is called with ANY int. regardless of value, ex 123231 will work.
            productRepositoryMoq.Setup(prodRepo => prodRepo.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedProduct);
            
            // Act
            var actual = await productService.GetProductAsync(123231);

            // Assert
            Assert.AreEqual(expectedProduct, actual);

            productRepositoryMoq.Verify(prodRepo => prodRepo.GetAsync(123231), Times.Once());

        }
    }
}
