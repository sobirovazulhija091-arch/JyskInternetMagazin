public interface IOrderService
{
    Task<Response<string>> AddAsync(OrderDto dto);
     Task<Response<List<Order>>> GetAsync();
     Task<Response<Order>> GetByIdAsync(int orderid);
     Task<Response<string>> UpdateStatusAsync(int orderid,string Status);
     Task<Response<string>> DeleteAsync(int orderid);   
    //  Task<Response<List<Order>>>  GetUserOrdersAsync(string userid);
}