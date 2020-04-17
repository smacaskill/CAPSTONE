using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Html5;
using System;
using System.Threading;

namespace FPSInventory.Tests
{
    class SeleniumTests
    {
        static IWebDriver driver; // WebDriver
        static IJavaScriptExecutor javaScriptExecutor;
        static IWebElement InvalidInput
        {
            get
            {
                return driver.FindElement(By.CssSelector("input:invalid"));
            }
            set { InvalidInput = value; }
        } //Element that returns an input with value in an invalid format
        static readonly string websiteURL = "http://localhost:5500"; //Website url
        public static void SetElements()
        {

            Console.WriteLine("Elements Set");
        } //Initializes all element variables

        [SetUp]
        public static void SetUpTest()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(websiteURL); //Opens browser and goes to URL
            javaScriptExecutor = (IJavaScriptExecutor)driver;
            Console.WriteLine("URL open in browser window");
            SetElements();
            Console.WriteLine("Elements set");
        }
        [TearDown]
        public static void TearDownTest()
        {
            driver.Close();
            Console.WriteLine("Test Browser window closed");

