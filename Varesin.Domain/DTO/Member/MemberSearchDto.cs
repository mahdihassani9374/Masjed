using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Member
{
    public class MemberSearchDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public MemberStatus? Status { get; set; }
    }
}
