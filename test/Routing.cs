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
        public void CapitalizedCategory()
        {
            driver.Navigate().GoToUrl(baseURL + "/Blog");
            Assert.Equal("Steve Desmond – Blog", driver.Title);
        }

        [Fact]
        public void CapitalizedCategorySlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Blog/");
            Assert.Equal("Steve Desmond – Blog", driver.Title);
        }

        [Fact]
        public void CapitalizedPost()
        {
            driver.Navigate().GoToUrl(baseURL + "/Blog/Hello-vNext-World");
            Assert.Equal("Steve Desmond – Blog – Hello, vNext World!", driver.Title);
        }

        [Fact]
        public void CapitalizedPostSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Blog/Hello-vNext-World/");
            Assert.Equal("Steve Desmond – Blog – Hello, vNext World!", driver.Title);
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
            driver.Navigate().GoToUrl(baseURL + "/Talks/HEWNY15");
            Assert.Equal("There's an App for That", driver.Title);
        }

        [Fact]
        public void CapitalizedStaticPageSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Talks/HEWNY15/");
            Assert.Equal("There's an App for That", driver.Title);
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

        [Fact]
        public void Resume()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume");
            Assert.Equal("Steve Desmond – Resume", driver.Title);
        }

        [Fact]
        public void ResumeSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume/");
            Assert.Equal("Steve Desmond – Resume", driver.Title);
        }

        [Fact]
        public void CapitalizedResume()
        {
            driver.Navigate().GoToUrl(baseURL + "/Resume");
            Assert.Equal("Steve Desmond – Resume", driver.Title);
        }

        [Fact]
        public void CapitalizedResumeSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Resume/");
            Assert.Equal("Steve Desmond – Resume", driver.Title);
        }

        [Fact]
        public void ResumeWithGoodForShowsContact()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume?for=test");
            Assert.NotEmpty(driver.FindElement(By.ClassName("contact")).Text);
        }

        [Fact]
        public void ResumeWithBadForDoesNotContact()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume?for=x");
            Assert.Empty(driver.FindElement(By.ClassName("contact")).Text);
        }

        [Fact]
        public void CannotHackContact()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume/contact");
            Assert.Contains(";)", driver.PageSource);
        }
    }
}