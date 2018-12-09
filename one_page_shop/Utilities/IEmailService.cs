using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace one_page_shop.Utilities
{
    public interface IEmailService
    {
        void SendEmail(string path, string sourceEmail, string targetEmail, string emailPassword);
    }
}
