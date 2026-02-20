// using System.Net;
// using Microsoft.EntityFrameworkCore;
// public class CartService(ApplicationDbContext dbContext):ICartService
// {
//     private readonly ApplicationDbContext context = dbContext;

//     public async Task<Response<string>> AddItemAsync(string userId, CartItemDto dto)
//     {
//        var cart = new Cart
//        {
//            UserId=dto.UserId
//        };
//        await context.Carts.AddAsync(cart);
//        await context.SaveChangesAsync();
//        return new Response<string>(HttpStatusCode.OK,"Add successfuly!");
//     }

//     public async Task<Response<string>> DeleteAsync(int cartid)
//     {
//         var del = await context.Carts.FindAsync(cartid);
//         context.Carts.Remove(del!);
//         await context.SaveChangesAsync();
//         return new Response<string>(HttpStatusCode.OK,"Deleted successfully!");
//     }

//     public async Task<Response<List<Cart>>> GetAsync()
//     {
//        return new Response<List<Cart>>(HttpStatusCode.OK,"ok",await context.Carts.ToListAsync());
//     }

//     public async Task<Response<Cart>> GetByIdAsync(int cartid)
//     {
//           var cart = await context.Carts.FindAsync(cartid);
//           return new Response<Cart>(HttpStatusCode.OK,"ok",cart!);
//     }

//     public Task<Response<List<Cart>>> GetUserCartAsync(string userid)
//     {
//         throw new NotImplementedException();
//     }

// }