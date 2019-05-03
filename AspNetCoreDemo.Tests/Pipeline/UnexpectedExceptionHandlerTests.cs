using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Model.SlcsOutbound;
using AspNetCoreDemo.Pipeline;
using AspNetCoreDemo.Tests.TestHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Tests.Pipeline
{
    // DefaultHttpContext is a 'fake' HttpContext

    [TestClass]
    public class UnexpectedExceptionHandlerTests
    {
        [TestMethod]
        public async Task UnhandledExceptionsReturnA500()
        {
            var context = new DefaultHttpContext();

            context.AddExceptionToContext(new ApplicationException("an exception"));

            await UnexpectedExceptionHandler.Invoke(context);

            Assert.AreEqual(500, context.Response.StatusCode);
        }

        [TestMethod]
        public async Task UnhandledValidationExceptionReturnA422()
        {
            var context = new DefaultHttpContext();

            var validationException = new SlcsValidationException(ValidationExceptionSeverity.Error, "a message",
                SlcsErrors.WrapError(new SlcsError { code = "123" }));

            context.AddExceptionToContext(validationException);

            await UnexpectedExceptionHandler.Invoke(context);

            Assert.AreEqual(422, context.Response.StatusCode);
        }

        [TestMethod]
        public async Task UnhandledValidationException_BodySerialisesToA_SlcsErrors_Instance()
        {
            var context = new DefaultHttpContext();

            // We need to initialise the fake httpContext to get to the body
            context.Response.Body = new MemoryStream();

            var ex = new SlcsValidationException(ValidationExceptionSeverity.Error, "a message",
                SlcsErrors.WrapError(new SlcsError { code = "123" }));

            context.AddExceptionToContext(ex);

            await UnexpectedExceptionHandler.Invoke(context);

            string body = await context.GetBodyFromResponse();

            var expectedErrorObject = JsonConvert.DeserializeObject<SlcsErrors>(body);

            Assert.IsNotNull(expectedErrorObject);
        }

        [TestMethod]
        public async Task UnhandledException_BodySerialisesToA_SlcsErrors_Instance()
        {
            var context = new DefaultHttpContext();

            // We need to initialise the fake httpContext to get to the body
            context.Response.Body = new MemoryStream();

            var ex = new ApplicationException("Some exception occurred");

            context.AddExceptionToContext(ex);

            await UnexpectedExceptionHandler.Invoke(context);

            string body = await context.GetBodyFromResponse();

            var expectedErrorObject = JsonConvert.DeserializeObject<SlcsErrors>(body);

            Assert.IsNotNull(expectedErrorObject);
        }
    }
}
