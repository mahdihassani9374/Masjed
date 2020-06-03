using System.Collections.Generic;
using Varesin.Domain.DTO.WorkingGroup;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class WorkingGroupMapping
    {
        public static WorkingGroupDto ToDto(this WorkingGroup source)
        {
            return new WorkingGroupDto
            {
                Id = source.Id,
                Title = source.Title
            };
        }
        public static List<WorkingGroupDto> ToDto(this List<WorkingGroup> sources)
        {
            var result = new List<WorkingGroupDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
    }
}
