using nettbutikk_api.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nettbutikk_api.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductDTO>> GetProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int productId);
        Task<ProductDTO?> AddProductAsync(ProductDTO productDto);
        Task<ProductDTO?> UpdateProductAsync(int productId, ProductDTO productDto);
        Task<ProductDTO?> DeleteProductAsync(int productId);
    }
}
