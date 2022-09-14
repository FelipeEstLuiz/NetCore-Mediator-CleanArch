using Microsoft.EntityFrameworkCore;
using NetCore6_Mediator_CleanArch.Domain.Entities;
using NetCore6_Mediator_CleanArch.Domain.Interfaces;
using NetCore6_Mediator_CleanArch.Infra.Data.Context;

namespace NetCore6_Mediator_CleanArch.Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CategoryProductAsync(int? id) => await _context.Products.FirstOrDefaultAsync(x => x.CategoryId == id) is not null;

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _context.Categories.ToListAsync();

    public async Task<Category?> GetCategoryByIdAsync(int? id) => await _context.Categories.FindAsync(id);

    public async Task<Category> RemoveAsync(Category category)
    {
        _context.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _context.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }
}
