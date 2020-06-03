using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models
{
    public class UserAccessItemModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public AccessCode Enum { get; set; }
        public bool Checked { get; set; }
    }
}
