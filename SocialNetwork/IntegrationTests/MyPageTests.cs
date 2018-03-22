using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace IntegrationTests
{
    [TestFixture]
    public class MyPageTests
    {
        public static String baseUrl = "http://localhost:21665/";
        
        [Test(Description = "Пользователь может войти на сайт, используя свои логин и пароль")]
        public void UserCanEnter()
        {
            var driver = new FirefoxDriver();

            driver.Navigate().GoToUrl(baseUrl);

            driver.FindElement(By.Id("UserName")).SendKeys("test");
            driver.FindElement(By.Id("Password")).SendKeys("test");
            driver.FindElement(By.Id("LoginButton")).Click();

            Assert.AreEqual("1 1 1", driver.Title);
            Assert.AreEqual(baseUrl + "id1", driver.Url);
        }
    }
}
