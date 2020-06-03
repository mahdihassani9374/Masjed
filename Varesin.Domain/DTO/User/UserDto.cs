using System;

namespace Varesin.Domain.DTO.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string ImageName { get; set; }
    }
}
