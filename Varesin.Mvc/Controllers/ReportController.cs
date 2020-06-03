using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Report;
using Varesin.Services;

namespace Varesin.Mvc.Controllers
{
    public class ReportController : BaseController
    {
        private readonly UserService _userService;
        public ReportController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(ReportUserSearchViewModel searchModel)
        {
            var data = _userService.GetReports(searchModel.ToDto());


            var workingGroups = _userService.GetAllWorkingGroup();

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("12", "12", searchModel.PageSize == 12));
            pageSizeSelector.Add(new SelectListItem("24", "24", searchModel.PageSize == 24));
            pageSizeSelector.Add(new SelectListItem("36", "36", searchModel.PageSize == 36));
            pageSizeSelector.Add(new SelectListItem("48", "48", searchModel.PageSize == 48));
            pageSizeSelector.Add(new SelectListItem("60", "60", searchModel.PageSize == 60));

            List<SelectListItem> workingGroupSelector = new List<SelectListItem>();

            workingGroupSelector.Add(new SelectListItem("همه", ""));

            foreach (var workingGroup in workingGroups)
                workingGroupSelector.Add(new SelectListItem(workingGroup.Title, workingGroup.Id.ToString(), searchModel.WorkingGroupId == workingGroup.Id));

            ViewBag.PageSizeSelector = pageSizeSelector;
            ViewBag.WorkingGroupSelector = workingGroupSelector;

            return View(new SearchModel<ReportUserSearchViewModel, PaginationViewModel<ReportViewModel>>(searchModel, data.ToVewModel()));
        }
        public IActionResult Detail(int id)
        {
            var report = _userService.GetReport(id);

            if (report == null)
                return RedirectPermanent("/");

            var files = _userService.GetAllReportFiles(id);

            ViewBag.Files = files.ToViewModel();

            var lastReports = _userService.GetLastReports(10);

            ViewBag.LastReports = lastReports.ToViewModel();

            return View(report.ToViewModel());
        }
    }
}