using Microsoft.AspNetCore.Mvc;
using Varesin.Mvc.Models.Event;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class EventController : BaseController
    {
        private readonly AdminService _adminService;
        public EventController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(EventCreateViewModel model)
        {
            return View(model);
        }
    }
}