using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspapp.Tests
{
    public class RegisterTest : IDisposable
    {
        private readonly IWebDriver driver;
        public RegisterTest()
        {
            var options = new EdgeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("ignore-certificate-errors");

            var service = EdgeDriverService.CreateDefaultService();
            driver = new EdgeDriver(service, options);
        }

        [Fact]
        public void Register_Test()
        {
            driver.Navigate().GoToUrl("https://localhost:7205/Identity/Account/Register");

            driver.FindElement(By.Id("Input_Email")).SendKeys("Asd@asdasd2.comzz");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Asd@asdasd2.comzz");
            driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("Asd@asdasd2.comzz");

            var roleSelect = new SelectElement(driver.FindElement(By.Id("Input_SelectedRole")));
            roleSelect.SelectByText("Admin");

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
