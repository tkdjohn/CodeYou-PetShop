using Domain.Product.Validators;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Domain.Product {
    public class ProductLogic : IProductLogic {
        // This readonly means we can't assign a new List to 
        // products. It doesn't mean we can't call
        // products.Add() or _products.Remove()
        private readonly List<IProduct> products = [];

        private ILogger logger;

        //public bool SkipTheDictionaries { get; set; } = true;
        public ProductLogic(ILogger logger) {
            this.logger = logger;
        }

        public void AddProduct<T>(IProduct product) where T: IProduct {
            logger.LogDebug($"Adding a {typeof(T)}");
            if (product is null) {
                return;
            }

            product.ValidateAndThrow<T>();

            // we already have a product by that name. 
            // TODO: might want to find a way check at the UI layer if a product
            // already exists (or if the name is already in use) and inform the user, but
            // since THIS isn't the UI layer, separation of concerns says we shouldn't 
            // do that here - so just return in that case.
            if (products.Contains(product) || products.Any(p => p.Name == product.Name)) {
                return;
            }

            products.Add(product);
        }

        public void RemoveProduct(string productName) {
            // So one of the challenges with the dictionary approach is
            // how do we remove a product from the dictionary when we
            // only have the product's name. Will the product have the 
            // correct type if we get it from the list?
            var product = products.FirstOrDefault(p => p.Name == productName);
            if (product != null) {
                RemoveProduct(product);
            }

        }

        public void RemoveProduct(IProduct product) {
            products.Remove(product);
        }

        // note that while this code is significantly better than
        // individual GetDogLeash and GetCatFood methods, it still
        // requires the caller to know the type beforehand.
        public T? GetProduct<T>(string name) where T: Product{
            if (string.IsNullOrEmpty(name)) return null;

            return products.Where(p => p is T)
                .FirstOrDefault(p => p.Name == name) as T;
        }
        
        public IProduct? GetProduct(string name) {
            return products.FirstOrDefault(p => p.Name == name);
        }

        // no need to call this 'GetAllProducts' the word 'All' doesn't
        //   add any value to the name.
        // also very unsafe to return the List as callers can 
        //   add or remove form it at will. better to return 
        //   the list as an IReadOnluyCollection.
        public IReadOnlyCollection<IProduct> GetProducts() {
            return products;
        }

        public IReadOnlyCollection<string> GetOnlyInStockProducts() {
            return products.InStock().Select(p => p.Name).ToList();
        }

        public decimal GetTotalPriceOfInventory() {
            return products.InStock().Select(p => p.Price * p.Quantity).Sum();
        }
    }
}
