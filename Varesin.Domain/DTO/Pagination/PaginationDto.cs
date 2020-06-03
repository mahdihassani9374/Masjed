using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.Pagination
{
    public class PaginationDto<T> : PaginationDto
    {
        public List<T> Data { get; set; }
    }
    public class PaginationDto
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
