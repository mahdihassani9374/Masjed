﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.ActionFilterAttributes;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Services;
using Varesin.Services;
using Varesin.Utility;
using Microsoft.AspNetCore.Hosting;
using Varesin.Mvc.Models.Post;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private readonly AdminService _adminService;
        private readonly FileService _fileService;
        private readonly IHostingEnvironment _hostingEnvironment;


        public PostController(AdminService adminService,
            FileService fileService,
            IHostingEnvironment hostingEnvironment)
        {
            _adminService = adminService;
            _fileService = fileService;
            _hostingEnvironment = hostingEnvironment;
        }

        [AccessCodeFlter(AccessCode.ViewPost)]
        public IActionResult Index(PostSearchViewModel searchModel)
        {
            var data = _adminService.GetPosts(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("10", "10", searchModel.PageSize == 10));
            pageSizeSelector.Add(new SelectListItem("20", "20", searchModel.PageSize == 20));
            pageSizeSelector.Add(new SelectListItem("30", "30", searchModel.PageSize == 30));
            pageSizeSelector.Add(new SelectListItem("40", "40", searchModel.PageSize == 40));
            pageSizeSelector.Add(new SelectListItem("50", "50", searchModel.PageSize == 50));

            ViewBag.PageSizeSelector = pageSizeSelector;

            return View(new SearchModel<PostSearchViewModel, PaginationViewModel<PostViewModel>>(searchModel, data.ToVewModel()));

        }

        [AccessCodeFlter(AccessCode.CreatePost)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.CreatePost)]
        public IActionResult Create(PostCreateViewModel model)
        {
            var uploadResult = _fileService.Upload(model.PrimaryPicture, "Post", 1024 * 500);

            var serviceResult = new ServiceResult();

            if (uploadResult.IsSuccess)
            {
                serviceResult = _adminService.CreatePost(model.ToDto(uploadResult.Data));

                if (serviceResult.IsSuccess)
                {
                    Swal(true, "یک پست با موفقیت اضافه شد");
                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                serviceResult.Errors = uploadResult.Errors;
                serviceResult.IsSuccess = false;
            }

            AddErrors(serviceResult);

            return View(model);
        }

        [AccessCodeFlter(AccessCode.EditPost)]
        public IActionResult Edit(int id)
        {
            var data = _adminService.GetPost(id);

            if (data == null)
            {
                Swal(false, "پستی با شناسه ارسالی یافت نشد");
                return RedirectToAction(nameof(Index));
            }

            return View(data.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.EditPost)]
        public IActionResult Edit(PostEditViewModel model)
        {
            var uploadResult = new ServiceResult<string>();
            var serviceResult = new ServiceResult<string>();

            if (model.PrimaryPicture != null)
                uploadResult = _fileService.Upload(model.PrimaryPicture, "Post", 1024 * 500);

            if (!uploadResult.IsSuccess && model.PrimaryPicture != null)
            {
                Swal(false, uploadResult.Errors.FirstOrDefault());
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            serviceResult = _adminService.EditPost(model.ToDto(uploadResult.Data));

            if (serviceResult.IsSuccess)
            {
                // delete file
                if (!string.IsNullOrEmpty(serviceResult.Data))
                    _fileService.Delete(serviceResult.Data, "Post");

                Swal(true, "پست با موفقیت ویرایش شد");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            AddErrors(serviceResult);

            var data = _adminService.GetPost(model.Id);

            return View(data.ToViewModel());
        }

        [AccessCodeFlter(AccessCode.DeletePost)]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeletePost(id);
            if (serviceResult.IsSuccess)
            {
                var deleteResult = _fileService.Delete(serviceResult.Data, "Post");
                Swal(true, "پست با موفقیت حذف شد");
            }
            else
                Swal(false, serviceResult.Errors.FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }

        [AccessCodeFlter(AccessCode.PostFileManagement)]
        public IActionResult File(int id)
        {
            var report = _adminService.GetPost(id);
            if (report == null)
            {
                Swal(false, "شناسه پست نامعتبر است");
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> fileTypeSelector = new List<SelectListItem>();
            fileTypeSelector.Add(new SelectListItem("", ""));
            fileTypeSelector.Add(new SelectListItem("عکس", Domain.Enumeration.FileType.Image.ToString()));
            fileTypeSelector.Add(new SelectListItem("صوتی", Domain.Enumeration.FileType.Audio.ToString()));
            fileTypeSelector.Add(new SelectListItem("تصویری", Domain.Enumeration.FileType.Video.ToString()));

            ViewBag.FileTypeSelector = fileTypeSelector;

            ViewBag.Files = _adminService.GetAllPostFiles(id).ToViewModel();

            return View(report.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AccessCodeFlter(AccessCode.PostFileManagement)]
        public IActionResult File(PostFileCreateViewModel model)
        {
            if (model.File == null)
                Swal(false, "فایلی انتخاب نکرده اید");
            else
            {
                long? maxLength = 0;

                if (model.FileType == Domain.Enumeration.FileType.Image)
                    maxLength = 500 * 1024;
                else maxLength = 25 * 1024 * 1024;

                var uploadResult = _fileService.Upload(model.File, "PostFile", maxLength);

                if (uploadResult.IsSuccess)
                {
                    var serviceResult = _adminService.CreatePostFile(model.ToDto(uploadResult.Data, model.File.Length));
                    if (serviceResult.IsSuccess)
                        Swal(true, "عملیات با موفقیت صورت گرفت");
                    else Swal(false, serviceResult.Errors.FirstOrDefault());
                }
                else
                    Swal(false, uploadResult.Errors.FirstOrDefault());
            }
            return RedirectToAction(nameof(File), new { id = model.PostId });
        }

        [AccessCodeFlter(AccessCode.PostFileManagement)]
        public IActionResult DeleteFile(int id)
        {
            var postFile = _adminService.GetPostFile(id);

            if (postFile == null)
                return RedirectToAction(nameof(Index));

            var deleteResult = _fileService.Delete(postFile.FileName, "PostFile");

            if (deleteResult.IsSuccess)
            {
                var serviceResult = _adminService.DeletePostFile(id);
                if (serviceResult.IsSuccess)
                    Swal(true, "عملیات با موفقیت انجام شد");
                else Swal(false, serviceResult.Errors.FirstOrDefault());
            }

            return RedirectToAction(nameof(File), new { id = postFile.PostId });
        }
    }
}