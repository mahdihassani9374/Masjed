using Varesin.Database.Identity.Entities;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.User
{
    public class UserSearchViewModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMan { get; set; }
        public bool IsFemale { get; set; }
        public OrderType OrderType { get; set; } = OrderType.Ascending;
        public UserSearchType SearchType { get; set; } = UserSearchType.FullName;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public bool SelectAllGender
        {
            get
            {
                if (!IsMan && !IsFemale) return true;
                else return false;
            }
        }
    }
}
