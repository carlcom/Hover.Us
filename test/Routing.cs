using OpenQA.Selenium;
using Xunit;

namespace Test
{
    public class Routing : IClassFixture<PhantomFixture>
    {
        private readonly IWebDriver driver;

        public Routing(PhantomFixture fixture)
        {
            driver = fixture.driver;
        }

        private const string baseURL = "http://localhost:5000";

        [Fact]
        public void HomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
            Assert.Equal("Steve Desmond", driver.Title);
        }

        [Fact]
        public void StaticFile()
        {
            driver.Navigate().GoToUrl(baseURL + "/index.css");
            Assert.Contains("h1, h2, h3", driver.PageSource);
        }

        [Fact]
        public void Category()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog");
            Assert.Equal("Steve Desmond – Blog", driver.Title);
        }

        [Fact]
        public void CategorySlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/");
            Assert.Equal("Steve Desmond – Blog", driver.Title);
        }

        [Fact]
        public void Post()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/hello-vnext-world");
            Assert.Equal("Steve Desmond – Blog – Hello, vNext World!", driver.Title);
        }

        [Fact]
        public void PostSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/hello-vnext-world/");
            Assert.Equal("Steve Desmond – Blog – Hello, vNext World!", driver.Title);
        }

        [Fact]
        public void StaticCategory()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks");
            Assert.Equal("Steve Desmond – Talks", driver.Title);
        }

        [Fact]
        public void StaticCategorySlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/");
            Assert.Equal("Steve Desmond – Talks", driver.Title);
        }

        [Fact]
        public void StaticPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/hewny15");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void StaticPageSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/hewny15/");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void OtherController()
        {
            driver.Navigate().GoToUrl(baseURL + "/about");
            Assert.Contains("About Steve", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void OtherControllerSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/about/");
            Assert.Contains("About Steve", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void OtherControllerMethod()
        {
            driver.Navigate().GoToUrl(baseURL + "/about/datacenter");
            Assert.Contains("machines", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void OtherControllerMethodSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/about/datacenter/");
            Assert.Contains("machines", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void Photos()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo");
            Assert.Equal("Steve Desmond – Photography", driver.Title);
        }

        [Fact]
        public void PhotosSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo/");
            Assert.Equal("Steve Desmond – Photography", driver.Title);
        }

        [Fact]
        public void Image()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo/image/135");
            Assert.Equal("Steve Desmond – Photography – Matthew Good – Toronto, ON – 6/8/2004", driver.Title);
        }

        [Fact]
        public void ImageSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo/image/135/");
            Assert.Equal("Steve Desmond – Photography – Matthew Good – Toronto, ON – 6/8/2004", driver.Title);
        }

        [Fact]
        public void CapitalizedStaticPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/hewny15");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void CapitalizedStaticPageSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/talks/hewny15/");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void CapitalizedOtherController()
        {
            driver.Navigate().GoToUrl(baseURL + "/About");
            Assert.Contains("About Steve", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void CapitalizedOtherControllerSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/About/");
            Assert.Contains("About Steve", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void CapitalizedOtherControllerMethod()
        {
            driver.Navigate().GoToUrl(baseURL + "/About/Datacenter");
            Assert.Contains("machines", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void CapitalizedOtherControllerMethodSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/About/Datacenter/");
            Assert.Contains("machines", driver.FindElement(By.TagName("body")).Text);
        }

        [Fact]
        public void CapitalizedPhotos()
        {
            driver.Navigate().GoToUrl(baseURL + "/Photo");
            Assert.Equal("Steve Desmond – Photography", driver.Title);
        }

        [Fact]
        public void CapitalizedPhotosSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Photo/");
            Assert.Equal("Steve Desmond – Photography", driver.Title);
        }

        [Fact]
        public void CapitalizedImage()
        {
            driver.Navigate().GoToUrl(baseURL + "/Photo/Image/135");
            Assert.Equal("Steve Desmond – Photography – Matthew Good – Toronto, ON – 6/8/2004", driver.Title);
        }

        [Fact]
        public void CapitalizedImageSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Photo/Image/135/");
            Assert.Equal("Steve Desmond – Photography – Matthew Good – Toronto, ON – 6/8/2004", driver.Title);
        }
    }
}