using System;
using Microsoft.AspNetCore.Identity;

namespace Varesin.Database.Identity.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string ImageName { get; set; }
    }
    public enum GenderType
    {
        Man = 1,
        Female = 2
    }
}
