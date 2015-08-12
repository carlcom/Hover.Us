using OpenQA.Selenium;
using Xunit;

namespace web.Tests.Browser
{
    public class RoutingTests : BrowserTest
    {
        private const string baseURL = "http://beta.stevedesmond.ca";

        [Fact]
        public void FindsHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
            Assert.Equal("Steve Desmond", driver.Title);
        }

        [Fact]
        public void FindsStaticFile()
        {
            driver.Navigate().GoToUrl(baseURL + "/steve.jpg");
            Assert.Equal("steve.jpg (400x400 pixels)", driver.Title);
        }

        [Fact]
        public void FindsCategory()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog");
            Assert.Equal("Steve Desmond - Blog", driver.Title);
        }

        [Fact]
        public void FindsCategoryWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/");
            Assert.Equal("Steve Desmond - Blog", driver.Title);
        }

        [Fact]
        public void FindsPost()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/hello-vnext-world");
            Assert.Equal("Steve Desmond - Blog - Hello, vNext World!", driver.Title);
        }

        [Fact]
        public void FindsPostWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/hello-vnext-world/");
            Assert.Equal("Steve Desmond - Blog - Hello, vNext World!", driver.Title);
        }

        [Fact]
        public void FindsStaticCategory()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks");
            Assert.Equal("Steve Desmond - Talks", driver.Title);
        }

        [Fact]
        public void FindsStaticCategoryWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/");
            Assert.Equal("Steve Desmond - Talks", driver.Title);
        }

        [Fact]
        public void FindsStaticPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/hewny15");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void FindsStaticPageWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/hewny15/");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void FindsNestedStaticPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/hewny15/presentation");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void FindsNestedStaticPageWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/hewny15/presentation/");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void FindsOtherController()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo");
            Assert.Equal("Steve Desmond Photography", driver.Title);
        }

        [Fact]
        public void FindsOtherControllerWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo/");
            Assert.Equal("Steve Desmond Photography", driver.Title);
        }

        [Fact]
        public void FindsOtherControllerMethod()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo/navbar");
            Assert.StartsWith("{\"TagTypes\":[{", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void FindsOtherControllerMethodWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo/navbar/");
            Assert.StartsWith("{\"TagTypes\":[{", driver.FindElement(By.TagName("body")).Text);
        }
    }
}