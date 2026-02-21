using Domain.DTOs;
using Infrastructure.Responses;

public interface IOrderService
{
    Task<Response<string>> CreateOrderAsync(string userId);
   Task<PagedResult<Order>> GetAllAsync(FilterOrder filter,PagedQuery query);
    Task<Response<List<Order>>> GetUserOrdersAsync(string userId);
    Task<Response<Order>> GetByIdAsync(int id);
    Task<Response<string>> UpdateStatusAsync(int id, EnumStatus status);
    Task<Response<string>> CancelOrderAsync(int id);
}