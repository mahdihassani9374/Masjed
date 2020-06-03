using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Project
{
    public class ProjectSearchViewModel
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int? TypeId { get; set; }
        public ProjectState? State { get; set; }
        public string Title { get; set; }
    }
}
