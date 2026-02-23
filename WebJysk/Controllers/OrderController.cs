using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService service) : ControllerBase
{
    private readonly IOrderService _service=service;


    [Authorize]
    [HttpPost("create")]
    public async Task<Response<string>> CreateOrderAsync()
    {
        var userId = User.FindFirst("sub")?.Value;
        return await _service.CreateOrderAsync(userId);
    }

    
    [Authorize(Roles = "Admin,Manager")]
    [HttpGet]
    public async Task<PagedResult<Order>> GetAllAsync(
        [FromQuery] FilterOrder filter,
        [FromQuery] PagedQuery query)
    {
        return await _service.GetAllAsync(filter, query);
    }

    [Authorize]
    [HttpGet("my-orders")]
    public async Task<Response<List<Order>>> GetUserOrdersAsync()
    {
        var userId = User.FindFirst("sub")?.Value;
        return await _service.GetUserOrdersAsync(userId);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<Response<Order>> GetByIdAsync(int id)
    {
        return await _service.GetByIdAsync(id);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}/status")]
    public async Task<Response<string>> UpdateStatusAsync(int id, EnumStatus status)
    {
        return await _service.UpdateStatusAsync(id, status);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteOrderAsync(int id)
    {
        return await _service.DeleteOrderAsync(id);
    }
}