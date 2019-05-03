using AspNetCoreDemo.Models.SlcsOutbound;
using AspNetCoreDemo.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Tests.Pipeline
{
    [TestClass]
    public class UnexpectedExceptionHandlerTests
    {
        [TestMethod]
        public async Task UnhandledExceptionsReturnA500()
        {
            var context = new DefaultHttpContext();

            await UnexpectedExceptionHandler.HandleUnexpectedException(context);
        }
    }
}
