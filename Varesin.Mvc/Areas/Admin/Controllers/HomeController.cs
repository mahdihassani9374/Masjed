using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{

    public class HomeController : BaseController
    {
        private readonly AdminService _adminService;
        public HomeController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            ViewBag.CountViewToday = _adminService.CountViewToday().ToPersianNumbers();
            ViewBag.CountViewLastWeek = _adminService.CountViewLastWeek().ToPersianNumbers();
            ViewBag.CountViewTwoLastWeek = _adminService.CountViewTwoLastWeek().ToPersianNumbers();
            ViewBag.CountViewLastMonth = _adminService.CountViewLastMonth().ToPersianNumbers();
            return View();
        }
    }
}