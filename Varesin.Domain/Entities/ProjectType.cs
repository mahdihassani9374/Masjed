using System.Collections.Generic;

namespace Varesin.Domain.Entities
{
    public class ProjectType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
