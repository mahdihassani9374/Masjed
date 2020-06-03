using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Varesin.Domain.Entities;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.Info;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class InfoController : BaseController
    {
        private readonly AdminService _adminService;
        public InfoController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [AccessCodeFlter(AccessCode.ViewAndManageInfo)]
        public IActionResult Index()
        {
            var infoes = _adminService.GetAllInfoes();
            return View(infoes.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.ViewAndManageInfo)]
        public IActionResult Index(InfoViewModel model)
        {
            var serviceResult = _adminService.CreateInfo(model.ToEntity());
            if (serviceResult.IsSuccess)
                Swal(true, "عملیات با موفقیت انجام شد");
            else Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}