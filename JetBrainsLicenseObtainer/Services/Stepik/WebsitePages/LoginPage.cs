using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.Services.Stepik.Helpers;
using OpenQA.Selenium;

namespace JetBrainsLicenseObtainer.Services.Stepik.WebsitePages
{
    static class LoginPage
    {
        #region Structures

        private struct Locator
        {
            public static readonly By EmailInput = By.XPath("//input[@name='login']");
            public static readonly By PasswordInput = By.XPath("//input[@name='password']");
            public static readonly By LoginButton = By.XPath("//button[@class='sign-form__btn button_with-loader ']");
            public static readonly By ProfileButton = By.XPath("//button[@class='navbar__profile-toggler st-button_style_none']");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Logins into Stepik account
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Login(IWebDriver driver, Account account)
        {
            bool isLogin;

            try
            {
                driver.Navigate().GoToUrl("https://stepik.org/login");
                Input.Fill(driver, Locator.EmailInput, account.Email);
                Input.Fill(driver, Locator.PasswordInput, account.Password);

                IWebElement loginButton = Wait.UntilElementIsVisible(driver, Locator.LoginButton, 30);
                loginButton.Click();
                Wait.UntilElementIsVisible(driver, Locator.ProfileButton, 30);

                isLogin = true;
            }
            catch
            {
                isLogin = false;
                driver.Quit();
            }

            return isLogin;
        }

        #endregion
    }
}
