using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Post;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class PostMapping
    {
        public static List<PostDto> ToDto(this List<Post> sources)
        {
            var result = new List<PostDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static PostDto ToDto(this Post source)
        {
            return new PostDto
            {
                CreateDate = source.CreateDate,
                Description = source.Description,
                Id = source.Id,
                PrimaryPicture = source.PrimaryPicture,
                Title = source.Title
            };
        }

        public static PaginationDto<PostDto> ToDto(this PaginationDto<Post> source)
        {
            return new PaginationDto<PostDto>
            {
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Data = source.Data.ToDto()

            };
        }

        public static Post ToEntity(this PostCreateDto source)
        {
            return new Post
            {
                Description = source.Description,
                PrimaryPicture = source.PrimaryPicture,
                Title = source.Title,
            };
        }
        public static List<PostFileDto> ToDto(this List<PostFile> sources)
        {
            var result = new List<PostFileDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static PostFileDto ToDto(this PostFile source)
        {
            return new PostFileDto
            {
                CountDownload = source.CountDownload,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                Title = source.Title,
                Type = source.Type,
                PostId = source.PostId
            };
        }
        public static PostFile ToEntity(this PostFileCreateDto source)
        {
            return new PostFile
            {
                PostId = source.PostId,
                CountDownload = 0,
                FileName = source.FileName,
                Length = source.Length,
                Title = source.Title,
                Type = source.FileType
            };
        }
    }
}
