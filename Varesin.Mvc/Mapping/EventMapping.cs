using System.Collections.Generic;
using Varesin.Domain.DTO.Event;
using Varesin.Domain.DTO.Pagination;
using Varesin.Mvc.Extensions;
using Varesin.Mvc.Models.Event;
using Varesin.Mvc.Models.Pagination;

namespace Varesin.Mvc.Mapping
{
    public static class EventMapping
    {
        public static EventCreateDto ToDto(this EventCreateViewModel source, string fileName)
        {
            return new EventCreateDto
            {
                Date = source.Date.ToDateTime(),
                Description = source.Description,
                EndDate = source.EndDate.ToDateTime(),
                MultiDay = source.MultiDay,
                PrimaryPicture = fileName,
                StartDate = source.StartDate.ToDateTime(),
                Time = source.Time,
                Title = source.Title
            };
        }

        public static EventSearchDto ToDto(this EventSearchViewModel source)
        {
            return new EventSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Title = source.Title
            };
        }

        public static EventViewModel ToViewModel(this EventDto source)
        {
            return new EventViewModel
            {
                Id = source.Id,
                CreateDate = source.CreateDate,
                Title = source.Title,
                Description = source.Description,
                PrimaryPicture = source.PrimaryPicture,
                Date = source.Date,
                EndDate = source.EndDate,
                MultiDay = source.MultiDay,
                StartDate = source.StartDate,
                Time = source.Time
            };
        }
        public static List<EventViewModel> ToViewModel(this List<EventDto> sources)
        {
            var result = new List<EventViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static PaginationViewModel<EventViewModel> ToVewModel(this PaginationDto<EventDto> sources)
        {
            return new PaginationViewModel<EventViewModel>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToViewModel()
            };
        }
    }
}
