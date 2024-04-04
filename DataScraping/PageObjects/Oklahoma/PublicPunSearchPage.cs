using OpenQA.Selenium;

namespace SeleniumFramework.PageObjects.TheInternet
{
    class PublicPunSearchPage : BasePage
    {
        private readonly By _punSearchInputLocator = By.XPath("//tr[.//span[text()='Search By PUN:']]//input");
        private readonly By _searchButtonLocator = By.XPath("//span[text()='Search']");

        public PublicPunSearchPage(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Clicks the link containing the provided PUN.
        /// </summary>
        /// <param name="pun">The PUN to click.</param>
        public void ClickPunLink(string pun)
        {
            Click(By.XPath($"//a[text()='{pun}']"));
        }

        /// <summary>
        /// Search by PUN.
        /// </summary>
        /// <param name="pun">The PUN to search for.</param>
        public void SearchByPun(string pun)
        {
            DateTime expire = DateTime.Now.AddSeconds(10);
            IReadOnlyCollection<IWebElement> inputs = FindElements(_punSearchInputLocator);
            while (inputs.Count == 0 && DateTime.Now < expire)
            {
                inputs = FindElements(_punSearchInputLocator);
                Thread.Sleep(500);
            }
            inputs.ElementAt(0).Click();
            inputs.ElementAt(1).SendKeys(pun);
            Click(_searchButtonLocator);
        }
    }
}