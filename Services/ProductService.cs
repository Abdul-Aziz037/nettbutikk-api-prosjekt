using AutoMapper;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Repositories.Interfaces;

namespace nettbutikk_api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ProductDTO>> GetProductsAsync()
        {
            // Henter produkter fra repository
            var products = await _productRepository.GetProductsAsync();

            // Mapper hvert produkt til ProductDTO og legger dem til i en liste
            var dto = products.Select(product => _mapper.Map<ProductDTO>(product)).ToList();

            // Returnerer listen med produkt-DTO-er
            return dto;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddProductAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> UpdateProductAsync(int productId, ProductDTO productDto)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                // Handle not found scenario
                return null;
            }

            _mapper.Map(productDto, existingProduct);
            await _productRepository.UpdateProductAsync(existingProduct);
            return _mapper.Map<ProductDTO>(existingProduct);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                // Handle not found scenario
                return;
            }

            await _productRepository.DeleteProductAsync(productId);
        }

        Task<IEnumerable<ProductDTO>> IProductService.GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}




