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

        private IWebDriver _chromeDriver;

        #endregion

        #region Constructor

        public Stepik()
        {
            _chromeDriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registrate Stepik account
        /// </summary>
        /// <returns>Account or null if registration failed</returns>
        public Account RegistrateAccount()
        {
            _chromeDriver?.Manage().Cookies.DeleteAllCookies();
            _chromeDriver?.Manage().Window.Maximize();

            Account account = UserInfo.GenerateAccount();
            bool isAccountRegistrated = false;
            bool isTasksSolved = false;

            if (account.FullName != null && account.Email != null && account.Password != null)
            {
                isAccountRegistrated = RegistrationPage.Registrate(_chromeDriver, account);
                isTasksSolved = ExercisePage.SolveTasks(_chromeDriver);
            }

            if (isAccountRegistrated && isTasksSolved) return account;

            return null;
        }

        /// <summary>
        /// Trying to parse jetbrains license key from Stepik account
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Key or null if parsing failed </returns>
        public Key ParseKey(Account account)
        {
            _chromeDriver?.Manage().Cookies.DeleteAllCookies();
            _chromeDriver?.Manage().Window.Maximize();

            bool hasAuth = LoginPage.Login(_chromeDriver, account);

            if (hasAuth)
            {
                Key licenseKey = NotificationPage.ParseKey(_chromeDriver, account);
                return licenseKey;
            }

            return null;
        }

        /// <summary>
        /// Trying to close the browser driver and every associated widows
        /// </summary>
        /// <returns></returns>
        public bool CloseDriver()
        {
            bool isClosed = false;

            if (_chromeDriver != null)
            {
                isClosed = true;
                _chromeDriver.Quit();
            }

            return isClosed;
        }

        #endregion
    }
}
