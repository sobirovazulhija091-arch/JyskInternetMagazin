public interface IReviewService//(Отзывы)
{
    Task<Response<string>> AddReviewAsync(ReviewDto dto);
    Task<Response<List<Review>>> GetProductReviewsAsync(int productId);
    Task<Response<double>> GetAverageRatingAsync(int productId);
}