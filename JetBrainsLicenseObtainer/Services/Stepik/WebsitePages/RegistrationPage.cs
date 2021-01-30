using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.Services.Stepik.Helpers;
using OpenQA.Selenium;

namespace JetBrainsLicenseObtainer.Services.Stepik.WebsitePages
{
    static class RegistrationPage
    {
        #region Structures

        private struct Locator
        {
            public static readonly By FullnameInput = By.XPath("//input[@id='id_registration_full-name']");
            public static readonly By EmailInput = By.XPath("//input[@id='id_registration_email']");
            public static readonly By PasswordInput = By.XPath("//input[@id='id_registration_password']");
            public static readonly By RegistrateButton = By.XPath("//button[@class='sign-form__btn button_with-loader ']");
            public static readonly By ProfileButton = By.XPath("//button[@class='navbar__profile-toggler st-button_style_none']");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registrate Stepik account
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool Registrate(IWebDriver driver, Account account)
        {
            bool isRegistrate;

            try
            {
                driver.Navigate().GoToUrl("http://stepik.org/registration");
                FillInput(driver, Locator.FullnameInput, account.FullName);
                FillInput(driver, Locator.EmailInput, account.Email);
                FillInput(driver, Locator.PasswordInput, account.Password);
                IWebElement registrateButton = Wait.UntilElementIsVisible(driver, Locator.RegistrateButton, 30);
                registrateButton.Click();
                Wait.UntilElementIsVisible(driver, Locator.ProfileButton, 30);
                isRegistrate = true;
            }
            catch
            {
                isRegistrate = false;
                driver.Quit();
            }

            return isRegistrate;
        }

        /// <summary>
        /// Finds input using a locator and fill it with text
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="text"></param>
        private static void FillInput(IWebDriver driver, By locator, string text)
        {
            IWebElement element = Wait.UntilElementIsVisible(driver, locator, 20);
            element.SendKeys(text);
        }

        #endregion
    }
}
