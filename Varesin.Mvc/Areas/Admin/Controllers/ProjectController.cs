using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Project;
using Varesin.Mvc.Models.Report;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly AdminService _adminService;
        public ProjectController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [AccessCodeFlter(AccessCode.ViewProject)]
        public IActionResult Index(ProjectSearchViewModel searchModel)
        {
            var data = _adminService.GetProjects(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            var projectTypes = _adminService.GetAllProjectTypes();

            List<SelectListItem> projectTypeSelector = new List<SelectListItem>();

            projectTypeSelector.Add(new SelectListItem("همه", ""));

            foreach (var type in projectTypes)
                projectTypeSelector.Add(new SelectListItem(type.Title, type.Id.ToString(), searchModel.TypeId == type.Id));

            List<SelectListItem> projectStateSelector = new List<SelectListItem>();

            projectStateSelector.Add(new SelectListItem("همه", ""));
            projectStateSelector.Add(new SelectListItem("در حال انجام", ProjectState.Doing.ToString(), searchModel.State == ProjectState.Doing));
            projectStateSelector.Add(new SelectListItem("اتمام", ProjectState.End.ToString(), searchModel.State == ProjectState.End));
            projectStateSelector.Add(new SelectListItem("در حال تأمین اعتبار", ProjectState.Funding.ToString(), searchModel.State == ProjectState.Funding));

            ViewBag.ProjectTypeSelector = projectTypeSelector;
            ViewBag.ProjectStateSelector = projectStateSelector;
            ViewBag.PageSizeSelector = pageSizeSelector;

            return View(new SearchModel<ProjectSearchViewModel, PaginationViewModel<ProjectViewModel>>(searchModel, data.ToViewModel()));
        }

        [AccessCodeFlter(AccessCode.CreateProject)]
        public IActionResult Create()
        {
            var projectTypes = _adminService.GetAllProjectTypes();

            List<SelectListItem> projectTypeSelector = new List<SelectListItem>();

            projectTypeSelector.Add(new SelectListItem("نوع پروژه", ""));

            foreach (var type in projectTypes)
                projectTypeSelector.Add(new SelectListItem(type.Title, type.Id.ToString()));

            List<SelectListItem> projectStateSelector = new List<SelectListItem>();

            projectStateSelector.Add(new SelectListItem("وضعیت پروژه", ""));
            projectStateSelector.Add(new SelectListItem("در حال انجام", ProjectState.Doing.ToString()));
            projectStateSelector.Add(new SelectListItem("اتمام", ProjectState.End.ToString()));
            projectStateSelector.Add(new SelectListItem("در حال تأمین اعتبار", ProjectState.Funding.ToString()));

            ViewBag.ProjectTypeSelector = projectTypeSelector;
            ViewBag.ProjectStateSelector = projectStateSelector;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.CreateProject)]
        public IActionResult Create(ProjectCreateViewModel model)
        {
            var serviceResult = _adminService.CreateProject(model.ToDto());

            if (serviceResult.IsSuccess)
            {
                Swal(true, "یک پروژه با موفقیت اضاقه شد");
                return RedirectToAction(nameof(Create));
            }

            var projectTypes = _adminService.GetAllProjectTypes();

            List<SelectListItem> projectTypeSelector = new List<SelectListItem>();

            projectTypeSelector.Add(new SelectListItem("نوع پروژه", ""));

            foreach (var type in projectTypes)
                projectTypeSelector.Add(new SelectListItem(type.Title, type.Id.ToString(), model.TypeId == type.Id));

            List<SelectListItem> projectStateSelector = new List<SelectListItem>();

            projectStateSelector.Add(new SelectListItem("وضعیت پروژه", ""));
            projectStateSelector.Add(new SelectListItem("در حال انجام", ProjectState.Doing.ToString(), model.State == ProjectState.Doing));
            projectStateSelector.Add(new SelectListItem("اتمام", ProjectState.End.ToString(), model.State == ProjectState.End));
            projectStateSelector.Add(new SelectListItem("در حال تأمین اعتبار", ProjectState.Funding.ToString(), model.State == ProjectState.Funding));

            ViewBag.ProjectTypeSelector = projectTypeSelector;
            ViewBag.ProjectStateSelector = projectStateSelector;

            AddErrors(serviceResult);

            return View(model);
        }

        [AccessCodeFlter(AccessCode.EditProject)]
        public IActionResult Edit(int id)
        {
            var data = _adminService.GetProject(id);

            if (data == null)
            {
                Swal(false, "پروژه ای با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }

            var projectTypes = _adminService.GetAllProjectTypes();

            List<SelectListItem> projectTypeSelector = new List<SelectListItem>();

            projectTypeSelector.Add(new SelectListItem("نوع پروژه", ""));

            foreach (var type in projectTypes)
                projectTypeSelector.Add(new SelectListItem(type.Title, type.Id.ToString(), type.Id == data.TypeId));

            List<SelectListItem> projectStateSelector = new List<SelectListItem>();

            projectStateSelector.Add(new SelectListItem("وضعیت پروژه", ""));
            projectStateSelector.Add(new SelectListItem("در حال انجام", ProjectState.Doing.ToString(), data.State == ProjectState.Doing));
            projectStateSelector.Add(new SelectListItem("اتمام", ProjectState.End.ToString(), data.State == ProjectState.End));
            projectStateSelector.Add(new SelectListItem("در حال تأمین اعتبار", ProjectState.Funding.ToString(), data.State == ProjectState.Funding));

            ViewBag.ProjectTypeSelector = projectTypeSelector;
            ViewBag.ProjectStateSelector = projectStateSelector;

            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.EditProject)]
        public IActionResult Edit(ProjectViewModel model)
        {
            var serviceResult = _adminService.EditProject(model.ToDto());

            if (serviceResult.IsSuccess)
            {
                Swal(true, "پروژه با موفقیت ویرایش شد");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            var projectTypes = _adminService.GetAllProjectTypes();

            List<SelectListItem> projectTypeSelector = new List<SelectListItem>();

            projectTypeSelector.Add(new SelectListItem("نوع پروژه", ""));

            foreach (var type in projectTypes)
                projectTypeSelector.Add(new SelectListItem(type.Title, type.Id.ToString(), type.Id == model.TypeId));

            List<SelectListItem> projectStateSelector = new List<SelectListItem>();

            projectStateSelector.Add(new SelectListItem("وضعیت پروژه", ""));
            projectStateSelector.Add(new SelectListItem("در حال انجام", ProjectState.Doing.ToString(), model.State == ProjectState.Doing));
            projectStateSelector.Add(new SelectListItem("اتمام", ProjectState.End.ToString(), model.State == ProjectState.End));
            projectStateSelector.Add(new SelectListItem("در حال تأمین اعتبار", ProjectState.Funding.ToString(), model.State == ProjectState.Funding));

            ViewBag.ProjectTypeSelector = projectTypeSelector;
            ViewBag.ProjectStateSelector = projectStateSelector;

            return View(model);
        }

        [AccessCodeFlter(AccessCode.DeleteProject)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteProject(id);
            if (serviceResult.IsSuccess)
                Swal(true, "پروژه با موفقیت حذف شد");
            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }

        [AccessCodeFlter(AccessCode.AttachReport)]
        public IActionResult Report(int id, ReportSearchViewModel searchModel)
        {
            var project = _adminService.GetProject(id);
            if (project == null)
            {
                Swal(false, "پروژه ای با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }

            var reports = _adminService.GetReports(searchModel.ToDto());

            var workingGroups = _adminService.GetAllWorkingGroup();

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            List<SelectListItem> workingGroupSelector = new List<SelectListItem>();

            workingGroupSelector.Add(new SelectListItem("همه", ""));

            foreach (var workingGroup in workingGroups)
                workingGroupSelector.Add(new SelectListItem(workingGroup.Title, workingGroup.Id.ToString(), searchModel.WorkingGroupId == workingGroup.Id));

            ViewBag.PageSizeSelector = pageSizeSelector;
            ViewBag.WorkingGroupSelector = workingGroupSelector;

            ViewBag.Project = project.ToViewModel();

            return View(new SearchModel<ReportSearchViewModel, PaginationViewModel<ReportViewModel>>(searchModel, reports.ToVewModel()));
        }

        [AccessCodeFlter(AccessCode.AttachReport)]
        public IActionResult SelectReport(int id, int reportId)
        {
            var serviceResult = _adminService.AttachReportToProject(id, reportId);
            if (serviceResult.IsSuccess)
                Swal(true, "عملیات با موفقیت انجام شد");
            else Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Report), new { id = id });
        }

        [AccessCodeFlter(AccessCode.ViewProjectPayment)]
        public IActionResult Payment(int id)
        {
            var project = _adminService.GetProject(id);

            if (project == null)
            {
                Swal(false, "پروژه ای با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }

            var payments = _adminService.GetPayment(id, PaymentType.Project);

            ViewBag.Project = project.ToViewModel();

            return View(payments.ToViewModel());
        }
    }
}