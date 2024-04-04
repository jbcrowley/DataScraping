using CsvHelper;
using SeleniumFramework.PageObjects.TheInternet;
using SeleniumFramework.Tests;
using System.Globalization;

namespace DataScraping.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class PunScraping : BaseTest
    {
        [Test]
        [Category("Oklahoma")]
        [TestCaseSource("GetData")]
        public void PunScrapingOk(string pun)
        {
            Driver.Value!.Url = "https://oktap.tax.ok.gov/OkTAP/web?link=PUBLICPUNLKP";

            PublicPunSearchPage publicPunSearchPage = new PublicPunSearchPage(Driver.Value!);
            publicPunSearchPage.SearchByPun(pun);
            publicPunSearchPage.ClickPunLink(pun);

            PunDetailPage punDetailPage = new PunDetailPage(Driver.Value!);
            punDetailPage.ClickPrintableTab();
            List<string> output = punDetailPage.GetGeneralLeaseLegalInformation();
        }

        /// <summary>
        /// Gets the data from the specified CSV for the data driven test.
        /// </summary>
        /// <returns>The test data.</returns>
        private static IEnumerable<string> GetData()
        {
            using (StreamReader reader = new StreamReader(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory) + @"\TestData\PunData.csv"))
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    yield return csv[0];
                }
            }
        }
    }
}