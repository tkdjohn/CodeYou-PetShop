using FluentAssertions;

namespace PetShop.DomainEntities.Tests {
    public class EntityBaseTests {
        // XUnit tests are very similar to MSTest tests. But we use [Fact] instead of [Test]        
        // We're also using Fluent Assertions in this test class. Note how they are a bit more
        // intuitive than MSTest's Asserts
        [Fact]
        public void EntityBaseConstructorShouldSetAuditDates() {
            // Arrange

            // Act
            var actual = new EntityBase();

            // Assert
            actual.Should().NotBeNull();
            actual.LastUpdatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            actual.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void EntityBaseShouldNotDeserializeDates() {
            // Arrange
            var entity = new EntityBase();

            // Act
            var actual = entity.Serialize();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().NotContain(nameof(EntityBase.CreatedDate));
            actual.Should().NotContain(nameof(EntityBase.LastUpdatedDate));
        }
    }
}
