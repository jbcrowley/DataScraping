using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text;

namespace SeleniumFramework.PageObjects.TheInternet
{
    class PublicPunSearchPage : BasePage
    {
        private readonly By _countySearchDropdownLocator = By.XPath("//tr[.//span[text()='Search By County:']]//select");
        private readonly By _countySearchInputLocator = By.XPath("//tr[.//span[text()='Search By County:']]//input");
        private readonly By _punResultsLocator = By.XPath("(//table[contains(@class,'DocTable')][.//thead//th[.='PUN'][not(contains(@class,'TVCH'))]])[1]/tbody/tr/td[1]");
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
        /// Search by county.
        /// </summary>
        /// <param name="countyIndex">The county SELECT index for the desired county.</param>
        public void ExportPunsByCounty(int countyIndex)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();

                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                DateTime expire = DateTime.Now.AddSeconds(10);
                IReadOnlyCollection<IWebElement> inputs = FindElements(_countySearchInputLocator);
                while (inputs.Count == 0 && DateTime.Now < expire)
                {
                    inputs = FindElements(_countySearchInputLocator);
                    Thread.Sleep(500);
                }
                inputs.ElementAt(0).Click();

                SelectElement select = new SelectElement(FindElement(_countySearchDropdownLocator));
                select.SelectByIndex(countyIndex);
                string countyName = select.SelectedOption.Text;
                writer.WritePropertyName(countyName);
                Console.WriteLine(countyName);
                Click(_searchButtonLocator);
                wait.Until(ExpectedConditions.StalenessOf(inputs.ElementAt(0)));

                // add loop for pages
                IReadOnlyCollection<IWebElement> punResults = FindElements(ExpectedConditions.VisibilityOfAllElementsLocatedBy(_punResultsLocator));
                writer.WriteValue($"{punResults.Count}");
                Console.WriteLine($"count: {punResults.Count}");
                writer.WritePropertyName("PUNs");
                writer.WriteStartArray();
                punResults.ToList().ForEach(e => writer.WriteValue(e.Text));
                //foreach (IWebElement pun in punResults)
                //{
                //    writer.WriteValue(pun.Text);
                //}
                // writer.WriteValue(string.Join(",", punResults.Select(e => e.Text)));
                Console.WriteLine(string.Join(",", punResults.Select(e => e.Text)));
                writer.WriteEndArray();
                writer.WriteEndObject();
                Console.WriteLine(sb.ToString());
                string filePath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory) + $@"\Output\";
                Directory.CreateDirectory(filePath);
                using (StreamWriter output = new StreamWriter(Path.Combine(filePath, $"{countyName}.json")))
                {
                    output.WriteLine(sb.ToString());
                }
            }
        }

        /// <summary>
        /// Search by county.
        /// </summary>
        public void ExportPunsByCounty_old()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();

                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                DateTime expire = DateTime.Now.AddSeconds(10);
                IReadOnlyCollection<IWebElement> inputs = FindElements(_countySearchInputLocator);
                while (inputs.Count == 0 && DateTime.Now < expire)
                {
                    inputs = FindElements(_countySearchInputLocator);
                    Thread.Sleep(500);
                }
                inputs.ElementAt(0).Click();

                SelectElement select = new SelectElement(FindElement(_countySearchDropdownLocator));
                for (int i = 1; i < 5; i++)
                // for (int i = 1; i < select.Options.Count; i++)
                {
                    select.SelectByIndex(i);
                    writer.WritePropertyName(select.SelectedOption.Text);
                    Console.WriteLine(select.SelectedOption.Text);
                    var punResults = FindElements(_punResultsLocator);
                    Click(_searchButtonLocator);
                    wait.Until(ExpectedConditions.StalenessOf(i == 1 ? inputs.ElementAt(0) : punResults.ElementAt(0)));
                    // add loop for pages
                    select = new SelectElement(FindElement(ExpectedConditions.ElementIsVisible(_countySearchDropdownLocator)));
                    punResults = FindElements(ExpectedConditions.VisibilityOfAllElementsLocatedBy(_punResultsLocator));
                    writer.WriteValue($"{punResults.Count}");
                    Console.WriteLine($"count: {punResults.Count}");
                    writer.WritePropertyName("PUNs");
                    writer.WriteStartArray();
                    foreach (IWebElement pun in punResults)
                    {
                        writer.WriteValue(pun.Text);
                    }
                    // writer.WriteValue(string.Join(",", punResults.Select(e => e.Text)));
                    Console.WriteLine(string.Join(",", punResults.Select(e => e.Text)));
                    writer.WriteEndArray();
                }
                writer.WriteEndObject();
                Console.WriteLine(sb.ToString());
            }
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