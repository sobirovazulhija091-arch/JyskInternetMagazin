[ApiController]
[Route("api/[controller]")]
public class ReviewController(IReviewService service) : ControllerBase
{
    private readonly IReviewService _service=service;
    [HttpPost]
    public async Task<Response<string>> AddReviewAsync(ReviewDto dto)
    {
       return await _service.AddReviewAsync(dto);
    }
    [HttpGet("product/{productId}")]
    public async Task<Response<List<Review>>> GetProductReviewsAsync(int productId)
    {
        return await _service.GetProductReviewsAsync(productId);
    }
    [HttpGet("average/{productId}")]
    public async Task<Response<double>> GetAverageAsync(int productId)
    {
        return await _service.GetAverageRatingAsync(productId);
    }
}