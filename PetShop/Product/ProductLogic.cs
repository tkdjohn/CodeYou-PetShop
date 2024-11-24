using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Product {
    public class ProductLogic : IProductLogic {
        // This readonly means we can't assign a new List to 
        // _products. It doesn't mean we can't call
        // _products.Add() or _products.Remove()
        private readonly List<Product> _products = [];

        // I'm not entirely sure why we are implementing
        // dictionaries for the individual product types.
        // But then again, I don't understand why we have
        // specific products as product types. 
        private readonly Dictionary<string, DogLeash> _DogLeashes = [];
        private readonly Dictionary<string, CatFood> _CatFoods = [];

        public bool SkipTheDictionaries { get; set; } = true;
        public ProductLogic() {
            // but lets not initialize _products here, lets do this
            // where _products is declared above.
            // _products = []; //shorthand for new List<Product>(); 

            // for now add test data here
            // - we'll clean this up when we add a data repository
            _products.Add(new Product { Name = "Test Product 1", Qty = 10 });
            _products.Add(new Product { Name = "Out of Stock", Qty= 0 });
            _products.Add(new Product { Name = "Only 1 left", Qty = 1  });
        }

        public void AddProduct(Product product) {
            if (product is null) {
                return;
            }
            if (_products.Contains(product)) {
                return;
            }

            _products.Add(product);

            if (SkipTheDictionaries) {
                return;
            }

            if (product is DogLeash) {
                _DogLeashes.Add(product.Name, product as DogLeash);
                return;
            }
            if (product is CatFood) {
                _CatFoods.Add(product.Name, product as CatFood);
                return;
            }
        }

        public void RemoveProduct(string productName) {
            // So one of the challenges with the dictionary approach is
            // how do we remove a product from the dictionary when we
            // only have the product's name. Will the product have the 
            // correct type if we get it from the list?
            var product = _products.FirstOrDefault(p => p.Name == productName);
            if (product != null) {
                RemoveProduct(product);
            }

        }

        public void RemoveProduct(Product product) {
            _products.Remove(product);

            // Don't bother checking SkipTheDictionaries here
            //   Dictionary.Remove will remove the product if it is in the
            //   dictionary, and just move on if not.
            // BUT! do check the product type just in case we have products
            //  in both dictionaries with the same name.
            if (product is DogLeash) {
                _DogLeashes.Remove(product.Name);
            }
            if (product is CatFood) {
                _CatFoods.Remove(product.Name);
            }
        }

        public DogLeash? GetDogLeash(string name) {
            try {
                if (!SkipTheDictionaries) {
                    return _DogLeashes[name];
                }

                return _products.Where(p => p is DogLeash)
                    .FirstOrDefault(dl => dl.Name == name) as DogLeash;
            } catch (Exception ex) {
                // this really isn't the way TBH,
                // avoid the exception if possible by checking 
                // if name is in the dictionary (or don't use a
                // dictionary for this) 
                return null;
            }
        }

        public CatFood? GetCatFood(string name) {
            try {
                if (!SkipTheDictionaries) {
                    return _CatFoods[name];
                }

                return _products.Where(p => p is CatFood)
                    .FirstOrDefault(cf => cf.Name == name) as CatFood;
            } catch (Exception ex) {
                return null;
            }
        }

        // no need to call this GetAllProducts the word 'All' doesn't
        //   add any value to the name.
        // also very unsafe to return the List as callers can 
        //   add or remove form it at will. better to return 
        //   the list as an IReadOnluyCollection.
        public IReadOnlyCollection<Product> GetProducts() {
            return _products;
        }

        public List<string> GetOnlyInStockProducts() {
            return _products.Where(p => p.Qty > 0).Select(p => p.Name).ToList();
        }
    }
}
