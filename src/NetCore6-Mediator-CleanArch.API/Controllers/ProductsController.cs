using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore6_Mediator_CleanArch.Application.DTOs;
using NetCore6_Mediator_CleanArch.Application.Interfaces;

namespace NetCore6_Mediator_CleanArch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productAppService)
    {
        _productService = productAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        IEnumerable<ProductDto> products = await _productService.GetProductsAsync();

        if (products == null)
            return NotFound("Products not found");

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        ProductDto product = await _productService.GetProductByIdAsync(id);

        if (product == null)
            return NotFound("Product not found");

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDto productDto)
    {
        if (productDto == null)
            return BadRequest("Invalid Data");

        await _productService.CreateProductyAsync(productDto);

        return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDto productDto)
    {
        if (productDto == null || productDto.Id != id)
            return BadRequest();

        await _productService.UpdateProductyAsync(productDto);

        return Ok(productDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        ProductDto product = await _productService.GetProductByIdAsync(id);

        if (product == null)
            return NotFound("Product not found");

        await _productService.DeleteProductyAsync(id);

        return Ok(product);
    }
}
