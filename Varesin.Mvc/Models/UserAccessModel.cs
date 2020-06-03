using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models
{
    public class UserAccessModel
    {
        public string Title { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public AccessCode Enum { get; set; }
    }
}
