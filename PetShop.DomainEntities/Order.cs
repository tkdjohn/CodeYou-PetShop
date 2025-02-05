using System.ComponentModel.DataAnnotations;

namespace PetShop.DomainEntities {
    public class Order : EntityBase {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        //TODO: make readonly (expose as IReadOnlyList with backing private list)
        public List<OrderProduct> OrderProducts { get; set; } = [];
        //TODO: Add Shipping Address once address entity and service exist.

        public override string ToString() => this.Serialize();

        public void AddItem(Product product, int quantity) {
            throw new NotImplementedException();
            //TODO: search for existing order item matching product and update qty
            // if not found the create new orderproduct from product and add to list
        }

        public void RemoveItem(Product product, int quantity) {
            throw new NotImplementedException();
            //TODO: remove existing qty from OrderProdcuts and
            // remove the OrderProduct entry if qty = 0
        }
    }
}
