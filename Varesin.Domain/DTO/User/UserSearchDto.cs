using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.User
{
    public class UserSearchDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMan { get; set; }
        public bool IsFemale { get; set; }
        public OrderType OrderType { get; set; }
        public UserSearchType SearchType { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; }
    }
}
