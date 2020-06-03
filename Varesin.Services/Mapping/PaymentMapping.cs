using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.Payment;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class PaymentMapping
    {
        public static Payment ToEntity(this PaymentCreateDto source)
        {
            return new Payment
            {
                CreateDate = source.CreateDate,
                FullName = source.FullName,
                PhoneNumber = source.PhoneNumber,
                Price = source.Price,
                RecordId = source.RecordId,
                Type = source.Type
            };
        }
        public static PaginationDto<PaymentDto> ToDto(this PaginationDto<Payment> sources, List<Project> projects)
        {
            return new PaginationDto<PaymentDto>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToDto(projects)
            };
        }
        public static List<PaymentDto> ToDto(this List<Payment> sources, List<Project> projects)
        {
            var result = new List<PaymentDto>();
            foreach (var source in sources)
                result.Add(source.ToDto(projects));
            return result;

        }
        public static List<PaymentDto> ToDto(this List<Payment> sources)
        {
            var result = new List<PaymentDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;

        }
        public static PaymentDto ToDto(this Payment source)
        {
            return new PaymentDto
            {
                CreateDate = source.CreateDate,
                FullName = source.FullName,
                Id = source.Id,
                IsSuccess = source.IsSuccess,
                PaymentDate = source.PaymentDate,
                PhoneNumber = source.PhoneNumber,
                Price = source.Price,
                Type = source.Type,
                RecordId = source.RecordId,
            };
        }
        public static PaymentDto ToDto(this Payment source, List<Project> projects)
        {
            return new PaymentDto
            {
                CreateDate = source.CreateDate,
                FullName = source.FullName,
                Id = source.Id,
                IsSuccess = source.IsSuccess,
                PaymentDate = source.PaymentDate,
                PhoneNumber = source.PhoneNumber,
                Price = source.Price,
                Type = source.Type,
                RecordId = source.RecordId,
                Project = projects.FirstOrDefault(c => c.Id == source.RecordId)?.ToDto()
            };
        }
    }
}
