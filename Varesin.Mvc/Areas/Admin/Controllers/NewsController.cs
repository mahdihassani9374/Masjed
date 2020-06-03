using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Report;
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
            pageSizeSelector.Add(new SelectListItem("", ""));
            pageSizeSelector.Add(new SelectListItem("اخبار محله", NewsType.Mahal.ToString(), searchModel.Type == NewsType.Mahal));
            pageSizeSelector.Add(new SelectListItem("سیاسی", NewsType.Siasi.ToString(), searchModel.Type == NewsType.Siasi));
            pageSizeSelector.Add(new SelectListItem("اقتصادی", NewsType.Eghtesadi.ToString(), searchModel.Type == NewsType.Eghtesadi));
            pageSizeSelector.Add(new SelectListItem("فرهنگی", NewsType.Farhangi.ToString(), searchModel.Type == NewsType.Farhangi));


            ViewBag.PageSizeSelector = pageSizeSelector;
            ViewBag.TypeSelector = typeSelector;

            return View(new SearchModel<NewsSearchViewModel, PaginationViewModel<NewsViewModel>>(searchModel, data.ToVewModel()));

        }

        [AccessCodeFlter(AccessCode.CreateNews)]
        public IActionResult Create()
        {
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

            ViewBag.Files = _adminService.GetAllPostFiles(id).ToViewModel();

            return View(report.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.NewsFileManagement)]
        public IActionResult File(ReportFileCreateViewModel model)
        {
            if (model.File == null)
                Swal(false, "فایلی انتخاب نکرده اید");
            else
            {
                long? maxLength = 0;

                if (model.FileType == Domain.Enumeration.FileType.Image)
                    maxLength = 500 * 1024;
                else maxLength = 25 * 1024 * 1024;

                var uploadResult = _fileService.Upload(model.File, "ReportFile", maxLength);

                if (uploadResult.IsSuccess)
                {
                    var serviceResult = _adminService.CreateReportFile(model.ToDto(uploadResult.Data, model.File.Length));
                    if (serviceResult.IsSuccess)
                        Swal(true, "عملیات با موفقیت صورت گرفت");
                    else Swal(false, serviceResult.Errors.FirstOrDefault());
                }
                else
                    Swal(false, uploadResult.Errors.FirstOrDefault());
            }
            return RedirectToAction(nameof(File), new { id = model.ReportId });
        }

        [AccessCodeFlter(AccessCode.NewsFileManagement)]
        public IActionResult DeleteFile(int id)
        {
            var postFile = _adminService.GetPostFile(id);

            if (postFile == null)
                return RedirectToAction(nameof(Index));

            var deleteResult = _fileService.Delete(postFile.FileName, "PostFile");

            if (deleteResult.IsSuccess)
            {
                var serviceResult = _adminService.DeleteReportFile(id);
                if (serviceResult.IsSuccess)
                    Swal(true, "عملیات با موفقیت انجام شد");
                else Swal(false, serviceResult.Errors.FirstOrDefault());
            }

            Swal(false, "در حذف فایل خطایی رخ داد");

            return RedirectToAction(nameof(File), new { id = postFile.Id });
        }

        [AccessCodeFlter(AccessCode.InstagramSharing)]
        public IActionResult Instagram(int id)
        {
            var report = _adminService.GetReport(id);

            if (report == null)
            {
                Swal(false, "شناسه گزارش نامعتبر است");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Tags = _adminService.GetAllInstaTags().Select(c => c.Name).ToList();
            ViewBag.Files = _adminService.GetAllReportFiles(id).Where(c => c.Type != FileType.Audio).ToList().ToViewModel();
            return View();
        }
    }
}