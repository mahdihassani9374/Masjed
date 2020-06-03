using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Payment;
using Varesin.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly AdminService _adminService;
        public PaymentController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [AccessCodeFlter(AccessCode.ViewPayment)]
        public IActionResult Index(PaymentSearchViewModel searchModel)

        {
            var data = _adminService.GetPayments(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            List<SelectListItem> paymentStateSelector = new List<SelectListItem>();

            paymentStateSelector.Add(new SelectListItem("همه", ""));
            paymentStateSelector.Add(new SelectListItem("پرداخت شده", "True", searchModel.IsSuccess == true));
            paymentStateSelector.Add(new SelectListItem("پرداخت نشده", "False", searchModel.IsSuccess == false));


            List<SelectListItem> paymentTypeSelector = new List<SelectListItem>();
            paymentTypeSelector.Add(new SelectListItem("همه", ""));
            paymentTypeSelector.Add(new SelectListItem("عمومی", PaymentType.General.ToString(), searchModel.Type == PaymentType.General));
            paymentTypeSelector.Add(new SelectListItem("پروژه", PaymentType.Project.ToString(), searchModel.Type == PaymentType.Project));


            ViewBag.PageSizeSelector = pageSizeSelector;
            ViewBag.PaymentStateSelector = paymentStateSelector;
            ViewBag.PaymentTypeSelector = paymentTypeSelector;

            return View(new SearchModel<PaymentSearchViewModel, PaginationViewModel<PaymentViewModel>>(searchModel, data.ToViewModel()));
        }
    }
}
