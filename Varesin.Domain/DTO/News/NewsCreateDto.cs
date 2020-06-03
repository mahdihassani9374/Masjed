using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.News
{
    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PrimaryPicture { get; set; }
        public NewsType? Type { get; set; }
    }
}
