using Microsoft.EntityFrameworkCore;
using NetCore6_Mediator_CleanArch.Domain.Entities;
using NetCore6_Mediator_CleanArch.Domain.Interfaces;
using NetCore6_Mediator_CleanArch.Infra.Data.Context;

namespace NetCore6_Mediator_CleanArch.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetProductById(int? id) => await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Product>> GetProductsAsync() => await _context.Products.ToListAsync();

    public async Task<Product> RemoveAsync(Product product)
    {
        _context.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
