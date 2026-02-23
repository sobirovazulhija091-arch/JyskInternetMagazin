 using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Runtime.Serialization;

[ApiController]
[Route("api/[controller]")]
public  class PaymentController(IPaymentService payment):ControllerBase
{
    public readonly IPaymentService  service = payment;
    [HttpPost]
    public async Task<Response<string>> CreatePaymentAsync(int orderId, EnumPaymentMethod method)
    {
        return await service.CreatePaymentAsync(orderId,method);
    }
    [HttpPut]
     public async Task<Response<string>> UpdateStatusAsync(int orderId, EnumPaymentStatus status)
    {
         return await service.UpdateStatusAsync(orderId,status);
    }
}
 