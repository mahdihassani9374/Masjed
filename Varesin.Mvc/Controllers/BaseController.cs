using Microsoft.AspNetCore.Mvc;
using Varesin.Utility;

namespace Varesin.Mvc.Controllers
{
    public abstract class BaseController : Controller
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
    }
}