using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechTalk.SpecFlow;
using EcsDigitalProject.PageObjects;
using EcsDigitalProject.UtilityHelpers;

namespace EcsDigitalProject.StepsDefs
{
    [Binding]
    public sealed class GivenStepDefs : BasePage
    {

        public GivenStepDefs(IWebDriver driver)
        {
            Driver = driver;
        }

        public List<int> listOfNumbersInArray = new List<int>();
        public List<int> arrayOne = new List<int>();
        public List<int> arrayTwo = new List<int>();
        public int arrayOneSumValue;
        public int arrayTwoSumValue;



        [Given(@"I navigate to site")]
        public void GivenINavigateToSite()
        {
            Driver.Navigate().GoToUrl(ConfigManager.WebSiteUrl);
        }

        [When(@"I select the challenge option")]
        public void WhenISelectTheChallengeOption()
        {
            CurrentPage<HomePage>().SelectRenderTheChallengeButton();
        }

        [When(@"I choose to select table row '(.*)'")]
        public void WhenIChooseToSelectTableRow(int arrayNo)
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

        [When(@"create array one with selected table row data up to index '(.*)' then miss the next index value and then create array two with the remaining data")]
        public void WhenCreateArrayOneWithSelectedTableRowDataUpToIndexThenMissTheNextIndexValueAndThenCreateArrayTwoWithTheRemainingData(int indexNo)
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
            }
        }

        [When(@"I choose to submit challenge '(.*)' arrays where the sum of integers at the index on the left is equal to the sum of integers on the right")]
        public void WhenIChooseToSubmitChallengeArraysWhereTheSumOfIntegersAtTheIndexOnTheLeftIsEqualToTheSumOfIntegersOnTheRight(string number)
        {
            arrayOneSumValue = arrayOne.Sum();
            arrayTwoSumValue = arrayTwo.Sum();

            var arrayOneAndTwoCalculationWorkout = $"{string.Join(" + ", arrayOne)} = {arrayOneSumValue} and {string.Join(" + ", arrayTwo)} = { arrayTwoSumValue}";
            Driver.FindElement(By.CssSelector($"[data-test-id='submit-{number}']")).SendKeys(arrayOneAndTwoCalculationWorkout);

            listOfNumbersInArray.Clear();
            arrayOne.Clear();
            arrayTwo.Clear();
        }

        [When(@"I choose to enter my name as '(.*)' and submit the challenge")]
        public void WhenIChooseToEnterMyNameAsAndSubmitTheChallenge(string name)
        {
            Driver.FindElement(By.CssSelector("[data-test-id='submit-4']")).SendKeys(name);
            Driver.FindElements(By.CssSelector("[type='button']"))[1].Click();
        }
    }
}
