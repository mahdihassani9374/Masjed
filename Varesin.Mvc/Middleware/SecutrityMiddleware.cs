using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Varesin.Database;
using Varesin.Database.Identity.Entities;
using Varesin.Database.Migrations;

namespace Varesin.Mvc.Middleware
{
    public class SecutrityMiddleware
    {
        private readonly RequestDelegate Next;
        public SecutrityMiddleware(RequestDelegate requestDelegate)
        {
            Next = requestDelegate;
        }

        public async Task Invoke(HttpContext context, AppDbContext dbContext, SignInManager<User> signInManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var securityStamp = context.User.Claims.FirstOrDefault(c => c.Type == "AspNet.Identity.SecurityStamp")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = dbContext.Users.FirstOrDefault(c => c.Id.Equals(userId));
                    if (user.SecurityStamp != securityStamp)
                        await signInManager.SignOutAsync();
                    await Next(context);
                }
            }
            else await Next(context);
        }
    }
}
