using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/caching")]
public class CategoryController(ICategoryService categoryService):ControllerBase
{
    private readonly ICategoryService service = categoryService;
    [ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    private readonly ICategoryService _service=service;
    
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<Response<string>> AddAsync(CategoryDto dto)
    {
         return await _service.AddAsync(dto);
       
    }
    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<Response<string>> UpdateAsync(int id, UpdateCategoryDto dto)
    {
         return await _service.UpdateAsync(id, dto);
        
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await _service.DeleteAsync(id);
    }
    [HttpGet]
    public async Task<Response<List<Category>>> GetAll()
    {
        return await _service.GetAllAsync();
    }
}
}