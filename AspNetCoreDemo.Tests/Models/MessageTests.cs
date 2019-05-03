using AspNetCoreDemo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCoreDemo.Tests.Models
{
    [TestClass]
    public class MessageTests
    {
        public class TestClass : Message
        {
            public string myProperty;
        }

        [TestMethod]
        public void ToJsonSerialisesWith2SpacesForIndentation()
        {
            var test = new TestClass { myProperty = "123" };
            var serialisedTest = test.ToJson();
            Assert.AreEqual("{\r\n  \"myProperty\": \"123\"\r\n}", serialisedTest);
        }
    }
}
