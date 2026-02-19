public interface ICategoryService
{
     Task<Response<string>> AddAsync(CategoryDto dto);
     Task<Response<List<Category>>> GetAsync();
     Task<Response<Category>> GetByIdAsync(int categoryid);
     Task<Response<string>> UpdateAsync(int categoryid,UpdateCategoryDto dto);
     Task<Response<string>> DeleteAsync(int categoryid);   
}