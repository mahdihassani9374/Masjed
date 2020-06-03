using System.Collections.Generic;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.User;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.User;
using DNTPersianUtils.Core;

namespace Varesin.Mvc.Mapping
{
    public static class UserMapping
    {
        public static UserSearchDto ToDto(this UserSearchViewModel source)
        {
            return new UserSearchDto
            {
                FullName = source.FullName,
                IsFemale = source.IsFemale,
                IsMan = source.IsMan,
                OrderType = source.OrderType,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                PhoneNumber = source.PhoneNumber,
                SearchType = source.SearchType
            };
        }
        public static UserViewModel ToViewModel(this UserDto source)
        {
            return new UserViewModel
            {
                RegisterDate = source.RegisterDate.ToFriendlyPersianDateTextify().ToPersianNumbers(),
                FullName = source.FullName.ToPersianNumbers(),
                Gender = source.Gender,
                Id = source.Id,
                PhoneNumber = source.PhoneNumber.ToPersianNumbers(),
                IsSuperAdmin = source.IsSuperAdmin
            };
        }
        public static List<UserViewModel> ToViewModel(this List<UserDto> sources)
        {
            var result = new List<UserViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static PaginationViewModel<UserViewModel> ToVewModel(this PaginationDto<UserDto> sources)
        {
            return new PaginationViewModel<UserViewModel>
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
