using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.SlideShow;
using Varesin.Mvc.Services;
using Varesin.Services;
using Varesin.Utility;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class SlideShowController : BaseController
    {
        private readonly AdminService _adminService;
        private readonly FileService _fileService;
        public SlideShowController(AdminService adminService,
            FileService fileService)
        {
            _adminService = adminService;
            _fileService = fileService;
        }

        [AccessCodeFlter(AccessCode.ViewSlideShow)]
        public IActionResult Index()
        {
            var data = _adminService.GetAllSlideShow();

            return View(data.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.CreateSlideShow)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.CreateSlideShow)]
        public IActionResult Create(SlideShowCreateViewModel model)
        {
            var uploadResult = _fileService.Upload(model.File, "SlideShow", 1024 * 500);

            var serviceResult = new ServiceResult();

            if (uploadResult.IsSuccess)
            {
                serviceResult = _adminService.CreateSlideShow(model.ToDto(uploadResult.Data, model.File.Length));

                if (serviceResult.IsSuccess)
                {
                    Swal(true, "یک اسلایدشو با موفقیت اضافه شد");
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

        [AccessCodeFlter(AccessCode.EditSlideShow)]
        public IActionResult Edit(int id)
        {
            var data = _adminService.GetSlideShow(id);

            if (data == null)
            {
                Swal(false, "اسلایدشویی با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }

            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.EditSlideShow)]
        public IActionResult Edit(SlideShowEditViewModel model)
        {
            var uploadResult = new ServiceResult<string>();
            var serviceResult = new ServiceResult<string>();

            if (model.File != null)
                uploadResult = _fileService.Upload(model.File, "SlideShow", 1024 * 500);

            if (!uploadResult.IsSuccess && model.File != null)
            {
                Swal(false, uploadResult.Errors.FirstOrDefault());
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            serviceResult = _adminService.EditSlideShow(model.ToDto(uploadResult.Data, model.File?.Length ?? 0));

            if (serviceResult.IsSuccess)
            {
                // delete file
                if (!string.IsNullOrEmpty(serviceResult.Data))
                    _fileService.Delete(serviceResult.Data, "SlideShow");

                Swal(true, "اسلایدشو با موفقیت ویرایش شد");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            AddErrors(serviceResult);

            var data = _adminService.GetSlideShow(model.Id);

            return View(data.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.DeleteSlideShow)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteSlideShow(id);
            if (serviceResult.IsSuccess)
            {
                var deleteResult = _fileService.Delete(serviceResult.Data, "SlideShow");
                Swal(true, "اسلایدشو با موفقیت حذف شد");
            }

            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}