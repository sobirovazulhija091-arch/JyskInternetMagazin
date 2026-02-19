public interface IProductService
{
     Task<Response<string>> AddAsync(ProductDto dto);
     Task<Response<List<Product>>> GetAsync();
     Task<Response<Product>> GetByIdAsync(int productid);
     Task<Response<string>> UpdateAsync(int productid,UpdateProductDto dto);
     Task<Response<string>> DeleteAsync(int productid);
     Task<Response<List<Product>>> GetByCategoryAsync(int categoryid);
}