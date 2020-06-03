using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Payment
{
    public class PaymentCreateDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public long Price { get; set; }
        public DateTime CreateDate { get; set; }
        public PaymentType Type { get; set; }
        public int? RecordId { get; set; }
    }
}
