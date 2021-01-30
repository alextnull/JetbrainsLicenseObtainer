﻿using JetBrainsLicenseObtainer.Models;
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
            _chromeDriver.Manage().Cookies.DeleteAllCookies();
            _chromeDriver.Manage().Window.Maximize();

            Account account = UserInfo.GenerateAccount();
            bool isAccountRegistrated = RegistrationPage.Registrate(_chromeDriver, account);

            if (isAccountRegistrated) return account;

            return null;
        }

        #endregion
    }
}
