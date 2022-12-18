using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Text;


public class Video
{
    public string Link { get; set; }
    public string Titel { get; set; }
    public string Weergaves { get; set; }
    public string Uploader { get; set; }
}


internal class Program
{
    public static void Main(string[] args)
    {

        StringBuilder csv = new StringBuilder();
        string seperator = ",";
        string[] headings = { "link", "titel", "weergaves", "uploader" };
        csv.AppendLine(string.Join(seperator, headings));
        Console.WriteLine("Welke zoekterm wilt u opzoeken?");
        string zoekterm = Console.ReadLine();
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://www.youtube.com/results?search_query="+zoekterm+"&sp=CAI%253D");
        Thread.Sleep(5000);
        driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/div[6]/div[1]/ytd-button-renderer[2]/yt-button-shape/button/yt-touch-feedback-shape/div/div[2]")).Click();
        Thread.Sleep(10000);

        var link = driver.FindElements(By.XPath("//*[@id=\"video-title\"]"));
        var titel = driver.FindElements(By.XPath(".//*[@id=\"video-title\"]/yt-formatted-string"));
        var weergaves = driver.FindElements(By.XPath("//*[@id=\"metadata-line\"]/span[1]"));
        var uploader = driver.FindElements(By.XPath("//*[@id=\"text\"]/a"));

        List<Video> videoList = new List<Video> { new Video { Link = link.ElementAt(0).GetAttribute("href"), Titel = titel.ElementAt(0).Text, Weergaves = weergaves.ElementAt(0).Text, Uploader = uploader.ElementAt(0).Text } };
        for (var i = 0; i < 5; i++)
        {
            string[] tekst = { link.ElementAt(i).GetAttribute("href"), titel.ElementAt(i).Text, weergaves.ElementAt(i).Text, uploader.ElementAt(i).Text };
            csv.AppendLine(string.Join(seperator, tekst));
            
        }
        
        File.WriteAllText(@"C:\Users\jbael\source\repos\WebscraperVoorbeeld\WebscraperVoorbeeld\file.csv", csv.ToString());
        driver.Close();

    }
}