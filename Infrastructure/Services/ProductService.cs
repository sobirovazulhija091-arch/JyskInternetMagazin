using System.Net;
using Domain.DTOs;
using Infrastructure.Responses;
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

   public async Task<Response<List<Product>>> FilterAsync(decimal? minPrice, decimal? maxPrice, int? brandId)
    {
            IQueryable<Product> products = context.Products.AsQueryable();
//  if (minPrice.HasValue)= if (minPrice!=null)
// value is number
    if (minPrice.HasValue)
    {
        products = products.Where(p => p.Price >= minPrice.Value);
    }
    if (maxPrice.HasValue)
    {
        products = products.Where(p => p.Price <= maxPrice.Value);
    }

    if (brandId.HasValue)
    {
        products = products.Where(p => p.BrandId == brandId.Value);
    }
    var result = await products.ToListAsync();
    return new Response<List<Product>>(HttpStatusCode.OK, "Filtered products", result);

    }

   public async Task<PagedResult<Product>> GetAllAsync(FilterProduct filter, PagedQuery query)
    {
       
            IQueryable<Product> products = context.Products.AsQueryable();
//  if (minPrice.HasValue)= if (minPrice!=null)
// value is number
    if (filter.MinPrice.HasValue)
    {
        products = products.Where(p => p.Price >= filter.MinPrice.Value);
    }
    if (filter.MaxPrice.HasValue)
    {
        products = products.Where(p => p.Price <= filter.MaxPrice.Value);
    }

    if (filter.BrandId.HasValue)
    {
        products = products.Where(p => p.BrandId == filter.BrandId.Value);
    }
    if (filter.CategoryId.HasValue)
    {
        products = products.Where(p => p.CategoryId == filter.CategoryId.Value);
    }
    if (filter.Name!=null)
    {
        products = products.Where(p => p.Name == filter.Name);
    }

    if (filter.IsActive!=null)
    {
        products = products.Where(p => p.IsActive == filter.IsActive);
    }
    var total = await products.CountAsync();
     if(query.Page>0 & query.PageSize >0)
      {
         products =  products.Skip((query.Page-1)*query.PageSize).Take(query.PageSize);
      }
    var product = await products.ToListAsync();

    return new PagedResult<Product>
    {
        Items = product,
        Page = query.Page,
        PageSize = query.PageSize,
        TotalCount = total,
        TotalPages = (int)Math.Ceiling((double)total / query.PageSize)
    };
    }
   public async Task<Response<List<Product>>> GetByBrandAsync(int brandId)
    {
        var brand = await context.Products.Include(b=>b.Brand).ToListAsync();
        return new Response<List<Product>>(HttpStatusCode.OK,"ok",brand);   
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

   public async Task<Response<List<Product>>> GetTopSellingAsync()// Join OrderItems → GroupBy → OrderBy desc
    {
       var products = await context.Products.OrderByDescending(p => p.OrderItems.Sum(o => o.Quantity))
        .ToListAsync();
        return new Response<List<Product>>(HttpStatusCode.OK,"Ok",products);
    }

   public async Task<Response<Product>> GetWithReviewsAsync(int id)
    {
       var product = await context.Products.Include(p=>p.Reviews).ThenInclude(r=>r.User).FirstOrDefaultAsync(p=>p.Id==id);
         if (product == null)
    {
        return new Response<Product>(HttpStatusCode.NotFound, "Product not found");
    }
        return new Response<Product>(HttpStatusCode.OK,"ok",product);   
    }
   public async Task<Response<List<Product>>> SearchAsync(string keyword)
    {
        var product = await context.Products.Where(p=>p.Name.Contains(keyword) || p.Description.Contains(keyword)).ToListAsync();
        return new Response<List<Product>>(HttpStatusCode.OK,"ok",product); 
    }

    public async Task<Response<List<Product>>> SortByPriceAsync(bool ascending)
    {
        var product = ascending ? await context.Products.OrderBy(p=>p.Price).ToListAsync()
        : await context.Products.OrderByDescending(p=>p.Price).ToListAsync();
        return new Response<List<Product>>(HttpStatusCode.OK,"ok",product); 

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