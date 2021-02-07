namespace JetBrainsLicenseObtainer.Models
{
    public class Solution
    {
        #region Properties

        public string Url { get; private set; }
        public string AnswerText { get; private set; }

        #endregion

        #region Constructor

        public Solution(string url, string answerText)
        {
            Url = url;
            AnswerText = answerText;
        }

        #endregion
    }
}
