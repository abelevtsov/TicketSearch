using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FlyTicketsSearch
{
    public abstract class SearchBase : IDisposable
    {
        private IWebDriver driver;

        protected IWebDriver WebDriver
        {
            get
            {
                return driver ?? (driver = InitDriver());
            }
        }

        public DateTime? DateTo { get; set; }

        public DateTime? DateBack { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        protected SearchBase(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public abstract Task<int> Search();

        private static IWebDriver InitDriver()
        {
            var webDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), new ChromeOptions());
            return webDriver;
        }

        public void Dispose()
        {
            if (WebDriver == null)
            {
                return;
            }

            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}
