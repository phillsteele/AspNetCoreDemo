using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Tests.TestHelpers
{
    /// <summary>
    /// Helper methods for unit tests
    /// </summary>
    public static class HttpContextExtensions
    {
        public static async Task<string> GetBodyFromResponse(this DefaultHttpContext context)
        {
            // Before we read from the body we need to reset the stream position back to 0
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // Now we can read the response
            using (var streamReader = new StreamReader(context.Response.Body))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        public static void AddExceptionToContext(this DefaultHttpContext context, Exception ex)
        {
            // We need to add a feature of type IExceptionHandlerPathFeature to associate the exception with the http context
            var exeptionHandlerPathFeature = new ExceptionHandlerFeature();
            exeptionHandlerPathFeature.Error = ex;
            context.Features.Set<IExceptionHandlerPathFeature>(exeptionHandlerPathFeature);
        }
    }
}
