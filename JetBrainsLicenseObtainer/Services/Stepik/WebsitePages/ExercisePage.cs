using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.Services.Stepik.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JetBrainsLicenseObtainer.Services.Stepik.WebsitePages
{
    class ExercisePage
    {
        #region Structures

        private struct Locator
        {
            public static readonly By HiddenTextarea = By.CssSelector(".code-editor__textarea");
            public static readonly By SubmitButton = By.XPath("//button[@class='submit-submission']");
        }

        #endregion

        #region Fields

        private static Solution[] solutions = new Solution[]
        {
            new Solution("https://stepik.org/lesson/13021/step/3", "#include <iostream>\nusing namespace std;\nint main()\n{\nint n, k;\ncin >> n >> k;\ncout << k / n;\nreturn 0;\n}"),
            new Solution("https://stepik.org/lesson/13021/step/4", "#include <iostream>\nusing namespace std;\nint main()\n{\nint n, k;\ncin >> n >> k;\ncout << k % n;\nreturn 0;\n}"),
            new Solution("https://stepik.org/lesson/13021/step/5", "#include <iostream>\nusing namespace std;\nint main()\n{\nint n;\ncin >> n;\ncout << n % 10;\nreturn 0;\n}"),
            new Solution("https://stepik.org/lesson/13021/step/6", "#include <iostream>\nusing namespace std;\nint main()\n{\nint n;\ncin >> n;\ncout << n / 10;\nreturn 0;\n}")
        };

        #endregion

        #region Methods

        /// <summary>
        /// Solves c++ exercises on Stepik.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool SolveTasks(IWebDriver driver)
        {
            bool isSolved;
            try
            {
                foreach (Solution solution in solutions)
                {
                    driver.Navigate().GoToUrl(solution.Url);
                    Wait.UntilElementIsExist(driver, Locator.HiddenTextarea, 120);
                    Thread.Sleep(TimeSpan.FromSeconds(3));

                    IWebElement textarea = driver.FindElement(Locator.HiddenTextarea);
                    textarea.RemoveAttributesJS(driver, "style", "readonly");
                    Thread.Sleep(TimeSpan.FromSeconds(2));

                    Actions actions = new Actions(driver).MoveToElement(textarea);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    textarea.TypeText(solution.AnswerText);
                    Thread.Sleep(TimeSpan.FromSeconds(2));

                    IWebElement submitButton = driver.FindElement(Locator.SubmitButton);
                    submitButton.Click();

                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                isSolved = true;
            }
            catch
            {
                isSolved = false;
                driver.Quit();
            }

            return isSolved;
        }

        #endregion
    }
}
