using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController(IWarehouseService service) : ControllerBase
{
    private readonly IWarehouseService _service=service;

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<Response<string>> CreateAsync([FromBody] WarehouseDto dto)
    {
        return await _service.CreateAsync(dto);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<Response<string>> UpdateAsync(int id, [FromBody] UpdateWarehouseDto dto)
    {
        return await _service.UpdateAsync(id, dto);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await _service.DeleteAsync(id);
    }

    [HttpGet]
    public async Task<Response<List<Warehouse>>> GetAllAsync()
    {
        return await _service.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<Response<Warehouse>> GetByIdAsync(int id)
    {
        return await _service.GetByIdAsync(id);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("add-stock")]
    public async Task<Response<string>> AddStockAsync([FromBody] AddStockDto dto)
    {
        return await _service.AddStockAsync(dto);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("decrease-stock")]
    public async Task<Response<string>> DecreaseStockAsync([FromBody] DecrStockDto dto)
    {
        return await _service.DecreaseStockAsync(dto);
    }
    [HttpGet("check-stock/{productId}")]
    public async Task<Response<int>> CheckStockAsync(int productId)
    {
        return await _service.CheckStockAsync(productId);
    }
}
