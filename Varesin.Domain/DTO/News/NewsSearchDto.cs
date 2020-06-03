using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.News
{
    public class NewsSearchDto
    {
        public string Title { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public NewsType? Type { get; set; }
    }
}
