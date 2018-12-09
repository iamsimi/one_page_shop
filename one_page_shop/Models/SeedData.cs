using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using one_page_shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace one_page_shop.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Products.Any())
                {
                    return;
                }

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Fujifilm X100T 16 MP Digital Camera (Silver)",
                        Description = "", 
                        Price = 520.00M
                    }, 
                    new Product
                    {
                        Name = "Samsung CF591 Series Curved 27-Inch FHD Monitor", 
                        Description = "",
                        Price = 610.00M
                    }, 
                    new Product
                    {
                        Name = "Blue Yeti USB Microphone Blackout Edition", 
                        Description = "", 
                        Price = 120.00M
                    }, 
                    new Product
                    {
                        Name = "DYMO LabelWriter 450 Turbo Thermal Label Printer", 
                        Description = "", 
                        Price = 410.00M
                    }, 
                    new Product
                    {
                        Name = "Pryma Headphones, Rose Gold & Grey", 
                        Description = "", 
                        Price = 180.00M
                    },
                    new Product
                    {
                        Name = "Fujifilm X100T 16 MP Digital Camera (Silver)", 
                        Description = "", 
                        Price = 520.00M
                    }, 
                    new Product
                    {
                        Name = "Samsung CF591 Series Curved 27-Inch FHD Monitor", 
                       
                        Price = 610.00M
                    }, 
                    new Product
                    {
                        Name = "Blue Yeti USB Microphone Blackout Edition",
                     
                        Price = 120.00M
                    },
                    new Product
                    {
                        Name = "DYMO LabelWriter 450 Turbo Thermal Label Printer", 
                     
                        Price = 410.00M
                    },
                    new Product
                    {
                        Name = "DYMO LabelWriter 450 Turbo Thermal Label Printer",
                        
                        Price = 410.00M
                    },
                    new Product
                    {
                        Name = "Pryma Headphones, Rose Gold & Grey",
                        Description = "",
                        Price = 180.00M
                    },
                    new Product
                    {
                        Name = "Fujifilm X100T 16 MP Digital Camera (Silver)",
                        Description = "",
                        Price = 520.00M
                    }
              );
                context.SaveChanges();
            }
        }
    }
}
