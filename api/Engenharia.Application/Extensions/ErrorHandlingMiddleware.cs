using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Engenharia.Application.ExceptionMiddleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (ex is ArgumentException)
                code = HttpStatusCode.BadRequest;

            //else if (ex is MyNotFoundException)
            //    code = HttpStatusCode.NotFound;

            //else if (ex is MyUnauthorizedException)
            //    code = HttpStatusCode.Unauthorized;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var errorDetails = new ErrorDetail { StatusCode = context.Response.StatusCode, ErrorMessage = ex.Message };

            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
