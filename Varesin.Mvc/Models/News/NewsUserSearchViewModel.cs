using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.News
{
    public class NewsUserSearchViewModel
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 12;
        public int PageNumber { get; set; } = 1;
        public NewsType? Type { get; set; }
    }
}
