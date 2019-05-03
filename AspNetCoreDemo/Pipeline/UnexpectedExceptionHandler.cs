using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Model.SlcsOutbound;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Pipeline
{
    public static class UnexpectedExceptionHandler
    {
        public static async Task Invoke(HttpContext context)
        {
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error == null)
                return;

            context.Response.ContentType = "application/json";

            // Get validation response if one exists, otherwise a default error message
            if (exceptionHandlerPathFeature.Error is SlcsValidationException)
            {
                await AssignValidationExceptionResponse(context, exceptionHandlerPathFeature);
                return;
            }

            await AssignUnexpectedExceptionResponse(context, exceptionHandlerPathFeature);
        }

        private static async Task AssignUnexpectedExceptionResponse(HttpContext context, IExceptionHandlerPathFeature exceptionHandlerPathFeature)
        {
            var errorResponse = SlcsErrors.WrapError(new SlcsError()
            {
                code = "Slcs.InternalError",
                description = exceptionHandlerPathFeature.Error.Message,
                retrySuggested = false
            });

            context.Response.StatusCode = 500;

            await context.Response.WriteAsync(errorResponse.ToJson());
        }

        private static async Task AssignValidationExceptionResponse(HttpContext context, IExceptionHandlerPathFeature exceptionHandlerPathFeature)
        {
            var errorResponse = ((SlcsValidationException)exceptionHandlerPathFeature?.Error).SlcsErrors;

            context.Response.StatusCode = 422;

            await context.Response.WriteAsync(errorResponse?.ToJson());
        }

    }
}
