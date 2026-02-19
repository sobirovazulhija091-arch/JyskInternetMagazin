using System.Net;
using Microsoft.EntityFrameworkCore;
public class UserService(ApplicationDbContext dbContext):IUserService
{
    private readonly ApplicationDbContext context = dbContext;

    public async Task<Response<string>> DeleteAsync(string userid)
    {
       var del = await context.Users.FindAsync(userid);
       context.Users.Remove(del);
       await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK , "ok");
    }

    public async Task<Response<List<User>>> GetAsync()
    {
        return new Response<List<User>>(HttpStatusCode.OK,"ok",await context.Users.ToListAsync());
    }

    public async Task<Response<User>> GetByIdAsync(string userid)
    {
        var user = await context.Users.FindAsync(userid);
         return new Response<User>(HttpStatusCode.OK,"ok", user);

    }

    public async Task<Response<string>> UpdateAsync(string userid, UpdateUserDto dto)
    {
        var user = await context.Users.FindAsync(userid);
            user.FullName = dto.FullName;
             await context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK,"ok" );
    }
}