using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Payment
{
    public class PaymentCreateViewModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public long Price { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public PaymentType Type { get; set; } = PaymentType.General;
        public int? RecordId { get; set; }
    }
}
