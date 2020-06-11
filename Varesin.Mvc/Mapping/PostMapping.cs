using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Post;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Post;

namespace Varesin.Mvc.Mapping
{
    public static class PostMapping
    {
        public static PostSearchDto ToDto(this PostSearchViewModel source)
        {
            return new PostSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Title = source.Title
            };
        }

        public static PostViewModel ToViewModel(this PostDto source)
        {
            return new PostViewModel
            {
                Id = source.Id,
                CreateDate = source.CreateDate,
                Title = source.Title,
                Description = source.Description,
                PrimaryPicture = source.PrimaryPicture
            };
        }
        public static List<PostViewModel> ToViewModel(this List<PostDto> sources)
        {
            var result = new List<PostViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static PaginationViewModel<PostViewModel> ToVewModel(this PaginationDto<PostDto> sources)
        {
            return new PaginationViewModel<PostViewModel>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToViewModel()
            };
        }
        public static PostCreateDto ToDto(this PostCreateViewModel source, string fileName)
        {
            return new PostCreateDto
            {
                Description = source.Description,
                Title = source.Title,
                PrimaryPicture = fileName
            };
        }

        public static PostEditDto ToDto(this PostEditViewModel source, string fileName)
        {
            return new PostEditDto
            {
                Description = source.Description,
                Id = source.Id,
                PrimaryPicture = fileName,
                Title = source.Title,
            };
        }
        public static List<PostFileViewModel> ToViewModel(this List<PostFileDto> sources)
        {
            var result = new List<PostFileViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static PostFileViewModel ToViewModel(this PostFileDto source)
        {
            return new PostFileViewModel
            {
                CountDownload = source.CountDownload,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                Title = source.Title,
                Type = source.Type
            };
        }

        public static PostFileCreateDto ToDto(this PostFileCreateViewModel source, string fileName, long length)
        {
            return new PostFileCreateDto
            {
                FileName = fileName,
                FileType = source.FileType,
                Length = length,
                PostId = source.PostId,
                Title = source.Title
            };
        }
    }
}
