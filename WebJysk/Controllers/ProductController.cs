using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService):ControllerBase
{
     private readonly IProductService service = productService;
     [HttpPost]
     public async Task<Response<string>> AddAsync(ProductDto dto)
    {
        return await service.AddAsync(dto);
    }
    [HttpGet]
    public async Task<Response<List<Product>>> GetAsync()
    {
        return await service.GetAsync();
    }
    [HttpGet("productid")]
    public async Task<Response<Product>> GetByIdAsync(int productid)
    {
        return await service.GetByIdAsync(productid);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(int productid,UpdateProductDto dto)
    {
        return await service.UpdateAsync(productid,dto);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int productid)
    {
        return await service.DeleteAsync(productid);
    }
    
    [HttpGet("categoryid")]
    public async Task<Response<List<Product>>> GetByCategoryAsync(int categoryid)
    {
        return await service.GetByCategoryAsync(categoryid);
    }
}
