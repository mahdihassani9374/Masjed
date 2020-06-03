using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.Project;
using Varesin.Mvc.Models.WorkingGroup;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class ProjectTypeController : BaseController
    {
        private readonly AdminService _adminService;
        public ProjectTypeController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [AccessCodeFlter(AccessCode.ProjectTypeManagement)]
        public IActionResult Index()
        {
            var data = _adminService.GetAllProjectTypes();
            return View(data.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.ProjectTypeManagement)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.ProjectTypeManagement)]
        public IActionResult Create(ProjectTypeCreateViewModel model)
        {
            var serviceResult = _adminService.CreateProjectType(model.ToDto());

            if (serviceResult.IsSuccess)
            {
                Swal(true, "یک نوع پروژه با موفقیت اضافه شد");
                return RedirectToAction(nameof(Create));
            }
            AddErrors(serviceResult);
            return View(model);
        }

        [AccessCodeFlter(AccessCode.ProjectTypeManagement)]
        public IActionResult Edit(int id)
        {
            var data = _adminService.GetProjectType(id);
            if (data == null)
            {
                Swal(false, "شناسه ارسالی نامعتبر می باشد");
                return RedirectToAction(nameof(Index));
            }
            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.ProjectTypeManagement)]
        public IActionResult Edit(ProjectTypeViewModel model)
        {
            var serviceResult = _adminService.EditProjectType(model.ToDto());
            if (serviceResult.IsSuccess)
            {
                Swal(true, "نوع پروژه با موفقیت ویرایش شد");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            AddErrors(serviceResult);
            return View(model);
        }

        [AccessCodeFlter(AccessCode.ProjectTypeManagement)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteProjectType(id);
            if (serviceResult.IsSuccess)
                Swal(true, "نوع پروژه با موفقیت حذف شد");
            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}