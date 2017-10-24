using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Meta.NET.Tests
{
    [TestClass]
    public class ParserTest
    {
        TestContext testContext;
        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }

        static readonly string sampleDescription = "A test page.";
        static readonly string sampleIcon = "http://www.example.com/favicon.ico";
        static readonly string sampleImageHTTP = "http://www.example.com/image.png";
        static readonly string sampleImageHTTPS = "https://www.example.com/secure_image.png";
        static readonly string sampleTitle = "Page Title";
        static readonly string sampleType = "article";
        static readonly string sampleUrl = "http://www.example.com/";
        static readonly string sampleProviderName = "Example Provider";

        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public async Task TestParseMetaData()
        {
            TestContext.WriteLine("parses metadata");

            var sampleHtml =
                $"<html>" +
                $"<head>" +
                $"<meta property=\"og:description\" content=\"{sampleDescription}\" />" +
                $"<link rel=\"icon\" href=\"{sampleIcon}\" />" +
                $"<meta property=\"og:image\" content=\"{sampleImageHTTP}\" />" +
                $"<meta property=\"og:image:url\" content=\"{sampleImageHTTP}\" />" +
                $"<meta property=\"og:image:secure_url\" content=\"{sampleImageHTTPS}\" />" +
                $"<meta property=\"og:title\" content=\"{sampleTitle}\" />" +
                $"<meta property=\"og:type\" content=\"{sampleType}\" />" +
                $"<meta property=\"og:url\" content=\"{sampleUrl}\" />" +
                $"<meta property=\"og:site_name\" content=\"{sampleProviderName}\" />" +
                $"</head>" +
                $"</html>";

            var parser = new Parser();
            var metaData = await parser.ParseHtmlAsync(sampleHtml, sampleUrl);

            Assert.AreEqual(sampleDescription, metaData["description"], $"Unable to find ${sampleDescription}");
            Assert.AreEqual(sampleIcon, metaData["icon"], $"Unable to find ${sampleIcon}");
            Assert.AreEqual(sampleImageHTTPS, metaData["image"], $"Unable to find ${sampleImageHTTPS}");
            Assert.AreEqual(sampleTitle, metaData["title"], $"Unable to find ${sampleTitle}");
            Assert.AreEqual(sampleType, metaData["type"], $"Unable to find ${sampleType}");
            Assert.AreEqual(sampleUrl, metaData["url"], $"Unable to find ${sampleUrl}");
            Assert.AreEqual(sampleProviderName, metaData["provider"], $"Unable to find ${sampleProviderName}");
        }

        [TestMethod]
        public async Task TestAbsoluteUrl()
        {
            TestContext.WriteLine("uses absolute URLs when url parameter passed in");

            var relativeHtml =
                $"<html>" +
                $"<head>" +
                $"<meta property=\"og:description\" content=\"{sampleDescription}\" />" +
                $"<link rel=\"icon\" href=\"/favicon.ico\" />" +
                $"<meta property=\"og:image\" content=\"/image.png\" />" +
                $"<meta property=\"og:title\" content=\"{sampleTitle}\" />" +
                $"<meta property=\"og:type\" content=\"{sampleType}\" />" +
                $"<meta property=\"og:url\" content=\"{sampleUrl}\" />" +
                $"</head>" +
                $"</html>";

            var parser = new Parser();
            var metaData = await parser.ParseHtmlAsync(relativeHtml, sampleUrl);

            Assert.AreEqual(sampleIcon, metaData["icon"], $"Unable to find ${sampleIcon}");
            Assert.AreEqual(sampleImageHTTP, metaData["image"], $"Unable to find ${sampleImageHTTP}");
        }

        [TestMethod]
        public async Task TestProvider()
        {
            TestContext.WriteLine("adds a provider when URL passed in");

            var sampleProvider = "www.example.com";
            var emptyHtml =
                $"<html>" +
                $"<head>" +
                $"</head>" +
                $"</html>";

            var parser = new Parser();
            var metaData = await parser.ParseHtmlAsync(emptyHtml, sampleUrl);

            Assert.AreEqual(sampleProvider, metaData["provider"], $"Unable to find {sampleProvider} in {sampleUrl}");
        }

        [TestMethod]
        public async Task TestOpenGraphProvider()
        {
            TestContext.WriteLine("prefers open graph site name over URL based provider");

            var sampleProvider = "OpenGraph Site Name";
            var providerHtml =
                $"<html>" +
                $"<head>" +
                $"<meta property=\"og:site_name\" content=\"{sampleProvider}\" />" +
                $"</head>" +
                $"</html>";

            var parser = new Parser();
            var metaData = await parser.ParseHtmlAsync(providerHtml, sampleUrl);

            Assert.AreEqual(sampleProvider, metaData["provider"], $"Unable to find ${sampleProvider}");
        }

        [TestMethod]
        public async Task TestFavicon()
        {
            TestContext.WriteLine("uses default favicon when no favicon is found");

            var noIconHtml =
                $"<html>" +
                $"<head>" +
                $"</head>" +
                $"</html>";

            var parser = new Parser();
            var metaData = await parser.ParseHtmlAsync(noIconHtml, sampleUrl);

            Assert.AreEqual(sampleIcon, metaData["icon"]);
        }

        [TestMethod]
        public async Task TestUrl()
        {
            TestContext.WriteLine("falls back on provided url when no canonical url found");

            var emptyHtml =
                $"<html>" +
                $"<head>" +
                $"</head>" +
                $"</html>";

            var parser = new Parser();
            var metaData = await parser.ParseHtmlAsync(emptyHtml, sampleUrl);

            Assert.AreEqual(sampleUrl, metaData["url"]);
        }
    }
}
