public interface IProductService
{
Task<Response<string>> AddAsync(ProductDto dto);// Add new product
Task<Response<string>> UpdateAsync(int id, UpdateProductDto dto);// Update product info
Task<Response<string>> DeleteAsync(int id);// Delete product (check Restrict)
Task<Response<List<Product>>> GetAllAsync();// Get all products
Task<Response<Product>> GetByIdAsync(int id); // Get single product
Task<Response<List<Product>>> GetByCategoryAsync(int categoryId); // Filter by category (WHERE CategoryId)
Task<Response<List<Product>>> GetByBrandAsync(int brandId);// Filter by brand
Task<Response<List<Product>>>  FilterAsync(decimal? minPrice, decimal? maxPrice, int? brandId); // Filter + WHERE conditions
Task<Response<List<Product>>>  SearchAsync(string keyword); // Search by name (Contains)
Task<Response<List<Product>>> SortByPriceAsync(bool ascending);// OrderBy price
Task<Response<List<Product>>> GetTopSellingAsync();// Join OrderItems → GroupBy → OrderBy desc
Task<Response<Product>> GetWithReviewsAsync(int id); // Include Reviews (JOIN)

}