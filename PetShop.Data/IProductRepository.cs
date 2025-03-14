﻿using PetShop.DomainEntities;

namespace PetShop.Data {
    public interface IProductRepository {
        Task<Product> AddAsync(Product product);
        Task RemoveAsync(Product product);
        Task<Product?> GetAsync(int productId);
        Task<Product?> GetAsync(string name);
        Task<List<Product>> GetAsync(bool InStockOnly);
        Task UpdateAsync(Product productUpdate);
    }
}