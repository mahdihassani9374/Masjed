using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Varesin.Services;

namespace Varesin.Mvc.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly UserService _userService;
        public PaymentController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public IActionResult Index(int id, bool isSuc)
        {
            if (isSuc)
                _userService.SuccessPay(id);
            else _userService.ErroPay(id);
            return View();
        }

        public IActionResult Result()
        {
            return View();
        }
    }
}