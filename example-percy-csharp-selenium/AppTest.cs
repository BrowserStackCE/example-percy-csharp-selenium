using System;
using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using percy_csharp_selenium;

namespace example_percy_csharp_selenium
{

    /**
    * Unit test for example App.
    */
    public class AppTest
    {
        private static readonly String TEST_URL = "http://localhost:8000";
        private static IWebDriver _driver;
        private static Percy _percy;
        private static TestServer _server;

        public void StartAppAndOpenBrowser()
        {
            _server = new TestServer();
            _server.StartTestServer();
            // Create a headless Chrome browser.
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new ChromeDriver(options);
            _percy = new Percy();
        }

        public void CloseBrowser()
        {
            // Close our test browser.
            _driver.Quit();
            _server.StopTestServer();
        }

        public void LoadsHomePage()
        {
            _driver.Navigate().GoToUrl(TEST_URL);
            IWebElement element = _driver.FindElement(By.ClassName("todoapp"));
            Assert.IsNotNull(element);
            // Take a Percy snapshot.
            _percy.Snapshot(_driver,"Home Page", null);
        }

        public void AcceptsANewTodo()
        {
            _driver.Navigate().GoToUrl(TEST_URL);

            // We start with zero todos.
            var todoEls = _driver.FindElements(By.CssSelector(".todo-list li"));
            Assert.AreEqual(0, todoEls.Count);
            // Add a todo in the browser.
            IWebElement newTodoEl = _driver.FindElement(By.ClassName("new-todo"));
            newTodoEl.SendKeys("A new fancy todo!");
            newTodoEl.SendKeys(Keys.Return);

            // Now we should have 1 todo.
            todoEls = _driver.FindElements(By.CssSelector(".todo-list li"));
            Assert.AreEqual(1, todoEls.Count);
            // Take a Percy snapshot specifying browser widths.
            _percy.Snapshot(_driver,"One todo", new Dictionary<string, object> { { "widths", new List<int> { 768, 992, 1200 } } });
            _driver.FindElement(By.ClassName("toggle")).Click();
            _driver.FindElement(By.ClassName("clear-completed")).Click();
        }

        public void LetsYouCheckOffATodo()
        {
            _driver.Navigate().GoToUrl(TEST_URL);

            IWebElement newTodoEl = _driver.FindElement(By.ClassName("new-todo"));
            newTodoEl.SendKeys("A new todo to check off");
            newTodoEl.SendKeys(Keys.Return);

            IWebElement todoCountEl = _driver.FindElement(By.ClassName("todo-count"));
            Assert.AreEqual("1 item left", todoCountEl.Text);

            _driver.FindElement(By.ClassName("toggle")).Click();

            todoCountEl = _driver.FindElement(By.ClassName("todo-count"));
            Assert.AreEqual("0 items left", todoCountEl.Text);

            // Take a Percy snapshot specifying a minimum height.
            _percy.Snapshot(_driver,"Checked off todo", new Dictionary<string, object> {
                { "minHeight", 1200 },
                { "enableJavaScript",  false },
                { "percyCSS", ".clear-completed { visibility: hidden; }" }
                });

        }

    }

}