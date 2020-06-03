using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Varesin.Utility;
using System.Security.Claims;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class BaseController : Controller
    {
        protected void AddErrors(ServiceResult serviceResult)
        {
            foreach (var error in serviceResult.Errors)
                ModelState.AddModelError("", error);
        }
        protected void Swal(bool isSuccess, string message)
        {
            TempData.Clear();
            TempData.Add("serviceResult.Message", message);
            TempData.Add("serviceResult.Success", isSuccess);
        }
        protected string UserId
        {
            get
            {
                var userClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return userClaim?.Value;
            }
        }
    }
}