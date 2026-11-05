using Lab_8_SMT.Data;
using Lab_8_SMT.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;

namespace Lab_8_SMT.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ProductRepository _repository;
        private readonly ProductImageService _images;

        public DeleteModel(
    ProductRepository repository,
    ProductImageService image)
      
            {
                _repository = repository;
                _images = image;
            }
        

        [BindProperty]
        public Product Product { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public void OnGet()
        {
            Product.status = "A";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError(
                    nameof(ImageFile), "Please select a product image.");
            }
            else
            {
                string? error = _images.Validate(ImageFile);

                if (error != null)
                    ModelState.AddModelError(nameof(ImageFile), error);
            }

            if (!ModelState.IsValid)
                return Page();

            string imagePath = await _images.SaveAsync(ImageFile!);

            try
            {
                Product.image = imagePath;
                await _repository.CreateAsync(Product);

                TempData["Success"] = "Product created successfully.";

                return RedirectToPage("Index");
            }
            catch
            {
                _images.Delete(imagePath);
                throw;
            }
        }
    }
}