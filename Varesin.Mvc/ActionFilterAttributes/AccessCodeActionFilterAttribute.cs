using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.ActionFilterAttributes
{
    public class AccessCodeFlterAttribute : ActionFilterAttribute
    {
        private readonly AccessCode _accessCode;
        public AccessCodeFlterAttribute(AccessCode accessCode)
        {
            _accessCode = accessCode;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.IsInRole(AccessCode.FullAccess.ToString()))
            {
                if (!context.HttpContext.User.IsInRole(_accessCode.ToString()))
                    context.Result = new ForbidResult();
            }
               
            base.OnActionExecuting(context);
        }
    }
}
