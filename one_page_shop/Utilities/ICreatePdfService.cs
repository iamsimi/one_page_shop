using one_page_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace one_page_shop.Utilities
{
    public interface ICreatePdfService
    {
        string CreatePdf(IList<Product> products);
    }
}
