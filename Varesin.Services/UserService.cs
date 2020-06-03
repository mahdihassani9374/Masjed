using System.Collections.Generic;
using System.Linq;
using Varesin.Database;
using Varesin.Domain.DTO.Member;
using Varesin.Domain.DTO.WorkingGroup;
using Varesin.Services.Mapping;
using Varesin.Utility;
using DNTPersianUtils.Core;
using Varesin.Domain.Enumeration;
using System;
using Varesin.Domain.DTO.SlideShow;
using Varesin.Domain.DTO.Project;
using Microsoft.EntityFrameworkCore;
using Varesin.Domain.DTO.Report;
using Varesin.Domain.DTO.Pagination;
using Varesin.Utility.Pagination;
using Varesin.Domain.DTO.Payment;
using Varesin.Domain.DTO.ContactUs;
using Varesin.Domain.Entities;

namespace Varesin.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public List<WorkingGroupDto> GetAllWorkingGroup()
        {
            var data = _context.WorkingGroups.ToList();
            return data.ToDto();
        }
        public ServiceResult CreateMember(RegistrationDto model)
        {
            var serviceResult = new ServiceResult(true);

            #region validation
            if (string.IsNullOrEmpty(model.FullName))
                serviceResult.AddError("نام و نام خانوادگی نمی تواند فاقد مقدار باشد");
            if (string.IsNullOrEmpty(model.PhoneNumber))
                serviceResult.AddError("شماره همراه نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.PhoneNumber) && !model.PhoneNumber.IsValidIranianMobileNumber())
                serviceResult.AddError("ساختار شماره همراه وارد شده معتبر نمی باشد");
            if (!model.WorkingGroupOfferId.HasValue)
                serviceResult.AddError("کارگروه پیشنهادی را انتخاب نکرده اید");
            if (string.IsNullOrEmpty(model.Field))
                serviceResult.AddError("رشته تحصیلی نمی تواند فاقد مقدار باشد");

            if (ExistPhoneNumberInMember(model.PhoneNumber))
                serviceResult.AddError("برای شماره همراه وارد شده قبلا ثبت عضویت انجام شده است");

            if (!ExistWorkingGroup(model.WorkingGroupOfferId.Value))
                serviceResult.AddError("کارگروه انتخاب شده در دیبابیس وجود ندارد");

            #endregion



            if (serviceResult.IsSuccess)
            {
                var entity = model.ToEntity();

                entity.Status = MemberStatus.Wating;
                entity.CreateDate = DateTime.Now;

                _context.Members.Add(entity);

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }
        private bool ExistPhoneNumberInMember(string phoneNumber)
        {
            return _context.Members.Any(c => c.PhoneNumber.Equals(phoneNumber));
        }
        private bool ExistWorkingGroup(int workingGroupId)
        {
            return _context.WorkingGroups.Any(c => c.Id.Equals(workingGroupId));
        }
        public List<SlideShowDto> GetAllSlideShows()
        {
            var data = _context.SlideShows.ToList();
            return data.ToDto();
        }
        public List<ProjectDto> GetLastProjects(int count)
        {
            var data = _context.Projects
                .Include(c => c.Type)
                .AsQueryable()
                .OrderByDescending(c => c.Id)
                .Take(count)
                .ToList();

            return data.ToDto();
        }
        public List<ReportDto> GetLastReports(int count)
        {
            var data = _context.Reports.Include(c => c.WorkingGroup).OrderByDescending(c => c.Id).Take(count).ToList();
            return data.ToDto();
        }
        public List<ProjectTypeDto> GetAllProjectTypes()
        {
            var data = _context.ProjectTypes.ToList();
            return data.ToDto();
        }
        public PaginationDto<ProjectDto> GetProjects(ProjectUserSearchDto searchDto)
        {
            var query = _context.Projects.Include(c => c.Type).AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
                query = query.Where(c => c.Title.Contains(searchDto.Title));

            if (searchDto.TypeId.HasValue)
                query = query.Where(c => c.TypeId == searchDto.TypeId.Value);

            if (searchDto.State.HasValue)
                query = query.Where(c => c.State == searchDto.State.Value);

            var projects = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return projects.ToDto();
        }
        public PaginationDto<ReportDto> GetReports(ReportUserSearchDto searchDto)
        {
            var query = _context.Reports.Include(c => c.WorkingGroup).AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
                query = query.Where(c => c.Title.Contains(searchDto.Title));

            if (searchDto.WorkingGroupId.HasValue)
                query = query.Where(c => c.WorkingGroupId == searchDto.WorkingGroupId.Value);

            var reports = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return reports.ToDto();
        }
        public ReportDto GetReport(int id)
        {
            var data = _context.Reports.Include(c => c.WorkingGroup).FirstOrDefault(c => c.Id.Equals(id));

            return data?.ToDto();
        }
        public List<ReportFileDto> GetAllReportFiles(int reportId)
        {
            var data = _context.ReportFile.Where(c => c.ReportId.Equals(reportId)).ToList();
            return data.ToDto();
        }
        public ServiceResult<int> CreatePayment(PaymentCreateDto dto)
        {
            var serviceResult = new ServiceResult<int>(true);

            #region validation
            if (!string.IsNullOrEmpty(dto.FullName) && dto.FullName.Length > 300)
                serviceResult.AddError("نام و نام خانوداگی نمی تواند بیش از 300 کارکتر را شامل شود".ToPersianNumbers());
            if (!string.IsNullOrEmpty(dto.PhoneNumber) && dto.PhoneNumber.Length > 100)
                serviceResult.AddError("شماره همراه نمی تواتد بیش از 100 کارکتر را شامل شود".ToPersianNumbers());
            if (!string.IsNullOrEmpty(dto.PhoneNumber) && !dto.PhoneNumber.IsValidIranianMobileNumber())
                serviceResult.AddError("ساختار شماره همراه وارد شده معتبر نمی باشد");
            if (dto.Price < 1000)
                serviceResult.AddError("مبلغ وارد شده نباید کمتر از 1000 تومان باشد".ToPersianNumbers());
            #endregion
            if (serviceResult.IsSuccess)
            {
                var entity = dto.ToEntity();
                _context.Payments.Add(entity);
                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد مجددا تلاش نمایید");
                else serviceResult.Data = entity.Id;
            }
            return serviceResult;
        }
        public ServiceResult CreateContactUs(ContactUsCreateDto model)
        {
            var serviceResult = new ServiceResult(true);

            #region validation

            if (!string.IsNullOrEmpty(model.FullName) && model.FullName.Length > 200)
                serviceResult.AddError("نام و نام خانوادگی نمی تواند بیش از 200 کارکتر را شامل شود".ToPersianNumbers());
            if (!string.IsNullOrEmpty(model.PhoneNumber) && model.PhoneNumber.Length > 200)
                serviceResult.AddError("شماره همراه نمی تواند بیش از 200 کارکتر را شامل شود".ToPersianNumbers());
            if (string.IsNullOrEmpty(model.Text))
                serviceResult.AddError("متن انتقاد و پیشنهاد نمی تواند فاثد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Text) && model.Text.Length > 3000)
                serviceResult.AddError("متن انتقاد و پیشنهاد نمی تواند بیش از 3000 کارکتر را شامل شود".ToPersianNumbers());
            if (!string.IsNullOrEmpty(model.PhoneNumber) && !model.PhoneNumber.IsValidIranianMobileNumber())
                serviceResult.AddError("ساختار شماره همراه وارد شده معتبر نمی باشد");

            #endregion

            if (serviceResult.IsSuccess)
            {
                _context.ContactUs.Add(new ContactUs
                {
                    Text = model.Text,
                    PhoneNumber = model.PhoneNumber,
                    FullName = model.FullName
                });

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public ServiceResult SuccessPay(int id)
        {
            var serviceResult = new ServiceResult(true);

            var payment = _context.Payments.FirstOrDefault(c => c.Id.Equals(id));

            if (payment != null)
            {
                payment.IsSuccess = true;
                payment.PaymentDate = DateTime.Now;

                if (payment.Type == PaymentType.Project)
                {
                    var project = _context.Projects.FirstOrDefault(c => c.Id.Equals(payment.RecordId));

                    if (project != null)
                    {
                        if (project.CostCollected.HasValue)
                            project.CostCollected += payment.Price;
                        else project.CostCollected = payment.Price;
                    }
                }

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            else serviceResult.AddError("پرداختی یافت نشد");

            return serviceResult;
        }

        public ServiceResult ErroPay(int id)
        {
            var serviceResult = new ServiceResult(true);

            var payment = _context.Payments.FirstOrDefault(c => c.Id.Equals(id));

            if (payment != null)
            {
                payment.IsSuccess = false;
                payment.PaymentDate = DateTime.Now;

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            else serviceResult.AddError("پرداختی یافت نشد");

            return serviceResult;
        }
    }
}
