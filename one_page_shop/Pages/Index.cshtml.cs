using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using one_page_shop.Data;
using one_page_shop.Models;

namespace one_page_shop.Pages
{
    public class IndexModel : PageModel
    {       

        private readonly ApplicationDbContext _dbContext;
        
        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<Product> Products { get; set; }
        public async Task OnGetAsync()
        {
            Products = await _dbContext.Products.AsNoTracking().ToListAsync();
        }

      
        public async Task <IActionResult> OnPostCartAsync(int id)
        {
            try
            {
                var cartItem = new UserCart
                {
                    CartItemId = id,
                    UserId = User.FindFirst(ClaimTypes.Name).Value
                };

                _dbContext.UserCarts.Add(cartItem);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //return RedirectToPage("~/Identity/Account/Login");
                return Redirect("~/Identity/Account/Login"); 
            }
           
            
            return RedirectToPage();
        }
    }
}
