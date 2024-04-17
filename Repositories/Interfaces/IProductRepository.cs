using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int productId);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int productId);
}
