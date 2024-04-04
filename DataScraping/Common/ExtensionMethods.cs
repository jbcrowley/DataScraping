using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFramework.Common
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Creates a <see cref="SelectElement"/> instance associated with the provided <see cref="IWebElement"/>.
        /// This extension method allows for easier interaction with HTML SELECT elements, making it more efficient and
        /// readable in Selenium-based test automation.
        /// </summary>
        /// <param name="e">The <see cref="IWebElement"/> representing the HTML SELECT element on the web page.</param>
        /// <returns>A <see cref="SelectElement"/> instance representing the SELECT element.</returns>
        public static SelectElement Select(this IWebElement e)
        {
            return new SelectElement(e);
        }
    }
}