using System.Collections.Generic;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Project;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Project;

namespace Varesin.Mvc.Mapping
{
    public static class ProjectMapping
    {
        public static ProjectCreateDto ToDto(this ProjectCreateViewModel source)
        {
            return new ProjectCreateDto
            {
                CostEstimation = source.CostEstimation,
                Description = source.Description,
                Location = source.Location,
                State = source.State,
                Time = source.Time,
                Title = source.Title,
                TypeId = source.TypeId
            };
        }
        public static ProjectSearchDto ToDto(this ProjectSearchViewModel source)
        {
            return new ProjectSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                TypeId = source.TypeId,
                State = source.State,
                Title = source.Title
            };
        }

        public static PaginationViewModel<ProjectViewModel> ToViewModel(this PaginationDto<ProjectDto> sources)
        {
            return new PaginationViewModel<ProjectViewModel>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToViewModel()

            };
        }
        public static List<ProjectViewModel> ToViewModel(this List<ProjectDto> sources)
        {
            var result = new List<ProjectViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static ProjectViewModel ToViewModel(this ProjectDto source)
        {
            return new ProjectViewModel
            {
                CostEstimation = source.CostEstimation,
                Description = source.Description,
                Id = source.Id,
                Location = source.Location,
                State = source.State,
                Time = source.Time,
                Title = source.Title,
                Type = source.Type.ToViewModel(),
                TypeId = source.TypeId,
                ReportId = source.ReportId,
                Report = source.Report?.ToViewModel(),
                CostCollected = source.CostCollected
            };
        }

        public static ProjectTypeViewModel ToViewModel(this ProjectTypeDto source)
        {
            return new ProjectTypeViewModel
            {
                Title = source.Title,
                Id = source.Id
            };
        }
        public static ProjectDto ToDto(this ProjectViewModel source)
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
                TypeId = source.TypeId
            };
        }

        public static ProjectUserSearchDto ToDto(this ProjectUserSearchViewModel source)
        {
            return new ProjectUserSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                State = source.State,
                Title = source.Title,
                TypeId = source.TypeId
            };
        }

        public static List<ProjectViewModel> SetImage(this List<ProjectViewModel> sources)
        {
            int i = 1;
            foreach (var source in sources)
            {
                if (i == 9) i = 1;
                source.RelativeImage = $"./img/project/project_0{i}.jpg";
                i++;
            }
            return sources;
        }

        public static List<ProjectTypeViewModel> ToViewModel(this List<ProjectTypeDto> sources)
        {
            var result = new List<ProjectTypeViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }

        public static ProjectTypeCreateDto ToDto(this ProjectTypeCreateViewModel source)
        {
            return new ProjectTypeCreateDto
            {
                Title = source.Title
            };
        }
        public static ProjectTypeDto ToDto(this ProjectTypeViewModel source)
        {
            return new ProjectTypeDto
            {
                Id = source.Id,
                Title = source.Title
            };
        }
    }
}
