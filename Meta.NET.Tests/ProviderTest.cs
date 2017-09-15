using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meta.NET.Extensions;

namespace Meta.NET.Tests
{
    [TestClass]
    public class ProviderTest
    {
        TestContext testContext;
        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }

        [TestMethod]
        public void TestProvider()
        {
            TestContext.WriteLine("gets a provider with no subdomain");

            Assert.AreEqual("example", "https://example.com/this/?id=that".GetUrlProvider(), "gets a provider with no subdomain");
            Assert.AreEqual("example", "https://www.example.com/this/?id=that".GetUrlProvider(), "removes www as a subdomain");
            Assert.AreEqual("example", "https://www1.example.com/this/?id=that".GetUrlProvider(), "removes www1 as a subdomain");
            Assert.AreEqual("things example", "https://things.example.com/this/?id=that".GetUrlProvider(), "preserves non-www subdomains");
            Assert.AreEqual("things example", "https://things.example.co.uk/this/?id=that".GetUrlProvider(), "removes secondary TLDs");
        }
    }
}
