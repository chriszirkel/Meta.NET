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

            Assert.AreEqual("example", Url.GetUrlProvider("https://example.com/this/?id=that"), "gets a provider with no subdomain");
            Assert.AreEqual("example", Url.GetUrlProvider("https://www.example.com/this/?id=that"), "removes www as a subdomain");
            Assert.AreEqual("example", Url.GetUrlProvider("https://www1.example.com/this/?id=that"), "removes www1 as a subdomain");
            Assert.AreEqual("things example", Url.GetUrlProvider("https://things.example.com/this/?id=that"), "preserves non-www subdomains");
            Assert.AreEqual("things example", Url.GetUrlProvider("https://things.example.co.uk/this/?id=that"), "removes secondary TLDs");
        }
    }
}
