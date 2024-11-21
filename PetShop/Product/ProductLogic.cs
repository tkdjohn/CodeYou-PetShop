using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Product {
    public class ProductLogic {
        private List<Product> _products;
        public ProductLogic()
        {
            _products = []; //shorthand for new List<Product>(); 
        }

        public void AddProduct(Product product) { 
            _products.Add(product);
        }

        public void RemoveProduct(Product product) {
            _products.Remove(product);
        }

        // no need to call this GetAllProducts the word 'All' doesn't
        // add any value to the name.
        public List<Product> GetProducts() {
            return _products;
        }


    }
}
