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
using Varesin.Domain.DTO.Event;
using Varesin.Domain.DTO.News;
using System.ComponentModel.DataAnnotations;
using Varesin.Domain.DTO.Post;

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

        public List<EventDto> GetAllNowEvent()
        {
            var query = _context.Events.AsQueryable();

            var data = query
                .Where(c => (c.MultiDay == false && c.Date.HasValue && c.Date.Value.Date >= DateTime.Now.Date)
                ||
                (c.MultiDay && c.StartDate.HasValue && c.EndDate.HasValue && c.StartDate.Value.Date <= DateTime.Now.Date && c.EndDate.Value.Date >= DateTime.Now.Date)
                ||
                (c.MultiDay && c.StartDate.HasValue && c.EndDate.HasValue && c.StartDate.Value.Date >= DateTime.Now.Date && c.EndDate.Value.Date >= DateTime.Now.Date))
                .ToList();

            var dd = query.ToList();

            return data.ToDto();
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

        public List<NewsDto> GetLastNews(int count, bool isMahal)
        {
            var query = _context.News.AsQueryable();
            if (isMahal)
                query = query.Where(c => c.Type == NewsType.Mahal);
            else query = query.Where(c => c.Type != NewsType.Mahal);

            var data = query.OrderByDescending(c => c.Id).Take(count).ToList();

            return data.ToDto();
        }

        public List<NewsDto> GetLastNews(int count)
        {
            var query = _context.News.AsQueryable();

            var data = query.OrderByDescending(c => c.Id).Take(count).ToList();

            return data.ToDto();
        }
        public List<PostDto> GetLastPosts(int count)
        {
            var data = _context.Posts.OrderByDescending(c => c.Id).Take(count).ToList();

            return data.ToDto();
        }

        public List<EventDto> GetLastEvent(int count)
        {
            var query = _context.Events.AsQueryable();

            var data = query
                .Where(c => !(c.MultiDay == false && c.Date.HasValue && c.Date.Value.Date >= DateTime.Now.Date)
                &&
                !(c.MultiDay && c.StartDate.HasValue && c.EndDate.HasValue && c.StartDate.Value.Date >= DateTime.Now.Date && c.EndDate.Value.Date <= DateTime.Now.Date)
                &&
                !(c.MultiDay && c.StartDate.HasValue && c.EndDate.HasValue && c.StartDate.Value.Date >= DateTime.Now.Date && c.EndDate.Value.Date >= DateTime.Now.Date))
                .ToList();

            return data.ToDto();
        }

        public PaginationDto<PostDto> GetPosts(PostUserSearchDto searchDto)
        {
            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
                query = query.Where(c => c.Title.Contains(searchDto.Title));

            var posts = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return posts.ToDto();
        }

        public PaginationDto<NewsDto> GetNews(NewsUserSearchDto searchDto)
        {
            var query = _context.News.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
                query = query.Where(c => c.Title.Contains(searchDto.Title));

            if (searchDto.Type.HasValue)
                query = query.Where(c => c.Type == searchDto.Type);

            var news = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return news.ToDto();
        }

        public PaginationDto<EventDto> GetEvents(EventUserSearchDto searchDto)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
                query = query.Where(c => c.Title.Contains(searchDto.Title));

            var news = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return news.ToDto();
        }

        public PostDto GetPost(int id)
        {
            var data = _context.Posts.FirstOrDefault(c => c.Id.Equals(id));

            return data?.ToDto();
        }

        public NewsDto GetNews(int id)
        {
            var data = _context.News.FirstOrDefault(c => c.Id.Equals(id));

            return data?.ToDto();
        }

        public EventDto GetEvent(int id)
        {
            var data = _context.Events.FirstOrDefault(c => c.Id.Equals(id));

            return data?.ToDto();
        }

        public List<PostFileDto> GetAllPostFiles(int postID)
        {
            var data = _context.PostFiles.Where(c => c.PostId.Equals(postID)).ToList();
            return data.ToDto();
        }
        public List<NewsFileDto> GetAllNewsFiles(int newsId)
        {
            var data = _context.NewsFiles.Where(c => c.NewsId.Equals(newsId)).ToList();
            return data.ToDto();
        }
        public List<EventFileDto> GetAllEventFiles(int eventId)
        {
            var data = _context.EventFiles.Where(c => c.EventId.Equals(eventId)).ToList();
            return data.ToDto();
        }
    }
}
