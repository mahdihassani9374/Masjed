using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Varesin.Database;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.ContactUs;
using Varesin.Services;

namespace Varesin.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserService _userService;
        public HomeController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var slideShows = _userService.GetAllSlideShows();
            var nowEvents = _userService.GetAllNowEvent();
            var lastMahalNews = _userService.GetLastNews(6, true);
            var lastNews = _userService.GetLastNews(6, false);
            var lastPosts = _userService.GetLastPosts(6);
            var lastEvents = _userService.GetLastEvent(8);

            ViewBag.SlideShows = slideShows.ToViewModel();
            ViewBag.NowEvents = nowEvents;
            ViewBag.LastMahalNews = lastMahalNews.ToViewModel();
            ViewBag.LastNews = lastNews.ToViewModel();
            ViewBag.LastPosts = lastPosts.ToViewModel();
            ViewBag.LastEvents = lastEvents.ToViewModel();

            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUS()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult ContactUS(ContactUsCreateViewModel model)
        {
            var serviceResult = _userService.CreateContactUs(model.ToDto());

            if (serviceResult.IsSuccess)
            {
                Swal(true, "عملیات با موفقیت انجام شد");
                return RedirectToAction(nameof(ContactUS));
            }

            else Swal(false, serviceResult.Errors.FirstOrDefault());

            return View(model);
        }
    }
}