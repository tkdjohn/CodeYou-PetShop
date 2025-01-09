using Domain.Product.Extensions;
using Domain.Product.Validators;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Domain.Product {
    public class ProductLogic : IProductLogic {
        // This readonly means we can't assign a new List to 
        // _products. It doesn't mean we can't call
        // _products.Add() or _products.Remove()
        private readonly List<Product> products = [];

        private ILogger logger;
        private DogLeashValidator dogLeashValidator;

        // I'm not entirely sure why we are implementing
        // dictionaries for the individual product types.
        // But then again, I don't understand why we have
        // specific products as product types. 
        private readonly Dictionary<string, DogLeash> DogLeashes = [];
        private readonly Dictionary<string, CatFood> CatFoods = [];

        public bool SkipTheDictionaries { get; set; } = true;
        public ProductLogic(ILogger logger, DogLeashValidator dogLeashValidator) {
            this.logger = logger;
            this.dogLeashValidator = dogLeashValidator;

            // but lets not initialize _products here, lets do this
            // where _products is declared above.
            // _products = []; //shorthand for new List<Product>(); 

            // for now add test data here
            // - we'll clean this up when we add a data repository
            AddProduct(new DogLeash { Name = "Test Product 1", Qty = 10, Price = 1.99M });
            AddProduct(new CatFood { Name = "Out of Stock", Qty = 0, Price = 15.99M });
            AddProduct(new DogLeash { Name = "Only 1 left", Qty = 1, Price = 99.99M });
        }

        public void AddProduct(Product product) {
            logger.LogDebug("Adding a product");
            if (product is null) {
                return;
            }

            // we already have a product by that name. 
            // TODO: might want to find a way check at the UI layer if a product
            // already exists (or if the name is already in use) and inform the user, but
            // since THIS isn't the UI layer, separation of concerns says we shouldn't 
            // do that here - so just return in that case.
            if (products.Contains(product) || products.Any(p => p.Name == product.Name)) {
                return;
            }

            if (product is DogLeash dogLeash) {
                dogLeashValidator.ValidateAndThrow(dogLeash);
                products.Add(product);

                if (!SkipTheDictionaries && dogLeash != null) {
                    DogLeashes.Add(product.Name, dogLeash);
                }
                return;
            }

            if (product is CatFood catFood) {
                //TODO: need a catFood validator
                products.Add(product);

                if (!SkipTheDictionaries && catFood != null) {
                    CatFoods.Add(product.Name, catFood);
                }
                return;
            }
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

        public void RemoveProduct(Product product) {
            products.Remove(product);

            // Don't bother checking SkipTheDictionaries here
            //   Dictionary.Remove will remove the product if it is in the
            //   dictionary, and just move on if not.
            // BUT! do check the product type just in case we have products
            //  in both dictionaries with the same name.
            if (product is DogLeash) {
                DogLeashes.Remove(product.Name);
            }
            if (product is CatFood) {
                CatFoods.Remove(product.Name);
            }
        }

        // note that while this code is significantly better than
        // individual GetDogLeash and GetCatFood methods, it still
        // requires the caller to know the type beforehand.
        public T? GetProduct<T>(string name) where T: Product{
            if (string.IsNullOrEmpty(name)) return null;
            var type = typeof(T);
            if (!SkipTheDictionaries) {

                if (type == typeof(DogLeash)) {
                    return DogLeashes[name] as T;
                }
                if (type == typeof(CatFood)) {
                    return CatFoods[name] as T;
                    }
                return null;
            }
            return products.Where(p => p is T)
                .FirstOrDefault(p => p.Name == name) as T;
        }
        
        // no need to call this 'GetAllProducts' the word 'All' doesn't
        //   add any value to the name.
        // also very unsafe to return the List as callers can 
        //   add or remove form it at will. better to return 
        //   the list as an IReadOnluyCollection.
        public IReadOnlyCollection<Product> GetProducts() {
            return products;
        }

        public IReadOnlyCollection<string> GetOnlyInStockProducts() {
            return products.InStock().Select(p => p.Name).ToList();
        }

        public decimal GetTotalPriceOfInventory() {
            return products.InStock().Select(p => p.Price * p.Qty).Sum();
        }
    }
}
