using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Text;


public class Vacature
{
    public string Titel { get; set; }
    public string Bedrijf { get; set; }
    public string Locatie { get; set; }
    public string Keywords { get; set; }
    public string Link { get; set; }
}


internal class Program
{
    public static void Main(string[] args)
    {

        StringBuilder csv = new StringBuilder();
        string seperator = ",";
        string[] headings = { "link", "titel", "bedrijf", "locatie", "keywords" };
        csv.AppendLine(string.Join(seperator, headings));
        Console.WriteLine("Welke zoekterm wilt u opzoeken?");
        string zoekterm = Console.ReadLine();
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://www.ictjob.be/nl/it-vacatures-zoeken?keywords="+zoekterm);
        Thread.Sleep(5000);
        driver.FindElement(By.XPath("//*[@id=\"body-ictjob\"]/div[2]/a")).Click();
        driver.FindElement(By.Id("sort-by-date")).Click();

        Thread.Sleep(10000);

        var link = driver.FindElements(By.ClassName("search-item-link"));
        var titel = driver.FindElements(By.ClassName("job-title"));
        var bedrijf = driver.FindElements(By.ClassName("job-company"));
        var locatie = driver.FindElements(By.ClassName("job-location"));
        var keywords = driver.FindElements(By.ClassName("job-keywords"));

        List<Vacature> vacatureList = new List<Vacature> { new Vacature { Link = link.ElementAt(0).GetAttribute("href"), Titel = titel.ElementAt(0).Text, Bedrijf = bedrijf.ElementAt(0).Text, Locatie = locatie.ElementAt(0).Text, Keywords = keywords.ElementAt(0).Text } };
        for (var i = 0; i < 5; i++)
        {
            string[] tekst = { link.ElementAt(i).GetAttribute("href").ToString(), titel.ElementAt(i).Text, bedrijf.ElementAt(i).Text, locatie.ElementAt(i).Text, keywords.ElementAt(i).Text };
            csv.AppendLine(string.Join(seperator, tekst));

        }

        File.WriteAllText(@"C:\Users\jbael\source\repos\WebscraperICTjobs\WebscraperICTjobs\file.csv", csv.ToString());
        driver.Close();

    }
}
