using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace Tests
{
    [TestFixture]
    public class DefaultPageTests
    {
        public static String BaseUrl = "http://localhost:18214";

        private static FirefoxDriver _ffDriver = null;

        private FirefoxDriver GetFirefoxDriver()
        {
            if (_ffDriver == null)
            {
                _ffDriver = new FirefoxDriver();
                _ffDriver.Navigate().GoToUrl(BaseUrl);
            }

            return _ffDriver;
        }

        private void PrepareDateForTests(String year, String month, String day)
        {
            var driver = GetFirefoxDriver();

            driver.FindElement(By.Id("btnCreateCalendar")).Click();
            driver.FindElement(By.Id("btnCreateCalendar2")).Click();

            driver.FindElement(By.Id("txtYear")).SendKeys(year);
            driver.FindElement(By.Id("txtMonth")).SendKeys(month);
            driver.FindElement(By.Id("txtDate")).SendKeys(day);
            driver.FindElement(By.Id("btnSetSelectedDate")).Click();
        }

        private void ClickToCalendarButtonByIdPart(String btnIdPart)
        {
            var driver = GetFirefoxDriver();

            IWebElement elem = driver.FindElement(By.Id("CalendarPlace"));
            ReadOnlyCollection<IWebElement> buttons = elem.FindElements(By.TagName("input"));
            foreach (var webElement in buttons)
            {
                if (webElement.GetAttribute("id").Contains(btnIdPart))
                {
                    webElement.Click();
                    break;
                }
            }
        }

        [Test(Description = "Создание календарей")]
        public void UserCanCreateCalendar()
        {
            var driver = GetFirefoxDriver();

            driver.FindElement(By.Id("btnDeleteCalendar")).Click();
            driver.FindElement(By.Id("btnDeleteCalendar2")).Click();
            
            driver.FindElement(By.Id("btnCreateCalendar")).Click();
            
            Assert.IsNotEmpty(driver.FindElement(By.Id("CalendarPlace")).Text);
            Assert.IsEmpty(driver.FindElement(By.Id("CalendarPlace2")).Text);

            driver.FindElement(By.Id("btnCreateCalendar2")).Click();
            Assert.IsNotEmpty(driver.FindElement(By.Id("CalendarPlace2")).Text);
        }

        [Test(Description = "Удаление календарей")]
        public void UserCanDeleteCalendar()
        {
            var driver = GetFirefoxDriver();

            driver.FindElement(By.Id("btnCreateCalendar")).Click();
            driver.FindElement(By.Id("btnCreateCalendar2")).Click();

            driver.FindElement(By.Id("btnDeleteCalendar")).Click();

            Assert.IsEmpty(driver.FindElement(By.Id("CalendarPlace")).Text);
            Assert.IsNotEmpty(driver.FindElement(By.Id("CalendarPlace2")).Text);

            driver.FindElement(By.Id("btnDeleteCalendar2")).Click();

            Assert.IsEmpty(driver.FindElement(By.Id("CalendarPlace2")).Text);
        }
        
        [Test(Description = "Установка указанной даты")]
        public void UserCanSetSelectedDate()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010","1","1");

            IWebElement elem = driver.FindElement(By.Id("CalendarPlace"));
            Assert.IsTrue(elem.Text.Contains("Январь, 2010"));
            Assert.IsFalse(driver.FindElement(By.Id("CalendarPlace2")).Text.Contains("Январь, 2010"));
            Assert.IsTrue(elem.FindElement(By.ClassName("selectedDateCell")).Text == "1");
        }

        [Test(Description = "Получение выбранной даты")]
        public void UserCanGetSelectedDate()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010", "1", "1");

            driver.FindElement(By.Id("btnGetSelectedDate")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("txtSelectedDate")).GetAttribute("value") == "2010/01/01");
            Assert.IsEmpty(driver.FindElement(By.Id("txtSelectedDate2")).Text);
        }

        [Test(Description = "Изменение выбранной даты по клику мышкой на ячейке")]
        public void UserCanChangeSelectedDateByClick()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010", "1", "1");

            IWebElement elem = driver.FindElement(By.Id("CalendarPlace"));
            ReadOnlyCollection<IWebElement> cells = elem.FindElements(By.TagName("td"));
            foreach (var cell in cells)
            {
                if (cell.Text == "15")
                {
                    cell.Click();
                    break;
                }
            }

            Assert.IsTrue(elem.FindElement(By.ClassName("selectedDateCell")).Text == "15");
        }

        [Test(Description = "Установка выбранной даты с пустыми и строковыми значениями")]
        public void UserCanSetSelectedDateWithEmptyAndStringValues()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010", "1", "1");
            PrepareDateForTests("2010", "", "asdfgh");

            driver.FindElement(By.Id("btnGetSelectedDate")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("txtSelectedDate")).GetAttribute("value") == "2010/01/01");
            Assert.IsEmpty(driver.FindElement(By.Id("txtSelectedDate2")).Text);
        }

        [Test(Description = "Установка выбранной даты с некорректными значениями")]
        public void UserCanSetSelectedDateWithIncorrectValues()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010", "1", "1");
            PrepareDateForTests("-2012", "500", "5");

            driver.FindElement(By.Id("btnGetSelectedDate")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("txtSelectedDate")).GetAttribute("value") == "2010/01/05");
            Assert.IsEmpty(driver.FindElement(By.Id("txtSelectedDate2")).Text);
        }

        [Test(Description = "Переход на месяц назад")]
        public void UserCanGoMonthBack()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010", "6", "1");
            ClickToCalendarButtonByIdPart("btnMonthBack");
            driver.FindElement(By.Id("btnGetSelectedDate")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("CalendarPlace")).Text.Contains("Май, 2010"));
        }

        [Test(Description = "Переход на месяц вперед")]
        public void UserCanGoMonthForward()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010", "6", "1");
            ClickToCalendarButtonByIdPart("btnMonthForward");
            driver.FindElement(By.Id("btnGetSelectedDate")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("CalendarPlace")).Text.Contains("Июль, 2010"));
        }

        [Test(Description = "Переход на год назад")]
        public void UserCanGoYearBack()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010", "6", "1");
            ClickToCalendarButtonByIdPart("btnYearBack");
            driver.FindElement(By.Id("btnGetSelectedDate")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("CalendarPlace")).Text.Contains("Июнь, 2009"));
        }

        [Test(Description = "Переход на год вперед")]
        public void UserCanGoYearForward()
        {
            var driver = GetFirefoxDriver();
            PrepareDateForTests("2010", "6", "1");
            ClickToCalendarButtonByIdPart("btnYearForward");
            driver.FindElement(By.Id("btnGetSelectedDate")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("CalendarPlace")).Text.Contains("Июнь, 2011"));
        }
    }
}
