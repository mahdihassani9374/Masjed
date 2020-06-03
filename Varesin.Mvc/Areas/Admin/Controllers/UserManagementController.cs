using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Varesin.Database.Identity.Entities;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.User;
using Varesin.Services;
using DNTPersianUtils.Core;
using Varesin.Mvc.Services;
using System.Linq;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Services.Mapping;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class UserManagementController : BaseController
    {

        private readonly AdminService _adminService;
        public UserManagementController(UserManager<User> userManager,
            AdminService adminService)
        {
            _adminService = adminService;
        }

        [AccessCodeFlter(AccessCode.ViewUser)]
        public IActionResult Index(UserSearchViewModel searchModel)
        {
            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            ViewBag.PageSizeSelector = pageSizeSelector;

            List<SelectListItem> orderTypeSelector = new List<SelectListItem>();
            orderTypeSelector.Add(new SelectListItem("نزولی", OrderType.Ascending.ToString(), searchModel.OrderType == OrderType.Ascending));
            orderTypeSelector.Add(new SelectListItem("صعودی", OrderType.Desending.ToString(), searchModel.OrderType == OrderType.Desending));

            ViewBag.OrderTypeSelector = orderTypeSelector;

            List<SelectListItem> userSearchType = new List<SelectListItem>();
            userSearchType.Add(new SelectListItem("نام و نام خانوادگی", UserSearchType.FullName.ToString(), searchModel.SearchType == UserSearchType.FullName));
            userSearchType.Add(new SelectListItem("زمان ثبت نام", UserSearchType.RegisterDate.ToString(), searchModel.SearchType == UserSearchType.RegisterDate));

            ViewBag.UserSearchType = userSearchType;

            var data = _adminService.GetUsers(searchModel.ToDto());

            return View(new SearchModel<UserSearchViewModel, PaginationViewModel<UserViewModel>>(searchModel, data.ToVewModel()));
        }

        [AccessCodeFlter(AccessCode.CreateUser)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AccessCodeFlter(AccessCode.CreateUser)]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            var serviceResult = await _adminService.CreateUser(model.ToDto());
            if (serviceResult.IsSuccess)
            {
                Swal(true, "یک کاربر با موفقیت اضافه شد");
                return RedirectToAction(nameof(Create));
            }
            AddErrors(serviceResult);
            return View(model);
        }

        [AccessCodeFlter(AccessCode.AccessManagement)]
        public async Task<IActionResult> Access(string id)
        {
            var user = _adminService.GetUserEntity(id);
            ViewBag.User = user?.ToDto()?.ToViewModel();
            var model = new List<UserAccessGroupingModel>();
            if (user == null)
            {
                Swal(false, "کاربری یافت نشد");
                return RedirectToAction(nameof(Index));
            }
            var roles = await _adminService.GetRoles(user);
            model = UserAccessService.GetGroupingAccess(roles.ToList());
            return View(model);
        }

        [HttpPost]
        [AccessCodeFlter(AccessCode.AccessManagement)]
        public async Task<IActionResult> Access(string userId, List<int> ids)
        {
            var user = _adminService.GetUserEntity(userId);

            if (user == null)
            {
                Swal(false, "شناسه کاربر نامعتبر می باشد");
                return RedirectToAction(nameof(Index));
            }

            var allRoles = Enum.GetValues(typeof(AccessCode)).Cast<AccessCode>().ToList();

            var selectRoles = allRoles.Where(c => ids.Any(i => i == (int)c)).ToList();

            var userRoles = await _adminService.GetRoles(user);

            var deletedRoles = userRoles.Where(c => !selectRoles.Any(i => i.ToString() == c)).ToList();

            var addedRoles = selectRoles.Where(c => !userRoles.Any(i => i == c.ToString())).ToList();

            var deeteRoleResult = await _adminService.RemoveRoles(user, deletedRoles);

            var addRoleResult = await _adminService.AddRoles(user, addedRoles.Select(c => c.ToString()).ToList());

            Swal(true, "عملیات با موفقیت صورت گرفت");

            return RedirectToAction(nameof(Access), new { id = userId });
        }
    }
}