using OpenQA.Selenium;

namespace EcsDigitalProject.PageObjects
{
    public class HomePage : BasePage
    {
        public IWebElement RenderTheChallengeBtn => Driver.FindElement(By.CssSelector("[data-test-id='render-challenge']"));

        public void SelectRenderTheChallengeButton()
        {
            WaitUntilElementIsVisible(By.CssSelector("[data-test-id='render-challenge']"));
            RenderTheChallengeBtn.Click();
            WaitUntilElementIsVisible(By.Id("challenge"));
        }
    }
}