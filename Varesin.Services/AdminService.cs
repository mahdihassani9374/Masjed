using Varesin.Database;
using Varesin.Domain.DTO.Pagination;
using Varesin.Domain.DTO.User;
using Varesin.Services.Mapping;
using Varesin.Utility.Pagination;
using System.Linq;
using Varesin.Database.Identity.Entities;
using Varesin.Domain.Enumeration;
using Varesin.Utility;
using Varesin.Domain.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Varesin.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using DNTPersianUtils.Core;
using Varesin.Domain.DTO.SlideShow;
using Varesin.Domain.DTO.Profile;
using Varesin.Domain.DTO.Payment;
using Varesin.Domain.DTO.ContactUs;
using Varesin.Domain.DTO.InstagramTag;
using Microsoft.AspNetCore.Authentication.Cookies;
using Varesin.Domain.DTO.Post;
using Varesin.Domain.DTO.News;
using Varesin.Domain.DTO.Event;

namespace Varesin.Services
{
    public class AdminService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public AdminService(AppDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public PaginationDto<UserDto> GetUsers(UserSearchDto searchDto)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.FullName))
                query = query.Where(c => c.FullName.Contains(searchDto.FullName));

            if (!string.IsNullOrEmpty(searchDto.PhoneNumber))
                query = query.Where(c => c.PhoneNumber.Contains(searchDto.PhoneNumber));

            if (searchDto.IsFemale && !searchDto.IsMan)
                query = query.Where(c => c.Gender == GenderType.Female);

            if (!searchDto.IsFemale && searchDto.IsMan)
                query = query.Where(c => c.Gender == GenderType.Man);

            IOrderedQueryable<User> queryOrderd = null;

            if (searchDto.OrderType == OrderType.Ascending)
            {
                if (searchDto.SearchType == UserSearchType.FullName)
                    queryOrderd = query.OrderBy(c => c.FullName);
                else queryOrderd = query.OrderBy(c => c.RegisterDate);
            }
            else
            {
                if (searchDto.SearchType == UserSearchType.FullName)
                    queryOrderd = query.OrderByDescending(c => c.FullName);
                else queryOrderd = query.OrderByDescending(c => c.RegisterDate);
            }

            var users = queryOrderd.ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return users.ToDto();
        }
        public UserDto GetUser(string userId)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id.Equals(userId));
            return user?.ToDto();
        }
        public User GetUserEntity(string userId)
        {
            return _context.Users.FirstOrDefault(c => c.Id.Equals(userId));
        }

        public ServiceResult DeleteInstagramTag(int id)
        {
            var serviceResult = new ServiceResult(true);

            var entity = _context.InstagramTags.FirstOrDefault(c => c.Id == id);

            if (entity == null)
                serviceResult.AddError("شناسه آیدی اینستاگرام یافت نشد");
            else
            {
                _context.Entry(entity).State = EntityState.Deleted;

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public async Task<ServiceResult> CreateUser(RegisterDto dto)
        {
            var serviceResult = new ServiceResult(true);

            #region validation
            if (string.IsNullOrEmpty(dto.FullName))
                serviceResult.AddError("نام و نام خانوداگی نمی تواند فاقد مقدار باشد");
            if (string.IsNullOrEmpty(dto.PhoneNumber))
                serviceResult.AddError("شماره همراه نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(dto.PhoneNumber) && !dto.PhoneNumber.IsValidIranianMobileNumber())
                serviceResult.AddError("ساختار شماره همراه وارد شده درست نمی باشد");
            if (string.IsNullOrEmpty(dto.Password))
                serviceResult.AddError("رمزعبور وارد شده نمی تواند فاقد مقدار باشد");
            if (!dto.Gender.HasValue)
                serviceResult.AddError("جنسیت را انتخاب نکرده اید");
            if (!string.IsNullOrEmpty(dto.FullName) && dto.FullName.Length > 500)
                serviceResult.AddError("نام و نام خانواگی نمی تواند بیش از 500 کاراکتر را شامل شود".ToPersianNumbers());
            if (!string.IsNullOrEmpty(dto.PhoneNumber) && dto.PhoneNumber.Length > 256)
                serviceResult.AddError("شماره همراه وارد نشده نباید بیش از 256 کارکتر را شامل شود".ToPersianNumbers());
            #endregion

            if (serviceResult.IsSuccess)
            {
                var identityResult = await _userManager.CreateAsync(new User
                {
                    FullName = dto.FullName,
                    Gender = (GenderType)dto.Gender,
                    IsSuperAdmin = false,
                    PhoneNumber = dto.PhoneNumber,
                    RegisterDate = DateTime.Now,
                    UserName = dto.PhoneNumber
                }, dto.Password);

                if (!identityResult.Succeeded)
                {
                    var identityErrors = GetIdentityErrorMessage(identityResult.Errors.Select(c => c.Code).ToList());
                    serviceResult.Errors = identityErrors;
                    serviceResult.IsSuccess = false;
                }
            }

            return serviceResult;
        }

        public async Task<List<string>> GetRoles(string userId)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == userId);
            var roles = await _userManager.GetRolesAsync(user);
            return roles?.ToList();
        }
        public async Task<List<string>> GetRoles(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles?.ToList();
        }

        private List<string> GetIdentityErrorMessage(List<string> codes)
        {
            var result = new List<string>();
            foreach (var code in codes)
            {
                if (code == "PasswordTooShort")
                    result.Add("رمزعبور باید دارای شش کاراکتر باشد");
                else if (code == "DuplicateUserName")
                    result.Add("شماره همراه وارد شده تکراری می باشد");
                else
                    result.Add(code);
            }
            return result;
        }

        public async Task<IdentityResult> RemoveRoles(User user, List<string> roles)
        {
            var ddd = await _userManager.UpdateSecurityStampAsync(user);
            return await _userManager.RemoveFromRolesAsync(user, roles);
        }
        public async Task<IdentityResult> AddRoles(User user, List<string> roles)
        {
            var ddd = await _userManager.UpdateSecurityStampAsync(user);
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public string GetFullName(string userId)
        {
            return _context.Users.Where(c => c.Id.Equals(userId)).Select(c => c.FullName).FirstOrDefault();
        }

        public ServiceResult CreateSlideShow(SlideShowCreateDto model)
        {
            var serviceResult = new ServiceResult(true);

            #region validation
            if (string.IsNullOrEmpty(model.Title))
                serviceResult.AddError("عنوان اسلایدشو نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Title) && model.Title.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای عنوان اسلایدشو نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (!string.IsNullOrEmpty(model.Description) && model.Description.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای توضیحات اسلایدشو نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (!string.IsNullOrEmpty(model.Link) && model.Link.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای لینک اسلایدشو نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            #endregion

            if (serviceResult.IsSuccess)
            {
                _context.SlideShows.Add(model.ToEntity());

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public List<SlideShowDto> GetAllSlideShow()
        {
            var query = _context.SlideShows.ToList();
            return query.ToDto();
        }

        public SlideShowDto GetSlideShow(int id)
        {
            var data = _context.SlideShows.FirstOrDefault(c => c.Id.Equals(id));

            return data?.ToDto();
        }
        public ServiceResult<string> EditSlideShow(SlideShowEditDto model)
        {
            var serviceResult = new ServiceResult<string>(true);

            var entity = _context.SlideShows.FirstOrDefault(c => c.Id == model.Id);

            if (entity == null)
                serviceResult.AddError("گزارشی با شناسه ارسالی یافت نشد");

            #region validation
            if (string.IsNullOrEmpty(model.Title))
                serviceResult.AddError("عنوان اسلایدشو نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Title) && model.Title.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای عنوان اسلایدشو نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (!string.IsNullOrEmpty(model.Description) && model.Description.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای توضیحات اسلایدشو نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (!string.IsNullOrEmpty(model.Link) && model.Link.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای لینک اسلایدشو نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            #endregion

            if (serviceResult.IsSuccess)
            {
                entity.Title = model.Title;
                entity.Link = model.Link;
                entity.Description = model.Description;

                if (!string.IsNullOrEmpty(model.FileName))
                {
                    serviceResult.Data = entity.FileName;
                    entity.FileName = model.FileName;
                    entity.Length = model.Length;
                }

                _context.Entry(entity).State = EntityState.Modified;
                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public ServiceResult<string> DeleteSlideShow(int id)
        {
            var serviceResult = new ServiceResult<string>(true);

            var entity = _context.SlideShows.FirstOrDefault(c => c.Id == id);

            if (entity == null)
                serviceResult.AddError(" اسلایدشویی با شناسه ارسالی یافت نشد");
            else
            {
                serviceResult.Data = entity.FileName;

                _context.Entry(entity).State = EntityState.Deleted;

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public ServiceResult<string> ChangeProfile(ChangeProfileDto dto)
        {
            var serviceResult = ChangeProfile_Validation(dto);

            var entity = _context.Users.FirstOrDefault(c => c.Id == dto.Id);

            if (entity == null)
                serviceResult.AddError("کاربری با شناسه ارسالی یافت نشد");

            if (entity.PhoneNumber != dto.PhoneNumber)
            {
                var countPhoneNumber = _context.Users.Where(c => c.PhoneNumber.Equals(dto.PhoneNumber)).Count();
                if (countPhoneNumber != 0)
                    serviceResult.AddError("شماره همراه وارد شده متعلق به شخص دیگری می باشد");
            }

            if (serviceResult.IsSuccess)
            {
                entity.PhoneNumber = dto.PhoneNumber;
                entity.FullName = dto.FullName;

                if (!string.IsNullOrEmpty(dto.ImageName))
                {
                    serviceResult.Data = entity.ImageName;
                    entity.ImageName = dto.ImageName;
                }

                _context.Entry(entity).State = EntityState.Modified;
                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }
            else serviceResult.Data = entity.ImageName;

            return serviceResult;
        }

        public ServiceResult<string> ChangeProfile_Validation(ChangeProfileDto dto)
        {
            var serviceResult = new ServiceResult<string>(true);

            #region validation
            if (string.IsNullOrEmpty(dto.FullName))
                serviceResult.AddError("نام و نام خانوداگی نمی تواند فاقد مقدار باشد");
            if (string.IsNullOrEmpty(dto.PhoneNumber))
                serviceResult.AddError("شماره همراه نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(dto.PhoneNumber) && !dto.PhoneNumber.IsValidIranianMobileNumber())
                serviceResult.AddError("ساختار شماره همراه وارد شده درست نمی باشد");
            if (!string.IsNullOrEmpty(dto.FullName) && dto.FullName.Length > 500)
                serviceResult.AddError("نام و نام خانواگی نمی تواند بیش از 500 کاراکتر را شامل شود".ToPersianNumbers());
            if (!string.IsNullOrEmpty(dto.PhoneNumber) && dto.PhoneNumber.Length > 256)
                serviceResult.AddError("شماره همراه وارد نشده نباید بیش از 256 کارکتر را شامل شود".ToPersianNumbers());
            #endregion

            return serviceResult;
        }

        public async Task<ServiceResult> ChangePassword(ChangePasswordDto model)
        {
            var serviceResult = new ServiceResult(true);

            var user = _context.Users.FirstOrDefault(c => c.Id.Equals(model.UserId));
            if (user == null)
                serviceResult.AddError("کاربری یافت نشد");

            #region validation

            if (string.IsNullOrEmpty(model.Password))
                serviceResult.AddError("رمز عبور نمی تواند فاقد مقدار باشد");

            if (string.IsNullOrEmpty(model.NewPassword))
                serviceResult.AddError("رمز عبور جدید نمی تواند فاقد مقدرا باشد");

            if (!string.IsNullOrEmpty(model.NewPassword) && model.NewPassword.Length < 6)
                serviceResult.AddError("رمز عبور جدید باید دارای حداقل شش کاراکتر باشد");

            if (model.NewPassword != model.ConfirmNewPassword)
                serviceResult.AddError("رمز عبور جدید با تکرارش مطابقت ندارد");
            #endregion

            if (serviceResult.IsSuccess)
            {
                var identityResult = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

                if (!identityResult.Succeeded)
                {
                    serviceResult.IsSuccess = false;
                    serviceResult.Errors = GetErrorsForChangePassword(identityResult.Errors.Select(c => c.Code).ToList());
                }
            }


            return serviceResult;
        }
        private List<string> GetErrorsForChangePassword(List<string> list)
        {
            var result = new List<string>();

            foreach (var item in list)
            {
                if (item == "PasswordMismatch")
                    result.Add("رمز عبور صحیح نمی باشد");
                else result.Add(item);
            }

            return result;
        }

        public PaginationDto<PaymentDto> GetPayments(PaymentSearchDto searchDto)
        {
            var query = _context.Payments.AsQueryable();

            if (searchDto.Type.HasValue)
                query = query.Where(c => c.Type == searchDto.Type.Value);

            if (searchDto.IsSuccess.HasValue)
                query = query.Where(c => c.IsSuccess == searchDto.IsSuccess.Value);


            var payments = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return payments.ToDto();
        }
        public ServiceResult CreateInfo(List<Info> model)
        {
            var serviceResult = new ServiceResult(true);

            var infoes = _context.Infoes.ToList();
            _context.Infoes.RemoveRange(infoes);
            _context.Infoes.AddRange(model);

            if (_context.SaveChanges() == 0)
                serviceResult.AddError("در انجام عملیات خطایی رخ داد");

            return serviceResult;
        }

        public ServiceResult CreateInstagramTag(string name)
        {
            var serviceResult = new ServiceResult(true);

            if (string.IsNullOrEmpty(name))
                serviceResult.AddError("آیدی نمی تواند فاقد مقدار باشد");

            if (!string.IsNullOrEmpty(name) && name.Length > 400)
                serviceResult.AddError("تعداد کاراکترهای آیدی نمی تواند بیش از 400 کاراکتر باشد".ToPersianNumbers());

            if (serviceResult.IsSuccess)
            {
                _context.InstagramTags.Add(new InstagramTag
                {
                    Name = name
                });

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }
        public List<Info> GetAllInfoes()
        {
            return _context.Infoes.ToList();
        }

        public PaginationDto<ContactUsDto> GetContactUs(ContactUsSearchDto searchDto)
        {
            var query = _context.ContactUs.AsQueryable();

            var data = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return data.ToDto();
        }

        public int CountViewToday()
        {
            var nowDate = DateTime.Now.Date;

            var count = _context.LogServices.Where(c => c.CreateDate.Date == nowDate)
                .GroupBy(c => c.IpAddress)
                .Select(c => c.Key)
                .Count();

            return count;
        }
        public int CountViewLastWeek()
        {
            var maxDate = DateTime.Now.Date;
            var minDate = maxDate.AddDays(-7);

            var count = _context.LogServices.Where(c => c.CreateDate.Date >= minDate && c.CreateDate.Date <= maxDate)
                .GroupBy(c => new { c.IpAddress, c.CreateDate.Date })
                .Select(c => c.Key)
                .Count();

            return count;
        }

        public int CountViewTwoLastWeek()
        {
            var maxDate = DateTime.Now.Date;
            var minDate = maxDate.AddDays(-14);

            var count = _context.LogServices.Where(c => c.CreateDate.Date >= minDate && c.CreateDate.Date <= maxDate)
                .GroupBy(c => new { c.IpAddress, c.CreateDate.Date })
                .Select(c => c.Key)
                .Count();

            return count;
        }
        public int CountViewLastMonth()
        {
            var maxDate = DateTime.Now.Date;
            var minDate = maxDate.AddMonths(-1);

            var count = _context.LogServices.Where(c => c.CreateDate.Date >= minDate && c.CreateDate.Date <= maxDate)
                .GroupBy(c => new { c.IpAddress, c.CreateDate.Date })
                .Select(c => c.Key)
                .Count();

            return count;
        }

        public ServiceResult CreateInstaTag(string name)
        {
            var servieResult = new ServiceResult(true);

            #region validation
            if (string.IsNullOrEmpty(name))
                servieResult.AddError("نام تگ نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(name) && name.Length > 400)
                servieResult.AddError("تعداد کاراکترهای نام تگ نباید بیش از 400 کاراکتر باشد".ToPersianNumbers());
            #endregion

            if (servieResult.IsSuccess)
            {
                if (_context.InstagramTags.Any(c => c.Name == name))
                    servieResult.AddError("نام تگ نمی تواند تکراری باشد");
                else
                {
                    _context.InstagramTags.Add(new InstagramTag
                    {
                        Name = name
                    });

                    if (_context.SaveChanges() == 0)
                        servieResult.AddError("در انجام عملیات خطایی رخ داد");
                }
            }

            return servieResult;
        }

        public List<InstagramTagDto> GetAllInstaTags()
        {
            var data = _context.InstagramTags.OrderByDescending(c => c.Id).ToList();
            return data.ToDto();
        }

        public PaginationDto<PostDto> GetPosts(PostSearchDto searchDto)
        {
            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
                query = query.Where(c => c.Title.Contains(searchDto.Title));

            var posts = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return posts.ToDto();
        }

        public ServiceResult CreateReport(PostCreateDto model)
        {
            var serviceResult = new ServiceResult(true);

            #region validation
            if (string.IsNullOrEmpty(model.Title))
                serviceResult.AddError("عنوان پست نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Title) && model.Title.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای عنوان پست نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (string.IsNullOrEmpty(model.PrimaryPicture))
                serviceResult.AddError("عکس اصلی پست نمی تواند فاقد مقدار باشد");
            #endregion

            if (serviceResult.IsSuccess)
            {
                _context.Posts.Add(model.ToEntity());

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public PostDto GetPost(int id)
        {
            var data = _context.Posts.FirstOrDefault(c => c.Id.Equals(id));

            return data?.ToDto();
        }
        public ServiceResult<string> EditPost(PostEditDto model)
        {
            var serviceResult = new ServiceResult<string>(true);

            var entity = _context.Posts.FirstOrDefault(c => c.Id == model.Id);

            if (entity == null)
                serviceResult.AddError("پستی با شناسه ارسالی یافت نشد");

            #region validation
            if (string.IsNullOrEmpty(model.Title))
                serviceResult.AddError("عنوان پست نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Title) && model.Title.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای عنوان پست نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            #endregion

            if (serviceResult.IsSuccess)
            {
                entity.Title = model.Title;

                if (!string.IsNullOrEmpty(model.PrimaryPicture))
                {
                    serviceResult.Data = entity.PrimaryPicture;
                    entity.PrimaryPicture = model.PrimaryPicture;
                }


                entity.Description = model.Description;

                _context.Entry(entity).State = EntityState.Modified;
                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public ServiceResult<string> DeletePost(int id)
        {
            var serviceResult = new ServiceResult<string>(true);

            var entity = _context.Posts.FirstOrDefault(c => c.Id == id);

            if (entity == null)
                serviceResult.AddError(" پستی با شناسه ارسالی یافت نشد");
            else
            {
                serviceResult.Data = entity.PrimaryPicture;
                _context.Entry(entity).State = EntityState.Deleted;

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public List<PostFileDto> GetAllPostFiles(int postId)
        {
            var data = _context.PostFiles.Where(c => c.PostId.Equals(postId)).ToList();
            return data.ToDto();
        }
        public PostFileDto GetPostFile(int id)
        {
            var data = _context.PostFiles.FirstOrDefault(c => c.Id.Equals(id));
            return data?.ToDto();
        }
        public ServiceResult DeletePostFile(int id)
        {
            var serviceResult = new ServiceResult(true);

            var entity = _context.PostFiles.FirstOrDefault(c => c.Id == id);

            if (entity == null)
                serviceResult.AddError(" قایلی با شناسه ارسالی یافت نشد");
            else
            {
                _context.Entry(entity).State = EntityState.Deleted;

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }
        public ServiceResult CreatePost(PostCreateDto model)
        {
            var serviceResult = new ServiceResult(true);

            #region validation
            if (string.IsNullOrEmpty(model.Title))
                serviceResult.AddError("عنوان پست نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Title) && model.Title.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای عنوان پست نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (string.IsNullOrEmpty(model.PrimaryPicture))
                serviceResult.AddError("عکس اصلی پست نمی تواند فاقد مقدار باشد");
            #endregion

            if (serviceResult.IsSuccess)
            {
                _context.Posts.Add(model.ToEntity());

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public PaginationDto<NewsDto> GetNews(NewsSearchDto searchDto)
        {
            var query = _context.News.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
                query = query.Where(c => c.Title.Contains(searchDto.Title));

            if (searchDto.Type.HasValue)
                query = query.Where(c => c.Type == searchDto.Type.Value);

            var newses = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return newses.ToDto();
        }

        public PaginationDto<EventDto> GetEvent(EventSearchDto searchDto)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
                query = query.Where(c => c.Title.Contains(searchDto.Title));

            var events = query.OrderByDescending(c => c.Id).ToPaginated(searchDto.PageNumber, searchDto.PageSize);

            return events.ToDto();
        }

        public ServiceResult CreateNews(NewsCreateDto model)
        {
            var serviceResult = new ServiceResult(true);

            #region validation
            if (string.IsNullOrEmpty(model.Title))
                serviceResult.AddError("عنوان خبر نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Title) && model.Title.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای عنوان خبر نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (string.IsNullOrEmpty(model.PrimaryPicture))
                serviceResult.AddError("عکس اصلی خبر نمی تواند فاقد مقدار باشد");
            if (!model.Type.HasValue)
                serviceResult.AddError("نوع خبر نمی تواند فاقد مقدار باشد");
            #endregion

            if (serviceResult.IsSuccess)
            {
                _context.News.Add(model.ToEntity());

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public NewsDto GetNews(int id)
        {
            var data = _context.News.FirstOrDefault(c => c.Id.Equals(id));

            return data?.ToDto();
        }

        public ServiceResult<string> EditNews(NewsEditDto model)
        {
            var serviceResult = new ServiceResult<string>(true);

            var entity = _context.News.FirstOrDefault(c => c.Id == model.Id);

            if (entity == null)
                serviceResult.AddError("خبری با شناسه ارسالی یافت نشد");

            #region validation
            if (string.IsNullOrEmpty(model.Title))
                serviceResult.AddError("عنوان خبر نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Title) && model.Title.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای عنوان خبر نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (!model.Type.HasValue)
                serviceResult.AddError("نوع خبر نیم توناد فاقد مقدار باشد");
            #endregion

            if (serviceResult.IsSuccess)
            {
                entity.Title = model.Title;

                if (!string.IsNullOrEmpty(model.PrimaryPicture))
                {
                    serviceResult.Data = entity.PrimaryPicture;
                    entity.PrimaryPicture = model.PrimaryPicture;
                }


                entity.Description = model.Description;

                _context.Entry(entity).State = EntityState.Modified;
                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }

        public ServiceResult<string> DeleteNews(int id)
        {
            var serviceResult = new ServiceResult<string>(true);

            var entity = _context.News.FirstOrDefault(c => c.Id == id);

            if (entity == null)
                serviceResult.AddError(" خبری با شناسه ارسالی یافت نشد");
            else
            {
                serviceResult.Data = entity.PrimaryPicture;
                _context.Entry(entity).State = EntityState.Deleted;

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }
        public List<NewsFileDto> GetAllNewsFiles(int newsId)
        {
            var data = _context.NewsFiles.Where(c => c.NewsId.Equals(newsId)).ToList();
            return data.ToDto();
        }
        public ServiceResult CreateEvent(EventCreateDto model)
        {
            var serviceResult = new ServiceResult(true);

            #region validation
            if (string.IsNullOrEmpty(model.Title))
                serviceResult.AddError("عنوان برنامه نمی تواند فاقد مقدار باشد");
            if (!string.IsNullOrEmpty(model.Title) && model.Title.Length > 128)
                serviceResult.AddError("تعداد کاراکترهای عنوان برنامه نمی تواند بیش از 128 کاراکتر باشد".ToPersianNumbers());
            if (string.IsNullOrEmpty(model.PrimaryPicture))
                serviceResult.AddError("عکس اصلی برنامه نمی تواند فاقد مقدار باشد");
            #endregion

            var entity = model.ToEntity();

            if (model.MultiDay)
            {
                if (!model.StartDate.HasValue)
                    serviceResult.AddError("تاریخ شروع برنامه یا وارد نشده است و یا ساختارش اشتباه می باشد");

                if (!model.EndDate.HasValue)
                    serviceResult.AddError("تاریخ پایان برنامه یا وارد نشده است و یا ساختارش اشتباه می باشد");

                if (model.StartDate.HasValue && model.EndDate.HasValue)
                    if (model.EndDate.Value <= model.StartDate.Value)
                        serviceResult.AddError("تاریخ پایان برنامه نمی تواند از تاریخ شروع برنامه کمتر باشد");

                model.Date = null;
                model.Time = null;
            }
            else
            {
                if (!model.Date.HasValue)
                    serviceResult.AddError("تاریخ برنامه یا وارد نشده است و یا ساختارش اشتباه می باشد");
                if (string.IsNullOrEmpty(model.Time))
                    serviceResult.AddError("ساعت برنامه نمی تواند فاقد مقدار باشد");

                model.StartDate = null;
                model.EndDate = null;
            }

            if (serviceResult.IsSuccess)
            {

                _context.Events.Add(entity);

                if (_context.SaveChanges() == 0)
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }
    }
}
