using OpenQA.Selenium;

namespace JetBrainsLicenseObtainer.Services.Stepik.Helpers
{
    public static class Input
    {
        /// <summary>
        /// Finds input using a locator and fill it with text
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="text"></param>
        public static void Fill(IWebDriver driver, By locator, string text)
        {
            IWebElement element = Wait.UntilElementIsVisible(driver, locator, 20);
            element.SendKeys(text);
        }
    }
}
