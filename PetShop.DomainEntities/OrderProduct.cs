using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PetShop.DomainEntities.Tests")]
namespace PetShop.DomainEntities {
    public class OrderProduct : EntityBase {
        [Key]
        public int OrderProductId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int OrderQuantity {
            get => orderQuantity;
            internal set => SetQuantity(value);
        }
        private int orderQuantity;

        [Required]
        public decimal UnitPrice { get; set; }

        internal int SetQuantity(int value) {
            orderQuantity = value;
            if (orderQuantity < 0) {
                orderQuantity = 0;
            }
            return OrderQuantity;
        }

        internal int AddQuantity(int value) {
            // use OrderQuantity property here to take advantage of checks in SetQuantity/
            // instead of accessing the backing private property directly.
            OrderQuantity += value;
            return OrderQuantity;
        }
    }
}
