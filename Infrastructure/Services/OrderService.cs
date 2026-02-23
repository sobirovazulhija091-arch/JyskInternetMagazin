using System.Net;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Responses;
using Domain.DTOs;

public class OrderService(ApplicationDbContext dbContext):IOrderService
{
     private readonly ApplicationDbContext context = dbContext;

    public async Task<Response<string>> DeleteOrderAsync(int id)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    if (order == null)
        {
          return new Response<string>(HttpStatusCode.NotFound, "Order not found");   
        }
    context.Orders.Remove(order);
    await context.SaveChangesAsync();
    return new Response<string>(HttpStatusCode.OK, "Order deleted successfully");
    }

    public async Task<Response<string>> CreateOrderAsync(OrderDto dto)
    {
        var order = new Order
    {
        PaymentMethod = dto.PaymentMethod,
        Status =EnumStatus.Paid,
        TotalAmount = dto.TotalAmount,
        DeliveryAddress = dto.DeliveryAddress,
        OrderDate = DateTime.UtcNow
    };

    await context.Orders.AddAsync(order);
    await context.SaveChangesAsync();

    return new Response<string>(HttpStatusCode.OK, "Order created successfully");
    }

    public async Task<PagedResult<Order>> GetAllAsync(FilterOrder filter, PagedQuery query)
    {
         var orders = context.Orders.AsQueryable();

        if (filter.Status!=null)
        {
            orders = orders.Where(o => o.Status == filter.Status);
         }
        if (filter.FromDate.HasValue)
        {
            orders = orders.Where(o => o.OrderDate >= filter.FromDate.Value);
        }

        if (filter.ToDate.HasValue)
        {
            orders = orders.Where(o => o.OrderDate <= filter.ToDate.Value);
        } 
          var total = await orders.CountAsync();
      if(query.Page>0 & query.PageSize >0)
      {
         orders = orders.Skip((query.Page-1)*query.PageSize).Take(query.PageSize);
      }
    var users = await orders.ToListAsync();

    return new PagedResult<Order>
    {
        Items = users,
        Page = query.Page,
        PageSize = query.PageSize,
        TotalCount = total,
        TotalPages = (int)Math.Ceiling((double)total / query.PageSize)
    };
    }

    public async Task<Response<Order>> GetByIdAsync(int id)
    {
        var order = await context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

    if (order == null)
        {
          return new Response<Order>(HttpStatusCode.NotFound, "Order not found");   
        }
    return new Response<Order>(HttpStatusCode.OK, "Success", order);
    }

    public  async Task<Response<List<Order>>> GetUserOrdersAsync(string userId)
    {
        var orders = await context.Orders.Include(o=>o.User).Where(o => o.UserId == userId).ToListAsync();
        return new Response<List<Order>>(HttpStatusCode.OK, "Success", orders);
    }

    public async Task<Response<string>> UpdateStatusAsync(int id, EnumStatus status)
    {
       var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    if (order == null)
        {
         return new Response<string>(HttpStatusCode.NotFound, "Order not found");   
        }
    order.Status = status;
    await context.SaveChangesAsync();
    return new Response<string>(HttpStatusCode.OK, "Order status updated");
    }
}