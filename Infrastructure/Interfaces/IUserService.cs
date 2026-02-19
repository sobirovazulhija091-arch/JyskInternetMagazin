public interface IUserService
{
    Task<Response<User>> GetByIdAsync(string userId);
    Task<Response<List<User>>> GetAsync();
     Task<Response<string>> UpdateAsync(string userid,UpdateUserDto dto);
     Task<Response<string>> DeleteAsync(string userid);   
}
