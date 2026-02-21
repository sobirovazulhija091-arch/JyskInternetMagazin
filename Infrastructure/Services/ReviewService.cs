using Domain.DTOs;
using Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;
public class ReviewService(ApplicationDbContext dbContext):IReviewService
{
     private readonly ApplicationDbContext context = dbContext;

    public async Task<Response<string>> AddReviewAsync(ReviewDto dto)
    {
       var review =  new Review
       {
        ProductId=dto.ProductId,
        UserId=dto.UserId,
       Rating=dto.Rating,
        Comment=dto.Comment
       };
        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"Add successfully!");
    }

    public async Task<PagedResult<Review>> GetAllAsync(FilterReview filter, PagedQuery query)
    {
       IQueryable<Review> reviews = context.Reviews.AsNoTracking();
        if (filter.ProductId != null)
        {
            reviews = reviews.Where(r=>r.ProductId==filter.ProductId);
        }
         if (filter.UserId != null)
        {
            reviews = reviews.Where(r=>r.UserId==filter.UserId);
        }
        if (filter.Rating.HasValue)
         {
         reviews  = reviews.Where(r => r.Rating == filter.Rating);
         }
          var total = await    reviews.CountAsync();
      if(query.Page > 0 & query.PageSize > 0)
      {
           reviews =  reviews.Skip((query.Page-1)*query.PageSize).Take(query.PageSize);
      }
    var review = await reviews.ToListAsync();
    return new PagedResult<Review>
    {
        Items = review,
        Page = query.Page,
        PageSize = query.PageSize,
        TotalCount = total,
        TotalPages = (int)Math.Ceiling((double)total / query.PageSize)
    };
    }

    public async Task<Response<double>> GetAverageRatingAsync(int productId)
    {
        var average = await context.Reviews.Where(o=>o.ProductId==productId).AverageAsync(r => r.Rating);
        return new Response<double>(HttpStatusCode.OK,"Ok",average);

    }

    public async Task<Response<List<Review>>> GetProductReviewsAsync(int productId)
    {
    var join = await context.Reviews.Where(o=>o.ProductId==productId).Include(r=>r.User)
    .OrderByDescending(r => r.Id).ToListAsync();
         return new Response<List<Review>>(HttpStatusCode.OK,"Ok",join);
    }
}