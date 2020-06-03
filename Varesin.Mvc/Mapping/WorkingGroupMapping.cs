using System.Collections.Generic;
using Varesin.Domain.DTO.WorkingGroup;
using Varesin.Mvc.Models.WorkingGroup;

namespace Varesin.Mvc.Mapping
{
    public static class WorkingGroupMapping
    {
        public static WorkingGroupCreateDto ToDto(this WorkingGroupCreateViewModel source)
        {
            return new WorkingGroupCreateDto
            {
                Title = source.Title
            };
        }

        public static WorkingGroupDto ToDto(this WorkingGroupViewModel source)
        {
            return new WorkingGroupDto
            {
                Id = source.Id,
                Title = source.Title
            };
        }
        public static List<WorkingGroupViewModel> ToViewModel(this List<WorkingGroupDto> sources)
        {
            var result = new List<WorkingGroupViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static WorkingGroupViewModel ToViewModel(this WorkingGroupDto source)
        {
            return new WorkingGroupViewModel
            {
                Title = source.Title,
                Id = source.Id
            };
        }
    }
}
