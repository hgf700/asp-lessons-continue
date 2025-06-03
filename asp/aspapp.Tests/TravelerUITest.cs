using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Xunit;

namespace aspapp.Tests
{
    public class TravelerUITest : IDisposable
    {
        private readonly IWebDriver driver;
        public TravelerUITest()
        {
            var options = new EdgeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("ignore-certificate-errors");

            var service = EdgeDriverService.CreateDefaultService();
            driver = new EdgeDriver(service, options);
        }

        [Fact]
        public void CreateTraveler_Form_SubmitsSuccessfully()
        {
            driver.Navigate().GoToUrl("https://localhost:7205/traveler/create");

            driver.FindElement(By.Id("Firstname")).SendKeys("Jan");
            driver.FindElement(By.Id("Lastname")).SendKeys("Kowalski");
            driver.FindElement(By.Id("Email")).SendKeys($"jan.kowalski@test.com");
            driver.FindElement(By.Id("BirthDate")).SendKeys("2002-05-17");

            driver.FindElement(By.CssSelector("form button[type='submit']")).Click();
            Thread.Sleep(1000);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
