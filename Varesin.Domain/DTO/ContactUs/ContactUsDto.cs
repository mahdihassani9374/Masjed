using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.ContactUs
{
    public class ContactUsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
    }
}
