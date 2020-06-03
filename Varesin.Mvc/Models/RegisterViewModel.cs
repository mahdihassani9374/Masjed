using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Database.Identity.Entities;

namespace Varesin.Mvc.Models
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public GenderType? Gender { get; set; }
    }
}
