using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace EcsDigitalProject.UtilityHelpers
{
    public class DriverManager
    {
        public IWebDriver Driver { get; set; }

        public const int TimeDuration = 30;

        public void WaitUntilElementIsVisible(By locator, int waitDuration = TimeDuration)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitDuration));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }
    }
}
