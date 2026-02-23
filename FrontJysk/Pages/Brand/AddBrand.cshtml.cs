using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontJysk.Pages.Brand
{
    public class AddBrandModel : PageModel
    {
        private readonly BrandService _brandService;

        public AddBrandModel(BrandService brandService)
        {
            _brandService = brandService;
        }

        [BindProperty]
        public BrandDto BrandDto { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _brandService.AddAsync(BrandDto);

           return RedirectToPage("/Brand/AddBrand");
        }
    }
}