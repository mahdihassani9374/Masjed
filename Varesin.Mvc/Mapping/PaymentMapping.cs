using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Payment;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Payment;

namespace Varesin.Mvc.Mapping
{
    public static class PaymentMapping
    {
        public static PaymentCreateDto ToDto(this PaymentCreateViewModel source)
        {
            return new PaymentCreateDto
            {
                CreateDate = source.CreateDate,
                FullName = source.FullName,
                PhoneNumber = source.PhoneNumber,
                Price = source.Price,
                RecordId = source.RecordId,
                Type = source.Type
            };
        }
        public static PaymentSearchDto ToDto(this PaymentSearchViewModel source)
        {
            return new PaymentSearchDto
            {
                IsSuccess = source.IsSuccess,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Type = source.Type
            };
        }

        public static PaginationViewModel<PaymentViewModel> ToViewModel(this PaginationDto<PaymentDto> sources)
        {
            return new PaginationViewModel<PaymentViewModel>
            {
                PageSize = sources.PageSize,
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                Data = sources.Data.ToViewModel()
            };
        }
        public static List<PaymentViewModel> ToViewModel(this List<PaymentDto> sources)
        {
            var result = new List<PaymentViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static PaymentViewModel ToViewModel(this PaymentDto source)
        {
            return new PaymentViewModel
            {
                CreateDate = source.CreateDate,
                FullName = source.FullName,
                Id = source.Id,
                IsSuccess = source.IsSuccess,
                PaymentDate = source.PaymentDate,
                PhoneNumber = source.PhoneNumber,
                Price = source.Price,
                Project = source.Project?.ToViewModel(),
                RecordId = source.RecordId,
                Type = source.Type
            };
        }
    }
}
