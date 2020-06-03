using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Varesin.Database;
using Varesin.Database.Identity.Entities;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Services;
using Varesin.Utility;

namespace Varesin.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserService _userService;
        private readonly AppDbContext _dbContext;
        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            UserService userService,
            AppDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectPermanent("/admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var serviceResult = new ServiceResult(true);

            var user = _dbContext.Users.FirstOrDefault(c => c.PhoneNumber == model.PhoneNumber);

            if (user == null)
                serviceResult.AddError("کاربری یافت نشد");

            if (serviceResult.IsSuccess)
            {
                var checkPass = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

                if (checkPass.Succeeded)
                {
                    var signInResult = await _signInManager
                     .PasswordSignInAsync(user.UserName, model.Password, true, false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectPermanent("/admin");
                    }
                }
                else serviceResult.AddError("کاربری یافت نشد");
            }

            Swal(false, serviceResult.Errors.FirstOrDefault());

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();
            return RedirectPermanent("/");
        }

        public IActionResult Sync()
        {
            new DatabaseInitializer().Seed(_userManager, _roleManager);
            return Json(true);
        }

        [Route("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}