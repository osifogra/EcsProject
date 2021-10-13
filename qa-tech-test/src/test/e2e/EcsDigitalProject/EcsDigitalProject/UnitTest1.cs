using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace EcsDigitalProject
{
    public class Tests
    {
        public IWebDriver Driver { get; set; }
        public const int TimeDuration = 30;

        public List<int> listOfNumbersInArray = new List<int>();
        public List<int> arrayOne = new List<int>();
        public List<int> arrayTwo = new List<int>();
        public int arrayOneSumValue;
        public int arrayTwoSumValue;

        [SetUp]
        public void Setup()
        {
            switch ("chrome")
            {
                case "chrome":
                    Console.WriteLine("Opening Chrome Browser");
                    Driver = new ChromeDriver();
                    break;

                default:
                    throw new Exception("Wrong browser specified");
            }
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("http://localhost:3000/");
        }


        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Console.WriteLine("\r\n" + "Close Browser" + "\r\n");
                Driver.Quit();
                Driver = null;
            }
        }

        public void WaitUntilElementIsVisible(By locator, int waitDuration = TimeDuration)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitDuration));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public void SelectArray(int arrayNo)
        {
            arrayNo--;
            int? tableRowsCount = null;
            IWebElement tableRow;
            ReadOnlyCollection<IWebElement> tableData;

            try
            {
                tableRowsCount = Driver.FindElements(By.CssSelector("tr")).Count;
                tableRow = Driver.FindElements(By.CssSelector("tr"))[arrayNo];
                tableData = tableRow.FindElements(By.CssSelector("td"));

                foreach (var item in tableData)
                {
                    listOfNumbersInArray.Add(Int32.Parse(item.Text));
                }
            }
            catch (Exception)
            {
                throw new Exception($"Array number {arrayNo} is Array out of range. Please enter between 1 and {tableRowsCount}");
            }
        }

        public void SelectIndex(int indexNo)
        {
            if (indexNo >= listOfNumbersInArray.Count)
            {
                throw new Exception($"Index number {indexNo} entered is greater than array count.");
            }
            else
            {
                foreach (var item in listOfNumbersInArray)
                {
                    if (arrayOne.Count < indexNo)
                    {
                        arrayOne.Add(item);
                    }
                    else
                    {
                        arrayTwo.Add(item);
                    }
                }

                arrayTwo.RemoveAt(0);

                arrayOneSumValue = arrayOne.Sum();
                arrayTwoSumValue = arrayTwo.Sum();
            }
        }



        [Test]
        public void Test1()
        {
            WaitUntilElementIsVisible(By.CssSelector("[data-test-id='render-challenge']"));

            Driver.FindElement(By.CssSelector("[data-test-id='render-challenge']")).Click();
            WaitUntilElementIsVisible(By.Id("challenge"));

            SelectArray(1);
            SelectIndex(4);

            Assert.AreEqual(arrayOneSumValue, arrayTwoSumValue);

            arrayOneSumValue = arrayOne.Sum();
            arrayTwoSumValue = arrayTwo.Sum();

            var arrayOneAndTwoCalculationWorkout = $"{string.Join(" + ", arrayOne)} = {arrayOneSumValue} and {string.Join(" + ", arrayTwo)} = { arrayTwoSumValue}";
            Driver.FindElement(By.CssSelector($"[data-test-id='submit-1']")).SendKeys(arrayOneAndTwoCalculationWorkout);

            listOfNumbersInArray.Clear();
            arrayOne.Clear();
            arrayTwo.Clear();

            Driver.FindElement(By.CssSelector("[data-test-id='submit-4']")).SendKeys("Graham Os");
            Driver.FindElements(By.CssSelector("[type='button']"))[1].Click();
        }
    }
}