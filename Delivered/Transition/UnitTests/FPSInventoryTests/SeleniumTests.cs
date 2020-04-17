using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Html5;
using System;
using System.Threading;
namespace FPSInventoryTests
{
    [TestFixture]
    public class Tests
    {
        static IWebDriver driver; //WebDriver
        static readonly string websiteURL = "http://localhost:5000"; //Website url
        public static string GenerateRandomValidName(int length)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];

            for (int x = 2; x < length; x++) //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            {
                Name += consonants[r.Next(consonants.Length)];
                x++;
                Name += vowels[r.Next(vowels.Length)];
            }
            return Name;
        } // This method is used when you need to generate a 
        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver(); //Instance of driver
            driver.Navigate().GoToUrl(websiteURL); //Navigate to the website url
            LogIn(); //Log in to application
        }
        [TearDown]
        public void TearDown()
        {
            LogOut(); //Logs out of application
            driver.Quit(); //Quits browser window
        }
        public void AcceptCookies()
        {
            try
            {
                IWebElement cookieConsentButton = driver.FindElement(By.ClassName("accept-policy"));
                cookieConsentButton.Click(); //Click accept button
            }
            catch
            {

            }
        } //Accepts cookie consent
        public void LogIn()
        {
            AcceptCookies();
            IWebElement loginInput = driver.FindElement(By.Id("Idemployee"));
            loginInput.SendKeys("1");
            loginInput.SendKeys(Keys.Enter);
        }
        public void LogOut()
        {
            driver.Navigate().GoToUrl(websiteURL + "/?logout=1"); //Opens browser and goes to URL
            AcceptCookies();
        }
        public void CreateCategory(string categoryName)
        {
            string url = "/Category/Create"; //Goes to URL
            driver.Navigate().GoToUrl(websiteURL + url);
            IWebElement categoryNameInput = driver.FindElement(By.Id("Namecategory"));
            categoryNameInput.SendKeys(categoryName);
            categoryNameInput.Submit();
        }
        public void DeleteCategory(string categoryName)
        {
            IWebElement categoryEntry = driver.FindElement(By.PartialLinkText(categoryName));

            string url = "/Category/Delete/";
            string[] elementSplit = categoryEntry.GetAttribute("href").Split('/');

            url += elementSplit[^1];
            IWebElement categoryDeleteButton = driver.FindElement(By.XPath("//a[@href='" + url + "']")); //Finds element by href attribute
            categoryDeleteButton.Click();
            IWebElement confirmDeleteButton = driver.FindElement(By.ClassName("btn-danger"));
            confirmDeleteButton.Click();
        }
        public void EditCategory(string categoryName, string newCategoryName)
        {
            IWebElement categoryEntry = driver.FindElement(By.PartialLinkText(categoryName));

            string editCategoryURL = "/Category/Edit/";
            string[] elementSplit = categoryEntry.GetAttribute("href").Split('/');

            editCategoryURL += elementSplit[^1];
            IWebElement categoryEditButton = driver.FindElement(By.XPath("//a[@href='" + editCategoryURL + "']")); // Finds element by href attribute
            categoryEditButton.Click();
            IWebElement categoryEditInput = driver.FindElement(By.Id("Namecategory"));
            categoryEditInput.Clear();
            categoryEditInput.SendKeys(newCategoryName);
            categoryEditInput.SendKeys(Keys.Enter);
        }
        public void CreateProduct(string productName, string categoryName)
        {
            string createProductURL = "/Product/Create";
            driver.Navigate().GoToUrl(websiteURL + createProductURL);
            IWebElement productNameInput = driver.FindElement(By.Id("Product1"));
            IWebElement productCategorySelect = driver.FindElement(By.Id("IdCategory"));
            productNameInput.SendKeys(productName);
            productCategorySelect.SendKeys(categoryName);
            productNameInput.Submit();
        }
        [Test]
        public void TestIfRedirectsToHomeAfterLogIn()
        {
            LogIn();
            Assert.AreEqual((websiteURL + "/home").ToLower(), driver.Url.ToLower());
        }
        [Test]
        public void TestIfRejectsInvalidUser()
        {
            LogOut();
            IWebElement loginInput = driver.FindElement(By.Id("Idemployee"));
            loginInput.SendKeys("-1");
            loginInput.SendKeys(Keys.Return);
            IWebElement errorMessage = driver.FindElement(By.ClassName("text-danger"));
            bool messageWasDisplayed = errorMessage.GetAttribute("textContent").Contains("That Employee is not on file");
            Assert.IsTrue(messageWasDisplayed);
        }
        [Test]
        public void TestIfLoggingOut()
        {
            string logOutURL = "/?logout=1";
            IWebElement logOutButton = driver.FindElement(By.XPath("//a[@href='" + logOutURL + "']"));
            logOutButton.Click();
            IWebElement logOutMessage = driver.FindElement(By.ClassName("text-danger"));
            bool logOutSuccessful = logOutMessage.GetAttribute("textContent").Contains("You have successfully logged out");
            Assert.IsTrue(logOutSuccessful);
        }
        [Test]
        public void TestCreateCategory()
        {
            string categoryName = GenerateRandomValidName(5);
            CreateCategory(categoryName);
            IWebElement categoryEntry = driver.FindElement(By.PartialLinkText(categoryName));
            bool containsCategory = categoryEntry.GetAttribute("textContent").Contains(categoryName);
            Assert.IsTrue(containsCategory);
            DeleteCategory(categoryName);
        }
        [Test]
        public void TestDeleteCategory()
        {
            string categoryName = GenerateRandomValidName(6);
            CreateCategory(categoryName);
            DeleteCategory(categoryName);
            bool success = driver.FindElements(By.PartialLinkText(categoryName)).Count == 0;
            Assert.IsTrue(success);
        }
        [Test]
        public void TestRejectDuplicateCategory() //Tries to create same category twice. Should fail at second time.
        {
            string categoryName = GenerateRandomValidName(5);
            CreateCategory(categoryName);
            CreateCategory(categoryName);
            IWebElement fieldValidationError = driver.FindElement(By.ClassName("field-validation-error"));
            bool isErrorDisplayed = fieldValidationError.GetAttribute("textContent").Contains("A category with that Name already exists");
            Assert.IsTrue(isErrorDisplayed);
            DeleteCategory(categoryName);
        }
        [Test]
        public void TestEditCategory() //Tries to edit a created category.
        {
            string categoryName = GenerateRandomValidName(10);
            string newCategoryName = GenerateRandomValidName(11);
            CreateCategory(categoryName);
            EditCategory(categoryName, newCategoryName);
            IWebElement categoryEntry = driver.FindElement(By.PartialLinkText(newCategoryName));
            bool containsCategory = categoryEntry.GetAttribute("textContent").Contains(newCategoryName);
            Assert.IsTrue(containsCategory);
            DeleteCategory(newCategoryName);
        }
        [Test]
        public void TestRejectsProductCreationWithEmptyName()
        {
            string url = "/Product/Create";
            driver.Navigate().GoToUrl(websiteURL + url);
            IWebElement categoryNameInput = driver.FindElement(By.Id("Product1"));
            categoryNameInput.Submit();
            IWebElement errorMessage = driver.FindElement(By.Id("Product1-error"));
            bool isErrorMessageDisplayed = errorMessage.GetAttribute("textContent").Contains("The Product Name field is required.");
            Assert.IsTrue(isErrorMessageDisplayed);
        }
        [Test]
        public void TestRejectsProductWithEmptyCategory()
        {
            string productName = GenerateRandomValidName(7);
            CreateProduct(productName, "");
            bool isInvalid = true;
            try
            {
                driver.FindElement(By.ClassName("input-validation-error"));
            }catch (NoSuchElementException)
            {
                isInvalid = false;
            }
            Assert.IsTrue(isInvalid);
        }
        [Test]
        public void TestCreateProduct()
        {
            string name = GenerateRandomValidName(8);
            string category = GenerateRandomValidName(9);
            CreateCategory(category);
            CreateProduct(name, category);
            bool elementCreated = driver.PageSource.Contains(name);
            Assert.IsTrue(elementCreated);
        }
    }
}