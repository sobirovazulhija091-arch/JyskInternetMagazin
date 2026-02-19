using System.Net;
using System.Data;
using Microsoft.EntityFrameworkCore;

public class OrderService(ApplicationDbContext dbContext):IOrderService
{
     private readonly ApplicationDbContext context = dbContext;

    public async Task<Response<string>> AddAsync(OrderDto dto)
    {
       var order = new Order
       {
         UserId = dto.UserId,
         status = dto.status,
         TotalAmount = dto.TotalAmount  
       };
       await context.Orders.AddAsync(order);
       await context.SaveChangesAsync();
       return new Response<string>(HttpStatusCode.OK,"Add successfully!");
    }

    public async Task<Response<string>> DeleteAsync(int orderid)
    {
       var del = await context.Orders.FindAsync(orderid);
       context.Orders.Remove(del);
       await context.SaveChangesAsync();
       return new Response<string>(HttpStatusCode.OK,"Deleted successfully!");
    }

    public async Task<Response<List<Order>>> GetAsync()
    {
        return new Response<List<Order>>(HttpStatusCode.OK,"ok", await context.Orders.ToListAsync());
    }

    public async Task<Response<Order>> GetByIdAsync(int orderid)
    {
       var  order = await context.Orders.FindAsync(orderid);
       return new Response<Order>(HttpStatusCode.OK,"Ok",order);
    }

   //  public async Task<Response<List<Order>>> GetUserOrdersAsync(string Userid)
   //  {
   //      var o = await context.Orders.Include(a=>a.Userid);
   //  }

    public async Task<Response<string>> UpdateStatusAsync(int orderid, string Status)
    {
       var order = await context.Orders.FirstOrDefaultAsync(a=>a.Id==orderid);
       order.status=Status;
       await context.SaveChangesAsync();
       return new Response<string>(HttpStatusCode.OK,"Updated successfully");
    }
}