using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using one_page_shop.Data;
using one_page_shop.Models;

namespace one_page_shop.Pages.Admin
{
   [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Product> Products { get; private set; }
        
        public async Task OnGetAsync()
        {
            Products = await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}