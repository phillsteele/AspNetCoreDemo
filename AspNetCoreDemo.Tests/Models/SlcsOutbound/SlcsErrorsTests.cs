using AspNetCoreDemo.Models.SlcsOutbound;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AspNetCoreDemo.Tests.Models.SlcsOutbound
{
    [TestClass]
    public class SlcsErrorsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WrapErrorThrowsExceptionOnNull()
        {
            SlcsErrors.WrapError(null);
        }

        [TestMethod]
        public void WrapErrorCreatesACollectionOfErrors()
        {
            var errors = SlcsErrors.WrapError(new SlcsError()
            {
                code = "123"
            });

            Assert.AreEqual(1, errors.errors.Count);
            Assert.AreEqual("123", errors.errors[0].code);
        }
    }
}
