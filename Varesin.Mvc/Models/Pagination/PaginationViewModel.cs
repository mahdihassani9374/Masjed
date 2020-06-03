using System.Collections.Generic;

namespace Varesin.Mvc.Models.Pagination
{
    public class PaginationViewModel<T> : PaginationViewModel
    {
        public List<T> Data { get; set; }
    }
    public class PaginationViewModel
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
