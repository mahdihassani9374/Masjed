using Varesin.Domain.DTO;
using Varesin.Mvc.Models;

namespace Varesin.Mvc.Mapping
{
    public static class AccountMapping
    {
        public static RegisterDto ToDto(this RegisterViewModel source)
        {
            return new RegisterDto
            {
                FullName = source.FullName,
                Gender = (int?)source.Gender,
                Password = source.Password,
                PhoneNumber = source.PhoneNumber
            };
        }
    }
}
