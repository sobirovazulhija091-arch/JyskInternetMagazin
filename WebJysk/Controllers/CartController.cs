using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class CartController(ICartService cartService) : ControllerBase
{
    private readonly ICartService service = cartService;
    [httpPost]
    public async Task<Response<string>> AddItemAsync(string userId, int productId, int quantity)
    {
        return await service.AddItemAsync(userId, productId, quantity);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateQuantityAsync(string userId, int productId, int quantity)
    {
         return await service.UpdateQuantityAsync(userId, productId, quantity);
    }
    [HttpDelete("remove")]
    public async Task<Response<string>> RemoveItemAsync(string userId, int productId)
    {
        return await service.RemoveItemAsync(userId, productId);
    }
    [HttpDelete]
    public async Task<Response<string>> DeltetCartAsync(string userId)
    {
        return await service.DeltetCartAsync(userId);
    }
    [HttGet]
    public async Task<Response<Cart>> GetUserCartAsync(string userId)
    {
        return await service.GetUserCartAsync(userId);
    }
}