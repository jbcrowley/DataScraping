using CsvHelper;
using System.Globalization;

namespace SeleniumFramework.TestData
{
    public class CsvPunData : ITestData
    {
        /// <summary>
        /// Returns a collection of data from a CSV file for the current test
        /// </summary>
        /// <returns>The data for the current test</returns>
        public Dictionary<string, string> GetTestData()
        {
            using (StreamReader reader = new StreamReader(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory) + @"\TestData\TestData.csv"))
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<TestData>().Where(d => d.TestName == TestContext.CurrentContext.Test.Name).ToDictionary(x => x.Property, y => y.Value);
            }
        }
    }
}