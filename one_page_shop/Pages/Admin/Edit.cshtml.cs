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

    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public EditModel(ApplicationDbContext dbContext)    
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product = await _dbContext.Products.SingleOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return RedirectToPage("/Admin/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Product.Id = id;
            _dbContext.Entry(Product).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("some databaseupdate concurrency exception", ex);
            }

            return RedirectToPage("/Admin/Index");
        }

    }
}