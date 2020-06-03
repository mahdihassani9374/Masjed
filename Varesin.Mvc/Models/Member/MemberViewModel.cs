using System;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.Models.WorkingGroup;

namespace Varesin.Mvc.Models.Member
{
    public class MemberViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// نام و نام خانواگی
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// شماهر همراه
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// نام دانشکاه / حوزه ؟
        /// </summary>
        public string UniversityName { get; set; }

        /// <summary>
        /// سابقه آموزشی دارید ؟ در کجا ؟
        /// </summary>
        public string EducationalBackground { get; set; }

        /// <summary>
        /// سابقه جهادی دارید؟ 
        /// </summary>
        public string JihadiHistory { get; set; }

        /// <summary>
        /// مهارت
        /// </summary>
        public string Skill { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// زمان آزاد پیشنهادی
        /// </summary>
        public string SuggestedFreeTime { get; set; }

        /// <summary>
        /// سابقه کرا با چه گروه سنی دارید
        /// </summary>
        public string WorkExperienceWithAgeGroup { get; set; }


        /// <summary>
        /// امکان حضور در روزهای جمعه
        /// </summary>
        public bool AttendOnFridays { get; set; }

        /// <summary>
        /// true یعنی مجرد 
        /// </summary>
        public bool IsSingle { get; set; }

        public MemberStatus Status { get; set; }

        public string Description { get; set; }

        public virtual WorkingGroupViewModel WorkingGroupOffer { get; set; }

        public int WorkingGroupOfferId { get; set; }

        public virtual WorkingGroupViewModel WorkingGroup { get; set; }

        public int? WorkingGroupId { get; set; }
        public DateTime CreateDate { get; set; }
        public string InterviewerId { get; set; }
        public string InterviewerDescription { get; set; }
    }
}
