using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Payment
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public long Price { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentType Type { get; set; }
    }
}
