using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Models.SlcsOutbound;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Pipeline
{
    public static class UnexpectedExceptionHandler
    {
        public static async Task HandleUnexpectedException(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            // Get validation response if one exists, otherwise a default error message
            if (exceptionHandlerPathFeature?.Error is SlcsValidationException)
            {
                await AssignValidationExceptionResponse(context, exceptionHandlerPathFeature);
                return;
            }

            await AssignUnexpectedExceptionResponse(context);
        }

        private static async Task AssignUnexpectedExceptionResponse(HttpContext context)
        {
            var errorResponse = SlcsErrors.WrapError(new SlcsError()
            {
                code = "Slcs.Validation.Error",
                data = "Newcastle Utd 1 - Manchester Utd 0",
                description = "The quick brown fox",
                retrySuggested = false,
                source = "Test"
            });

            context.Response.StatusCode = 500;

            await context.Response.WriteAsync(errorResponse.ToJson());
        }

        private static async Task AssignValidationExceptionResponse(HttpContext context, IExceptionHandlerPathFeature exceptionHandlerPathFeature)
        {
            var errorResponse = ((SlcsValidationException)exceptionHandlerPathFeature?.Error).SlcsErrors;

            context.Response.StatusCode = 500;

            await context.Response.WriteAsync(errorResponse?.ToJson());
        }

    }
}
