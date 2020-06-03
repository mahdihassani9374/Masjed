using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.Post
{
    public class PostSearchDto
    {
        public string Title { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
