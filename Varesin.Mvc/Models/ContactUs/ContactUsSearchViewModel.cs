using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Varesin.Mvc.Models.ContactUs
{
    public class ContactUsSearchViewModel
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
