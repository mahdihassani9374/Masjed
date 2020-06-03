using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.DTO.InstagramTag;
using Varesin.Mvc.Models.Instagram;

namespace Varesin.Mvc.Mapping
{
    public static class InstagramMapping
    {
        public static List<InstagramTagViewModel> ToViewModel(this List<InstagramTagDto> sources)
        {
            var result = new List<InstagramTagViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static InstagramTagViewModel ToViewModel(this InstagramTagDto source)
        {
            return new InstagramTagViewModel
            {
                Id = source.Id,
                Name = source.Name
            };
        }
    }
}
