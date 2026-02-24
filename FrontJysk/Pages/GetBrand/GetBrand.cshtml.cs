using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontJysk.Pages.GetBrand;

public class GetBrandModel : PageModel
{
    public List<Brand> brandall { get; set; } = new();  // ✅ MUST be List<Brand>

    private readonly BrandService _brandService;

    public GetBrandModel(BrandService brandService)
    {
        _brandService = brandService;
    }

    public async Task OnGetAsync()
    {
        var result = await _brandService.GetAllAsync();
        brandall = result.Data ?? new List<Brand>();
    }
}