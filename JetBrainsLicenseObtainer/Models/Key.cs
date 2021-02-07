using System;

namespace JetBrainsLicenseObtainer.Models
{
    public class Key
    {
        #region Properties

        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Account Account { get; set; }

        #endregion

        #region Constructor

        public Key(Account account, string licenseKey, DateTime registrationDate, DateTime expirationDate)
        {
            Account = account;
            LicenseKey = licenseKey;
            RegistrationDate = registrationDate;
            ExpirationDate = expirationDate;
        }

        #endregion
    }
}
