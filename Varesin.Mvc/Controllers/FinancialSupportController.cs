using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.Payment;
using Varesin.Services;

namespace Varesin.Mvc.Controllers
{
    public class FinancialSupportController : BaseController
    {
        private readonly UserService _userService;
        public FinancialSupportController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(int? id, PaymentType? type)
        {
            ViewBag.id = id;
            ViewBag.type = type;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Index(PaymentCreateViewModel model)
        {
            var serviceResult = _userService.CreatePayment(model.ToDto());

            if (serviceResult.IsSuccess)
            {
                // برو به بانک
                return RedirectPermanent($"/Payment/Index/{serviceResult.Data}");
            }

            ViewBag.id = model.RecordId;
            ViewBag.type = model.Type;

            AddErrors(serviceResult);

            return View(model);
        }
    }
}