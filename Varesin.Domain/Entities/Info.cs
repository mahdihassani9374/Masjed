using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.Entities
{
    public class Info
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public InfoType Type { get; set; }
    }
}
