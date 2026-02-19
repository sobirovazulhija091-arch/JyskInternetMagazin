using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
public class ProductService(ApplicationDbContext dbContext):IProductService
{
    private readonly ApplicationDbContext context= dbContext;

    public async Task<Response<string>> AddAsync(ProductDto dto)
    {
        var product = new Product
        {
          Name = dto.Name,
          Description = dto.Description,
          Price = dto.Price,
        };
       await context.Products.AddAsync(product);
       await context.SaveChangesAsync();
       return  new Response<string>(HttpStatusCode.OK,"Product added successfully");
    }

    public async Task<Response<string>> DeleteAsync(int productid)
    {
        var product = await  context.Products.FindAsync(productid);
        if(product == null){return new Response<string>(HttpStatusCode.NotFound,"Product not found");}
         context.Products.Remove(product);
          await context.SaveChangesAsync();
          return new Response<string>(HttpStatusCode.OK,"Deleted !");
    }

    public async Task<Response<List<Product>>> GetAsync()
    {
        try
        {
             return new Response<List<Product>>(HttpStatusCode.OK,"OK", await context.Products.ToListAsync());
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<List<Product>>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
     
    }

    public async Task<Response<List<Product>>> GetByCategoryAsync(int categoryid)
    {
        var cp = await context.Products.Include(a=>a.Category).ToListAsync();
        return new Response<List<Product>>(HttpStatusCode.OK,"ok",cp);   
    }

    public async Task<Response<Product>> GetByIdAsync(int productid)
    {
        var product = await context.Products.FindAsync(productid);
        return new Response<Product>(HttpStatusCode.OK,"Ok",product);
    }

    public async Task<Response<string>> UpdateAsync(int productid, UpdateProductDto dto)
    {
        var p = await context.Products.FindAsync(productid);
        p.Name = dto.Name;
        p.Description = dto.Description;
        p.Price = dto.Price;
        await context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK,"ok" );
    }
}