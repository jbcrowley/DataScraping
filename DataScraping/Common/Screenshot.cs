using OpenQA.Selenium;

namespace SeleniumFramework.Common
{
    public class Screenshot
    {
        /// <summary>
        /// Takes a screenshot and attaches it to the current NUnit TestContext.
        /// </summary>
        /// <param name="driver">The current IWebDriver instance.</param>
        public static void TakeScreenshot(IWebDriver driver)
        {
            string fullFilePath = $"{Utils.GenerateLogPath()}.png";
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fullFilePath);
            TestContext.AddTestAttachment(fullFilePath);
        }
    }
}