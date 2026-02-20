public interface IOrderService
{
    Task<Response<string>> CreateOrderAsync(string userId);
    Task<Response<List<Order>>> GetAllAsync();
    Task<Response<List<Order>>> GetUserOrdersAsync(string userId);
    Task<Response<Order>> GetByIdAsync(int id);
    Task<Response<string>> UpdateStatusAsync(int id, EnumStatus status);
    Task<Response<string>> CancelOrderAsync(int id);
}