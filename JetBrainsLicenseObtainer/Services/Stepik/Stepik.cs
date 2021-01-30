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
    }
}
