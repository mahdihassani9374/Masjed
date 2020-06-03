using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Varesin.Database.Identity.Entities;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.Profile;
using Varesin.Mvc.Services;
using Varesin.Services;
using Varesin.Utility;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly AdminService _adminService;
        private readonly FileService _fileService;
        private readonly SignInManager<User> _signInManager;
        public ProfileController(AdminService adminService,
            FileService fileService,
            SignInManager<User> signInManager)
        {
            _adminService = adminService;
            _fileService = fileService;
            _signInManager = signInManager;
        }
        public IActionResult ChangeProfile()
        {
            var user = _adminService.GetUser(UserId);
            return View(user.ToProfileViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> ChangeProfile(ChangeProfileViewModel model, IFormFile file)
        {
            var dto = model.ToDto();

            var validationResult = _adminService.ChangeProfile_Validation(dto);

            if (validationResult.IsSuccess)
            {
                var uploadService = new ServiceResult<string>(false);
                if (file != null)
                    uploadService = _fileService.Upload(file, "Profile", 1024 * 200);

                model.ImageName = uploadService.Data;

                if ((file != null && uploadService.IsSuccess) || file == null)
                {
                    var serviceResult = _adminService.ChangeProfile(model.ToDto());

                    if (serviceResult.IsSuccess)
                    {
                        Swal(true, "عملیات با موفقیت انجام شد");
                        if (file != null && !string.IsNullOrEmpty(serviceResult.Data))
                            _fileService.Delete(serviceResult.Data, "Profile");

                        var user = _adminService.GetUserEntity(UserId);

                        await _signInManager.SignOutAsync();
                        await _signInManager.SignInAsync(user, true);

                        return RedirectToAction(nameof(ChangeProfile));
                    }
                    model.ImageName = serviceResult.Data;
                    AddErrors(serviceResult);
                }

                else Swal(false, uploadService.Errors.FirstOrDefault());
            }

            else
                AddErrors(validationResult);

            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var serviceResult = await _adminService.ChangePassword(model.ToDto(UserId));

            if (serviceResult.IsSuccess)
            {
                Swal(true, "عملیات با موفقیت انجام شد");
                return RedirectToAction(nameof(ChangePassword));
            }

            AddErrors(serviceResult);

            return View();
        }
    }
}