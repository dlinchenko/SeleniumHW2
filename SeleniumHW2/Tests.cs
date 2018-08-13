using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumHW2
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private Actions _action;

        [SetUp]
        public void TestSetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _action = new Actions(_driver);

        }

        [TearDown]
        public void TestTearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void Test1()
        {
            _driver.Url ="http://toolsqa.com";

            var demoSites = _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("DEMO SITES")));
            
            _action.MoveToElement(demoSites).Perform();

            var targetElement = _wait.Until(ExpectedConditions.ElementIsVisible((By.LinkText("Automation Practice Switch Windows"))));

            targetElement.Click();

            var newTabElement = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='content']//button[@onclick='newBrwTab()']")));

            newTabElement.Click();

            Assert.That(_driver.SwitchTo().Window(_driver.WindowHandles[1]).Title, Is.EqualTo("QA Automation Tools Tutorial"));

        }

        [Test]
        public void Test2()
        {
            _driver.Url = "http://toolsqa.com/automation-practice-switch-windows/";

            var timingAlertButton = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("timingAlert")));
            timingAlertButton.Click();

            var alert = _wait.Until(ExpectedConditions.AlertIsPresent());

            Assert.That(alert.Text, Is.EqualTo("Knowledge increases by sharing but not by saving. Please share this website with your friends and in your organization."));

        }


        [Test]
        public void Test3()
        {
            _driver.Url = "https://www.w3schools.com/hTml/html_iframe.asp";

            var frame = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//iframe[@src='default.asp' and @height='310px' and @width='99%']")));

            _driver.SwitchTo().Frame(frame);
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main']/div[2]/a[2]"))).Click();
            var frameTitle = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main']/h1"))).Text;
            Assert.That(_driver.Title, Is.EqualTo("HTML Iframes"));
            Assert.That(frameTitle, Is.EqualTo("HTML Introduction"));

        }

    }   
}
