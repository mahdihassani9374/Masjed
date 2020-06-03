using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.DTO.News;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Post;
using Varesin.Mvc.Models.News;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Post;

namespace Varesin.Mvc.Mapping
{
    public static class NewsMapping
    {
        public static NewsSearchDto ToDto(this NewsSearchViewModel source)
        {
            return new NewsSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Title = source.Title,
                Type = source.Type
            };
        }

        public static NewsViewModel ToViewModel(this NewsDto source)
        {
            return new NewsViewModel
            {
                Id = source.Id,
                CreateDate = source.CreateDate,
                Title = source.Title,
                Description = source.Description,
                PrimaryPicture = source.PrimaryPicture,
                Type = source.Type
            };
        }
        public static List<NewsViewModel> ToViewModel(this List<NewsDto> sources)
        {
            var result = new List<NewsViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static PaginationViewModel<NewsViewModel> ToVewModel(this PaginationDto<NewsDto> sources)
        {
            return new PaginationViewModel<NewsViewModel>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToViewModel()
            };
        }
        public static NewsCreateDto ToDto(this NewsCreateViewModel source, string fileName)
        {
            return new NewsCreateDto
            {
                Description = source.Description,
                Title = source.Title,
                PrimaryPicture = fileName
            };
        }

        public static NewsEditDto ToDto(this NewsEditViewModel source, string fileName)
        {
            return new NewsEditDto
            {
                Description = source.Description,
                Id = source.Id,
                PrimaryPicture = fileName,
                Title = source.Title,
                Type = source.Type
            };
        }
        public static List<NewsFileViewModel> ToViewModel(this List<NewsFileDto> sources)
        {
            var result = new List<NewsFileViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static NewsFileViewModel ToViewModel(this NewsFileDto source)
        {
            return new NewsFileViewModel
            {
                CountDownload = source.CountDownload,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                Title = source.Title,
                Type = source.Type
            };
        }
    }
}
