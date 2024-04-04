using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace SeleniumFramework.PageObjects.TheInternet
{
    class PunDetailPage : BasePage
    {
        private readonly By _printableTabLocator = By.XPath("//span[text()='Printable']");

        public PunDetailPage(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Clicks the Printable tab.
        /// </summary>
        public void ClickPrintableTab()
        {
            Click(_printableTabLocator);
        }

        public List<string> GetGeneralLeaseLegalInformation()
        {
            List<string> output = new List<string>();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'TableContainer')][.//h3[text()='General Lease Legal Information']]//table[contains(@class,'DocTableNormal')]")));
            output.Add(string.Join(",", table.FindElements(By.CssSelector("th")).Select(e => e.Text).ToArray()));

            ReadOnlyCollection<IWebElement> cells = table.FindElements(By.CssSelector("td"));
            output.Add(string.Join(",", cells.Select(e => e.Text).ToArray()));

            return output;
        }
    }
}