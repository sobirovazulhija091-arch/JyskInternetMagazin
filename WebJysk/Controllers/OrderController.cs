using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService):ControllerBase
{
    private IOrderService service = orderService;
}
