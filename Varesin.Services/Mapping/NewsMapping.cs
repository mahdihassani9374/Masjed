using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.News;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Post;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class NewsMapping
    {
        public static List<NewsDto> ToDto(this List<News> sources)
        {
            var result = new List<NewsDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static NewsDto ToDto(this News source)
        {
            return new NewsDto
            {
                CreateDate = source.CreateDate,
                Description = source.Description,
                Id = source.Id,
                PrimaryPicture = source.PrimaryPicture,
                Title = source.Title,
                Type = source.Type
            };
        }

        public static PaginationDto<NewsDto> ToDto(this PaginationDto<News> source)
        {
            return new PaginationDto<NewsDto>
            {
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Data = source.Data.ToDto()

            };
        }

        public static News ToEntity(this NewsCreateDto source)
        {
            return new News
            {
                Description = source.Description,
                PrimaryPicture = source.PrimaryPicture,
                Title = source.Title,
                Type = source.Type.Value
            };
        }
        public static List<NewsFileDto> ToDto(this List<NewsFile> sources)
        {
            var result = new List<NewsFileDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static NewsFileDto ToDto(this NewsFile source)
        {
            return new NewsFileDto
            {
                CountDownload = source.CountDownload,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                Title = source.Title,
                Type = source.Type,
                NewsId = source.NewsId
            };
        }

        public static NewsFile ToEntity(this NewsFileCreateDto source)
        {
            return new NewsFile
            {
                CountDownload = 0,
                FileName = source.FileName,
                Title = source.Title,
                Length = source.Length,
                NewsId = source.NewsId,
                Type = source.FileType
            };
        }
    }
}
