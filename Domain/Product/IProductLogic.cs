namespace Domain.Product {
    public interface IProductLogic {
        bool SkipTheDictionaries { get; set; }
        void AddProduct(Product product);
        CatFood? GetCatFood(string name);
        DogLeash? GetDogLeash(string name);
        IReadOnlyCollection<Product> GetProducts();
        void RemoveProduct(Product product);
        void RemoveProduct(string productName);
        List<string> GetOnlyInStockProducts();
        decimal GetTotalPriceOfInventory();
    }
}