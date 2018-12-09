using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace one_page_shop.Models
{
    public class UserCart
    {
        public int Id { get; set; }
        public string  UserId { get; set; }
        public int CartItemId { get; set; }
    }
}
