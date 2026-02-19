public interface ICartService
{
   Task<Response<string>> AddItemAsync(string userId, CartItemDto dto);
     Task<Response<List<Cart>>> GetAsync();
     Task<Response<Cart>> GetByIdAsync(int cartid);
     Task<Response<string>> DeleteAsync(int cartid);   
     Task<Response<List<Cart>>>  GetUserCartAsync(string userid);
}
// kapzina