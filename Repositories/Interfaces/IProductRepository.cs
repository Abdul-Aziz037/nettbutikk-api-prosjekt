using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<ICollection<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(int productId, Product product);
    Task<Product?> DeleteProductByIdAsync(int productId);
}
