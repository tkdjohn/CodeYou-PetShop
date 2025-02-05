using System.ComponentModel.DataAnnotations;

namespace PetShop.DomainEntities {
    public class Order : EntityBase {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        //TODO: make readonly (expose as IReadOnlyList and remove set;
        public ICollection<OrderProduct> OrderProducts { get; set; } = [];
        //TODO: Add Shipping Address once address entity and service exist.

        public override string ToString() => this.Serialize();

        public void AddUpdateOrderProduct(Product product, int quantity) {
            throw new NotImplementedException();
            //TODO: search for existing order item matching product and update qty
            // if not found the create new OrderProduct from product and add to list
            //TODO: remove existing qty from OrderProdcuts and
            // remove the OrderProduct entry if qty = 0
        }

        public void AddUpdateOrderProducts(IEnumerable<OrderProduct> products) { 
            
            throw new NotImplementedException(); 
        }
    }
}
