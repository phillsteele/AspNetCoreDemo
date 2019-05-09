using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreDemo.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCoreDemo.Tests.Security
{
    [TestClass]
    public class HasClaimAuthorizeAttributeTests
    {
        [TestMethod]
        public void CanCreate()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute("123", "");
            Assert.IsNotNull(hasClaimAuthorizeAttribute);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoClaimSpecified_NullReferenceExceptionExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute("", "");
            Assert.IsNotNull(hasClaimAuthorizeAttribute);
        }

        [TestMethod]
        public void ClaimNameSpecified_ClaimNameExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute("abc");
            Assert.AreEqual("abc", hasClaimAuthorizeAttribute.Claim);
        }

        [TestMethod]
        public void ClaimNameSpecifiedNoValueSpecified_ClaimNameExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute("abc", "");
            Assert.AreEqual("abc", hasClaimAuthorizeAttribute.Claim);
        }

        [TestMethod]
        public void ValueSpecified_ValueExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute("abc", "xyz");
            Assert.AreEqual("xyz", hasClaimAuthorizeAttribute.Value);
        }

        [TestMethod]
        public void NoValueSpecified_EmptyValueExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute("abc", "");
            Assert.AreEqual("", hasClaimAuthorizeAttribute.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void NoClaimSpecified_CannotReferenceClaim_ApplicationExceptionExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute();
            var x = hasClaimAuthorizeAttribute.Claim;
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void NoClaimSpecified_CannotReferenceValue_ApplicationExceptionExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute();
            var x = hasClaimAuthorizeAttribute.Value;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ClaimContainsColonCharacter_ArgumentExceptionExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute("this:test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValueContainsColonCharacter_ArgumentExceptionExpected()
        {
            var hasClaimAuthorizeAttribute = new HasClaimAuthorizeAttribute("xyz","this:test");
        }
    }
}
