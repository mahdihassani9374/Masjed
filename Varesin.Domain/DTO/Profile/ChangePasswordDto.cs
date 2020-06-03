using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.Profile
{
    public class ChangePasswordDto
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string UserId { get; set; }
    }
}
