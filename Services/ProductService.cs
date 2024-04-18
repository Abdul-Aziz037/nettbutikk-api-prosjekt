using Microsoft.EntityFrameworkCore;
using nettbutikk_api.Data;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;


namespace nettbutikk_api.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _context;

        public ProductService(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            // Hente produkter fra databasen og konvertere dem til ProductDTO
            var products = await _context.products.ToListAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            });
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            // Hente produkt fra databasen basert på Id og konvertere til ProductDTO
            var product = await _context.products.FindAsync(productId);
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO productDto)
        {
            // Legge til et nytt produkt i databasen og returnere det tilsvarende ProductDTO
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };
            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<ProductDTO> UpdateProductAsync(int productId, ProductDTO productDto)
        {
            // Oppdatere et eksisterende produkt i databasen og returnere det tilsvarende ProductDTO
            var product = await _context.products.FindAsync(productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            await _context.SaveChangesAsync();
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task DeleteProductAsync(int productId)
        {
            // Slette et produkt fra databasen basert på Id
            var product = await _context.products.FindAsync(productId);
            if (product != null)
            {
                _context.products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}



