using System;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CloudQAAutomation
{
    public class RobustFormTest
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private const string URL = "https://app.cloudqa.io/home/AutomationPracticeForm";

        // Test Configuration
        private readonly Dictionary<string, string> _testData = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName", "Doe" },
            { "Email", "john.doe@example.com" },
            { "Mobile", "1234567890" },
            { "Gender", "Male" },
            { "DateOfBirth", "1990-01-01" } // Added date of birth
        };

        public void InitializeDriver()
        {
            var chromeOptions = new ChromeOptions();

            // Enable headless mode and disable sandbox for Docker
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--window-size=1920,1080");

            _driver = new ChromeDriver(chromeOptions);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        private IWebElement FindElementWithMultipleStrategies(By[] locators)
        {
            foreach (By locator in locators)
            {
                try
                {
                    return _wait.Until(driver => driver.FindElement(locator));
                }
                catch
                {
                    continue;
                }
            }
            throw new NotFoundException("Could not find element with any provided locator");
        }

        public void TestFirstNameField()
        {
            By[] firstNameLocators = new By[]
            {
                By.Id("fname"),
                By.Name("First Name"),
                By.XPath("//input[@placeholder='Name']"),
                By.CssSelector("input[name='First Name']")
            };

            var firstNameField = FindElementWithMultipleStrategies(firstNameLocators);
            firstNameField.Clear();
            firstNameField.SendKeys(_testData["FirstName"]);
            Console.WriteLine("First Name field test successful");
        }

        public void TestLastNameField()
        {
            By[] lastNameLocators = new By[]
            {
                By.Id("lname"),
                By.Name("Last Name"),
                By.XPath("//input[@placeholder='Surname']"),
                By.CssSelector("input[name='Last Name']")
            };

            var lastNameField = FindElementWithMultipleStrategies(lastNameLocators);
            lastNameField.Clear();
            lastNameField.SendKeys(_testData["LastName"]);
            Console.WriteLine("Last Name field test successful");
        }

        public void TestEmailField()
        {
            By[] emailLocators = new By[]
            {
                By.Id("email"),
                By.Name("Email"),
                By.XPath("//input[@placeholder='Email']"),
                By.CssSelector("input[type='text'][name='Email']")
            };

            var emailField = FindElementWithMultipleStrategies(emailLocators);
            emailField.Clear();
            emailField.SendKeys(_testData["Email"]);
            Console.WriteLine("Email field test successful");
        }

        public void TestMobileNumberField()
        {
            By[] mobileLocators = new By[]
            {
                By.Id("mobile"),
                By.Name("Mobile Number"),
                By.XPath("//input[@placeholder='Mobile Number']"),
                By.CssSelector("input[type='number'][maxlength='10']")
            };

            var mobileField = FindElementWithMultipleStrategies(mobileLocators);
            mobileField.Clear();
            mobileField.SendKeys(_testData["Mobile"]);
            Console.WriteLine("Mobile Number field test successful");
        }

        public void TestGenderField()
        {
            By[] genderLocators = new By[]
            {
                By.Id("male"),
                By.Id("female"),
                By.Id("transgender"),
                By.XPath("//input[@name='gender']")
            };

            var genderValue = _testData["Gender"].ToLower();

            // Explicit wait for the gender radio buttons to be present and clickable
            if (genderValue == "male")
            {
                var maleRadioButton = _wait.Until(driver => driver.FindElement(By.Id("male")));
                maleRadioButton.Click();
            }
            else if (genderValue == "female")
            {
                var femaleRadioButton = _wait.Until(driver => driver.FindElement(By.Id("female")));
                femaleRadioButton.Click();
            }
            else if (genderValue == "transgender")
            {
                var transgenderRadioButton = _wait.Until(driver => driver.FindElement(By.Id("transgender")));
                transgenderRadioButton.Click();
            }

            Console.WriteLine("Gender field test successful");
        }

        public void TestDateOfBirthField()
        {
            By[] dobLocators = new By[]
            {
                By.Id("dob"),
                By.Name("Date of Birth"),
                By.XPath("//input[@placeholder='YYYY-MM-DD']"),
                By.CssSelector("input[name='Date of Birth']")
            };

            var dobField = FindElementWithMultipleStrategies(dobLocators);
            dobField.Clear();
            dobField.SendKeys(_testData["DateOfBirth"]);
            Console.WriteLine("Date of Birth field test successful");
        }

        private void TakeScreenshot(string scenarioName)
        {
            try
            {
                // Ensure screenshots directory exists
                string screenshotDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotDir);

                // Generate unique filename
                string filename = $"{scenarioName}_Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string fullPath = Path.Combine(screenshotDir, filename);

                // Capture screenshot
                ITakesScreenshot screenshotDriver = (ITakesScreenshot)_driver;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(fullPath, ScreenshotImageFormat.Png);

                Console.WriteLine($"Screenshot saved: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Screenshot capture failed: {ex.Message}");
            }
        }

        public void RunAutomationTests()
        {
            try
            {
                InitializeDriver();
                _driver.Navigate().GoToUrl(URL);

                // Perform tests
                TestFirstNameField();
                TestLastNameField();
                TestEmailField();
                TestMobileNumberField();
                TestGenderField();
                TestDateOfBirthField(); // Added date of birth test

                // Take a single screenshot after all tests
                TakeScreenshot("FullPageCompletion");

                Console.WriteLine("All automation tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test execution failed: {ex.Message}");
                TakeScreenshot("ErrorState");
            }
            finally
            {
                _driver?.Quit();
            }
        }

        public static void Main(string[] args)
        {
            var automationTest = new RobustFormTest();
            automationTest.RunAutomationTests();
        }
    }
}
