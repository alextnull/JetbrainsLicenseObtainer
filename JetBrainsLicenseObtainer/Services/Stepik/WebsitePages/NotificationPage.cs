using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.Services.Stepik.Helpers;
using OpenQA.Selenium;
using System;

namespace JetBrainsLicenseObtainer.Services.Stepik.WebsitePages
{
    public static class NotificationPage
    {
        #region Structures

        private struct Locator
        {
            public static readonly By LicenseLink = By.XPath("//div[@class='notification-widget__text']/a");
            public static readonly By LicenseKey = By.XPath("//code[@class='license__activation-code']");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parse jetbrains license key from Stepik account 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="account"></param>
        /// <returns>Key or null if failed</returns>
        public static Key ParseKey(IWebDriver driver, Account account)
        {
            try
            {
                driver.Navigate().GoToUrl("https://stepik.org/notifications");
                IWebElement licenseLink = Wait.UntilElementIsVisible(driver, Locator.LicenseLink, 30);
                DateTime expirationDate = DateTime.Parse(licenseLink.Text.Split('(', ')')[1]);
                licenseLink.Click();

                IWebElement licenseKey = Wait.UntilElementIsVisible(driver, Locator.LicenseKey, 30);
                Key key = new Key(licenseKey.Text, DateTime.Now, expirationDate);

                return key;
            }
            catch
            {
                driver?.Quit();
                return null;
            }
        }

        #endregion
    }
}
