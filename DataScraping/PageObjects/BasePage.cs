using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SeleniumFramework.PageObjects
{
    public class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        /// <summary>
        /// Clicks the element.
        /// </summary>
        /// <param name="locator">The By locator for the desired element.</param>
        /// <param name="timeOutSeconds">[Optional] How long to wait for the element. The default is 10.</param>
        public void Click(By locator, int timeOutSeconds = 10)
        {
            DateTime expire = DateTime.Now.AddSeconds(timeOutSeconds);
            while (DateTime.Now < expire)
            {
                try
                {
                    new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutSeconds)).Until(ExpectedConditions.ElementToBeClickable(locator)).Click();

                    return;
                }
                catch (Exception e) when (e is ElementClickInterceptedException || e is StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to click element <{locator}> within {timeOutSeconds}s.");
        }

        /// <summary>
        /// Sends the provided string to the element.
        /// </summary>
        /// <param name="locator">The By locator for the desired element.</param>
        /// <param name="text">The string to be typed.</param>
        /// <param name="timeOutSeconds">[Optional] How long to wait for the element. The default is 10.</param>
        public void SendKeys(By locator, string text, int timeOutSeconds = 10)
        {
            DateTime expire = DateTime.Now.AddSeconds(timeOutSeconds);
            while (DateTime.Now < expire)
            {
                try
                {
                    new WebDriverWait(Driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(locator)).SendKeys(text);

                    return;
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to .SendKeys() to element <{locator}> within {timeOutSeconds}s.");
        }

        /// <summary>
        /// Determines if the element exists.
        /// </summary>
        /// <param name="locator">The By locator for the desired element.</param>
        /// <returns>true if the element exists, false otherwise.</returns>
        public bool ElementExists(By locator)
        {
            return FindElements(locator).Any();
        }

        /// <summary>
        /// Finds a single element with the provided locator.
        /// </summary>
        /// <param name="locator">The By locator for the desired element.</param>
        /// <returns>The found element.</returns>
        public IWebElement FindElement(By locator)
        {
            return Driver.FindElement(locator);
        }

        /// <summary>
        /// Finds a single element using the provided wait condition.
        /// </summary>
        /// <param name="waitCondition">The wait condition can be an ExpectedCondition or a custom wait that returns an IWebElement.</param>
        /// <param name="timeOutSeconds">[Optional] How long to wait for the element. The default is 10.</param>
        /// <returns>The found element.</returns>
        public IWebElement FindElement(Func<IWebDriver, IWebElement> waitCondition, int timeOutSeconds = 10)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutSeconds)).Until(waitCondition);
        }

        /// <summary>
        /// Finds all elements with the provided locator.
        /// </summary>
        /// <param name="locator">The By locator for the desired element.</param>
        /// <returns>The collection of elements.</returns>
        public IReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }

        /// <summary>
        /// Finds all elements using the provided wait condition.
        /// </summary>
        /// <param name="waitCondition">The wait condition can be an ExpectedCondition or a custom wait that returns a boolean.</param>
        /// <param name="timeOutSeconds">[Optional] How long to wait for the element. The default is 10.</param>
        /// <returns>true if the waitCondition succeeds, false otherwise.</returns>
        public bool FindElements(Func<IWebDriver, bool> waitCondition, int timeOutSeconds = 10)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutSeconds)).Until(waitCondition);
        }

        /// <summary>
        /// Finds all elements using the provided wait condition.
        /// </summary>
        /// <param name="waitCondition">The wait condition can be an ExpectedCondition or a custom wait that returns a collection of IWebElement.</param>
        /// <param name="timeOutSeconds">[Optional] How long to wait for the element. The default is 10.</param>
        /// <returns>The collection of found elements.</returns>
        public IReadOnlyCollection<IWebElement> FindElements(Func<IWebDriver, IReadOnlyCollection<IWebElement>> waitCondition, int timeOutSeconds = 10)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutSeconds)).Until(waitCondition);
        }

        /// <summary>
        /// Returns the text of the element.
        /// </summary>
        /// <param name="locator">The By locator for the desired element.</param>
        /// <returns>The text contained in the element.</returns>
        public string GetText(By locator)
        {
            int timeOutSeconds = 10;
            DateTime expire = DateTime.Now.AddSeconds(timeOutSeconds);
            while (DateTime.Now < expire)
            {
                try
                {
                    return FindElement(ExpectedConditions.ElementIsVisible(locator)).Text;
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to get .Text from element <{locator}> within {timeOutSeconds}s.");
        }

        /// <summary>
        /// Returns the value of the element.
        /// </summary>
        /// <param name="locator">The By locator for the desired element.</param>
        /// <returns>The value of the element.</returns>
        public string GetValue(By locator)
        {
            int timeOutSeconds = 10;
            DateTime expire = DateTime.Now.AddSeconds(timeOutSeconds);
            while (DateTime.Now < expire)
            {
                try
                {
                    return FindElement(ExpectedConditions.ElementIsVisible(locator)).GetAttribute("value");
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to get 'value' from element <{locator}> within {timeOutSeconds}s.");
        }

        /// <summary>
        /// Determines if an element is displayed with an optional wait.
        /// </summary>
        /// <param name="locator">The By locator for the desired element.</param>
        /// <param name="timeOutSeconds">[Optional] How long to wait for the element. The default is 10.</param>
        /// <returns>true if displayed, false otherwise</returns>
        public bool IsElementDisplayed(By locator, int timeOutSeconds = 10)
        {
            try
            {
                FindElement(ExpectedConditions.ElementIsVisible(locator), timeOutSeconds);

                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}