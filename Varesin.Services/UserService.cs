using System.Collections.Generic;
using System.Linq;
using Varesin.Database;
using Varesin.Services.Mapping;
using Varesin.Utility;
using DNTPersianUtils.Core;
using Varesin.Domain.Enumeration;
using System;
using Varesin.Domain.DTO.SlideShow;
using Microsoft.EntityFrameworkCore;
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
       
        public List<SlideShowDto> GetAllSlideShows()
        {
            var data = _context.SlideShows.ToList();
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
