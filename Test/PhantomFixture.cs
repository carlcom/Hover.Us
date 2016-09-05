using System;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Test
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
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