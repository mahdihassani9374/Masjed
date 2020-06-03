using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Project
{
    public class ProjectSearchDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? TypeId { get; set; }
        public ProjectState? State { get; set; }
        public string Title { get; set; }
    }
}
