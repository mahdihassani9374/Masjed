using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Project
{
    public class ProjectUserSearchViewModel
    {
        public string Title { get; set; }
        public ProjectState? State { get; set; }
        public int? TypeId { get; set; }
        public int PageSize { get; set; } = 12;
        public int PageNumber { get; set; } = 1;
    }
}
