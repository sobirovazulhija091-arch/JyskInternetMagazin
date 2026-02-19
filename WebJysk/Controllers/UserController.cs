using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService  userService):ControllerBase
{
    private readonly IUserService  service = userService;
    [HttpGet("userid")]
   public async Task<Response<User>> GetByIdAsync(string userId)
    {
        return await service.GetByIdAsync(userId);
    }
    [HttpGet]
   public async Task<Response<List<User>>> GetAsync()
    {
        return await service.GetAsync();
    }
   [HttpPut]
    public async Task<Response<string>> UpdateAsync(string userid,UpdateUserDto dto)
    {
        return await service.UpdateAsync(userid,dto);
    }
    [Authorize(Roles = "Admin,Manager")]
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(string userid)
    {
        return await service.DeleteAsync(userid);
    }
}
