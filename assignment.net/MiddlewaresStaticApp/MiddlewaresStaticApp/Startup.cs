using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MiddlewareStaticApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
          
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseHttpsRedirection();

            
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");
                await next();
                Console.WriteLine($"[Response] {context.Response.StatusCode} for {context.Request.Path}");
            });

            app.Use(async (context, next) =>
            {
                context.Response.Headers["Content-Security-Policy"] = "default-src 'self'; script-src 'self'; style-src 'self';";
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                context.Response.Headers["X-Frame-Options"] = "DENY";
                await next();
            });

           
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] {ex.Message}");
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                }
            });

            app.UseDefaultFiles(); 
            app.UseStaticFiles();

  
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from MiddlewareStaticApp");
            });
        }
    }
}