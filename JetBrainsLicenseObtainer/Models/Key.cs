using System;

namespace JetBrainsLicenseObtainer.Models
{
    public class Key : KeyBase
    {
        #region Properties

        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime RegistrationDate { get; set; }

        #endregion

        #region Constructor

        public Key()
        {

        }

        public Key(string licenseKey, DateTime registrationDate, DateTime expirationDate)
        {
            LicenseKey = licenseKey;
            RegistrationDate = registrationDate;
            ExpirationDate = expirationDate;
        }

        #endregion
    }
}
