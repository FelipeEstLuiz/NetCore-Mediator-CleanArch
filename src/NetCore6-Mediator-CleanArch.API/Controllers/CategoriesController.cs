using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore6_Mediator_CleanArch.Application.DTOs;
using NetCore6_Mediator_CleanArch.Application.Interfaces;

namespace NetCore6_Mediator_CleanArch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
    {
        IEnumerable<CategoryDto> categories = await _categoryService.GetCategoriesAsync();

        if (categories == null)
            return NotFound("Categories not found");

        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDto>> Get(int id)
    {
        CategoryDto category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
            return NotFound("Category not found");

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null)
            return BadRequest("Invalid Data");

        await _categoryService.CreateCategoryAsync(categoryDto);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.Id }, categoryDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null || categoryDto.Id != id)
            return BadRequest();

        await _categoryService.UpdateCategoryAsync(categoryDto);

        return Ok(categoryDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        CategoryDto category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
            return NotFound("Category not found");

        await _categoryService.DeleteCategoryAsync(id);

        return Ok(category);
    }
}
