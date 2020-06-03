using System.Collections.Generic;
using Varesin.Domain.DTO.SlideShow;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class SlideShowMapping
    {
        public static SlideShow ToEntity(this SlideShowCreateDto source)
        {
            return new SlideShow
            {
                Description = source.Description,
                FileName = source.FileName,
                Length = source.Length,
                Link = source.Link,
                Title = source.Title
            };
        }
        public static List<SlideShowDto> ToDto(this List<SlideShow> sources)
        {
            var result = new List<SlideShowDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static SlideShowDto ToDto(this SlideShow source)
        {
            return new SlideShowDto
            {
                Description = source.Description,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                Link = source.Link,
                Title = source.Title
            };
        }
    }
}
