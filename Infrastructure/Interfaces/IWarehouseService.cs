public interface IWarehouseService
{
    Task<Response<string>> AddStockAsync(int warehouseId, int productId, int quantity);
    Task<Response<string>> DecreaseStockAsync(int productId, int quantity);
    Task<Response<int>> CheckStockAsync(int productId);//
}