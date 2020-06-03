using System.Collections.Generic;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Report;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Report;

namespace Varesin.Mvc.Mapping
{
    public static class ReportMapping
    {
        public static ReportCreateDto ToDto(this ReportCreateViewModel source, string fileName)
        {
            return new ReportCreateDto
            {
                Description = source.Description,
                Title = source.Title,
                WorkingGroupId = source.WorkingGroupId,
                PrimaryPicture = fileName
            };
        }
        public static ReportSearchDto ToDto(this ReportSearchViewModel source)
        {
            return new ReportSearchDto
            {
                WorkingGroupId = source.WorkingGroupId,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Title = source.Title
            };
        }

        public static ReportViewModel ToViewModel(this ReportDto source)
        {
            return new ReportViewModel
            {
                Id = source.Id,
                Title = source.Title,
                WorkingGroupId = source.WorkingGroupId,
                Description = source.Description,
                PrimaryPicture = source.PrimaryPicture,
                WorkingGroup = source.WorkingGroup?.ToViewModel()
            };
        }
        public static List<ReportViewModel> ToViewModel(this List<ReportDto> sources)
        {
            var result = new List<ReportViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static PaginationViewModel<ReportViewModel> ToVewModel(this PaginationDto<ReportDto> sources)
        {
            return new PaginationViewModel<ReportViewModel>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToViewModel()
            };
        }

        public static ReportEditDto ToDto(this ReportEditViewModel source, string fileName)
        {
            return new ReportEditDto
            {
                Description = source.Description,
                Id = source.Id,
                PrimaryPicture = fileName,
                Title = source.Title,
                WorkingGroupId = source.WorkingGroupId,
            };
        }

        public static ReportFileCreateDto ToDto(this ReportFileCreateViewModel source, string fileName, long length)
        {
            return new ReportFileCreateDto
            {
                FileType = source.FileType,
                ReportId = source.ReportId,
                Title = source.Title,
                Length = length,
                FileName = fileName
            };
        }
        public static List<ReportFileViewModel> ToViewModel(this List<ReportFileDto> sources)
        {
            var result = new List<ReportFileViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static ReportFileViewModel ToViewModel(this ReportFileDto source)
        {
            return new ReportFileViewModel
            {
                CountDownload = source.CountDownload,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                Report = source.Report?.ToViewModel(),
                ReportId = source.ReportId,
                Title = source.Title,
                Type = source.Type
            };
        }
        public static ReportUserSearchDto ToDto(this ReportUserSearchViewModel source)
        {
            return new ReportUserSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Title = source.Title,
                WorkingGroupId = source.WorkingGroupId
            };
        }
    }
}
