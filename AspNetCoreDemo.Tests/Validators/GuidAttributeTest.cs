using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Model.SlcsOutbound;
using AspNetCoreDemo.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using AspNetCoreDemo.Tests.TestHelpers;
using AspNetCoreDemo.Validators;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDemo.Tests.Validators
{
    [TestClass]
    public class GuidAttributeTest
    {
        [TestMethod]
        public void GuidAttributeReturnsFalseForANonGuid()
        {
            var attribute = new GuidAttribute();
            Assert.IsFalse(attribute.IsValid("123"));
        }

        [TestMethod]
        public void GuidAttributeReturnsTrueForNull()
        {
            var attribute = new GuidAttribute();
            Assert.IsTrue(attribute.IsValid(null));
        }

        [TestMethod]
        public void GuidAttributeReturnsTrueForValidGuid()
        {
            var attribute = new GuidAttribute();
            Assert.IsTrue(attribute.IsValid("e68e181c-0317-4547-a529-bfe2f22096d4"));
        }

        [TestMethod]
        public void GuidAttributeReturnsErrorMessageForInvalidGuid()
        {
            var inst = "123";
            var attribute = new GuidAttribute();
            var validationContext = new ValidationContext(inst);
            string validationExceptionMessage = null;

            try
            {
                attribute.Validate(inst, validationContext);
            }
            catch (ValidationException ex)
            {
                validationExceptionMessage = ex.Message;
            }

            Assert.IsNotNull(validationExceptionMessage);
        }
    }
}
