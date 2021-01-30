using System;

namespace JetBrainsLicenseObtainer.Models
{
    class Account
    {
        #region Properties

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        #endregion

        #region Constructor

        public Account(string fullName, string email, string password, DateTime registrationDate)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            RegistrationDate = registrationDate;
        }

        #endregion
    }
}
