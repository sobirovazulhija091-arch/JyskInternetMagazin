[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly IBrandService _service;

    public BrandController(IBrandService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<Response<string>> AddAsync(BrandDto dto)
    {
        var result = await _service.AddAsync(dto);
        return result;
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<Response<string>> UpdateAsync(int id, BrandDto dto)
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
    public async Task<Response<List<Brand>>> GetAllAsync()
    {
         return await _service.GetAllAsync();
    }
    [HttpGet("{id}")]
    public async Task<Response<Brand>> GetByIdAsync(int id)
    {
        return await _service.GetByIdAsync(id);

    }
}