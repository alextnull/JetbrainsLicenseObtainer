using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.Services.Stepik.WebsitePages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace JetBrainsLicenseObtainer.Services.Stepik
{
    class Stepik
    {
        #region Fields

        private IWebDriver chromeDriver;

        #endregion

        #region Constructor

        public Stepik()
        {
            chromeDriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registrate Stepik account
        /// </summary>
        /// <returns>Account or null if registration failed</returns>
        public Account RegistrateAccount()
        {
            chromeDriver.Manage().Cookies.DeleteAllCookies();
            chromeDriver.Manage().Window.Maximize();

            Account account = UserInfo.GenerateAccount();
            bool isAccountRegistrated = RegistrationPage.Registrate(chromeDriver, account);

            if (isAccountRegistrated) return account;

            return null;
        }

        #endregion
    }
}
