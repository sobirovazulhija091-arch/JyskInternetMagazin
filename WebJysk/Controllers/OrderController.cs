using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService):ControllerBase
{
    private IOrderService service = orderService;
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<Response<string>> AddAsync(OrderDto dto)
    {
        return await service.AddAsync(dto);
    }
    [HttpGet]
    public async Task<Response<List<Order>>> GetAsync()
    {
        return await service.GetAsync();
    }
    [HttpGet("orderid")]
    public async Task<Response<Order>> GetByIdAsync(int orderid)
    {
        return await service.GetByIdAsync(orderid);
    }
    [HttpPut]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<Response<string>> UpdateStatusAsync(int orderid,string Status)
    {
        return await service.UpdateStatusAsync(orderid, Status);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int orderid)
    {
        return await service.DeleteAsync(orderid);
    } 
    // [HttpGet("userid")]
    // public async Task<Response<List<Order>>>  GetUserOrdersAsync(string userid)
    // {
    //     return await service.GetUserOrdersAsync(userid);
    // }
}
