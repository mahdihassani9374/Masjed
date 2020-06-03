using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Payment
{
    public class PaymentSearchDto
    {
        public PaymentType? Type { get; set; }
        public bool? IsSuccess { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
