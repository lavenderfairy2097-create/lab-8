using Lab_8_SMT.Data;
using Lab_8_SMT.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab_8_SMT.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ProductRepository _repository;

        public DetailsModel(ProductRepository repository)
        {
            _repository = repository;
        }

        public Product Product { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            Product = product;

            return Page();
        }
    }
}
