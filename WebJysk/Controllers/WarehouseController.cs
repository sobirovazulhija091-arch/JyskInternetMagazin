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
    [HttpPost("add-stock")]
    public async Task<Response<string>> AddStockAsync(int warehouseId, int productId, int quantity)
    {
        return await _service.AddStockAsync(warehouseId, productId, quantity);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("decrease-stock")]
    public async Task<Response<string>> DecreaseStockAsync(int productId, int quantity)
    {
        return await _service.DecreaseStockAsync(productId, quantity);
    }

    [HttpGet("check-stock/{productId}")]
    public async Task<Response<int>> CheckStockAsync(int productId)
    {
        return await _service.CheckStockAsync(productId);
    }
}