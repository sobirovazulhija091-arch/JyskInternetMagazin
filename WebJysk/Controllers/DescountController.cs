using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;

[ApiController]
[Route("api/[controller]")]
public class DescountController(IDescountService descount):ControllerBase
{
    public readonly IDescountService service=descount;
    [HttpPost]
   public async Task<Response<string>> CreateAsync(DiscountDto dto)
    {
        return await service.CreateAsync(dto);
    }
    [HttpPost]
    public async Task<Response<bool>> ValidateAsync(string code)
    {
        return await service.ValidateAsync(code);
    }
    [HttpPost]
    public async Task<Response<decimal>> ApplyDiscountAsync(int orderId, string code)
    {
        return await service.ApplyDiscountAsync(orderId,code);
    }
}
