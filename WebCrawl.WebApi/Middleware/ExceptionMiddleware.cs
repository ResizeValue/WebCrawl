using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using WebCrawl.WebApi.Models;

namespace WebCrawl.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.Headers.Add("Host", context.Request.Host.Value);
            context.Response.Headers.Add("Port", context.Request.Host.Port.Value.ToString());
            context.Response.Headers.Add("Path", context.Request.Path.Value);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetailsModel()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
