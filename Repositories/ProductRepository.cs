using Microsoft.EntityFrameworkCore;
using nettbutikk_api.Data;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Repositories.Interfaces;

namespace nettbutikk_api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        var nyProduct = await _context.Products.AddAsync(product);
        _context.SaveChanges();
        return nyProduct.Entity;
    }

    public async Task<Product?> UpdateProductAsync(int productId, Product product)
    {
        var productToUpdate = await _context.Products.FindAsync(productId);

        if (productToUpdate != null && product != null)
        {
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;

            await _context.SaveChangesAsync();
        }
        return null;
    }

    public async Task<Product?> DeleteProductByIdAsync(int productId)
    {
        var productToDelete = await GetProductByIdAsync(productId);
        if (productToDelete != null)
        {
            _context.Remove(productToDelete);
            await _context.SaveChangesAsync();

            return productToDelete;
        }
        return null;
    }
}