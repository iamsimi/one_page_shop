using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using one_page_shop.Data;
using one_page_shop.Models;


namespace one_page_shop.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public CartModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;           
        }

        public IList<Product> Products { get; set; }
        public void OnGet()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.Name).Value;
                var cartItems = _dbContext.UserCarts.Where(m => m.UserId == userId).Select(m => m.CartItemId).Distinct().ToArray();

                Products = (from product in _dbContext.Products
                            where cartItems.Contains(product.Id)
                            select product).ToList();

                if (!Products.Any())
                {
                    RedirectToPage("./Index");
                }

            }
            catch (NullReferenceException )
            {
                RedirectToPage("./Error");
            }

        }


        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            try
            {
                var removeItem = _dbContext.UserCarts.FirstOrDefault(m=> m.CartItemId == id);
                 _dbContext.UserCarts.Remove(removeItem);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception )
            {
                return RedirectToPage("./Cart");
            }

            return RedirectToAction("OnGet");
        }

    }
}