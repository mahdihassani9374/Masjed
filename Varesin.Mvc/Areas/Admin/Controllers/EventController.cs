using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Extensions;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Event;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Services;
using Varesin.Services;
using Varesin.Utility;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class EventController : BaseController
    {
        private readonly AdminService _adminService;
        private readonly FileService _fileService;
        public EventController(AdminService adminService,
            FileService fileService)
        {
            _adminService = adminService;
            _fileService = fileService;
        }

        [AccessCodeFlter(AccessCode.ViewEvent)]
        public IActionResult Index(EventSearchViewModel searchModel)
        {
            var data = _adminService.GetEvent(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            ViewBag.PageSizeSelector = pageSizeSelector;

            return View(new SearchModel<EventSearchViewModel, PaginationViewModel<EventViewModel>>(searchModel, data.ToVewModel()));

        }

        [AccessCodeFlter(AccessCode.CreateEvent)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.CreateEvent)]
        public IActionResult Create(EventCreateViewModel model)
        {
            var uploadResult = _fileService.Upload(model.PrimaryPicture, "Event", 1024 * 500);

            var serviceResult = new ServiceResult();

            if (uploadResult.IsSuccess)
            {
                serviceResult = _adminService.CreateEvent(model.ToDto(uploadResult.Data));

                if (serviceResult.IsSuccess)
                {
                    Swal(true, "یک برنامه با موفقیت اضافه شد");
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

        [AccessCodeFlter(AccessCode.EditEvent)]
        public IActionResult Edit(int id)
        {
            var data = _adminService.GetEvent(id);

            if (data == null)
            {
                Swal(false, "برنامه با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }

            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.EditEvent)]
        public IActionResult Edit(EventEditViewModel model)
        {
            var uploadResult = new ServiceResult<string>();
            var serviceResult = new ServiceResult<string>();

            if (model.PrimaryPicture != null)
                uploadResult = _fileService.Upload(model.PrimaryPicture, "Event", 1024 * 500);

            if (!uploadResult.IsSuccess && model.PrimaryPicture != null)
            {
                Swal(false, uploadResult.Errors.FirstOrDefault());
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            serviceResult = _adminService.EditEvent(model.ToDto(uploadResult.Data));

            if (serviceResult.IsSuccess)
            {
                // delete file
                if (!string.IsNullOrEmpty(serviceResult.Data))
                    _fileService.Delete(serviceResult.Data, "Event");

                Swal(true, "برنامه با موفقیت ویرایش شد");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            AddErrors(serviceResult);

            var data = _adminService.GetEvent(model.Id);

            return View(data.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.DeleteEvent)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteEvent(id);
            if (serviceResult.IsSuccess)
            {
                var deleteResult = _fileService.Delete(serviceResult.Data, "Event");
                Swal(true, "برنامه با موفقیت حذف شد");
            }
            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }

        [AccessCodeFlter(AccessCode.EventFileManagement)]
        public IActionResult File(int id)
        {
            var eventEntity = _adminService.GetEvent(id);
            if (eventEntity == null)
            {
                Swal(false, "شناسه برنامه نامعتبر است");
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> fileTypeSelector = new List<SelectListItem>();
            fileTypeSelector.Add(new SelectListItem("", ""));
            fileTypeSelector.Add(new SelectListItem("عکس", Domain.Enumeration.FileType.Image.ToString()));
            fileTypeSelector.Add(new SelectListItem("صوتی", Domain.Enumeration.FileType.Audio.ToString()));
            fileTypeSelector.Add(new SelectListItem("تصویری", Domain.Enumeration.FileType.Video.ToString()));

            ViewBag.FileTypeSelector = fileTypeSelector;

            ViewBag.Files = _adminService.GetAllEventFiles(id).ToViewModel();

            return View(eventEntity.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.EventFileManagement)]
        public IActionResult File(EventFileCreateViewModel model)
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
                    var serviceResult = _adminService.CreateEventFile(model.ToDto(uploadResult.Data, model.File.Length));
                    if (serviceResult.IsSuccess)
                        Swal(true, "عملیات با موفقیت صورت گرفت");
                    else Swal(false, serviceResult.Errors.FirstOrDefault());
                }
                else
                    Swal(false, uploadResult.Errors.FirstOrDefault());
            }
            return RedirectToAction(nameof(File), new { id = model.EventId });
        }

        [AccessCodeFlter(AccessCode.EventFileManagement)]
        public IActionResult DeleteFile(int id)
        {
            var eventFile = _adminService.GetEventFile(id);

            if (eventFile == null)
                return RedirectToAction(nameof(Index));

            var deleteResult = _fileService.Delete(eventFile.FileName, "EventFile");

            if (deleteResult.IsSuccess)
            {
                var serviceResult = _adminService.DeleteEventFile(id);
                if (serviceResult.IsSuccess)
                    Swal(true, "عملیات با موفقیت انجام شد");
                else Swal(false, serviceResult.Errors.FirstOrDefault());
            }

            return RedirectToAction(nameof(File), new { id = eventFile.Id });
        }
    }
}