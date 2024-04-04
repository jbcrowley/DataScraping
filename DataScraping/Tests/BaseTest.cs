using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using SeleniumFramework.Common;
using SeleniumFramework.TestData;
using Logger = SeleniumFramework.Common.Logger;
using Screenshot = SeleniumFramework.Common.Screenshot;

namespace SeleniumFramework.Tests
{
    public class BaseTest
    {
        /// <summary>
        /// The thread-safe driver instance.
        /// </summary>
        public ThreadLocal<IWebDriver> Driver = new ThreadLocal<IWebDriver>();

        /// <summary>
        /// The path to the globaldata.json file.
        /// </summary>
        public static string GlobalDataPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory) + @"\globaldata.json";

        /// <summary>
        /// Holds the global data from the JSON file.
        /// </summary>
        public static Dictionary<string, string> GlobalData { get; } = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(GlobalDataPath))!;

        /// <summary>
        /// Holds the URL for the landing page.
        /// </summary>
        public string Url;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Url = EnvironmentManager.LandingUrl;
        }

        [SetUp]
        public void Setup()
        {
            Driver.Value = WebDriverHelper.GetDriver();
            Driver.Value.Url = Url;
            Driver.Value.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            //if (TestExecutionContext.CurrentContext.CurrentResult.ResultState != ResultState.Success)
            //{
            //    try
            //    {
            //        Screenshot.TakeScreenshot(Driver.Value!);
            //    }
            //    catch (WebDriverException e)
            //    {
            //        Logger.Log($"DOM may not be ready, debug info not added: {e.Message}");
            //    }
            //}

            // Logger.AttachLog();

            Driver.Value?.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Driver.Dispose();
        }
    }
}