using SeleniumFramework.PageObjects.TheInternet;
using SeleniumFramework.Tests;

namespace DataScraping.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class ExportPunsByCounty : BaseTest
    {
        [Test]
        [Category("Oklahoma")]
        [TestCaseSource("GetData")]
        public void ExportPunsByCountyOk(int countyIndex)
        {
            Driver.Value!.Url = "https://oktap.tax.ok.gov/OkTAP/web?link=PUBLICPUNLKP";

            PublicPunSearchPage publicPunSearchPage = new PublicPunSearchPage(Driver.Value!);
            publicPunSearchPage.ExportPunsByCounty(countyIndex);
            // write output to file, TBD
        }

        /// <summary>
        /// Gets the data from the specified CSV for the data driven test.
        /// </summary>
        /// <returns>The test data.</returns>
        private static IEnumerable<int> GetData()
        {
            // i must start at 1 to avoid the "Required" OPTION
            // int counties = 5;
            int counties = 78;
            for (int i = 1; i < counties; i++)
            {
                yield return i;
            }
        }
    }
}