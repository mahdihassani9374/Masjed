using System.Collections.Generic;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Report;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class ReportMapping
    {
        public static Report ToEntity(this ReportCreateDto source)
        {
            return new Report
            {
                Description = source.Description,
                PrimaryPicture = source.PrimaryPicture,
                Title = source.Title,
                WorkingGroupId = source.WorkingGroupId.Value,
            };
        }

        public static PaginationDto<ReportDto> ToDto(this PaginationDto<Report> source)
        {
            return new PaginationDto<ReportDto>
            {
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Data = source.Data.ToDto()

            };
        }
        public static List<ReportDto> ToDto(this List<Report> sources)
        {
            var result = new List<ReportDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static ReportDto ToDto(this Report source)
        {
            return new ReportDto
            {
                Id = source.Id,
                WorkingGroup = source.WorkingGroup?.ToDto(),
                WorkingGroupId = source.WorkingGroupId,
                Title = source.Title,
                Description = source.Description,
                PrimaryPicture = source.PrimaryPicture
            };
        }
        public static ReportFile ToEntity(this ReportFileCreateDto source)
        {
            return new ReportFile
            {
                CountDownload = 0,
                FileName = source.FileName,
                Length = source.Length,
                ReportId = source.ReportId,
                Title = source.Title,
                Type = source.FileType
            };
        }
        public static List<ReportFileDto> ToDto(this List<ReportFile> sources)
        {
            var result = new List<ReportFileDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static ReportFileDto ToDto(this ReportFile source)
        {
            return new ReportFileDto
            {
                CountDownload = source.CountDownload,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                ReportId = source.ReportId,
                Title = source.Title,
                Type = source.Type,
                Report = source.Report?.ToDto()
            };
        }
    }
}
