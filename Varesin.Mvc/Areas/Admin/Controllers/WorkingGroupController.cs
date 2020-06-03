using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.WorkingGroup;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class WorkingGroupController : BaseController
    {
        private readonly AdminService _adminService;
        public WorkingGroupController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [AccessCodeFlter(AccessCode.ViewWorkingGroup)]
        public IActionResult Index()
        {
            var model = _adminService.GetAllWorkingGroup().ToViewModel();
            return View(model);
        }

        [AccessCodeFlter(AccessCode.CreateWorkingGroup)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.CreateWorkingGroup)]
        public IActionResult Create(WorkingGroupCreateViewModel model)
        {
            var serviceResult = _adminService.CreateWorkingGroup(model.ToDto());

            if (serviceResult.IsSuccess)
            {
                Swal(true, "یک کارگروه با موفقیت اضافه شد");
                return RedirectToAction(nameof(Create));
            }
            AddErrors(serviceResult);
            return View(model);
        }

        [AccessCodeFlter(AccessCode.EditWorkingGroup)]
        public IActionResult Edit(int id)
        {
            var data = _adminService.GetWorkingGroup(id);
            if (data == null)
            {
                Swal(false, "کارگروهی با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }
            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.EditWorkingGroup)]
        public IActionResult Edit(WorkingGroupViewModel model)
        {
            var serviceResult = _adminService.EditWorkingGroup(model.ToDto());
            if (serviceResult.IsSuccess)
            {
                Swal(true, "کارگروه با موفقیت ویرایش شد");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            AddErrors(serviceResult);
            return View(model);
        }

        [AccessCodeFlter(AccessCode.DeleteWorkingGroup)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteWorkingGroup(id);
            if (serviceResult.IsSuccess)
                Swal(true, "کارگروه با موفقیت حذف شد");
            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}