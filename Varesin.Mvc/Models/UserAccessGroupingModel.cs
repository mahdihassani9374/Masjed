using System.Collections.Generic;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models
{
    public class UserAccessGroupingModel
    {
        public string Title { get; set; }
        public AccessCode Enum { get; set; }
        public List<UserAccessItemModel> Items { get; set; } = new List<UserAccessItemModel>();
    }
}
