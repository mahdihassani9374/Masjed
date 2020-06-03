using System.Collections.Generic;
using Varesin.Domain.DTO.Member;
using Varesin.Domain.DTO.Pagination;
using Varesin.Mvc.Models.Member;
using Varesin.Mvc.Models.Pagination;

namespace Varesin.Mvc.Mapping
{
    public static class MemberMapping
    {
        public static RegistrationDto ToDto(this MemberRegistrationViewModel source)
        {
            return new RegistrationDto
            {
                AttendOnFridays = source.AttendOnFridays,
                BirthDate = source.BirthDate,
                Description = source.Description,
                EducationalBackground = source.EducationalBackground,
                Field = source.Field,
                FullName = source.FullName,
                IsSingle = source.IsSingle,
                JihadiHistory = source.JihadiHistory,
                Other = source.Other,
                PhoneNumber = source.PhoneNumber,
                Skill = source.Skill,
                SuggestedFreeTime = source.SuggestedFreeTime,
                UniversityName = source.UniversityName,
                WorkExperienceWithAgeGroup = source.WorkExperienceWithAgeGroup,
                WorkingGroupOfferId = source.WorkingGroupOfferId  
            };
        }

        public static MemberSearchDto ToDto(this MemberSearchViewModel source)
        {
            return new MemberSearchDto
            {
                FullName = source.FullName,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                PhoneNumber = source.PhoneNumber,
                Status = source.Status
            };
        }

        public static List<MemberViewModel> ToViewModel(this List<MemberDto> sources)
        {
            var result = new List<MemberViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static MemberViewModel ToViewModel(this MemberDto source)
        {
            return new MemberViewModel
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
                WorkingGroup = source.WorkingGroup?.ToViewModel(),
                WorkingGroupId = source.WorkingGroupId,
                WorkingGroupOffer = source.WorkingGroupOffer?.ToViewModel(),
                WorkingGroupOfferId = source.WorkingGroupOfferId,
                CreateDate = source.CreateDate,
                InterviewerId = source.InterviewerId,
                 InterviewerDescription=source.InterviewerDescription
            };
        }

        public static PaginationViewModel<MemberViewModel> ToViewModel(this PaginationDto<MemberDto> sources)
        {
            return new PaginationViewModel<MemberViewModel>
            {
                Count = sources.Count,
                Data = sources.Data.ToViewModel(),
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize
            };
        }
    }


}
