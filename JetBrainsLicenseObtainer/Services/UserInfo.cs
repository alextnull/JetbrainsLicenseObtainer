using System;
using System.Xml;
using JetBrainsLicenseObtainer.Models;
using RestSharp;

namespace JetBrainsLicenseObtainer.Services
{
    static class UserInfo
    {
        #region Methods

        /// <summary>
        /// Return response from request of url
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        private static IRestResponse ExecuteRestRequest(string baseUrl)
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return response;
        }

        /// <summary>
        /// Returns root of the XML document
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static XmlElement GetXmlRoot(string xml)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            XmlElement xRoot = xDoc.DocumentElement;

            return xRoot;
        }

        /// <summary>
        /// Generates full name with randomuser.me API
        /// </summary>
        /// <returns>Full name or null if the generation failed</returns>
        public static string GenerateFullName()
        {
            try
            {
                IRestResponse response = ExecuteRestRequest("https://randomuser.me/api/?inc=name&nat=us&noinfo&format=xml");
                XmlElement xRoot = GetXmlRoot(response.Content);
                XmlNode nameNode = xRoot.SelectSingleNode("//name");

                string firstName = nameNode.SelectSingleNode("//first").InnerText;
                string lastName = nameNode.SelectSingleNode("//last").InnerText;
                string fullName = firstName + " " + lastName;

                return fullName;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Generates email with randomuser.me API
        /// </summary>
        /// <returns>Email or null if the generation failed</returns>
        public static string GenerateEmail()
        {
            try
            {
                IRestResponse response = ExecuteRestRequest("https://randomuser.me/api/?inc=login&nat=us&noinfo&format=xml");
                XmlElement xRoot = GetXmlRoot(response.Content);
                string[] emailDomains = { "@gmail.com", "@outlook.com", "@yahoo.com", "@aol.com", "@yandex.ru" };

                Random rnd = new Random();
                string email = xRoot.SelectSingleNode("//login/username").InnerText + emailDomains[rnd.Next(emailDomains.Length)];

                return email;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Generates password with randomuser.me API
        /// </summary>
        /// <returns>Password or null if the generation failed</returns>
        public static string GeneratePassword()
        {
            try
            {
                IRestResponse response = ExecuteRestRequest("https://randomuser.me/api/?inc=login&password=upper,lower,number,10-18&nat=us&noinfo&format=xml");
                XmlElement xRoot = GetXmlRoot(response.Content);
                string password = xRoot.SelectSingleNode("//login/password").InnerText;

                return password;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Generates account
        /// </summary>
        /// <returns>Account or null if the generation failed</returns>
        public static Account GenerateAccount()
        {
            try
            {
                string fullName = GenerateFullName();
                string email = GenerateEmail();
                string password = GeneratePassword();

                return new Account(fullName, email, password, DateTime.Now);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
