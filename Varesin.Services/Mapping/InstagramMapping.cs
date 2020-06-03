using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.InstagramTag;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class InstagramMapping
    {
        public static List<InstagramTagDto> ToDto(this List<InstagramTag> sources)
        {
            var result = new List<InstagramTagDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static InstagramTagDto ToDto(this InstagramTag source)
        {
            return new InstagramTagDto
            {
                Id = source.Id,
                Name = source.Name
            };
        }
    }
}
