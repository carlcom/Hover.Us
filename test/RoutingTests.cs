using OpenQA.Selenium;
using Xunit;

namespace Test
{
    public class RoutingTests : IClassFixture<PhantomFixture>
    {
        private readonly IWebDriver driver;

        public RoutingTests(PhantomFixture fixture)
        {
            driver = fixture.driver;
        }

        private const string baseURL = "http://localhost:5000";

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
        public void FindsOtherController()
        {
            driver.Navigate().GoToUrl(baseURL + "/about");
            Assert.Contains("About Steve", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void FindsOtherControllerWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/about/");
            Assert.Contains("About Steve", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void FindsOtherControllerMethod()
        {
            driver.Navigate().GoToUrl(baseURL + "/about/datacenter");
            Assert.Contains("machines", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void FindsOtherControllerMethodWithSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/about/datacenter/");
            Assert.Contains("machines", driver.FindElement(By.TagName("body")).Text);
        }
    }
}