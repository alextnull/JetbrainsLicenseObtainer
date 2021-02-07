using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace JetBrainsLicenseObtainer.Services.Stepik.Helpers
{
    public static class Wait
    {
        #region Methods

        /// <summary>
        /// Waits until element becomes visible.
        ///  Visibility means that the element is not only displayed but also has a height and width that is greater than 0.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static IWebElement UntilElementIsVisible(IWebDriver driver, By locator, int seconds = 25)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /// <summary>
        /// Waits until element loaded.
        /// This does not necessarily mean that the element is visible.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static IWebElement UntilElementIsExist(IWebDriver driver, By locator, int seconds = 25)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementExists(locator));
        }

        #endregion
    }
}
