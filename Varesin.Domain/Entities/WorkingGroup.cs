using System.Collections;
using System.Collections.Generic;

namespace Varesin.Domain.Entities
{
    public class WorkingGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Member> Members { get; set; }

        public virtual ICollection<Member> MemberOffers { get; set; }
    }
}
