using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class CartController(ICartService cartService) : ControllerBase
{
    private readonly ICartService service = cartService;
    [Authorize(Roles = "Manager")]
    [HttpPost]
     public async Task<Response<string>> AddItemAsync(string userId, CartItemDto dto)
    {
        return await service.AddItemAsync(userId, dto);
    }

    [HttpGet]
    public async Task<Response<List<Cart>>> GetAsync()
    {
        return await service.GetAsync();
    }
    [HttpGet("cartid")]
    public async Task<Response<Cart>> GetByIdAsync(int cartid)
    {
         return await service.GetByIdAsync(cartid);
    }
    // [HttpPut]
    // public async Task<Response<string>> UpdateAsync(string userid, int productid, int quantity)
    // {
    //     return await service.UpdateAsync(userid,productid,quantity);
    [Authorize(Roles = "Admin")]
    // }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int cartid)
    {
        return await service.DeleteAsync(cartid);
    }   
    [HttpGet("user/{userid}")]
    public async Task<Response<List<Cart>>>  GetUserCartAsync(string userid)
    {
        return await service.GetUserCartAsync(userid);
    }
}
