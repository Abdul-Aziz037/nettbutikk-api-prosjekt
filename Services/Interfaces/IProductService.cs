using nettbutikk_api.Models.DTOs;


namespace nettbutikk_api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int productId);
        Task<ProductDTO> AddProductAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateProductAsync(int productId, ProductDTO productDto);
        Task DeleteProductAsync(int productId);
    }
}
