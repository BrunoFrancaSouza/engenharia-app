using Engenharia.Application.ExceptionMiddleware;
using Microsoft.AspNetCore.Builder;

namespace Engenharia.Application.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
