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

        public static bool Login(IWebDriver driver, string email, string password)
        {
            bool isLogin;

            try
            {
                driver.Navigate().GoToUrl("https://stepik.org/login");
                Input.Fill(driver, Locator.EmailInput, email);
                Input.Fill(driver, Locator.PasswordInput, password);

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
