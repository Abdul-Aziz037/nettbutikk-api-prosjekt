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

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _context.products.FindAsync(productId);
    }

    public async Task AddProductAsync(Product product)
    {
        _context.products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productId)
    {
        var productToDelete = await _context.products.FindAsync(productId);
        if (productToDelete != null)
        {
            _context.products.Remove(productToDelete);
            await _context.SaveChangesAsync();
        }
    }
}