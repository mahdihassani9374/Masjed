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
using Varesin.Mvc.Models.ContactUs;
using Varesin.Mvc.Models.Pagination;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class ContactUsController : BaseController
    {
        private readonly AdminService _adminService;
        public ContactUsController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [AccessCodeFlter(AccessCode.ViewContactUs)]
        public IActionResult Index(ContactUsSearchViewModel searchModel)
        {
            var data = _adminService.GetContactUs(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            ViewBag.PageSizeSelector = pageSizeSelector;

            return View(new SearchModel<ContactUsSearchViewModel, PaginationViewModel<ContactUsViewModel>>(searchModel, data.ToViewModel()));
        }
    }
}