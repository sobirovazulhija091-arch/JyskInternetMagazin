public interface IUserService
{
    Task<Response<List<User>>> GetAllAsync();
    Task<Response<User>> GetByIdAsync(string id);
    Task<Response<string>> UpdateAsync(string id, UpdateUserDto dto);
    Task<Response<string>> DeleteAsync(string id);
}