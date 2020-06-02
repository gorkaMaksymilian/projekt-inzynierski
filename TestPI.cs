using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;


namespace PI {
    public class TestPI {
        IWebDriver driver;

        [SetUp]
        public void Initialize() {
            driver = new ChromeDriver();

        }

        [Test]
        public void GenerateTestUsers() {
            driver.Url = "https://localhost:44358/GenerateTestScenarios.aspx";
            driver.FindElement(By.XPath("//html//body//form//div[3]//input")).Click();
        }

        // Scenario when user exist but he did not voted yet
        [Test]
        public void Login_UserExist() {
            GenerateTestUsers();
            driver.Url = "https://localhost:44358/Login.aspx";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            // Set email
            object email = driver.FindElement(By.XPath("//html//body//form//div[5]//input[1]"));
            js.ExecuteScript("arguments[0].setAttribute('value', 'tester@emailer.domainer')", email);
            // Set album number
            object album = driver.FindElement(By.XPath("//html//body//form//div[5]//input[2]"));
            js.ExecuteScript("arguments[0].setAttribute('value', '917161')", album);
            // Click button
            driver.FindElement(By.XPath("//html//body//form//div[5]//input[3]")).Click();
            // Assert that url changed
            Assert.AreEqual("https://localhost:44358/Survey.aspx", driver.Url);
        }

        // Scenario when user exist and he already voted
        [Test]
        public void Login_UserExistVoted() {
            GenerateTestUsers();
            driver.Url = "https://localhost:44358/Login.aspx";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            // Set email
            object email = driver.FindElement(By.XPath("//html//body//form//div[5]//input[1]"));
            js.ExecuteScript("arguments[0].setAttribute('value', 'test@email.domain')", email);
            // Set album number
            object album = driver.FindElement(By.XPath("//html//body//form//div[5]//input[2]"));
            js.ExecuteScript("arguments[0].setAttribute('value', '115511')", album);
            // Click button
            driver.FindElement(By.XPath("//html//body//form//div[5]//input[3]")).Click();
            // Assert correct error message appears
            Assert.AreEqual("Uzytkownik juz oddal glos! Sprawdz swoj mail.", driver.FindElement(By.XPath("//html//body//form//div[5]//span")).Text);
        }

        // Check specific user login action
        [Test]
        public void Login_UserCheckAnswers() {
            GenerateTestUsers();
            driver.Url = "https://localhost:44358/Login.aspx";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            // Set email
            object email = driver.FindElement(By.XPath("//html//body//form//div[5]//input[1]"));
            js.ExecuteScript("arguments[0].setAttribute('value', 'test@email.domain')", email);
            // Set album number
            object album = driver.FindElement(By.XPath("//html//body//form//div[5]//input[2]"));
            js.ExecuteScript("arguments[0].setAttribute('value', '115511')", album);
            // Click button
            driver.FindElement(By.XPath("//html//body//form//div[5]//input[4]")).Click();
            // Assert page changed
            Assert.AreEqual("https://localhost:44358/check.aspx", driver.Url);
        }

        [TearDown]
        public void EndTest() {
            driver.Close();
        }


    }
}