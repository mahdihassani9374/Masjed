using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Payment
{
    public class PaymentSearchViewModel
    {
        public PaymentType? Type { get; set; }
        public bool? IsSuccess { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
