using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.DTO.SlideShow;
using Varesin.Mvc.Models.SlideShow;

namespace Varesin.Mvc.Mapping
{
    public static class SlideShowMapping
    {
        public static SlideShowCreateDto ToDto(this SlideShowCreateViewModel source, string fileName, long length)
        {
            return new SlideShowCreateDto
            {
                Description = source.Description,
                Link = source.Link,
                Title = source.Title,
                Length = length,
                FileName = fileName
            };
        }

        public static List<SlideShowViewModel> ToViewModel(this List<SlideShowDto> sources)
        {
            var result = new List<SlideShowViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static SlideShowViewModel ToViewModel(this SlideShowDto source)
        {
            return new SlideShowViewModel
            {
                Description = source.Description,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                Link = source.Link,
                Title = source.Title
            };
        }

        public static SlideShowEditDto ToDto(this SlideShowEditViewModel source, string fileName, long length)
        {
            return new SlideShowEditDto
            {
                Description = source.Description,
                FileName = fileName,
                Id = source.Id,
                Length = length,
                Link = source.Link,
                Title = source.Title
            };
        }
    }
}
