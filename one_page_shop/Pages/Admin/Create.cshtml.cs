using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using one_page_shop.Data;
using one_page_shop.Models;

namespace one_page_shop.Pages.Admin
{
  [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var product = new Product
            {
                Name = Product.Name,
                Price = Product.Price,
            };
            using (var memoryStream = new MemoryStream())
            {
                await Product.ProductImageFormFile.CopyToAsync(memoryStream);
                product.ProductImage = memoryStream.ToArray();
            }
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("/Admin/Index");
        }
    }
}