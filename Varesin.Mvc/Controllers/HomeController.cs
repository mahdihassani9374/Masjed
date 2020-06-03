﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var lastProjects = _userService.GetLastProjects(7);
            var lastReports = _userService.GetLastReports(8);

            ViewBag.SlideShows = slideShows.ToViewModel();
            ViewBag.Projects = lastProjects.ToViewModel().SetImage();
            ViewBag.Reports = lastReports.ToViewModel();

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