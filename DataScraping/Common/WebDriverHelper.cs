using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SeleniumFramework.Common
{
    class WebDriverHelper
    {
        /// <summary>
        /// Gets the name of the browser from the NUnit TestContext.
        /// </summary>
        /// <returns>The browser name.</returns>
        public static Browser GetBrowserFromTestContext()
        {
            string browserValue = TestContext.Parameters.Get("browser")!;
            if (Enum.TryParse(browserValue, true, out Browser browser))
            {
                return browser;
            }

            throw new ArgumentException($"<{browserValue}> is not a defined browser type.");
        }

        /// <summary>
        /// Gets the desired Selenium driver from the config.
        /// </summary>
        /// <returns>A driver instance.</returns>
        public static IWebDriver GetDriver()
        {
            Browser browser = GetBrowserFromTestContext();
            switch (browser)
            {
                case Browser.Chrome:
                    return new ChromeDriver();
                case Browser.Edge:
                    return new EdgeDriver();
                default:
                    throw new ArgumentException($"{browser} is not defined in GetDriver().");
            }
        }

        /// <summary>
        /// A list of supported browsers.
        /// </summary>
        public enum Browser
        {
            Chrome,
            Edge
        }
    }
}