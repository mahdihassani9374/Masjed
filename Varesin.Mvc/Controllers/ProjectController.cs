using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Project;
using Varesin.Services;

namespace Varesin.Mvc.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly UserService _userService;
        public ProjectController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(ProjectUserSearchViewModel searchModel)
        {
            var data = _userService.GetProjects(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("12", "12", searchModel.PageSize == 12));
            pageSizeSelector.Add(new SelectListItem("24", "24", searchModel.PageSize == 24));
            pageSizeSelector.Add(new SelectListItem("36", "36", searchModel.PageSize == 36));
            pageSizeSelector.Add(new SelectListItem("48", "48", searchModel.PageSize == 48));
            pageSizeSelector.Add(new SelectListItem("60", "60", searchModel.PageSize == 60));

            var projectTypes = _userService.GetAllProjectTypes();

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

            var vm = data.ToViewModel();
            vm.Data.SetImage();

            return View(new SearchModel<ProjectUserSearchViewModel, PaginationViewModel<ProjectViewModel>>(searchModel, vm));
        }
    }
}