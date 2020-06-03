using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.Member;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.Entities;

namespace Varesin.Services.Mapping
{
    public static class MemberMapping
    {
        public static Member ToEntity(this RegistrationDto source)
        {
            return new Member
            {
                AttendOnFridays = source.AttendOnFridays,
                BirthDate = source.BirthDate,
                Description = source.Description,
                EducationalBackground = source.EducationalBackground,
                Field = source.Field,
                FullName = source.FullName,
                IsSingle = source.IsSingle,
                JihadiHistory = source.JihadiHistory,
                PhoneNumber = source.PhoneNumber,
                Skill = source.Skill,
                SuggestedFreeTime = source.SuggestedFreeTime,
                UniversityName = source.UniversityName,
                WorkExperienceWithAgeGroup = source.WorkExperienceWithAgeGroup,
                WorkingGroupOfferId = source.WorkingGroupOfferId.Value
            };
        }
        public static PaginationDto<MemberDto> ToDto(this PaginationDto<Member> sources)
        {
            return new PaginationDto<MemberDto>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToDto()
            };
        }
        public static MemberDto ToDto(this Member source)
        {
            return new MemberDto
            {
                AttendOnFridays = source.AttendOnFridays,
                BirthDate = source.BirthDate,
                Description = source.Description,
                EducationalBackground = source.EducationalBackground,
                Field = source.Field,
                FullName = source.FullName,
                Id = source.Id,
                IsSingle = source.IsSingle,
                JihadiHistory = source.JihadiHistory,
                PhoneNumber = source.PhoneNumber,
                Skill = source.Skill,
                Status = source.Status,
                SuggestedFreeTime = source.SuggestedFreeTime,
                UniversityName = source.UniversityName,
                WorkExperienceWithAgeGroup = source.WorkExperienceWithAgeGroup,
                WorkingGroup = source.WorkingGroup?.ToDto(),
                WorkingGroupOffer = source.WorkingGroupOffer?.ToDto(),
                WorkingGroupId = source.WorkingGroupId,
                WorkingGroupOfferId = source.WorkingGroupOfferId,
                CreateDate = source.CreateDate,
                InterviewerId = source.InterviewerId,
                InterviewerDescription = source.InterviewerDescription
            };
        }

        public static List<MemberDto> ToDto(this List<Member> sources)
        {
            var result = new List<MemberDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }


    }
}
