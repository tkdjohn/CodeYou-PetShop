namespace Domain.Product {
    public interface IProductLogic {
        bool SkipTheDictionaries { get; set; }
        void AddProduct(Product product);
        T? GetProduct<T>(string name) where T: Product;
        IReadOnlyCollection<Product> GetProducts();
        void RemoveProduct(Product product);
        void RemoveProduct(string productName);
        IReadOnlyCollection<string> GetOnlyInStockProducts();
        decimal GetTotalPriceOfInventory();
    }
}