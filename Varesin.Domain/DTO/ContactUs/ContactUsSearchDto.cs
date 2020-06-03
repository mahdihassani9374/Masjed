using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.ContactUs
{
    public class ContactUsSearchDto
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
