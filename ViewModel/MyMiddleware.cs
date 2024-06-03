namespace Ogani.ViewModel
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Ogani.Data;
    using System;
    using System.Threading.Tasks;

    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, OganiDbContext _context)
        {
            string newUser = httpContext.Session.GetString("newUser");
            if (newUser == null)
            {
                httpContext.Session.SetString("newUser", "true");
                var page = _context.Pages.Find(new Guid("3d5a6cad-25cd-4aef-9d16-3f22d6d5d717"));
                page.TotalView += 1;
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();
            }
            httpContext.Session.SetString("TotalVisistor", _context.Pages.Find(new Guid("3d5a6cad-25cd-4aef-9d16-3f22d6d5d717")).TotalView.ToString());

            //get the url
            string visitorId = httpContext.Request.Cookies["VisitorId"];
            if (visitorId == null)
            {
                //find your data
                httpContext.Response.Cookies.Append("VisitorId", Guid.NewGuid().ToString(), new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = false,
                });
                _context.SaveChanges();
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}