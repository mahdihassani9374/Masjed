using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.DTO.ContactUs;
using Varesin.Domain.DTO.Pagination;
using Varesin.Mvc.Models.ContactUs;
using Varesin.Mvc.Models.Pagination;

namespace Varesin.Mvc.Mapping
{
    public static class ContactUsMapping
    {
        public static ContactUsCreateDto ToDto(this ContactUsCreateViewModel source)
        {
            return new ContactUsCreateDto
            {
                FullName = source.FullName,
                PhoneNumber = source.PhoneNumber,
                Text = source.Text
            };
        }
        public static ContactUsSearchDto ToDto(this ContactUsSearchViewModel source)
        {
            return new ContactUsSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }

        public static List<ContactUsViewModel> ToViewModel(this List<ContactUsDto> sources)
        {
            var result = new List<ContactUsViewModel>();

            foreach (var source in sources)
                result.Add(source.ToViewModel());

            return result;
        }
        public static ContactUsViewModel ToViewModel(this ContactUsDto source)
        {
            return new ContactUsViewModel
            {
                FullName = source.FullName,
                Id = source.Id,
                PhoneNumber = source.PhoneNumber,
                Text = source.Text
            };
        }

        public static PaginationViewModel<ContactUsViewModel> ToViewModel(this PaginationDto<ContactUsDto> sources)
        {
            return new PaginationViewModel<ContactUsViewModel>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToViewModel()
            };
        }
    }
}
