using System;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Test
{
    public class PhantomFixture : IDisposable
    {
        public readonly IWebDriver driver;
        private readonly Process server;

        public PhantomFixture()
        {
            server = Process.Start(new ProcessStartInfo
            {
                FileName = "dnx.exe",
                Arguments = "web",
                WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "Web")
            });
            driver = new PhantomJSDriver();
        }

        public void Dispose()
        {
            server.Kill();
            driver.Dispose();
        }
    }
}