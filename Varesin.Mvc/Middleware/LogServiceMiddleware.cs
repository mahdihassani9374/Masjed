using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Varesin.Database;
using Varesin.Domain.Entities;

namespace Varesin.Mvc.Middleware
{
    public class LogServiceMiddleware
    {
        private readonly RequestDelegate Next;
        public LogServiceMiddleware(RequestDelegate requestDelegate)
        {
            Next = requestDelegate;
        }

        public async Task Invoke(HttpContext context, AppDbContext dbCcontext)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            string userId = null;

            if (context.User.Identity.IsAuthenticated)
            {
                userId = context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            }

            await Next(context);

            stopwatch.Stop();

            dbCcontext.LogServices.Add(new LogService
            {
                CreateDate = DateTime.Now,
                UserId = userId,
                Method = context.Request.Method,
                RelativePath = context.Request.Path.Value,
                ContentLengthRequest = context.Request.ContentLength,
                ContentLengthResponse = context.Response.ContentLength,
                ResponseStatusCode = context.Response.StatusCode,
                Elabsed = stopwatch.Elapsed,
                IpAddress = context.Connection.RemoteIpAddress.ToString()
            });

            dbCcontext.SaveChanges();
        }
    }
}
