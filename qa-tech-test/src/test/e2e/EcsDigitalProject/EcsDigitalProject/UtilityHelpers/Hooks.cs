using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace EcsDigitalProject.UtilityHelpers
{
    [Binding]
    public sealed class Hooks : DriverManager
    {
        private readonly IObjectContainer _objectContaniner;

        public Hooks(IObjectContainer objectContaniner)
        {
            _objectContaniner = objectContaniner;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            InitializeBrowser();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            CloseBrowser();
        }


        public void InitializeBrowser()
        {
            switch (ConfigManager.BrowsersType)
            {
                case "chrome":
                    Console.WriteLine("Opening Chrome Browser");
                    Driver = new ChromeDriver();
                    break;

                default:
                    Assert.Fail("Wrong browser specified");
                    break;
            }
            _objectContaniner.RegisterInstanceAs<IWebDriver>(Driver);
            Driver.Manage().Window.Maximize();
        }

        public void CloseBrowser()
        {
            if (Driver != null)
            {
                Console.WriteLine("\r\n" + "Close Browser" + "\r\n");
                Driver.Quit();
                Driver = null;
            }
        }
    }
}
