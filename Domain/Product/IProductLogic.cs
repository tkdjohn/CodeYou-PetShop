namespace Domain.Product {
    public interface IProductLogic {
        void AddProduct<T>(IProduct product) where T :IProduct;
        T? GetProduct<T>(string name) where T: Product;
        IProduct? GetProduct(string name);
        IReadOnlyCollection<IProduct> GetProducts();
        void RemoveProduct(IProduct product);
        void RemoveProduct(string productName);
        IReadOnlyCollection<string> GetOnlyInStockProducts();
        decimal GetTotalPriceOfInventory();
    }
}