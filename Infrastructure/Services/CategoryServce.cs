using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Net;
public class CategoryServce(ApplicationDbContext dbContext):ICategoryService
{
    private readonly ApplicationDbContext context = dbContext;

    public async Task<Response<string>> AddAsync(CategoryDto dto)
    {
        var category = new Category
        {
           Name=dto.Name
        }; 
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"Add successfully!");
    }

    public async Task<Response<string>> DeleteAsync(int categoryid)
    {
        var del = await context.Categories.FindAsync(categoryid);
        context.Categories.Remove(del);
        return new Response<string>(HttpStatusCode.OK,"Deleted successfuly!");
    }

    public async Task<Response<List<Category>>> GetAsync()
    {
         return new Response<List<Category>>(HttpStatusCode.OK,"ok",await context.Categories.ToListAsync());
    }

    public async Task<Response<Category>> GetByIdAsync(int categoryid)
    {
       var category = await context.Categories.FindAsync(categoryid);
       return new Response<Category>(HttpStatusCode.OK,"Ok",category);
    }

    public async Task<Response<string>> UpdateAsync(int categoryid, UpdateCategoryDto dto)
    {
        var c = await context.Categories.FindAsync(categoryid);
        c.Name=dto.Name;
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");
    }
}