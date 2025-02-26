using Microsoft.Extensions.Options;
using System.Drawing;

namespace task5.Middleware
{
    public class ColorMiddleware
    {
        
        private readonly RequestDelegate next;
        private readonly IConfiguration _configuration;

        public ColorMiddleware(RequestDelegate next, IConfiguration configuration)
        {        
            this.next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, IOptions<IConfiguration> options)
        {
            string color = _configuration["Color"] ?? "green";
            context.Response.Headers.Append("Content-Type", "text/html; charset=utf-8");
            context.Items["Color"] = color;
            await next(context);
        }


    }
}
