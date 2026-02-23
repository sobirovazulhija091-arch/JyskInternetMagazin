using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml;
public class CategoryServce(ApplicationDbContext dbContext):ICategoryService
{
    private readonly ApplicationDbContext context = dbContext;

    public async Task<Response<string>> AddAsync(CategoryDto dto)
    {
       if (dto.ParentId != null)
    {
        var parentExists = await context.Categories
            .AnyAsync(c => c.Id == dto.ParentId);

        if (!parentExists)
            return new Response<string>(
                HttpStatusCode.BadRequest,
                "Parent category not found");
    }

    var category = new Category
    {
        Name = dto.Name,
        ParentId = dto.ParentId
    };

    await context.Categories.AddAsync(category);
    await context.SaveChangesAsync();

    return new Response<string>(HttpStatusCode.OK, "Category created");
    }

    public async Task<Response<string>> DeleteAsync(int categoryid)
    {
        var del = await context.Categories.FindAsync(categoryid);
        context.Categories.Remove(del);
        return new Response<string>(HttpStatusCode.OK,"Deleted successfuly!");
    }

    public async Task<Response<List<Category>>> GetAllAsync()
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
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryid);

    if (category == null)
    {
        return new Response<string>(HttpStatusCode.NotFound,"Category not found");
    }
    category.Name = dto.Name;
    if (dto.ParentId != null)
    {
        var parentExists = await context.Categories
            .AnyAsync(c => c.Id == dto.ParentId);

        if (!parentExists)
        {
     return new Response<string>(HttpStatusCode.BadRequest,"Parent category not found");
        }
    }
    category.ParentId = dto.ParentId;
 await context.SaveChangesAsync();
    return new Response<string>(HttpStatusCode.OK, "Updated successfully");
    }
    public async     Task<Response<List<Category>>> GetSubCategoriesAsync(int parentId)
    {
       var sub = await context.Categories.Where(p=>p.ParentId==parentId).ToListAsync();
       return new Response<List<Category>>(HttpStatusCode.OK,"ok",sub);
    }

}