using System.Net;
using Microsoft.EntityFrameworkCore;
using Quartz.Xml.JobSchedulingData20;
// stock - остаток на складе
// quantity → количество
public class CartService(ApplicationDbContext dbContext):ICartService//корзина
{
    private readonly ApplicationDbContext context = dbContext;

    public async Task<Response<string>> AddItemAsync(string userId, int productId, int quantity)
    {
        //sanjidani hast hamin product boz hast mi hamin product dar sklad  mo 
        var product = context.Products.FirstOrDefault(p=>p.Id==productId);
        if(product == null)
        {
            return new Response<string>(HttpStatusCode.NotFound,"Not found product");
        }
        if(product.StockQuantity<quantity){ return new Response<string>(HttpStatusCode.NotFound,"Not enough stock");}
        // 
        var cart =  await context.Carts.Include(c=>c.CartItems).FirstOrDefaultAsync(c=>c.UserId==userId);
        //sohtani cart baroi user
        if (cart==null)
        {
             cart = new Cart {UserId = userId};
            await context.Carts.AddAsync(cart);
        }
        // adding product 
        var existinginsideusercart = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
       if (existinginsideusercart  != null)
        existinginsideusercart .Quantity += quantity;
    else
        cart.CartItems.Add(new CartItem
        {
            ProductId = productId,
            Quantity = quantity
        });

    await context.SaveChangesAsync();

    return new Response<string>(HttpStatusCode.OK,"Item added to cart");
    }

    public async Task<Response<string>> DeltetCartAsync(string userId)
    {
        var del = await context.Carts.Include(a=>a.CartItems).FirstOrDefaultAsync(a=>a.UserId==userId);
          context.CartItems.RemoveRange(del.CartItems); // remove items first 
          context.Carts.Remove(del); // remove cart
         await context.SaveChangesAsync();
         return new Response<string>(HttpStatusCode.OK,"Cart deleted successfully");
    }

    public async Task<Response<Cart>> GetUserCartAsync(string userId)
    {
        var get = await context.Carts.Include(c=>c.CartItems).ThenInclude(p=>p.ProductId)
        .FirstOrDefaultAsync(c=>c.UserId==userId);
       return new Response<Cart>(HttpStatusCode.OK,"Ok,",get);
    
    }

    public async Task<Response<string>> RemoveItemAsync(string userId, int productId)
    {
        var cart = await context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
    if (cart == null)
    {
        return new Response<string>(HttpStatusCode.NotFound, "Cart not found");
    }
    var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
    if (item == null)
        {
          return new Response<string>(HttpStatusCode.NotFound, "Product not in cart");   
        }
    context.CartItems.Remove(item);
    await context.SaveChangesAsync();
    return new Response<string>(HttpStatusCode.OK, "Item removed successfully");
    }

    public async Task<Response<string>> UpdateQuantityAsync(string userId, int productId, int quantity)
    {

        var update = await context.Carts.Include(c=>c.CartItems).FirstAsync(c=>c.UserId==userId);
        if (update == null)
        {
            return new Response<string>(HttpStatusCode.NotFound,"Not Found");
        }
         var itemupdate = update.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
    if (itemupdate == null)
        {
          return new Response<string>(HttpStatusCode.NotFound, "Product not in cart");   
        }
         itemupdate.Quantity=quantity;
         await context.SaveChangesAsync();
          return new Response<string>(HttpStatusCode.OK,"Updated successfully");  
    }
}