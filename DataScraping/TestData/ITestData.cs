namespace SeleniumFramework.TestData
{
    interface ITestData
    {
        /// <summary>
        /// Returns a collection of data for the current test
        /// </summary>
        /// <returns>The data for the current test</returns>
        Dictionary<string, string> GetTestData();
    }
}
