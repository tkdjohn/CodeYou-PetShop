using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Product {
    public class ProductLogic {
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
        public ProductLogic()
        {
            // but lets not do this here, lets do this above.
            // _products = []; //shorthand for new List<Product>(); 
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
            if (!SkipTheDictionaries) {
                return GetDogLeashFromDictionary(name);
            }

            return GetDogLeashLINQ(name);
        }

        public CatFood? GetCatFood(string name) {
            if (!SkipTheDictionaries) {
                return GetCatFoodFromDictionary(name);
            }
            return GetCatFoodLINQ(name);
        }

        // GetDogLeashByName - the "ByName" is redundant
        // GetDogLeash(string name) tells us we are getting a DogLeash
        //  by the provided name.
        // Also since this is a single line method using the lambda expression
        // makes for more compact/readable code. Basically => is shorthand for
        // return but allows us to forgo the curly boys. 
        // Finally, one of the problems with this approach is that 
        // we will get a runtime error if name isn't in the dictionary.
        protected DogLeash GetDogLeashFromDictionary(string name) 
            => _DogLeashes[name];

        protected CatFood GetCatFoodFromDictionary(string name) 
            => _CatFoods[name]; 

        // While GetDogLeash and GetCatFood are great examples of how
        // a dictionary works, it isn't necessary to use a dictionary
        // to accomplish this. A simple LINQ statement can do this 
        // without resorting to the dictionary. 
        // Making the return type nullable since name could be null
        // and a null result makes sense in that case.
        protected DogLeash? GetDogLeashLINQ(string name) 
            => _products.Where(p => p is DogLeash)
                .FirstOrDefault(dl => dl.Name == name) as DogLeash;
        protected CatFood? GetCatFoodLINQ(string name)
            => _products.Where(p => p is  CatFood)
                .FirstOrDefault(cf => cf.Name == name) as CatFood;

        // no need to call this GetAllProducts the word 'All' doesn't
        // add any value to the name.
        public List<Product> GetProducts() {
            return _products;
        }


    }
}
