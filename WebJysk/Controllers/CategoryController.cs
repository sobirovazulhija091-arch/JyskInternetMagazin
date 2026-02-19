using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/caching")]
public class CategoryController(ICategoryService categoryService):ControllerBase
{
    private readonly ICategoryService service = categoryService;
    [Authorize(Roles = "Manager")]
    [HttpPost]
     public async Task<Response<string>> AddAsync(CategoryDto dto)
    {
        return await service.AddAsync(dto);
    }
    [HttpGet]
     public async Task<Response<List<Category>>> GetAsync()
    {
        return await service.GetAsync();
    }
    [HttpGet("categoryid")]
     public async Task<Response<Category>> GetByIdAsync(int categoryid)
    {
        return await service.GetByIdAsync(categoryid);
    }
    [Authorize(Roles = "Admin,Manager")]
    [HttpPut]
     public async Task<Response<string>> UpdateAsync(int categoryid,UpdateCategoryDto dto)
    {
        return await service.UpdateAsync(categoryid,dto);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
     public async Task<Response<string>> DeleteAsync(int categoryid)
    {
        return await service.DeleteAsync(categoryid);
    } 
}

