using Lab_8_SMT.Data;
using Lab_8_SMT.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab_8_SMT.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductRepository _repository;

        public IndexModel(ProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> Products { get; set; } = new();

        public async Task OnGetAsync()
        {
            Products = await _repository.GetAllAsync();
        }
    }
}