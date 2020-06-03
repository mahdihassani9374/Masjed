using System.Collections.Generic;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Project;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class ProjectMapping
    {
        public static List<ProjectTypeDto> ToDto(this List<ProjectType> sources)
        {
            var result = new List<ProjectTypeDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static ProjectTypeDto ToDto(this ProjectType source)
        {
            return new ProjectTypeDto
            {
                Id = source.Id,
                Title = source.Title
            };
        }
        public static Project ToEntity(this ProjectCreateDto source)
        {
            return new Project
            {
                CostEstimation = source.CostEstimation,
                Description = source.Description,
                Location = source.Location,
                State = source.State.Value,
                Time = source.Time,
                Title = source.Title,
                TypeId = source.TypeId.Value
            };
        }

        public static PaginationDto<ProjectDto> ToDto(this PaginationDto<Project> sources)
        {
            return new PaginationDto<ProjectDto>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToDto()

            };
        }
        public static List<ProjectDto> ToDto(this List<Project> sources)
        {
            var result = new List<ProjectDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static ProjectDto ToDto(this Project source)
        {
            return new ProjectDto
            {
                CostEstimation = source.CostEstimation,
                Description = source.Description,
                Id = source.Id,
                Location = source.Location,
                State = source.State,
                Time = source.Time,
                Title = source.Title,
                Type = source.Type.ToDto(),
                TypeId = source.TypeId,
                Report = source.Report?.ToDto(),
                ReportId = source.ReportId,
                CostCollected = source.CostCollected
            };
        }
    }
}
