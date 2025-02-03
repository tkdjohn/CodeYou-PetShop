using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities {
    public class OrderProduct :BaseEntity {
        [Key]
        public int OrderProductId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [Required]
        public int ProductId { get; set; }

        [Required]
        //TODO: make readonly (private set)
        public int OrderQuantity { get;  set; }

        [Required]
        public decimal UnitPrice { get; set; }

        internal void AddQuantity(int quantity) {
            OrderQuantity += quantity;
            if (OrderQuantity < 0 ) {
                OrderQuantity = 0;
            }
        }

        internal void UpdateQuantity(int quantity) {
            OrderQuantity = quantity;
            if (OrderQuantity < 0) {
                OrderQuantity = 0;
            }
        }
    }
}
