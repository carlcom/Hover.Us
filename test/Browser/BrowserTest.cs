using System;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace test.Browser
{
    public abstract class BrowserTest : IDisposable
    {
        protected readonly IWebDriver driver;

        protected BrowserTest()
        {
            driver = new PhantomJSDriver();
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}