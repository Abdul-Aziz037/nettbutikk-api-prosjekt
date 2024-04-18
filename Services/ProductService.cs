using AutoMapper;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Repositories.Interfaces;

namespace nettbutikk_api.Services;

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
        var products = await _productRepository.GetProductsAsync();
        var dto = products.Select(product => _mapper.Map<ProductDTO>(product)).ToList();

        return dto;
    }

    public async Task<ProductDTO?> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        return product != null ? _mapper.Map<ProductDTO>(product) : null;
    }

    public async Task<ProductDTO?> AddProductAsync(ProductDTO productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _productRepository.AddProductAsync(product);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO?> UpdateProductAsync(int productId, ProductDTO productDto)
    {

        var product = _mapper.Map<Product>(productDto);
        var updatedProduct = await _productRepository.UpdateProductAsync(productId, product);

        if (updatedProduct == null)
        {
            return null;
        }

        return _mapper.Map<ProductDTO>(updatedProduct);
    }

    public async Task<ProductDTO?> DeleteProductAsync(int productId)
    {
        var product = await _productRepository.DeleteProductByIdAsync(productId);
        if (product == null)
        {
            return null;
        }

        return _mapper?.Map<ProductDTO>(product);
    }
}




