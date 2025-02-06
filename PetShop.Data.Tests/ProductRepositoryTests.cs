using Microsoft.EntityFrameworkCore;
using PetShop.DomainEntities;

namespace PetShop.Data.Tests;

[TestClass]
public class ProductRepositoryTests {
    private ProductRepository productRepository;
    private IDatabaseContext db;

    public TestContext TestContext { get; set; }
    public ProductRepositoryTests() {
        //TODO: should this move or do we want a different database for each test class?
        // or do we setup injection 
        //TODO: context is disposable and should be managed as such with
        // either a using stmt or implementing disposable
        // this is an argument for a text fixture of some kind.
        var dbOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(nameof(ProductRepositoryTests))
            .Options;
        db = new DatabaseContext(dbOptions);
        productRepository = new ProductRepository(db);
    }

    [TestMethod]
    public async Task AddShouldAddAsync() {
        // Arrange
        var product = new Product {
            Description = "Description",
            Name = "Name",
            Price = 1.23M,
            ProductId = 77,
            Quantity = 111,
            CreatedDate = DateTime.Now,
            LastUpdatedDate = DateTime.Now
        };

        // Act
        Product actual = await productRepository.AddAsync(product);

        // Assert
        Assert.AreEqual(product, actual);
        var dbProduct = await db.Products.FindAsync(product.ProductId);
        Assert.IsNotNull(dbProduct);
        Assert.AreEqual(dbProduct, actual);
        TestContext.WriteLine("Yay!!!!");
    }

    [TestMethod]
    public async Task RemoveShouldRemoveAsync() {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();
    }

    [TestMethod]
    public async Task GetShouldGetByIdAsync() {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();
    }

    [TestMethod]
    public async Task GetShouldGetByNameAsync() {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();

    }

    [TestMethod]
    public async Task GeShouldGetAllAsync() {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();

    }

    [TestMethod]
    public async Task GetInStockShouldGetInStockAsync() {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();

    }

    [TestMethod]
    public async Task ProductExistsShouldReturnTrueAsync() {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();

    }

    [TestMethod]
    public async Task ProductExistsShouldReturnFalseAsync() {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();

    }
}