using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace one_page_shop.Models
{
    public class ProductViewModel
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Product image")]
        public IFormFile ProductImageFormFile { get; set; }
    }
}
