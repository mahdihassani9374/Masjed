﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
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
    }
}