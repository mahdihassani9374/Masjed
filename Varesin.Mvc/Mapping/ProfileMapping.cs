using Varesin.Domain.DTO.Profile;
using Varesin.Domain.DTO.User;
using Varesin.Mvc.Models.Profile;

namespace Varesin.Mvc.Mapping
{
    public static class ProfileMapping
    {
        public static ChangeProfileViewModel ToProfileViewModel(this UserDto source)
        {
            return new ChangeProfileViewModel
            {
                FullName = source.FullName,
                Id = source.Id,
                PhoneNumber = source.PhoneNumber,
                ImageName = source.ImageName
            };
        }
        public static ChangeProfileDto ToDto(this ChangeProfileViewModel source)
        {
            return new ChangeProfileDto
            {
                Id = source.Id,
                ImageName = source.ImageName,
                FullName = source.FullName,
                PhoneNumber = source.PhoneNumber
            };
        }
        public static ChangePasswordDto ToDto(this ChangePasswordViewModel source, string userId)
        {
            return new ChangePasswordDto
            {
                UserId = userId,
                ConfirmNewPassword = source.ConfirmNewPassword,
                NewPassword = source.NewPassword,
                Password = source.Password
            };
        }
    }
}
