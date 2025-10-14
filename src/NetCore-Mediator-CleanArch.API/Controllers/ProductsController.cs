using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore_Mediator_CleanArch.Application.DTOs;
using NetCore_Mediator_CleanArch.Application.Interfaces;

namespace NetCore_Mediator_CleanArch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController(IProductService productAppService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        IEnumerable<ProductDto> products = await productAppService.GetProductsAsync();

        return products is not null
            ? (ActionResult<IEnumerable<ProductDto>>)Ok(products)
            : (ActionResult<IEnumerable<ProductDto>>)NotFound("Products not found");
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        ProductDto product = await productAppService.GetProductByIdAsync(id);

        return product is not null
            ? (ActionResult<ProductDto>)Ok(product)
            : (ActionResult<ProductDto>)NotFound("Product not found");
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDto productDto)
    {
        if (productDto is null)
            return BadRequest("Invalid Data");

        await productAppService.CreateProductyAsync(productDto);

        return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDto productDto)
    {
        if (productDto is null || productDto.Id != id)
            return BadRequest();

        await productAppService.UpdateProductyAsync(productDto);

        return Ok(productDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        ProductDto product = await productAppService.GetProductByIdAsync(id);

        if (product is null)
            return NotFound("Product not found");

        await productAppService.DeleteProductyAsync(id);

        return Ok(product);
    }
}
