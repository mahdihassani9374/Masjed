using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.ContactUs;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class ContactUsMapping
    {
        public static PaginationDto<ContactUsDto> ToDto(this PaginationDto<ContactUs> sources)
        {
            return new PaginationDto<ContactUsDto>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToDto()
            };
        }

        public static List<ContactUsDto> ToDto(this List<ContactUs> sources)
        {
            var result = new List<ContactUsDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static ContactUsDto ToDto(this ContactUs source)
        {
            return new ContactUsDto
            {
                FullName = source.FullName,
                Id = source.Id,
                PhoneNumber = source.PhoneNumber,
                Text = source.Text
            };
        }
    }
}
