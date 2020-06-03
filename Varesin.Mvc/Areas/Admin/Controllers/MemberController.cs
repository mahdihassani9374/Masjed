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
using Varesin.Mvc.Models.Member;
using Varesin.Mvc.Models.Pagination;
using Varesin.Services;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class MemberController : BaseController
    {
        private readonly AdminService _adminService;
        public MemberController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [AccessCodeFlter(AccessCode.PrimaryResponsibble)]
        public IActionResult Index(MemberSearchViewModel searchModel)
        {
            var data = _adminService.GetMembers(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            List<SelectListItem> statusSelector = new List<SelectListItem>();
            statusSelector.Add(new SelectListItem("همه", ""));
            statusSelector.Add(new SelectListItem("درحال انتظار", MemberStatus.Wating.ToString(), searchModel.Status == MemberStatus.Wating));
            statusSelector.Add(new SelectListItem("درحال مصاحبه", MemberStatus.InterViewing.ToString(), searchModel.Status == MemberStatus.InterViewing));
            statusSelector.Add(new SelectListItem("تایید شده", MemberStatus.Confirmed.ToString(), searchModel.Status == MemberStatus.Confirmed));
            statusSelector.Add(new SelectListItem("رد شده", MemberStatus.Rejected.ToString(), searchModel.Status == MemberStatus.Rejected));

            ViewBag.PageSizeSelector = pageSizeSelector;
            ViewBag.StatusSelector = statusSelector;

            return View(new SearchModel<MemberSearchViewModel, PaginationViewModel<MemberViewModel>>(searchModel, data.ToViewModel()));
        }

        [AccessCodeFlter(AccessCode.PrimaryResponsibble)]
        public async Task<IActionResult> Check(int id)
        {

            var model = _adminService.GetMember(id);

            if (model == null)
            {
                Swal(false, "شناسه عضو نامعتبر می باشد");
                return RedirectToAction(nameof(Index));
            }


            var interviewers = await _adminService.GetAllInterviewers();

            List<SelectListItem> interviewerSelector = new List<SelectListItem>();

            interviewerSelector.Add(new SelectListItem("", ""));

            foreach (var item in interviewers)
                interviewerSelector.Add(new SelectListItem(item.FullName, item.Id));

            var workingGroups = _adminService.GetAllWorkingGroup();

            List<SelectListItem> workingGroupSelector = new List<SelectListItem>();

            workingGroupSelector.Add(new SelectListItem("", ""));

            foreach (var item in workingGroups)
                workingGroupSelector.Add(new SelectListItem(item.Title, item.Id.ToString()));

            ViewBag.InterviewerSelector = interviewerSelector;
            ViewBag.WorkingGroupSelector = workingGroupSelector;


            if (!string.IsNullOrEmpty(model.InterviewerId))
                ViewBag.FullName = _adminService.GetFullName(model.InterviewerId);

            return View(model.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.PrimaryResponsibble)]
        public IActionResult Check(int id, string interviewerId, int workingGroupId)
        {
            var servicResult = _adminService.SetCompleteDataForMember(id, interviewerId, workingGroupId);
            if (servicResult.IsSuccess)
                Swal(true, "عملیات با موفقیت انجام شد");
            else
                Swal(false, servicResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }

        [AccessCodeFlter(AccessCode.Interviewer)]
        public IActionResult Interviewer()
        {
            var data = _adminService.GetAllForInterviewer(UserId);
            return View(data.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.Interviewer)]
        public IActionResult CheckInterviewer(int id)
        {
            var member = _adminService.GetMember(id);

            if (member == null)
            {
                Swal(false, "شناسه عضو یافت نشد");
                return RedirectToAction(nameof(Interviewer));
            }

            if (member.InterviewerId != UserId || member.Status == MemberStatus.Wating)
            {
                Swal(false, "این عضو متعلق به شما نمی باشد");
                return RedirectToAction(nameof(Interviewer));
            }

            List<SelectListItem> statusSelector = new List<SelectListItem>();
            statusSelector.Add(new SelectListItem("", ""));
            statusSelector.Add(new SelectListItem("قابل قبول", MemberStatus.Confirmed.ToString()));
            statusSelector.Add(new SelectListItem("غیرقابل قبول", MemberStatus.Rejected.ToString()));

            ViewBag.StatusSelector = statusSelector;

            return View(member.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.Interviewer)]
        [HttpPost]
        public IActionResult CheckInterviewer(int id, MemberStatus status, string description)
        {
            var serviceResult = _adminService.SetCompleteDataForInterviewer(id, status, description);
            if (serviceResult.IsSuccess)
                Swal(true, "عملیات با موفقیت انجام شد");
            else Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Interviewer));
        }
    }
}