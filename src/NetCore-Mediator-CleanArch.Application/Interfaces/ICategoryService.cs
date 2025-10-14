using NetCore_Mediator_CleanArch.Application.DTOs;

namespace NetCore_Mediator_CleanArch.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetCategoryByIdAsync(int? id);
    Task CreateCategoryAsync(CategoryDto category);
    Task UpdateCategoryAsync(CategoryDto category);
    Task DeleteCategoryAsync(int? id);
}
