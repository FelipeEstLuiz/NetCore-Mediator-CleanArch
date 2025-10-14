using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore_Mediator_CleanArch.Application.DTOs;
using NetCore_Mediator_CleanArch.Application.Interfaces;

namespace NetCore_Mediator_CleanArch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
    {
        IEnumerable<CategoryDto> categories = await categoryService.GetCategoriesAsync();

        return categories is not null
            ? (ActionResult<IEnumerable<CategoryDto>>)Ok(categories)
            : (ActionResult<IEnumerable<CategoryDto>>)NotFound("Categories not found");
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDto>> Get(int id)
    {
        CategoryDto category = await categoryService.GetCategoryByIdAsync(id);

        return category is not null ?
            (ActionResult<CategoryDto>)Ok(category) :
            (ActionResult<CategoryDto>)NotFound("Category not found");
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto is null)
            return BadRequest("Invalid Data");

        await categoryService.CreateCategoryAsync(categoryDto);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.Id }, categoryDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
    {
        if (categoryDto is null || categoryDto.Id != id)
            return BadRequest();

        await categoryService.UpdateCategoryAsync(categoryDto);

        return Ok(categoryDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        CategoryDto category = await categoryService.GetCategoryByIdAsync(id);

        if (category is null)
            return NotFound("Category not found");

        await categoryService.DeleteCategoryAsync(id);

        return Ok(category);
    }
}
