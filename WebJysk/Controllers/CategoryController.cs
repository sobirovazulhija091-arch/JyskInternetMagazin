using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/caching")]
public class CategoryController(ICategoryService categoryService):ControllerBase
{
    private readonly ICategoryService service = categoryService;
}