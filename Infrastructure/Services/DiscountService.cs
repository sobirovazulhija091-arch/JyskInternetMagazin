using System.Net;
using Microsoft.EntityFrameworkCore;
public class DiscountService(ApplicationDbContext dbContext):IDiscountService
{
    private readonly ApplicationDbContext context = dbContext;

 public async Task<Response<string>> CreateAsync(DiscountDto dto)
    {
       var di = new Discount
       {
       Code=dto.Code,
       Percentage=dto.Percentage,
       IsActive = dto.IsActive
       };
       await context.Discounts.AddAsync(di);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"Add successfully!");
    }
    public async Task<Response<decimal>> ApplyDiscountAsync(int orderId, string code)
    {
       var order = context.Orders.FirstOrDefault(o=>o.Id==orderId);
       if (order == null)
        {
            return new Response<decimal>(HttpStatusCode.OK,"Not found");
        }
        var discount = await context.Discounts.FirstOrDefaultAsync(d => d.Code == code);

       if (discount == null)
        {
          return new Response<decimal>(HttpStatusCode.NotFound, "Invalid(Not found) discount code");   
        }
        var discountAmount = order.TotalAmount * discount.Percentage / 100;
    var exectamountwithdescount = order.TotalAmount -= discountAmount;
    await context.SaveChangesAsync();
    return new Response<decimal>(HttpStatusCode.OK, "Discount applied", exectamountwithdescount);

    }
    public async Task<Response<bool>> ValidateAsync(string code)
    {
       var discount = await context.Discounts.FirstOrDefaultAsync(d => d.Code == code);

       if (discount == null)
        {
          return new Response<bool>(HttpStatusCode.NotFound, "Not found discount code",false);   
        }
         return new Response<bool>(HttpStatusCode.OK, "Valid discount", true);
         
    }
}