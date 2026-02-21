using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService  userService):ControllerBase
{
    private readonly IUserService  service = userService;
     [HttpGet]
    public async Task<PagedResult<User>> GetAllAsync([FromQuery] FilterUser filter,[FromQuery] PagedQuery query)
    {
       return await service.GetAllAsync(filter, query);
    }
    [HttpGet("{id}")]
    public async Task<Response<User>> GetByIdAsync(string id)
    {
        return await service.GetByIdAsync(id);   
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(string id)
    {
         return await service.DeleteAsync(id);
    }
}
