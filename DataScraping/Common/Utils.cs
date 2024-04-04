using OpenQA.Selenium;

namespace SeleniumFramework.Common
{
    public class Utils
    {
        /// <summary>
        /// Returns a datetime stamped path in the \Logs folder.
        /// </summary>
        /// <returns>The full file path to the log file.</returns>
        public static string GenerateLogPath()
        {
            string path = $@"{Environment.GetEnvironmentVariable("USERPROFILE")}\Downloads\Logs";
            Directory.CreateDirectory(path);
            string filename = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyy.dd.MM_HH.mm.ss.fff}";

            return Path.Combine(path, filename);
        }

        /// <summary>
        /// Determines if an IMAGE is loaded.
        /// </summary>
        /// <param name="image">The IWebElement of the IMAGE tag.</param>
        /// <returns>true if the IMAGE is loaded, false otherwise.</returns>
        public static bool IsImageLoaded(IWebElement image)
        {
            return !image.GetAttribute("naturalWidth").Equals("0");
        }
    }
}