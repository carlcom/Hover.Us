using System;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Test
{
    public class PhantomFixture : IDisposable
    {
        public readonly IWebDriver driver;

        public PhantomFixture()
        {
            driver = new PhantomJSDriver();
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}