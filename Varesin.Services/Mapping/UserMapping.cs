using System.Collections.Generic;
using Varesin.Database.Identity.Entities;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.User;

namespace Varesin.Services.Mapping
{
    public static class UserMapping
    {
        public static PaginationDto<UserDto> ToDto(this PaginationDto<User> source)
        {
            return new PaginationDto<UserDto>
            {
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Data = source.Data.ToDto()

            };
        }
        public static List<UserDto> ToDto(this List<User> sources)
        {
            var result = new List<UserDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static UserDto ToDto(this User source)
        {
            return new UserDto
            {
                RegisterDate = source.RegisterDate,
                FullName = source.FullName,
                Gender = (int)source.Gender,
                Id = source.Id,
                PhoneNumber = source.PhoneNumber,
                IsSuperAdmin = source.IsSuperAdmin,
                ImageName = source.ImageName
            };
        }
    }
}
