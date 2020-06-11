using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.Event;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class EventMapping
    {
        public static Event ToEntity(this EventCreateDto source)
        {
            return new Event
            {
                Date = source.Date,
                Description = source.Description,
                EndDate = source.EndDate,
                PrimaryPicture = source.PrimaryPicture,
                StartDate = source.StartDate,
                Title = source.Title,
                Time = source.Time
            };
        }

        public static List<EventDto> ToDto(this List<Event> sources)
        {
            var result = new List<EventDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static EventDto ToDto(this Event source)
        {
            return new EventDto
            {
                CreateDate = source.CreateDate,
                Description = source.Description,
                Id = source.Id,
                PrimaryPicture = source.PrimaryPicture,
                Title = source.Title,
                Date = source.Date,
                EndDate = source.EndDate,
                MultiDay = source.MultiDay,
                StartDate = source.StartDate,
                Time = source.Time
            };
        }

        public static PaginationDto<EventDto> ToDto(this PaginationDto<Event> source)
        {
            return new PaginationDto<EventDto>
            {
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Data = source.Data.ToDto()

            };
        }
    }
}
