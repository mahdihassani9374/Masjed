using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Services;
using Varesin.Services;
using Varesin.Utility;
using Microsoft.AspNetCore.Hosting;
using Varesin.Mvc.Models.News;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        private readonly AdminService _adminService;
        private readonly FileService _fileService;
        private readonly IHostingEnvironment _hostingEnvironment;


        public NewsController(AdminService adminService,
            FileService fileService,
            IHostingEnvironment hostingEnvironment)
        {
            _adminService = adminService;
            _fileService = fileService;
            _hostingEnvironment = hostingEnvironment;
        }

        [AccessCodeFlter(AccessCode.ViewNews)]
        public IActionResult Index(NewsSearchViewModel searchModel)
        {
            var data = _adminService.GetNews(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            List<SelectListItem> typeSelector = new List<SelectListItem>();
            typeSelector.Add(new SelectListItem("نوع خبر", ""));
            typeSelector.Add(new SelectListItem("اخبار محله", NewsType.Mahal.ToString(), searchModel.Type == NewsType.Mahal));
            typeSelector.Add(new SelectListItem("سیاسی", NewsType.Siasi.ToString(), searchModel.Type == NewsType.Siasi));
            typeSelector.Add(new SelectListItem("اقتصادی", NewsType.Eghtesadi.ToString(), searchModel.Type == NewsType.Eghtesadi));
            typeSelector.Add(new SelectListItem("فرهنگی", NewsType.Farhangi.ToString(), searchModel.Type == NewsType.Farhangi));


            ViewBag.PageSizeSelector = pageSizeSelector;
            ViewBag.TypeSelector = typeSelector;

            return View(new SearchModel<NewsSearchViewModel, PaginationViewModel<NewsViewModel>>(searchModel, data.ToVewModel()));

        }

        [AccessCodeFlter(AccessCode.CreateNews)]
        public IActionResult Create()
        {
            List<SelectListItem> typeSelector = new List<SelectListItem>();
            typeSelector.Add(new SelectListItem("نوع خبر", ""));
            typeSelector.Add(new SelectListItem("اخبار محله", NewsType.Mahal.ToString()));
            typeSelector.Add(new SelectListItem("سیاسی", NewsType.Siasi.ToString()));
            typeSelector.Add(new SelectListItem("اقتصادی", NewsType.Eghtesadi.ToString()));
            typeSelector.Add(new SelectListItem("فرهنگی", NewsType.Farhangi.ToString()));

            ViewBag.TypeSelector = typeSelector;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.CreateNews)]
        public IActionResult Create(NewsCreateViewModel model)
        {
            var uploadResult = _fileService.Upload(model.PrimaryPicture, "News", 1024 * 500);

            var serviceResult = new ServiceResult();

            if (uploadResult.IsSuccess)
            {
                serviceResult = _adminService.CreateNews(model.ToDto(uploadResult.Data));

                if (serviceResult.IsSuccess)
                {
                    Swal(true, "یک خبر با موفقیت اضافه شد");
                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                serviceResult.Errors = uploadResult.Errors;
                serviceResult.IsSuccess = false;
            }

            AddErrors(serviceResult);

            List<SelectListItem> typeSelector = new List<SelectListItem>();
            typeSelector.Add(new SelectListItem("نوع خبر", ""));
            typeSelector.Add(new SelectListItem("اخبار محله", NewsType.Mahal.ToString(), model.Type == NewsType.Mahal));
            typeSelector.Add(new SelectListItem("سیاسی", NewsType.Siasi.ToString(), model.Type == NewsType.Siasi));
            typeSelector.Add(new SelectListItem("اقتصادی", NewsType.Eghtesadi.ToString(), model.Type == NewsType.Eghtesadi));
            typeSelector.Add(new SelectListItem("فرهنگی", NewsType.Farhangi.ToString(), model.Type == NewsType.Farhangi));

            ViewBag.TypeSelector = typeSelector;

            return View(model);
        }

        [AccessCodeFlter(AccessCode.EditNews)]
        public IActionResult Edit(int id)
        {
            var data = _adminService.GetNews(id);

            if (data == null)
            {
                Swal(false, "خبری با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> typeSelector = new List<SelectListItem>();
            typeSelector.Add(new SelectListItem("نوع خبر", ""));
            typeSelector.Add(new SelectListItem("اخبار محله", NewsType.Mahal.ToString(), data.Type == NewsType.Mahal));
            typeSelector.Add(new SelectListItem("سیاسی", NewsType.Siasi.ToString(), data.Type == NewsType.Siasi));
            typeSelector.Add(new SelectListItem("اقتصادی", NewsType.Eghtesadi.ToString(), data.Type == NewsType.Eghtesadi));
            typeSelector.Add(new SelectListItem("فرهنگی", NewsType.Farhangi.ToString(), data.Type == NewsType.Farhangi));

            ViewBag.TypeSelector = typeSelector;

            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.EditNews)]
        public IActionResult Edit(NewsEditViewModel model)
        {
            var uploadResult = new ServiceResult<string>();
            var serviceResult = new ServiceResult<string>();

            if (model.PrimaryPicture != null)
                uploadResult = _fileService.Upload(model.PrimaryPicture, "News", 1024 * 500);

            if (!uploadResult.IsSuccess && model.PrimaryPicture != null)
            {
                Swal(false, uploadResult.Errors.FirstOrDefault());
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            serviceResult = _adminService.EditNews(model.ToDto(uploadResult.Data));

            if (serviceResult.IsSuccess)
            {
                // delete file
                if (!string.IsNullOrEmpty(serviceResult.Data))
                    _fileService.Delete(serviceResult.Data, "Post");

                Swal(true, "خبر با موفقیت ویرایش شد");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            AddErrors(serviceResult);

            var data = _adminService.GetNews(model.Id);

            List<SelectListItem> typeSelector = new List<SelectListItem>();
            typeSelector.Add(new SelectListItem("نوع خبر", ""));
            typeSelector.Add(new SelectListItem("اخبار محله", NewsType.Mahal.ToString(), data.Type == NewsType.Mahal));
            typeSelector.Add(new SelectListItem("سیاسی", NewsType.Siasi.ToString(), data.Type == NewsType.Siasi));
            typeSelector.Add(new SelectListItem("اقتصادی", NewsType.Eghtesadi.ToString(), data.Type == NewsType.Eghtesadi));
            typeSelector.Add(new SelectListItem("فرهنگی", NewsType.Farhangi.ToString(), data.Type == NewsType.Farhangi));

            ViewBag.TypeSelector = typeSelector;

            return View(data.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.DeleteNews)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteNews(id);
            if (serviceResult.IsSuccess)
            {
                var deleteResult = _fileService.Delete(serviceResult.Data, "News");
                Swal(true, "خبر با موفقیت حذف شد");
            }
            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }

        [AccessCodeFlter(AccessCode.NewsFileManagement)]
        public IActionResult File(int id)
        {
            var report = _adminService.GetNews(id);
            if (report == null)
            {
                Swal(false, "شناسه پست نامعتبر است");
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> fileTypeSelector = new List<SelectListItem>();
            fileTypeSelector.Add(new SelectListItem("", ""));
            fileTypeSelector.Add(new SelectListItem("عکس", Domain.Enumeration.FileType.Image.ToString()));
            fileTypeSelector.Add(new SelectListItem("صوتی", Domain.Enumeration.FileType.Audio.ToString()));
            fileTypeSelector.Add(new SelectListItem("تصویری", Domain.Enumeration.FileType.Video.ToString()));

            ViewBag.FileTypeSelector = fileTypeSelector;

            ViewBag.Files = _adminService.GetAllNewsFiles(id).ToViewModel();

            return View(report.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.NewsFileManagement)]
        public IActionResult File(NewsFileCreateViewModel model)
        {
            if (model.File == null)
                Swal(false, "فایلی انتخاب نکرده اید");
            else
            {
                long? maxLength = 0;

                if (model.FileType == Domain.Enumeration.FileType.Image)
                    maxLength = 500 * 1024;
                else maxLength = 25 * 1024 * 1024;

                var uploadResult = _fileService.Upload(model.File, "NewsFile", maxLength);

                if (uploadResult.IsSuccess)
                {
                    var serviceResult = _adminService.CreateNewsFile(model.ToDto(uploadResult.Data, model.File.Length));
                    if (serviceResult.IsSuccess)
                        Swal(true, "عملیات با موفقیت صورت گرفت");
                    else Swal(false, serviceResult.Errors.FirstOrDefault());
                }
                else
                    Swal(false, uploadResult.Errors.FirstOrDefault());
            }
            return RedirectToAction(nameof(File), new { id = model.NewsId });
        }

        [AccessCodeFlter(AccessCode.NewsFileManagement)]
        public IActionResult DeleteFile(int id)
        {
            var newsFile = _adminService.GetNewsFile(id);

            if (newsFile == null)
                return RedirectToAction(nameof(Index));

            var deleteResult = _fileService.Delete(newsFile.FileName, "NewsFile");

            if (deleteResult.IsSuccess)
            {
                var serviceResult = _adminService.DeleteNewsFile(id);
                if (serviceResult.IsSuccess)
                    Swal(true, "عملیات با موفقیت انجام شد");
                else Swal(false, serviceResult.Errors.FirstOrDefault());
            }

            return RedirectToAction(nameof(File), new { id = newsFile.NewsId });
        }
    }
}