using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace JetBrainsLicenseObtainer.Services.Stepik.Helpers
{
    public static class IWebElementExtension
    {
        public static void RemoveAttributesJS(this IWebElement element, IWebDriver driver, params string[] attributes)
        {
            foreach (string attribute in attributes)
            {
                driver.ExecuteJavaScript($"arguments[0].removeAttribute('{attribute}')", element);
            }
        }

        public static void TypeText(this IWebElement element, params string[] text)
        {
            element.SendKeys(Keys.Tab);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
            element.SendKeys(text[0]);
        }
    }
}
