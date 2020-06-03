using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varesin.Domain.DTO.Pagination;

namespace Varesin.Utility.Pagination
{
    public static class Extension
    {
        public static PaginationDto<TEntity> ToPaginated<TEntity>(this IQueryable<TEntity> query, int pageNumber, int pageSize) where TEntity : class
        {
            var result = new PaginationDto<TEntity>();

            result.Count = query.Count();

            result.PageNumber = pageNumber;

            result.PageSize = pageSize;

            result.PageCount = result.Count / result.PageSize;

            if (result.Count % result.PageSize > 0) result.PageCount++;

            result.Data = query
                .Skip((result.PageNumber - 1) * result.PageSize)
                .Take(result.PageSize)
                .ToList();

            return result;
        }
    }
}
