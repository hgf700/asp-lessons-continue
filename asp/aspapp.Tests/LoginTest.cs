using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspapp.Tests
{
    public class LoginTest : IDisposable
    {
        private readonly IWebDriver driver;
        public LoginTest()
        {
            var options = new EdgeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("ignore-certificate-errors");

            var service = EdgeDriverService.CreateDefaultService();
            driver = new EdgeDriver(service, options);
        }

        [Fact]
        public void Login_Test()
        {
            driver.Navigate().GoToUrl("https://localhost:7205/Identity/Account/Login");

            driver.FindElement(By.Id("Input_Email")).SendKeys("Asd@asdasd2.comzz");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Asd@asdasd2.comzz");

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
