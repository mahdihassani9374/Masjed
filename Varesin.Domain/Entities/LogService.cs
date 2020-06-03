using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.Entities
{
    public class LogService
    {
        public int Id { get; set; }

        public string Method { get; set; }

        public long? ContentLengthRequest { get; set; }

        public long? ContentLengthResponse { get; set; }

        public string RelativePath { get; set; }

        public string UserId { get; set; }

        public long? ElabsedTime { get; set; }

        public int ResponseStatusCode { get; set; }

        public DateTime CreateDate { get; set; }

        public TimeSpan Elabsed { get; set; }

        public string IpAddress { get; set; }
    }
}
