namespace Domain.Product {
    public interface IProduct {
        string Description { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        int Qty { get; set; }

        string ToString();
    }
}