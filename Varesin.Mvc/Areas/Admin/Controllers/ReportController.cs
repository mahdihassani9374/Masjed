using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Logger;
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
using System.Runtime.InteropServices.WindowsRuntime;
using InstagramApiSharp.API;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        private readonly AdminService _adminService;
        private readonly FileService _fileService;
        private readonly IHostingEnvironment _hostingEnvironment;


        public ReportController(AdminService adminService,
            FileService fileService,
            IHostingEnvironment hostingEnvironment)
        {
            _adminService = adminService;
            _fileService = fileService;
            _hostingEnvironment = hostingEnvironment;
        }

        [AccessCodeFlter(AccessCode.ViewReport)]
        public IActionResult Index(ReportSearchViewModel searchModel)
        {
            var data = _adminService.GetReports(searchModel.ToDto());


            var workingGroups = _adminService.GetAllWorkingGroup();

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            List<SelectListItem> workingGroupSelector = new List<SelectListItem>();

            workingGroupSelector.Add(new SelectListItem("همه", ""));

            foreach (var workingGroup in workingGroups)
                workingGroupSelector.Add(new SelectListItem(workingGroup.Title, workingGroup.Id.ToString(), searchModel.WorkingGroupId == workingGroup.Id));

            ViewBag.PageSizeSelector = pageSizeSelector;
            ViewBag.WorkingGroupSelector = workingGroupSelector;

            return View(new SearchModel<ReportSearchViewModel, PaginationViewModel<ReportViewModel>>(searchModel, data.ToVewModel()));

        }

        [AccessCodeFlter(AccessCode.CreateReport)]
        public IActionResult Create()
        {
            var workingGroups = _adminService.GetAllWorkingGroup();

            List<SelectListItem> workingGroupSelector = new List<SelectListItem>();

            workingGroupSelector.Add(new SelectListItem("", ""));

            foreach (var workingGroup in workingGroups)
                workingGroupSelector.Add(new SelectListItem(workingGroup.Title, workingGroup.Id.ToString()));

            ViewBag.WorkingGroupSelector = workingGroupSelector;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.CreateReport)]
        public IActionResult Create(ReportCreateViewModel model)
        {
            var uploadResult = _fileService.Upload(model.PrimaryPicture, "Report", 1024 * 500);

            var serviceResult = new ServiceResult();

            if (uploadResult.IsSuccess)
            {
                serviceResult = _adminService.CreateReport(model.ToDto(uploadResult.Data));

                if (serviceResult.IsSuccess)
                {
                    Swal(true, "یک گزارش با موفقیت اضافه شد");
                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                serviceResult.Errors = uploadResult.Errors;
                serviceResult.IsSuccess = false;
            }

            var workingGroups = _adminService.GetAllWorkingGroup();

            List<SelectListItem> workingGroupSelector = new List<SelectListItem>();

            workingGroupSelector.Add(new SelectListItem("", ""));

            foreach (var workingGroup in workingGroups)
                workingGroupSelector.Add(new SelectListItem(workingGroup.Title, workingGroup.Id.ToString(), model.WorkingGroupId == workingGroup.Id));

            ViewBag.WorkingGroupSelector = workingGroupSelector;

            AddErrors(serviceResult);

            return View(model);
        }

        [AccessCodeFlter(AccessCode.EditReport)]
        public IActionResult Edit(int id)
        {
            var data = _adminService.GetReport(id);

            if (data == null)
            {
                Swal(false, "گزارشی با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }

            var workingGroups = _adminService.GetAllWorkingGroup();

            List<SelectListItem> workingGroupSelector = new List<SelectListItem>();

            workingGroupSelector.Add(new SelectListItem("", ""));

            foreach (var workingGroup in workingGroups)
                workingGroupSelector.Add(new SelectListItem(workingGroup.Title, workingGroup.Id.ToString(), data.WorkingGroupId == workingGroup.Id));

            ViewBag.WorkingGroupSelector = workingGroupSelector;

            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.EditReport)]
        public IActionResult Edit(ReportEditViewModel model)
        {
            var uploadResult = new ServiceResult<string>();
            var serviceResult = new ServiceResult<string>();

            if (model.PrimaryPicture != null)
                uploadResult = _fileService.Upload(model.PrimaryPicture, "Report", 1024 * 500);

            if (!uploadResult.IsSuccess && model.PrimaryPicture != null)
            {
                Swal(false, uploadResult.Errors.FirstOrDefault());
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            serviceResult = _adminService.EditReport(model.ToDto(uploadResult.Data));

            if (serviceResult.IsSuccess)
            {
                // delete file
                if (!string.IsNullOrEmpty(serviceResult.Data))
                    _fileService.Delete(serviceResult.Data, "Report");

                Swal(true, "گزارش با موفقیت ویرایش شد");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            var workingGroups = _adminService.GetAllWorkingGroup();

            List<SelectListItem> workingGroupSelector = new List<SelectListItem>();

            workingGroupSelector.Add(new SelectListItem("", ""));

            foreach (var workingGroup in workingGroups)
                workingGroupSelector.Add(new SelectListItem(workingGroup.Title, workingGroup.Id.ToString(), model.WorkingGroupId == workingGroup.Id));

            ViewBag.WorkingGroupSelector = workingGroupSelector;

            AddErrors(serviceResult);

            var data = _adminService.GetReport(model.Id);

            return View(data.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.DeleteReport)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteReport(id);
            if (serviceResult.IsSuccess)
            {
                var deleteResult = _fileService.Delete(serviceResult.Data, "Report");
                Swal(true, "گزارش با موفقیت حذف شد");
            }
            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }

        [AccessCodeFlter(AccessCode.ReportFileManagement)]
        public IActionResult File(int id)
        {
            var report = _adminService.GetReport(id);
            if (report == null)
            {
                Swal(false, "شناسه گزارش نامعتبر است");
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> fileTypeSelector = new List<SelectListItem>();
            fileTypeSelector.Add(new SelectListItem("", ""));
            fileTypeSelector.Add(new SelectListItem("عکس", Domain.Enumeration.FileType.Image.ToString()));
            fileTypeSelector.Add(new SelectListItem("صوتی", Domain.Enumeration.FileType.Audio.ToString()));
            fileTypeSelector.Add(new SelectListItem("تصویری", Domain.Enumeration.FileType.Video.ToString()));

            ViewBag.FileTypeSelector = fileTypeSelector;

            ViewBag.Files = _adminService.GetAllReportFiles(id).ToViewModel();

            var files = _adminService.GetAllReportFiles(id).ToViewModel();

            return View(report.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.ReportFileManagement)]
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

        [AccessCodeFlter(AccessCode.ReportFileManagement)]
        public IActionResult DeleteFile(int id)
        {
            var reportFile = _adminService.GetReportFile(id);

            if (reportFile == null)
                return RedirectToAction(nameof(Index));

            var deleteResult = _fileService.Delete(reportFile.FileName, "ReportFile");

            if (deleteResult.IsSuccess)
            {
                var serviceResult = _adminService.DeleteReportFile(id);
                if (serviceResult.IsSuccess)
                    Swal(true, "عملیات با موفقیت انجام شد");
                else Swal(false, serviceResult.Errors.FirstOrDefault());
            }

            Swal(false, "در حذف فایل خطایی رخ داد");

            return RedirectToAction(nameof(File), new { id = reportFile.ReportId });
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