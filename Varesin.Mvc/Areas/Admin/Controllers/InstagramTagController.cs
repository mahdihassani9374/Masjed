using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class InstagramTagController : BaseController
    {
        private readonly AdminService _adminService;
        public InstagramTagController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [AccessCodeFlter(AccessCode.InstagramTagManagement)]
        public IActionResult Index()
        {
            var data = _adminService.GetAllInstaTags();
            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.InstagramTagManagement)]
        public IActionResult Index(string name)
        {
            var serviceResult = _adminService.CreateInstagramTag(name);
            if (serviceResult.IsSuccess)
                Swal(true, "عملیات با موفقیت صورت گرفت");
            else Swal(false, serviceResult.Errors.FirstOrDefault());

            return RedirectToAction(nameof(Index));
        }

        [AccessCodeFlter(AccessCode.InstagramTagManagement)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteInstagramTag(id);
            if (serviceResult.IsSuccess)
                Swal(true, "عملیات با موفقیت صورت گرفت");
            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}